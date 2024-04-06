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
            return await _context.InsuranceCoys.ToListAsync();
        }

         
        public async Task<InsuranceCoyDTO> GetById(int id)
        {
            InsuranceCoy i = await _context.InsuranceCoys.Where(m => m.Coy_Id == id).FirstOrDefaultAsync();
            InsuranceCoyDTO insuranceCoyDTO = new()
            {
                Coy_Name = i.Coy_Name,
                Coy_id = i.Coy_Id,
            };
            return insuranceCoyDTO;
        }


        public async Task<InsuranceCoy> Add_Coy(InsuranceCoy model)
        {
           await _context.InsuranceCoys.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<int> Update_Coy(InsuranceCoyDTO model)
        {
            InsuranceCoy coy = new()
            {
                Coy_Id = model.Coy_id,
                Coy_Name = model.Coy_Name,
            };
            _context.InsuranceCoys.Update(coy);

            return await _context.SaveChangesAsync();
        }


        public async Task<InsuranceCoy> Delete_Coy(InsuranceCoyDTO model)
        {
            InsuranceCoy coy = new()
            {
                Coy_Id = model.Coy_id,
                Coy_Name = model.Coy_Name,
            };
            _context.InsuranceCoys.Remove(coy);
            await _context.SaveChangesAsync();
            return coy;
        }


        public async Task<bool> CoyIsExist(string coy_Name)
        {
            return await _context.InsuranceCoys.AnyAsync(b => b.Coy_Name == coy_Name);
        }







    }
}
