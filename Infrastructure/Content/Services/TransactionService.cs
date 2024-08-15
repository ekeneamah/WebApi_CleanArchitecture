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
            return await _dbContext.SaveChangesAsync(); ;
        }

        public async Task<int> DeleteResponse(Transaction transactionResponse)
        {
            _dbContext.Transactions.Remove(transactionResponse);
            return await _dbContext.SaveChangesAsync(); ;
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

        public async Task<TransactionDto> GetTransactionByReference(string reference)
        {
            Transaction t =  await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Reference == reference);
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
            return td;
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByUserId(string userId)
        {
            List<TransactionDto> td = new();
            List<Transaction> transactions = await _dbContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
            foreach(Transaction t in transactions)
            {
                TransactionDto tdto = new()
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

                };
                td.Add(tdto);
            }
            return td;
        }
    }
}
