using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dlgEncoderGui.models
{
    public class linkPoint : ObservableObject
    {
        private Point _location;

        public linkPoint(Point location)
        {
            _location = location;
        }
        public linkPoint()
        {
            _location = new Point();
        }

        public Point location
        {
            get { return _location; }
            set { if(value!=null) _location = value; RaisePropertyChanged("location"); }
        } 

    }
}
