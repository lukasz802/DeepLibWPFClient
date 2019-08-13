using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using AzureStorage;
using System.Net;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Interop;

namespace DeepLibClient.ViewModels
{
    public class MediaElementsViewModel : INotifyPropertyChanged
    {
        private bool isAzureAvailable;
        private ObservableCollection<Models.MediaElement> mediaElementsList;
        private BackgroundWorker task = null;
        private IList<Models.MediaElement> list;
        private readonly List<Models.MediaElement> creatorMediaElements;
        private bool isConnected;
        public event EventHandler ConnectionChanged;

        public ICollectionView FilteredMediaElementsList { get; private set; }
        public ICommand UploadCoverCommand { get; private set; }
        public ICommand CoverValidationCommand { get; private set; }

        private void OnConnectionChanged()
        {
            ConnectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public List<Models.MediaElement> CreatorMediaElements
        {
            get
            {
                return creatorMediaElements;
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

        public MediaElementsViewModel()
        {
            creatorMediaElements = new List<Models.MediaElement>();
            this.IsAzureAvailable = SharedFunctions.AzureStorageConnectionCheck();
            this.mediaElementsList = new ObservableCollection<Models.MediaElement>();
            this.UploadCoverCommand = new RelayCommand(UploadCover);
            this.CoverValidationCommand = new RelayCommand(CoverValidationRule);
            this.FilteredMediaElementsList = CollectionViewSource.GetDefaultView(mediaElementsList);
            task = new BackgroundWorker();
            task.DoWork += Task_DoWork;
            task.RunWorkerCompleted += Task_RunWorkerCompleted;
            task.RunWorkerAsync();
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

        private void Task_DoWork(object sender, DoWorkEventArgs e)
        {
            IList<Models.MediaElement> result = SharedFunctions.ConnectionTestWithGetMediaElements();
            list = result;
        }

        private void Init(IList<Models.MediaElement> list)
        {
            foreach (Models.MediaElement me in list)
            {
                mediaElementsList.Add(me);
            }
        }

        internal void Refresh()
        {
            this.IsAzureAvailable = SharedFunctions.AzureStorageConnectionCheck();
            mediaElementsList.Clear();
            BackgroundWorker refresh_task = new BackgroundWorker();
            refresh_task.DoWork += Task_DoWork;
            refresh_task.RunWorkerCompleted += Task_RunWorkerCompleted;
            refresh_task.RunWorkerAsync();
        }

        internal void CoverValidationRule(object obj)
        {
            Task.Factory.StartNew(() =>
            {
                if ( obj != null && ((Models.MediaElement)obj).Cover != null)
                {
                    WebRequest request = WebRequest.Create(((Models.MediaElement)obj).Cover);
                    request.Timeout = 5000;
                    request.Method = "HEAD";

                    try
                    {
                        request.GetResponse();
                    }
                    catch
                    {
                        ((Models.MediaElement)obj).Cover = null;
                    }
                }
            });
        }

        private void WarningTaskDialog(object obj)
        {
            TaskDialog dialog = new TaskDialog
            {
                Caption = Application.Current.MainWindow.Title.ToString(),
                InstructionText = "Brak połączenia z bazą DeepLIB",
                Text = "Wystąpił nieznany problem podczas próby połączenia z bazą danych DeepLIB. Wykonywanie operacji zostało przerwane.",
                Icon = TaskDialogStandardIcon.Error,
                Cancelable = true,
                StartupLocation = TaskDialogStartupLocation.CenterOwner,
                OwnerWindowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle
            };

            TaskDialogButton customButtonYes = new TaskDialogButton("customButtonYes", "Spróbuj ponownie");
            TaskDialogButton customButtonNo = new TaskDialogButton("customButtonNo", "Anuluj");

            customButtonNo.Click += (s,e) => { dialog.Close(); };
            customButtonYes.Click += (s, e) => {
                                                    IList<object> loc = new List<object> { s, obj };
                                                    dialog.Close();

                                                    if (SharedFunctions.ConnectionTest() == true) { DialogResultFunction(loc, e); }
                                                    else { WarningTaskDialog(obj); }
                                               };
            dialog.Controls.Add(customButtonYes);
            dialog.Controls.Add(customButtonNo);
            dialog.Show();
        }

        private void DialogResultFunction(object sender, EventArgs e)
        {
            IList<object> loc = sender as IList<object>;

            if (((TaskDialogButton)loc[0]).Name == "customButtonYes")
            {
                UploadCover(loc[1]);
            }
        }

        internal void UploadCover(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki obrazów (*.jpeg;*.jpg;*.bmp;*.png;*.gif;*.tiff;*tif)|*.jpeg;*.jpg;*.bmp;*.png;*.gif;*.tiff;*tif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (SharedFunctions.ConnectionTest() == false) { WarningTaskDialog(obj); }
                else
                {
                    Models.MediaElement mediaElement = obj as Models.MediaElement;

                    if (mediaElement.Cover == null)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            string coverPath = CoverRepository.Upload(openFileDialog.FileName).GetAwaiter().GetResult();
                            mediaElement.Cover = coverPath;
                        })
                             .ContinueWith(next =>
                             {
                                 SharedFunctions.ModifyMediaElementCover(Convert.ToInt32(mediaElement.MediaElementId), mediaElement);
                             });
                    }
                    else
                    {
                        Task.Factory.StartNew(() =>
                        {
                            string coverPath = CoverRepository.Update(openFileDialog.FileName, mediaElement.Cover).GetAwaiter().GetResult();
                            mediaElement.Cover = coverPath;
                        })
                        .ContinueWith(next =>
                        {
                            SharedFunctions.ModifyMediaElementCover(Convert.ToInt32(mediaElement.MediaElementId), mediaElement);
                        });
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Models.MediaElement> MediaElementsList
        {
            get { return mediaElementsList; }
        }
    }
}
