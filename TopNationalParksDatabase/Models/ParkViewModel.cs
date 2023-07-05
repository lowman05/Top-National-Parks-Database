namespace TopNationalParksDatabase.Models
{
    public class ParkViewModel
    {
        public Park Park { get; set; }
        public int? PreviousParkId { get; set; }
        public int? NextParkId { get; set; }

    }
}
