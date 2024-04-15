﻿using EntityLayer.DTOs.Comment;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Abstract;

namespace User_Posts_API.Controllers
{
    [Route("api/CommentServices")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        private readonly IValidator<CommentCreateDTO> _createValidator;
        private readonly IValidator<CommentUpdateDTO> _updateValidator;

        public CommentController(ICommentService service, IValidator<CommentCreateDTO> createValidator, IValidator<CommentUpdateDTO> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }



        [HttpGet("GetAllComments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllComments()
        {
            var result = await _service.GetAllCommentAsync();

            return Ok(result);
        }


        [HttpGet("GetSingleComment/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSingleComment([FromRoute] Guid id)
        {
            var result = await _service.GetSingleCommentAsync(id);
            if (result == null)
            {
                return NotFound("Invalid Id!");
            }

            return Ok(result);
        }


        [HttpPost("CreateComment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateDTO model)
        {
            var validation = await _createValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return BadRequest();
            }

            var result = await _service.CreateCommentAsync(model);

            return CreatedAtAction(nameof(GetSingleComment), new { id = result.Id }, result);
        }


        [HttpDelete("RemoveComment/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveComment([FromRoute] Guid id)
        {
            var result = await _service.RemoveCommentAsync(id);
            if (!result)
            {
                return NotFound("Invalid Id!");
            }

            return NoContent();
        }
    }
}
