using dlgEncoderGui.models.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace dlgEncoderGui.models.scene
{
   public  class scene_camera : ObservableObject
    {
        private camera camera;
        private string asset_name;

        public scene_camera(string name, camera cam)
        {
            Asset_name = name;
            Camera = cam;
        }
        public scene_camera() { }


        public camera Camera
        {
            get
            {
                return camera;
            }

            set
            {
                camera = value;RaisePropertyChanged("Camera");
            }
        }

        public string Asset_name
        {
            get
            {
                return asset_name;
            }

            set
            {
                asset_name = value; RaisePropertyChanged("Asset_Name");
            }
        }

       
    }
}
