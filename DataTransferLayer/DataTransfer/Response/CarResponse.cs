namespace DataTransferLayer.DataTransfer.Response;

public class CarResponse
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public string ThumbnailImage { get; set; }
    public int PricePerDay { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public string Location { get; set; }
}