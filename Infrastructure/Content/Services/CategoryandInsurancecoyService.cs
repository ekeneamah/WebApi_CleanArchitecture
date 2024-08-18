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

      
        public ApiResult<CategoryandInsurancecoy> GetById(int id)
        {
            var result = _dbContext.CategoryandInsurancecoys.FirstOrDefault(x => x.Id == id);
            if (result is null)
                return ApiResult<CategoryandInsurancecoy>.NotFound();
                
            return ApiResult<CategoryandInsurancecoy>.Successful(result);

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
                    InsuranceCoy = (await _insuranceCoyService.GetById(item.InsuranceCoyId)).Data,
                    Id = item.Id
                };
                result.Add(cd);
            }
            return ApiResult<List<CategoryandInsurancecoyDto>>.Successful(result);
        }

        public async Task<ApiResult<List<CategoryandInsurancecoyDto>>> GetByInsuranceCoyId(int insuranceCoyId)
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
                    InsuranceCoy = (await _insuranceCoyService.GetById(item.InsuranceCoyId)).Data,
                    Id = item.Id
                };
                result.Add(cd);
            }
            return ApiResult<List<CategoryandInsurancecoyDto>>.Successful(result);

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
