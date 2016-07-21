using dlgEncoderGui.models.repo;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dlgEncoderGui.models.scene
{
   public  class scene_animation : ObservableObject
    {
        private animation animation;
        private string asset_name;
        private scene_actor actor;

        public scene_animation(string name, animation animation, scene_actor actor)
        {
            Asset_name = name;
            Animation = animation;
            Actor = actor;
        }
        public scene_animation() { }

        public animation Animation
        {
            get
            {
                return animation;
            }

            set
            {
                animation = value; RaisePropertyChanged("Animation");
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
                asset_name = value; RaisePropertyChanged("Asset_name");
            }
        }

        public scene_actor Actor
        {
            get
            {
                return actor;
            }

            set
            {
                actor = value; RaisePropertyChanged("Actor");
            }
        }

       
    }
}
