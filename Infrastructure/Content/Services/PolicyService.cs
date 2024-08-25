using Application.Dtos;
using Application.Interfaces.Content.Policy;
using Domain.Entities;
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
using Application.Common;
using Application.Interfaces.Content.Brands;

namespace Infrastructure.Content.Services
{
    public class PolicyService : IPolicy
    {
        private readonly AppDbContext _context;
        private readonly IInsuranceCoy _insuranceCoyService ;
        private readonly ProductService productService;
        private readonly UserProfileService userProfileService;
        private UserManager<AppUser> _userManager;
        private readonly GetPolicyNumberService _getPolicyNumberService;
        private readonly KYCService _kYCService;

        public PolicyService(AppDbContext context, UserManager<AppUser> userManager,IInsuranceCoy insuranceCoy)
        {
            _context = context;
            _insuranceCoyService = insuranceCoy;
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
        public async Task<int> AddPolicy(Application.Dtos.TransactionDto x)
        {
            Policy pd = new()
            {
                CoyId = x.CoyId,
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

        public Application.Dtos.TransactionDto DeletePolicy(Application.Dtos.TransactionDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<List<PolicyDetailDto>>> GetAll()
        {
            List<Policy> p = await _context.Policies
                 .AsNoTracking()
               .ToListAsync();
            List<PolicyDetailDto> result = new List<PolicyDetailDto>();
            foreach (Policy x in p)
            {
                PolicyDetailDto pd = new()
                {
                    CoyId = x.CoyId,
                    PolicyNo = x.PolicyNo,
                    Price = x.Price,
                    ProductCode = x.ProductCode,
                    ProductId = x.ProductId,
                    PurchasedDate = x.PurchasedDate,
                    TransactionRef = x.TransactionRef,
                    TransactionStatus = x.TransactionStatus,
                    UserId = x.UserId,
                    InsuranceCoy = (await _insuranceCoyService.GetInsuranceCoyDetailById(x.CoyId)).Data,
                    Product = (await productService.GetProductByCode(x.ProductCode)).Data,
                    UserProfile = (await userProfileService.GetProfilebyUserid(x.UserId)).Data
                };
                result.Add(pd);
            }
            return ApiResult<List<PolicyDetailDto>>.Successful(result);


        }

        public async Task<ApiResult<PolicyDetailDto>> GetById(int id)
        {
            Policy x = await _context.Policies.FirstOrDefaultAsync(x => x.Id == id);
            PolicyDetailDto pd = new()
            {
                CoyId = x.CoyId,
                PolicyNo = x.PolicyNo,
                Price = x.Price,
                ProductCode = x.ProductCode,
                ProductId = x.ProductId,
                PurchasedDate = x.PurchasedDate,
                TransactionRef = x.TransactionRef,
                TransactionStatus = x.TransactionStatus,
                UserId = x.UserId,
                PaymentRef = x.PaymentRef,
                InsuranceCoy = (await _insuranceCoyService.GetInsuranceCoyDetailById(x.Id)).Data,
                Product = (await productService.GetProductByCode(x.ProductCode)).Data,
                UserProfile = (await userProfileService.GetProfilebyUserid(x.UserId)).Data
            };
            return ApiResult<PolicyDetailDto>.Successful(pd);


        }

        public async Task<ApiResult<List<PolicyDetailDto>>> GetAllPolicyByUserId(string userId)
        {
            List<Policy> p = await _context.Policies.Where(u => u.UserId == userId)
                  .ToListAsync();
            if (p.Any())
            {
               
                List<PolicyDetailDto> result = new();
                foreach (Policy x in p)
                {
                    PolicyDetailDto pd = new()
                    {
                        PolicyId = x.Id,
                        PolicyNo = x.PolicyNo,
                        CoyId = x.CoyId,
                        Price = x.Price,
                        ProductCode = x.ProductCode,
                        ProductId = x.ProductId,
                        PurchasedDate = x.PurchasedDate,
                        TransactionRef = x.TransactionRef,
                        TransactionStatus = x.TransactionStatus,
                        UserId = x.UserId,
                        PaymentRef = x.PaymentRef,
                        InsuranceCoy = (await _insuranceCoyService.GetInsuranceCoyDetailById(x.CoyId)).Data,
                        Product = (await productService.GetProductByCode(x.ProductCode)).Data,
                        UserProfile = (await userProfileService.GetProfilebyUserid(x.UserId)).Data
                    };
                    result.Add(pd);
                }
                return ApiResult<List<PolicyDetailDto>>.Successful(result);

            }
            else
            {
              return ApiResult<List<PolicyDetailDto>>.Successful(null);

            }
        }

        public async Task<ApiResult<List<PolicyDetailDto>>> GetByUserName(string userid)
        {
            List<PolicyDetailDto> result = new();
            List<Policy> policies = await _context.Policies.Where(u=>u.UserId==userid).ToListAsync();
            foreach (Policy x in policies)
            {
                PolicyDetailDto pd = new()
                {
                    CoyId = x.CoyId,
                    PolicyNo = x.PolicyNo,
                    Price = x.Price,
                    ProductCode = x.ProductCode,
                    ProductId = x.ProductId,
                    PurchasedDate = x.PurchasedDate,
                    TransactionRef = x.TransactionRef,
                    PaymentRef = x.PaymentRef,
                    TransactionStatus = x.TransactionStatus,
                    UserId = x.UserId,
                    InsuranceCoy = (await _insuranceCoyService.GetInsuranceCoyDetailById(x.CoyId)).Data,
                    Product = (await productService.GetProductByCode(x.ProductCode)).Data,
                    UserProfile = (await userProfileService.GetProfilebyUserid(x.UserId)).Data
                };
                result.Add(pd);
            }
            return ApiResult<List<PolicyDetailDto>>.Successful(result);


        }

        public Application.Dtos.TransactionDto UpdatePolicy(Category model)
        {
            throw new NotImplementedException();
        }

        public PolicyDetailDto UpdatePolicy(Application.Dtos.TransactionDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GeneratePolicyNumber(GeneratePolicyDto generatePolicyDTO)
        {
            var productDetails = (await productService.GetDetailsById(generatePolicyDTO.ProductId)).Data;
            UserProfileDto userProfileDto = await userProfileService.GetProfilebyUserid(generatePolicyDTO.Userid);
            var kYCDTO = (await _kYCService.GetKYCById(generatePolicyDTO.Kycid)).Data;
            InsuredDto insured = new InsuredDto()
            {
                Address = userProfileDto.ResidentialAddress,
                IsOrg = productDetails.InsuranceCoy.IsOrg,
                Title = productDetails.InsuranceCoy.Title,
                FirstName = userProfileDto.FirstName,
                LastName = userProfileDto.LastName,
                OtherName = userProfileDto.OtherName,
                Gender = userProfileDto.Gender,
                Email = userProfileDto.Email,
                PhoneLine1 = userProfileDto.Phone,
                CityLga = userProfileDto.LocalGovernment,
                StateId = userProfileDto.City,
                Nationality = userProfileDto.Country,
                DateOfBirth = userProfileDto.DateofBirth == null ? DateTime.Today : DateTime.Parse(userProfileDto.DateofBirth),
                TaxIdNumber = userProfileDto.TaxIdNumber,
                KycType = kYCDTO.IdentityType,
                KycExpiryDate = kYCDTO.FromExpiryDate,
                KycIssueDate = kYCDTO.ToExpiryDate,
                KycNumber = kYCDTO.IdentityNumber,
                

            };
           
           

            GetPolicyNumberDto getPolicyNumberDTO = new GetPolicyNumberDto()
            {
                AgentId = productDetails.InsuranceCoy.CoyAgentId,
                EndDate=generatePolicyDTO.EndDate,
                StartDate = generatePolicyDTO.StartDate,
                Insured = insured,
                Sections = generatePolicyDTO.Sections,
                Token = generatePolicyDTO.Token,

            };
            PolicyGenReturnedDataCornerstoneDto retPolicy =  await _getPolicyNumberService.PostPolicyAndTransform("https://testcipapiservices.gibsonline.com/api/policies", getPolicyNumberDTO);
            if (retPolicy != null)
            {
               
                PolicyGenReturnedDataCornerstoneDto.ProductId = productDetails.ProductId;
                Policy p = _context.Policies.FirstOrDefault(p => p.ProductId == productDetails.ProductId);
                if (p != null)
                {
                    p.PolicyNo = retPolicy.PolicyNo;
                }
                _context.Policies.Update(p);
                 await _context.SaveChangesAsync();
                await SavePolicyNo(retPolicy);
                return retPolicy.PolicyNo;

            }
            else
            {
                return "No policy number yet";
            }
        }

        private async Task SavePolicyNo(PolicyGenReturnedDataCornerstoneDto retPolicy)
        {
            PolicyGenReturnedDataCornerstone policyGenReturnedData_Cornerstone = new()
            {
                AgentId = retPolicy.AgentId,
                Certificate = retPolicy.Certificate,
                CustomerId = retPolicy.CustomerId,
                CustomerName = retPolicy.CustomerName,
                DocumentNo = retPolicy.DocumentNo,
                EndDate = retPolicy.EndDate,
                StartDate = retPolicy.StartDate,
                EntryDate = retPolicy.EntryDate,
                FxCurrency = retPolicy.FxCurrency,
                FxRate = retPolicy.FxRate,
                NaicomId = retPolicy.NaicomId,
                PolicyNo = retPolicy.PolicyNo,
                Premium = retPolicy.Premium,
                ProductId = retPolicy.Product_Id,
                SumInsured = retPolicy.SumInsured,
            };
            _context.PolicyGenReturnedData_cornerstone.Add(policyGenReturnedData_Cornerstone);
           var result = await _context.SaveChangesAsync();
            List<PolicySection> policySection = new List<PolicySection>();
            foreach(Section ps in retPolicy.Sections)
            {
                PolicySection psSection = new PolicySection()
                {
                    SectionPremium = ps.SectionPremium,
                    PolicyGenReturnedDataCornerstoneId = policyGenReturnedData_Cornerstone.Id,
                    SectionSumInsured = ps.SectionSumInsured,
                };
                _context.policySections.Add(psSection);
                await _context.SaveChangesAsync();
                foreach(Rate sr in ps.Rates)
                {
                    PolicySectionRate pr = new()
                    {
                        PolicySectionId = psSection.Id,
                        Code = sr.Code,
                        Value = sr.Value,
                    };
                    _context.PolicySectionRates.Add(pr);
                    await _context.SaveChangesAsync();

                }

                foreach (Field item in ps.Fields)
                {
                    PolicySectionField sf = new()
                    {
                        PolicySectionId = psSection.Id,
                        Value = item.Value,
                        Code = item.Code,

                    };
                    _context.PolicySectionFields.Add(sf);
                    await _context.SaveChangesAsync();

                    foreach(Smi sm in ps.SmIs)
                    {
                        PolicySectionSmi smi = new()
                        {
                            PolicySectionId = psSection.Id,
                            Code = sm.Code,
                            Description = sm.Description,
                            Premium = sm.Premium,
                            PremiumRate = sm.PremiumRate,
                            SumInsured = sm.SumInsured,

                        };
                        _context.PolicySectionSmis.Add(smi);
                        await _context.SaveChangesAsync();  
                    }
                }


            }

        }

        
    }
}
