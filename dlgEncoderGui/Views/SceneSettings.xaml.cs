using dlgEncoderGui.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace dlgEncoderGui.Views
{
    /// <summary>
    /// Interaction logic for SceneSettings.xaml
    /// </summary>
    public partial class SceneSettings : Window
    {
        public SceneSettings()
        {
            InitializeComponent();
        }

        private void browse_folder_button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            var result =dlg.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                folder_textbox.Text = dlg.SelectedPath;
            }
            


        }


        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            
        
            foreach (System.Windows.Controls.TextBox child in MainWindow.FindLogicalChildren<System.Windows.Controls.TextBox>(mainGrid))
            {
                var bnd = child.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty);
                bnd.UpdateSource();
            }
            DialogResult = true;
            this.Close();
        }

    }
}

