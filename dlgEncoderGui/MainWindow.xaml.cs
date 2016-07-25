using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using dlgEncoderGui.ViewModel;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;
using dlgEncoderGui.models;
using dlgEncoderGui.Views;
using dlgEncoderGui.models.scene;
using System.ComponentModel;
using System.Diagnostics;

namespace dlgEncoderGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainViewModel mainVM;
        ViewModelLocator Locator;

        public MainWindow()
        {
            
            InitializeComponent();
            Locator = App.Current.FindResource("Locator") as ViewModelLocator;
            mainVM = Locator?.Main;

            if(Properties.Settings.Default.Repo != null)
                 mainVM.RepoVM = Properties.Settings.Default.Repo;


        }

        

        private void add_section_grid_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                section type = section.dialog;
                Grid draged = sender as Grid;
                string tag = draged.Tag as string;
        
                if(tag == "dlg")
                {
                    type = section.dialog;
                }               
                if(tag == "choice")
                {
                    type = section.choice;
                }
                 if(tag == "script")
                {
                    type = section.script;
                }
                 if(tag == "condition")
                {
                    type = section.condition;
                }
                if (tag == "random")
                {
                    type = section.random;
                }

                var data = new DataObject();
                data.SetData(DataFormats.StringFormat, type.ToString());
                DragDrop.DoDragDrop(sender as Grid, data, DragDropEffects.Move);
            }
           
        }

        

        private void mainCanvas_Drop(object sender, DragEventArgs e)
        {
            section type;
            Point pos = e.GetPosition(sender as Canvas);
            string sentData = e.Data.GetData(DataFormats.StringFormat) as string;
            Enum.TryParse<section>(sentData, out type);
            if (type == section.none)
                return;
            mainVM.SceneVM.add_new_section(type, pos.X, pos.Y);
        }

        private void OutPort_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                var data = new DataObject();
                data.SetData(DataFormats.StringFormat, "outPort");
                data.SetData("Object", sender as Rectangle);
                DragDrop.DoDragDrop(sender as Rectangle, data, DragDropEffects.All);
            }
           
        }

     

        private void InPort_Drop(object sender, DragEventArgs e)
        {
            var inport = sender as Rectangle;
            var type = e.Data.GetData(DataFormats.StringFormat) as string;
            if (type != "outPort")
                return;
            var outPort = e.Data.GetData("Object") as Rectangle;
            if (outPort == null)
                return;

            mainVM.SceneVM.link(outPort.DataContext, inport.DataContext);
        }

        private void drag_thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var elem = sender as Thumb;
            if (elem == null)
                return;

            mainVM.SceneVM.deltaPosition(elem.DataContext, e.HorizontalChange, e.VerticalChange);
        }



        private void load_button_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.ShowDialog();
            if(File.Exists(dlg.FileName))
            {
                var ser = new DataContractSerializer(typeof(SceneViewModel), null, 0x7FFF, false, true, null);
                using ( FileStream stream = new FileStream(dlg.FileName, FileMode.Open))
                {
                    object loaded;

                    try
                    {
                        loaded = ser.ReadObject(stream);

                    
                        SceneViewModel loadedScene = loaded as SceneViewModel;
                        if (loadedScene != null)
                        {
                            mainVM.SceneVM = loadedScene;
                            //   mainVM.updateAssets();

                        }
                        else
                        {
                            MessageBox.Show("error loading file");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("error loading file");
                    }
                   
                }
               
               
            }
            
        }
        public void saveScene()
        {

            bool? response = true;

            while (!(mainVM.SceneVM.isReadyToSave()) & (response == true))
            {
                response = new SceneSettings().ShowDialog();
            }

            if (response != true)
                return;

            /*
            // var dlg = new SaveFileDialog();
             //dlg.AddExtension = true;
             //dlg.ShowDialog();
             if (dlg.FileName != "")
             {

                 if(!dlg.FileName.Contains(".dlg"))
                 {
                     dlg.FileName = dlg.FileName + ".dlg";
                 }
             */
            string path = mainVM.SceneVM.Folder + "\\" + mainVM.SceneVM.name + ".W3Scene";
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                var ser = new DataContractSerializer(typeof(SceneViewModel), null, 0x7FFF, false, true, null);
                ser.WriteObject(stream, mainVM.SceneVM);
                MessageBox.Show("Saved","w3dlgEdtior");
            }

            //
            // stream.Close();


        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            saveScene();   

        }

        private void yaml_button_Click(object sender, RoutedEventArgs e)
        {
            mainVM.serializeToYaml();
        }


     

        private void new_Button_Click(object sender, RoutedEventArgs e)
        {
            var dlgre = MessageBox.Show("Do yo want to save the current scene ?","Save Current", MessageBoxButton.YesNo,MessageBoxImage.Question);
            if(dlgre == MessageBoxResult.Yes)
            {
                saveScene();                

            }

            mainVM.newScene();
            new SceneSettings().ShowDialog(); 
        }

        private void start_repo_button_Click(object sender, RoutedEventArgs e)
        {
            Window rep = new Repos();
            rep.Show();
        }

        private void manage_scene_button_Click(object sender, RoutedEventArgs e)
        {
            Window sc = new scene_assets();
            sc.Show();
        }

        private void advanced_dlg_button_Click(object sender, RoutedEventArgs e)
        {
            var adv_win = new advancedDialogEdit();
            adv_win.DataContext = (sender as Button).DataContext;
            adv_win.ShowDialog();
            
        }


        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            var mainCanvas = getMainCanvas();
            if (mainCanvas == null)
                return;

            var theSilder = sender as Slider;
            var scale = theSilder.Value / 100;
            var scaleform = new ScaleTransform(scale, scale);
            mainCanvas.LayoutTransform = scaleform;
            mainCanvas.UpdateLayout();

        }

        //helper*
        public Canvas getMainCanvas()
        {
            if (mainitemControl == null)//scrollview is renderd before, so it return nulls
                return null;
            var count = VisualTreeHelper.GetChildrenCount(mainitemControl);
            if (count <= 0)
                return null;

            var child = VisualTreeHelper.GetChild(mainitemControl, 0);

            while ((child as Canvas) == null && count > 0)
            {
                count = VisualTreeHelper.GetChildrenCount(child);
                child = VisualTreeHelper.GetChild(child, 0);
            }
            var mainCanvas = child as Canvas;
           
            return mainCanvas;
        }

        public static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                LogicalTreeHelper.GetChildren(depObj);
                foreach (var child in LogicalTreeHelper.GetChildren(depObj))
                {
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }
                    var innerchild = child as DependencyObject;
                    if (child == null)
                        continue;
                    foreach (T childOfChild in FindLogicalChildren<T>(innerchild))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void open_settings_menu_Click(object sender, RoutedEventArgs e)
        {
            new SceneSettings().ShowDialog();
        }

        //hack for Pause not showing
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            var act = (combo.DataContext as dlg_line).speaker?.Asset_name;
            if (act == "Pause")
                combo.SelectedIndex = 0;

        }

        private void connector_line_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var connector = sender as System.Windows.Shapes.Path;
           connector.Focus();
            
        }

        private void connector_line_KeyDown(object sender, KeyEventArgs e)
        {
            var con = (sender as System.Windows.Shapes.Path)?.DataContext;
            mainVM.SceneVM.unLink(con);
        }

        private void mainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            var mainCanvas = getMainCanvas();
            if (mainCanvas == null)
                return;

            int correction;
            if (e.Delta > 0) correction = 1; else correction = -1;

            var OldScale = mainCanvas.LayoutTransform.Value.M11;
            if ((OldScale <= 0.25 && correction == -1) || (OldScale >= 1.75 && correction == 1))
                return;


            
            double scale = OldScale +( 0.25 * correction);
            zoom_slider.Value = scale * 100;
            
            var newScaleForm = new ScaleTransform( scale,scale);
            mainCanvas.LayoutTransform = newScaleForm;
            mainCanvas.UpdateLayout();
            
            
        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            
        }

        private void export_repo_menu_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
          dlg.ShowDialog();

            if (dlg.FileName == "")
                return;

            string path = dlg.FileName;
            if (!path.Contains(".W3DLGREPO"))
                path = path + ".W3DLGREPO";

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                var ser = new DataContractSerializer(typeof(repoViewModel), null, 0x7FFF, false, true, null);
                ser.WriteObject(stream, mainVM.RepoVM);
               // MessageBox.Show("Saved", "w3dlgEdtior");
            }
        }

        private void import_repo_menu_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.ShowDialog();
            if (File.Exists(dlg.FileName))
            {
                var ser = new DataContractSerializer(typeof(repoViewModel), null, 0x7FFF, false, true, null);
                using (FileStream stream = new FileStream(dlg.FileName, FileMode.Open))
                {
                    object loaded;

                    loaded = ser.ReadObject(stream);

                    repoViewModel loadedRepo = loaded as repoViewModel;
                    if (loadedRepo != null)
                    {
                        mainVM.RepoVM = loadedRepo;
                      //  mainVM.updateAssets();

                    }
                    else
                    {
                        MessageBox.Show("error loading file");
                    }
                }


            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Properties.Settings.Default.Repo = mainVM.RepoVM;
            Properties.Settings.Default.Save();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void settings_menu_Click(object sender, RoutedEventArgs e)
        {
            new settings().ShowDialog();
        }

        private void encode_menu_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            if (!File.Exists(Properties.Settings.Default.Encoder_path))
            {
                MessageBox.Show("W2Scene.exe Not Found! Please Check settings", "Error");
                return;
            }
           info.FileName = Properties.Settings.Default.Encoder_path;
            info.Arguments = string.Format("--repo-dir {0} --output-dir {0}  --encode {1}",mainVM.SceneVM.Folder, mainVM.SceneVM.Folder+ "\\dialogscript");
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            info.CreateNoWindow = true;
          
            var pros = Process.Start(info);
            string output ="";
            while (!pros.StandardOutput.EndOfStream)
            {
                output =  output + pros.StandardOutput.ReadLine()+ " \n ";

            }

            var outputWindow  =  new encoderOutput();
            outputWindow.output_textBox.AppendText( output);
            outputWindow.ShowDialog();

        }
    }
}
