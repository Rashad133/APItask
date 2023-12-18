﻿using APItask.DAL;
using APItask.Dtos.Tag;
using APItask.Entities;
using APItask.Repositoris.Interfaces;
using APItask.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APItask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;
        public TagsController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=3)
        {
            return Ok(await _service.GetAllAsync(page, take)); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateTagDto tagDto)
        {
            await _service.CreateAsync(tagDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateTagDto updateTagDto)
        {
            if(id<=0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id,updateTagDto);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
