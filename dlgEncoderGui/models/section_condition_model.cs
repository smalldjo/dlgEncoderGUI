using dlgEncoderGui.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dlgEncoderGui.models
{ 
 

    public class section_condition_model : section_complex_model
    {
        private static readonly string[] operators =
        {
            "=",
            "!=",
            "<",
            "<=",
            ">=",
            ">"
        };



        public section_condition_model()
        {
            _whenFalse = new objectWithLinks();
            _whenTrue = new objectWithLinks();
            type = section.condition;
        }

      


        private string _factName;
        private string _operator;
        private string _value;
        objectWithLinks _whenTrue;
        objectWithLinks _whenFalse;


        public static string[] Operators
        {
            get
            {
                return operators;
            }
        }

        public string FactName
        {
            get
            {
                return _factName;
            }

            set
            {
                _factName = value; RaisePropertyChanged("FactName");
            }
        }

        public string Operator
        {
            get
            {
                return _operator;
            }

            set
            {
                _operator = value; RaisePropertyChanged("Operator");
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value; RaisePropertyChanged("Value");
            }
        }

        public objectWithLinks WhenTrue
        {
            get
            {
                return _whenTrue;
            }

            set
            {
                _whenTrue = value; RaisePropertyChanged("WhenTrue"); updatePorts();
            }
        }

        public objectWithLinks WhenFalse
        {
            get
            {
                return _whenFalse;
            }

            set
            {
                _whenFalse = value; RaisePropertyChanged("WhenFlase"); updatePorts();
            }
        }

        override protected void updatePorts()
        {
            base.updatePorts();
            inPort.location = new Point(location.X + 5, location.Y + 75 + 5);
            WhenTrue.outPort.location = new Point(location.X + 237 + 5, location.Y +40);
            WhenFalse.outPort.location = new Point(location.X + 235 + 5, location.Y + 70 + 5);
            

        }


    }


}
