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
    public class PublicationController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IImageStorage _imageStorage;
        private readonly ApplicationDbContext _dbContext;
        private const string Container = "animals";

        public PublicationController(IMapper mapper, IImageStorage imageStorage, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _imageStorage = imageStorage;
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

        [HttpGet("{id}", Name = "GetSinglePublication")]
        public async Task<ActionResult<PublicationDetail>> Get(int id)
        {
            try
            {
                var publication = await _dbContext.Publications
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (publication == null)
                    return NotFound();

                return _mapper.Map<PublicationDetail>(publication);

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PublicationCreate publicationCreate)
        {
            try
            {
                var localPublicationCreate = publicationCreate;
                localPublicationCreate.CreatedAt = DateTime.Now;
                localPublicationCreate.UpdateAt = DateTime.Now;
            
                var publication = _mapper.Map<Publication>(localPublicationCreate);

                if (localPublicationCreate.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await localPublicationCreate.Image.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(localPublicationCreate.Image.FileName);

                        publication.Image = await _imageStorage.SaveFile(content, extension, Container,
                            localPublicationCreate.Image.ContentType);
                    }
                }

                _dbContext.Add(publication);
                await _dbContext.SaveChangesAsync();

                var publicationDetail = _mapper.Map<PublicationDetail>(publication);
                
                return new CreatedAtRouteResult("GetSinglePublication", new { id = publication.Id }, publicationDetail);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
            

        }
        
    }
}