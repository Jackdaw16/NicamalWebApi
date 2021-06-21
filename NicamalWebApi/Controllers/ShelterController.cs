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
    public class ShelterController: ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ShelterController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserShelterList>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var queryable = _dbContext.Users.AsQueryable();
                queryable = queryable.OrderByDescending(p => p.UpdatedAt);
                
                await HttpContext.AddPaginationParams(queryable, pagination.PageSize);

                var shelters = await queryable.Paginate(pagination)
                    .Where(u => u.IsShelter)
                    .Include(u=> u.Publications)
                    .ToListAsync();
                
                var shelterMapped = _mapper.Map<List<UserShelter>>(shelters);

                foreach (var shelter in shelterMapped)
                {
                    shelter.PublicationCount = shelter.Publications.Count;
                }

                return _mapper.Map<List<UserShelterList>>(shelterMapped);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}