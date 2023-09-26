using backendAPIs.Models;
using backendAPIs.Repository.Interfaces;
using backendAPIs.Models.Request;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace backendAPIs.Repository
{
    public class LoanCardRepo : ILoanCardRepo
    {
        private readonly LoansContext _db;

        public LoanCardRepo(LoansContext db)
        {
            _db=db;
        }

        public List<LoanCardMaster> GetAllLoanCards()
        {
            return  _db.LoanCardMasters.ToList();
        }

        public LoanCardMaster GetLoanCardById(string id)
        {
            return _db.LoanCardMasters.Find(id);
        }

        public string AddLoanCard(LoanCardMaster loanCard)
        {
            try
            {
                _db.LoanCardMasters.Add(loanCard);
                _db.SaveChanges();
                return loanCard.LoanId;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool UpdateLoanCard(UpdateLoanCardRequest loanCard)
        {
            var existingLoanCard = _db.LoanCardMasters.FirstOrDefault(l => l.LoanId == loanCard.LoanId);

            if(existingLoanCard!= null)
            {
                existingLoanCard.LoanType = loanCard.LoanType;
                existingLoanCard.DurationInYears = loanCard.DurationInYears;

                var entry = _db.Entry(existingLoanCard);
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                    entry.State = EntityState.Detached;
                _db.Entry(existingLoanCard).State = EntityState.Modified;

                try
                {
                    _db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return false;

        }

        public bool DeleteLoanCard(string id)
        {
            LoanCardMaster? loanCard =  _db.LoanCardMasters.Find(id);
            if (loanCard == null)
            {
                return false;
            }
            else
            {
                try
                {
                    _db.LoanCardMasters.Remove(loanCard);

                    _db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
