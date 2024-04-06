using Application.Dtos;
using Application.Interfaces.Content.Policy;
using Domain.Models;
using Infrastructure.Content.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Content.Services
{
    public class PolicyService : IPolicy
    {
        private readonly AppDbContext _context;
        private readonly InsuranceCoyService _insuranceCoyService ;
        private readonly ProductService productService;
        private readonly UserProfileService userProfileService;
        private UserManager<AppUser> _userManager;

        public PolicyService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _insuranceCoyService = new InsuranceCoyService(context);
            productService = new ProductService(context);
            _userManager = userManager;
            userProfileService = new UserProfileService(context, _userManager);

        }
        public async Task<int> AddPolicy(PolicyDTO x)
        {
            Policy pd = new()
            {
                Coy_Id = x.Coy_Id,
                PolicyNo = x.PolicyNo,
                Price = x.Price,
                ProductCode = x.ProductCode,
                ProductId = x.ProductId,
                PurchasedDate = x.PurchasedDate,
                TransactionRef = x.TransactionRef,
                TransactionStatus = x.TransactionStatus,
                UserId = x.UserId,
                PaymentRef = x.PaymentRef,
                
            };
           _context.Policies.Add(pd);
            return await _context.SaveChangesAsync();
        }

        public Task<bool> POlicyIsExist(string categoryName)
        {
            throw new NotImplementedException();
        }

        public PolicyDTO DeletePolicy(PolicyDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PolicyDTO>> GetAll()
        {
            List<Policy> p = await _context.Policies
                 .AsNoTracking()
               .ToListAsync();
            List<PolicyDTO> result = new List<PolicyDTO>();
            foreach (Policy x in p)
            {
                PolicyDTO pd = new()
                {
                    Coy_Id = x.Coy_Id,
                    PolicyNo = x.PolicyNo,
                    Price = x.Price,
                    ProductCode = x.ProductCode,
                    ProductId = x.ProductId,
                    PurchasedDate = x.PurchasedDate,
                    TransactionRef = x.TransactionRef,
                    TransactionStatus = x.TransactionStatus,
                    UserId = x.UserId,
                    InsuranceCoy = await _insuranceCoyService.GetById(x.Coy_Id),
                    Product = await productService.GetProductByCode(x.ProductCode),
                    UserProfile = await userProfileService.GetById(x.UserId)
                };
                result.Add(pd);
            }
            return result;

        }

        public async Task<PolicyDTO> GetById(int id)
        {
            Policy x = await _context.Policies.FirstOrDefaultAsync(x => x.Id == id);
            PolicyDTO pd = new()
            {
                Coy_Id = x.Coy_Id,
                PolicyNo = x.PolicyNo,
                Price = x.Price,
                ProductCode = x.ProductCode,
                ProductId = x.ProductId,
                PurchasedDate = x.PurchasedDate,
                TransactionRef = x.TransactionRef,
                TransactionStatus = x.TransactionStatus,
                UserId = x.UserId,
                PaymentRef = x.PaymentRef,
                InsuranceCoy = await _insuranceCoyService.GetById(x.Coy_Id),
                Product = await productService.GetProductByCode(x.ProductCode),
                UserProfile = await userProfileService.GetById(x.UserId)
            };
            return pd;

        }

        public async Task<List<PolicyDTO>> GetAllPolicyByUserId(string userId)
        {
            List<Policy> p = await _context.Policies.Where(u=>u.UserId==userId)
                 .AsNoTracking()
               .ToListAsync();
            List<PolicyDTO> result = new();
            foreach (Policy x in p)
            {
                PolicyDTO pd = new()
                {
                    PolicyId = x.Id,
                    PolicyNo= x.PolicyNo,
                    Coy_Id = x.Coy_Id,
                    Price = x.Price,
                    ProductCode = x.ProductCode,
                    ProductId = x.ProductId,
                    PurchasedDate = x.PurchasedDate,
                    TransactionRef = x.TransactionRef,
                    TransactionStatus = x.TransactionStatus,
                    UserId = x.UserId,
                    PaymentRef = x.PaymentRef,
                    InsuranceCoy = await _insuranceCoyService.GetById(x.Coy_Id),
                    Product = await productService.GetProductByCode(x.ProductCode),
                    UserProfile = await userProfileService.GetById(x.UserId)
                };
                result.Add(pd);
            }
            return result;
        }

        public async Task<PolicyDTO> GetByUserName(string userid)
        {
            Policy x = await _context.Policies.FirstOrDefaultAsync(x => x.UserId == userid);
            PolicyDTO pd = new()
            {
                Coy_Id = x.Coy_Id,
                PolicyNo = x.PolicyNo,
                Price = x.Price,
                ProductCode = x.ProductCode,
                ProductId = x.ProductId,
                PurchasedDate = x.PurchasedDate,
                TransactionRef = x.TransactionRef,
                PaymentRef = x.PaymentRef,
                TransactionStatus = x.TransactionStatus,
                UserId = x.UserId,
                InsuranceCoy = await _insuranceCoyService.GetById(x.Coy_Id),
                Product = await productService.GetProductByCode(x.ProductCode),
                UserProfile = await userProfileService.GetById(x.UserId)
            };
            return pd;

        }

        public PolicyDTO UpdatePolicy(Category model)
        {
            throw new NotImplementedException();
        }

        public PolicyDTO UpdatePolicy(PolicyDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
