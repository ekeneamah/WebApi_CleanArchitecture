using Application.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Content.Claim
{
    public interface IClaim
    {
        Task<List<ClaimsDto>> GetAll();


        Task<ClaimsDto> GetById(string ClaimsId);


        Task<ClaimsDto> AddClaims(ClaimsDto model);

        Task<int> AddClaimsForm(ClaimsFormEntity model);
        Task<int> UpdateClaimsForm(ClaimsFormEntity model);
        Task<ClaimsFormEntity> GetClaimsForm(int policyid);



        Task<ClaimsDto> UpdateClaims(ClaimsDto model);
    }
}
