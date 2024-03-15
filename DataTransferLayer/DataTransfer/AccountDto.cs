namespace DataTransferLayer.DataTransfer;

public class AccountDto
{
    public int Id { get; set; } = 0;
    public string? Email { get; set; }
    public string? Name { get; set; }
    //1 --> Admin
    //2 --> Employee
    //3 --> Customer
    public int Role { get; set; }
    
}