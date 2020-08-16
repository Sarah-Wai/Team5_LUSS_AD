﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LUSS_API.DB;
using LUSS_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LUSS_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemPriceController : ControllerBase
    {
        public MyDbContext context123;
        private readonly ILogger<ItemPriceController> _logger;
        public ItemPriceController(ILogger<ItemPriceController> logger, MyDbContext context123)
        {
            _logger = logger;
            this.context123 = context123;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public int GetItemPrice(int id)
        {
            ItemPrice itemPrice = context123.ItemPrice
                .Where(x => x.ItemID == id).FirstOrDefault();

            int price = itemPrice.Price;
            
            return price; 
        }

        //get supplier list by item
        [HttpGet("get-supplier-by-item/{id}")]
        public List<Supplier> GetSupplierByItem(int id)
        {
            List<ItemPrice> itemPriceList = context123.ItemPrice.Where(x => x.ItemID == id).ToList();
            List<Supplier> suppliers = itemPriceList.Select(x => x.Supplier).ToList();
            return suppliers;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
