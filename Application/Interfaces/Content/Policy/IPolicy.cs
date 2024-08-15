using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Content.Policy
{
    public interface IPolicy
    {
        Task<List<PolicyDetailDto>> GetAll();


        Task<PolicyDetailDto> GetById(int id);
        Task<List<PolicyDetailDto>> GetAllPolicyByUserId(string userid);
        Task<List<PolicyDetailDto>> GetByUserName(string username);
        Task<string> GeneratePolicyNumber(GeneratePolicyDto ret );


        Task<int> AddPolicy(Dtos.TransactionDto model);



        PolicyDetailDto UpdatePolicy(Dtos.TransactionDto model);



        Dtos.TransactionDto DeletePolicy(Dtos.TransactionDto model);



        Task<bool> POlicyIsExist(string policyName);

    }
}
