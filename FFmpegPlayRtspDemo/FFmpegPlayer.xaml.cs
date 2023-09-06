using FFmpeg.AutoGen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FFmpegPlayRtspDemo
{
    /// <summary>
    /// FFmpegPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class FFmpegPlayer : UserControl
    {
        private string _RtspUrl;

        public string RtspUrl
        {
            get { return _RtspUrl; }
            set
            {
                _RtspUrl = value;
                Play();
            }
        }

        public bool CanRun;
        Thread thPlayer;

        public FFmpegPlayer()
        {
            InitializeComponent();
        }

        private void Play()
        {
            if (string.IsNullOrEmpty(RtspUrl))
                return;
            thPlayer = new Thread(DeCoding);
            thPlayer.SetApartmentState(ApartmentState.MTA);//设置单线程
            thPlayer.IsBackground = true;
            thPlayer.Start();

        }

        object _bitMapLocker = new object();
        FFmpegHelp _ffmpegHelp = null;
        private unsafe void DeCoding()
        {
            try
            {
                _ffmpegHelp = new FFmpegHelp();
                _ffmpegHelp.Register();
                string strResult = "";
                lock (_bitMapLocker)
                {
                    // 更新图片显示
                    FFmpegHelp.ShowBitmap show = (bmp) =>
                    {
                        Dispatcher.BeginInvoke(new Action(() =>
                        {
                            if (bmp != null)
                            {
                                pic.Image = bmp;
                                //ImageSource videSource = ImageSourceForBitmap(bmp);
                                //this.videodest01.Source = videSource;
                            }
                        }));
                    };
                    _ffmpegHelp.Start(show, RtspUrl, out strResult);
                    if (!string.IsNullOrEmpty(strResult))
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            MessageBox.Show($"错误信息:{strResult}");
                        }));
                    }
                }
            }
            catch(Exception ex)
            {

            }
            //finally
            //{
            //    thPlayer.DisableComObjectEagerCleanup();
            //    thPlayer = null;
            //    thPlayer = new Thread(DeCoding);
            //    thPlayer.IsBackground = true;
            //    thPlayer.Start();
            //}
        }

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public ImageSource ImageSourceForBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                ImageSource newSource = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(handle);
                return newSource;
            }
            catch (Exception ex)
            {
                DeleteObject(handle);
                return null;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            thPlayer.Abort();
            //Thread.Sleep(200);
            _ffmpegHelp.Stop();
        }
    }
}
