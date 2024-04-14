using Application.Dtos;
using Application.Interfaces.Content.Policy;
using Domain.Entities;
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
        private readonly GetPolicyNumberService _getPolicyNumberService;
        private readonly KYCService _kYCService;

        public PolicyService(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _insuranceCoyService = new InsuranceCoyService(context);
            productService = new ProductService(context);
            _userManager = userManager;
            userProfileService = new UserProfileService(context, _userManager);

        }
        public PolicyService(AppDbContext context,GetPolicyNumberService getPolicyNumberService,KYCService kYCService)
        {
            _context = context;
            _getPolicyNumberService = getPolicyNumberService;
            _kYCService = kYCService;

         

        }
        public async Task<int> AddPolicy(PolicyDTO x)
        {
            PolicyEntity pd = new()
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

        public async Task<List<PolicyDetailDTO>> GetAll()
        {
            List<PolicyEntity> p = await _context.Policies
                 .AsNoTracking()
               .ToListAsync();
            List<PolicyDetailDTO> result = new List<PolicyDetailDTO>();
            foreach (PolicyEntity x in p)
            {
                PolicyDetailDTO pd = new()
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
                    UserProfile = await userProfileService.GetProfilebyUserid(x.UserId)
                };
                result.Add(pd);
            }
            return result;

        }

        public async Task<PolicyDetailDTO> GetById(int id)
        {
            PolicyEntity x = await _context.Policies.FirstOrDefaultAsync(x => x.Id == id);
            PolicyDetailDTO pd = new()
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
                UserProfile = await userProfileService.GetProfilebyUserid(x.UserId)
            };
            return pd;

        }

        public async Task<List<PolicyDetailDTO>> GetAllPolicyByUserId(string userId)
        {
            List<PolicyEntity> p = await _context.Policies.Where(u=>u.UserId==userId)
                 .AsNoTracking()
               .ToListAsync();
            List<PolicyDetailDTO> result = new();
            foreach (PolicyEntity x in p)
            {
                PolicyDetailDTO pd = new()
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
                    UserProfile = await userProfileService.GetProfilebyUserid(x.UserId)
                };
                result.Add(pd);
            }
            return result;
        }

        public async Task<List<PolicyDetailDTO>> GetByUserName(string userid)
        {
            List<PolicyDetailDTO> result = new();
            List<PolicyEntity> policies = await _context.Policies.Where(u=>u.UserId==userid).ToListAsync();
            foreach (PolicyEntity x in policies)
            {
                PolicyDetailDTO pd = new()
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
                    UserProfile = await userProfileService.GetProfilebyUserid(x.UserId)
                };
                result.Add(pd);
            }
            return result;

        }

        public PolicyDTO UpdatePolicy(CategoryEntity model)
        {
            throw new NotImplementedException();
        }

        public PolicyDetailDTO UpdatePolicy(PolicyDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<PolicyGenReturnedData> GeneratePolicyNumber(GeneratePolicyDTO generatePolicyDTO)
        {
            ProductDtoDetails productDetails = await productService.GetDetailsById(generatePolicyDTO.ProductId);
            UserProfileDto userProfileDto = await userProfileService.GetProfilebyUserid(generatePolicyDTO.Userid);
            KYCDTO kYCDTO = await _kYCService.GetKYCById(generatePolicyDTO.kycid);
            InsuredDTO insured = new InsuredDTO()
            {
                address = userProfileDto.ResidentialAddress,
                isOrg = productDetails.InsuranceCoy.IsOrg,
                title = productDetails.InsuranceCoy.Title,
                firstName = userProfileDto.FirstName,
                lastName = userProfileDto.LastName,
                otherName = userProfileDto.OtherName,
                gender = userProfileDto.Gender,
                email = userProfileDto.Email,
                phoneLine1 = userProfileDto.Phone,
                cityLGA = userProfileDto.LocalGovernment,
                stateID = userProfileDto.City,
                nationality = userProfileDto.Country,
                dateOfBirth = userProfileDto.DateofBirth == null ? DateTime.Today : DateTime.Parse(userProfileDto.DateofBirth),
                taxIdNumber = userProfileDto.TaxIdNumber,
                kycType = kYCDTO.IdentityType,
                kycExpiryDate = kYCDTO.FromExpiryDate,
                kycIssueDate = kYCDTO.ToExpiryDate,
                kycNumber = kYCDTO.IdentityNumber,
                

            };
           
           

            GetPolicyNumberDTO getPolicyNumberDTO = new GetPolicyNumberDTO()
            {
                agentID = productDetails.InsuranceCoy.Coy_AgentId,
                endDate=generatePolicyDTO.EndDate,
                startDate = generatePolicyDTO.StartDate,
                insured = insured,
                sections = generatePolicyDTO.sections,
                Token = generatePolicyDTO.Token,

            };
          return await _getPolicyNumberService.PostPolicyAndTransform("https://testcipapiservices.gibsonline.com/api/policies", getPolicyNumberDTO);
        }
    }
}
