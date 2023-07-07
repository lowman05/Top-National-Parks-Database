//using Org.BouncyCastle.Crypto.Tls;
using System.Collections.Generic;

namespace TopNationalParksDatabase.Models
{
    public class ParkViewModel
    {
        public Park Park { get; set; }
        public int? PreviousParkId { get; set; }
        public int? NextParkId { get; set; }

        public List<Park> Alerts { get; set; }

    }
}
