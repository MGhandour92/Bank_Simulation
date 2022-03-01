using Account_API.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Task_Bank_WebService.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRespository _accRepo;

        public AccountController(ILogger<AccountController> logger, IAccountRespository accRepo)
        {
            _logger = logger;
            _accRepo = accRepo;
        }

        /// <summary>
        ///  Create
        /// </summary>
        /// <param name="account">New Account Data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Account account)
        {
            _accRepo.Create(account, out string Response);

            return Ok(Response);
        }

        /// <summary>
        ///  Update Account
        /// </summary>
        /// <param name="account">Modified Account Data</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(Account account)
        {
            _accRepo.Update(account, out string Response);

            return Ok(Response);
        }

        /// <summary>
        ///  Delete (Soft) Account
        /// </summary>
        /// <param name="accountID">Account ID</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int accountID)
        {
            _accRepo.Delete(accountID, out string Response);

            return Ok(Response);
        }

        /// <summary>
        ///  Deposit Transaction
        /// </summary>
        /// <param name="trx">Transaction Data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Deposit(Transaction trx)
        {
            trx.Type = "deposit";
            _accRepo.AddTransaction(trx, out string Response);

            return Ok(Response);
        }

        /// <summary>
        ///  Withdraw Transaction
        /// </summary>
        /// <param name="trx">Transaction Data</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Withdraw(Transaction trx)
        {
            trx.Type = "withdraw";
            _accRepo.AddTransaction(trx, out string Response);

            return Ok(Response);
        }


        /// <summary>
        ///  List available accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var allAccounts = _accRepo.FindAll();
            return Ok(allAccounts);
        }

        /// <summary>
        ///  Print Bank Statement for an Account
        /// </summary>
        /// <param name="accountID">Account ID</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PrintStatement(int accountID)
        {
            var alltrx = _accRepo.FindAccTRXs(accountID);
            return Ok(alltrx);
        }
    }
}
