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

    public async Task<MotorClaim> CreateMotorClaim(MotorClaim motorClaim)
    {
        _context.MotorClaims.Add(motorClaim);
        await _context.SaveChangesAsync();
        return motorClaim;
    }

    public async Task<MotorClaim> GetMotorClaimById(int id)
    {
        return await _context.MotorClaims.FindAsync(id);
    }

    public async Task<IEnumerable<MotorClaim>> GetAllMotorClaims(string userid)
    {
        return await _context.MotorClaims.Where(u=>u.User_Id==userid).ToListAsync();
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