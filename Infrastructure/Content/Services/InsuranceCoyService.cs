using Application.Common;
using Application.Dtos;
using Application.Interfaces.Content.Brands;
using Domain.Entities;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Content.Services
{
    public class InsuranceCoyService : IInsuranceCoy
    {
        private readonly AppDbContext _context;

        public InsuranceCoyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<List<InsuranceCoyDto>>> GetAll(int pageNumber, int pageSize)
        {
            List<InsuranceCoyDto> result = new List<InsuranceCoyDto>();
            List<InsuranceCoy> InsuranceCoy = await _context.InsuranceCompany
                .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
            foreach (InsuranceCoy i in InsuranceCoy)
            {
                InsuranceCoyDto insuranceCoyDTO = new()
                {
                    CoyName = i.CoyName,
                    CoyId = i.CoyId,
                    CoyEmail = i.CoyEmail,
                    CoyImage = i.CoyImage,
                    CoyBenefits = await _context.CoyBenefits.Where(c => c.CoyId == i.CoyId).ToListAsync(),
                    CoyLogo = i.CoyLogo,
                    CoyCity = i.CoyCity,
                    CoyVideoLink = i.CoyVideoLink,
                    CoyDescription = i.CoyDescription,
                    CoyCountry = i.CoyCountry,
                    CoyPhone = i.CoyPhone,
                    CoyCityCode = i.CoyCityCode,
                    CoyStreet = i.CoyStreet,
                    CoyCountryCode = i.CoyCountryCode,
                    CoyPostalCode = i.CoyPostalCode,
                    CoyState = i.CoyState,
                    CoyStatus = i.CoyStatus,
                    CoyAgentId = i.CoyAgentId,
                    IsOrg = i.IsOrg,
                    Title = i.Title==null?"":i.Title,
                };
                result.Add(insuranceCoyDTO);
            } 
            return ApiResult<List<InsuranceCoyDto>>.Successful(result);

        }


        public async Task<ApiResult<InsuranceCoyDto>> GetById(int id)
        {
            var i = await _context.InsuranceCompany.Where(m => m.CoyId == id).FirstOrDefaultAsync();
            if (i is null)
            {
                return ApiResult<InsuranceCoyDto>.NotFound("Insurance Company not found");
            }
            InsuranceCoyDto insuranceCoyDTO = new()
            {
                CoyName = i.CoyName,
                CoyId = i.CoyId,
                CoyEmail = i.CoyEmail,
                CoyImage = i.CoyImage,
                CoyBenefits = await _context.CoyBenefits.Where(c => c.CoyId == i.CoyId).ToListAsync(),
                CoyLogo = i.CoyLogo,
                CoyCity = i.CoyCity,
                CoyVideoLink = i.CoyVideoLink,
                CoyDescription = i.CoyDescription,
                CoyCountry = i.CoyCountry,
                CoyPhone = i.CoyPhone,
                CoyCityCode = i.CoyCityCode,
                CoyStreet = i.CoyStreet,
                CoyCountryCode = i.CoyCountryCode,
                CoyPostalCode = i.CoyPostalCode,
                CoyState = i.CoyState,
                CoyStatus = i.CoyStatus,
                CoyAgentId = i.CoyAgentId,
                IsOrg = i.IsOrg,
                Title = i.Title,
                CoyZipCode = i.CoyZipCode
            };
            return ApiResult<InsuranceCoyDto>.Successful(insuranceCoyDTO);

        }


        public async Task<ApiResult<InsuranceCoyDto>> Add_Coy(InsuranceCoyDto model)
        {
            InsuranceCoy i = new()
            {
                CoyEmail = model.CoyEmail,
                CoyCity = model.CoyCity,
                CoyCityCode = model.CoyCityCode,
                CoyCountry = model.CoyCountry,
                CoyCountryCode = model.CoyCountryCode,
                CoyDescription = model.CoyDescription,
                CoyStreet = model.CoyStreet,
                CoyName = model.CoyName,
                CoyState = model.CoyState,
                CoyImage = model.CoyImage,
                CoyLogo = model.CoyLogo,
                CoyPhone = model.CoyPhone,
                CoyVideoLink = model.CoyVideoLink,
                CoyStatus = model.CoyStatus,
                CoyPostalCode = model.CoyPostalCode,
                CoyAgentId = model.CoyAgentId,
                Title = model.Title,
                IsOrg = model.IsOrg,
                CoyId = model.CoyId,
                CoyZipCode = model.CoyZipCode


            };

            await _context.InsuranceCompany.AddAsync(i);
            await _context.SaveChangesAsync();
            foreach (CoyBenefitEntity cb in model.CoyBenefits)
            {
                cb.CoyId = i.CoyId;
                _context.CoyBenefits.Add(cb);
                await _context.SaveChangesAsync();
            }
            return ApiResult<InsuranceCoyDto>.Successful(model);

        }


        public async Task<int> Update_Coy(InsuranceCoyDto model)
        {
            InsuranceCoy insuranceCoy = new()
            {
                CoyId = model.CoyId,
                CoyName = model.CoyName,
                CoyEmail = model.CoyEmail,
                CoyCity = model.CoyCity,
                CoyCityCode = model.CoyCityCode,
                CoyCountry = model.CoyCountry,
                CoyCountryCode = model.CoyCountryCode,
                CoyDescription = model.CoyDescription,
                CoyPhone = model.CoyPhone,
                CoyPostalCode = model.CoyPostalCode,
                CoyState = model.CoyState,
                CoyStatus = model.CoyStatus,
                CoyStreet = model.CoyStreet,
                CoyZipCode = model.CoyZipCode,
                CoyAgentId = model.CoyAgentId,
                IsOrg = model.IsOrg,
                Title = model.Title,
                CoyImage = model.CoyImage,
                CoyLogo = model.CoyLogo,
                CoyVideoLink = model.CoyVideoLink



            };
            InsuranceCoy coy = insuranceCoy;
            _context.InsuranceCompany.Update(coy);


            return await _context.SaveChangesAsync();
        }


        public async Task<ApiResult<InsuranceCoy>> Delete_Coy(InsuranceCoyDto model)
        {
            InsuranceCoy coy = new()
            {
                CoyId = model.CoyId,
                CoyName = model.CoyName,
                CoyEmail = model.CoyEmail,
                CoyCity = model.CoyCity,
                CoyAgentId = model.CoyAgentId,

            };
            _context.InsuranceCompany.Remove(coy);
            await _context.SaveChangesAsync();
            return ApiResult<InsuranceCoy>.Successful(coy);

        }


        public async Task<bool> CoyIsExist(string coy_Name)
        {
            return await _context.InsuranceCompany.AnyAsync(b => b.CoyName == coy_Name);
        }







    }
}
