using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DeepLibClient.Contols
{
    public class AutoGreyableImage : Image
    {
        static AutoGreyableImage()
        {
            IsEnabledProperty.OverrideMetadata(typeof(AutoGreyableImage), new FrameworkPropertyMetadata(true, OnAutoGreyScaleImageIsEnabledPropertyChanged));
        }

        private static void OnAutoGreyScaleImageIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            AutoGreyableImage autoGreyScaleImg = (AutoGreyableImage)source;
            bool isEnable = (bool)args.NewValue;

            if (autoGreyScaleImg != null)
            {
                if (isEnable == false)
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(autoGreyScaleImg.Source.ToString()));
                    autoGreyScaleImg.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray32Float, null, 0);
                    autoGreyScaleImg.OpacityMask = new ImageBrush(bitmapImage);
                }
                else
                {
                    autoGreyScaleImg.Source = ((FormatConvertedBitmap)autoGreyScaleImg.Source).Source;
                    autoGreyScaleImg.OpacityMask = null;
                }
            }
        }
    }
}
