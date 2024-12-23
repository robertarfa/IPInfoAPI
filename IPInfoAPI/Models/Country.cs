﻿using System.Net;

namespace IPInfoAPI.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<IPAddress> IPAddresses { get; set; }
    }
}
