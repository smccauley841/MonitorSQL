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
    public class SQLFileSizeController : ControllerBase
    {
        private readonly DataContext _context;

        public SQLFileSizeController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSQLFileSize()
        {
            // throw new System.Exception("This is a test");
            var sqlBackup = await _context.DatabaseFileSize.ToListAsync();
            return Ok(sqlBackup);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSQLFileSize(int id)
        {
            var filesize = await _context.vwDatabaseFileSizes.Where(x => x.InstanceId == id).ToListAsync();

            var sqlBackup = await _context.DatabaseFileSize.Where(x => x.Instance.ID == id).ToListAsync();
            return Ok(filesize);
        }
    }
}
