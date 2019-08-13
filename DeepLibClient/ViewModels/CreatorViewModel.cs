using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace DeepLibClient.ViewModels
{
    public class CreatorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Models.Creator> creatorsList;
        private BackgroundWorker task = null;
        private IList<Models.Creator> list;
        private bool isConnected;
        private MainWindowViewModel tempMainWindowViewModel;
        private MediaElementsViewModel tempMediaElementsViewModel;

        public event EventHandler ConnectionChanged;
        public ICommand CreatorDetailsCommand { get; private set; }
        public ICommand CreatorMediaElementsCommand { get; private set; }

        private void OnConnectionChanged()
        {
            ConnectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            set
            {
                isConnected = value;
                OnConnectionChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Models.Creator> CreatorsList
        {
            get { return creatorsList; }
        }

        public CreatorViewModel()
        {
            this.creatorsList = new ObservableCollection<Models.Creator>();
            this.CreatorDetailsCommand = new RelayCommand(CreatorDetails);
            this.CreatorMediaElementsCommand = new RelayCommand(CreatorMediaElements);
            task = new BackgroundWorker();
            task.DoWork += Task_DoWork;
            task.RunWorkerCompleted += Task_RunWorkerCompleted;
            task.RunWorkerAsync();
        }

        private void CreatorDetails(object obj)
        {
            Models.Creator creator = obj as Models.Creator;
            System.Diagnostics.Process.Start(String.Format("http://www.google.com/search?q={0}+{1}", creator.Name, creator.Surname));
        }

        private void CreatorMediaElements(object obj)
        {
            tempMediaElementsViewModel = (from INotifyPropertyChanged inpc in DataContextsContainer.Containers
                                          where inpc is MediaElementsViewModel
                                          select inpc).ToList().FirstOrDefault() as MediaElementsViewModel;
            tempMediaElementsViewModel.CreatorMediaElements.Clear();

            foreach (Models.MediaElement mediaElement in ((Models.Creator)obj).MediaElements)
            {
                Models.Creator temp = new Models.Creator() {
                                                                CreatorId = ((Models.Creator)obj).CreatorId,
                                                                Name = ((Models.Creator)obj).Name,
                                                                Surname = ((Models.Creator)obj).Surname
                                                            };
                mediaElement.Creator = temp;
                tempMediaElementsViewModel.CreatorMediaElements.Add(mediaElement);
            }

            tempMainWindowViewModel = (from INotifyPropertyChanged inpc in DataContextsContainer.Containers
                                       where inpc is MainWindowViewModel
                                       select inpc).ToList().FirstOrDefault() as MainWindowViewModel;
            tempMainWindowViewModel.IsShowingCreatorMediaElements = true;
            tempMainWindowViewModel.IsShowingCreatorMediaElements = false;
        }

        private void Task_DoWork(object sender, DoWorkEventArgs e)
        {
            IList<Models.Creator> result = SharedFunctions.ConnectionTestWithGetCreators();
            list = result;
        }

        private void Task_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (list != null)
            {
                Init(list);
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }

        private void Init(IList<Models.Creator> list)
        {
            foreach (Models.Creator cr in list)
            {
                creatorsList.Add(cr);
            }
        }

        internal void Refresh()
        {
            creatorsList.Clear();
            BackgroundWorker refresh_task = new BackgroundWorker();
            refresh_task.DoWork += Task_DoWork;
            refresh_task.RunWorkerCompleted += Task_RunWorkerCompleted;
            refresh_task.RunWorkerAsync();
        }
    }
}
