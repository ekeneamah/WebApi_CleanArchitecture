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
        Task<List<PolicyDTO>> GetAll();


        Task<PolicyDTO> GetById(int id);
        Task<List<PolicyDTO>> GetAllPolicyByUserId(string userid);
        Task<List<PolicyDTO>> GetByUserName(string username);


        Task<int> AddPolicy(PolicyDTO model);



        PolicyDTO UpdatePolicy(PolicyDTO model);



        PolicyDTO DeletePolicy(PolicyDTO model);



        Task<bool> POlicyIsExist(string policyName);

        Task<PolicyGenReturnedData> GeneratePolicyNumber(GeneratePolicyDTO generatePolicyDTO);
    }
}
