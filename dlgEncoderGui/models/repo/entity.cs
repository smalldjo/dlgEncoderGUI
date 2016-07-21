using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dlgEncoderGui.models.repo
{
    public class entity : ObservableObject
    {
        public entity() { }
        public entity(string name, string path,mimic mimic) { Name = name;Path = path;DefaultMimic = mimic; }
        private string name;
        private string path;
        private mimic defaultMimic;


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

        public string Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value; RaisePropertyChanged("Path");
            }
        }

        public mimic DefaultMimic
        {
            get
            {
                return defaultMimic;
            }

            set
            {
                defaultMimic = value; RaisePropertyChanged("DefaultMimic");
            }
        }
    }
}
