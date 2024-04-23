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
        public async Task<int> AddPolicy(Application.Dtos.TransactionDTO x)
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

        public Application.Dtos.TransactionDTO DeletePolicy(Application.Dtos.TransactionDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PolicyDetailDTO>> GetAll()
        {
            List<Policy> p = await _context.Policies
                 .AsNoTracking()
               .ToListAsync();
            List<PolicyDetailDTO> result = new List<PolicyDetailDTO>();
            foreach (Policy x in p)
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
            Policy x = await _context.Policies.FirstOrDefaultAsync(x => x.Id == id);
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
            List<Policy> p = await _context.Policies.Where(u => u.UserId == userId)
                  .ToListAsync();
            if (p.Any())
            {
               
                List<PolicyDetailDTO> result = new();
                foreach (Policy x in p)
                {
                    PolicyDetailDTO pd = new()
                    {
                        PolicyId = x.Id,
                        PolicyNo = x.PolicyNo,
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
            else
            {
              return   new List<PolicyDetailDTO>();
            }
        }

        public async Task<List<PolicyDetailDTO>> GetByUserName(string userid)
        {
            List<PolicyDetailDTO> result = new();
            List<Policy> policies = await _context.Policies.Where(u=>u.UserId==userid).ToListAsync();
            foreach (Policy x in policies)
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

        public Application.Dtos.TransactionDTO UpdatePolicy(Category model)
        {
            throw new NotImplementedException();
        }

        public PolicyDetailDTO UpdatePolicy(Application.Dtos.TransactionDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GeneratePolicyNumber(GeneratePolicyDTO generatePolicyDTO)
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
            PolicyGenReturnedData_cornerstoneDTO retPolicy =  await _getPolicyNumberService.PostPolicyAndTransform("https://testcipapiservices.gibsonline.com/api/policies", getPolicyNumberDTO);
            if (retPolicy != null)
            {
               
                PolicyGenReturnedData_cornerstoneDTO.ProductId = productDetails.Product_Id;
                Policy p = _context.Policies.FirstOrDefault(p => p.ProductId == productDetails.Product_Id);
                if (p != null)
                {
                    p.PolicyNo = retPolicy.policyNo;
                }
                _context.Policies.Update(p);
                 await _context.SaveChangesAsync();
                await SavePolicyNo(retPolicy);
                return retPolicy.policyNo;

            }
            else
            {
                return "No policy number yet";
            }
        }

        private async Task SavePolicyNo(PolicyGenReturnedData_cornerstoneDTO retPolicy)
        {
            PolicyGenReturnedData_cornerstone policyGenReturnedData_Cornerstone = new()
            {
                agentID = retPolicy.agentID,
                Certificate = retPolicy.Certificate,
                customerID = retPolicy.customerID,
                customerName = retPolicy.customerName,
                documentNo = retPolicy.documentNo,
                endDate = retPolicy.endDate,
                startDate = retPolicy.startDate,
                entryDate = retPolicy.entryDate,
                fxCurrency = retPolicy.fxCurrency,
                fxRate = retPolicy.fxRate,
                naicomID = retPolicy.naicomID,
                policyNo = retPolicy.policyNo,
                premium = retPolicy.premium,
                productID = retPolicy.productID,
                sumInsured = retPolicy.sumInsured,
            };
            _context.PolicyGenReturnedData_cornerstone.Add(policyGenReturnedData_Cornerstone);
           var result = await _context.SaveChangesAsync();
            List<PolicySection> policySection = new List<PolicySection>();
            foreach(Section ps in retPolicy.sections)
            {
                PolicySection psSection = new PolicySection()
                {
                    sectionPremium = ps.sectionPremium,
                    PolicyGenReturnedData_cornerstone_Id = policyGenReturnedData_Cornerstone.Id,
                    sectionSumInsured = ps.sectionSumInsured,
                };
                _context.policySections.Add(psSection);
                await _context.SaveChangesAsync();
                foreach(Rate sr in ps.rates)
                {
                    PolicySectionRate pr = new()
                    {
                        PolicySection_id = psSection.Id,
                        code = sr.code,
                        value = sr.value,
                    };
                    _context.PolicySectionRates.Add(pr);
                    await _context.SaveChangesAsync();

                }

                foreach (Field item in ps.fields)
                {
                    PolicySectionField sf = new()
                    {
                        PolicySection_id = psSection.Id,
                        value = item.value,
                        code = item.code,

                    };
                    _context.PolicySectionFields.Add(sf);
                    await _context.SaveChangesAsync();

                    foreach(Smi sm in ps.smIs)
                    {
                        PolicySectionSmi smi = new()
                        {
                            PolicySection_id = psSection.Id,
                            code = sm.code,
                            description = sm.description,
                            premium = sm.premium,
                            premiumRate = sm.premiumRate,
                            sumInsured = sm.sumInsured,

                        };
                        _context.PolicySectionSmis.Add(smi);
                        await _context.SaveChangesAsync();  
                    }
                }


            }

        }

        
    }
}
