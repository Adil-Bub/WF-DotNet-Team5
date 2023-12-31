﻿using Azure.Core;
using backendAPIs.Models;
using backendAPIs.Models.Request;
using backendAPIs.Models.Response;
using backendAPIs.Repository.Interfaces;
using backendAPIs.Services;
using backendAPIs.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;
using System;

namespace backendAPIs.Repository
{
    public class EmployeeRequestDetailRepo : IEmployeeRequestDetailRepo
    {
        private readonly LoansContext _db;

        public EmployeeRequestDetailRepo(LoansContext db)
        {
            _db = db;
        }
        public string AddEmployeeRequest(EmployeeRequestDetail employeeRequestDetail)
        {
            try
            {
                var employeeRequestExists = _db.EmployeeRequestDetails
                    .FirstOrDefault(employeeRequest => employeeRequest.EmployeeId == employeeRequestDetail.EmployeeId &&
                    employeeRequest.ItemId == employeeRequestDetail.ItemId);

                if (employeeRequestExists !=null)
                {
                    return null;
                }
                _db.EmployeeRequestDetails.Add(employeeRequestDetail);
                _db.SaveChanges();
                return employeeRequestDetail.RequestId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public EmployeeRequestDetail DeleteEmployeeRequest(string id)
        {
            EmployeeRequestDetail? employeeRequestDetail = _db.EmployeeRequestDetails.Find(id);
            if (employeeRequestDetail == null)
            {
                return null;
            }
            else
            {
                try
                {
                    _db.EmployeeRequestDetails.Remove(employeeRequestDetail);

                    _db.SaveChanges();
                    return employeeRequestDetail;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public bool UpdateEmployeeRequest(UpdateEmployeeLoanRequest employeeRequestDetail)
        {
            var existingEmployeeRequest = _db.EmployeeRequestDetails.FirstOrDefault(emplReq => emplReq.RequestId == employeeRequestDetail.RequestId);
            Console.WriteLine("employee is " + existingEmployeeRequest);
            if (existingEmployeeRequest != null)
            {
                var duration = 0;
                var issueDate = DateTime.Now.Date;
                existingEmployeeRequest.RequestStatus = employeeRequestDetail.RequestStatus;

                //Adding approveed loans to employee loan card details
                //Return date gets updated only when status is approved
                if (employeeRequestDetail.RequestStatus == "Approved")
                {
                    var loanCategory = _db.ItemMasters
                        .Where(item => existingEmployeeRequest.ItemId == item.ItemId)
                        .Select(item => item.ItemCategory)
                        .FirstOrDefault();

                    var loanId = _db.LoanCardMasters
                        .Where(loan => loan.LoanType == loanCategory)
                        .Select(loan => loan.LoanId)
                        .FirstOrDefault();

                    duration = _db.LoanCardMasters
                        .Where(loan => loan.LoanId == loanId)
                        .Select(loan => loan.DurationInYears)
                        .FirstOrDefault();

                    //adding a new approved loan card
                    var employeeLoanCardDetail = new EmployeeLoanCardDetail
                    {
                        CardId = UIDGenerator.GenerateUniqueVarcharId("LOAN_CARD"),
                        RequestId = existingEmployeeRequest.RequestId,
                        EmployeeId = existingEmployeeRequest.EmployeeId,
                        LoanId = loanId,
                        CardIssueDate = DateTime.Now.Date
                    };
                    try
                    {
                        var loanCard = _db.EmployeeLoanCardDetails
                            .Where(lc => lc.RequestId ==  existingEmployeeRequest.RequestId)
                            .FirstOrDefault();
                        if (loanCard == null)
                        {
                            _db.EmployeeLoanCardDetails.Add(employeeLoanCardDetail);
                            _db.SaveChanges();
                        }

                        //return employeeLoanCardDetail.CardId;
                        //updating return date after adding loan card
                        existingEmployeeRequest.ReturnDate = issueDate.AddYears(duration);
                        var entry = _db.Entry(existingEmployeeRequest);
                        if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                            entry.State = EntityState.Detached;
                        _db.Entry(existingEmployeeRequest).State = EntityState.Modified;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return false;
                    }
                }
                try
                {
                    _db.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return false;
        }

        public List<EmployeeRequestDetail> GetAllEmployeeRequests()
        {
            return _db.EmployeeRequestDetails.ToList();
        }

        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByEmployeeId(string employeeId)
        {
            return _db.EmployeeRequestDetails
                .Where(employeeRequest => employeeRequest.EmployeeId == employeeId)
                .ToList();
        }

        public EmployeeRequestDetail GetEmployeeRequestDetailByRequestId(string requestId)
        {
            return _db.EmployeeRequestDetails.Find(requestId);
        }

        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByStatus(string status)
        {
            return _db.EmployeeRequestDetails
               .Where(employeeRequest => employeeRequest.RequestStatus == status)
               .ToList();
        }

        public List<EmployeeRequestDetail> GetAllEmployeeRequestsByItemId(string itemId)
        {
            return _db.EmployeeRequestDetails
               .Where(employeeRequest => employeeRequest.ItemId == itemId)
               .ToList();
        }

        public List<LoanDetailsResponse> GetAllLoanDetailsByEmployeeId(string employeeId)
        {
            var loanDetails = _db.EmployeeRequestDetails
                .Where(employeeRequest => employeeRequest.EmployeeId == employeeId)
                .Join(
                    _db.ItemMasters,
                    employeeRequest => employeeRequest.ItemId,
                    item => item.ItemId,
                    (employeeRequest, item) => new { EmployeeRequestDetail = employeeRequest, ItemMaster = item }
                )
                .Join(
                    _db.LoanCardMasters,
                    joined => joined.ItemMaster.ItemCategory,
                    loan => loan.LoanType,
                    (joined, loan) => new { joined.EmployeeRequestDetail, joined.ItemMaster, LoanCardMaster = loan }
                )
                .Select(
                    joined => new LoanDetailsResponse
                    {
                        ItemId = joined.ItemMaster.ItemId,
                        ItemDescription = joined.ItemMaster.ItemDescription,
                        IssueStatus = joined.ItemMaster.IssueStatus,
                        ItemMake = joined.ItemMaster.ItemMake,
                        ItemCategory = joined.ItemMaster.ItemCategory,
                        ItemValuation = joined.ItemMaster.ItemValuation,

                        RequestId = joined.EmployeeRequestDetail.RequestId,
                        EmployeeId = joined.EmployeeRequestDetail.EmployeeId,
                        RequestDate = joined.EmployeeRequestDetail.RequestDate,
                        RequestStatus = joined.EmployeeRequestDetail.RequestStatus,
                        ReturnDate = joined.EmployeeRequestDetail.ReturnDate,

                        LoanId = joined.LoanCardMaster.LoanId,
                        DurationInYears = joined.LoanCardMaster.DurationInYears

                    }
                )
                .ToList();

            return loanDetails;
        }

        public List<LoanDetailsAdminResponse>? GetAllRequestDetails()
        {
            List<EmployeeRequestDetail> loanDetails;
            List<LoanDetailsAdminResponse> loanDetailsResponse;
            try
            {
                loanDetails = _db.EmployeeRequestDetails.Include(r => r.Item).ThenInclude(r => r.ItemCategoryNavigation).ToList();
                loanDetailsResponse = loanDetails.Select(request => new LoanDetailsAdminResponse
                {
                    RequestId = request.RequestId,
                    ItemId = request.Item.ItemId,
                    ItemCategory = request.Item.ItemDescription,
                    ItemDescription = request.Item.ItemDescription,
                    IssueStatus = request.Item.IssueStatus,
                    ItemMake = request.Item.ItemMake,
                    ItemValuation = request.Item.ItemValuation,
                    RequestDate = request.RequestDate,
                    RequestStatus = request.RequestStatus,
                    ReturnDate = request.ReturnDate,
                    LoanId = request.Item.ItemCategoryNavigation.LoanId,
                    DurationInYears = request.Item.ItemCategoryNavigation.DurationInYears

                }).ToList();

                return loanDetailsResponse;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
