using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace dlgEncoderGui.models.repo
{
   public  class camera : ObservableObject
    {

        public camera(string name,int fov,Point3D pos,Point3D rot)
        {
            Name = name;
            Fov = fov;
            Transform_positionX = pos.X;
            Transform_positionY = pos.Y;
            Transform_positionZ = pos.Z;

            Transform_rotationX = rot.X;
            Transform_rotationY = rot.Y;
            Transform_rotationZ = rot.Z;
        }
        public camera() { }

        private string name;
        private int fov;

        private double transform_positionZ;
        private double transform_positionX;
        private double transform_positionY;

        private double transform_rotationX;
        private double transform_rotationY;
        private double transform_rotationZ;

        private int zoom;

        private double dof_apertureX;
        private double dof_apertureY;


        private double dof_blurX;
        private double dof_blurY;


        private double dof_focusX;
        private double dof_focusY;


        private int dof_intensity;
        private int source_height;
        private string source_slotname;

        private double target_vectorX;
        private double target_vectorY;
        private double target_vectorZ;
        private double target_vectorW;

        private string target_slotname;
        private string event_plane;
        private string event_tag;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;RaisePropertyChanged("Name");
            }
        }

        public int Fov
        {
            get
            {
                return fov;
            }

            set
            {
                fov = value; RaisePropertyChanged("Fov");
            }
        }

        public double Transform_positionZ
        {
            get
            {
                return transform_positionZ;
            }

            set
            {
                transform_positionZ = value; RaisePropertyChanged("Transform_positionZ");
            }
        }

        public double Transform_positionX
        {
            get
            {
                return transform_positionX;
            }

            set
            {
                transform_positionX = value; RaisePropertyChanged("Transform_positionX");
            }
        }

        public double Transform_positionY
        {
            get
            {
                return transform_positionY;
            }

            set
            {
                transform_positionY = value; RaisePropertyChanged("Transform_positionY");
            }
        }

        public double Transform_rotationX
        {
            get
            {
                return transform_rotationX;
            }

            set
            {
                transform_rotationX = value; RaisePropertyChanged("Transform_rotationX");
            }
        }

        public double Transform_rotationY
        {
            get
            {
                return transform_rotationY;
            }

            set
            {
                transform_rotationY = value; RaisePropertyChanged("Transform_rotationY");
            }
        }

        public double Transform_rotationZ
        {
            get
            {
                return transform_rotationZ;
            }

            set
            {
                transform_rotationZ = value; RaisePropertyChanged("Transform_rotationZ");
            }
        }

        public int Zoom
        {
            get
            {
                return zoom;
            }

            set
            {
                zoom = value; RaisePropertyChanged("Zoom");
            }
        }

        public double Dof_apertureX
        {
            get
            {
                return dof_apertureX;
            }

            set
            {
                dof_apertureX = value; RaisePropertyChanged("Dof_apertureX");
            }
        }

        public double Dof_apertureY
        {
            get
            {
                return dof_apertureY;
            }

            set
            {
                dof_apertureY = value; RaisePropertyChanged("Dof_apertureY");
            }
        }

        public double Dof_blurX
        {
            get
            {
                return dof_blurX;
            }

            set
            {
                dof_blurX = value; RaisePropertyChanged("Dof_blurX");
            }
        }

        public double Dof_blurY
        {
            get
            {
                return dof_blurY;
            }

            set
            {
                dof_blurY = value; RaisePropertyChanged("Dof_blurY");
            }
        }

        public double Dof_focusX
        {
            get
            {
                return dof_focusX;
            }

            set
            {
                dof_focusX = value; RaisePropertyChanged("Dof_focusX");
            }
        }

        public double Dof_focusY
        {
            get
            {
                return dof_focusY;
            }

            set
            {
                dof_focusY = value; RaisePropertyChanged("Dof_focusY");
            }
        }

        public int Dof_intensity
        {
            get
            {
                return dof_intensity;
            }

            set
            {
                dof_intensity = value; RaisePropertyChanged("Dof_intensity");
            }
        }

        public int Source_height
        {
            get
            {
                return source_height;
            }

            set
            {
                source_height = value; RaisePropertyChanged("Source_height");
            }
        }

        public string Source_slotname
        {
            get
            {
                return source_slotname;
            }

            set
            {
                source_slotname = value; RaisePropertyChanged("Source_slotname");
            }
        }

        public double Target_vectorX
        {
            get
            {
                return target_vectorX;
            }

            set
            {
                target_vectorX = value; RaisePropertyChanged("Target_vectorX");
            }
        }

        public double Target_vectorY
        {
            get
            {
                return target_vectorY;
            }

            set
            {
                target_vectorY = value; RaisePropertyChanged("Target_vectorY");
            }
        }

        public double Target_vectorZ
        {
            get
            {
                return target_vectorZ;
            }

            set
            {
                target_vectorZ = value; RaisePropertyChanged("Target_vectorZ");
            }
        }

        public double Target_vectorW
        {
            get
            {
                return target_vectorW;
            }

            set
            {
                target_vectorW = value; RaisePropertyChanged("Target_vectorW");
            }
        }

        public string Target_slotname
        {
            get
            {
                return target_slotname;
            }

            set
            {
                target_slotname = value; RaisePropertyChanged("Target_slotname");
            }
        }

        public string Event_plane
        {
            get
            {
                return event_plane;
            }

            set
            {
                event_plane = value; RaisePropertyChanged("Event_plane");
            }
        }

        public string Event_tag
        {
            get
            {
                return event_tag;
            }

            set
            {
                event_tag = value; RaisePropertyChanged("Event_tag");
            }
        }
    }
}
