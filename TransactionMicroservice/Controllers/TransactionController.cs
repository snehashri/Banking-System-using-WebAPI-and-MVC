using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionMicroservice.Repo;

namespace TransactionMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(TransactionController));
        private readonly ITransactionService _repo;
        public TransactionController(ITransactionService repo)
        {
            _repo = repo;
        }


        [HttpGet]
        [Route("GetAllTransactionByAccountId/{AccId}")]
        public IActionResult GetAllTransactionByAccountId(int AccId)
        {
            _log4net.Info("TransactionController-GetAllTransactionByAccountId method IN");
            try
            {
                var tranList = _repo.getTransactions(AccId);
                return Ok(tranList);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("WithDraw/{AccId}/{Amount}")]
        public IActionResult WithDraw(int AccId, float Amount)
        {
            _log4net.Info("TransactionController-WithDraw method IN");
            try
            {
                var result = _repo.withdraw(AccId,Amount);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("Deposit/{AccId}/{Amount}")]
        public IActionResult Deposit(int AccId, float Amount)
        {
            _log4net.Info("TransactionController-Deposit method IN");
            try
            {
                var result = _repo.deposit(AccId, Amount);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("Transfer/{SrcAccId}/{TarAccId}/{amount}")]
        public IActionResult Transfer(int SrcAccId, int TarAccId, float amount)
        {
            _log4net.Info("TransactionController-Transfer method IN");
            try
            {
                var result = _repo.transfer(SrcAccId,TarAccId, amount);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


    }
}
