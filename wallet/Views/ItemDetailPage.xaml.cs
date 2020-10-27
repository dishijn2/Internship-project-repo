using System.ComponentModel;
using Xamarin.Forms;
using wallet.ViewModels;

namespace wallet.Views
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