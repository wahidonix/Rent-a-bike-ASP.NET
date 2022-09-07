namespace JWT
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public int Hours { get; set; }
        public DateTime DateTime { get; set; }
        public bool Returned { get; set; }
        public int UserId { get; set; }
        public int BikeId { get; set; }
    }
}
