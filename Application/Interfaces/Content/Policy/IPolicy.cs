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
        Task<List<PolicyDetailDTO>> GetAll();


        Task<PolicyDetailDTO> GetById(int id);
        Task<List<PolicyDetailDTO>> GetAllPolicyByUserId(string userid);
        Task<List<PolicyDetailDTO>> GetByUserName(string username);
        Task<string> GeneratePolicyNumber(GeneratePolicyDTO ret );


        Task<int> AddPolicy(Dtos.TransactionDTO model);



        PolicyDetailDTO UpdatePolicy(Dtos.TransactionDTO model);



        Dtos.TransactionDTO DeletePolicy(Dtos.TransactionDTO model);



        Task<bool> POlicyIsExist(string policyName);

    }
}
