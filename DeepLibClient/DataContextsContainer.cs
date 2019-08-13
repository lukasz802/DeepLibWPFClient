using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLibClient
{
    public static class DataContextsContainer
    {
        private static readonly List<INotifyPropertyChanged> container = new List<INotifyPropertyChanged>();

        public static List<INotifyPropertyChanged> Containers
        {
            get
            {
                return container;
            }
        }
    }
}
