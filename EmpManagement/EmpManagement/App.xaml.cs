using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmpManagement.View;
using EmpManagement.ViewModel;
using Xamarin.Forms;

namespace EmpManagement
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //MainPage = new EmpManagement.MainPage();



            var model = DependencyInject<LoginViewModel>.Get();

            model.CurrentPage = DependencyInject<LoginView>.Get();

            model.CurrentPage.BindingContext = model;

            var nav = new NavigationPage(model.CurrentPage);

            model._nav = nav.Navigation;

            MainPage = nav;



            //var model = DependencyInject<UpdateViewModel>.Get();

            //model.CurrentPage = DependencyInject<DetailView>.Get();

            //model.CurrentPage.BindingContext = model;

            //var nav = new NavigationPage(model.CurrentPage);

            //model._nav = nav.Navigation;

            //MainPage = nav;
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
