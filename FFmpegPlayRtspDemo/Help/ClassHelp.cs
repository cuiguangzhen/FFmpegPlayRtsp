using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using FFmpegPlayRtspDemo.Help;

namespace FFmpegPlayRtspDemo
{
    public class ClassHelp
    {
        public ClassHelp() { }
        private static ClassHelp _instance = null;
        public static ClassHelp Instance => _instance ?? (_instance = new ClassHelp());
       
        public FFmpegHelp FFmpegHelp
        {
            get
            {
                return _FFmpegHelp;
            }

            set
            {
                _FFmpegHelp = value;
            }
        }

        public bool IsNetwork
        {
            get
            {
                return _IsNetwork;
            }

            set
            {
                _IsNetwork = value;
            }
        }

        public bool IsAlert
        {
            get
            {
                return _IsAlert;
            }

            set
            {
                _IsAlert = value;
            }
        }

        private bool _IsNetwork = true;
        private bool _IsAlert = true;
       
        private FFmpegHelp _FFmpegHelp;

    }
}
