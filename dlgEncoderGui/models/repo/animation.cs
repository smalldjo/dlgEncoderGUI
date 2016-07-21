using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dlgEncoderGui.models.repo
{
   public  class animation
    {
        public string Name { get; set; }
        public string Game_animation { get; set; }
        public double Duration { get; set; }
        public int Frames { get; set; }
        public double Weight { get; set; }
        public double ClipFront { get; set; }
        public double ClipEnd { get; set; }
        public double Stretch { get; set; }
        public double BlendIn { get; set; }
        public double BlendOut { get; set; }

        public animation(string name)
        {
            Name = name;
        }
        public animation() { }
    }
}
