using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Content.Services;

public class MotorClaimService : IMotorClaimRepository
{
    private readonly AppDbContext _context;

    public MotorClaimService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<MotorClaim>> CreateMotorClaim(MotorClaim motorClaim)
    {
        _context.MotorClaims.Add(motorClaim);
        await _context.SaveChangesAsync();
        return ApiResult<MotorClaim>.Successful(motorClaim);

    }

    public async Task<ApiResult<MotorClaim>> GetMotorClaimById(int id)
    {
        var motorClaim =  await _context.MotorClaims.FindAsync(id);
        return ApiResult<MotorClaim>.Successful(motorClaim);

    }

    public async Task<ApiResult<List<MotorClaim>>> GetAllMotorClaims(string userid)
    {
        var result = await _context.MotorClaims.Where(u=>u.UserId==userid).ToListAsync();
        return ApiResult<List<MotorClaim>>.Successful(result);

    }

    public async Task UpdateMotorClaim(MotorClaim motorClaim)
    {
        _context.Entry(motorClaim).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMotorClaim(int id)
    {
        var motorClaim = await _context.MotorClaims.FindAsync(id);
        if (motorClaim != null)
        {
            _context.MotorClaims.Remove(motorClaim);
            await _context.SaveChangesAsync();
        }
    }
}