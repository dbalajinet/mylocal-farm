﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using farmApi.Models;

namespace farmApi.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TodoItem> TodoItemRepository { get; }
        IUserManager<User> UserManager { get; }
        void Save();
    }
}
