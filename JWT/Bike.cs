namespace JWT
{
    public class Bike
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }
        public bool Repair { get; set; }
        private Station Station { get; set; }
        public int StationId { get; set; }
    }
}
