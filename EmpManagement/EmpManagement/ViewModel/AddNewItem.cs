using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using EmpManagement.Model;
using EmpManagement.View;
using Xamarin.Forms;

namespace EmpManagement.ViewModel
{
  public  class AddNewItem : BaseViewModel
    {



        public int nnn;
        public Employee emp;


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

     
        public AddNewItem(INavigation nav)

        {
            _nav = nav;

            CurrentPage = DependencyInject<AddView>.Get();

            OpenPage();
        }



        public AddNewItem()

        {

        }

        public ICommand SaveCommand => new Command(async () =>

        {
            try
            {


                Employee emp = new Employee

                {
                    Name = name,
                    GSM = Gsm,

                };

                int res = await DataStoreEmp.AddAsync(emp);

                EmployeeList.Add(emp);


                Console.Write("result add =  " + res);

                await _nav.PopAsync();

                //var page = DependencyService.Get<ViewModel.EmployeeViewModel>() ?? new EmployeeViewModel(_nav);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        });



    }

}
