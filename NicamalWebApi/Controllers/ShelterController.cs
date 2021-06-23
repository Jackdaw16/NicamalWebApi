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
        
        [HttpGet("detail")]
        public async Task<ActionResult<UserShelterDetail>> Get([FromQuery] string id)
        {
            List<PublicationCount> urgentPublications = new List<PublicationCount>();
            try
            {
                var shelter = await _dbContext.Users.Where(u => u.IsShelter)
                    .Include(u => u.Publications)
                    .FirstOrDefaultAsync(u => u.Id == id);

                var shelterMapped = _mapper.Map<UserShelter>(shelter);
                foreach (var urgent in shelterMapped.Publications)
                {
                    if (urgent.IsUrgent)
                        urgentPublications.Add(urgent);
                }

                shelterMapped.PublicationCount = shelterMapped.Publications.Count;
                shelterMapped.UrgentCount = urgentPublications.Count;

                return _mapper.Map<UserShelterDetail>(shelterMapped);

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
                using (SHA256 sha256 = SHA256.Create())
                {
                    userShelterRegister.Password =
                        String.Concat(sha256.ComputeHash(Encoding.UTF8.GetBytes(userShelterRegister.Password))
                            .Select(item => item.ToString("x2")));
                }
                
                User user = _mapper.Map<User>(userShelterRegister);

                user.Id = Guid.NewGuid().ToString();
                user.IsShelter = true;
                user.Verify = false;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                
                if (userShelterRegister.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
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
                
                return new CreatedAtRouteResult("GetSingleUser", new {user.Id}, _mapper.Map<UserShelterDetail>(user));

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
            {
                return NotFound();
            }

            var sanction = await _dbContext.Sanctions
                .Include(u => u.User)
                .FirstOrDefaultAsync(s => s.User.Id == user.Id);
            
            if (sanction != null)
            {
                return new CreatedAtRouteResult("GetSanction", new {sanction.Id}, sanction);
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                userLogin.Password = String.Concat(sha256.ComputeHash(Encoding.UTF8.GetBytes(userLogin.Password))
                    .Select(item => item.ToString("x2")));
            }

            if (user.Password != userLogin.Password)
            {
                return Unauthorized();
            }

            return await TokenGenerator(user);
        }
        
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromQuery] string id, [FromForm] UserShelterUpdate userShelterUpdate)
        {
            try
            {
                var shelter = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                
                if (shelter == null)
                    return NotFound();
                if (!shelter.IsShelter)
                    return BadRequest();
                
                var shelterImage = shelter.Image;
                
                shelter = _mapper.Map(userShelterUpdate, shelter);
                
                if (userShelterUpdate.Image != null)
                {
                    using (var memoryStream = new MemoryStream())
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
        public async Task<ActionResult> Patch([FromQuery] string id, [FromBody] JsonPatchDocument<UserShelterPatch> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var shelter = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (shelter == null)
                return NotFound();
            if (!shelter.IsShelter)
                return BadRequest();
            
            var shelterUpdate = _mapper.Map<UserShelterPatch>(shelter);

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
        public async Task<ActionResult> PatchPassword([FromQuery] string id, [FromBody] JsonPatchDocument<UserShelterPatch> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            var shelter = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (shelter == null)
                return NotFound();
            if (!shelter.IsShelter)
                return BadRequest();
            
            var shelterUpdate = _mapper.Map<UserShelterPatch>(shelter);

            patchDocument.ApplyTo(shelterUpdate, ModelState);

            _mapper.Map(shelterUpdate, shelter);
            
            using (SHA256 sha256 = SHA256.Create())
            {
                shelter.Password =
                    String.Concat(sha256.ComputeHash(Encoding.UTF8.GetBytes(shelter.Password))
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
                var shelter = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (shelter == null)
                    return NotFound();

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