using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class TransactionController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();

        //Post

        public IHttpActionResult Post(Transaction transaction)
        {
            //if Customer is empty
            if (transaction == null)
            {
                return BadRequest("Your request body cannot be empty");
            }
            // if the modelstate is not valid
            if (ModelState.IsValid && transaction.CustomerId != 0 && transaction.ProductSKU != null)
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //Get

        public IHttpActionResult Get()
        {
            List<Transaction> transactions = _context.Transactions.ToList();
            if (transactions.Count != 0)
            {

                return Ok(transactions);
            }
            return BadRequest("Your database contains no Transactions");
        }

        //Get{id}
        public IHttpActionResult Get(int id)
        {
            Transaction transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        //Put

        //Put{id}

        //Delete{id}
    }
}
