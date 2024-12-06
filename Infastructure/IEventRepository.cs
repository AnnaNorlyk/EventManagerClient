using EventManagerClient.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerClient.Infastructure
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
    }


}
