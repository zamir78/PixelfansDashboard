using CommunityToolkit.Mvvm.Input;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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
            PlayCommand = new RelayCommand(PlayCommandHandler);
            LoadImageCommand = new RelayCommand(LoadImageCommandHandler);
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

        public bool IsPlaying { get; set; }
        public RelayCommand UploadCommand { get; set; }
        public RelayCommand DownloadCommand { get; set; }
        public RelayCommand PlayCommand { get; set; }
        public RelayCommand LoadImageCommand { get; set; }

        private void InitPixels()
        {
            PixelCollectionList = new ObservableCollection<PixelViewModel>();
            for (int i = 0; i < NumberOfCols; i++)
            {
                for (int j = 0; j < NumberOfRows; j++)
                    PixelCollectionList.Add(new PixelViewModel() { Location = new System.Windows.Point(i, j) });
            }
        }

        PixelCollection pc;
        private void UploadCommandHandler()
        {
            pc = new PixelCollection();
            pc.Data.Columns = NumberOfCols;
            pc.Data.Rows = NumberOfRows;
            foreach (PixelViewModel pixel in PixelCollectionList)
                pc.PixelList.Add(new Pixel() { IsLit = pixel.IsLit, X = (int)pixel.Location.X + 1, Y = (int)pixel.Location.Y + 1 });

            string json = JsonConvert.SerializeObject(pc);
            pc.IsPlaying = IsPlaying;
            _client.Update("PixelImage", pc);
        }

        private void DownloadCommandHandler()
        {
            pc = _client.Get("PixelImage").ResultAs<PixelCollection>();
            _numberOfRows = pc.Data.Rows;
            OnPropertyChanged("NumberOfRows");
            _numberOfCols = pc.Data.Columns;
            OnPropertyChanged("NumberOfCols");
            PixelCollectionList = new ObservableCollection<PixelViewModel>();

            foreach (Pixel pixel in pc.PixelList)
            {
                PixelCollectionList.Add(new PixelViewModel() { IsLit = pixel.IsLit, Location = new System.Windows.Point(pixel.X, pixel.Y) });
            }

            OnPropertyChanged("PixelCollectionList");
        }

        private void PlayCommandHandler()
        {
            UploadCommandHandler();
        }

        private void LoadImageCommandHandler()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "bmp files (*.bmp)|*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                Bitmap bmp = new Bitmap(openFileDialog.FileName);
                Bitmap scaled = new Bitmap(bmp, NumberOfCols, NumberOfRows);
                scaled.Save($@"C:\temp\Scaled.bmp");
                for (int i = 0; i < scaled.Height; i++)
                    for (int j = 0; j < scaled.Width; j++)
                    {
                        var color = scaled.GetPixel(j, i);
                        PixelCollectionList[j + (i * scaled.Width)].IsLit = color.R > 220;
                        Console.WriteLine(j + (i * scaled.Width));
                    }
            }
        }
    }
}
