using backend.Models;
using backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace backend.Repository
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

        public bool UpdateLoanCard(LoanCardMaster loanCard)
        {
            try
            {
                _db.LoanCardMasters.Update(loanCard);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
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
