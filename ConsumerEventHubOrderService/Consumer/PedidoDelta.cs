using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer
{
    public class PedidoDelta
    {
        public string accountId { get; set; }
        public DateTime placementDate { get; set; }
        public string orderNumber { get; set; }
        public string deliveryCenter { get; set; }
    }
}
