namespace ExpenseSharing.Application.DTO.Responses;

public class GroupResponseModel
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public List<UserResponseModel> Members { get; set; }
}