using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            this.GotFocus += SearchTxtBox_Focus;
            this.LostFocus += SearchTxtBox_Focus;
            SearchButtonControl.Click += OnSearchClick;
            ClearButtonControl.Click += OnRemoveResultsClick;
            SearchTextBoxControl.TextChanged += OnTextChanged;
        }

        private RoutedEventArgs _args;

        private void SearchTextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text != "") { DefaultSearchLabelControl.Visibility = Visibility.Hidden; }
            else { DefaultSearchLabelControl.Visibility = Visibility.Visible; }
        }

        private void SearchTxtBox_Focus(object sender, RoutedEventArgs e)
        {
            if (this.IsKeyboardFocusWithin == false)
            {
                DefaultSearchLabelControl.Foreground = (Brush)Application.Current.FindResource("MenuContainerForegroundColorBrush");
                SearchTextBoxControl.Background = new SolidColorBrush() { Color = Colors.White, Opacity = 0.5 };
            }
            else
            {
                DefaultSearchLabelControl.Foreground = (Brush)Application.Current.FindResource("MenuBorderColorBrush");    
                SearchTextBoxControl.Background = Brushes.White;
            }

        }

        private void ClearButtonControl_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBoxControl.Clear();
        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            _args = new RoutedEventArgs(SearchClickEvent);
            RaiseEvent(_args);
        }

        public event RoutedEventHandler SearchClick
        {
            add { AddHandler(SearchClickEvent, value); }
            remove { RemoveHandler(SearchClickEvent, value); }
        }

        public static readonly RoutedEvent SearchClickEvent =
            EventManager.RegisterRoutedEvent("SearchClick",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchTextBox));

        private void OnRemoveResultsClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            _args = new RoutedEventArgs(RemoveResultsClickEvent);
            RaiseEvent(_args);
        }

        public event RoutedEventHandler RemoveResultsClick
        {
            add { AddHandler(RemoveResultsClickEvent, value); }
            remove { RemoveHandler(RemoveResultsClickEvent, value); }
        }

        public static readonly RoutedEvent RemoveResultsClickEvent =
            EventManager.RegisterRoutedEvent("RemoveResultsClick",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchTextBox));

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            _args = new RoutedEventArgs(TextChangedEvent);
            RaiseEvent(_args);
        }

        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        public static readonly RoutedEvent TextChangedEvent =
            EventManager.RegisterRoutedEvent("TextChanged",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(SearchTextBox));
    }
}
