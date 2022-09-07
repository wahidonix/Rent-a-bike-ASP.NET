namespace JWT
{
    public class History
    {
        public int Id { get; set; }
        public int Hours { get; set; }
        public DateTime DateTime { get; set; }
        public bool Returned { get; set; }

        private User User { get; set; }
        public int UserId { get; set; }

        private Bike Bike { get; set; }

        public int BikeId { get; set; }

    }
}
