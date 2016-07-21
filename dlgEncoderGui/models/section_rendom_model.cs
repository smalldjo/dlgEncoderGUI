using dlgEncoderGui.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace dlgEncoderGui.models
{
    public class random_line : objectWithLinks
    {

        public random_line()
        {
            outPort = new linkPoint();
        }


        #region properties
       
        #endregion

    }


    public class section_random_model : section_complex_model
    {
       private ObservableCollection<random_line> _random_lines;
        private RelayCommand _addCommand;
        private RelayCommand<random_line> _deleteCommand;

        public section_random_model()
        {
            _name = "section_randomizer_" + Path.GetRandomFileName().Replace(".", "");
            type = section.random;
            _random_lines = new ObservableCollection<random_line>();
            _random_lines.Add(new random_line());
            _random_lines.Add(new random_line());
            _addCommand = new RelayCommand(add_dlg_line);
            _deleteCommand = new RelayCommand<random_line>(delete_this);
        }

        #region properties
        public ObservableCollection<random_line> random_lines
        {
            get { return _random_lines; }
            set {
                    if (value != null)
                    {
                        _random_lines = value;
                        RaisePropertyChanged("random_lines");
                        updatePorts();
                    }
                }
        }

       

        #endregion

        #region commands

        public RelayCommand<random_line> deleteCommand
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
            if (_random_lines.Count >= 6)
                return;
            _random_lines.Add(new random_line());
            RaisePropertyChanged("random_lines");
            updatePorts();
        }

        private void delete_this(random_line random)
        {
            if (_random_lines.Count <= 2)
                return;
            _random_lines.Remove(random);
            RaisePropertyChanged("random_lines");
            updatePorts();
        }

        override protected void updatePorts()
        {
            base.updatePorts();
            int i;
            for (i = 0;i< _random_lines.Count; i+=1 )
            {
                var line = _random_lines.ElementAt(i);
                line.outPort.location =new Point(location.X+235+5, location.Y+20+(30*i)+5 );
                
            }

        }
    }
}
