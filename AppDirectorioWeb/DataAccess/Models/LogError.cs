using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class LogError
    {
        public long Id { get; set; }
        public string MessageError { get; set; }
        public string Observation { get; set; }
        public DateTime? Date { get; set; }
        public bool? Status { get; set; }
    }
}
