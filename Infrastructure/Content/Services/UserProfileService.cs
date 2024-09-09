﻿using Application.Dtos;
using Application.Interfaces.Content.UserProfiles;
using Application.POCO;
using Infrastructure.Content.Data;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

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

        public async Task<ApiResult<UserProfileDto>> UpdateUser(UserProfileDto u)
        {
            using var scope = await _context.Database.BeginTransactionAsync();

            if (u.UserId == null)
                return ApiResult<UserProfileDto>.Failed("Invalid User");
            
            var user = await _userManager.FindByIdAsync(u.UserId);
            
            if (user == null)
                return ApiResult<UserProfileDto>.Failed("Invalid User");
            
            AppUser au = new()
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                BusinessLocation = u.BusinessLocation,
                BVN = u.Bvn,
                City = u.City,
                Country = u.Country,
                DateofBirth = u.DateOfBirth,
                Gender = u.Gender,
                Maidenname = u.MaidenName,
                MaritalStatus = u.MaritalStatus,
                NIN = u.Nin,
                Phone = u.Phone,
                ResidentialAddress = u.ResidentialAddress,
                Stateoforigin = u.StateOfOrigin,
                PostalCode = u.PostalCode,
                Town = u.Town,
                SignatureUrl = u.SignatureUrl
            };
            await _userManager.UpdateAsync(au);

            if (user.UserName != u.UserName)
            {
                var result = await _userManager.SetUserNameAsync(user, u.UserName);
                if (!result.Succeeded)
                {
                    var errorMessages = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description));

                    return ApiResult<UserProfileDto>.Failed(errorMessages);
                }

            }
            await _context.SaveChangesAsync();
            await scope.CommitAsync();
            
           
            return ApiResult<UserProfileDto>.Successful(u);
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

        public async Task<ApiResult<List<UserProfileDto>>> GetAll()
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
                    Bvn = u.BVN,
                    City = u.City,
                    Country = u.Country,
                    DateOfBirth = u.DateofBirth,
                    Gender = u.Gender,
                    MaidenName = u.Maidenname,
                    MaritalStatus = u.MaritalStatus,
                    Nin = u.NIN,
                    Phone = u.Phone,
                    ResidentialAddress = u.ResidentialAddress,
                    StateOfOrigin = u.Stateoforigin,
                    PostalCode = u.PostalCode,
                    Town = u.Town,
                    SignatureUrl = u.SignatureUrl
                };

                result.Add(au);
                
            }
            return ApiResult<List<UserProfileDto>>.Successful(result);
        }

        public async Task<ApiResult<UserProfileDto>> GetProfilebyUserid(string id)
        {
            
            var u = await _userManager.FindByIdAsync(id);
            if (u == null)
            {
                return ApiResult<UserProfileDto>.Failed("User Not Found");
            }
            UserProfileDto au = new()
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                BusinessLocation = u.BusinessLocation,
                Bvn = u.BVN,
                City = u.City,
                Country = u.Country,
                DateOfBirth = u.DateofBirth,
                Gender = u.Gender,
                MaidenName = u.Maidenname,
                MaritalStatus = u.MaritalStatus,
                Nin = u.NIN,
                Phone = u.Phone,
                ResidentialAddress = u.ResidentialAddress,
                StateOfOrigin = u.Stateoforigin,
                PostalCode = u.PostalCode,
                Town = u.Town,
                SignatureUrl = u.SignatureUrl,
                UserId = u.Id,
                UserName = u.UserName,
                ResidentPerminNo = u.ResidentPerminNo,
                Region = u.Region
                
            };

            return ApiResult<UserProfileDto>.Successful(au);
        }

        public async Task<ApiResult<UserProfileDto>> Update_UserProfile(UserProfileDto model)
        {
            _userProfilePoco = new UserProfilePoco();
            UserProfile userProfile = await _context.UserProfiles.Where(u=>u.UserId==model.UserId).SingleOrDefaultAsync();
            userProfile = _userProfilePoco.UserProfilePocoDto(model);
            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
            return ApiResult<UserProfileDto>.Successful(model);
        }

        public async Task<Boolean> Update_UserProfilePix(string profilePix, string id)
        {
           AppUser uprofile = await _userManager.Users.FirstOrDefaultAsync();
            if (uprofile != null)
            {
                uprofile.ProfilePix = profilePix;
            }
            IdentityResult i = await _userManager.UpdateAsync(uprofile);
            return i.Succeeded;
        }

        public async Task<bool> UserHasBVN(string userid)
        {
            string bvn = await _context.UserProfiles.Where(u => u.UserId == userid).Select(b => b.Bvn).FirstOrDefaultAsync();
            return !string.IsNullOrEmpty(bvn);
        }

        public async Task<bool> UserHasNIN(string userid)
        {
            string nin = await _context.UserProfiles.Where(u => u.UserId == userid).Select(b => b.Nin).FirstOrDefaultAsync();
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
