using dlgEncoderGui.models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using dlgEncoderGui.models.scene;
namespace dlgEncoderGui.ViewModel
{
    [KnownType(typeof(MainViewModel))]
    public class MainViewModel : ViewModelBase
    {
        private repoViewModel repoVM;
        private SceneViewModel sceneVM;


        public SceneViewModel SceneVM
        {
            get
            {
                return sceneVM;
            }

            set
            {
                sceneVM = value;RaisePropertyChanged("SceneVM");
            }
        }

        public repoViewModel RepoVM
        {
            get
            {
                return repoVM;
            }

            set
            {
                repoVM = value;RaisePropertyChanged("RepoVM");
            }
        }


        public MainViewModel()
        {
            if(IsInDesignMode)
            {

                RepoVM = new repoViewModel();
                SceneVM = new SceneViewModel();

                SceneVM.dialogs.Add(new section_dlg_model() {location=new System.Windows.Point(10,10) });
              //  SceneVM.add_new_section(section.choice, 250, 50);
              //  SceneVM.add_new_section(section.condition, 500, 100);
                SceneVM.add_new_section(section.random, 700, 200);
              //  SceneVM.add_new_section(section.script, 10, 250);
                SceneVM.link(SceneVM.dialogs[0], SceneVM.dialogs[1]);
            }
            else
            {

                RepoVM = new repoViewModel();
                SceneVM = new SceneViewModel();

            }
        }

        public void newScene()
        {
            SceneVM = new SceneViewModel();
        }

        public void serializeToYaml()
        {
            var handler = new yamlHandler();
            handler.serialize(this);
        }

      

       

    }
}
