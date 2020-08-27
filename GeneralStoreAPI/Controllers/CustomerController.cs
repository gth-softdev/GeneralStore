using GeneralStoreAPI.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();

        //Post
        public IHttpActionResult Post(Customer customer)
        {
            //if Customer is empty
            if (customer == null)
            {
                return BadRequest("Your request body cannot be empty");
            }
            // if the modelstate is not valid
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //Get
        public IHttpActionResult Get()
        {
            List<Customer> customers = _context.Customers.ToList();
            if (customers.Count != 0)
            {

                return Ok(customers);
            }
            return BadRequest("Your database contains no Customers");
        }



        //Get{id}
        //public IHttpActionResult Post(Customer customer)

        public IHttpActionResult Get(int id)
        {
            Customer customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        //Put
        //public IHttpActionResult Post(Customer customer)

        //Put{id}
        //public IHttpActionResult Post(Customer customer)

        //Delete{id}
        //public IHttpActionResult Delete(Customer customer)

        //_context.Customers.Remove(customer);
        //_context.SaveChanges();
        //return Ok();

    }
}

