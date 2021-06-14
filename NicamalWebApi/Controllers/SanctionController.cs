using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NicamalWebApi.DbContexts;
using NicamalWebApi.Models;
using NicamalWebApi.Models.ViewModels;

namespace NicamalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionController: ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        
        public SanctionController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet("detail", Name = "GetSanction")]
        public async Task<ActionResult<Sanction>> Get([FromQuery] string id)
        {
            try
            {
                var sanction = await _dbContext.Sanctions
                    .Include(u => u.User)
                    .ThenInclude(u => u.Reported)
                    .ThenInclude(r => r.User)
                    .FirstOrDefaultAsync(r => r.Id == id);
                
                if (sanction == null)
                    return NotFound();

                return sanction;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Sanction>> Post([FromBody] Sanction sanctionCreate)
        {
            try
            {
                var sanction = sanctionCreate;
                sanction.Id = Guid.NewGuid().ToString();
                sanction.CreatedAt = DateTime.Now;

                _dbContext.Add(sanction);
                await _dbContext.SaveChangesAsync();

                return new CreatedAtRouteResult("GetSingleReport", new {id = sanction.Id}, sanction);
                
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
                var sanction = await _dbContext.Sanctions
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (sanction == null)
                    return NotFound();

                _dbContext.Sanctions.Remove(sanction);
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