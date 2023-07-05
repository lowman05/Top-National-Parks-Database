namespace TopNationalParksDatabase.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public int ParkID { get; set; }
        public string Reviewer { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }


        public string ParkName { get; set; }

       public IEnumerable<Park> Parks { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        


    }
}
