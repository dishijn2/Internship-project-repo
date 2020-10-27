using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Wallet.Models
{
    public class Block
    {
        public int Index { get; set; }
        
        public DateTime Timestamp { get; set; }

        public List<Transaction> Transactions { get; set; }
        public int Proof { get; set; }
        public string Sender { get; internal set; }
        public string Recipient { get; internal set; }

        public override string ToString()
        {
            return base.ToString();

        }
    }
}
