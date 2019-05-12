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

namespace DeepLibClient.Contols
{
    /// <summary>
    /// Logika interakcji dla klasy SearchTextBox.xaml
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        public SearchTextBox()
        {
            InitializeComponent();
        }

        private void SearchTextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text != "") { DefaultSearchLabelControl.Visibility = Visibility.Hidden; }
            else { DefaultSearchLabelControl.Visibility = Visibility.Visible; }
        }
    }
}
