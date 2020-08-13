using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLMonitorAPI.Data;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SQLMonitorAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SQLInstancesController : ControllerBase
    {
        private readonly DataContext _context;

        public SQLInstancesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstanceNames()
        {
            var sqlinstances = await _context.SQLInstances.ToListAsync();
            return Ok(sqlinstances);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstanceById(int id)
        {
            var sqlinstance = await _context.SQLInstances.FirstOrDefaultAsync(x => x.ID == id);
            return Ok(sqlinstance);
        }

        //api/Instance/getRAM
        //[Route("api/Instance/getRAM")]
        [HttpGet("getServerStats")]
        public async Task<IActionResult> GetServerStats()
        {
            var startTime = DateTime.Now.Date.AddMinutes(-30);

            var date = DateTime.MinValue;
            var cpuList = await (from d in _context.SQLInstances
                                 join stat in _context.ServerStats on d.PhysicalServerName equals stat.PhysicalServerName into stats
                                 from serverstats in stats.DefaultIfEmpty()
                                     //where serverstats.Timestamp > startTime
                                 select new
                                 {
                                     Instance = d,
                                     RAM = serverstats == null ? 0.0 : serverstats.RAM,
                                     CPU = serverstats == null ? 0.0 : serverstats.CPU,
                                     DiskIO = serverstats == null ? 0.0 : serverstats.DiskIO,
                                     Timestamp = serverstats == null ? date : serverstats.Timestamp
                                 }).ToListAsync();
            
            var grpCPUList = cpuList
                .GroupBy(u => u.Instance.PhysicalServerName)
                .Select(grp => grp.OrderByDescending(p => p.Timestamp).First()).ToList();

            return Ok(grpCPUList);
        }

        [HttpGet("getServerStats/{id}")]
        public async Task<IActionResult> GetServerStats(int id)
        {
            var startTime = DateTime.Now.Date.AddMinutes(-30);

            var date = DateTime.MinValue;
            var cpuList = await (from d in _context.SQLInstances
                                 join stat in _context.ServerStats on d.PhysicalServerName equals stat.PhysicalServerName into stats
                                 from serverstats in stats.DefaultIfEmpty()
                                 //where serverstats.Timestamp > startTime
                                 select new
                                 {
                                     Instance = d,
                                     RAM = serverstats == null ? 0.0 : serverstats.RAM,
                                     CPU = serverstats == null ? 0.0 : serverstats.CPU,
                                     DiskIO = serverstats == null ? 0.0 : serverstats.DiskIO,
                                     Timestamp = serverstats == null ? date : serverstats.Timestamp
                                 }).ToListAsync();

            var grpCPUList = cpuList
                .Where(i => i.Instance.ID == id)
                .GroupBy(u => u.Instance.PhysicalServerName)
                .Select(grp => grp.OrderByDescending(p => p.Timestamp).First()).ToList();

            return Ok(grpCPUList);
        }

    }
}