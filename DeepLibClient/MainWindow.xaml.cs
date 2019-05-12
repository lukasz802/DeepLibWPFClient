using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Media;

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
            MusicAlbumRadioButton.Checked += MenuContentButton_Checked;
            MusicTitleRadioButton.Checked += MenuContentButton_Checked;
            MusicPerformerRadioButton.Checked += MenuContentButton_Checked;
            MovieDirectorRadioButton.Checked += MenuContentButton_Checked;
            MovieTitleRadioButton.Checked += MenuContentButton_Checked;
            MovieYearRadioButton.Checked += MenuContentButton_Checked;
            BookAuthorRadioButton.Checked += MenuContentButton_Checked;
            BookTitleRadioButton.Checked += MenuContentButton_Checked;
            BookYearRadioButton.Checked += MenuContentButton_Checked;
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
            Random rnd = new Random();
            int img_num = rnd.Next(1, 4);

            MainPanelImage.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\IMG\\Image_" + img_num.ToString() + ".jpg", UriKind.Absolute));
            MainPanelImage.Height = (int)SystemParameters.MaximizedPrimaryScreenHeight - 40;
        }

        private void MenuContentButton_Checked(object sender, RoutedEventArgs e)
        {
            if (((RadioButton)sender).IsChecked != false)
            {
                Point tempPoint = new Point(0, 0);
                Point constPoint = new Point(0, 0);

                if (MainTabControl.SelectedIndex == 2) { constPoint = MusicTitleRadioButton.TransformToAncestor(MainTabControl).Transform(tempPoint); }
                else if (MainTabControl.SelectedIndex == 3) { constPoint = MovieTitleRadioButton.TransformToAncestor(MainTabControl).Transform(tempPoint); }
                else if (MainTabControl.SelectedIndex == 1) { constPoint = BookTitleRadioButton.TransformToAncestor(MainTabControl).Transform(tempPoint); }

                tempPoint.X = -constPoint.X;
                tempPoint.Y = -constPoint.Y;
                Point relativePoint = ((RadioButton)sender).TransformToAncestor(MainTabControl).Transform(tempPoint);
                ThicknessAnimation a = new ThicknessAnimation { To = new Thickness(relativePoint.X, 0, 0, 0) };
                DoubleAnimation d = new DoubleAnimation { To = ((RadioButton)sender).ActualWidth };
                a.Duration = d.Duration = new Duration(TimeSpan.Parse("0:0:0.08"));

                if (MainTabControl.SelectedIndex == 2)
                {
                    MusicMenuRectangle.BeginAnimation(WidthProperty, d);
                    MusicMenuRectangle.BeginAnimation(MarginProperty, a);
                }
                else if (MainTabControl.SelectedIndex == 3)
                {
                    MovieMenuRectangle.BeginAnimation(WidthProperty, d);
                    MovieMenuRectangle.BeginAnimation(MarginProperty, a);
                }
                else if (MainTabControl.SelectedIndex == 1)
                {
                    BookMenuRectangle.BeginAnimation(WidthProperty, d);
                    BookMenuRectangle.BeginAnimation(MarginProperty, a);
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text != "") { DefaultSearchLabel.Visibility = Visibility.Hidden; }
            else { DefaultSearchLabel.Visibility = Visibility.Visible; }
        }
    }
}
