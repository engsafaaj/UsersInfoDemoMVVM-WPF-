using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UsersInfo.Core;
using UsersInfo.Data;
using UsersInfo.Data.SqlOperations;

namespace UsersInfo.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {

        #region Fields
        private IDataHelper<User> _dataHelper;
        private User user;
        private User selectedUser;
        private ObservableCollection<User> users;
        #endregion

        #region Properties
        public string FullName 
        {
            get
            {
                return user.FullName;
            }
            set
            {
                if(user.FullName != value)
                {
                    user.FullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public string Email
        {
            get
            {
                return user.Email;
            }
            set
            {
                if (user.Email != value)
                {
                    user.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                return user.PhoneNumber;
            }
            set
            {
                if (user.PhoneNumber != value)
                {
                    user.PhoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        public ObservableCollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        #endregion

        public ICommand AddCommand { get;}
        public ICommand EditCommand { get;}
        public ICommand DeleteCommand { get;}
        public ICommand SaveCommand { get;}

        public UserViewModel()
        {
            user = new User();
            users = new ObservableCollection<User>();
            _dataHelper = new UserEntity();

            // Load Data
            LoadData();
            AddCommand = new RelayCommand(AddData);
            EditCommand = new RelayCommand(EditData);
            SaveCommand = new RelayCommand(SaveData);
            DeleteCommand = new RelayCommand(DeleteData);
        }

        private async void SaveData()
        {
            // Check User Add or Edit
           
            if (SelectedUser != null)
            {
                // Edit
                user = new User
                {
                    FullName = FullName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Id=SelectedUser.Id,
                };
                await _dataHelper.EditAsync(user);
            }
            else
            {
                // Add
                user = new User
                {
                    FullName=FullName,
                    Email=Email,
                    PhoneNumber=PhoneNumber,

                };

               await _dataHelper.AddAsync(user);
                ClearData();
            }
            LoadData();
        }

        private void AddData()
        {
            // Clear Data
            ClearData();
            // Open Add User
            Views.AddUser addUser = new Views.AddUser(this);
            addUser.Show();
        }
        private void EditData()
        {
            if(SelectedUser!=null)
            {
                SetData();
                // Open Add User
                Views.AddUser addUser = new Views.AddUser(this);
                addUser.Show();
            }
            else
            {
                MessageBox.Show("اختر مستخدم لتعديلة");
            }
            
        }
        private async void DeleteData()
        {
            if (SelectedUser != null)
            {
                await _dataHelper.RemoveAsync(SelectedUser.Id);
                LoadData();
            }
            else
            {
                MessageBox.Show("اختر مستخدم لحذفه");
            }

        }
        private void SetData()
        {
            FullName = selectedUser.FullName;
            PhoneNumber= selectedUser.PhoneNumber;
            Email= selectedUser.Email; 
        }

        private void ClearData()
        {
            FullName=string.Empty;
            PhoneNumber=string.Empty;
            Email=string.Empty;
        }

        private async void LoadData()
        {
            users.Clear();
            foreach (User user in await _dataHelper.GetAllAsync())
            {
                users.Add(user);
            }
        }



        // PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
