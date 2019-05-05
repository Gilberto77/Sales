namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Helpers;
    using System.Windows.Input;
    using Xamarin.Forms;
    
    public class PerfilViewModel : BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        private bool isEnabled;

        #endregion
        #region Properties
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool IsRunning
        {
            // debuelve el atributo privado products
            get { return this.isRunning; }

            // Este metodo se encuentra en la BaseViewModel es el que hace que se refresque
            set { this.SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            // debuelve el atributo privado products
            get { return this.isEnabled; }

            // Este metodo se encuentra en la BaseViewModel es el que hace que se refresque
            set { this.SetValue(ref this.isEnabled, value); }
        }

        #endregion
        #region Constructors
        public PerfilViewModel()
        {
            this.IsEnabled = true;
        }

        #endregion
        #region Commands
        public ICommand ModifyCommand
        {
            get
            {
                return new RelayCommand(Modify);
            }
        }

        private async void Modify()
        {
            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.TelephoneError, 
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Direccion))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DireccionError,
                    Languages.Accept);
                return;
            }
            var telefono = decimal.Parse(this.Telefono);
            if (telefono < 7)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.TelephoneError,
                    Languages.Accept);
                return;
            }
        }


        #endregion
    }
}
