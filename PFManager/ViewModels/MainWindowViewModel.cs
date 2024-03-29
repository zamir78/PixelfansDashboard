﻿using CommunityToolkit.Mvvm.Input;
using PFManager.DataLayer;
using PFManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PFManager.ViewModels
{
    public class MainWindowViewModel : NotifyBase
    {
        public MainWindowViewModel()
        {
            UserAddRemove = new User();
            FeedAddRemove = new EcFeeding();
            DataAccess da = new DataAccess(new LiteDBAPI("PFMgrLiteDb.db"));

            AddUserCommand = new RelayCommand(AddUserCommandHandler);
            RemoveUserCommand = new RelayCommand(RemoveUserCommandHandler);

            AddFeedingCommand = new RelayCommand(AddFeedingCommandHandler);
            RemoveFeedingCommand = new RelayCommand(RemoveFeedingCommandHandler);

            Users = DataAccess.DataBaseAPI.LoadDocuments<User>(DataAccess.Users);
            Feedings = DataAccess.DataBaseAPI.LoadDocuments<EcFeeding>(DataAccess.Feedings);
        }

        public string? UserID { get; set; }

        private User _userAddRemove = null!;
        public User UserAddRemove 
        {
            get { return _userAddRemove; } 
            set
            {
                _userAddRemove = value;
                OnPropertyChanged();
            }
        }

        public List<User> Users { get; private set; }

        public string? KKSID { get; set; }

        private EcFeeding _feedAddRemove = null!;
        public EcFeeding FeedAddRemove
        {
            get { return _feedAddRemove; }
            set
            {
                _feedAddRemove = value;
                OnPropertyChanged();
            }
        }

        public List<EcFeeding> Feedings { get; private set; }

        #region Commands

        public RelayCommand AddUserCommand { get; set; }
        private void AddUserCommandHandler()
        {
            if (Users.Exists(x => x.IdNumber == UserAddRemove.IdNumber) == false)
                DataAccess.DataBaseAPI.InsertDocument(DataAccess.Users, UserAddRemove);
            else
                MessageBox.Show("A user with this ID number already exists");
        }

        public RelayCommand RemoveUserCommand { get; set; }
        private void RemoveUserCommandHandler()
        {
            var userToRemove = Users.Single(x => x.IdNumber == UserID);
            if(userToRemove != null) 
            {
                DataAccess.DataBaseAPI.DeleteRecord<User>(DataAccess.Users, userToRemove.Id);
                Users.Remove(userToRemove);
                UserID = null;
                OnPropertyChanged("UserID");
            }
        }

        public RelayCommand AddFeedingCommand { get; set; }
        private void AddFeedingCommandHandler()
        {
            if (Feedings.Exists(x => x.KKS == FeedAddRemove.KKS) == false)
                DataAccess.DataBaseAPI.InsertDocument(DataAccess.Feedings, FeedAddRemove);
            else
                MessageBox.Show("A feeding with this KKS already exists");
        }

        public RelayCommand RemoveFeedingCommand { get; set; }
        private void RemoveFeedingCommandHandler()
        {
            var feedingToRemove = Feedings.Single(x => x.KKS == KKSID);
            if (feedingToRemove != null)
            {
                DataAccess.DataBaseAPI.DeleteRecord<EcFeeding>(DataAccess.Feedings, feedingToRemove.Id);
                Feedings.Remove(feedingToRemove);
                KKSID = null;
                OnPropertyChanged("KKSID");
            }
        }

        #endregion
    }
}
