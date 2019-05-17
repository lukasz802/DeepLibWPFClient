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
    public static class SharedFunctions
    {
        public static void RadioButtonAnimation(RadioButton selectedRadioButton, RadioButton firstRadioButton, Page container,
            Rectangle radioButtonRectangle, DependencyProperty widthProperty, DependencyProperty marginProperty)
        {
            if ((selectedRadioButton).IsChecked != false)
            {
                Point tempPoint = new Point(0, 0);
                Point constPoint = new Point(0, 0);
                constPoint = firstRadioButton.TransformToAncestor(container).Transform(tempPoint);
                tempPoint.X = -constPoint.X;
                tempPoint.Y = -constPoint.Y;
                Point relativePoint = (selectedRadioButton).TransformToAncestor(container).Transform(tempPoint);
                ThicknessAnimation a = new ThicknessAnimation { To = new Thickness(relativePoint.X, 0, 0, 0) };
                DoubleAnimation d = new DoubleAnimation { To = (selectedRadioButton).ActualWidth };
                a.Duration = d.Duration = new Duration(TimeSpan.Parse("0:0:0.08"));
                radioButtonRectangle.BeginAnimation(widthProperty, d);
                radioButtonRectangle.BeginAnimation(marginProperty, a);
            }
        }

        public static void OnSortButtonClick(Button sortButton)
        {
            if (sortButton.Content.ToString() == "A-Z") { sortButton.Content = "Z-A"; }
            else if (sortButton.Content.ToString() == "Z-A") { sortButton.Content = "A-Z"; }
            else if (sortButton.Content.ToString() == "0-9") { sortButton.Content = "9-0"; }
            else if (sortButton.Content.ToString() == "9-0") { sortButton.Content = "0-9"; }
        }
    }
}
