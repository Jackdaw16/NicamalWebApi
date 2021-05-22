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
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public ReportController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportResponse>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var queryable = _context.Reports.AsQueryable();
                queryable = queryable.OrderByDescending(p => p.CreatedAt);
                
                await HttpContext.AddPaginationParams(queryable, pagination.PageSize);

                var reports = await queryable.Paginate(pagination)
                    .Include(p => p.Publication)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                return _mapper.Map<List<ReportResponse>>(reports);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}", Name = "GetSingleReport")]
        public async Task<ActionResult<ReportResponse>> Get(int id)
        {
            try
            {
                var report = await _context.Reports
                    .Include(p => p.Publication)
                    .Include(ur => ur.ReportedUser)
                    .Include(u => u.User)
                    .FirstOrDefaultAsync(r => r.Id == id);
                
                if (report == null)
                    return NotFound();

                return _mapper.Map<ReportResponse>(report);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }
    }
}