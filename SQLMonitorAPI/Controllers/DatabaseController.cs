using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLMonitorAPI.Data;

namespace SQLMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {

        private readonly DataContext _context;

        public DatabaseController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Database
        [HttpGet]
        public async Task<IActionResult> GetDatabases()
        {
            var sqldatabases = await (from d in _context.SQLDatabases
                         join i in _context.SQLInstances on d.Instance.ID equals i.ID
                         select new {
                             d.ID,
                             d.Name,
                             Instance = i.Name,
                             d.Status}).ToListAsync();
            return Ok(sqldatabases);
        }

        // GET: api/Database/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDatabaseByDatabaseId(int id)
        {

            var sqldatabase = await (from d in _context.SQLDatabases
                                      join i in _context.SQLInstances on d.Instance.ID equals i.ID
                                     where d.ID == id
                                      select new
                                      {
                                          d.ID,
                                          d.Name,
                                          Instance = i.Name,
                                          d.Collation,
                                          d.Status
                                      }).ToListAsync();
            return Ok(sqldatabase);
        }

        //api/Database/byInstance?id=1
        
        [HttpGet("byInstance/{id}")]
        public async Task<IActionResult> GetDatabaseByInstanceId(int id)
        {

            var sqldatabase = await (from d in _context.SQLDatabases
                                     join i in _context.SQLInstances on d.Instance.ID equals i.ID
                                     where d.Instance.ID == id
                                     select new
                                     {
                                         d.ID,
                                         d.Name,
                                         Instance = i.Name,
                                         d.Collation,
                                         d.Status
                                     }).ToListAsync();
            return Ok(sqldatabase);
        }

        // POST: api/Database
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Database/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
