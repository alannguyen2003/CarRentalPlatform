namespace DataTransferLayer.DataTransfer;

public class CarDto
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public string ThumbnailImage { get; set; }
    public int PricePerDay { get; set; }
    public int PricePerHour { get; set; }
    public int PricePerMonth { get; set; }
    public string Description { get; set; } = null!;
    public string Brand { get; set; }
    public string Location { get; set; }
    
}