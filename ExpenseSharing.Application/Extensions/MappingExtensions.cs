using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Paging;

namespace ExpenseSharing.Application.Extensions;

public static class MappingExtensions
{
    public static PaginatedList<T> ToPaginated<T, TB>(this PaginatedList<TB> list, IEnumerable<T> items)
        where T : notnull where TB : notnull
    {
        return new PaginatedList<T>()
        {
            Items = items,
            Page = list.Page,
            PageSize = list.PageSize,
            TotalItems = list.TotalItems
        };
    }

    public static SettlementResponseModel ToSettlementResponse(this Settlement settlement)
    {
        return new SettlementResponseModel()
        {
            ExpenseId = settlement.ExpenseId,
            GroupId = settlement.Expense.GroupId,
            Expense = settlement.Expense.Description,
            Amount = settlement.Amount,
            AmountPaid = settlement.AmountPaid
        };
    }

    public static GroupResponseModel ToGroupResponse(this Group group)
    {
        return new GroupResponseModel()
        {
            Id = group.Id,
            Name = group.Name,
            Members = group.Members?.Select(member => new UserResponseModel
            {
                 FirstName = member.User.FirstName,
                 LastName = member.User.LastName,
                 Email = member.User.Email
            }).ToList()

        };
    }

    public static ExpenseResponseModel ToExpenseResponse(this Expense expense)
    {
        return new ExpenseResponseModel()
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Date = expense.CreatedAt,
            Description = expense.Description
        };
    }
}