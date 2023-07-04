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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            player1.RtspUrl = "rtsp://admin:hk123456@192.168.4.52:554/h264/ch1/main/av_stream";
            player2.RtspUrl = "rtsp://admin:hk123456@192.168.4.57:554/h264/ch1/main/av_stream";
            player3.RtspUrl = "rtsp://admin:hk123456@192.168.4.58:554/h264/ch1/main/av_stream";
            player4.RtspUrl = "rtsp://admin:hk123456@192.168.4.60:554/h264/ch1/main/av_stream";
        }
    }
}
