using AccountMicroservice.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AccountController));
        private readonly IAccountService _repo;
        public AccountController(IAccountService repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// This method creates customer account
        /// </summary>
        /// <param name="Custid"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("CreateAccount/{Custid}")]
        public IActionResult CreateAccount(int Custid)
        {
            _log4net.Info("AccountController-CreateAccount Method IN");
            try
            {
                var result = _repo.createAccount(Custid);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// This method returns all accounts of the customer
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCustomerAccounts/{custId}")]
        public IActionResult GetCustomerAccounts(int custId)
        {
            _log4net.Info("AccountController-GetCustomerAccounts method IN");
            try
            {
                var accList = _repo.getCustomerAccounts(custId);
                return Ok(accList);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// This method returns account by AccountId
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAccountById/{AccountID}")]
        public IActionResult GetAccountById(int AccountID)
        {
            _log4net.Info("AccountController-GetAccountById method IN");
            try
            {
                var acc = _repo.getAccountById(AccountID);
                return Ok(acc);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// This method returns Account balance by AccountId
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetBalance/{AccountID}")]
        public IActionResult GetBalance(int AccountID)
        {
            _log4net.Info("AccountController-GetBalance method IN");
            try
            {
                float balance = _repo.GetBalance(AccountID);
                return Ok(balance);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// This method returns all Statements of account for given dates
        /// </summary>
        /// <param name="AccountID"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAccountStatements/{AccountID}/{fromDate}/{toDate}")]
        public IActionResult GetAccountStatements(int AccountID, DateTime fromDate, DateTime toDate)
        {
            _log4net.Info("AccountController-GetAccountStatements method IN");
            try
            {
                var statList = _repo.getAccountStatement(AccountID,fromDate,toDate);
                return Ok(statList);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// This method deposits amount 
        /// </summary>
        /// <param name="AccId"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("Deposit/{AccId}/{Amount}")]
        public IActionResult Deposit(int AccId,float Amount)
        {
            _log4net.Info("AccountController-Deposit Method IN");
            try
            {
                var result = _repo.Deposite(AccId,Amount);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// This method withdraws amount
        /// </summary>
        /// <param name="AccId"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Withdraw/{AccId}/{Amount}")]
        public IActionResult Withdraw(int AccId, float Amount)
        {
            _log4net.Info("AccountController-Withdraw Method IN");
            try
            {
                var result = _repo.Withdraw(AccId, Amount);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// This method returns all accounts
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
            _log4net.Info("AccountController-GetAllAccounts Method IN");
            try
            {
                var result = _repo.GetAllAccounts();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
