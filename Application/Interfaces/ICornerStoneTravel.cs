using Application.Common;
using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICornerStoneTravelService
    {
        Task<ApiResult<TravelPollicyResponseDto>> GetPolicyAmountAsync(FormBody formBody);
    }

}
