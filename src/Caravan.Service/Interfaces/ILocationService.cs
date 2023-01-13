using Caravan.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Interfaces
{
    public interface ILocationService
    {
        public Task<(bool IsSuccessful, long Id)> CreateAsync(LocationCreateDto createDto);
    }
}
