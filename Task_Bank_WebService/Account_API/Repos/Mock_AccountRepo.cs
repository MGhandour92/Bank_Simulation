using Account_API.Interfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Account_API.Repos
{
    public class Mock_AccountRepo : IAccountRespository
    {
        private readonly ICBRespository<Account> _cbAccRepo;
        private readonly ICBRespository<Transaction> _cbTRXRepo;

        public Mock_AccountRepo(ICBRespository<Account> cbAccRepo, ICBRespository<Transaction> cbTRXRepo)
        {
            _cbAccRepo = cbAccRepo;
            _cbTRXRepo = cbTRXRepo;
        }

        /// <summary>
        /// Create New Account
        /// </summary>
        /// <param name="account">Account Data</param>
        public void Create(Account account, out string Response)
        {
            _cbAccRepo.WriteToFile(account, "create", out Response);
        }

        /// <summary>
        /// Update Existing Account
        /// </summary>
        /// <param name="account">Account New Data</param>
        public void Update(Account account, out string Response)
        {
            _cbAccRepo.WriteToFile(account, "update", out Response);
        }

        /// <summary>
        /// Soft Delete an Account, by setting isDeleted flag = true
        /// </summary>
        /// <param name="id">ID of an Account</param>
        public void Delete(int id, out string Response)
        {
            _cbAccRepo.WriteToFile(new Account() { Id = id }, "update", out Response);
        }

        /// <summary>
        /// Get all available accounts only with flag isDeleted = false
        /// </summary>
        /// <returns>List of Accounts</returns>
        public IQueryable<Account> FindAll() => _cbAccRepo.ReadFile().Where(acc => acc.IsDeleted == false);

        ///// <summary>
        ///// [Read] Get accounts by search pattern (1 or more)
        ///// </summary>
        ///// <returns>Search for Accounts</returns>
        //public IQueryable<Account> FindByCondition(Expression<Func<Account, bool>> expression) => _cbAccRepo.ReadFile().Where(expression);

        /// <summary>
        /// Add Transaction (Deposit or Withdraw)
        /// </summary>
        /// <param name="log">TRX Data</param>
        public void AddTransaction(Transaction log, out string Response)
        {
            //Apply the trx to the balance
            //get the account
            var account = _cbAccRepo.ReadFile().FirstOrDefault(acc => acc.Id == log.Account_Id);

            if (account is not null)
            {
                switch (log.Type.ToLower())
                {
                    case "deposit":
                        //modify balance value
                        account.Balance += log.Amount;
                        break;
                    case "withdraw":
                        //modify balance value
                        account.Balance -= log.Amount;
                        break;
                }

                //update account
                _cbAccRepo.WriteToFile(account, "update", out string AccResponse);

                if (AccResponse == "Success")
                    //Add transaction
                    _cbTRXRepo.WriteToFile(log, "", out Response);
                else
                    Response = AccResponse;
            }
            else
                Response = "No Account with this data";
        }

        /// <summary>
        /// [Read] Get Transactions of an Account
        /// </summary>
        /// <returns>List of Transactions</returns>
        public IQueryable<Transaction> FindAccTRXs(int Acc_Id) => _cbTRXRepo.ReadFile().Where(trx => trx.Account_Id == Acc_Id);

    }
}
