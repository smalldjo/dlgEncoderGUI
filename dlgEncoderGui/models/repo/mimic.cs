using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dlgEncoderGui.models.repo
{
    public class mimic : ObservableObject
    {
        private string name;
        private double weight;
        private double duration;
        private string emotional_state;
        private string eyes;
        private string pose;
        private string anim;

        public mimic(string name)
        {
            Name = name;
        }
        public mimic() { }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value.Replace(" ","_");RaisePropertyChanged("Name");
            }
        }

        public double Weight
        {
            get
            {
                return weight;
            }

            set
            {
                if (value <= 0 || value > 1)
                    weight = 1;
                else
                    weight = value; RaisePropertyChanged("Weight");
            }
        }

        public double Duration
        {
            get
            {
                return duration;
            }

            set
            {
                if (value < 0)
                    duration = 0;
                else
                    duration = value; RaisePropertyChanged("Duration");
            }
        }

        public string Emotional_state
        {
            get
            {
                return emotional_state;
            }

            set
            {
                emotional_state = value; RaisePropertyChanged("Emotional_state");
            }
        }

        public string Eyes
        {
            get
            {
                return eyes;
            }

            set
            {
                eyes = value; RaisePropertyChanged("Eyes");
            }
        }

        public string Pose
        {
            get
            {
                return pose;
            }

            set
            {
                pose = value; RaisePropertyChanged("Pose");
            }
        }

        public string Anim
        {
            get
            {
                return anim;
            }

            set
            {
                anim = value; RaisePropertyChanged("Anim");
            }
        }

       
    }
}
