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
    
    public class BackUpHistoryController : ControllerBase
    {
        private readonly DataContext _context;

        public BackUpHistoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBackupHistory()
        {
            // throw new System.Exception("This is a test");
            var sqlBackup = await _context.BackupHistory.ToListAsync();
            return Ok(sqlBackup);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBackupHistoryByInstance(int id)
        {
            // throw new System.Exception("This is a test");
            var sqlBackup = await _context.BackupHistory.Where(x => x.Instance.ID == id).ToListAsync();
            //var sqlinstance = await _context.BackupHistory.FirstOrDefaultAsync(x => x.Instance.ID == id);
            return Ok(sqlBackup);
        }
    }
}