using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EmpManagement.Model;
using EmpManagement.View;
using Root.Services.Sqlite;
using Xamarin.Forms;

namespace EmpManagement.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {



        #region Fields

        //public INavigation navigation;
      
        public Command<Object> _tapCommand;
        public Command<Object> _tapCommandR;
        public Command<Object> _tapSearch;
        private Employee Old_Employee;
        public ICommand LoadItemsCommand { get; set; }

        public void ShoworHiddenEmployee(Employee employee)
        {
            if (Old_Employee == employee)
            {
                employee.IsVisible = !employee.IsVisible;

                UpdateEmp(employee);
            }
            else
            {
                if (Old_Employee != null)
                {
                    Old_Employee.IsVisible = false;

                    UpdateEmp(Old_Employee);
                }
                employee.IsVisible = !employee.IsVisible;

                UpdateEmp(employee);
            }

            Old_Employee = employee;
        }

        private void UpdateEmp(Employee employee)
        {
            var index = Employees.IndexOf(employee);
            Employees.Remove(employee);
            Employees.Insert(index, employee);
        }


        #endregion



        #region Properties

        public Command<Object> TapCommand

        {

            get { return _tapCommand; }

            set { _tapCommand = value; }

        }

        
        public Command<Object> TapCommandR

        {

            get { return _tapCommandR; }

            set { _tapCommandR = value; }

        }

     

     

        #endregion



        #region Constructor with parameters



        public EmployeeViewModel(INavigation nav)

        {

            ExecuteLoadEmployeeCommand();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            _nav = nav;
       
            _tapCommand = new Command<Object>(OnTapped);
            _tapCommandR = new Command<Object>(OnRemove);
         


            CurrentPage = DependencyInject<View.EmpExpanView>.Get();
         
            OpenPage();


        }




        public async Task ExecuteLoadItemsCommand()

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

        public async void ExecuteLoadEmployeeCommand()

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
           

            EmployeeList = ctv;
            CurrentPage = DependencyInject<View.EmpExpanView>.Get();

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

            var nextPage = new DetailView();

            nextPage.BindingContext = o;

            //_nav.PushAsync(nextPage);

            //emp=new Employee();

            var page = DependencyService.Get<ViewModel.DeleteViewModel>() ?? new DeleteViewModel(o, _nav);


        }
  


        public Command<Employee> OnAddCommand

        {

            get
            {
                return new Command<Employee>((emp) =>

                {

                    var page = DependencyService.Get<ViewModel.AddNewItem>() ?? new AddNewItem(_nav);


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
        
        public string searchvalue;

        public string SearchValue

        {

            get { return searchvalue; }

            set

            {
                searchvalue = value;

                OnPropertyChanged();
         

                IEnumerable<Employee> searchresult = EmployeeList.Where(x => x.Name.ToLower().Contains(SearchValue.ToLower()));

                var l = CurrentPage.FindByName<ListView>("list");
                l.ItemsSource = searchresult;
            }

        }


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
