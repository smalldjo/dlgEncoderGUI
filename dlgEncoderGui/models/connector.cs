using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace dlgEncoderGui.models
{
   public  class connector : ObservableObject
    {
       
        objectWithLinks _end;
        objectWithLinks _start;
        linkPoint _point1;
        linkPoint _point2;
        linkPoint _point3;
        linkPoint _point4;
        linkPoint _midPoint;

        public connector()
        {
            _start = new objectWithLinks();
            _end = new objectWithLinks();
            _midPoint = new linkPoint();
            
        }
        public connector(objectWithLinks start, objectWithLinks end)
        {
            _end = end;
            _start = start;
            updateMidPoint();
        }

        #region properties

        public objectWithLinks end
        {
            get { return _end; }
            set { if (value != null) _end = value; RaisePropertyChanged("end"); }
        }

        public objectWithLinks start
        {
            get { return _start; }
            set { if (value != null) _start = value; RaisePropertyChanged("start");  }
        }
        public linkPoint midPoint
        {
            get { return _midPoint;}
            set { _midPoint = value;RaisePropertyChanged("midPoint"); }
        }

        public linkPoint Point1
        {
            get
            {
                return _point1;
            }

            set
            {
                _point1 = value; RaisePropertyChanged("Point1");
            }
        }

        public linkPoint Point2
        {
            get
            {
                return _point2;
            }

            set
            {
                _point2 = value; RaisePropertyChanged("Point2");
            }
        }

        public linkPoint Point3
        {
            get
            {
                return _point3;
            }

            set
            {
                _point3 = value; RaisePropertyChanged("Point3");
            }
        }

        public linkPoint Point4
        {
            get
            {
                return _point4;
            }

            set
            {
                _point4 = value; RaisePropertyChanged("Point4");
            }
        }
        #endregion
        // public void 

        public void updateMidPoint()
        {
            int correction;

            midPoint  = new linkPoint();
            Point1 = new linkPoint();
            Point2 = new linkPoint();
            Point3 = new linkPoint();
            Point4 = new linkPoint();

            Point1.location = new Point(start.outPort.location.X + 50, start.outPort.location.Y);
            

            if (start.outPort.location.Y - end.inPort.location.Y > 50)
                correction = -1;
            else
                correction = 1;

            Point2.location = new Point(start.outPort.location.X + 50, start.outPort.location.Y + (correction) * 120);



            Point3.location = new Point(end.inPort.location.X - 50, end.inPort.location.Y);


            if (start.outPort.location.Y - end.inPort.location.Y < 50)
                correction = 1;
            else
                correction = -1;

            Point4.location = new Point(end.inPort.location.X- 50, end.inPort.location.Y + (correction) * 120);


            midPoint.location =  new Point((_start.outPort.location.X + _end.inPort.location.X) / 2, (_start.outPort.location.Y + _end.inPort.location.Y+100) / 2);
        }
    }
}