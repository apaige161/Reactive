using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;


namespace DatingApp.API.Controllers
{
    //DEMO CONTROLLER
    //route to get to this controller
    //[controller] is a place holder for the first part of ValuesController, values
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //inject DataContext to be able to query ef database
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            //allows access to DataContext inside the controller
            _context = context;
        }

        //specify GET request
        //GET api/values
        //root of controller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> Get()
        {
            //get the values inside the database and put all in a list
            var values = await _context.Values.ToListAsync();
            //return a 200 http status and return values
            return Ok(values);
        }

        //GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> Get(int id)
        {
            var value = await _context.Values.FindAsync(id);
            return Ok(value);
        }

        //POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        //update
        //PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}