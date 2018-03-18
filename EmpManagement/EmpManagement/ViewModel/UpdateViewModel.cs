using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EmpManagement.Model;
using EmpManagement.View;
using Xamarin.Forms;

namespace EmpManagement.ViewModel
{
 public   class UpdateViewModel : BaseViewModel
    {
        Employee emp = new Employee();
        public UpdateViewModel()

        {


        }
       
        public UpdateViewModel(object o, INavigation nav)

        {
            emp = (Employee) o;
            Id = emp.Id;

            Name= emp.Name ;

              GSM= emp.GSM;

            _nav = nav;

            CurrentPage = DependencyInject<DetailView>.Get();

            OpenPage();

        }


        public UpdateViewModel(INavigation nav)

        {
            
            _nav = nav;

            CurrentPage = DependencyInject<DetailView>.Get();

            OpenPage();

        }


        public int id;

        public int Id

        {

            get { return id; }

            set

            {

                id = value;

                OnPropertyChanged();

            }

        }

        public ICommand SaveCommandUp => new Command(async () =>

        {

            

                var emp = (await DataStoreEmp.GetAllAsync(e => e.Id == Id)).ToList().First();

                emp.Name = Name;

                emp.GSM = GSM;
            try
            {
               await DataStoreEmp.UpdateAsync(emp);

                await _nav.PopAsync();

                //var page = DependencyService.Get<ViewModel.EmployeeViewModel>() ??
                //           new EmployeeViewModel(_nav);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        });



        public ICommand RemoveUp => new Command(async () =>

        {



            var emp = (await DataStoreEmp.GetAllAsync(e => e.Id == Id)).ToList().First();

            try
            {
                await DataStoreEmp.DeleteAsync(emp);

                await _nav.PopAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        });
    }
}

