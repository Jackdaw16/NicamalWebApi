using System;
using System.Collections.Generic;
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
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;
        
        public PublicationController(IMapper mapper, ApplicationDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationsResponseForList>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var queryable = dbContext.Publications.AsQueryable();
                await HttpContext.AddPaginationParams(queryable, pagination.CountRegistryPerPage);
                
                var publications = await queryable.Paginate(pagination).Include(p => p.User).ToListAsync();

                return mapper.Map<List<PublicationsResponseForList>>(publications);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
    }
}