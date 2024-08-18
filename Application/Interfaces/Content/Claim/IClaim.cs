using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace Application.Interfaces.Content.Claim
{
    public interface IClaim
    {
        Task<ApiResult<List<ClaimDetailDto>>> GetAll();
        Task<ApiResult<List<ClaimDetailDto>>> GetAllMyClaims(string userid);

        Task<ApiResult<ClaimDetailDto>> GetById(string ClaimsId);


        Task<ApiResult<ClaimsDto>> AddClaims(ClaimsDto model);

        Task<int> AddClaimsForm(ClaimsForm model);
        Task<int> UpdateClaimsForm(ClaimsForm model);
        Task<ApiResult<ClaimsForm>> GetClaimsForm(int policyid);



        Task<ApiResult<ClaimsDto>> UpdateClaims(ClaimsDto model);
        Task<ApiResult<NotificationDto>> AddNotification(NotificationDto tokenObject);
    }
}
