using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using wallet.Models;
using wallet.ViewModels;
using NBitcoin;
using Wallet.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Block = Wallet.Models.Block;
using System.Linq;
using Transaction = Wallet.Models.Transaction;

namespace wallet.Views
{
    public partial class NewItemPage : ContentPage
    {

        public NewItemPage()
        {
            InitializeComponent();
            var blocks = GetChain();
            var trns = TransactionByAddress(Credential.PublicKey, blocks);
            decimal balance = 0;
            decimal receives = 0;
            decimal deduct = 0;
            List<string> lstStr = new List<string>();
            foreach (var item in trns)
            {
                if (item.Recipient == Credential.PublicKey)
                {
                    balance = balance + item.Amount;
                    receives = receives + item.Amount;
                }
                else
                {
                    balance = balance - item.Amount;
                    deduct = deduct + item.Amount;
                }
                lstStr.Add(item.Sender + " sent " + item.Amount + " to " + item.Recipient);
            }
            txtReceives.Text = receives.ToString();
            txtDeduct.Text = deduct.ToString();
            txtBalance.Text = balance.ToString();
            lst.ItemsSource = lstStr;
        }

        private List<Block> GetChain()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var url = new Uri("http://192.160.1.198:5000" + "/api/chain");
                var response = client.GetAsync(url).Result;

                var content = response.Content.ReadAsStringAsync().Result;
                var model = new
                {
                    chain = new List<Block>(),
                    length = 0
                };
                var data = JsonConvert.DeserializeAnonymousType(content, model);

                return data.chain;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Transaction> TransactionByAddress(string ownerAddress, List<Block> chain)
        {
            List<Transaction> trns = new List<Transaction>();
            foreach (var block in chain.OrderByDescending(x => x.Sender == ownerAddress || x.Recipient == ownerAddress))
            {
                var ownerTransactions = block.Transactions.Where(x => x.Sender == ownerAddress || x.Recipient == ownerAddress);
                trns.AddRange(ownerTransactions);
            }
            return trns;
        }
    }
}