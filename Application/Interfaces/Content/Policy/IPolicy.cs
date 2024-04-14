using Application.Dtos;
using Domain.Entities;
using Domain.Models;
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


        Task<int> AddPolicy(PolicyDTO model);



        PolicyDetailDTO UpdatePolicy(PolicyDTO model);



        PolicyDTO DeletePolicy(PolicyDTO model);



        Task<bool> POlicyIsExist(string policyName);

        Task<PolicyGenReturnedData> GeneratePolicyNumber(GeneratePolicyDTO generatePolicyDTO);
    }
}
