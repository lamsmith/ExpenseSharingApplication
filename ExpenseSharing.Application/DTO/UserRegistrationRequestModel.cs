using System.ComponentModel.DataAnnotations;

public class UserRegistrationRequestModel
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public decimal WalletAmount { get; set; }
    

}