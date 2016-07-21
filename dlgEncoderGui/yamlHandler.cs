using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using dlgEncoderGui.ViewModel;
using System.Windows;
using Microsoft.Win32;
//using YamlDotNet.Serialization;
using SharpYaml.Serialization;
using dlgEncoderGui.Views;

namespace dlgEncoderGui
{
    public class yamlHandler
    {

        public void serialize(MainViewModel viewModel)
        {
            bool? response = true;

            while (!(viewModel.SceneVM.isReadyToSave()) & (response == true))
            {
                response = new SceneSettings().ShowDialog();
            }

            if (response != true)
                return;


            var set = new SerializerSettings();
            set.EmitTags = false;
            set.EmitAlias = false;
            var serializer = new Serializer(set) ;
           
           
                StreamWriter repoStream = new StreamWriter(new FileStream(viewModel.SceneVM.Folder+"\\"+"Scene.repo.yml", FileMode.Create, FileAccess.Write));
               // StreamWriter ProductionStream = new StreamWriter(new FileStream(viewModel.SceneVM.Folder + "\\" + "production.yml", FileMode.Create, FileAccess.Write));
                StreamWriter ScriptStream = new StreamWriter(new FileStream(viewModel.SceneVM.Folder + "\\" + "dialogscript.yml", FileMode.Create, FileAccess.Write));
               // StreamWriter StoryStream = new StreamWriter(new FileStream(viewModel.SceneVM.Folder + "\\" + "storyboard.yml", FileMode.Create, FileAccess.Write));

                var repo = viewModel.RepoVM.getFormatedRepo();
                var production = viewModel.SceneVM.getFormatedProduction();
                var storyBoard = viewModel.SceneVM.getFormatedStoryBoard();

                var script = viewModel.SceneVM.getDialoScript();

                if (repo.Count > 0)
                    serializer.Serialize(repoStream, repo);
              
                if (script.Count > 0)
                    serializer.Serialize(ScriptStream, script);

                if (storyBoard.Count > 0)
                        serializer.Serialize(ScriptStream, storyBoard);


                if (production.Count > 0)
                    serializer.Serialize(ScriptStream, production);

            repoStream.Flush();
            repoStream.Close();
        //    ProductionStream.Flush();
        //    ProductionStream.Close();
            ScriptStream.Flush();
            ScriptStream.Close();
        //    StoryStream.Flush();
         //   StoryStream.Close();
        }
            //    MessageBox.Show(dlg.SafeFileName);



        }
    }
