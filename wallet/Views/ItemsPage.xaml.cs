using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using wallet.Models;
using wallet.Views;
using wallet.ViewModels;
using NBitcoin;
using Wallet.Models;

namespace wallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public Transaction Transaction { get; set; }

        public ItemsPage()
        {
            InitializeComponent();
            Transaction = new Transaction
            {
                PrivateKey = "3bbd640d9b2e8c9639771cd6c45d79d3",
                Sender = "1144a8424e41be20fa6c75d2b06ee8fc",
            };

            BindingContext = this;
        }

        async Task Save_Clicked(object sender, EventArgs e)
        {
            Credential.PublicKey = Transaction.Sender;
            Credential.PrivateKey = Transaction.PrivateKey;

            await DisplayAlert("Credential", "Keys Updated", "OK");
        }

        async void CreateSign_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }
      

    }

}