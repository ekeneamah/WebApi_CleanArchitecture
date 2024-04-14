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
        Task<int> SaveResponse(TransactionEntity transactionResponse);
        Task<int> DeleteResponse(TransactionEntity transactionResponse);
        Task<int> UpdateResponse(TransactionEntity transactionResponse);
        Task<TransactionDTO> GetTransactionByReference(string reference); // New method
        Task<IEnumerable<TransactionDTO>> GetTransactionsByUserId(string userId); // New method
    }

}
