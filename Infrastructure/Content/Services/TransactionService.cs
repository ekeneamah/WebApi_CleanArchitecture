using Application.Dtos.Email;
using Application.Interfaces;
using Application.Interfaces.Email.Templates;
using Domain.Models;
using Infrastructure.Content.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Product p = await _dbContext.Products.Where(p => p.Product_Id == transactionResponse.ProductId).FirstOrDefaultAsync();
            AppUser a = await _userManager.FindByIdAsync(transactionResponse.UserId);
            TRansactionComplete emailBody = new();
            int coyId = await _dbContext.Products.Where(p=>p.Product_Id==transactionResponse.ProductId).Select(c=>c.Coy_Id).FirstOrDefaultAsync();
            string coyEmail =await  _dbContext.InsuranceCompany.Where(i=>i.Coy_Id== coyId).Select(e=>e.Coy_Email).FirstOrDefaultAsync();
            await _emailSender.SendEmailAsync(new EmailRequest()
            {
                ToEmail = transactionResponse.UserEmail ,
                CCEmail = coyEmail,
                Subject = "Transaction Receipt",

                Body = emailBody.GetEmailBody(transactionResponse, p, a.FirstName),
               
            });
            await _emailSender.SendEmailAsync(new EmailRequest()
            {
                ToEmail = coyEmail,
               // CCEmail = coyEmail,
                Subject = "Transaction Receipt for "+a.FirstName+" "+a.LastName,

                Body = emailBody.GetEmailBodyInsuranceCoy(transactionResponse, p, a.FirstName),

            });
            return await _dbContext.SaveChangesAsync();
        }
    }
}
