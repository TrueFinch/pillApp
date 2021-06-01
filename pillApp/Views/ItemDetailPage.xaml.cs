using System.ComponentModel;
using pillApp.ViewModels;
using Xamarin.Forms;

namespace pillApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}