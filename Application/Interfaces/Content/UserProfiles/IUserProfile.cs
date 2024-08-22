using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Dtos;

namespace Application.Interfaces.Content.UserProfiles
{
    public interface IUserProfile
    {
        Task<ApiResult<List<UserProfileDto>>> GetAll();

        Task<ApiResult<UserProfileDto>> GetProfilebyUserid(string id);

        Task<ApiResult<UserProfileDto>> UpdateUser(UserProfileDto model);

        Task<ApiResult<UserProfileDto>> Update_UserProfile(UserProfileDto model);
        Task<Boolean> Update_UserProfilePix(string profilePix, string id);

        Task<int> Delete_UserProfile(UserProfileDto model);

        Task<bool> UserProfileIsExist(string code);
        Task<bool> UserHasBVN(string userid);
        Task<bool> UserHasNIN(string userid);
    }
}
