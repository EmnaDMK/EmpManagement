using Root.Services.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using EmpManagement.Model;
using Xamarin.Forms;

namespace EmpManagement.ViewModel
{
  public  class BaseViewModel : INotifyPropertyChanged

    {

        public IDataStore<Employee> DataStoreEmp => DependencyService.Get<IDataStore<Employee>>() ?? new DataStore<Employee>("DataBase.db3");

        public IDataStore<User> DataStoreUser => DependencyService.Get<IDataStore<User>>() ?? new DataStore<User>("DataBase.db3");

        public INavigation _nav;

        public ContentPage CurrentPage { get; set; }



        public BaseViewModel()

        {

            DataStoreEmp.CreateTableAsync();

            DataStoreUser.CreateTableAsync();

        }

        public void OpenPage()

        {



            if (CurrentPage != null)

            {

                CurrentPage.BindingContext = this;

                _nav.PushAsync(CurrentPage);

            }

        }

        bool isBusy = false;

        public bool IsBusy

        {

            get { return isBusy; }

            set { SetProperty(ref isBusy, value); }

        }



        string title = string.Empty;

        public string Title

        {

            get { return title; }

            set { SetProperty(ref title, value); }

        }



        protected bool SetProperty<T>(ref T backingStore, T value,

            [CallerMemberName]string propertyName = "",

            Action onChanged = null)

        {

            if (EqualityComparer<T>.Default.Equals(backingStore, value))

                return false;



            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);

            return true;

        }



        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")

        {

            var changed = PropertyChanged;

            if (changed == null)

                return;



            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion




        public string name;

        public string Name

        {

            get { return name; }

            set

            {

                name = value;

                OnPropertyChanged();

            }

        }

        public string Gsm;

        public string GSM

        {

            get { return Gsm; }

            set

            {

                Gsm = value;

                OnPropertyChanged();

            }

        }

        public ObservableCollection<Employee> Employees;

        public ObservableCollection<Employee> EmployeeList

        {

            get

            {

                return Employees ?? (Employees = new ObservableCollection<Employee>());

            }

            set

            {

                Employees = value;

                OnPropertyChanged("EmployeeList");

            }

        }


    }

}