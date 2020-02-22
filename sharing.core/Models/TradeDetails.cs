﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sharing.Core.Models
{
    public class TradeDetails
    {
        public string TradeId { get; set; }
        public int Code { get; set; }
        public long CreatedDateTime { get; set; }
        public TradeStates TradeState { get; set; }
        public string Attach { get; set; }
    }
}
