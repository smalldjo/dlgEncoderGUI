using dlgEncoderGui.models.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace dlgEncoderGui.models.scene
{
   public class scene_mimic : ObservableObject
    {
        private mimic mimic;
        private string asset_name;
        private scene_actor actor;


        public scene_mimic(string name, mimic mimic, scene_actor actor)
        {
            Asset_name = name;
            Mimic = mimic;
            Actor = actor;
        }
        public scene_mimic() { }

        public mimic Mimic
        {
            get
            {
                return mimic;
            }

            set
            {
                mimic = value;RaisePropertyChanged("Mimic");
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
