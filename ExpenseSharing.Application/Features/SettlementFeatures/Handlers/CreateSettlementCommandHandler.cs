using ExpenseSharing.Application.Common.Interfaces.Repository;
using ExpenseSharing.Application.Common.Interfaces.Service;
using ExpenseSharing.Application.DTO.Responses;
using ExpenseSharing.Application.Extensions;
using ExpenseSharing.Application.Interfaces.Repositories;
using ExpenseSharing.Domain.Entities;
using ExpenseSharing.Domain.Enums;
using ExpenseSharing.Domain.Exceptions;
using ExpenseSharing.Domain.Paging;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExpenseSharing.Application.Features.SettlementFeatures.Command
{
    public class CreateSettlementCommandHandler : IRequestHandler<CreateSettlementCommand, SettlementResponseModel>
    {
        private readonly ISettlementRepository _settlementRepository;
        private readonly IUserService _userService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository walletRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IExpenseRepository _expenseRepository;
        public CreateSettlementCommandHandler(ISettlementRepository settlementRepository, IUserService userService, ITransactionRepository transactionRepository, IWalletRepository walletRepository, IUnitOfWork unitOfWork, IExpenseRepository expenseRepository)
        {
            _settlementRepository = settlementRepository;
            _userService = userService;
            _transactionRepository = transactionRepository;
            this.walletRepository = walletRepository;
            this.unitOfWork = unitOfWork;
            _expenseRepository = expenseRepository;
        }

        public async Task<SettlementResponseModel> Handle(CreateSettlementCommand request, CancellationToken cancellationToken)
        {

            var loginUser = await _userService.GetLoggedInUserAsync();

            var wallet = await walletRepository.GetByUserIdAsync(loginUser.Id);
            if (wallet.Balance < request.Settlement.Amount) 
            {
                throw new InvalidParameterException("Wallet balance is insufficient for the requested settlement amount.");
            }

            var settlement = await _settlementRepository.GetAsync(x => x.UserId == loginUser.Id && x.ExpenseId == request.ExpenseId)
                ?? throw new InvalidParameterException("Settlement not found.");
          
            var amountToPay = Math.Min(request.Settlement.Amount, settlement.Amount - settlement.AmountPaid);
            if (amountToPay <= 0)
            {
                throw new InvalidParameterException("This settlement is already completed.");
            }
            
            settlement.AmountPaid += amountToPay;

            wallet.Balance -= amountToPay;

            await walletRepository.UpdateAsync(wallet);
            
            
            //check if all payment is settled and update the expense settled status 
            if (settlement.AmountPaid == settlement.Amount)
            {
                var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId)
                    ?? throw new InvalidParameterException("Expense not found.");

                var expenseSettlements = await _settlementRepository.GetAllAsync(x => x.ExpenseId == request.ExpenseId, null, false);

                if (expenseSettlements.Items.All(x => x.AmountPaid == x.Amount))
                {
                    expense.IsSettled = true;
                    await _expenseRepository.UpdateAsync(expense);
                }
              
            }
            

            var transnastaion = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = amountToPay,
                CreatedAt = DateTime.UtcNow,
                Type = TransactionType.Debit,
                Narration = "Settlement transaction",
                WalletId = wallet.Id,
                ExpenseId = settlement.ExpenseId,
            };


            await _transactionRepository.AddAsync(transnastaion);
            _settlementRepository.Update(settlement);
            await unitOfWork.SaveAsync();

         

            // Map to response model
            return settlement.ToSettlementResponse();
        }
    }
}
