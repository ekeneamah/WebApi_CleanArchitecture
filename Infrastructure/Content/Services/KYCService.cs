using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Content.Data;
using Infrastructure.Identity.Services;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Content.Services
{
    public class KYCService : IKYC
    {
        private readonly AppDbContext _context;

        public KYCService(AppDbContext context )
        {
            _context = context;
        }

        public async Task<ApiResult<Kycdto>> CreateKYC(Kycdto kycDto)
        {
            if (kycDto.IdentityType != "NIN")
            {


                // Check if expiry date is greater than issued date
                if (kycDto.FromExpiryDate > kycDto.ToExpiryDate)
                {
                    return ApiResult<Kycdto>.Failed("Expiry date must be greater than issued date.");
                }

                // Check if expiry date has not passed
                if (DateTime.Today > kycDto.ToExpiryDate)
                {
                    return ApiResult<Kycdto>.Failed("Identity has expired.");
                }
                // Check if expiry date has not passed
                if (DateTime.Today < kycDto.FromExpiryDate)
                {
                    return ApiResult<Kycdto>.Failed("Identity has expired.");
                }
            }
            else if (DateTime.Today < kycDto.FromExpiryDate)
            {
                return ApiResult<Kycdto>.Failed("Date is invalid.");
            }
            Kyc kycEntity = new()
            {
                IdentityType = kycDto.IdentityType,
                Name = kycDto.Name,
                FromExpiryDate = kycDto.FromExpiryDate,
                ToExpiryDate = kycDto.ToExpiryDate,
                IdentityNumber = kycDto.IdentityNumber,
                UserId = kycDto.UserId
                
            };

            _context.Kycs.Add(kycEntity);
            await _context.SaveChangesAsync();

            // Return the created KYC DTO with its ID populated
            var result = new Kycdto
            {
                Id = kycEntity.Id,
                IdentityType = kycEntity.IdentityType,
                Name = kycEntity.Name,
                FromExpiryDate = kycEntity.FromExpiryDate,
                ToExpiryDate = kycEntity.ToExpiryDate,
                IdentityNumber = kycEntity.IdentityNumber
            };
            return ApiResult<Kycdto>.Successful(result);

        }

        public async Task<ApiResult<List<Kycdto>>> GetKYCByUserId(string userid)
        {
            List<Kycdto> result = new();
            List<Kyc> kentity = await _context.Kycs.Where(u => u.UserId == userid).ToListAsync();

            if (kentity == null)
            {
                return ApiResult<List<Kycdto>>.NotFound("KYC not found");
            }
            foreach (Kyc entity in kentity)
            {
                Kycdto k = new()
                {
                    Id = entity.Id,
                    IdentityType = entity.IdentityType,
                    Name = entity.Name,
                    FromExpiryDate = entity.FromExpiryDate,
                    ToExpiryDate = entity.ToExpiryDate,
                    IdentityNumber = entity.IdentityNumber

                }; 
                result.Add(k);
            }
            
            return ApiResult<List<Kycdto>>.Successful(result);


           
        }

        public async Task<ApiResult<Kycdto>> GetKYCById(int id)
        {
            var kycEntity = await _context.Kycs.FindAsync(id);

            if (kycEntity == null)
            {
                return ApiResult<Kycdto>.NotFound();

            }

            var result =  new Kycdto
            {
                Id = kycEntity.Id,
                IdentityType = kycEntity.IdentityType,
                Name = kycEntity.Name,
                FromExpiryDate = kycEntity.FromExpiryDate,
                ToExpiryDate = kycEntity.ToExpiryDate,
                IdentityNumber = kycEntity.IdentityNumber

            };
            return ApiResult<Kycdto>.Successful(result);

        }

        public async Task<ApiResult<List<Kycdto>>> GetAllKYC()
        {
            var kycEntities = await _context.Kycs.ToListAsync();

            var result = kycEntities.Select(kycEntity => new Kycdto
            {
                Id = kycEntity.Id,
                IdentityType = kycEntity.IdentityType,
                Name = kycEntity.Name,
                FromExpiryDate = kycEntity.FromExpiryDate,
                ToExpiryDate = kycEntity.ToExpiryDate,
                IdentityNumber = kycEntity.IdentityNumber
            }).ToList();
            return ApiResult<List<Kycdto>>.Successful(result);

        }

        public async Task<ApiResult<Kycdto>> UpdateKYC(int id, Kycdto kycDto)
        {
            if (id != kycDto.Id)
            {
                return ApiResult<Kycdto>.Failed("Invalid KYC ID.");
            }
            if (kycDto.IdentityType != "NIN")
            {


                // Check if expiry date is greater than issued date
                if (kycDto.FromExpiryDate > kycDto.ToExpiryDate)
                {
                    return ApiResult<Kycdto>.Failed("Expiry date must be greater than issued date.");
                }

                // Check if expiry date has not passed
                if (DateTime.Today > kycDto.ToExpiryDate)
                {
                    return ApiResult<Kycdto>.Failed("Identity has expired.");
                }
               
            }
            else if (DateTime.Today < kycDto.FromExpiryDate)
            {
                return ApiResult<Kycdto>.Failed("Date is invalid.");
            }
            
            Kyc kycEntity = await _context.Kycs.Where(u=>u.UserId==kycDto.UserId && u.Id==id).FirstOrDefaultAsync();

            if (kycEntity == null)
            {
                return ApiResult<Kycdto>.NotFound();
            }

            kycEntity.IdentityType = kycDto.IdentityType;
            kycEntity.Name = kycDto.Name;
            kycEntity.FromExpiryDate = kycDto.FromExpiryDate;
            kycEntity.ToExpiryDate = kycDto.ToExpiryDate;
            kycEntity.IdentityNumber = kycDto.IdentityNumber;
            kycEntity.UserId = kycDto.UserId;

            await _context.SaveChangesAsync();

            var result =  new Kycdto
            {
                Id = kycEntity.Id,
                IdentityType = kycEntity.IdentityType,
                Name = kycEntity.Name,
                FromExpiryDate = kycEntity.FromExpiryDate,
                ToExpiryDate = kycEntity.ToExpiryDate,
                IdentityNumber = kycEntity.IdentityNumber,
            };
            return ApiResult<Kycdto>.Successful(result);

        }

        public async Task<bool> DeleteKYC(int id)
        {
            var kycEntity = await _context.Kycs.FindAsync(id);

            if (kycEntity == null)
            {
                return false;
            }

            _context.Kycs.Remove(kycEntity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}