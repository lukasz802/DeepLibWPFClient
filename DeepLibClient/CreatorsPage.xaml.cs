using DeepLibClient.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private CreatorViewModel viewModel = new CreatorViewModel();

        public BooksPage()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
            DataContextsContainer.Containers.Add(viewModel);
        }

        private void CreatorsListBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double temp = (e.NewSize.Width - 37.0) / Math.Floor((e.NewSize.Width - 37.0) / 150.0) - 110.0;
            CreatorsListBox.AutoSizableMargin = new Thickness(temp, 0, 0, 0);
        }
    }
}
