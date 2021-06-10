using System;
using System.Collections.Generic;
using System.IO;
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
using NicamalWebApi.Services;

namespace NicamalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisappearanceController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly string Container = "disappearances";
        private readonly IImageStorage _imageStorage;

        public DisappearanceController(IImageStorage imageStorage, IMapper mapper, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _imageStorage = imageStorage;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DisappearanceListResponse>>> Get([FromQuery] Pagination pagination)
        {
            try
            {
                var queryable = _dbContext.Disappearances.AsQueryable();
                queryable = queryable.OrderByDescending(d => d.CreatedAt);

                await HttpContext.AddPaginationParams(queryable, pagination.PageSize);

                var publication = await queryable.Paginate(pagination).ToListAsync();

                return _mapper.Map<List<DisappearanceListResponse>>(publication);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        [HttpGet("detail", Name = "GetSingleDisappearance")]
        public async Task<ActionResult<DisappearanceDetail>> Get([FromQuery] string id)
        {
            try
            {
                var disappearance = await _dbContext.Disappearances
                    .FirstOrDefaultAsync(d => d.Id == id);
                if (disappearance == null)
                    return NotFound();

                return _mapper.Map<DisappearanceDetail>(disappearance);

            }
            catch (Exception e)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] DisappearanceCreate disappearanceCreate)
        {
            try
            {
                var localDisappearance = disappearanceCreate;
                localDisappearance.Id = Guid.NewGuid().ToString();
                localDisappearance.CreatedAt = DateTime.Now;

                var disappearance = _mapper.Map<Disappearance>(localDisappearance);
                if (localDisappearance.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await localDisappearance.Image.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(localDisappearance.Image.FileName);

                        disappearance.Image = await _imageStorage.SaveFile(content, extension, Container,
                            localDisappearance.Image.ContentType);
                    }
                }

                _dbContext.Add(disappearance);
                await _dbContext.SaveChangesAsync();

                var disappearanceDetail = _mapper.Map<DisappearanceDetail>(disappearance);
                
                return new CreatedAtRouteResult("GetSingleDisappearance", new {id = disappearance.Id}, disappearanceDetail);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var disappearance = await _dbContext.Disappearances
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (disappearance == null)
                    return NotFound();

                _dbContext.Disappearances.Remove(disappearance);
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