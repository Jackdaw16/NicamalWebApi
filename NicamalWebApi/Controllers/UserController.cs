using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NicamalWebApi.DbContexts;
using NicamalWebApi.Models;
using NicamalWebApi.Models.ViewModels;
using NicamalWebApi.Services;


namespace NicamalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IImageStorage _imageStorage;
        private readonly IConfiguration _configuration;
        
        public UserController(ApplicationDbContext dbContext, IMapper mapper, IConfiguration configuration, IImageStorage imageStorage)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            _imageStorage = imageStorage;

        }

        [HttpGet("detail", Name = "GetSingleUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDetail>> GetDetail ([FromQuery] string id)
        {
            try
            {
                var user = await _dbContext.Users
                    .Include(u => u.Publications)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return NotFound();
                
                var userMapped = _mapper.Map<UserCount>(user);

                userMapped.PublicationCount = user.Publications.Count;

                return _mapper.Map<UserDetail>(userMapped);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<UserResponseWhenLoggedIn>> RegisterNormalUser([FromBody] UserRegister userRegister)
        {
            try
            {
                using (var sha256 = SHA256.Create())
                {
                    userRegister.Password = string.Concat(sha256.ComputeHash(Encoding.UTF8.GetBytes(userRegister.Password))
                        .Select(item => item.ToString("x2")));
                }

                var user = _mapper.Map<User>(userRegister);

                user.Id = Guid.NewGuid().ToString();
                user.IsShelter = false;
                user.Verify = false;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                
                _dbContext.Add(user);
                await _dbContext.SaveChangesAsync();
                
                return new CreatedAtRouteResult("GetSingleUser", new {user.Id}, _mapper.Map<UserResponseWhenLoggedIn>(user));

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserLoggedIn>> LoggingUser([FromBody] UserLogIn userLogin)
        {
            var user = await _dbContext.Users
                .Where(u => !u.IsShelter)
                .FirstOrDefaultAsync(u => u.Email == userLogin.Email);

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
        public async Task<ActionResult> Put([FromQuery] string id, [FromBody] UserUpdate userUpdate)
        {
            try
            {
                var user = await _dbContext.Users
                    .Where(u => !u.IsShelter)
                    .FirstOrDefaultAsync(u => u.Id == id);
                
                if (user == null)
                    return NotFound();

                var userImage = user.Image;
                
                user = _mapper.Map(userUpdate, user);

                if (string.IsNullOrEmpty(userUpdate.Image))
                    user.Image = userImage;

                user.UpdatedAt = DateTime.Now;

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

            var user = await _dbContext.Users
                .Where(u => !u.IsShelter)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            var userUpdate = _mapper.Map<UserPatch>(user);

            patchDocument.ApplyTo(userUpdate, ModelState);

            _mapper.Map(userUpdate, user);
            
            var isValid = await TryUpdateModelAsync(user);
            
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

            var user = await _dbContext.Users
                .Where(u => !u.IsShelter)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            var userUpdate = _mapper.Map<UserPatch>(user);

            patchDocument.ApplyTo(userUpdate, ModelState);

            _mapper.Map(userUpdate, user);
            
            using (var sha256 = SHA256.Create())
            {
                user.Password =
                    string.Concat(sha256.ComputeHash(Encoding.UTF8.GetBytes(user.Password))
                        .Select(item => item.ToString("x2")));
            }
            
            var isValid = await TryUpdateModelAsync(user);
            
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
                var user = await _dbContext.Users
                    .Where(u => !u.IsShelter)
                    .Include(u => u.Publications)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return NotFound();
                
                foreach (var publication in user.Publications)
                    await _imageStorage.DeleteFile(publication.Image, "animals");

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            
        }
        
        private async Task<ActionResult<UserLoggedIn>> TokenGenerator(User user)
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
            
            return new UserLoggedIn
            {
                UserResponse = _mapper.Map<UserResponseWhenLoggedIn>(user),
                Token = tokenHandler.WriteToken(createdToken)
            };
        }
    }
}