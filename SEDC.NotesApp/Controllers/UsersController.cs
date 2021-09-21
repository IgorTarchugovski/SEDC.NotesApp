using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.DtoModels;
using Microsoft.AspNetCore.Http;
using SEDC.NotesApp.Shared.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet] // api/users
        public ActionResult<List<UserDto>> Get()
        {
            try
            {
                return Ok(_userService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message});
            }
            
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")] // api/users/5
        public ActionResult<UserDto> Get(int id)
        {
            try
            {
                return Ok(_userService.GetById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult Post([FromBody] UserDto newUser)
        {
            try
            {
                _userService.Create(newUser);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(BadRequestException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, 
                    new { Message = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        // PUT api/<UserController>/5
        [HttpPut] //api/users
        public ActionResult Put([FromBody] UserDto user)
        {
            try
            {
                _userService.Update(user);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (UserException ex) {
                return StatusCode(StatusCodes.Status400BadRequest,
                        new { Message = ex.Message, UserId = ex.UserId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
