using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerDemo.Services;
using CustomerDemo.Models;

 namespace CustomerDemo.API.Controllers
{
    [Route("api/[controller]")]
     public class CustomerController: Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
           _customerService = customerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();

            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Customer customer) 
        {
             var result = await _customerService.Create(customer);

            if(string.IsNullOrEmpty(result.Id))
            { 
                return Ok(result);
            }
            else
            {
                return BadRequest("Bad request");
            }
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] Customer customer)
        {
            if (customer !=null)
            {
                var result = await _customerService.Update(customer);

                return Ok(result);
            }
            else
            {
                return BadRequest("Bad request");
            }
        }

       

       
        [HttpPost("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var result = await _customerService.Delete(id);

                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
