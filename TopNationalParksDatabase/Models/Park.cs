namespace TopNationalParksDatabase.Models
{
    public class Park
    {
        public Park() 
        { 
        }
        public int ParkID { get ; set; }      
        public string FullName { get; set; }

        public string ParkCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Website { get; set; }
        
    }
}
