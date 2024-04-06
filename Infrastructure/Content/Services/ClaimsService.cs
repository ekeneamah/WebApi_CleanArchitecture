using Application.Dtos;
using Application.Interfaces.Content.Claim;
using Domain.Models;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Content.Services
{
    public class ClaimService : IClaim
    {
        private readonly AppDbContext _dbContext;

        public ClaimService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ClaimsDto>> GetAll()
        {
            return await _dbContext.Claims
                .Select(c => new ClaimsDto
                {
                    PolicyId = c.ClaimsId,
                    PolicyNo = c.PolicyNo,
                    LossDate = c.LossDate,
                    NotifyDate = c.NotifyDate,
                    ClaimForm = c.ClaimForm,
                    Reference = c.Reference,
                    InsuranceCompanyId = c.InsuranceCompanyId,
                })
                .ToListAsync();
        }

        public async Task<ClaimsDto> GetById(string claimId)
        {
            var claim = await _dbContext.Claims.FindAsync(claimId);
            if (claim == null)
            {
                return null;
            }

            return new ClaimsDto
            {
                PolicyId = claim.ClaimsId,
                PolicyNo = claim.PolicyNo,
                LossDate = claim.LossDate,
                NotifyDate = claim.NotifyDate,
                ClaimForm = claim.ClaimForm,
                Reference = claim.Reference,
                InsuranceCompanyId= claim.InsuranceCompanyId,
            };
        }

        public async Task<ClaimsDto> AddClaims(ClaimsDto model)
        {
            var claim = new Claim
            {
                ClaimsId = Guid.NewGuid(), // Generating a new Guid for ClaimsId
                UserId = model.UserId,
                PolicyNo = model.PolicyNo,
                LossDate = model.LossDate,
                NotifyDate = model.NotifyDate,
                ClaimForm = model.ClaimForm,
                Reference = model.Reference,
                InsuranceCompanyId = model.InsuranceCompanyId,
            };

            _dbContext.Claims.Add(claim);
            await _dbContext.SaveChangesAsync();

            return model;
        }
        #region get claims form by insuarance coy
        public async Task<ClaimsForm> GetClaimsForm(int PolicyId)
        { int InsuranceCompanyId = await _dbContext.Policies.Where(i => i.Id == PolicyId).Select(c => c.Coy_Id).FirstOrDefaultAsync();
            return await _dbContext.ClaimsForms.FirstOrDefaultAsync(p => p.Coy_id == InsuranceCompanyId);
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
            var claim = await _dbContext.Claims.FirstOrDefaultAsync(c => c.ClaimsId == model.PolicyId);
            if (claim == null)
            {
                return null; // Claim not found
            }

            // Update claim properties
            claim.UserId = model.UserId;
            claim.PolicyNo = model.PolicyNo;
            claim.LossDate = model.LossDate;
            claim.NotifyDate = model.NotifyDate;
            claim.ClaimForm = model.ClaimForm;
            claim.Reference = model.Reference;
            _dbContext.Update(claim);
            await _dbContext.SaveChangesAsync();

            return model;
        }
    }
}
