using NBitcoin;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Wallet.Models
{
    public static class RSA //method to generate signature
    {
        public static string Sign(string privKey, string msgToSign)
        {
            var secret = Network.Main.CreateBitcoinSecret(privKey);
            var signature = secret.PrivateKey.SignMessage(msgToSign);
            //var bol = pkh.VerifyMessage(msgToSign, signature));
            var v = secret.PubKey.VerifyMessage(msgToSign, signature);
            return signature;
        }

    }
}
