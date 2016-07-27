using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dlgEncoderGui.models.repo
{
    public class keep_defaults
    {
        private bool weight;
        private bool clipFront;
        private bool clipEnd;
        private bool stretch;
        private bool blendIn;
        private bool blendOut;

        public bool Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }

        public bool ClipFront
        {
            get
            {
                return clipFront;
            }

            set
            {
                clipFront = value;
            }
        }

        public bool ClipEnd
        {
            get
            {
                return clipEnd;
            }

            set
            {
                clipEnd = value;
            }
        }

        public bool Stretch
        {
            get
            {
                return stretch;
            }

            set
            {
                stretch = value;
            }
        }

        public bool BlendIn
        {
            get
            {
                return blendIn;
            }

            set
            {
                blendIn = value;
            }
        }

        public bool BlendOut
        {
            get
            {
                return blendOut;
            }

            set
            {
                blendOut = value;
            }
        }
    }

   public  class animation
    {
        private string name;
        private string game_animation;
        //private double duration;
        private int frames;
        private double weight;
        private double clipFront;
        private double clipEnd;
        private double stretch;
        private double blendIn;
        private double blendOut;
        private keep_defaults defaults_states;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value.Replace(" ","_");
            }
        }

        public string Game_animation
        {
            get
            {
                return game_animation;
            }

            set
            {
                game_animation = value;
            }
        }
/*
        public double Duration
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }*/

        public int Frames
        {
            get
            {
                return frames;
            }

            set
            {
                if (value <= 0) frames = 1;

                frames = value;
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

                weight = value;
            }
        }

        public double ClipFront
        {
            get
            {
                return clipFront;
            }

            set
            {
                clipFront = value;
            }
        }

        public double ClipEnd
        {
            get
            {
                return clipEnd;
            }

            set
            {
                clipEnd = value;
            }
        }

        public double Stretch
        {
            get
            {
                return stretch;
            }

            set
            {
                if (value <= 0 || value > 1.5)
                    stretch = 1;
                else

                stretch = value;
            }
        }

        public double BlendIn
        {
            get
            {
                return blendIn;
            }

            set
            {
                blendIn = value;
            }
        }

        public double BlendOut
        {
            get
            {
                return blendOut;
            }

            set
            {
                blendOut = value;
            }
        }

        public keep_defaults Defaults_states
        {
            get
            {
                return defaults_states;
            }

            set
            {
                defaults_states = value;
            }
        }

        public animation(string name)
        {
            Name = name;
            defaults_states = new keep_defaults();
        }
        public animation() { }
    }
}
