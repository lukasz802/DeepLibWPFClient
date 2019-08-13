using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Linq;
using System.IO;
using System.Collections.ObjectModel;
using Microsoft.Rest;
using DeepLibClient.ViewModels;
using System.Diagnostics;
using System.Windows.Threading;
using System.Collections.Generic;

namespace DeepLibClient
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        { 
            InitializeComponent();
            this.Loaded += MainPanelImage_Load;
            this.Loaded += (s, e) => { this.DataContext = this.viewModel; };
            MediaElementsRadioButton.Checked += MenuContentButton_Checked;
            CreatorsRadioButton.Checked += MenuContentButton_Checked;
            DataContextsContainer.Containers.Add(viewModel);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == CloseButton) { this.Close(); }
            else if (sender == MaximizeButton)
            {
                if (this.WindowState != WindowState.Maximized) { this.WindowState = WindowState.Maximized; }
                else { this.WindowState = WindowState.Normal; }
            }
            else if (sender == MinimizeButton) { this.WindowState = WindowState.Minimized; }
        }

        private void MainPanelImage_Load(object sender, RoutedEventArgs e)
        {
            KeyValuePair<Stream, string> kvp = AppData.RouteData.GetImageWithBrushPair();
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = kvp.Key;
            image.EndInit();
            image.Freeze();
            MainPanelImage.Source = image;
            Application.Current.Resources["CreatorsDetailsPanelColorBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(kvp.Value));
            MainPanelImage.Height = (int)SystemParameters.MaximizedPrimaryScreenHeight - 40;
        }

        private void MenuContentButton_Checked(object sender, RoutedEventArgs e)
        {
            SharedFunctions.RadioButtonAnimation((RadioButton)sender, MediaElementsRadioButton, this, MediaElementsMenuRectangle, WidthProperty, MarginProperty);
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            SharedFunctions.OnSortButtonClick((Button)sender);
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            ReportPanel.Visibility = Visibility.Visible;
            DoubleAnimation a = new DoubleAnimation {From = 0, To = 100 };
            a.Duration = new Duration(TimeSpan.Parse("0:0:25"));
            ReportPanel.BeginAnimation(OpacityProperty, a);
        }
        private void MainTabControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ReportPanel.Visibility = Visibility.Hidden;
        }
    }
}
