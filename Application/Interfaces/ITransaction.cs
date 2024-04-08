using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransaction
    {
        Task<int> SaveResponse(Transaction transactionResponse);
        Task<int> DeleteResponse(Transaction transactionResponse);
        Task<int> UpdateResponse(Transaction transactionResponse);
    }
}
