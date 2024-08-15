using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Content.Claim
{
    public interface IClaim
    {
        Task<List<ClaimDetailDTO>> GetAll();
        Task<List<ClaimDetailDTO>> GetAllMyClaims(string userid);

        Task<ClaimDetailDTO> GetById(string ClaimsId);


        Task<ClaimsDto> AddClaims(ClaimsDto model);

        Task<int> AddClaimsForm(ClaimsForm model);
        Task<int> UpdateClaimsForm(ClaimsForm model);
        Task<ClaimsForm> GetClaimsForm(int policyid);



        Task<ClaimsDto> UpdateClaims(ClaimsDto model);
        Task<NotificationDTO> AddNotification(NotificationDTO tokenObject);
    }
}
