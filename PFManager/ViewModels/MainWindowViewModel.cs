using CommunityToolkit.Mvvm.Input;
using PFManager.DataLayer;
using PFManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFManager.ViewModels
{
    public class MainWindowViewModel : NotifyBase
    {
        public MainWindowViewModel()
        {
            UserToAdd = new User();
            DataAccess da = new DataAccess(new LiteDBAPI("PFMgrLiteDb.db"));

            AddUserCommand = new RelayCommand(AddUserCommandHandler);
        }

        private User _userToAdd = null!;
        public User UserToAdd 
        {
            get { return _userToAdd; } 
            set
            {
                _userToAdd = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public RelayCommand AddUserCommand { get; set; }
        private void AddUserCommandHandler()
        {
            DataAccess.DataBaseAPI.InsertDocument("Users", UserToAdd);
        }

        #endregion
    }
}
