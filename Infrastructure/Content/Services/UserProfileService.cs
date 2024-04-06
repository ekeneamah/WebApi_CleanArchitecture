using Application.Dtos;
using Application.Interfaces.Content.UserProfiles;
using Application.POCO;
using Domain.Models;
using Infrastructure.Content.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
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
        private  UserProfilePoco _userProfilePoco;
        private UserManager<AppUser> _userManager;

        public UserProfileService(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<UserProfileDto> Add(UserProfileDto u)
        {
            _userProfilePoco = new UserProfilePoco();
            UserProfile model = _userProfilePoco.UserProfilePocoDto(u);
            _context.UserProfiles.Add(model);
            await _context.SaveChangesAsync();
            return u;
        }

        public async Task<int> Delete_UserProfile(UserProfileDto m)
        {
            UserProfile model = _context.UserProfiles.FirstOrDefault(p => p.UserId == m.UserId);
            if (model != null)
            {
                _context.UserProfiles.Remove(model);
                await _context.SaveChangesAsync();
                return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<List<UserProfileDto>> GetAll()
        {
            List<UserProfileDto> result = new();
            List<UserProfile> uprofile =  await _context.UserProfiles.ToListAsync();
            _userProfilePoco = new UserProfilePoco();
            foreach (UserProfile u in uprofile)
            {
                
                result.Add(_userProfilePoco.UserProfilePocoModel(u));
                
            }
            return result;
        }

        public async Task<UserProfileDto> GetById(string id)
        {
            _userProfilePoco = new UserProfilePoco();
            UserProfile u = await _context.UserProfiles.FirstOrDefaultAsync(u => u.UserId == id);
            AppUser user = await _userManager.FindByIdAsync(id);
            if (u == null || user ==null)
                return _userProfilePoco.UserProfilePocoModelNull();
            return _userProfilePoco.UserProfilePocoModel(u);
        }

        public async Task<UserProfileDto> Update_UserProfile(UserProfileDto model)
        {
            _userProfilePoco = new UserProfilePoco();
            UserProfile userProfile = await _context.UserProfiles.Where(u=>u.UserId==model.UserId).SingleOrDefaultAsync();
            userProfile = _userProfilePoco.UserProfilePocoDto(model);
            _context.UserProfiles.Update(userProfile);
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

        
    }
}
