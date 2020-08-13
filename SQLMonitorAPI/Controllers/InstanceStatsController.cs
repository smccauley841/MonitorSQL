using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLMonitorAPI.Data;
using System.Linq;
using System;

namespace SQLMonitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstanceStatsController : ControllerBase
    {
        private readonly DataContext _context;

        public InstanceStatsController(DataContext context)
        {
            _context = context;
        }


    [HttpGet("getInstanceStats/{id}")]
        public async Task<IActionResult> GetServerStats(int id)
        {
            var instanceStats = await _context.InstanceStats
                .Where(x => x.Instance.ID == id)
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefaultAsync();

            return Ok(instanceStats);
        }

        [HttpGet("GetWaitStats/{id}")]
        public async Task<IActionResult> GetWaitStats(int id)
        {
            var waitStats = await _context.vwInstanceWaitStats
                .Where(x => x.InstanceId == id)
                .OrderByDescending(x => x.timestamp)
                .Take(5)
                .ToListAsync();

            var wsByPercentage = waitStats
                .OrderByDescending(x => x.WaitPercentage)
                .ToList();

            return Ok(wsByPercentage);
        }

        [HttpGet("getAllInstanceStats/{id}")]
        public async Task<IActionResult> GetAllServerStats(int id)
        {
            var date = DateTime.MinValue;
            var cpuList = await (from d in _context.SQLInstances
                                 join stat in _context.ServerStats on d.PhysicalServerName equals stat.PhysicalServerName into stats
                                 from serverstats in stats.DefaultIfEmpty()
                                 where d.ID == id
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
                .Select(grp => grp.OrderBy(p => p.Timestamp)).ToList();

            return Ok(grpCPUList);
        }
    }
}