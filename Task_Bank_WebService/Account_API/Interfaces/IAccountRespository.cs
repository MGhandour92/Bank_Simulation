﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account_API.Interfaces
{
    public interface IAccountRespository
    {
        void Create(Account account, out string Response);
        void Update(Account account, out string Response);
        void Delete(int id, out string Response);

        IQueryable<Account> FindAll();
        //IQueryable<Account> FindByCondition(Expression<Func<Account, bool>> expression);

        void AddTransaction(Transaction log, out string Response);
        IQueryable<Transaction> FindAccTRXs(int Acc_Id);
    }
}
