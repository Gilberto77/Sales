﻿namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Helpers;
    using Sales.Views;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public EditProductViewModel EditProduct { get; set; }
        public ProductsViewModel Products { get; set; }
        public MyUserASP UserASP { get; set; }
        public PerfilViewModel Perfil { get; set; }
        public AddProductViewModel AddProduct { get; set; }
        public RegisterViewModel Register { get; set; }
     
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public string UserImageFullPath
        {
            get
            {
                foreach (var claim in this.UserASP.Claims)
                {
                    if (claim.ClaimType == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri")
                    {
                        if (claim.ClaimValue.StartsWith("~"))
                        {
                            return $"https://salesapi20190430115441.azurewebsites.net{claim.ClaimValue.Substring(1)}";
                        }
                        return claim.ClaimValue;
                    }
                }
                return null;
            }
        }

        public string UserFullName
        {
            get
            {
               
            if (this.UserASP != null && this.UserASP.Claims != null && this.UserASP.Claims.Count > 1)
                {
                    return $"{this.UserASP.Claims[0].ClaimValue} {this.UserASP.Claims[1].ClaimValue}";
                }
                return null;
            }
        }
        #endregion

        // Products es una instancia de la ProductsViewModel
        #region Constructors
        public MainViewModel()
        {
            this.Perfil = new PerfilViewModel();
            this.Categories = new CategoriesViewModel();
            instance = this;
            this.LoadMenu();
            
        }

        #endregion

        // Methods
        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_shortcut_info",
                PageName = "AboutPage",
                Title = Languages.About,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_shortcut_phonelink_setup",
                PageName = "SetupPage",
                Title = Languages.Setup,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_shortcut_exit_to_app",
                PageName = "LoginPage",
                Title = Languages.Exit,
            });
        }





        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion




        #region Commands
        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);
            }
        }

        private async void GoToAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await App.Navigator.PushAsync(new AddProductPage());
        } 
        #endregion
    }
}
