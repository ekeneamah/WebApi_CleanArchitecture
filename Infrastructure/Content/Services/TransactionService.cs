using Application.Dtos.Email;
using Application.Interfaces;
using Infrastructure.Content.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Interfaces.Email;
using Application.Interfaces.Email.Templates;
using Domain.Entities;

namespace Infrastructure.Content.Services
{
    public class TransactionService : ITransaction
    {
        private readonly AppDbContext _dbContext;
        private readonly IEmailService _emailSender;
        private readonly UserManager<AppUser> _userManager;
        public TransactionService(AppDbContext appDbContext,
            IEmailService emailSender,
            UserManager<AppUser> userManager)
        {
            _dbContext = appDbContext;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public async Task<int> SaveResponse(Transaction transactionResponse)
        {
            _dbContext.Transactions.Add(transactionResponse);
            return await _dbContext.SaveChangesAsync(); 
        }

        public async Task<int> DeleteResponse(Transaction transactionResponse)
        {
            _dbContext.Transactions.Remove(transactionResponse);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateResponse(Transaction transactionResponse)
        {
            _dbContext.Transactions.Update(transactionResponse);
            Product p = await _dbContext.Products.Where(p => p.ProductId == transactionResponse.ProductId).FirstOrDefaultAsync();
            AppUser a = await _userManager.FindByIdAsync(transactionResponse.UserId);
            TRansactionComplete emailBody = new();
            int coyId = await _dbContext.Products.Where(p=>p.ProductId==transactionResponse.ProductId).Select(c=>c.CoyId).FirstOrDefaultAsync();
            string coyEmail =await  _dbContext.InsuranceCompany.Where(i=>i.CoyId== coyId).Select(e=>e.CoyEmail).FirstOrDefaultAsync();
        
            /*await _emailSender.SendEmailAsync(coyEmail,
                // CCEmail = coyEmail,
                "TransactionEntity Receipt for " + a.FirstName + " " + a.LastName,

                //.GetEmailBodyInsuranceCoy(transactionResponse, p, a.FirstName));*/
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<ApiResult<TransactionDto>> GetTransactionByReference(string reference)
        {
            var t =  await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Reference == reference);
            TransactionDto td = new()
            {
                Reference = reference,
                Status = t.Status,
                AccessCode = t.AccessCode,
                Amount = t.Amount,
                AuthorizationUrl = t.AuthorizationUrl,
                DateTime = t.DateTime,
                PaymentRef = t.PaymentRef,
                PolicyNo = t.PolicyNo,
                UserEmail = t.UserEmail,
                Product = _dbContext.Products.FirstOrDefault(i => i.ProductId == t.ProductId)

            };
            return ApiResult<TransactionDto>.Successful(td);

        }

        public async Task<ApiResult<PaginatedListWithFIlter<TransactionDto>>> GetTransactionsByUserId(
    string userId,
    int pageNumber,
    int pageSize,
    DateTime? startDate = null,
    DateTime? endDate = null,
    string transactionType = null,
    string status = null)
        {
            // Retrieve the transactions for the user
            var query = _dbContext.Transactions.Where(t => t.UserId == userId);

            // Apply filters
            if (startDate.HasValue)
            {
                query = query.Where(t => t.DateTime >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.DateTime <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(transactionType))
            {
                query = query.Where(t => t.TransactionType == transactionType);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status == status);
            }

            // Apply pagination
            var paginatedTransactions = await PaginatedListWithFIlter<Transaction>.CreateAsync(query, pageNumber, pageSize);

            // Convert paginated list of transactions to TransactionDto list
            var td = paginatedTransactions.Select(t => new TransactionDto
            {
                Reference = t.Reference,
                Status = t.Status,
                AccessCode = t.AccessCode,
                Amount = t.Amount,
                AuthorizationUrl = t.AuthorizationUrl,
                DateTime = t.DateTime,
                PaymentRef = t.PaymentRef,
                PolicyNo = t.PolicyNo,
                UserEmail = t.UserEmail,
                Product = _dbContext.Products.FirstOrDefault(i => i.ProductId == t.ProductId)
            }).ToList();

            // Return the result wrapped in a PaginatedList
            var result = new PaginatedListWithFIlter<TransactionDto>(td, paginatedTransactions.TotalPages, paginatedTransactions.PageIndex, pageSize);

            return ApiResult<PaginatedListWithFIlter<TransactionDto>>.Successful(result);
        }

    }
}
