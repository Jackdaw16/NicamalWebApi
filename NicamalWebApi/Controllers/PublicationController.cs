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
    public class PublicationController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        
        public PublicationController(IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationsResponseForList>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var queryable = _dbContext.Publications.AsQueryable();
                await HttpContext.AddPaginationParams(queryable, pagination.CountRegistryPerPage);
                
                var publications = await queryable.Paginate(pagination).Include(p => p.User).OrderByDescending(x => x.CreatedAt).ToListAsync();

                return _mapper.Map<List<PublicationsResponseForList>>(publications);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
    }
}