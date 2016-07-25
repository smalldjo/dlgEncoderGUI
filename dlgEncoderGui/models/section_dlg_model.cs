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
using System.Xml.Serialization;
using dlgEncoderGui.models.scene;

namespace dlgEncoderGui.models
{
    //section_dlg is under this one
    //dlg_line too
    public class camera_change : ObservableObject
    {
        public camera_change() { }
        public double Time { get; set; }
        public scene_camera Camera { get; set; }
    }

    public class animation_change : ObservableObject
    {
        public animation_change() { }
        public double Time { get; set; }
        public scene_animation Animation { get; set; }
    }
    public class mimic_change : ObservableObject
    {
        public mimic_change() { }
        public double Time { get; set; }
        public scene_mimic Mimic { get; set; }
    }


    public class dlg_line : ObservableObject
    {
        /* public  static readonly string[] _speakers =  
        {
            "npc",
            "geralt",
            "PAUSE"
        };
        */

        scene_actor _speaker;
        string _line_content;
        ObservableCollection<camera_change> _camera_changes;
        ObservableCollection<animation_change> _animation_changes;
        ObservableCollection<mimic_change> _mimic_changes;
        ObservableCollection<animation_change> _mimicAnimations_changes;

        private RelayCommand _addCameraCommand;
        private RelayCommand _addAnimationCommand;
        private RelayCommand _addMimicAnimationCommand;

        private RelayCommand _addMimicCommand;

        private RelayCommand<camera_change> _deleteCameraCommand;
        private RelayCommand<animation_change> _deleteAnimationCommand;
        private RelayCommand<animation_change> _deleteMimicAnimationCommand;

        private RelayCommand<mimic_change> _deleteMimicCommand;


        public dlg_line()
        {
            //_speaker = _speakers[0];

            Camera_changes = new ObservableCollection<camera_change>();
            Animation_changes = new ObservableCollection<animation_change>();
            Mimic_changes = new ObservableCollection<mimic_change>();
            MimicAnimations_changes = new ObservableCollection<animation_change>();


            _addAnimationCommand = new RelayCommand(addAnim);
            _addCameraCommand = new RelayCommand(addCam);
            _addMimicCommand = new RelayCommand(addMim);
            _addMimicAnimationCommand = new RelayCommand(addMimicAnim);


            _deleteAnimationCommand = new RelayCommand<animation_change> (deleteAnim);
            _deleteCameraCommand = new  RelayCommand<camera_change>(deleteCam);
            _deleteMimicCommand = new RelayCommand<mimic_change>(deleteMim);
            _deleteMimicAnimationCommand = new RelayCommand<animation_change>(deleteMimicAnim);

        }


        #region porperties
        public scene_actor speaker
        {
            get { return _speaker; }
            set {  _speaker = value; RaisePropertyChanged("speaker"); }
        }

        public string line_content
        {
            get { return _line_content; }
            set { if (value != null) _line_content = value; RaisePropertyChanged("line_content"); }
        }

        public ObservableCollection<camera_change> Camera_changes
        {
            get
            {
                return _camera_changes;
            }

            set
            {
                _camera_changes = value;
            }
        }

        public ObservableCollection<animation_change> Animation_changes
        {
            get
            {
                return _animation_changes;
            }

            set
            {
                _animation_changes = value;
            }
        }

        public ObservableCollection<mimic_change> Mimic_changes
        {
            get
            {
                return _mimic_changes;
            }

            set
            {
                _mimic_changes = value;
            }
        }

        public ObservableCollection<animation_change> MimicAnimations_changes
        {
            get
            {
                return _mimicAnimations_changes;
            }

            set
            {
                _mimicAnimations_changes = value;
            }
        }


        #endregion

        #region commands
        public RelayCommand AddCameraCommand
        {
            get
            {
                return _addCameraCommand;
            }

            
        }

        public RelayCommand AddAnimationCommand
        {
            get
            {
                return _addAnimationCommand;
            }

        }


        public RelayCommand AddMimicCommand
        {
            get
            {
                return _addMimicCommand;
            }

           
        }

        public RelayCommand<camera_change> DeleteCameraCommand
        {
            get
            {
                return _deleteCameraCommand;
            }
        }

        public RelayCommand<animation_change> DeleteAnimationCommand
        {
            get
            {
                return _deleteAnimationCommand;
            }
        }

        public RelayCommand<mimic_change> DeleteMimicCommand
        {
            get
            {
                return _deleteMimicCommand;
            }
        }

        public RelayCommand AddMimicAnimationCommand
        {
            get
            {
                return _addMimicAnimationCommand;
            }

        
        }

        public RelayCommand<animation_change> DeleteMimicAnimationCommand
        {
            get
            {
                return _deleteMimicAnimationCommand;
            }

        }









        #endregion


        private void addChange(string type)
        {
            switch (type)
            {
                case "anim":Animation_changes.Add(new animation_change());break;
                case "cam":Camera_changes.Add(new camera_change()); break;
                case "mim":Mimic_changes.Add(new mimic_change()); break;
                case "mimicAnim":MimicAnimations_changes.Add(new animation_change());break;
            }
           
        }

        private void addAnim()
        {
            addChange("anim");
        }

        private void addMimicAnim()
        {
            addChange("mimicAnim");
        }

        private void addCam()
        {
            addChange("cam");
        }
        private void addMim()
        {
            addChange("mim");
        }

        private void deleteAnim( animation_change anim)
        {
            Animation_changes.Remove(anim);
        }

        private void deleteMimicAnim(animation_change anim)
        {
            MimicAnimations_changes.Remove(anim);
        }
        private void deleteCam(camera_change cam)
        {
            Camera_changes.Remove(cam);
        }
        private void deleteMim(mimic_change mim)
        {
            Mimic_changes.Remove(mim);
        }
    }


    public  class section_dlg_model : section_simple_model
    {
        private ObservableCollection<dlg_line> _dlg_lines;
        private RelayCommand _addCommand;
        private RelayCommand<dlg_line> _deleteCommand;

        public section_dlg_model()
        {
            type = section.dialog;
            _dlg_lines = new ObservableCollection<dlg_line>();
            _dlg_lines.Add(new dlg_line());
            _addCommand = new RelayCommand(add_dlg_line);
            _deleteCommand = new RelayCommand<dlg_line>(delete_this);
        }

        #region porperties
        public ObservableCollection<dlg_line> dlg_lines
        {
            get { return _dlg_lines; }
            set { if (value != null) _dlg_lines = value; RaisePropertyChanged("dlg_lines"); }
        }


        #endregion

        #region commands

        public RelayCommand<dlg_line> deleteCommand
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
            _dlg_lines.Add(new dlg_line());
        }

        private void delete_this(dlg_line dlg)
        {
            if (_dlg_lines.Count <= 1)
                return;
            _dlg_lines.Remove(dlg);
        }



    }



   

}
