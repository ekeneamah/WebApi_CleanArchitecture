using Application.Dtos;
using Application.Interfaces.Content.Brands;
using Domain.Models;
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

        public async Task<List<InsuranceCoyDTO>> GetAll(int pageNumber, int pageSize)
        {
            List<InsuranceCoyDTO> result = new List<InsuranceCoyDTO>();
            List<InsuranceCoy> InsuranceCoy = await _context.InsuranceCompany
                .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();
            foreach (InsuranceCoy i in InsuranceCoy)
            {
                InsuranceCoyDTO insuranceCoyDTO = new()
                {
                    Coy_Name = i.Coy_Name,
                    Coy_id = i.Coy_Id,
                    Coy_Email = i.Coy_Email,
                    Coy_Image = i.Coy_Image,
                    Coy_Benefits = await _context.CoyBenefits.Where(c => c.Coy_id == i.Coy_Id).ToListAsync(),
                    Coy_Logo = i.Coy_Logo,
                    Coy_City = i.Coy_City,
                    Coy_VideoLink = i.Coy_VideoLink,
                    Coy_Description = i.Coy_Description,
                    Coy_Country = i.Coy_Country,
                    Coy_Phone = i.Coy_Phone,
                    Coy_CityCode = i.Coy_CityCode,
                    Coy_Street = i.Coy_Street,
                    Coy_CountryCode = i.Coy_CountryCode,
                    Coy_PostalCode = i.Coy_PostalCode,
                    Coy_State = i.Coy_State,
                    Coy_Status = i.Coy_Status,
                    Coy_AgentId = i.Coy_AgentId,
                    IsOrg = i.IsOrg,
                    Title = i.Title==null?"":i.Title,
                };
                result.Add(insuranceCoyDTO);
            }
            return result;
        }


        public async Task<InsuranceCoyDTO> GetById(int id)
        {
            InsuranceCoy i = await _context.InsuranceCompany.Where(m => m.Coy_Id == id).FirstOrDefaultAsync();
            InsuranceCoyDTO insuranceCoyDTO = new()
            {
                Coy_Name = i.Coy_Name,
                Coy_id = i.Coy_Id,
                Coy_Email = i.Coy_Email,
                Coy_Image = i.Coy_Image,
                Coy_Benefits = await _context.CoyBenefits.Where(c => c.Coy_id == i.Coy_Id).ToListAsync(),
                Coy_Logo = i.Coy_Logo,
                Coy_City = i.Coy_City,
                Coy_VideoLink = i.Coy_VideoLink,
                Coy_Description = i.Coy_Description,
                Coy_Country = i.Coy_Country,
                Coy_Phone = i.Coy_Phone,
                Coy_CityCode = i.Coy_CityCode,
                Coy_Street = i.Coy_Street,
                Coy_CountryCode = i.Coy_CountryCode,
                Coy_PostalCode = i.Coy_PostalCode,
                Coy_State = i.Coy_State,
                Coy_Status = i.Coy_Status,
                Coy_AgentId = i.Coy_AgentId,
                IsOrg = i.IsOrg,
                Title = i.Title,
                Coy_ZipCode = i.Coy_ZipCode
            };
            return insuranceCoyDTO;
        }


        public async Task<InsuranceCoyDTO> Add_Coy(InsuranceCoyDTO model)
        {
            InsuranceCoy i = new()
            {
                Coy_Email = model.Coy_Email,
                Coy_City = model.Coy_City,
                Coy_CityCode = model.Coy_CityCode,
                Coy_Country = model.Coy_Country,
                Coy_CountryCode = model.Coy_CountryCode,
                Coy_Description = model.Coy_Description,
                Coy_Street = model.Coy_Street,
                Coy_Name = model.Coy_Name,
                Coy_State = model.Coy_State,
                Coy_Image = model.Coy_Image,
                Coy_Logo = model.Coy_Logo,
                Coy_Phone = model.Coy_Phone,
                Coy_VideoLink = model.Coy_VideoLink,
                Coy_Status = model.Coy_Status,
                Coy_PostalCode = model.Coy_PostalCode,
                Coy_AgentId = model.Coy_AgentId,
                Title = model.Title,
                IsOrg = model.IsOrg,
                Coy_Id = model.Coy_id,
                Coy_ZipCode = model.Coy_ZipCode


            };

            await _context.InsuranceCompany.AddAsync(i);
            await _context.SaveChangesAsync();
            foreach (CoyBenefitEntity cb in model.Coy_Benefits)
            {
                cb.Coy_id = i.Coy_Id;
                _context.CoyBenefits.Add(cb);
                await _context.SaveChangesAsync();
            }
            return model;
        }


        public async Task<int> Update_Coy(InsuranceCoyDTO model)
        {
            InsuranceCoy insuranceCoy = new()
            {
                Coy_Id = model.Coy_id,
                Coy_Name = model.Coy_Name,
                Coy_Email = model.Coy_Email,
                Coy_City = model.Coy_City,
                Coy_CityCode = model.Coy_CityCode,
                Coy_Country = model.Coy_Country,
                Coy_CountryCode = model.Coy_CountryCode,
                Coy_Description = model.Coy_Description,
                Coy_Phone = model.Coy_Phone,
                Coy_PostalCode = model.Coy_PostalCode,
                Coy_State = model.Coy_State,
                Coy_Status = model.Coy_Status,
                Coy_Street = model.Coy_Street,
                Coy_ZipCode = model.Coy_ZipCode,
                Coy_AgentId = model.Coy_AgentId,
                IsOrg = model.IsOrg,
                Title = model.Title,
                Coy_Image = model.Coy_Image,
                Coy_Logo = model.Coy_Logo,
                Coy_VideoLink = model.Coy_VideoLink



            };
            InsuranceCoy coy = insuranceCoy;
            _context.InsuranceCompany.Update(coy);


            return await _context.SaveChangesAsync();
        }


        public async Task<InsuranceCoy> Delete_Coy(InsuranceCoyDTO model)
        {
            InsuranceCoy coy = new()
            {
                Coy_Id = model.Coy_id,
                Coy_Name = model.Coy_Name,
                Coy_Email = model.Coy_Email,
                Coy_City = model.Coy_City,
                Coy_AgentId = model.Coy_AgentId,

            };
            _context.InsuranceCompany.Remove(coy);
            await _context.SaveChangesAsync();
            return coy;
        }


        public async Task<bool> CoyIsExist(string coy_Name)
        {
            return await _context.InsuranceCompany.AnyAsync(b => b.Coy_Name == coy_Name);
        }







    }
}
