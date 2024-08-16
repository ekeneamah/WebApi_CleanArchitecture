using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Content.Brands;
using Domain.Entities;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Infrastructure.Content.Services
{
    public class CategoryandInsurancecoyService : ICategoryandInsurancecoy
    {
        private readonly AppDbContext _dbContext; 
        private readonly IInsuranceCoy _insuranceCoyService;

        public CategoryandInsurancecoyService(AppDbContext dbContext, IInsuranceCoy insuranceCoyService)
        {
            _dbContext = dbContext;
            _insuranceCoyService = insuranceCoyService;
        }

        public void Add(CategoryandInsurancecoy item)
        {
            _dbContext.CategoryandInsurancecoys.Add(item);
            _dbContext.SaveChanges();
        }

      
        public CategoryandInsurancecoy GetById(int id)
        {
            return _dbContext.CategoryandInsurancecoys.FirstOrDefault(x => x.Id == id);
        }

        public ApiResult<List<CategoryandInsurancecoy>> GetAll()
        {
            var result =  _dbContext.CategoryandInsurancecoys.ToList();
            return ApiResult<List<CategoryandInsurancecoy>>.Successful(result);

        }


        public async Task<ApiResult<List<CategoryandInsurancecoyDto>>> GetByCategoryId(int categoryId)
        {
            List<CategoryandInsurancecoyDto> result = new();

            List<CategoryandInsurancecoy> ci =  await _dbContext.CategoryandInsurancecoys.Where(x => x.CategoryId == categoryId).ToListAsync();
            foreach (CategoryandInsurancecoy item in ci)
            {

                CategoryandInsurancecoyDto cd = new()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    InsuranceCoyId = item.InsuranceCoyId,
                    InsuranceCoy = await _insuranceCoyService.GetById(item.InsuranceCoyId),
                    Id = item.Id
                };
                result.Add(cd);
            }
            return ApiResult<List<CategoryandInsurancecoyDto>>.Successful(result);
        }

        public async Task<List<CategoryandInsurancecoyDto>> GetByInsuranceCoyId(int insuranceCoyId)
        {
            List<CategoryandInsurancecoyDto> result = new();

            List<CategoryandInsurancecoy> ci = await _dbContext.CategoryandInsurancecoys.Where(x => x.InsuranceCoyId == insuranceCoyId).ToListAsync();
            foreach (CategoryandInsurancecoy item in ci)
            {

                CategoryandInsurancecoyDto cd = new()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    InsuranceCoyId = item.InsuranceCoyId,
                    InsuranceCoy = await _insuranceCoyService.GetById(item.InsuranceCoyId),
                    Id = item.Id
                };
                result.Add(cd);
            }
            return result;
        }

        // Update operation
        public void Update(CategoryandInsurancecoy item)
        {
            _dbContext.CategoryandInsurancecoys.Update(item);
            _dbContext.SaveChanges();
        }

        // Delete operation
        public void Delete(int id)
        {
            var itemToDelete = _dbContext.CategoryandInsurancecoys.FirstOrDefault(x => x.Id == id);
            if (itemToDelete != null)
            {
                _dbContext.CategoryandInsurancecoys.Remove(itemToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
