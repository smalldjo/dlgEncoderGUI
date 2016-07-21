using dlgEncoderGui.ViewModel;
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
using System.ComponentModel;

namespace dlgEncoderGui.Views
{
    /// <summary>
    /// Interaction logic for Repos.xaml
    /// </summary>
    public partial class Repos : Window
    {
        public Repos()
        {
            InitializeComponent();
        }

        private void cam_edit_Click(object sender, RoutedEventArgs e)
        {

            if (cam_listView.SelectedItem == null)
                return;
            Window cam_edit = new cameraEdit();
            cam_edit.DataContext = cam_listView.SelectedItem ;
            cam_edit.Show();
        }

        private void mimic_edit_Click(object sender, RoutedEventArgs e)
        {

            if (mimics_listView.SelectedItem == null)
                return;
            Window mimic_edit = new mimic_edit();
            mimic_edit.DataContext = mimics_listView.SelectedItem;
            mimic_edit.Show();
        }

        private void entity_edit_click(object sender, RoutedEventArgs e)
        {
            if (entities_listView.SelectedItem == null)
                return;
            Window entity_edit = new entityEdit(this);
            entity_edit.DataContext = entities_listView.SelectedItem;
            entity_edit.Show();
        }

        private void add_cam_click(object sender, RoutedEventArgs e)
        {
            var reposVM = DataContext as repoViewModel;
            reposVM.addNewCamera();
        }

        private void add_mimic_click(object sender, RoutedEventArgs e)
        {
            var reposVM = DataContext as repoViewModel;
            reposVM.addNewMimic();
        }

        private void add_entity_click(object sender, RoutedEventArgs e)
        {
            var reposVM = DataContext as repoViewModel;
            reposVM.addNewEntity();
        }

        private void delete_entity_click(object sender, RoutedEventArgs e)
        {
            if (entities_listView.SelectedItem == null)
                return;

            var reposVM = DataContext as repoViewModel;
            reposVM.deleteEntity(entities_listView.SelectedItem );
        }

        private void delete_mimic_click(object sender, RoutedEventArgs e)
        {
            if (mimics_listView.SelectedItem == null)
                return;
            var reposVM = DataContext as repoViewModel;
            reposVM.deleteMimic(mimics_listView.SelectedItem);
        }

        private void delete_camera_click(object sender, RoutedEventArgs e)
        {
            if (cam_listView.SelectedItem == null)
                return;
            var reposVM = DataContext as repoViewModel;
            reposVM.deleteCamera(cam_listView.SelectedItem);
        }

        private void add_animation_click(object sender, RoutedEventArgs e)
        {
            var reposVM = DataContext as repoViewModel;
            reposVM.addNewAnimation();
        }

        private void add_mimicanimation_click(object sender, RoutedEventArgs e)
        {
            var reposVM = DataContext as repoViewModel;
            reposVM.addNewMimicsAnimation();
        }

        private void delete_animation_click(object sender, RoutedEventArgs e)
        {
            if (animations_listView.SelectedItem == null)
                return;

            var reposVM = DataContext as repoViewModel;
            reposVM.deleteAnimation(animations_listView.SelectedItem);
        }

      /*  private void delete_mimicanimation_click(object sender, RoutedEventArgs e)
        {
            if (mimicAnimations_listView.SelectedItem == null)
                return;
            var reposVM = DataContext as repoViewModel;
            reposVM.deleteMimicAnimation(mimicAnimations_listView.SelectedItem);
        }*/

        private void animation_edit_Click(object sender, RoutedEventArgs e)
        {
            
            if (animations_listView.SelectedItem == null)
                return;
            Window edit = new anim_edit();
            edit.DataContext = animations_listView.SelectedItem;
            edit.Show();
        }

      /*  private void mimic_mimicanimation_Click(object sender, RoutedEventArgs e)
        {

            if (mimicAnimations_listView.SelectedItem == null)
                return;
            Window edit = new anim_edit();
            edit.DataContext = mimicAnimations_listView.SelectedItem;
            edit.Show();
        }
        */
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            var repo = DataContext as repoViewModel;
            Properties.Settings.Default.Repo = repo;
            Properties.Settings.Default.Save();

        }
    }


    
}
