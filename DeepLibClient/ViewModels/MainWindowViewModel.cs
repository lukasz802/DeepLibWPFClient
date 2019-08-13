using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DeepLibClient.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private bool isConnected;
        private bool isAzureAvailable;
        private bool isShowingCreatorMediaElements;
        private BackgroundWorker task = null;
        private List<int> elements;
        private List<int> availableElements;
        private IList<Models.MediaElement> mediaElementList;
        private int creatorsCount;
        private MediaElementsViewModel tempMediaElementsViewModel;
        private CreatorViewModel tempCreatorViewModel;

        public event EventHandler ShowedCreatorMediaElements;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand RefreshCommand { get; private set; }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnShowedCreatorMediaElements()
        {
            ShowedCreatorMediaElements?.Invoke(this, EventArgs.Empty);
        }

        public bool IsShowingCreatorMediaElements
        {
            get { return isShowingCreatorMediaElements; }
            set
            {
                if (value == true)
                {
                    OnShowedCreatorMediaElements();
                }
                isShowingCreatorMediaElements = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                OnPropertyChanged();
            }
        }

        public bool IsAzureAvailable
        {
            get { return isAzureAvailable; }
            set
            {
                isAzureAvailable = value;
                OnPropertyChanged();
            }
        }

        public int CDsCount
        {
            get { return elements[0]; }
            set
            {
                elements[0] = value;
                this.MediaElementsCount = 0;
                OnPropertyChanged();
            }
        }

        public int DVDsCount
        {
            get { return elements[1]; }
            set
            {
                elements[1] = value;
                this.MediaElementsCount = 0;
                OnPropertyChanged();
            }
        }

        public int BooksCount
        {
            get { return elements[2]; }
            set
            {
                elements[2] = value;
                this.MediaElementsCount = 0;
                OnPropertyChanged();
            }
        }

        public int AvailableCDsCount
        {
            get { return availableElements[0]; }
            set
            {
                availableElements[0] = value;
                this.AvailableMediaElementsCount = 0;
                OnPropertyChanged();
            }
        }

        public int AvailableDVDsCount
        {
            get { return availableElements[1]; }
            set
            {
                availableElements[1] = value;
                this.AvailableMediaElementsCount = 0;
                OnPropertyChanged();
            }
        }

        public int AvailableBooksCount
        {
            get { return availableElements[2]; }
            set
            {
                availableElements[2] = value;
                this.AvailableMediaElementsCount = 0;
                OnPropertyChanged();
            }
        }

        public int AvailableMediaElementsCount
        {
            get
            {
                return availableElements.Sum();
;
            }
            private set
            {
                OnPropertyChanged();
            }
        }

        public int MediaElementsCount
        {
            get { return elements.Sum(); }
            private set
            {
                OnPropertyChanged();
            }
        }

        public int CreatorsCount
        {
            get { return creatorsCount; }
            set
            {
                creatorsCount = value;
                OnPropertyChanged();
            }
        }

        private void Task_DoWork(object sender, DoWorkEventArgs e)
        {
            IList<Models.MediaElement> result = SharedFunctions.ConnectionTestWithGetMediaElements();
            mediaElementList = result;

            if (mediaElementList != null) { this.CreatorsCount = SharedFunctions.ConnectionTestWithGetCreators().Count; }
        }

        public MainWindowViewModel()
        {
            this.IsAzureAvailable = SharedFunctions.AzureStorageConnectionCheck();
            elements = new List<int>() { 0, 0, 0 };
            availableElements = new List<int>() { 0, 0, 0 };
            this.RefreshCommand = new RelayCommand(execute => Refresh());
            task = new BackgroundWorker() { WorkerSupportsCancellation = true };
            task.DoWork += Task_DoWork;
            task.RunWorkerCompleted += Task_RunWorkerCompleted;
            task.RunWorkerAsync();
        }

        internal void Refresh()
        {
            if (task.IsBusy == false)
            {
                this.IsAzureAvailable = SharedFunctions.AzureStorageConnectionCheck();
                tempMediaElementsViewModel = (from INotifyPropertyChanged inpc in DataContextsContainer.Containers
                                              where inpc is MediaElementsViewModel
                                              select inpc).ToList().FirstOrDefault() as MediaElementsViewModel;
                tempMediaElementsViewModel.Refresh();

                tempCreatorViewModel = (from INotifyPropertyChanged inpc in DataContextsContainer.Containers
                                        where inpc is CreatorViewModel
                                        select inpc).ToList().FirstOrDefault() as CreatorViewModel;
                tempCreatorViewModel.Refresh();

                CDsCount = 0;
                DVDsCount = 0;
                BooksCount = 0;
                AvailableCDsCount = 0;
                AvailableDVDsCount = 0;
                AvailableBooksCount = 0;
                CreatorsCount = 0;
                task.RunWorkerAsync();
            }
        }

        private void Task_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (mediaElementList != null)
            {
                Init(mediaElementList);
                this.IsConnected = true;
            }
            else
            {
                this.IsConnected = false;
            }
        }

        private void Init(IList<Models.MediaElement> list)
        {
            foreach (Models.MediaElement me in list)
            {
                if (me.MediaType == 0) { this.CDsCount += 1; }
                else if (me.MediaType == 1) { this.DVDsCount += 1; }
                else if (me.MediaType == 2) { this.BooksCount += 1; }

                if (me.IsBorrowed == false && me.MediaType == 0) { this.AvailableCDsCount +=1; }
                else if (me.IsBorrowed == false && me.MediaType == 1) { this.AvailableDVDsCount += 1; }
                else if (me.IsBorrowed == false && me.MediaType == 2) { this.AvailableBooksCount += 1; }
            }
        }
    }
}
