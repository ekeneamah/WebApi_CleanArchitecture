using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Content.UserProfiles
{
    public interface IUserProfileRepo
    {
        Task<List<UserProfileDto>> GetAll();

        Task<UserProfileDto> GetById(string userid);

        Task<UserProfileDto> Add_UserProfile(UserProfileDto model);

        Task<UserProfileDto> Update_UserProfile(UserProfileDto model);

        Task<int> Delete_UserProfile(UserProfileDto model);

        Task<bool> UserProfileIsExist(string code);
        Task<bool> UserHasBVN(string userid);
        Task<bool> UserHasNIN(string userid);
    }
}
