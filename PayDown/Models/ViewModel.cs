using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using PayDown.Views;

namespace PayDown
{

    public class ViewModel : NotificationObject
    {
        private string appTille = "Pay Down Debt Destroyer";
        public string AppTitle
        {
            get { return appTille; }
            set
            {
                this.RaisePropertyChanged("AppTitle");
            }
        }


        private string connectStatus = "Connected";
        public string ConnectStatus
        {
            get { return connectStatus; }
            set
            {
                connectStatus = value;
                this.RaisePropertyChanged("ConnectStatus");
            }
        }

        private string connectColor = "DarkGreen";
        public string ConnectColor
        {
            get { return connectColor; }
            set
            {
                connectColor = value;
                this.RaisePropertyChanged("ConnectColor");
            }
        }

        public ViewModel()
        {
            Initialize();
        }


        private void Initialize()
        {

        }

        private Brush titleBarBackground = new SolidColorBrush(Color.FromRgb(43, 87, 154));
        private Brush titleBarForeground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        public Brush TitleBarBackground
        {
            get { return titleBarBackground; }
            set
            {
                titleBarBackground = value;
                this.RaisePropertyChanged("TitleBarBackground");
            }
        }
        public Brush TitleBarForeground
        {
            get { return titleBarForeground; }
            set
            {
                titleBarForeground = value;
                this.RaisePropertyChanged("TitleBarForeground");
            }
        }
    }

}
