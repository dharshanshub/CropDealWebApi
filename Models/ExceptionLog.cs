using System;
using System.Collections.Generic;

namespace CropDealWebAPI.Models
{
    public partial class ExceptionLog
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? ErrorDescription { get; set; }
        public string? Data { get; set; }
        public string? StackTrace { get; set; }
    }
}
