using dlgEncoderGui.models.repo;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace dlgEncoderGui.ViewModel
{
    public class repoViewModel : ViewModelBase
    {
        private ObservableCollection<camera> _cameras;
        private ObservableCollection<entity> _entities;
        private ObservableCollection<mimic> _mimics;
        private ObservableCollection<animation> _animations;
        private ObservableCollection<animation> _mimics_animations;


        public repoViewModel()
        {
            Cameras = new ObservableCollection<camera>();
            Entities = new ObservableCollection<entity>();
            Mimics = new ObservableCollection<mimic>();
            Animations = new ObservableCollection<animation>();
            Mimics_animations = new ObservableCollection<animation>();

            if (IsInDesignMode)
            { 
                Cameras.Add(new camera("1_12_close", 90, new Point3D(12, 10, 5), new Point3D()));
                Mimics.Add(new mimic("determined"));
                Entities.Add(new entity("geralt", "gameplay\\templates\\characters\\player\\player.w2ent", Mimics.ElementAt(0)));
                Animations.Add(new animation("new_animation"));
                Mimics_animations.Add(new animation("new_mimic_animation"));
            }
        }





        public ObservableCollection<camera> Cameras
        {
            get
            {
                return _cameras;
            }

            set
            {
                _cameras = value;RaisePropertyChanged("Cameras");
            }
        }

        public ObservableCollection<mimic> Mimics
        {
            get
            {
                return _mimics;
            }

            set
            {
                _mimics = value;RaisePropertyChanged("Mimics");
            }
        }

        public ObservableCollection<entity> Entities
        {
            get
            {
                return _entities;
            }

            set
            {
                _entities = value; RaisePropertyChanged("Entities");
            }
        }

        public ObservableCollection<animation> Animations
        {
            get
            {
                return _animations;
            }

            set
            {
                _animations = value;RaisePropertyChanged("Animations");
            }
        }

        public ObservableCollection<animation> Mimics_animations
        {
            get
            {
                return _mimics_animations;
            }

            set
            {
                _mimics_animations = value;RaisePropertyChanged("Mimics_animations");
            }
        }

        public void addNewMimic()
        {
            Mimics.Add(new mimic("new_mimic"));
            RaisePropertyChanged("Mimics");
        }
        public void addNewCamera()
        {
            Cameras.Add(new camera("new_Camera", 90, new Point3D(), new Point3D()));
            RaisePropertyChanged("Cameras");
        }
        public void addNewEntity()
        {
            Entities.Add(new entity("new_actor","path",Mimics.ElementAt(0)));
            RaisePropertyChanged("Animations");
        }
        public void addNewAnimation()
        {
            Animations.Add(new animation("new_animation")); RaisePropertyChanged("Animations");

        }
        public void addNewMimicsAnimation()
        {
            Mimics_animations.Add(new animation("new_mimic_animation"));RaisePropertyChanged("Mimics_animations");

        }
        public void deleteMimic(object toBeDeleted)
        {
            var MimictoBeDeleted = toBeDeleted as mimic;
            if (MimictoBeDeleted == null)
                return;
            Mimics.Remove(MimictoBeDeleted);RaisePropertyChanged("Mimics");

        }
        public void deleteCamera(object toBeDeleted)
        {
            var CameratoBeDeleted = toBeDeleted as camera;
            if (CameratoBeDeleted == null)
                return;
            Cameras.Remove(CameratoBeDeleted); RaisePropertyChanged("Cameras");

        }
        public void deleteEntity(object toBeDeleted)
        {
            var EntitytoBeDeleted = toBeDeleted as entity;
            if (EntitytoBeDeleted == null)
                return;
            Entities.Remove(EntitytoBeDeleted); RaisePropertyChanged("Entities");
        }
        public void deleteAnimation(object toBeDeleted)
        {
            var AnimationtoBeDeleted = toBeDeleted as animation;
            if (AnimationtoBeDeleted == null)
                return;
            Animations.Remove(AnimationtoBeDeleted); RaisePropertyChanged("Animations");
        }
        public void deleteMimicAnimation(object toBeDeleted)
        {
            var AnimationtoBeDeleted = toBeDeleted as animation;
            if (AnimationtoBeDeleted == null)
                return;
            Mimics_animations.Remove(AnimationtoBeDeleted); RaisePropertyChanged("Mimics_animations");
        }



        //yaml

        public Dictionary<string, Dictionary<string, object>> getFormatedRepo()
        {
            var wrapper = new Dictionary<string, Dictionary<string, object>>();
            var data = new Dictionary<string, object>();


            //actors
            var actors_data = new Dictionary<string, object>();
            foreach (entity  actor in Entities)
            {
                var actor_data = new Dictionary<string, object>();
                actor_data.Add("template", actor.Path);

                if (actor.DefaultMimic == null)
                    continue;

                var mimic = actor.DefaultMimic; 
                var mimic_data = new Dictionary<string, string>();
                mimic_data.Add("emotional_state", mimic.Emotional_state);
                mimic_data.Add("eyes", mimic.Eyes);
                mimic_data.Add("pose", mimic.Pose);
                mimic_data.Add("weight", mimic.Weight.ToString());
                mimic_data.Add("anim", mimic.Anim);
                mimic_data.Add("duration", mimic.Duration.ToString());

                actor_data.Add("mimic", mimic_data);

                actors_data.Add(actor.Name, actor_data);
            }
            if(actors_data.Count>0)
                data.Add("actors", actors_data);


            //mimics
            var mimics_data = new Dictionary<string, object>();
            foreach (mimic mimic in Mimics)
            {
                var mimic_data = new Dictionary<string, string>();
                mimic_data.Add("emotional_state", mimic.Emotional_state);
                mimic_data.Add("eyes", mimic.Eyes);
                mimic_data.Add("pose", mimic.Pose);
                mimic_data.Add("weight", mimic.Weight.ToString());
                mimic_data.Add("anim", mimic.Anim);
                mimic_data.Add("duration", mimic.Duration.ToString());

                mimics_data.Add(mimic.Name, mimic_data);
            }
            if(mimics_data.Count>0)
                data.Add("mimics", mimics_data);

            //Cameras
            var cameras_data = new Dictionary<string, object>();
            foreach(camera camera in Cameras)
            {
                var cam_data = new Dictionary<string, object>();

                cam_data.Add("fov", camera.Fov.ToString());
                cam_data.Add("zoom", camera.Zoom.ToString());

                //transform
                var transform_data = new Dictionary<string, List<string>>();
                transform_data.Add("pos", new List<string>() { camera.Transform_positionX.ToString(), camera.Transform_positionY.ToString(), camera.Transform_positionZ.ToString() });
                transform_data.Add("rot", new List<string>() { camera.Transform_rotationX.ToString(), camera.Transform_rotationY.ToString(), camera.Transform_rotationZ.ToString() });
                cam_data.Add("transform", transform_data);

                //dof
                var dof_data = new Dictionary<string, object>();
                dof_data.Add("aperture", new List<string>() { camera.Dof_apertureX.ToString(), camera.Dof_apertureY.ToString() });
                dof_data.Add("blur", new List<string>() { camera.Dof_blurX.ToString(), camera.Dof_blurY.ToString() });
                dof_data.Add("focus", new List<string>() { camera.Dof_focusX.ToString(), camera.Dof_focusY.ToString() });
                dof_data.Add("intensity", camera.Dof_intensity.ToString());
                cam_data.Add("dof", dof_data);

                //source
                var source_data = new Dictionary<string, object>();
                source_data.Add("height", camera.Source_height.ToString());
                source_data.Add("slotname", camera.Source_slotname);
                cam_data.Add("source", source_data);

                //target

                var target_data = new Dictionary<string, object>();
                    target_data.Add("vector", new List<string>()
                    { camera.Target_vectorX.ToString(),
                      camera.Target_vectorY.ToString(),
                      camera.Target_vectorZ.ToString(),
                      camera.Target_vectorW.ToString()
                    });
                target_data.Add("slotname", camera.Target_slotname);

                cam_data.Add("target", target_data);

                //event_generator
                var event_data = new Dictionary<string, object>();
                event_data.Add("plane", camera.Event_plane);
                event_data.Add("tags", new List<string>() { camera.Event_tag });//TODo check if  this is working with binding
                cam_data.Add("event_generator", event_data);

                cameras_data.Add(camera.Name, cam_data);
            }

            if(cameras_data.Count>0)
                data.Add("cameras", cameras_data);

            //animations
            var animations_data = new Dictionary<string, object>();

            foreach(animation anim in Animations)
            {
                var anim_data = new Dictionary<string, string>();
                anim_data.Add("animation", anim.Game_animation );
                anim_data.Add("duration",   anim.Duration.ToString());
                anim_data.Add("frames", anim.Frames.ToString());
                anim_data.Add("weight", anim.Weight.ToString());
                anim_data.Add("clipfront", anim.ClipFront.ToString());
                anim_data.Add("clipend", anim.ClipEnd.ToString());
                anim_data.Add("stretch", anim.Stretch.ToString());
                anim_data.Add("blendin", anim.BlendIn.ToString());
                anim_data.Add("blendout", anim.BlendOut.ToString()  );

                animations_data.Add(anim.Name, anim_data);
            }
            if(animations_data.Count>0)
                data.Add("animations", animations_data);

            wrapper.Add("repository", data);
            return wrapper;
        }
    }
}
