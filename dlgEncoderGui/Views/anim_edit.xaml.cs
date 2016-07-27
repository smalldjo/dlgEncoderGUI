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
    /// Interaction logic for anim_edit.xaml
    /// </summary>
    public partial class anim_edit : Window
    {
        public anim_edit()
        {
            InitializeComponent();
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            foreach(TextBox child in MainWindow.FindLogicalChildren<TextBox>(MainGrid))
            {
                var bnd = child.GetBindingExpression(TextBox.TextProperty);
                bnd.UpdateSource();
            }
            foreach (CheckBox child in MainWindow.FindLogicalChildren<CheckBox>(MainGrid))
            {
                var bnd = child.GetBindingExpression(CheckBox.IsCheckedProperty);
                bnd.UpdateSource();
            }

            this.Close();
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
