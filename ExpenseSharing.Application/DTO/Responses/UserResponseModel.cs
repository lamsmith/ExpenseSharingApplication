namespace ExpenseSharing.Application.DTO.Responses;

public class UserResponseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<GroupResponseModel> Groups { get; set; }
}