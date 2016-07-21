using dlgEncoderGui.models.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace dlgEncoderGui.models.scene
{
     public  class scene_actor : ObservableObject
    {
        private entity entity;
        private string asset_name;
        private scene_camera default_camera;
        private mimic default_mimic;
        private bool by_voicetag;
        private bool force_spawn;
        private List<string> tags;
        private bool isPlayer;

        public entity Entity
        {
            get
            {
                return entity;
            }

            set
            {
                entity = value;RaisePropertyChanged("Entity");
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

        public mimic Default_mimic
        {
            get
            {
                return default_mimic;
            }

            set
            {
                default_mimic = value; RaisePropertyChanged("Default_mimic");
            }
        }

        public bool By_voicetag
        {
            get
            {
                return by_voicetag;
            }

            set
            {
                by_voicetag = value; RaisePropertyChanged("By_voicetag");
            }
        }

        public bool Force_spawn
        {
            get
            {
                return force_spawn;
            }

            set
            {
                force_spawn = value; RaisePropertyChanged("Force_spawn");
            }
        }

        public List<string> Tags
        {
            get
            {
                return tags;
            }

            set
            {
                tags = value; RaisePropertyChanged("Tags");
            }
        }

        public scene_camera Default_camera
        {
            get
            {
                return default_camera;
            }

            set
            {
                default_camera = value;RaisePropertyChanged("Default_camera");
            }
        }

        public bool IsPlayer
        {
            get
            {
                return isPlayer;
            }

            set
            {
                isPlayer = value;RaisePropertyChanged("IsPlayer");
            }
        }

        public scene_actor()
        {

        }
    }
}
