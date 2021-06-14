using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NicamalWebApi.DbContexts;
using NicamalWebApi.Helpers;
using NicamalWebApi.Models;
using NicamalWebApi.Models.ViewModels;

namespace NicamalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController: ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public ReportController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportList>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var queryable = _dbContext.Reports.AsQueryable();
                queryable = queryable.OrderByDescending(p => p.CreatedAt);
                
                await HttpContext.AddPaginationParams(queryable, pagination.PageSize);

                var reports = await queryable.Paginate(pagination)
                    .Include(p => p.Publication)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                return _mapper.Map<List<ReportList>>(reports);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("detail", Name = "GetSingleReport")]
        public async Task<ActionResult<ReportDetail>> Get([FromQuery] string id)
        {
            try
            {
                var report = await _dbContext.Reports
                    .Include(p => p.Publication)
                    .Include(ur => ur.ReportedUser)
                    .Include(u => u.User)
                    .FirstOrDefaultAsync(r => r.Id == id);
                
                if (report == null)
                    return NotFound();

                return _mapper.Map<ReportDetail>(report);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReportCreate reportCreate)
        {
            try
            {
                var localReport = reportCreate;
                reportCreate.Id = Guid.NewGuid().ToString();
                reportCreate.CreatedAt = DateTime.Now;

                var report = _mapper.Map<Report>(localReport);

                _dbContext.Add(report);
                await _dbContext.SaveChangesAsync();

                var reportDetail = _mapper.Map<ReportDetail>(report);
                
                return new CreatedAtRouteResult("GetSingleReport", new {id = report.Id}, reportDetail);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string id)
        {
            try
            {
                var report = await _dbContext.Reports
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (report == null)
                    return NotFound();

                _dbContext.Reports.Remove(report);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        
    }
}