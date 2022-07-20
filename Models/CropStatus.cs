using System;
using System.Collections.Generic;

namespace CropDealWebAPI.Models
{
    public partial class CropStatus
    {
        public int CropAdId { get; set; }
        public int DealerId { get; set; }
        public string CropStatus1 { get; set; } = null!;
        public int FarmerId { get; set; }

        public virtual CropOnSale CropAd { get; set; } = null!;
        public virtual UserProfile Dealer { get; set; } = null!;
        public virtual UserProfile Farmer { get; set; } = null!;
    }
}
