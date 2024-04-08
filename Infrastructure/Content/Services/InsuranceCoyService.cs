using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces.Content.Brands;
using Domain.Models;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class InsuranceCoyService:IInsuranceCoy
    {
        private readonly AppDbContext _context;

        public InsuranceCoyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<InsuranceCoy>> GetAll()
        {
            return await _context.InsuranceCompany.ToListAsync();
        }

         
        public async Task<InsuranceCoyDTO> GetById(int id)
        {
            InsuranceCoy i = await _context.InsuranceCompany.Where(m => m.Coy_Id == id).FirstOrDefaultAsync();
            InsuranceCoyDTO insuranceCoyDTO = new()
            {
                Coy_Name = i.Coy_Name,
                Coy_id = i.Coy_Id,
                Coy_Email = i.Coy_Email,
            };
            return insuranceCoyDTO;
        }


        public async Task<InsuranceCoy> Add_Coy(InsuranceCoy model)
        {
           await _context.InsuranceCompany.AddAsync(model);
            await _context.SaveChangesAsync();
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
