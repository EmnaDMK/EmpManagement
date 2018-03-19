using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpManagement.Model;
using EmpManagement.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmpManagement.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmpExpanView : ContentPage
	{
		public EmpExpanView ()
		{
			InitializeComponent ();
		}

	   
	    private void List_OnItemTapped(object sender, ItemTappedEventArgs e)

        {
            
	        var emp = e.Item as Employee;
            
	        var vm = BindingContext as EmployeeViewModel;
            
	        vm.ShoworHiddenEmployee(emp);

	    }
	    private void Detail_Clicked(object sender, EventArgs e)

	    {

	        var button = sender as Button;

	        var employee = button.BindingContext as Employee;

	        var vm = BindingContext as EmployeeViewModel;
            
	        Navigation.PushModalAsync(new DetailView());
            
	    }

        
        protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        var pv = this.BindingContext as EmployeeViewModel;
	        pv.GetData();
	        list.ItemsSource = pv.EmployeeList;

	    }

        //private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    {
        //        //thats all you need to make a search  
        //        var pv = this.BindingContext as EmployeeViewModel;
        //        if (string.IsNullOrEmpty(e.NewTextValue))
        //        {
        //            list.ItemsSource = pv.EmployeeList;

        //        }

        //        else
        //        {
        //            list.ItemsSource = pv.EmployeeList.Where(x => x.Name.ToLower().Contains(e.NewTextValue.ToLower()));
        //        }
        //    }
        //}
    }
}