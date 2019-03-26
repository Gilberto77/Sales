namespace Sales.Infrastructure
{
    using Sales.ViewModels;

    public class InstanceLocator    // tiene como objetivo instanicar la MainViewModel
    {
        public MainViewModel Main { get; set; }  //  MainPage es el objeto principal que todas las paginas van a bindar = unirse


        public InstanceLocator()      // instancia la MainViewModel
        {
            this.Main = new MainViewModel();
        }
    }
}
