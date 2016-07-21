using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace dlgEncoderGui.models
{
 

    [KnownType(typeof(objectWithLinks))]
    [KnownType(typeof(section_model))]
    [KnownType(typeof(section_simple_model))]
    [KnownType(typeof(section_complex_model))]
    [KnownType(typeof(section_dlg_model))]
    [KnownType(typeof(section_choice_model))]
    [KnownType(typeof(section_script_model))]
    [KnownType(typeof(dlg_line))]
    [KnownType(typeof(choice_line))]
    [KnownType(typeof(parametre_line))]
    [KnownType(typeof(random_line))]
    [KnownType(typeof(linkPoint))]
    [KnownType(typeof(section_condition_model))]
    [KnownType(typeof(section_random_model))]
    [KnownType(typeof(animation_change))]
    [KnownType(typeof(camera_change))]
    [KnownType(typeof(mimic_change))]


    public class objectWithLinks : ObservableObject
    {
        public linkPoint inPort { get; set; }
        public linkPoint outPort { get; set; }
        public List<connector> inLinks { get; set; }
        public connector outLink { get; set; }

 
        public objectWithLinks()
        {
            inPort = new linkPoint();
            outPort = new linkPoint();
            inLinks = new List<connector>();
        }

       

    }
}
