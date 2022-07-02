using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RulesMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RulesMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RulesController));

        [HttpGet]
        [Route("EvaluateMinimumBalance/{balance}")]
        public IActionResult EvaluateMinimumBalance(float balance)
        {
            _log4net.Info("RulesController-EvaluateMinimumBalance Method IN");
            RuleStatus rs = new RuleStatus();
            if (balance < 1000)
            {
                rs.status = "denied";
            }
            else
            {
                rs.status = "allowed";
            }
            return Ok(rs);

        }

        [HttpGet]
        [Route("GetServiceCharges")]
        public float GetServiceCharges()
        {
            float charge = 200;
            _log4net.Info("Rules-GetServiceCharges Method IN");
            return charge;
        }


    }
}
