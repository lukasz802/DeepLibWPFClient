using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DeepLibClient.Contols
{
    public class AutoSizableListBox : ListBox
    {
        public AutoSizableListBox()
        {
            this.AutoSizableMargin = new Thickness(0);
        }

        public static readonly DependencyProperty AutoSizableMarginProperty =
           DependencyProperty.Register("AutoSizableMargin", typeof(Thickness), typeof(AutoSizableListBox));

        public Thickness AutoSizableMargin
        {
            get { return (Thickness)GetValue(AutoSizableMarginProperty); }
            set { SetValue(AutoSizableMarginProperty, value); }
        }
    }
}
