using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.CV.Structure;

using DirectShowLib;

namespace HttpStream
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        VideoCapture capture;
        bool isRunning = false;
        FilterGraph encoder = new FilterGraph();
        IGraphBuilder graphBuilder;
        IMediaControl mediaControl;
        IBaseFilter theDevice = null;
        IBaseFilter theCompressor = null;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            
            DeviceNames = new ObservableCollection<string>();
            GetVideoDevices();
        }

        void InitGraph()
        {
            //Create the Graph
            graphBuilder = (IGraphBuilder)new FilterGraph();

            //Create the Capture Graph Builder
            ICaptureGraphBuilder2 captureGraphBuilder = null;
            captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();

            //Create the media control for controlling the graph
            mediaControl = (IMediaControl)this.graphBuilder;

            // Attach the filter graph to the capture graph
            int hr = captureGraphBuilder.SetFiltergraph(this.graphBuilder);
            DsError.ThrowExceptionForHR(hr);

            //Add the Video input device to the graph
            hr = graphBuilder.AddFilter(theDevice, "source filter");
            DsError.ThrowExceptionForHR(hr);

            //Add the Video compressor filter to the graph
            hr = graphBuilder.AddFilter(theCompressor, "compressor filter");
            DsError.ThrowExceptionForHR(hr);

            //Create the file writer part of the graph. SetOutputFileName does this for us, and returns the mux and sink
            IBaseFilter mux;
            IFileSinkFilter sink;
            //hr = captureGraphBuilder.SetOutputFileName(MediaSubType.Avi, textBox1.Text, out mux, out sink);
            DsError.ThrowExceptionForHR(hr);


            //Render any preview pin of the device
            hr = captureGraphBuilder.RenderStream(PinCategory.Preview, MediaType.Video, theDevice, null, null);
            DsError.ThrowExceptionForHR(hr);

            //Connect the device and compressor to the mux to render the capture part of the graph
            //hr = captureGraphBuilder.RenderStream(PinCategory.Capture, MediaType.Video, theDevice, theCompressor, mux);
            DsError.ThrowExceptionForHR(hr);

//#if DEBUG
//            m_rot = new DsROTEntry(graphBuilder);
//#endif

            //get the video window from the graph
            IVideoWindow videoWindow = null;
            videoWindow = (IVideoWindow)graphBuilder;

            ////Set the owener of the videoWindow to an IntPtr of some sort (the Handle of any control - could be a form / button etc.)
            //hr = videoWindow.put_Owner(panel1.Handle);
            //DsError.ThrowExceptionForHR(hr);

            ////Set the style of the video window
            //hr = videoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
            //DsError.ThrowExceptionForHR(hr);

            //// Position video window in client rect of main application window
            //hr = videoWindow.SetWindowPosition(0, 0, panel1.Width, panel1.Height);
            //DsError.ThrowExceptionForHR(hr);

            //// Make the video window visible
            //hr = videoWindow.put_Visible(OABool.True);
            //DsError.ThrowExceptionForHR(hr);

            //Marshal.ReleaseComObject(mux);
            //Marshal.ReleaseComObject(sink);
            //Marshal.ReleaseComObject(captureGraphBuilder);
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
        }

        private void ProcessFrame(object sender, System.EventArgs e)
        {
            Mat frame = capture.QueryFrame();
            Image<Bgr, byte> img = frame.ToImage<Bgr, byte>();
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
                                    Mat frame = capture.QueryFrame();
                                    Image<Bgr, byte> img = frame.ToImage<Bgr, byte>();
                                    VideoDisplay.Image = img;
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
