﻿using System;
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

using SharpFFmpeg;

namespace HttpStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        VideoCapture capture;
        bool isRunning = false;

        FFmpeg.AVCodec codec;
        FFmpeg.AVCodecContext c;
        FFmpeg.AVFrame frame;
        FFmpeg.AVPacket pkt;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            DeviceNames = new ObservableCollection<string>();
            GetVideoDevices();
        }

        void GetVideoDevices()
        {
            DeviceNames.Clear();
            //Find system video devices
            DsDevice[] _SystemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            foreach (DsDevice d in _SystemCameras)
            {
                DeviceNames.Add(d.Name);
            }
            InitEncoder();
        }

        void InitEncoder()
        {
            string dir = Environment.SystemDirectory;
            FFmpeg.avcodec_register_all();

            codec = FFmpeg.avcodec_find_encoder(FFmpeg.AVCodecID.AV_CODEC_ID_MPEG4);
            c = FFmpeg.avcodec_alloc_context3(codec);
            pkt = FFmpeg.av_packet_alloc();

            c.bit_rate = 400000;
            c.width = 640;
            c.height = 480;
            FFmpeg.AVRational dummy;
            dummy.num = 1;
            dummy.den = 30;
            c.time_base = dummy;
            dummy.num = 30;
            dummy.den = 1;
            c.framerate = dummy;

            c.gop_size = 10;
            c.max_b_frames = 1;
            c.pix_fmt = FFmpeg.AVPixelFormat.AV_PIX_FMT_RGB24;

            int ret = FFmpeg.avcodec_open2(c, codec, IntPtr.Zero);
            if (ret < 0)
            {
                Debug.Print("Could not open codec!");
            }

            frame = FFmpeg.av_frame_alloc();
            if(frame == null)
            {
                Debug.Print("Could not allocate video frame!");
            }

            frame.format = (int)c.pix_fmt;
            frame.width = c.width;
            frame.height = c.height;

            //ret = FFmpeg.av_frame_get_buffer(frame, 32);
            //if (ret < 0)
            //    Debug.Print("Could not allocate video frame data!");
        }

        void Encode()
        {
            int err = FFmpeg.avcodec_send_frame(c, frame);
            if (err > 0)
                Debug.Print("Error sending a frame for encoding!");

            while( err >= 0)
            {
                err = FFmpeg.avcodec_receive_packet(c, pkt);
                if (err < 0)
                    Debug.Print("Error during encoding!");
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

                DsDevice[] _SystemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

                for (int i = 0; i < _SystemCameras.Length; i++)
                {
                    if (_SystemCameras[i].Name == selectedDeviceName)
                    {
                        capture = new VideoCapture(i);
                        //capture.ImageGrabbed += ProcessFrame;
                        break;
                    }
                }

                RaisePropertyChanged(SelectedDeviceNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SettingNames" /> property's name.
        /// </summary>
        public const string SettingNamesPropertyName = "SettingNames";

        private ObservableCollection<string> settingNames = null;

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
        public RelayCommand<string> StartCommand
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
                                    Mat rawFrame = capture.QueryFrame();
                                    Image<Bgr, byte> img = rawFrame.ToImage<Bgr, byte>();
                                    VideoDisplay.Image = img;

                                    //int size = Marshal.ReadInt32(rawFrame.Total) * rawFrame.ElementSize;
                                    //byte[] bytes = new byte[size];

                                    //Array.Copy(rawFrame.Data, bytes, size);

                                    //frame.data = bytes;
                                    //Encode();
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
