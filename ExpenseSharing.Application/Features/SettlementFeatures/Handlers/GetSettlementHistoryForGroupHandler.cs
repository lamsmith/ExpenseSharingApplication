using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Features.SettlementFeatures.Queries;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Exceptions;
using MediatR;

namespace ExpenseSharing.Application.Features.TransactionFeatures.Handlers
{
    public class GetSettlementHistoryForGroupHandler : IRequestHandler<GetSettlementHistoryForGroupQuery, List<Transaction>>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IGroupRepository _groupRepository;

        public GetSettlementHistoryForGroupHandler(ITransactionRepository transactionRepository, IGroupRepository groupRepository)
        {
            _transactionRepository = transactionRepository;
            _groupRepository = groupRepository;
        }

        public async Task<List<Transaction>> Handle(GetSettlementHistoryForGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);
            if (group == null)
                throw new GroupNotFoundException();

            var histories = new List<Transaction>();

            foreach (var expense in group.Expenses)
            {
                var history = await _transactionRepository.GetSettlementHistoryByExpenseIdAsync(expense.Id);
                histories.AddRange(history);
            }

            return histories;

        }
    }
}
