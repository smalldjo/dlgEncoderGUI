using dlgEncoderGui.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace dlgEncoderGui.models
{
    public class choice_line : objectWithLinks
    {
        public static readonly string[] _icons =
         {
            "",
            "shop",
            "blacksmith",
            "betting",
            "gwent",
            "axii",
            "exit"
        };

        string _icon;
        string _choice_content;
        bool _single_use;
        bool _emphasize;
       // linkPoint _outPort;

        public choice_line()
        {
            _icon = _icons[0];
            outPort = new linkPoint();
        }


        #region properties
        public string icon
        {
            get { return _icon; }
            set { /*if (_icon.Contains(value))*/ _icon = value; RaisePropertyChanged("icon"); }
        }

        public string choice_content
        {
            get { return _choice_content; }
            set { if (value != null) _choice_content = value; RaisePropertyChanged("choice_content"); }
        }
        public string[] icons
        {
            get { return _icons; }
        }

        public bool Single_use
        {
            get
            {
                return _single_use;
            }

            set
            {
                _single_use = value;
            }
        }

        public bool Emphasize
        {
            get
            {
                return _emphasize;
            }

            set
            {
                _emphasize = value;
            }
        }



        /* public linkPoint outPort
         {
             get { return _outPort; }
             set { if (value != null) _outPort = value; RaisePropertyChanged("outPort"); }
         }*/
        #endregion

    }


    public class section_choice_model : section_complex_model
    {
        private ObservableCollection<choice_line> _choice_lines;
        private double _time_limit;
        private RelayCommand _addCommand;
        private RelayCommand<choice_line> _deleteCommand;

        public section_choice_model()
        {
            _name = "section_choice_" + Path.GetRandomFileName().Replace(".", "");
            type = section.choice;
            _choice_lines = new ObservableCollection<choice_line>();
            _choice_lines.Add(new choice_line());
            _addCommand = new RelayCommand(add_dlg_line);
            _deleteCommand = new RelayCommand<choice_line>(delete_this);
        }

        #region properties
        public ObservableCollection<choice_line> choice_lines
        {
            get { return _choice_lines; }
            set {
                    if (value != null)
                    {
                        _choice_lines = value;
                        RaisePropertyChanged("choice_lines");
                        updatePorts();
                    }
                }
        }

        public double Time_limit
        {
            get
            {
                return _time_limit;
            }

            set
            {
                _time_limit = value;
            }
        }

        #endregion

        #region commands

        public RelayCommand<choice_line> deleteCommand
        {
            get { return _deleteCommand; }
        }

        public RelayCommand addCommand
        {
            get { return _addCommand; }
        }

       

        #endregion


        private void add_dlg_line()
        {
            if (_choice_lines.Count >= 6)
                return;
            _choice_lines.Add(new choice_line());
            RaisePropertyChanged("choice_lines");
            updatePorts();
        }

        private void delete_this(choice_line choice)
        {
            if (_choice_lines.Count <= 1)
                return;
            _choice_lines.Remove(choice);
            RaisePropertyChanged("choice_lines");
            updatePorts();
        }

        override protected void updatePorts()
        {
           // base.updatePorts();
            inPort.location = new Point(location.X + 5, location.Y + 125.5 + 5);
            int i;
            for (i = 0;i<_choice_lines.Count; i+=1 )
            {
                var line = _choice_lines.ElementAt(i);
                line.outPort.location =new Point(location.X+235+5, location.Y+80 + (30*i)+5 );
                
            }

        }
    }
}
