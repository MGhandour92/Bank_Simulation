using Account_API.Interfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Account_API.Repos
{
    public class DB_AccountRepo : IAccountRespository
    {
        public void AddTransaction(Transaction log, out string Response)
        {
            throw new NotImplementedException();
        }

        public void Create(Account account, out string Response)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id, out string Response)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> FindAccTRXs(int Acc_Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Account account, out string Response)
        {
            throw new NotImplementedException();
        }
    }
}
