using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using dlgEncoderGui.ViewModel;
using System.Windows;
using System.IO;

namespace dlgEncoderGui.models
{
   abstract public class section_model : objectWithLinks
    {
        protected string _name;
        public section type { get; set; }
         
        private Point _location;
        private double _z; //hack zindex

        public section_model()
        {
            _name = "section_" + Path.GetRandomFileName().Replace(".", ""); 
            _location = new Point();
            inPort = new linkPoint(_location);
            _z = 5;//hack zindex
        }

        #region properties

        public string name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("name");}
        }


        public Point location
        {
            get { return (_location); }
            set { _location = value; RaisePropertyChanged("location");updatePorts(); }
        }

        public double Z
        {
            get
            {
                return _z;
            }

            set
            {
                _z = value;RaisePropertyChanged("Z");
            }
        }
        #endregion

        #region commands

        #endregion

        virtual protected  void updatePorts()
        {
            inPort.location = new Point(location.X+5, location.Y + 102.5+5);//TODO design change and this is fucked up
        }
    }
}
