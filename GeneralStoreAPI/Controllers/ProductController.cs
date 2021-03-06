﻿using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private StoreDbContext _context = new StoreDbContext();

        //Post
        public IHttpActionResult Post(Product product)
        {
            //if Customer is empty
            if (product == null)
            {
                return BadRequest("Your request body cannot be empty");
            }
            // if the modelstate is not valid
            product.SKU = GenerateSku(product.Name);
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //Get

        public IHttpActionResult Get()
        {
            List<Product> products = _context.Products.ToList();
            if (products.Count != 0)
            {

                return Ok(products);
            }
            return BadRequest("Your database contains no Customers");
        }

        //Get{id}
        public IHttpActionResult Get(string sku)
        {
            Product product = _context.Products.Find(sku);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //Put

        //Put{id}
        public IHttpActionResult Put(string sku, Product changeProd)
        {
            Product product = _context.Products.Find(sku);
            if (product == null)
            {
                return NotFound();
            }
            product.Cost = changeProd.Cost;
            product.Name = changeProd.Name;
            product.NumberInInventory = changeProd.NumberInInventory;
            _context.Products.AddOrUpdate(product);
            _context.SaveChanges();
            return Ok();
        }


        //Delete{id}
        public IHttpActionResult Delete([FromUri] string sku)
        {
            Product product = _context.Products.Find(sku);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }
        private string GenerateSku(string productName)
        {
            // Initialize a Random object so we can randomly assign this a number
            Random random = new Random();
            // Get a Random number and turn it into a string
            var randItemNum = random.Next(0, 1000).ToString();
            // Construct a 3 character string based on the above number. If the number is less than 3 digits, add 0's in front of it to make it 3 digits
            var itemId = new string('0', 3 - randItemNum.Length) + randItemNum;
            // Create the entire SKU and return it
            return $"EFA-{productName.Substring(0, 3)}-{itemId}";
        }
    }
}
