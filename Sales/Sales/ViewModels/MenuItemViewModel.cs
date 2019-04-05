﻿using GalaSoft.MvvmLight.Command;
using Sales.Helpers;
using Sales.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class MenuItemViewModel
    {
        // Propiedades
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }

        // Comandos
        public ICommand GotoCommand
        {
            get
            {
                return new RelayCommand(GoTo);
            }
            
        }

        private async void GoTo()
        {
            if (this.PageName == "LoginPage")
            {
                Settings.AccessToken = string.Empty;
                Settings.TokenType = string.Empty;
                Settings.IsRemembered = false;
                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (this.PageName == "About Page")
            {
                App.Master.IsPresented = false;
                await App.Navigator.PushAsync(new MapPage());
                    }
        }
    }
}
