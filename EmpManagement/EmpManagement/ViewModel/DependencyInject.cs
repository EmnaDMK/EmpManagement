using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EmpManagement.ViewModel
{
  public  class DependencyInject<T> where T : class

    {

        public static T Get()

        {

            return DependencyService.Get<T>() ?? (T)Activator.CreateInstance(typeof(T), new object[] { });

        }



    }
}
