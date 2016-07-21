using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dlgEncoderGui.models
{
   public  class section_simple_model : section_model
    {

        override protected void updatePorts()
        {
            base.updatePorts();
           outPort.location = new Point(location.X + 235+5, location.Y + 102.5+5);

        }

    }
}
