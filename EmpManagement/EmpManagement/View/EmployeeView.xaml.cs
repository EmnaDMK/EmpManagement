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
	public partial class EmployeeView : ContentPage
	{
		public EmployeeView ()
		{
			InitializeComponent ();


           
		}

	     
	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        var pv = this.BindingContext as EmployeeViewModel;
            pv.GetData();
	        list.ItemsSource = pv.EmployeeList;

	    }

    }



    
}
