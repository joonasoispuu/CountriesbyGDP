using System;
using System.Collections.Generic;

#nullable disable

namespace CountriesbyGDP.Models
{
    public partial class CountriesGdp
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public int? EstimatebyDollar { get; set; }
        public int? Population { get; set; }
    }
}
