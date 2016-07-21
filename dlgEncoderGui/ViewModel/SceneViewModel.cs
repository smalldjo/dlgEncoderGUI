using GalaSoft.MvvmLight;
using dlgEncoderGui.models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.ComponentModel;
using System.Windows;
using System;
using System.Linq;
using System.Runtime.Serialization;
using GalaSoft.MvvmLight.Ioc;
using System.IO;
using dlgEncoderGui.models.scene;

namespace dlgEncoderGui.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    /// 


    public enum section
    {
        none,
        dialog,
        choice,
        script,
        condition,
        random
    }
    
    public class SceneViewModel : ViewModelBase
    {
        public int id { get; set; }
        public int string_id_space { get; set; }
        public int string_id_space_start { get; set; }
        public string name { get; set; }
        private string folder;

        SceneAssetsViewModel _assets;

        ObservableCollection<section_model> _dialogs;
        ObservableCollection<connector> _connectors;
        RelayCommand<section_model> _deleteSectionCommand;

    
        public SceneViewModel()
        {
            Assets = new SceneAssetsViewModel();
            _dialogs = new ObservableCollection<section_model>();
            _connectors = new ObservableCollection<connector>();
            _deleteSectionCommand = new RelayCommand<section_model>(delete_this);
        
        }

        #region properties
        public ObservableCollection<section_model> dialogs
        {
            get { return _dialogs; }
            set { _dialogs = value;RaisePropertyChanged("dialogs"); }
        }

        public ObservableCollection<connector> connectors
        {
            get { return _connectors; }
            set { if (value != null) _connectors = value; RaisePropertyChanged("connectors"); }
        }

        public SceneAssetsViewModel Assets
        {
            get
            {
                return _assets;
            }

            set
            {
                _assets = value;RaisePropertyChanged("Assets");
            }
        }

        public string Folder
        {
            get
            {
                return folder;
            }

            set
            {
                folder = value; RaisePropertyChanged("Folder");
            }
        }

        #endregion

        #region commands
        public RelayCommand<section_model> deleteSectionCommand
        {
            get { return _deleteSectionCommand; }
        }

      

        #endregion

        private void delete_this(section_model section)
        {
            foreach (connector link in section.inLinks)
                connectors.Remove(link);
            connectors.Remove(section.outLink);

           
            if(section.type == ViewModel.section.choice)
            {
                var choice_sec = section as section_choice_model;
                foreach (choice_line choice in choice_sec.choice_lines)
                {
                        _connectors.Remove(choice.outLink);
                }
            }

            if (section.type == ViewModel.section.random)
            {
                var random_sec = section as section_random_model;
                foreach (random_line random in random_sec.random_lines)
                {
                    _connectors.Remove(random.outLink);
                }
            }

            if (section.type == ViewModel.section.condition)
            {
                var condi_sectio = section as section_condition_model;
                _connectors.Remove(condi_sectio.WhenTrue.outLink);
                _connectors.Remove(condi_sectio.WhenFalse.outLink);
            }


            _dialogs.Remove(section);
          
        }

        public void add_new_section(section type, double toleft = 10, double totop = 10)
        {
            section_model sec;
        switch(type)
            {
                case section.dialog :
                        sec = new section_dlg_model();
                        break;
                case section.choice :
                        sec = new section_choice_model();
                        break;
              case section.script:
                        sec = new section_script_model();
                        break;
                case section.condition:
                        sec = new section_condition_model();
                        break;
                case section.random:
                    sec = new section_random_model();
                    break;
                default:
                        sec = new section_dlg_model();
                        break;               
            }
            
            //TODO Hard coded Values
            if (toleft < 0) toleft = 0;
            if (toleft + 260 > 20000) toleft = 20000 - 260;

            if (totop < 0) totop = 0;
            if (totop + 200 > 5000) totop = 5000 - 200;

            sec.location = new Point(toleft, totop);
            _dialogs.Add(sec);
        }

        public void link(object start,object end)
        {
            objectWithLinks _start, _end;

            _start = start as objectWithLinks;
            _end = end as objectWithLinks;

            if (_end == null || _start == null)
                return;

            var link = new connector(_start, _end);
            connectors.Remove(_start.outLink);
            _start.outLink = link;
            _end.inLinks.Add( link);
            _connectors.Add(link);
              
        }

        public void unLink(object connector)
        {
            var con = connector as connector;
            if (con == null)
                return;
            con.start.outLink = null;
            con.end.inLinks.Remove(con);
            connectors.Remove(con);

        }
        public void deltaPosition(object obj,double xDelta,double yDelta)
        {
            var section = obj as section_model;
            if (section == null)
                return;
            Point oldPos = section.location;
            Point newPos = new Point(oldPos.X + xDelta, oldPos.Y + yDelta);

            //TODO Hard coded values!
            if (newPos.X < 0) newPos.X = 0;
            if (newPos.X+260 > 20000) newPos.X = 20000-260;

            if (newPos.Y < 0) newPos.Y = 0;
            if (newPos.Y + 200 > 5000) newPos.Y = 5000 - 200;
            //()
            section.location = newPos;



            //midpoint update hack, cant get binding to fire properly
            if(section.type == ViewModel.section.choice)
            {
                var choice_section = section as section_choice_model;
                foreach(choice_line choice in choice_section.choice_lines)
                {
                    if(choice.outLink != null)
                        choice.outLink.updateMidPoint();
                }
            }
            if (section.type == ViewModel.section.random)
            {
                var random_section = section as section_random_model;
                foreach (random_line random in random_section.random_lines)
                {
                    if (random.outLink != null)
                        random.outLink.updateMidPoint();
                }
            }

            if(section.type == ViewModel.section.condition)
            {
                var cond_section = section as section_condition_model;
                if (cond_section.WhenFalse.outLink != null)
                    cond_section.WhenFalse.outLink.updateMidPoint();

                if (cond_section.WhenTrue.outLink != null)
                    cond_section.WhenTrue.outLink.updateMidPoint();
            }

            if (section.outLink != null)
                section.outLink.updateMidPoint();
            foreach(connector link in section.inLinks)
            {
               
                link.updateMidPoint();
            }

            //hack out
        }

        public bool isReadyToSave()
        {
            if (name == "")
                return false;
            if (!Directory.Exists(Folder))
                return false;

            return true;



        }

        public Dictionary<string,Dictionary<string,object>> getDialoScript()
        {
           var wrapper = new Dictionary<string, Dictionary<string, object>>();

            //duplicate keys ==> fuck up
            getFormatedDialogs().ToList().ForEach(x => getFormatedChoices().Add(x.Key, x.Value));
            getFormatedDialogs().ToList().ForEach(x => getFormatedRandoms().Add(x.Key, x.Value));
            getFormatedDialogs().ToList().ForEach(x => getFormatedScripts().Add(x.Key, x.Value));

        
                
           


            wrapper.Add("dialogscript", getFormatedDialogs());
            return wrapper;
        }

        public  Dictionary<string, object> getFormatedDialogs()
        {
            var data = new Dictionary<string, Object>();
            List<Object> section_lines;
            Dictionary<string, string> line;
            Dictionary<string,object> next_line;

            // ListBox ui_lines;

            //list player and actors
            var actors = new List<string>();
            foreach(scene_actor act in Assets.Actors)
            {
                if (act.IsPlayer)
                    data.Add("player", act.Asset_name);
                actors.Add(act.Asset_name);
            }

            data.Add("actors", actors);
            foreach (section_dlg_model section in dialogs.OfType<section_dlg_model>())
            {

                var lines = section.dlg_lines;
                section_lines = new List<Object>();

                foreach (dlg_line dlg_line in lines)
                {
                    line = new Dictionary<string, string>();
                    line.Add(dlg_line.speaker?.Asset_name, dlg_line.line_content);
                    section_lines.Add(line);
                }
                next_line = new Dictionary<String, Object>();
                if (section.outLink != null && section.outLink.end != null)
                {
                    if((section.outLink.end as section_model).type == ViewModel.section.condition)
                    {
                        section_condition_model cond_sect =  section.outLink.end as section_condition_model;
                        var all = new Dictionary<string,object>();
                        var con_details = new List<string>();
                                con_details.Add(cond_sect.FactName);
                                con_details.Add(cond_sect.Operator);
                                con_details.Add(cond_sect.Value);
                        all.Add("condition", con_details);
                        all.Add("on_true", (cond_sect.WhenTrue.outLink.end as section_model).name);
                        all.Add("on_false", (cond_sect.WhenFalse.outLink.end as section_model).name);
                        next_line.Add("NEXT", all);
                        section_lines.Add(next_line);

                    }
                    else
                    {
                        next_line.Add("NEXT", (section.outLink.end as section_model).name);
                        section_lines.Add(next_line);
                    }
                   
                }
                else
                {
                    next_line.Add("NEXT", "section_exit");
                    section_lines.Add(next_line);
                }
                data.Add(section.name, section_lines);


                
            }
            data.Add("section_exit", new List<string>() { "EXIT" });
            return data;
        }
        public  Dictionary<string, object> getFormatedChoices()
        {
            var data = new Dictionary<string,object>();
            List<Dictionary<string, List<Dictionary<string, object>>>> parent_section;
            Dictionary<string, List<Dictionary<string, object>>> section;
            List<Dictionary<string, object>> choices;
            Dictionary<string, object> choice;

            //  ListBox ui_choices;
            foreach (section_choice_model choice_section in dialogs.OfType<section_choice_model>())
            {
                //parent_section = new List<Dictionary<string, List<List<string>>>>();
                // section = new Dictionary<string, List<List<string>>>();
                //choices = new List<List<string>>();
                parent_section = new List<Dictionary<string,List<Dictionary<string,object>>>>();
                section = new Dictionary<string, List<Dictionary<string, object>>>();
                choices = new List<Dictionary<string, object>>();
                
                //choices
                foreach (choice_line choice_data in choice_section.choice_lines)
                {
                    choice = new Dictionary<string, object>();
                    var choice_args1 = new List<string>();


                    //nxt section, icon, choiceline 
                    choice_args1.Add(choice_data.choice_content);
                    if (choice_data.outLink != null && choice_data.outLink.end != null)
                    {
                        choice_args1.Add((choice_data.outLink.end as section_model).name);
                    }
                    else
                    {
                        choice_args1.Add("section_exit");
                    }

                    if (choice_data.icon != "")
                    {
                        choice_args1.Add(choice_data.icon);
                    }
                    choice.Add("choice", choice_args1);
                    // condition
                    //TODO...

                    //emphasize
                    if (choice_data.Emphasize)
                    {
                        choice.Add("emphasize","true");
                    }


                    //single_use
                    if (choice_data.Single_use)
                    {
                        choice.Add("single_use", "true");
                    }

                    

                   
                    choices.Add(choice);
                   

                }
                //time limite
                if (choice_section.Time_limit != "0")
                {
                    var time_lmt = new Dictionary<string, object>();
                    time_lmt.Add("TIME_LIMIT", choice_section.Time_limit);
                    choices.Add(time_lmt);
                }
                section.Add("CHOICE", choices);
                parent_section.Add(section);
                data.Add(choice_section.name, parent_section);
                //add secion to data here
            }
            return data;
        }
        public  Dictionary<string, object> getFormatedScripts()
        {
            //int i;
            var data = new Dictionary<string, object>();
            List<Dictionary<string, object>> sub_section_list;
            Dictionary<string, object> sub_section_list_value;
            Dictionary<string, object> function_details;
            List<Dictionary<string, string>> parameters;
            Dictionary<string, string> parameter;


            foreach (section_script_model script_section in dialogs.OfType<section_script_model>())
            {
                sub_section_list = new List<Dictionary<string, object>>();
                sub_section_list_value = new Dictionary<string, object>();
                function_details = new Dictionary<string, object>();
                parameters = new List<Dictionary<string, string>>();
                parameter = new Dictionary<string, string>();

                function_details.Add("function", script_section.function_name);

                foreach (parametre_line script_param in script_section.parametres)
                {
                    parameter = new Dictionary<string, string>();
                    parameter.Add(script_param.name, script_param.value);
                    parameters.Add(parameter);
                }

                function_details.Add("parameter", parameters);

                sub_section_list_value.Add("SCRIPT", function_details);
                sub_section_list.Add(sub_section_list_value);
                sub_section_list_value = new Dictionary<string, object>();
                if (script_section.outLink != null && script_section.outLink.end != null)
                {
                    sub_section_list_value.Add("NEXT", (script_section.outLink.end as section_model).name);
                }
                else
                {
                    sub_section_list_value.Add("NEXT", "section_exit");
                }

                sub_section_list.Add(sub_section_list_value);
                //some.Add("NEXT","section_next");


                //name_args.Add("function");

                data.Add(script_section.name, sub_section_list);
            }


            return data;
            //  var serializer = new Serializer();
            //  serializer.Serialize(Console.Out, data);
        }
        public  Dictionary<string, object> getFormatedRandoms()
        {
          var data = new Dictionary<string, object>();
            List<string> randoms;
            foreach(section_random_model section in _dialogs.OfType<section_random_model>())
            {
                randoms = new List<string>();
                foreach (random_line random in section.random_lines)
                {
                    if(random.outLink != null && random.outLink.end !=null)
                    {
                        var linkedsection = random.outLink.end as section_model;
                        randoms.Add(linkedsection.name);
                    }
                }

                data.Add(section.name, randoms);
            }
           // var test = new  arrat <string, string>("trd","d");
            return data;
           
        }
      /*  public Dictionary<string, object> getFormatedConditions()
        {
            var data = new Dictionary<string, object>();

            foreach(section_condition_model section in dialogs.OfType<section_condition_model>() )
            {
                section.
            }
        }*/

        public Dictionary<string, Dictionary<string, object>> getFormatedProduction()
        {
            var wrapper = new Dictionary<string, Dictionary<string, object>>();
            var data = new Dictionary<string, object>();

            //settings
            var settings_data = new Dictionary<string, string>();
                settings_data.Add("sceneid", id.ToString());
                settings_data.Add("strings-idspace", string_id_space.ToString());
                settings_data.Add("strings-idstart", string_id_space_start.ToString());
            if(settings_data.Count>0)
                data.Add("settings", settings_data);

            //assets

            var assets_data = new Dictionary<string, object>();
            //actors
            var actors_data = new Dictionary<string, Dictionary<string, string>>();

                foreach (scene_actor actor in Assets.Actors )
                {
                    var actor_data = new Dictionary<string, string>();
                        actor_data.Add("repo", actor.Entity?.Name);

                    actors_data.Add(actor.Asset_name,actor_data);
                }
            if(actors_data.Count>0)
                 assets_data.Add("actors", actors_data);

            //mimics
            var mimics_data = new Dictionary<string, Dictionary<string, string>>();

            foreach (scene_mimic mimic in Assets.Mimics)
            {
                var mimic_data = new Dictionary<string, string>();
                mimic_data.Add("repo", mimic.Mimic?.Name);
                mimic_data.Add("actor", mimic.Actor.Asset_name);

                mimics_data.Add(mimic.Asset_name, mimic_data);
            }

            if(mimics_data.Count>0)
             assets_data.Add("mimics", mimics_data);

            //cameras
            var cameras_data = new Dictionary<string, Dictionary<string, string>>();

            foreach (scene_camera camera in Assets.Cameras)
            {
                var camera_data = new Dictionary<string, string>();
                camera_data.Add("repo", camera.Camera?.Name);

                cameras_data.Add( camera.Asset_name , camera_data);

            }

            if (cameras_data.Count>0)
             assets_data.Add("cameras", cameras_data);

            //animations
            var animations_data = new Dictionary<string, Dictionary<string, string>>();

            foreach (scene_animation animation in Assets.Animations)
            {
                var animation_data = new Dictionary<string, string>();
                animation_data.Add("repo", animation.Animation?.Name);

                animations_data.Add(animation.Asset_name, animation_data);
            }

            if (animations_data.Count>0)
                assets_data.Add("animations", animations_data);

            if(assets_data.Count>0)
             data.Add("assets", assets_data);

            wrapper.Add("production", data);
            return wrapper;

        }
        public Dictionary<string,Dictionary<string, object>> getFormatedStoryBoard()
        {
            var wrapper = new Dictionary<string, Dictionary<string, object>>();
            var data = new Dictionary<string, object>();

            //defaults
            var def_data = new Dictionary<string, Dictionary<string, string>>();
            var cameras_data = new Dictionary<string, string>();

            //compteurs for story boards
            var count = new Dictionary<string, int>();


            foreach (scene_actor act in Assets.Actors )
            {
                if (act.Default_camera == null)
                    continue;

                cameras_data.Add(act.Asset_name, act.Default_camera?.Asset_name);
                count.Add(act.Asset_name, 1);//just filling the list so i dont itirate again :)
            }
            //lets not forget the "Pause" actor 
            count.Add("Pause", 1);


            if(cameras_data.Count>0)
                def_data.Add("camera", cameras_data);

            if(def_data.Count>0)
                data.Add("defaults", def_data);

            //section  

           // var sections_data = new Dictionary<string, object>();

            foreach(section_dlg_model section in dialogs.OfType<section_dlg_model>())
            {
               // var section_data = new Dictionary<string, Dictionary<string, List<Dictionary<string, List<string>>>>>() ;
                var lines_data = new Dictionary<string, List< Dictionary < string, List< string >>>>();

                foreach(dlg_line line in section.dlg_lines)
                {
                    var line_data = new List<Dictionary<string, List<string>>>();
                    
                    //camera changes
                    foreach(camera_change change in line.Camera_changes)
                    {
                        var change_data = new Dictionary<string, List<string>>();
                        change_data.Add("cam", new List<string>() { change.Time.ToString(), change.Camera?.Asset_name });

                        line_data.Add(change_data);
                    }

                    //animations
                    foreach (animation_change change in line.Animation_changes)
                    {
                        var change_data = new Dictionary<string, List<string>>();
                        change_data.Add("anim", new List<string>() { change.Time.ToString(), change.Animation?.Asset_name });
                        line_data.Add(change_data);
                    }

                    //mimics
                    foreach (mimic_change change in line.Mimic_changes)
                    {
                        var change_data = new Dictionary<string, List<string>>();
                        change_data.Add("mimic", new List<string>() { change.Time.ToString(), change.Mimic?.Asset_name });
                        line_data.Add(change_data);
                    }

                    if(line_data.Count>0)
                        lines_data.Add(line.speaker?.Asset_name + "_" + count[line.speaker?.Asset_name].ToString(),line_data);
                    if(line.speaker!=null)
                         count[line.speaker?.Asset_name]++;
                }

                if(lines_data.Count>0)
                    data.Add(section.name, lines_data);
            }
            wrapper.Add("storyboard", data);
            return wrapper;
        }
    
    }
}