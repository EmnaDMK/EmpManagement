using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmpManagement.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailView : ContentPage
	{
		public DetailView ()
		{
			InitializeComponent ();
		}

         //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    EmployeeViewModel pv = new EmployeeViewModel();

        //    list.ItemsSource = pv.Employees;


        //}
	}
}