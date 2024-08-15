using Application.Dtos;
using Application.Interfaces.Content.Claim;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Content.Services
{
    public class ClaimService : IClaim
    {
        private readonly AppDbContext _dbContext;

        public ClaimService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ClaimDetailDto>> GetAll()
        {
            return await _dbContext.Claims
                .Select(c => new ClaimDetailDto
                {
                    
                    PolicyNo = c.PolicyNo,
                    LossDate = c.LossDate.ToString(),
                    NotifyDate = c.NotifyDate.ToString(),
                   
                    Reference = c.Reference,
                   
                })
                .ToListAsync();
        }

        public async Task<List<ClaimDetailDto>> GetAllMyClaims(string userid)
        {
            return await _dbContext.Claims.Where(u=>u.UserId==userid)
                .Select(c => new ClaimDetailDto
                {

                    PolicyNo = c.PolicyNo,
                    LossDate = c.LossDate.ToString(),
                    NotifyDate = c.NotifyDate.ToString(),
                    NotificationNo = c.NotificationNo,
                    Status = c.Status,
                    ClaimNo = c.ClaimNo,
                    ClaimsId = c.ClaimsId,
                    Reference = c.Reference,

                })
                .ToListAsync();
        }

        public async Task<ClaimDetailDto> GetById(string claimId)
        {
            var claim = await _dbContext.Claims.FindAsync(claimId);
            if (claim == null)
            {
                return null;
            }

            return new ClaimDetailDto
            {
                PolicyNo = claim.PolicyNo,
                LossDate = claim.LossDate.ToString(),
                NotifyDate = claim.NotifyDate.ToString(),
                Reference = claim.Reference,
                ClaimNo = claim.ClaimNo,
                ClaimsId = claim.ClaimsId,
                NotificationNo = claim.NotificationNo,
                Status = claim.Status
               
            };
        }

        public async Task<ClaimsDto> AddClaims(ClaimsDto model)
        {
            var claim = new Claim
            {
                ClaimsId = Guid.NewGuid(), // Generating a new Guid for ClaimsId
                UserId = model.UserId,
                PolicyNo = model.PolicyNo,
                LossDate = model.LossDate.ToString(),
                NotifyDate = model.NotifyDate.ToString(),
                Reference = model.Reference,
            };

            _dbContext.Claims.Add(claim);
            await _dbContext.SaveChangesAsync();
            model.ClaimId = claim.ClaimsId;
            return model;
        }
        #region get claims form by insuarance coy
        public async Task<ClaimsForm> GetClaimsForm(int PolicyId)
        { int InsuranceCompanyId = await _dbContext.Policies.Where(i => i.Id == PolicyId).Select(c => c.CoyId).FirstOrDefaultAsync();
            return await _dbContext.ClaimsForms.FirstOrDefaultAsync(p => p.CoyId == InsuranceCompanyId);
        }

        #endregion

        #region Add Claims form by insuarance coy
        public async Task<int> AddClaimsForm(ClaimsForm model)
        {
            _dbContext.ClaimsForms.Add(model);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

        #region update Claims form by insuarance coy
        public async Task<int> UpdateClaimsForm(ClaimsForm model)
        {
            ClaimsForm claimForm = await _dbContext.ClaimsForms.FindAsync(model.Id);
            claimForm = model;
            _dbContext.ClaimsForms.Update(claimForm);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion
       

        public async Task<ClaimsDto> UpdateClaims(ClaimsDto model)
        {
            var claim = await _dbContext.Claims.FirstOrDefaultAsync(c => c.PolicyNo == model.PolicyNo);
            if (claim == null)
            {
                return null; // ClaimEntity not found
            }

            // Update claim properties
            claim.UserId = model.UserId;
            claim.PolicyNo = model.PolicyNo;
            claim.LossDate = model.LossDate.ToString();
            claim.NotifyDate = model.NotifyDate.ToString();
            claim.Reference = model.Reference;
            _dbContext.Update(claim);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<NotificationDto> AddNotification(NotificationDto model)
        {
            var claim = await _dbContext.Claims.FirstOrDefaultAsync(c => c.ClaimsId == model.ClaimsId);
            if (claim == null)
            {
                return null; // ClaimEntity not found
            }

            // Update claim properties
            claim.Status = model.Status;
            claim.ClaimNo = model.ClaimNo;  
            claim.NotificationNo = model.NotificationNo;
            _dbContext.Update(claim);
            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}
