using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NicamalWebApi.DbContexts;
using NicamalWebApi.Helpers;
using NicamalWebApi.Models;
using NicamalWebApi.Models.ViewModels;
using NicamalWebApi.Services;

namespace NicamalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterController: ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IImageStorage _imageStorage;
        private const string Container = "shelters";
        private readonly IConfiguration _configuration;

        public ShelterController(ApplicationDbContext dbContext, IMapper mapper, IImageStorage imageStorage, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _imageStorage = imageStorage;
            _configuration = configuration;
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
        
        [HttpGet("detail", Name = "GetSingleShelter")]
        public async Task<ActionResult<UserShelterDetail>> Get([FromQuery] string id)
        {
            var urgentPublications = new List<PublicationCount>();
            
            try
            {
                var shelter = await _dbContext.Users
                    .Where(u => u.IsShelter)
                    .Include(u => u.Publications)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (shelter == null)
                    return NotFound();

                var shelterMapped = _mapper.Map<UserShelter>(shelter);
                urgentPublications.AddRange(shelterMapped.Publications.Where(urgent => urgent.IsUrgent));

                shelterMapped.PublicationCount = shelterMapped.Publications.Count;
                shelterMapped.UrgentCount = urgentPublications.Count;

                return _mapper.Map<UserShelterDetail>(shelterMapped);

            }
            catch (Exception e)
            {
                 return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpGet("filters")]
        public async Task<ActionResult<IEnumerable<UserShelterList>>> GetFromFilters(
            [FromQuery] ShelterFilters filters)
        {
            try
            {
                var queryable = _dbContext.Users.AsQueryable();

                if (!string.IsNullOrEmpty(filters.Address))
                {
                    queryable = queryable.Where(x => x.Address.Contains(filters.Address) && x.IsShelter).OrderByDescending(p => p.UpdatedAt);
                }

                if (!string.IsNullOrEmpty(filters.Province))
                {
                    queryable = queryable.Where(x => x.Province.Contains(filters.Province) && x.IsShelter).OrderByDescending(p => p.UpdatedAt);
                }

                if (!string.IsNullOrEmpty(filters.Text))
                {
                    queryable = queryable
                        .Where(x => x.Name.Contains(filters.Text.Trim()) && x.IsShelter);
                }

                await HttpContext.AddPaginationParams(queryable, filters.PageSize);
                
                var shelter = await queryable.Paginate(filters.Pagination).OrderByDescending(x => x.CreatedAt).ToListAsync();

                return _mapper.Map<List<UserShelterList>>(shelter);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("publications")]
        public async Task<ActionResult<IEnumerable<PublicationsList>>> GetShelterPublication([FromQuery] Pagination pagination, [FromQuery] string id)
        {
            try
            {
                var queryable = _dbContext.Publications.AsQueryable();
                queryable = queryable
                    .Where(p => p.UserId == id && p.User.IsShelter)
                    .OrderByDescending(p => p.UpdateAt);

                await HttpContext.AddPaginationParams(queryable, pagination.PageSize);

                var publications = await queryable
                    .Paginate(pagination)
                    .Include(p => p.User)
                    .ToListAsync();

                return _mapper.Map<List<PublicationsList>>(publications);
                
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpGet("publications/filters")]
        public async Task<ActionResult<IEnumerable<PublicationsList>>> GetPublicationsFilters([FromQuery] ShelterPublicationsFilters filters, string id)
        {
            try
            {
                var queryable = _dbContext.Publications.AsQueryable();

                if (!string.IsNullOrEmpty(filters.Text))
                {
                    queryable = queryable
                        .Where(p => p.User.Id == id && p.User.IsShelter)
                        .Where(p => p.Name.Contains(filters.Text.Trim()));
                }

                await HttpContext.AddPaginationParams(queryable, filters.PageSize);
                
                var publications = await queryable
                    .Paginate(filters.Pagination)
                    .Include(p => p.User)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                return _mapper.Map<List<PublicationsList>>(publications);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpGet("publications/urgents")]
        public async Task<ActionResult<IEnumerable<PublicationsList>>> GetUrgentShelterPublication([FromQuery] Pagination pagination, [FromQuery] string id)
        {
            try
            {
                var queryable = _dbContext.Publications.AsQueryable();
                
                queryable = queryable
                    .Where(p => p.UserId == id)
                    .Where(p => p.IsUrgent)
                    .OrderByDescending(p => p.UpdateAt);

                await HttpContext.AddPaginationParams(queryable, pagination.PageSize);

                var publications = await queryable
                    .Paginate(pagination)
                    .Include(p => p.User)
                    .ToListAsync();

                return _mapper.Map<List<PublicationsList>>(publications);
                
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpGet("publications/urgents/filters")]
        public async Task<ActionResult<IEnumerable<PublicationsList>>> GetUrgentPublicationsFilters([FromQuery] ShelterPublicationsFilters filters, string id)
        {
            try
            {
                var queryable = _dbContext.Publications.AsQueryable();

                if (!string.IsNullOrEmpty(filters.Text))
                {
                    queryable = queryable
                        .Where(p => p.User.Id == id && p.User.IsShelter && p.IsUrgent)
                        .Where(p => p.Name.Contains(filters.Text.Trim()));
                }

                await HttpContext.AddPaginationParams(queryable, filters.PageSize);
                
                var publications = await queryable
                    .Paginate(filters.Pagination)
                    .Include(p => p.User)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();

                return _mapper.Map<List<PublicationsList>>(publications);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<UserShelterDetail>> Post([FromForm] UserShelterRegister userShelterRegister)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    userShelterRegister.Password =
                        string.Concat(sha256.ComputeHash(Encoding.UTF8.GetBytes(userShelterRegister.Password))
                            .Select(item => item.ToString("x2")));
                }
                
                var user = _mapper.Map<User>(userShelterRegister);

                user.Id = Guid.NewGuid().ToString();
                user.IsShelter = true;
                user.Verify = false;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                
                if (userShelterRegister.Image != null)
                {
                    await using (var memoryStream = new MemoryStream())
                    {
                        await userShelterRegister.Image.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(userShelterRegister.Image.FileName);

                        user.Image = await _imageStorage.SaveFile(content, extension, Container,
                            userShelterRegister.Image.ContentType);
                    }
                }

                _dbContext.Add(user);
                await _dbContext.SaveChangesAsync();
                
                return new CreatedAtRouteResult("GetSingleShelter", new {user.Id}, _mapper.Map<UserShelterDetail>(user));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserShelterLoggedIn>> LoggingUser([FromBody] UserLogIn userLogin)
        {
            var user = await _dbContext.Users.Where(u => u.IsShelter).FirstOrDefaultAsync(u => u.Email == userLogin.Email);

            if (user == null)
                return NotFound();

            var sanction = await _dbContext.Sanctions
                .Include(u => u.User)
                .FirstOrDefaultAsync(s => s.User.Id == user.Id);
            
            if (sanction != null)
                return new CreatedAtRouteResult("GetSanction", new {sanction.Id}, sanction);

            using (var sha256 = SHA256.Create())
            {
                userLogin.Password = string.Concat(sha256.ComputeHash(Encoding.UTF8.GetBytes(userLogin.Password))
                    .Select(item => item.ToString("x2")));
            }

            if (user.Password != userLogin.Password)
                return Unauthorized();

            return await TokenGenerator(user);
        }
        
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromQuery] string id, [FromForm] UserShelterUpdate userShelterUpdate)
        {
            try
            {
                var shelter = await _dbContext.Users
                    .Where(u => u.IsShelter)
                    .FirstOrDefaultAsync(u => u.Id == id);
                
                if (shelter == null)
                    return NotFound();

                var shelterImage = shelter.Image;
                
                shelter = _mapper.Map(userShelterUpdate, shelter);
                
                if (userShelterUpdate.Image != null)
                {
                    await using (var memoryStream = new MemoryStream())
                    {
                        await userShelterUpdate.Image.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(userShelterUpdate.Image.FileName);

                        shelter.Image = await _imageStorage.EditFile(content, extension, Container,
                            shelter.Image, userShelterUpdate.Image.ContentType);
                    }
                }
                else
                {
                    shelter.Image = shelterImage;
                }

                shelter.UpdatedAt = DateTime.Now;

                await _dbContext.SaveChangesAsync();
                
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPatch]
        [Authorize]
        public async Task<ActionResult> Patch([FromQuery] string id, [FromBody] JsonPatchDocument<UserPatch> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var shelter = await _dbContext.Users
                .Where(u => u.IsShelter)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (shelter == null)
                return NotFound();

            var shelterUpdate = _mapper.Map<UserPatch>(shelter);

            patchDocument.ApplyTo(shelterUpdate, ModelState);

            _mapper.Map(shelterUpdate, shelter);
            
            var isValid = await TryUpdateModelAsync(shelter);
            
            if (!isValid)
                return BadRequest();

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPatch("password")]
        [Authorize]
        public async Task<ActionResult> PatchPassword([FromQuery] string id, [FromBody] JsonPatchDocument<UserPatch> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var shelter = await _dbContext.Users
                .Where(u => u.IsShelter)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (shelter == null)
                return NotFound();

            var shelterUpdate = _mapper.Map<UserPatch>(shelter);

            patchDocument.ApplyTo(shelterUpdate, ModelState);

            _mapper.Map(shelterUpdate, shelter);
            
            using (var sha256 = SHA256.Create())
            {
                shelter.Password =
                    string.Concat(sha256.ComputeHash(Encoding.UTF8.GetBytes(shelter.Password))
                        .Select(item => item.ToString("x2")));
            }
            
            var isValid = await TryUpdateModelAsync(shelter);
            
            if (!isValid)
                return BadRequest();

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete([FromQuery] string id)
        {
            try
            {
                var shelter = await _dbContext.Users
                    .Where(u => u.IsShelter)
                    .Include(u => u.Publications)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (shelter == null)
                    return NotFound();

                await _imageStorage.DeleteFile(shelter.Image, Container);

                foreach (var publication in shelter.Publications)
                    await _imageStorage.DeleteFile(publication.Image, "animals");

                _dbContext.Users.Remove(shelter);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }
        
        private async Task<ActionResult<UserShelterLoggedIn>> TokenGenerator(User user)
        {
            var secretKey = _configuration.GetValue<string>("key");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            };
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            
            return new UserShelterLoggedIn
            {
                Shelter = _mapper.Map<UserShelterDetail>(user),
                Token = tokenHandler.WriteToken(createdToken)
            };
        }
        
    }
    
    
}