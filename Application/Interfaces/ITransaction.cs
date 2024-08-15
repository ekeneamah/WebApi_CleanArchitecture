﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransaction
    {
        Task<int> SaveResponse(Transaction transactionResponse);
        Task<int> DeleteResponse(Transaction transactionResponse);
        Task<int> UpdateResponse(Transaction transactionResponse);
        Task<TransactionDto> GetTransactionByReference(string reference); // New method
        Task<IEnumerable<TransactionDto>> GetTransactionsByUserId(string userId); // New method
    }

}
