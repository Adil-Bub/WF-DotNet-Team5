using backend.Models;
using backend.Models.Request;
using backend.Models.Response;
using backend.Repository.Interfaces;
using backend.Services.Interfaces;
using backend.Util;

namespace backend.Services
{
    public class LoanCardService : ILoanCardService
    {
        private readonly ILoanCardRepo _loanCardRepo;

        public LoanCardService(ILoanCardRepo loanCardRepo)
        {
            _loanCardRepo = loanCardRepo;
        }

        public List<LoanCardResponse> GetAllLoanCards()
        {
            var loanCards =  _loanCardRepo.GetAllLoanCards();

            var loanCardsResponse = loanCards.Select(loanCard => new LoanCardResponse
            {
                LoanId = loanCard.LoanId,
                LoanType = loanCard.LoanType,
                DurationInYears = loanCard.DurationInYears,
            }).ToList();

            return loanCardsResponse;
        }

        public LoanCardResponse? GetLoanCardById(string id)
        {
            var loanCard = _loanCardRepo.GetLoanCardById(id);

            if(loanCard == null)
            {
                return null;
            }

            var loanCardResponse = new LoanCardResponse

            {
                LoanId = loanCard.LoanId,
                LoanType = loanCard.LoanType,
                DurationInYears = loanCard.DurationInYears
            };

            return loanCardResponse;
        }

        public string AddLoanCard(LoanCardRequest loanCardRequest)
        {
            var loanCard = new LoanCardMaster
            {
                LoanId = UIDGenerator.GenerateUniqueVarcharId("CARD"),
                LoanType = loanCardRequest.LoanType,
                DurationInYears = loanCardRequest.DurationInYears
            };
            Console.WriteLine("loan id is " + loanCard.LoanId);
            return _loanCardRepo.AddLoanCard(loanCard);
        }

        public bool UpdateLoanCard(LoanCardMaster loanCard)
        {
            return _loanCardRepo.UpdateLoanCard(loanCard);
        }

        public bool DeleteLoanCard(string id)
        {
            return _loanCardRepo.DeleteLoanCard(id);
        }
    }
}
