namespace DataTransferLayer.DataTransfer.Request;

public class CreateAccountRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int Gender { get; set; }
    public string Password { get; set; }
    public int WalletBalance { get; set; }
    public int Role { get; set; }
    public string DriverLicense { get; set; }
}