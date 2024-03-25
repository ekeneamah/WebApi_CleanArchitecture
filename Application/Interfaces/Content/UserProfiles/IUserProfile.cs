using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.Content.UserProfiles
{
    public interface IUserProfile
    {
        Task<List<UserProfile>> GetAll();

        Task<UserProfile> GetById(string id);

        Task<UserProfile> Add(UserProfile model);

        Task<UserProfile> Update_UserProfile(UserProfile model);

        UserProfile Delete_UserProfile(UserProfile model);

        Task<bool> UserProfileIsExist(string code);
        Task<bool> UserHasBVN(string userid);
        Task<bool> UserHasNIN(string userid);
    }
}
