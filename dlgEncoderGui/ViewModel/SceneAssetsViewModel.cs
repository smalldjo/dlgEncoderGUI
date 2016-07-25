using dlgEncoderGui.models.scene;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace dlgEncoderGui.ViewModel
{
    [KnownType(typeof(SceneAssetsViewModel))]

    

    public  class SceneAssetsViewModel : ViewModelBase
    {
        // public repoViewModel Repo { get; set; }
        private ObservableCollection<scene_actor> actors;
        private ObservableCollection<scene_mimic> mimics;
        private ObservableCollection<scene_animation> animations;
        private ObservableCollection<scene_camera> cameras;
        private ObservableCollection<scene_animation> mimicAnimations;
        //hack
        private bool isBusy;

       

        public SceneAssetsViewModel(/*repoViewModel repo*/)
        {
           // Repo = repo;
            Actors = new ObservableCollection<scene_actor>();
            Mimics = new ObservableCollection<scene_mimic>();
            Cameras = new ObservableCollection<scene_camera>();
            Animations = new ObservableCollection<scene_animation>();
            MimicAnimations = new ObservableCollection<scene_animation>();

          
            if (IsInDesignMode)
            {
                addNewActor();
                addNewMimic();
                addNewAnimation();
                addNewCamera();
            }
        }

        #region properties

        public ObservableCollection<scene_actor> Actors
        {
            get
            {
                return actors;
            }

            set
            {
                actors = value; RaisePropertyChanged("Actors");
            }
        }

        public ObservableCollection<scene_animation> Animations
        {
            get
            {
                return animations;
            }

            set
            {
                animations = value; RaisePropertyChanged("Animations");
            }
        }

        public ObservableCollection<scene_mimic> Mimics
        {
            get
            {
                return mimics;
            }

            set
            {
                mimics = value; RaisePropertyChanged("Mimics");
            }
        }

        public ObservableCollection<scene_camera> Cameras
        {
            get
            {
                return cameras;
            }

            set
            {
                cameras = value;RaisePropertyChanged("Cameras");
            }
        }

        public ObservableCollection<scene_animation> MimicAnimations
        {
            get
            {
                return mimicAnimations;
            }

            set
            {
                mimicAnimations = value; RaisePropertyChanged("MimicAnimations");
            }
        }

        #endregion

        public void addNewActor()
        {
            if (Actors.Count >= 2)
                return;

            var act = new scene_actor() { Asset_name = "newActor", Entity = null /* Repo.Entities.First() */ };
            act.PropertyChanged += handleActorIsPlayerChanged;
            Actors.Add(act);
 
        }

        public void removeActor(object toBeDeleted)
        {

            var ActorBeDeleted = toBeDeleted as scene_actor;
            if (ActorBeDeleted == null)
                return;
        
            Actors.Remove(ActorBeDeleted);
        }

        public void addNewMimic()
        {
            Mimics.Add(new scene_mimic("newMimic", null /*Repo.Mimics.First() */ ,null));

        }

        public void removeMimic(object toBeDeleted)
        {

            var MimicBeDeleted = toBeDeleted as scene_mimic;
            if (MimicBeDeleted == null)
                return;

            Mimics.Remove(MimicBeDeleted);
        }

        public void addNewAnimation()
        {
            Animations.Add(new scene_animation("newAnimation",null /*Repo.Animations.First() */, null ));

        }

        public void removeAnimation(object toBeDeleted)
        {

            var AnimationBeDeleted = toBeDeleted as scene_animation;
            if (AnimationBeDeleted == null)
                return;

           Animations.Remove(AnimationBeDeleted);
        }

        public void addNewMimicAnimation()
        {
            MimicAnimations.Add(new scene_animation("newMimicAnimation", null /*Repo.Animations.First() */, null));

        }

        public void removeMimicAnimation(object toBeDeleted)
        {

            var AnimationBeDeleted = toBeDeleted as scene_animation;
            if (AnimationBeDeleted == null)
                return;

            MimicAnimations.Remove(AnimationBeDeleted);
        }

        public void addNewCamera()
        {
            Cameras.Add(new scene_camera("newCamera",null /* Repo.Cameras.First() */));

        }

        public void removeCamera(object toBeDeleted)
        {

            var CameraBeDeleted = toBeDeleted as scene_camera;
            if (CameraBeDeleted == null)
                return;

            Cameras.Remove(CameraBeDeleted);
        }
        
        public  void handleActorIsPlayerChanged(object sender, PropertyChangedEventArgs  e)
        {
            if( !isBusy && e.PropertyName=="IsPlayer")
            {
                isBusy = true;
                var act = sender as scene_actor;
                foreach(scene_actor actor in Actors)
                {
                    if (actor != act)
                        actor.IsPlayer = !act.IsPlayer;
                }
            }
            isBusy = false;
        }

    }
}
