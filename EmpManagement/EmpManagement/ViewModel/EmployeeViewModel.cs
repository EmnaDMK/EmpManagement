using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EmpManagement.Model;
using EmpManagement.View;
using Xamarin.Forms;

namespace EmpManagement.ViewModel
{
  public  class EmployeeViewModel : BaseViewModel
    {



        #region Fields

        //public INavigation navigation;

        public Command<Object> _tapCommand;
        public Command<Object> _RemoveCommand;
        public ICommand LoadItemsCommand { get; set; }


        #endregion



        #region Properties

        public Command<Object> TapCommand

        {

            get { return _tapCommand; }

            set { _tapCommand = value; }

        }

        public Command<Object> RemoveCommand

        {

            get { return _RemoveCommand; }

            set { _RemoveCommand = value; }

        }
        
        #endregion

        private bool _isRefreshing = false;

        public bool IsRefreshing

        {

            get { return _isRefreshing; }

            set

            {

                _isRefreshing = value;

                OnPropertyChanged(nameof(IsRefreshing));

            }

        }

        #region Constructor with parameters



        public EmployeeViewModel(INavigation nav)

        {

            ExecuteLoadEmployeeCommand();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            _nav = nav;

            _tapCommand = new Command<Object>(OnTapped);

            _RemoveCommand = new Command<Object>(OnRemove);
            CurrentPage = DependencyInject<View.EmployeeView>.Get();
            OpenPage();


        }




     public   async Task ExecuteLoadItemsCommand()

        {
            try

            {
                EmployeeList.Clear();


                var employees = await DataStoreEmp.GetAllAsync();

                foreach (var item in employees)

                {

                    EmployeeList.Add(item);

                }

                IsRefreshing = true;

                int nb = EmployeeList.Count;

                Console.WriteLine("Employee Number=  " + nb);

            }

            catch (Exception ex)

            {

                Debug.WriteLine(ex);

            }

            finally

            {

                IsBusy = false;

            }

        }


        public async void GetData()
        {
            EmployeeList.Clear();
            var emplist = await DataStoreEmp.GetAllAsync();
            foreach (Employee e in emplist)
            {
                EmployeeList.Add(e);
            }
        }
        public  async void ExecuteLoadEmployeeCommand()

        {

            try

            {

                EmployeeList.Clear();

                var employees = await DataStoreEmp.GetAllAsync();

                foreach (var item in employees)

                {

                    EmployeeList.Add(item);

                }

                int nb = EmployeeList.Count;

            }

            catch (Exception ex)

            {

                Debug.WriteLine(ex);

            }


        }

        public EmployeeViewModel(INavigation nav, ObservableCollection<Employee> ctv)

        {

            ExecuteLoadEmployeeCommand();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _nav = nav;

            _tapCommand = new Command<Object>(OnTapped);
            _RemoveCommand = new Command<Object>(OnRemove);
            EmployeeList = ctv;
            CurrentPage = DependencyInject<View.EmployeeView>.Get();

            OpenPage();

        }


        #endregion

        public EmployeeViewModel(Employee emp)
        {

        }

        public EmployeeViewModel()
        {
        }

        public void OnTapped(Object o)

        {

            var nextPage = new DetailView();

            nextPage.BindingContext = o;

            //_nav.PushAsync(nextPage);

            //emp=new Employee();

            var page = DependencyService.Get<ViewModel.UpdateViewModel>() ?? new UpdateViewModel(o, _nav);


        }

        public void OnRemove(Object o)
        {
            if (o != null)
            {
                var employee = o as Employee;
                EmployeeList.Remove(employee);
                DataStoreEmp.DeleteAsync(emp);
            }
        }

        public Command<Employee> OnAddCommand

        {

            get
            {
                return new Command<Employee>((emp) =>

                {
                    
                    var page = DependencyService.Get<ViewModel.AddNewItem>() ?? new AddNewItem(_nav );
                    

                });
            }
        }


        //public ICommand OnAddCommand => new Command(() =>
        //{

        //    var page = DependencyService.Get<ViewModel.AddNewItem>() ?? new AddNewItem(_nav);

        //});


        public Employee emp;

        public ICommand OnUpCommand => new Command(() =>
        {

            var page = DependencyService.Get<ViewModel.UpdateViewModel>() ?? new UpdateViewModel(emp, _nav);

        });


        //public ICommand OnUpCommand => new Command(async () =>
        //{
        //var page = DependencyService.Get<ViewModel.UpdateViewModel>() ?? new UpdateViewModel(_nav);

        //});


        //public Command<Employee> OnUpCommand

        //{

        //    get
        //    {
        //        return new Command<Employee>((emp) =>

        //        {

        //            var page = DependencyService.Get<ViewModel.UpdateViewModel>() ?? new UpdateViewModel(emp,_nav);


        //        });
        //    }
        //}


    }
}
