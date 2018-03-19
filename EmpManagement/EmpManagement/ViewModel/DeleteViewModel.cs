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
    class DeleteViewModel : BaseViewModel
    {
        Employee emp = new Employee();
    public DeleteViewModel()

    {


    }

 


    public DeleteViewModel(INavigation nav)

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
        public DeleteViewModel(object o, INavigation nav)

        {
            emp = (Employee)o;
            Id = emp.Id;

            Name = emp.Name;

            GSM = emp.GSM;

            _nav = nav;

            CurrentPage = DependencyInject<DetailViewRem>.Get();

            OpenPage();

        }


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

