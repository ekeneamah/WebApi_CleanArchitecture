using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Application.Dtos;

namespace Application.Interfaces.Content.UserProfiles
{
    public interface IUserProfile
    {
        Task<List<UserProfileDto>> GetAll();

        Task<UserProfileDto> GetProfilebyUserid(string id);

        Task<UserProfileDto> Add(UserProfileDto model);

        Task<UserProfileDto> Update_UserProfile(UserProfileDto model);

        Task<int> Delete_UserProfile(UserProfileDto model);

        Task<bool> UserProfileIsExist(string code);
        Task<bool> UserHasBVN(string userid);
        Task<bool> UserHasNIN(string userid);
    }
}
