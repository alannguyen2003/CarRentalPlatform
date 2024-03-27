namespace DataTransferLayer.DataTransfer;

public class BookingRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? ActualReturnDate { get; set; }
    public string Note { get; set; }
    public int DepositAmount { get; set; }
    public bool IsSigned { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    
    
}