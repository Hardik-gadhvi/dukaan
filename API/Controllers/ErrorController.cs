﻿using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : BaseApiController
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }
		[HttpGet("badrequest")]
		public IActionResult GetBadRequest()
		{
			return BadRequest("Oops! Bad request!");
		}
		[HttpGet("notfound")]
		public IActionResult GetNotFound()
		{
			return NotFound();
		}
		[HttpGet("internalerror")]
		public IActionResult GetInternalError()
		{
			throw new Exception("Exception!");
		}
		[HttpPost("validationerror")]
		public IActionResult GetValidationError(CreateProductDto product)
		{
			return Ok();
		}
	}
}