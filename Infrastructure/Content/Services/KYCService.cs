using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<KYCDTO> CreateKYC(KYCDTO kycDto)
        {
            kyc kycEntity = new()
            {
                IdentityType = kycDto.IdentityType,
                Name = kycDto.Name,
                FromExpiryDate = kycDto.FromExpiryDate,
                ToExpiryDate = kycDto.ToExpiryDate,
                IdentityNumber = kycDto.IdentityNumber,
                UserId = kycDto.UserId
                
            };

            _context.KYCs.Add(kycEntity);
            await _context.SaveChangesAsync();

            // Return the created KYC DTO with its ID populated
            return new KYCDTO
            {
                Id = kycEntity.Id,
                IdentityType = kycEntity.IdentityType,
                Name = kycEntity.Name,
                FromExpiryDate = kycEntity.FromExpiryDate,
                ToExpiryDate = kycEntity.ToExpiryDate,
                IdentityNumber = kycEntity.IdentityNumber
            };
        }

        public async Task<List<KYCDTO>> GetKYCByUserId(string userid)
        {
            List<KYCDTO> result = new();
            List<kyc> kentity = await _context.KYCs.Where(u => u.UserId == userid).ToListAsync();

            if (kentity == null)
            {
                return null;
            }
            foreach (kyc entity in kentity)
            {
                KYCDTO k = new()
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

            return result;

           
        }

        public async Task<KYCDTO> GetKYCById(int id)
        {
            var kycEntity = await _context.KYCs.FindAsync(id);

            if (kycEntity == null)
            {
                return null;
            }

            return new KYCDTO
            {
                Id = kycEntity.Id,
                IdentityType = kycEntity.IdentityType,
                Name = kycEntity.Name,
                FromExpiryDate = kycEntity.FromExpiryDate,
                ToExpiryDate = kycEntity.ToExpiryDate,
                IdentityNumber = kycEntity.IdentityNumber

            };
        }

        public async Task<IEnumerable<KYCDTO>> GetAllKYC()
        {
            var kycEntities = await _context.KYCs.ToListAsync();

            return kycEntities.Select(kycEntity => new KYCDTO
            {
                Id = kycEntity.Id,
                IdentityType = kycEntity.IdentityType,
                Name = kycEntity.Name,
                FromExpiryDate = kycEntity.FromExpiryDate,
                ToExpiryDate = kycEntity.ToExpiryDate,
                IdentityNumber = kycEntity.IdentityNumber
            }).ToList();
        }

        public async Task<KYCDTO> UpdateKYC(int id, KYCDTO kycDto)
        {
            kyc kycEntity = await _context.KYCs.Where(u=>u.UserId==kycDto.UserId && u.Id==id).FirstOrDefaultAsync();

            if (kycEntity == null)
            {
                return null;
            }

            kycEntity.IdentityType = kycDto.IdentityType;
            kycEntity.Name = kycDto.Name;
            kycEntity.FromExpiryDate = kycDto.FromExpiryDate;
            kycEntity.ToExpiryDate = kycDto.ToExpiryDate;
            kycEntity.IdentityNumber = kycDto.IdentityNumber;
            kycEntity.UserId = kycDto.UserId;

            await _context.SaveChangesAsync();

            return new KYCDTO
            {
                Id = kycEntity.Id,
                IdentityType = kycEntity.IdentityType,
                Name = kycEntity.Name,
                FromExpiryDate = kycEntity.FromExpiryDate,
                ToExpiryDate = kycEntity.ToExpiryDate,
                IdentityNumber = kycEntity.IdentityNumber,
            };
        }

        public async Task<bool> DeleteKYC(int id)
        {
            var kycEntity = await _context.KYCs.FindAsync(id);

            if (kycEntity == null)
            {
                return false;
            }

            _context.KYCs.Remove(kycEntity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}