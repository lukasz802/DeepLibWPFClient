using DeepLibClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;

namespace DeepLibClient
{
    /// <summary>
    /// Logika interakcji dla klasy MyLibraryPae.xaml
    /// </summary>
    public partial class MyLibraryPage : Page
    {
        private MediaElementsViewModel viewModel = new MediaElementsViewModel();

        public MyLibraryPage()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
            this.Loaded += MyLibraryPage_Loaded;
            viewModel.ConnectionChanged += ViewModel_ConnectionChanged;
            MyLibraryListBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Title", System.ComponentModel.ListSortDirection.Ascending));
            DataContextsContainer.Containers.Add(viewModel);
        }

        private MainWindowViewModel tempMainWindowViewModel;

        private void MyLibraryPage_Loaded(object sender, RoutedEventArgs e)
        {
            tempMainWindowViewModel = (from INotifyPropertyChanged inpc in DataContextsContainer.Containers
                                       where inpc is MainWindowViewModel
                                       select inpc).ToList().FirstOrDefault() as MainWindowViewModel;
            tempMainWindowViewModel.ShowedCreatorMediaElements += (s,a) => SearchUserControl_SearchTriggered();
        }

        private void ViewModel_ConnectionChanged(object sender, EventArgs e)
        {
            if (viewModel.IsConnected == false)
            {
                Connection1Image.Visibility = Visibility.Visible;
                Connection2Image.Visibility = Visibility.Visible;
                MediaInfoGrid.Visibility = Visibility.Collapsed;
                ConnectionPropertiesGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                Connection1Image.Visibility = Visibility.Collapsed;
                Connection2Image.Visibility = Visibility.Collapsed;
                MediaInfoGrid.Visibility = Visibility.Visible;
                ConnectionPropertiesGrid.Visibility = Visibility.Visible;
            }
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            SharedFunctions.OnSortButtonClick((Button)sender);
            MyLibraryListBox.Items.SortDescriptions.Clear();

            if (((Button)sender).Content.ToString() == "Z-A")
            {
                MyLibraryListBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Title", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                MyLibraryListBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Title", System.ComponentModel.ListSortDirection.Descending));
            }
        }

        private void SearchUserControl_SearchClick(object sender, RoutedEventArgs e)
        {
            if (SearchUserControl.SearchTextBoxControl.Text != "")
            {
                viewModel.FilteredMediaElementsList.Filter = MediaElementFilter;
                SearchGrid.Visibility = Visibility.Visible;
                TitleGrid.Visibility = Visibility.Collapsed;
                ControlGrid.Height = new GridLength(130);
            }
        }

        private void SearchUserControl_SearchTriggered()
        {
            viewModel.FilteredMediaElementsList.Filter = CreatorMediaElementFilter;

            if (viewModel.CreatorMediaElements[0].Creator.Surname != null)
            {
                SearchUserControl.SearchTextBoxControl.Text = String.Format("{0} {1}", viewModel.CreatorMediaElements[0].Creator.Name, viewModel.CreatorMediaElements[0].Creator.Surname);
            }
            else { SearchUserControl.SearchTextBoxControl.Text = String.Format("{0}", viewModel.CreatorMediaElements[0].Creator.Name); }

            SearchGrid.Visibility = Visibility.Visible;
            TitleGrid.Visibility = Visibility.Collapsed;
            CDCheckBox.IsChecked = true;
            DVDCheckBox.IsChecked = true;
            BookCheckBox.IsChecked = true;
            ControlGrid.Height = new GridLength(130);
        }

        private bool MediaElementFilter(object element)
        {
            Models.MediaElement me = element as Models.MediaElement;
            List<int> list = new List<int>();

            if (CDCheckBox.IsChecked == true) { list.Add(0); }
            if (DVDCheckBox.IsChecked == true) { list.Add(1); }
            if (BookCheckBox.IsChecked == true) { list.Add(2); }

            return (me.Title.ToLower().Contains(SearchUserControl.SearchTextBoxControl.Text.ToLower()) 
                    || (me.Creator.Name == null ? false : me.Creator.Name.ToLower().Contains(SearchUserControl.SearchTextBoxControl.Text.ToLower()))
                    || (me.Creator.Surname == null ? false : me.Creator.Surname.ToLower().Contains(SearchUserControl.SearchTextBoxControl.Text.ToLower()))
                    || SearchUserControl.SearchTextBoxControl.Text.ToLower().Contains(me.Title.ToLower())
                    || (me.Creator.Name == null ? false : SearchUserControl.SearchTextBoxControl.Text.ToLower().Contains(me.Creator.Name.ToLower()))
                    || (me.Creator.Surname == null ? false : SearchUserControl.SearchTextBoxControl.Text.ToLower().Contains(me.Creator.Surname.ToLower())))
                    && list.Contains((int)me.MediaType);       
        }

        private bool CreatorMediaElementFilter(object element)
        {
            Models.MediaElement me = element as Models.MediaElement;
            List<int> temp = new List<int>();

            foreach (Models.MediaElement mediaElement in viewModel.CreatorMediaElements)
            {
                temp.Add(Convert.ToInt32(mediaElement.MediaElementId));
            }

            return temp.Contains(Convert.ToInt32(me.MediaElementId));
        }

        private void SearchUserControl_RemoveResultsClick(object sender, RoutedEventArgs e)
        {
            if (sender == ShowAllButton)
            {
                if (viewModel.FilteredMediaElementsList.Filter != null) { viewModel.FilteredMediaElementsList.Filter = null; }
                SearchGrid.Visibility = Visibility.Collapsed;
                TitleGrid.Visibility = Visibility.Visible;
                ControlGrid.Height = new GridLength(55);
                SearchUserControl.SearchTextBoxControl.Text = "";
                CDCheckBox.IsChecked = true;
                DVDCheckBox.IsChecked = true;
                BookCheckBox.IsChecked = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            byte val = 0;

            if (CDCheckBox.IsChecked == true) { val += 1; }
            if (DVDCheckBox.IsChecked == true) { val += 1; }
            if (BookCheckBox.IsChecked == true) { val += 1; }
            if (val == 0) {((CheckBox)sender).IsChecked = true; }
        }

        private void SearchUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { SearchUserControl_SearchClick(sender, null); }
        }
    }
}
