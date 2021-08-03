using ClientApp.Commands;
using ClientApp.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly API.ProductsService _productsService;
        public MainViewModel()
        {
            _productsService = new API.ProductsService("https://localhost:44315/", new System.Net.Http.HttpClient(new HttpClientHandler { UseDefaultCredentials = true }));
            GetAllProductsCommand = new RelayCommand(GetProducts);
            CreateNewProductCommand = new RelayCommand(CreateNewProduct);

            Products = new ObservableCollection<API.IProduct>();
        }

        private async void CreateNewProduct(object obj)
        {
            var item = new API.Product();
            item.DisplayName = "Newly Created Product";
            item.Price = 99;
            await _productsService.ProductsAsync(item);
            Products.Add(item.FromProductToIProduct());
        }

        private async void GetProducts(object obj)
        {
            var items = await _productsService.ProductsAllAsync();
            foreach (var item in items)
            {
                Products.Add(item);
            }
        }

        public IRelayCommand CreateNewProductCommand { get; set; }

        public IRelayCommand GetAllProductsCommand { get; set; }

        private ObservableCollection<ClientApp.API.IProduct> products;

        public ObservableCollection<ClientApp.API.IProduct> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged(); }
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
