using System;
using System.Diagnostics;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
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

        public int vidWidth = 1920;
        public int vidHeight = 1080;
        public int frameRate = 30;

        public unsafe AVCodec* codec;
        public unsafe AVCodecContext* c;
        public unsafe AVFrame* frame;
        public unsafe AVPacket* pkt;
        public unsafe AVPacket* enPkt;
        public unsafe AVOutputFormat* ofmt;
        public unsafe AVFormatContext* ifmt_ctx;
        public unsafe AVFormatContext* ofmt_ctx;
        public unsafe AVDeviceCapabilitiesQuery* dcq;
        public unsafe AVDeviceInfoList* dil;
        public unsafe AVDictionary* avdic;
        public int VideoStream;

        byte[] Udata;
        byte[] Vdata;
        bool UorV = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Udata = new byte[vidHeight * vidWidth];
            Vdata = new byte[vidHeight * vidWidth];

            DeviceNames = new ObservableCollection<string>();
            GetVideoDevices();
            InitEncoder();
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

        unsafe void CameraInit()
        {
            int res;
            AVIOContext* ioctx;
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
                res = ffmpeg.av_dict_set(pAVdic, "pixel_format", "none", 0);
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
            //return;
            //ffmpeg.av_find_best_stream(ifmt_ctx, AVMediaType.AVMEDIA_TYPE_VIDEO, -1, -1, null, 0);
        }

        unsafe void GrabFrames()
        {
            int ret;
            byte* pData;
            byte[] YUVdata = new byte[vidWidth * vidHeight * 4];
            byte[] Ydata = new byte[vidWidth * vidHeight];
            byte[] BGRdata = new byte[vidWidth * vidHeight * 3];

            pkt = ffmpeg.av_packet_alloc();
            ret = ffmpeg.av_read_frame(ifmt_ctx, pkt);
            if (ret < 0)
            {
                Debug.Print("Error reading new frame1!");
            }
            pData = pkt->data;

            ret = ffmpeg.av_frame_make_writable(frame);
            if (ret < 0)
            {
                Debug.Print("Cannot make frame writeable1!");
            }
            frame->data[0] = pData;

            ret = ffmpeg.avcodec_send_frame(c, frame);
            if (ret < 0)
            {
                Debug.Print("Error sending a frame for encoding1!");
            }

            //////////////////////////////////////////////
            ret = ffmpeg.av_read_frame(ifmt_ctx, pkt);
            if (ret < 0)
            {
                Debug.Print("Error reading new frame2!");
            }
            pData = pkt->data;

            ret = ffmpeg.av_frame_make_writable(frame);
            if (ret < 0)
            {
                Debug.Print("Cannot make frame writeable2!");
            }
            frame->data[0] = pData;

            ret = ffmpeg.avcodec_send_frame(c, frame);
            if (ret < 0)
            {
                Debug.Print("Error sending a frame for encoding2!");
            }
            ////////////////////////////////////////////////

            enPkt = ffmpeg.av_packet_alloc();
            ret = 0;
            //while (ret >= 0)
            //{
                ret = ffmpeg.avcodec_receive_packet(c, enPkt);
                if (ret < 0)
                    Debug.Print("Error during encoding!");
            //}

            fixed (AVPacket** pPkt = &enPkt)
            {
                ffmpeg.av_packet_free(pPkt);
            }
            ///////////////////////////////////////////////////

            byte[] data = new byte[pkt->size];
            Marshal.Copy((IntPtr)pData, data, 0, pkt->size);

            fixed (AVPacket** pPkt = &pkt)
            {
                ffmpeg.av_packet_free(pPkt);
            }
            for (int i = 0; i < Ydata.Length; i++)
            {
                Ydata[i] = data[2 * i];
                //if(UorV)
                //    Udata[i] = data[2 * i + 1];
                //else
                //    Vdata[i] = data[2 * i + 1];
            }

            Image<Gray, byte> img = new Image<Gray, byte>(vidWidth, vidHeight);

            //Image<Bgr, byte> img = new Image<Bgr, byte>(vidWidth, vidHeight);
            //for (int i = 0; i < Ydata.Length; i++)
            //{
            //    YUVdata[4 * i] = Ydata[i];
            //    YUVdata[4 * i + 1] = Vdata[i];
            //    YUVdata[4 * i + 2] = Ydata[i];
            //    YUVdata[4 * i + 3] = Udata[i];
            //}

            //for (int i = 0; i < Ydata.Length; i++)
            //{
            //    BGRdata[3 * i] = (byte)Math.Round(1.164 * (Ydata[i] - 16) + 2.018 * (Udata[i] - 128));
            //    BGRdata[3 * i + 1] = (byte)Math.Round(1.164 * (Ydata[i] - 16) - 0.813 * (Vdata[i] - 128) - 0.391 * (Udata[i] - 128));
            //    BGRdata[3 * i + 2] = (byte)Math.Round(1.164 * (Ydata[i] - 16) + 1.596 * (Vdata[i] - 128));
            //}

            //img.Bytes = BGRdata;
            img.Bytes = Ydata;

            VideoDisplay.Image = img;
            //EncodePacket();
            //EncodeFrame();
            //AVStream* inStream;
            //frame->data.UpdateFrom(pkt->data);
            //inStream = ifmt_ctx->streams[pkt->stream_index];
        }

        unsafe void InitEncoder()
        {
            ffmpeg.avcodec_register_all();
            ffmpeg.av_register_all();
            ffmpeg.avdevice_register_all();
            ffmpeg.avfilter_register_all();

            codec = ffmpeg.avcodec_find_encoder(AVCodecID.AV_CODEC_ID_MPEG4);
            if(codec->name == null)
            {
                Debug.Print("Error finding encoder!");
            }

            c = ffmpeg.avcodec_alloc_context3(codec);
            //pkt = ffmpeg.av_packet_alloc();

            c->bit_rate = 400000;
            c->width = vidWidth;
            c->height = vidHeight;
            AVRational dummy;
            dummy.num = 1;
            dummy.den = frameRate;
            c->time_base = dummy;
            dummy.num = frameRate;
            dummy.den = 1;
            c->framerate = dummy;

            c->gop_size = 10;
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

            frame->format = (int)c->pix_fmt;
            frame->width = c->width;
            frame->height = c->height;

            ret = ffmpeg.av_frame_get_buffer(frame, 32);
            if (ret < 0)
                Debug.Print("Could not allocate video frame data!");
        }

        unsafe void EncodeFrame()
        {
            int err = ffmpeg.avcodec_send_frame(c, frame);
            if (err < 0)
            {
                Debug.Print("Error sending a frame for encoding!");
                return;
            }

            enPkt = ffmpeg.av_packet_alloc();

            while ( err >= 0)
            {
                err = ffmpeg.avcodec_receive_packet(c, enPkt);
                if (err < 0)
                    Debug.Print("Error during encoding!");
            }

            fixed (AVPacket** pPkt = &pkt)
            {
                ffmpeg.av_packet_free(pPkt);
            }

            fixed (AVPacket** pPkt = &enPkt)
            {
                ffmpeg.av_packet_free(pPkt);
            }
        }

        unsafe void EncodePacket()
        {
            int err = ffmpeg.avcodec_send_packet(c, pkt);
            if (err < 0)
            {
                Debug.Print("Error sending a frame for encoding!");
                return;
            }

            enPkt = ffmpeg.av_packet_alloc();

            while (err >= 0)
            {
                err = ffmpeg.avcodec_receive_packet(c, enPkt);
                if (err < 0)
                    Debug.Print("Error during encoding!");
            }

            fixed (AVPacket** pPkt = &pkt)
            {
                ffmpeg.av_packet_free(pPkt);
            }
            fixed (AVPacket** pPkt = &enPkt)
            {
                ffmpeg.av_packet_free(pPkt);
            }
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

                            Task t = Task.Run(() =>
                            {
                                while (isRunning)
                                {
                                    GrabFrames();
                                    //Mat rawFrame = capture.QueryFrame();
                                    //Image<Ycc, ushort> img = rawFrame.ToImage<Ycc, ushort>();
                                    //VideoDisplay.Image = img;

                                    //byte[] bytes = img.Bytes;

                                    //var handles = new GCHandle[bytes.Length];
                                    //byte*[] pBytes = new byte*[bytes.Length];

                                    //for (int i = 0; i < bytes.Length; ++i)
                                    //{
                                    //    handles[i] = GCHandle.Alloc(bytes[i], GCHandleType.Pinned);
                                    //    pBytes[i] = (byte*)handles[i].AddrOfPinnedObject();
                                    //}

                                    //frame->data.UpdateFrom(pBytes);
                                    //EncodeFrame();
                                }
                            });
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
