using CommunityToolkit.Mvvm.Input;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelfansDashboard
{
    public class MainWindowViewModel : NotifyViewModelBase
    {
        IFirebaseClient _client;
        public MainWindowViewModel()
        {
            PixelCollectionList = new ObservableCollection<PixelViewModel>();
            UploadCommand = new RelayCommand(UploadCommandHandler);
            DownloadCommand = new RelayCommand(DownloadCommandHandler);
            NumberOfRows = 5;
            NumberOfCols = 5;
            InitPixels();
            ConfigFireBase();
        }

        private void ConfigFireBase()
        {
            IFirebaseConfig _fireBaseConf = new FirebaseConfig() { BasePath = "https://fansfixels.firebaseio.com", AuthSecret = "QZ8gvfrLFzT5zqxiccvYGjJThy4EwkSBVDHglNae" };
            _client = new FirebaseClient(_fireBaseConf);
        }

        private int _numberOfRows;
        public int NumberOfRows
        {
            get { return _numberOfRows; }
            set 
            { 
                _numberOfRows = value;
                OnPropertyChanged();
                InitPixels();
            }
        }

        private int _numberOfCols;
        public int NumberOfCols
        {
            get { return _numberOfCols; }
            set 
            { 
                _numberOfCols = value;
                OnPropertyChanged();
                InitPixels();
            }
        }

        private ObservableCollection<PixelViewModel> _pixelCollectionList;

        public ObservableCollection<PixelViewModel> PixelCollectionList
        {
            get { return _pixelCollectionList; }
            set 
            { 
                _pixelCollectionList = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand UploadCommand { get; set; }
        public RelayCommand DownloadCommand { get; set; }

        private void InitPixels()
        {
            PixelCollectionList = new ObservableCollection<PixelViewModel>();
            for(int i = 0; i < NumberOfCols; i++)
            {
                for(int j = 0; j < NumberOfRows; j++)
                    PixelCollectionList.Add( new PixelViewModel() {Location = new System.Windows.Point(i, j) });
            }
        }

        private void UploadCommandHandler()
        {
            PixelCollection pc = new PixelCollection() { Width = NumberOfCols,Height=NumberOfRows };
            foreach (PixelViewModel pixel in PixelCollectionList)
                pc.PixelList.Add(new Pixel() { IsLit = pixel.IsLit, X = (int)pixel.Location.X, Y = (int)pixel.Location.Y });

            string json = JsonConvert.SerializeObject(pc);
            _client.Update("PixelImage", pc);
        }

        private void DownloadCommandHandler()
        {
            PixelCollection pcLoad = _client.Get("PixelImage").ResultAs<PixelCollection>();
            _numberOfRows = pcLoad.Height;
            OnPropertyChanged("NumberOfRows");
            _numberOfCols = pcLoad.Width;
            OnPropertyChanged("NumberOfCols");
            PixelCollectionList = new ObservableCollection<PixelViewModel>();
            
            foreach (Pixel pixel in pcLoad.PixelList)
            {
                PixelCollectionList.Add(new PixelViewModel() { IsLit = pixel.IsLit ,Location = new System.Windows.Point(pixel.X, pixel.Y)});
            }

            OnPropertyChanged("PixelCollectionList");
        }
    }
}
