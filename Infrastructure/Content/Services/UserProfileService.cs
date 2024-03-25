using Application.Interfaces.Content.UserProfiles;
using Domain.Models;
using Infrastructure.Content.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Content.Services
{
    public class UserProfileService : IUserProfile
    {
        private readonly AppDbContext _context;

        public UserProfileService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserProfile> Add(UserProfile model)
        {
            _context.UserProfiles.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<UserProfile> Delete_UserProfile(UserProfile model)
        {
            _context.UserProfiles.Remove(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<UserProfile>> GetAll()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile> GetById(string id)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<UserProfile> Update_UserProfile(UserProfile model)
        {
            _context.UserProfiles.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UserHasBVN(string userid)
        {
            string bvn = await _context.UserProfiles.Where(u => u.UserId == userid).Select(b => b.BVN).FirstOrDefaultAsync();
            return !string.IsNullOrEmpty(bvn);
        }

        public async Task<bool> UserHasNIN(string userid)
        {
            string nin = await _context.UserProfiles.Where(u => u.UserId == userid).Select(b => b.NIN).FirstOrDefaultAsync();
            return !string.IsNullOrEmpty(nin);
        }

        public async Task<bool> UserProfileIsExist(string userid)
        {
            return await _context.UserProfiles.AnyAsync(p => p.UserId == userid);
        }

        UserProfile IUserProfile.Delete_UserProfile(UserProfile model)
        {
            throw new NotImplementedException();
        }
    }
}
