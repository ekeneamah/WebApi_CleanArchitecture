using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;

namespace Application.Interfaces.Content.Policy
{
    public interface IPolicy
    {
        Task<ApiResult<List<PolicyDetailDto>>> GetAll();


        Task<ApiResult<PolicyDetailDto>> GetById(int id);
        Task<ApiResult<List<PolicyDetailDto>>> GetAllPolicyByUserId(string userid);
        Task<ApiResult<List<PolicyDetailDto>>> GetByUserName(string username);
        Task<ApiResult<string>> GeneratePolicyNumber(GeneratePolicyDto ret );


        Task<int> AddPolicy(Dtos.CreatePolicyDto model);



        PolicyDetailDto UpdatePolicy(Dtos.CreatePolicyDto model);



        Dtos.CreatePolicyDto DeletePolicy(Dtos.CreatePolicyDto model);



        Task<bool> POlicyIsExist(string policyName);

    }
}
