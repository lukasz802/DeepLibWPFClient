using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeepLibClient
{
    /// <summary>
    /// Logika interakcji dla klasy BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            BookAuthorRadioButton.Checked += MenuContentButton_Checked;
            BookTitleRadioButton.Checked += MenuContentButton_Checked;
            BookYearRadioButton.Checked += MenuContentButton_Checked;
        }

        private void MenuContentButton_Checked(object sender, RoutedEventArgs e)
        {
            SharedFunctions.RadioButtonAnimation((RadioButton)sender, BookTitleRadioButton, this, BookMenuRectangle, WidthProperty, MarginProperty);
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            SharedFunctions.OnSortButtonClick((Button)sender);
        }
    }
}
