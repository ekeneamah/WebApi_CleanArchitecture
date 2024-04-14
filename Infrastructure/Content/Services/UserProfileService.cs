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
           var user = await _userManager.FindByIdAsync(u.UserId);
            AppUser au = new()
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
               
                BusinessLocation = u.BusinessLocation,
                BVN = u.BVN,
                City = u.City,
                Country = u.Country,
                DateofBirth = u.DateofBirth,
                Gender = u.Gender,
                Maidenname = u.Maidenname,
                MaritalStatus = u.MaritalStatus,
                NIN = u.NIN,
                Phone = u.Phone,
                ResidentialAddress = u.ResidentialAddress,
                Stateoforigin = u.Stateoforigin,
                PostalCode = u.PostalCode,
                Town = u.Town,
                SignatureUrl = u.SignatureUrl
            };
            if (user != null)
            {
                _userManager.UpdateAsync(au);
                await _context.SaveChangesAsync();
            } 
           
            return u;
        }

        public async Task<int> Delete_UserProfile(UserProfileDto m)
        {
            UserProfileEntity model = _context.UserProfiles.FirstOrDefault(p => p.UserId == m.UserId);
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
            List<AppUser> uprofile =  await _userManager.Users.ToListAsync();
           
            foreach (AppUser u in uprofile)
            {
                UserProfileDto au = new()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserId = u.Id,
                    BusinessLocation = u.BusinessLocation,
                    BVN = u.BVN,
                    City = u.City,
                    Country = u.Country,
                    DateofBirth = u.DateofBirth,
                    Gender = u.Gender,
                    Maidenname = u.Maidenname,
                    MaritalStatus = u.MaritalStatus,
                    NIN = u.NIN,
                    Phone = u.Phone,
                    ResidentialAddress = u.ResidentialAddress,
                    Stateoforigin = u.Stateoforigin,
                    PostalCode = u.PostalCode,
                    Town = u.Town,
                    SignatureUrl = u.SignatureUrl
                };

                result.Add(au);
                
            }
            return result;
        }

        public async Task<UserProfileDto> GetProfilebyUserid(string id)
        {
            
            AppUser u = await _userManager.FindByIdAsync(id);
            UserProfileDto au = new()
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                BusinessLocation = u.BusinessLocation,
                BVN = u.BVN,
                City = u.City,
                Country = u.Country,
                DateofBirth = u.DateofBirth,
                Gender = u.Gender,
                Maidenname = u.Maidenname,
                MaritalStatus = u.MaritalStatus,
                NIN = u.NIN,
                Phone = u.Phone,
                ResidentialAddress = u.ResidentialAddress,
                Stateoforigin = u.Stateoforigin,
                PostalCode = u.PostalCode,
                Town = u.Town,
                SignatureUrl = u.SignatureUrl
            };

            return au;
        }

        public async Task<UserProfileDto> Update_UserProfile(UserProfileDto model)
        {
            _userProfilePoco = new UserProfilePoco();
            UserProfileEntity userProfile = await _context.UserProfiles.Where(u=>u.UserId==model.UserId).SingleOrDefaultAsync();
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

        internal Task<UserProfileDto> GetProfilebyUserid(object userid)
        {
            throw new NotImplementedException();
        }
    }
}
