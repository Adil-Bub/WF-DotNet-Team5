﻿using backend.Models;
using backend.Repository.Interfaces;

namespace backend.Repository
{
    public class EmployeeLoanCardDetailRepo : IEmployeeLoanCardDetailRepo
    {
        private readonly LoansContext _db;

        public EmployeeLoanCardDetailRepo(LoansContext db)
        {
            _db = db;
        }
        public string AddEmployeeLoanCard(EmployeeLoanCardDetail employeeLoanCardDetail)
        {
            try
            {
                _db.EmployeeLoanCardDetails.Add(employeeLoanCardDetail);
                _db.SaveChanges();
                return employeeLoanCardDetail.CardId;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}