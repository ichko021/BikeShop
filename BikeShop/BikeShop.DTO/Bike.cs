namespace BikeShop.DTO
{
    public class Bike
    {
        public int id { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public double price { get; set; }
        public int availabilityInStore { get; set; }
        public Parts bikeParts { get; set; }
    }
}
