using Microsoft.AspNetCore.Mvc;
using NwbaApi.Models;
using NwbaApi.Models.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwbaApi.Controllers
{
    [ApiController]
    [Route("api/billpayments")]
    public class BillPayController : Controller
    {
        private readonly BillPayManager _repo;
        public BillPayController(BillPayManager repo)
        {
            _repo = repo;
        }

        // GET: api/billpay
        [HttpGet]
        public IEnumerable<BillPay> Get()
        {
            return _repo.GetAll();
        }

       
        // GET api/billpay/1
        [HttpGet("{id}")]
        public BillPay Get(int id)
        {
            return _repo.Get(id);
        }


        // This api is used to block the scheduled bill payment by passing billpay ID
        [Route("{id}/block")]
        [HttpPost("{id}")]
        [ValidateModel]
        public IActionResult Block(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("", "Invalid Billpay ID");
                return BadRequest();
            }
            var billpay = _repo.Get(id);

            if (billpay == null)
            {
                ModelState.AddModelError("", "Invalid Billpay ID");
                return BadRequest();
            }

            if (billpay.BillPayStatus != BillPayStatus.ReadyToProcess)
            {
                ModelState.AddModelError("", "Cannot block Billpay");
                return BadRequest();
            }

            _repo.Block(id);
            return Ok();
        }

        // This api is used to unblock the scheduled bill payment by passing billpay ID
        [Route("{id}/unblock")]
        [HttpPost("{id}")]
        [ValidateModel]
        public IActionResult UnBlock(int id)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("", "Invalid Billpay ID");
                return BadRequest();
            }
            var billpay = _repo.Get(id);

            if (billpay == null)
            {
                ModelState.AddModelError("", "Invalid Billpay ID");
                return BadRequest();
            }

            if (billpay.BillPayStatus != BillPayStatus.Blocked)
            {
                ModelState.AddModelError("", "Cannot unblock Billpay");
                return BadRequest();
            }

            _repo.UnBlock(id);
            return Ok();
        }

    }
}
