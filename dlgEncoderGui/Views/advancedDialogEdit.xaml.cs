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
    /// Interaction logic for advancedDialogEdit.xaml
    /// </summary>
    public partial class advancedDialogEdit : Window
    {
        public advancedDialogEdit()
        {
            InitializeComponent();
        }


        //hack for Pause not showing 
        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            var act = (combo.DataContext as models.dlg_line)?.speaker?.Asset_name;
            if (act == "Pause")
                combo.SelectedIndex = 0;
                
        }
    }
}
