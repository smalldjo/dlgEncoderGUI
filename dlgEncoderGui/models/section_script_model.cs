using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Collections.ObjectModel;
using dlgEncoderGui.ViewModel;
using System.IO;

namespace dlgEncoderGui.models
{
    //section_dlg is under this one

    public class parametre_line : ObservableObject
    {
       

        string _name;
        string _value;

        public parametre_line()
        {
           
          
        }


        #region porperties

        public string name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("name"); }
        }

        public string value
        {
            get { return _value; }
            set { if (value != null) _value = value; RaisePropertyChanged("value"); }
        }
        
        #endregion



    }

    public  class section_script_model : section_simple_model
    {
        private string _function_name;
        private ObservableCollection<parametre_line> _parametres;
        private RelayCommand _addCommand;
        private RelayCommand<parametre_line> _deleteCommand;

        public section_script_model()
        {
            _name = "script_" + Path.GetRandomFileName().Replace(".", "");
            type = section.script;
            _parametres = new ObservableCollection<parametre_line>();
            _addCommand = new RelayCommand(add_parametre);
            _deleteCommand = new RelayCommand<parametre_line>(delete_this);
        }

        #region porperties

        public string function_name
        {
            get { return _function_name; }
            set { _function_name = value;RaisePropertyChanged("function_name"); }
        }

        public ObservableCollection<parametre_line> parametres
        {
            get { return _parametres; }
            set { if (value != null) _parametres = value; RaisePropertyChanged("parametres"); }
        }


        #endregion

        #region commands

        public RelayCommand<parametre_line> deleteCommand
        {
            get { return _deleteCommand; }
        }

        public RelayCommand addCommand
        {
            get { return _addCommand; }
        }

        #endregion


        private void add_parametre()
        {
            _parametres.Add(new parametre_line());
        }

        private void delete_this(parametre_line parametre)
        {
            _parametres.Remove(parametre);
        }



    }
}
