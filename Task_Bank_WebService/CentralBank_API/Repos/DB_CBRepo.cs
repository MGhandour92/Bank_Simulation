using Account_API.Interfaces;
using Entities.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Account_API.Repos
{
    public class DB_CBRepo : ICBRespository<Account>
    {
        public IQueryable<Account> ReadFile()
        {
            throw new NotImplementedException();
        }

        public void WriteToFile(Account log, string action, out string response)
        {
            throw new NotImplementedException();
        }
    }
}
