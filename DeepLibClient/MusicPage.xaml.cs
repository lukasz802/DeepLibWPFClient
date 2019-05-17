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

namespace DeepLibClient
{
    /// <summary>
    /// Logika interakcji dla klasy MusicPage.xaml
    /// </summary>
    public partial class MusicPage : Page
    {
        public MusicPage()
        {
            InitializeComponent();
            MusicAlbumRadioButton.Checked += MenuContentButton_Checked;
            MusicTitleRadioButton.Checked += MenuContentButton_Checked;
            MusicPerformerRadioButton.Checked += MenuContentButton_Checked;
        }

        private void MenuContentButton_Checked(object sender, RoutedEventArgs e)
        {
            SharedFunctions.RadioButtonAnimation((RadioButton)sender, MusicTitleRadioButton, this, MusicMenuRectangle, WidthProperty, MarginProperty);
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            SharedFunctions.OnSortButtonClick((Button)sender);
        }
    }
}
