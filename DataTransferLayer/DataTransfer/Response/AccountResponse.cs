namespace DataTransferLayer.DataTransfer.Response;

public class AccountResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public int WalletBalance { get; set; }
    public string Role { get; set; }
    public string DriverLicense { get; set; }
}