using System;
using System.Diagnostics;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.Structure;

using DirectShowLib;
using Accord.Video.DirectShow;

//using SharpFFmpeg;
using FFmpeg.AutoGen;

namespace HttpStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        VideoCapture capture;
        VideoCaptureDevice CaptureDevice;
        VideoCapabilities[] _deviceCapabilites;
        bool isRunning = false;
        bool isStreaming = false;

        public int vidWidth = 1280;
        public int vidHeight = 720;
        public int frameRate = 10;
        Image<Bgr, byte> img;
        byte[] data;

        public unsafe AVCodec* codec;
        public unsafe AVCodecContext* c;
        public unsafe AVFrame* frame;
        public unsafe AVFrame* yuy2Frame;
        public unsafe AVFrame* gbrFrame;
        public unsafe AVPacket* pkt;
        public unsafe AVPacket* enPkt;
        public unsafe AVOutputFormat* ofmt;
        public unsafe AVFormatContext* ifmt_ctx;
        public unsafe AVFormatContext* ofmt_ctx;
        public unsafe AVDeviceCapabilitiesQuery* dcq;
        public unsafe AVDeviceInfoList* dil;
        public unsafe AVDictionary* avdic;
        public unsafe SwsContext* gbr_swctx;
        public unsafe SwsContext* yuv_swctx;

        bool encoderInit = false;

        UdpClient videoListener;
        int videoPort = 11000;
        int bitrate = 500000;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // set up byte array for display
            data = new byte[vidHeight * vidWidth * 3];
            img = new Image<Bgr, byte>(vidWidth, vidHeight);

            DeviceNames = new ObservableCollection<string>();
            GetVideoDevices();
            //InitEncoder();
            ffmpeg.avcodec_register_all();
            ffmpeg.av_register_all();
            ffmpeg.avdevice_register_all();
            ffmpeg.avfilter_register_all();

            GetAddresses();
        }

        void GetVideoDevices()
        {
            DeviceNames.Clear();
            //Find system video devices
            DsDevice[] _SystemCameras = DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice);
            foreach (DsDevice d in _SystemCameras)
            {
                DeviceNames.Add(d.Name);
            }
        }

        unsafe void InitEncoder()
        {
            codec = ffmpeg.avcodec_find_encoder(AVCodecID.AV_CODEC_ID_MPEG4);
            //codec = ffmpeg.avcodec_find_decoder(AVCodecID.AV_CODEC_ID_MPEG2VIDEO);
            if (codec->name == null)
            {
                Debug.Print("Error finding encoder!");
            }

            c = ffmpeg.avcodec_alloc_context3(codec);

            c->bit_rate = bitrate;
            c->width = vidWidth;
            c->height = vidHeight;
            AVRational dummy;
            dummy.num = 1;
            dummy.den = frameRate;
            c->time_base = dummy;
            dummy.num = frameRate;
            dummy.den = 1;
            c->framerate = dummy;

            c->gop_size = 200;
            c->max_b_frames = 1;
            c->pix_fmt = AVPixelFormat.AV_PIX_FMT_YUV420P;

            int ret = ffmpeg.avcodec_open2(c, codec, null);
            if (ret < 0)
            {
                Debug.Print("Could not open codec!");
            }

            frame = ffmpeg.av_frame_alloc();
            if (frame == null)
            {
                Debug.Print("Could not allocate video frame!");
            }

            frame->format = (int)AVPixelFormat.AV_PIX_FMT_YUV420P;
            frame->width = c->width;
            frame->height = c->height;

            ret = ffmpeg.av_frame_get_buffer(frame, 32);
            if (ret < 0)
                Debug.Print("Could not allocate video frame data!");
        }

        unsafe void InitDecoder()
        {
            codec = ffmpeg.avcodec_find_decoder(AVCodecID.AV_CODEC_ID_MPEG4);
            //codec = ffmpeg.avcodec_find_decoder(AVCodecID.AV_CODEC_ID_MPEG2VIDEO);
            if (codec->name == null)
            {
                Debug.Print("Error finding encoder!");
            }

            c = ffmpeg.avcodec_alloc_context3(codec);

            c->bit_rate = bitrate;
            c->width = vidWidth;
            c->height = vidHeight;

            c->pix_fmt = AVPixelFormat.AV_PIX_FMT_YUV420P;

            int ret = ffmpeg.avcodec_open2(c, codec, null);
            if (ret < 0)
            {
                Debug.Print("Could not open codec!");
            }

            frame = ffmpeg.av_frame_alloc();
            if (frame == null)
            {
                Debug.Print("Could not allocate video frame!");
            }

            frame->format = (int)AVPixelFormat.AV_PIX_FMT_YUV420P;
            frame->width = c->width;
            frame->height = c->height;

            ret = ffmpeg.av_frame_get_buffer(frame, 32);
            if (ret < 0)
                Debug.Print("Could not allocate video frame data!");

            gbrFrame = ffmpeg.av_frame_alloc();
            gbrFrame->format = (int)AVPixelFormat.AV_PIX_FMT_BGR24;
            gbrFrame->width = vidWidth;
            gbrFrame->height = vidHeight;
            ret = ffmpeg.av_frame_get_buffer(gbrFrame, 32);

            gbr_swctx = ffmpeg.sws_getContext(frame->width, frame->height, (AVPixelFormat)frame->format, gbrFrame->width, gbrFrame->height, (AVPixelFormat)gbrFrame->format, 4, null, null, null);
            if (gbr_swctx == null)
                Debug.Print("Error getting sws context!");
        }

        unsafe void CameraInit()
        {
            int res;
            ofmt = null;
            ifmt_ctx = null;
            ofmt_ctx = null;
            avdic = null;
            
            string fullDName = "video=" + selectedDeviceName;

            AVInputFormat* fmt = ffmpeg.av_find_input_format("dshow");

            string resString = vidWidth.ToString() + "x" + vidHeight.ToString();

            ifmt_ctx = ffmpeg.avformat_alloc_context();
            fixed (AVDictionary** pAVdic = &avdic)
            {
                res = ffmpeg.av_dict_set(pAVdic, "video_size", resString, 0);
                res = ffmpeg.av_dict_set(pAVdic, "pixel_format", "yuyv422", 0);
                fixed (AVFormatContext** pFmtCxt = &ifmt_ctx)
                {
                    res = ffmpeg.avformat_open_input(pFmtCxt, fullDName, fmt, pAVdic);
                }

                ffmpeg.av_dict_free(pAVdic);
            }
            if (res < 0)
            {
                Debug.Print("Unable to open input device!");
                return;
            }

            res = ffmpeg.avformat_find_stream_info(ifmt_ctx, null);
            ffmpeg.av_dump_format(ifmt_ctx, 0, fullDName, 0);

            yuy2Frame = ffmpeg.av_frame_alloc();
            if (yuy2Frame == null)
            {
                Debug.Print("Could not allocate video frame!");
            }

            yuy2Frame->format = (int)AVPixelFormat.AV_PIX_FMT_YUYV422;
            yuy2Frame->width = c->width;
            yuy2Frame->height = c->height;

            res = ffmpeg.av_frame_get_buffer(yuy2Frame, 32);
            if (res < 0)
                Debug.Print("Could not allocate video frame data!");

            gbrFrame = ffmpeg.av_frame_alloc();
            gbrFrame->format = (int)AVPixelFormat.AV_PIX_FMT_BGR24;
            gbrFrame->width = vidWidth;
            gbrFrame->height = vidHeight;
            res = ffmpeg.av_frame_get_buffer(gbrFrame, 32);

            gbr_swctx = ffmpeg.sws_getContext(yuy2Frame->width, yuy2Frame->height, (AVPixelFormat)yuy2Frame->format, gbrFrame->width, gbrFrame->height, (AVPixelFormat)gbrFrame->format, 4, null, null, null);
            if (gbr_swctx == null)
                Debug.Print("Error getting sws context!");

            yuv_swctx = ffmpeg.sws_getContext(yuy2Frame->width, yuy2Frame->height, (AVPixelFormat)yuy2Frame->format, frame->width, frame->height, (AVPixelFormat)frame->format, 4, null, null, null);
            if (gbr_swctx == null)
                Debug.Print("Error getting sws context!");
        }

        unsafe void GrabFrames()
        {
            int ret;

            // allocate video packet
            pkt = ffmpeg.av_packet_alloc();
            // read frame from capture device
            ret = ffmpeg.av_read_frame(ifmt_ctx, pkt);
            if (ret < 0)
            {
                Debug.Print("Error reading new frame!");
            }

            yuy2Frame->data[0] = pkt->data;
            // convert YUY2 to GBR
            ret = ffmpeg.sws_scale(gbr_swctx, yuy2Frame->data, yuy2Frame->linesize, 0, yuy2Frame->height, gbrFrame->data, gbrFrame->linesize);
            if (ret < 0)
                Debug.Print("Error during GBR scaling!");
            // convert YUY2 to YUV
            ret = ffmpeg.sws_scale(yuv_swctx, yuy2Frame->data, yuy2Frame->linesize, 0, yuy2Frame->height, frame->data, frame->linesize);
            if (ret < 0)
                Debug.Print("Error during YUV scaling!");

            // copy GBR data into byte array from pointer
            Marshal.Copy((IntPtr)gbrFrame->data[0], data, 0, vidHeight * vidWidth * 3);
            // free the packet
            fixed (AVPacket** pPkt = &pkt)
            {
                ffmpeg.av_packet_free(pPkt);
            }
            img.Bytes = data;
            VideoDisplay.Image = img;

            EncodeFrame();
        }

        unsafe void EncodeFrame()
        {
            int ret;
            if (!encoderInit)
            {
                ret = ffmpeg.avcodec_send_frame(c, frame);
                if (ret < 0)
                    Debug.Print("Error sending a frame for encoding!");

                encoderInit = true;
            }

            ret = ffmpeg.avcodec_send_frame(c, frame);
            if (ret < 0)
                Debug.Print("Error sending a frame for encoding!");

            enPkt = ffmpeg.av_packet_alloc();

            ret = ffmpeg.avcodec_receive_packet(c, enPkt);
            if (ret < 0)
                Debug.Print("Error during encoding!");

            //if (isStreaming)
            if(MyAddress != "")
                SendVideoFrame(enPkt);

            fixed (AVPacket** pPkt = &enPkt)
                ffmpeg.av_packet_free(pPkt);
        }

        unsafe void DecodePacket()
        {

        }

        unsafe void SendVideoFrame(AVPacket* enPacket)
        {
            byte[] byteArray = new byte[enPacket->size];
            Marshal.Copy((IntPtr)enPacket->data, byteArray, 0, enPacket->size);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                int num = s.SendTo(byteArray, new IPEndPoint(IPAddress.Parse(MyAddress), videoPort));
            }
            catch(SocketException ex)
            {
                Debug.Print(ex.Message);
            }
            return;
        }

        public void ListenForVideo()
        {
            if (videoListener == null)
                videoListener = new UdpClient(videoPort);

            videoListener.BeginReceive(new AsyncCallback(videoImgReceived), null);
        }

        unsafe private void videoImgReceived(IAsyncResult Ar)
        {
            int ret;
            IPEndPoint masterEP = new IPEndPoint(IPAddress.Parse(MyAddress), videoPort);
            byte[] array = videoListener.EndReceive(Ar, ref masterEP);

            pkt = ffmpeg.av_packet_alloc();
            pkt->size = array.Length;
            fixed(byte* pArray = &array[0])
                pkt->data = pArray;

            ret = ffmpeg.avcodec_send_packet(c, pkt);
            ret = ffmpeg.avcodec_receive_frame(c, frame);
            if (ret == 0)
            {
                ret = ffmpeg.sws_scale(gbr_swctx, frame->data, frame->linesize, 0, frame->height, gbrFrame->data, gbrFrame->linesize);
                if (ret < 0)
                    Debug.Print("Error during GBR scaling!");

                // copy GBR data into byte array from pointer
                Marshal.Copy((IntPtr)gbrFrame->data[0], data, 0, vidHeight * vidWidth * 3);
                // free the packet
                fixed (AVPacket** pPkt = &pkt)
                {
                    ffmpeg.av_packet_free(pPkt);
                }
                img.Bytes = data;
                VideoDisplay.Image = img;
            }

            ListenForVideo();
        }

        //public ObservableCollection<string> DeviceNames { get; set; }
        /// <summary>
        /// The <see cref="DeviceNames" /> property's name.
        /// </summary>
        public const string DeviceNamesPropertyName = "DeviceNames";

        private ObservableCollection<string> deviceNames = null;

        /// <summary>
        /// Sets and gets the DeviceNames property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> DeviceNames
        {
            get
            {
                return deviceNames;
            }

            set
            {
                if (deviceNames == value)
                {
                    return;
                }

                deviceNames = value;
                RaisePropertyChanged(DeviceNamesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedDeviceName" /> property's name.
        /// </summary>
        public const string SelectedDeviceNamePropertyName = "SelectedDeviceName";

        private string selectedDeviceName = null;

        /// <summary>
        /// Sets and gets the SelectedDeviceName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SelectedDeviceName
        {
            get
            {
                return selectedDeviceName;
            }

            set
            {
                if (selectedDeviceName == value)
                {
                    return;
                }

                selectedDeviceName = value;

                if (IsMaster)
                    InitEncoder();

                CameraInit();

                //DsDevice[] _SystemCameras = DsDevice.GetDevicesOfCat(DirectShowLib.FilterCategory.VideoInputDevice);

                //for (int i = 0; i < _SystemCameras.Length; i++)
                //{
                //    if (_SystemCameras[i].Name == selectedDeviceName)
                //    {
                //        //capture = new VideoCapture(i);
                //        CaptureDevice = new VideoCaptureDevice(_SystemCameras[i].Name);
                //        _deviceCapabilites = CaptureDevice.VideoCapabilities;
                //        SelectedSetting = 0;
                //        CreateCapabilityList();
                //        //capture.ImageGrabbed += ProcessFrame;
                //        break;
                //    }
                //}

                RaisePropertyChanged(SelectedDeviceNamePropertyName);
            }
        }

        void CreateCapabilityList()
        {
            string dummyString = "";
            SettingNames.Clear();
            foreach (VideoCapabilities settings in _deviceCapabilites)
            {
                dummyString = "";
                dummyString = settings.FrameSize.Width + "x" + settings.FrameSize.Height + " " + settings.AverageFrameRate + "FPS";
                SettingNames.Add(dummyString);
            }
        }

        /// <summary>
        /// The <see cref="SettingNames" /> property's name.
        /// </summary>
        public const string SettingNamesPropertyName = "SettingNames";

        private ObservableCollection<string> settingNames = new ObservableCollection<string>();

        /// <summary>
        /// Sets and gets the SettingNames property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> SettingNames
        {
            get
            {
                return settingNames;
            }

            set
            {
                if (settingNames == value)
                {
                    return;
                }

                settingNames = value;
                RaisePropertyChanged(SettingNamesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedSetting" /> property's name.
        /// </summary>
        public const string SelectedSettingPropertyName = "SelectedSetting";

        private int selectedSetting = 0;

        /// <summary>
        /// Sets and gets the SelectedSetting property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SelectedSetting
        {
            get
            {
                return selectedSetting;
            }

            set
            {
                if (selectedSetting == value)
                {
                    return;
                }

                selectedSetting = value;
                RaisePropertyChanged(SelectedSettingPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsMaster" /> property's name.
        /// </summary>
        public const string IsMasterPropertyName = "IsMaster";

        private bool isMaster = false;

        /// <summary>
        /// Sets and gets the IsMaster property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsMaster
        {
            get
            {
                return isMaster;
            }

            set
            {
                if (isMaster == value)
                {
                    return;
                }

                isMaster = value;
                RaisePropertyChanged(IsMasterPropertyName);
            }
        }

        void GetAddresses()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddresses.Clear();
            foreach (IPAddress ip in host.AddressList)
            {
                IPAddresses.Add(ip.ToString());
            }
        }

        /// <summary>
        /// The <see cref="IPAddresses" /> property's name.
        /// </summary>
        public const string IPAddressesPropertyName = "IPAddresses";

        private ObservableCollection<string> iPAddresses = new ObservableCollection<string>();

        /// <summary>
        /// Sets and gets the IPAddresses property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<string> IPAddresses
        {
            get
            {
                return iPAddresses;
            }

            set
            {
                if (iPAddresses == value)
                {
                    return;
                }

                iPAddresses = value;
                RaisePropertyChanged(IPAddressesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MyAddress" /> property's name.
        /// </summary>
        public const string MyAddressPropertyName = "MyAddress";

        private string myaddress = "";

        /// <summary>
        /// Sets and gets the MyAddress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string MyAddress
        {
            get
            {
                return myaddress;
            }

            set
            {
                if (myaddress == value)
                {
                    return;
                }

                myaddress = value;
                RaisePropertyChanged(MyAddressPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ConnectButtonText" /> property's name.
        /// </summary>
        public const string ConnectButtonTextPropertyName = "ConnectButtonText";

        private string connectButtonText = "Connect";

        /// <summary>
        /// Sets and gets the ConnectButtonText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ConnectButtonText
        {
            get
            {
                return connectButtonText;
            }

            set
            {
                if (connectButtonText == value)
                {
                    return;
                }

                connectButtonText = value;
                RaisePropertyChanged(ConnectButtonTextPropertyName);
            }
        }

        private RelayCommand<string> startCommand;

        /// <summary>
        /// Gets the DetectCOMsCommand.
        /// </summary>
        unsafe public RelayCommand<string> StartCommand
        {
            get
            {
                return startCommand
                    ?? (startCommand = new RelayCommand<string>(
                    p =>
                    {
                        if (isRunning)
                        {
                            ConnectButtonText = "Connect";
                            isRunning = false;
                        }
                        else
                        {
                            ConnectButtonText = "Disconnect";
                            isRunning = true;

                            if (!IsMaster)
                                InitDecoder();

                            Task t = Task.Run(() =>
                            {
                                if (IsMaster)
                                    while (isRunning)
                                    {
                                        GrabFrames();
                                    }
                                else
                                    ListenForVideo();
                            });
                        }
                    }));
            }
        }

        private RelayCommand<string> streamCommand;

        /// <summary>
        /// Gets the DetectCOMsCommand.
        /// </summary>
        unsafe public RelayCommand<string> StreamCommand
        {
            get
            {
                return startCommand
                    ?? (startCommand = new RelayCommand<string>(
                    p =>
                    {
                        if (isRunning)
                        {
                            isStreaming = !isStreaming;
                        }
                    }));
            }
        }

        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
