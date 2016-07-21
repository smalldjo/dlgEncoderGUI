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
    /// Interaction logic for entityEdit.xaml
    /// </summary>
    public partial class entityEdit : Window
    {
        public Window ParentW { get; set; }
        public entityEdit(Window parent)
        {
            InitializeComponent();
            ParentW = parent;
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            var controls = MainWindow.FindLogicalChildren<TextBox>(mainGrid);
            foreach(TextBox child in controls)
            {
                var bnd = child.GetBindingExpression(TextBox.TextProperty);
                bnd.UpdateSource();
            }
            var combos = MainWindow.FindLogicalChildren<ComboBox>(mainGrid);
            foreach(ComboBox child in combos)
            {
                var bnd = child.GetBindingExpression(ComboBox.SelectedItemProperty);
                bnd.UpdateSource();
                this.Close();

            }
        }

        private void cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
