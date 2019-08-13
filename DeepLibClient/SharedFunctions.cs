using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AzureStorage;

namespace DeepLibClient
{
    public static class SharedFunctions
    {
        public static void RadioButtonAnimation(RadioButton selectedRadioButton, RadioButton firstRadioButton, Window container,
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

        public static IList<Models.MediaElement> ConnectionTestWithGetMediaElements()
        {
            IList<Models.MediaElement> list = null;

            try
            {
                MyAPI client = App.GetDeepLibClient();
                list = client.GetMediaElements();
            }
            catch
            {
                return null;
            }

            return list;
        }

        public static IList<Models.Creator> ConnectionTestWithGetCreators()
        {
            IList<Models.Creator> list = null;

            try
            {
                MyAPI client = App.GetDeepLibClient();
                list = client.GetCreators();
            }
            catch
            {
                return null;
            }

            return list;
        }

        public static bool ConnectionTest()
        {
            bool result = false;

            WebRequest request = WebRequest.Create(App.SERVICE_URL + "api/MediaElements/5");
            request.Timeout = 2000;

            try
            {
                request.GetResponse();
                return true;
            }
            catch
            {
                return result;
            }
        }

        public static void ModifyMediaElementCover(int mediaElementId, Models.MediaElement mediaElement)
        {
            try
            {
                MyAPI client = App.GetDeepLibClient();
                client.PutMediaElement(mediaElementId, mediaElement);
            }
            catch (Microsoft.Rest.HttpOperationException)
            {
                return;
            }
        }

        public static bool AzureStorageConnectionCheck()
        {
            Process[] processes = Process.GetProcessesByName("AzureStorageEmulator");

            if (processes.Count() != 0) { return true; }
            else { return false; }
        }
    }
}
