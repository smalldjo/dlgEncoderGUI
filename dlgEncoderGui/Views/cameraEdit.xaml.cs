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

namespace dlgEncoderGui.Views
{
    /// <summary>
    /// Interaction logic for cameraEdit.xaml
    /// </summary>
    public partial class cameraEdit : Window
    {
        public cameraEdit()
        {
            InitializeComponent();
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            var mytextboxes = MainWindow.FindLogicalChildren<TextBox>(mainGrid);
            foreach (TextBox txtbx in mytextboxes )
            {
                string txt = txtbx.Text;
                BindingExpression bnd = txtbx.GetBindingExpression(TextBox.TextProperty);
                bnd.UpdateSource();
            }
            this.Close();
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
