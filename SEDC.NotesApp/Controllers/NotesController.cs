using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Models.DtoModels;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Exceptions;

namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")] // api/notes
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet] // api/notes
        public ActionResult<List<NoteDto>> GetAllNotes()
        {
            try
            {
                return Ok(_noteService.GetAllNotes());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message });
            }
        }

        [HttpGet("user/{id}")] // api/notes/user/1
        public ActionResult<List<NoteDto>> GetAllUserNotes(int id)
        {
            try
            {
                List<NoteDto> userNotes = _noteService.GetAllUserNotes(id);
                return Ok(userNotes);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message });
            }
        }

        [HttpPost("addNewNote")] // POST api/notes/addNewNote
        public ActionResult<string> AddNewNote ([FromBody] NoteDto newNote)
        {
            try
            {
                string result = _noteService.AddNewNote(newNote);
                return Ok(result);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message });
            }
        }

        [HttpDelete("deleteNote/{id}")] // DELETE api/notes/deleteNote/1
        public ActionResult<string> RemoveNote(int id)
        {
            try
            {
                string result = _noteService.RemoveNote(id);
                return Ok(result);
            }
            catch (NoteException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new
                    {
                        Message = ex.Message,
                        NoteId = ex.NoteId
                    });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message });
            }
        }

        [HttpPut("updateNote")] // PUT api/notes/updateNote/1
        public ActionResult<string> UpdateNote([FromBody] NoteDto noteModel)
        {
            try
            {
                string result = _noteService.UpdateNote(noteModel);
                return Ok(result);
            }
            catch (NoteException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new
                    {
                        Message = ex.Message,
                        NoteId = ex.NoteId,
                        UserId = ex.UserId
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = ex.Message });
            }
        }
    }
}
