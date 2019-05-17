using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.IO;

namespace DeepLibClient
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainPanelImage_Load;
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
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = AppData.RouteData.GetImageStream();
            image.EndInit();
            image.Freeze();
            MainPanelImage.Source = image;
            MainPanelImage.Height = (int)SystemParameters.MaximizedPrimaryScreenHeight - 40;
        } 
    }
}
