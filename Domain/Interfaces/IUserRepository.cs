﻿using EventManagerClient.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerClient.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserAsync();
        Task DeleteUserAsync(string userId);

        Task UpdateUserAsync(User user);

        Task CreateUserAsync(User user);
       
    }
}
