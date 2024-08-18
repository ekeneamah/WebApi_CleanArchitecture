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
        Task<string> GeneratePolicyNumber(GeneratePolicyDto ret );


        Task<int> AddPolicy(Dtos.TransactionDto model);



        PolicyDetailDto UpdatePolicy(Dtos.TransactionDto model);



        Dtos.TransactionDto DeletePolicy(Dtos.TransactionDto model);



        Task<bool> POlicyIsExist(string policyName);

    }
}
