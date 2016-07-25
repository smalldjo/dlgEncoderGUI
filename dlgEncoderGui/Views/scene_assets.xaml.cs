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
using System.Windows.Shapes;
using dlgEncoderGui.ViewModel;
using dlgEncoderGui.models.scene;

namespace dlgEncoderGui.Views
{
    /// <summary>
    /// Interaction logic for scene_assets.xaml
    /// </summary>
    public partial class scene_assets : Window
    {
      
        public scene_assets()
        {
            InitializeComponent();
            var loc = App.Current.FindResource("Locator") as ViewModelLocator;
            var assetsVM = DataContext as SceneAssetsViewModel;

            foreach(scene_actor act in assetsVM.Actors)
            {
                act.Entity = loc.Main.RepoVM.Entities.SingleOrDefault(i => i.Name == act.Entity?.Name);
            }
            foreach (scene_animation anim in assetsVM.Animations)
            {
                anim.Animation = loc.Main.RepoVM.Animations.SingleOrDefault(i => i.Name == anim.Animation?.Name);
            }
            foreach (scene_mimic mimic in assetsVM.Mimics)
            {
                mimic.Mimic = loc.Main.RepoVM.Mimics.SingleOrDefault(i => i.Name == mimic.Mimic?.Name);
            }
            foreach (scene_camera cam in assetsVM.Cameras)
            {
                cam.Camera = loc.Main.RepoVM.Cameras.SingleOrDefault(i => i.Name == cam.Camera?.Name);
            }
            foreach (scene_animation anim in assetsVM.MimicAnimations)
            {
                anim.Animation = loc.Main.RepoVM.Mimics_animations.SingleOrDefault(i => i.Name == anim.Animation?.Name);
            }

        }

        

        private void add_actor_button_Click(object sender, RoutedEventArgs e)
        {
            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.addNewActor();
        }

        private void delete_actor_button_Click(object sender, RoutedEventArgs e)
        {
            if (actor_assets_listView.SelectedItem == null)
                return;

            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.removeActor(actor_assets_listView.SelectedItem);

        }

        private void add_mimic_button_Click(object sender, RoutedEventArgs e)
        {
            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.addNewMimic();
        }

        private void delete_mimic_button_Click(object sender, RoutedEventArgs e)
        {
            if (mimics_assets_listView.SelectedItem == null)
                return;

            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.removeMimic(mimics_assets_listView.SelectedItem);

        }

        private void delete_animation_button1_Click(object sender, RoutedEventArgs e)
        {
            if (animations_assets_listView.SelectedItem == null)
                return;

            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.removeAnimation(animations_assets_listView.SelectedItem);
        }

        private void add_animation_button1_Click(object sender, RoutedEventArgs e)
        {
            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.addNewAnimation();
        }

        private void add_camera_button_Click(object sender, RoutedEventArgs e)
        {
            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.addNewCamera();
        }

        private void delete_camera_button_Click(object sender, RoutedEventArgs e)
        {
            if (cameras_assets_listView.SelectedItem == null)
                return;

            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.removeCamera(cameras_assets_listView.SelectedItem);
        }

        private void delete_mimicAnimation_button1_Click(object sender, RoutedEventArgs e)
        {
            if(mimicanimations_assets_listView.SelectedItem == null)
                return;

            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.removeMimicAnimation(mimicanimations_assets_listView.SelectedItem);
        }

        private void add_mimicAnimation_button1_Click(object sender, RoutedEventArgs e)
        {
            var SceneAssetsVM = DataContext as SceneAssetsViewModel;
            SceneAssetsVM.addNewMimicAnimation();
        }
    }
}
