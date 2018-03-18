using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using EmpManagement.Model;
using Xamarin.Forms;

namespace EmpManagement.ViewModel
{
  public  class LoginViewModel: BaseViewModel
    {
       

        public Action DisplayPrompt;
    public ObservableCollection<User> users { get; set; }
    public string Msg;
    public User user;



    private String _login;
    private String _password;


    public String Login
    {
        get { return _login; }
        set
        {
            _login = value;
            SetProperty(ref _login, value);
        }
    }



    public String Password
    {
        get { return _password; }
        set
        {
            _password = value;
            SetProperty(ref _password, value);
        }
    }



    public LoginViewModel(INavigation nav)
    {
        _nav = nav;
        CurrentPage = DependencyInject<MainPage>.Get();
        OpenPage();
    }

    public LoginViewModel()
    {

    }

  
    #region MyRegion

    async void SignIn()

    {

        if (IsBusy)

            return;

        IsBusy = true;



        try

        {

            users = new ObservableCollection<User>();

            users.Clear();

            var us = await DataStoreUser.GetAllAsync();

            foreach (var item in us)

            {

                users.Add(item);

            }

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


    #endregion

    //connect
    public ICommand SubmitCommand => new Command(async () =>

    {
        try
        {


            users = new ObservableCollection<User>();

            users.Clear();

            var us = await DataStoreUser.GetAllAsync(x => x.Login.Equals(Login) && x.Password.Equals(Password));

            // u.ToList());

            if (us.Count() > 0)

            {

                var ss = DependencyService.Get<EmployeeViewModel>() ?? (new EmployeeViewModel(_nav));

            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    });





    public ICommand RegistrationCommand => new Command(async () =>

    {

        user = new User

        {

            Login = Login,

            Password = Password,

        };

        int res = await DataStoreUser.AddAsync(user);

        Console.WriteLine("Add user = " + res);

        if (res == 1)

            await Application.Current.MainPage.Navigation.PopAsync();

    });
}
}
