﻿using Application.Dtos;
using Domain.Entities;
using Infrastructure.Content.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryandInsurancecoyController : ControllerBase
    {
        private readonly CategoryandInsurancecoyService _service;

        public CategoryandInsurancecoyController(CategoryandInsurancecoyService service)
        {
            _service = service;
        }

        #region GET Operations

        // GET: api/CategoryandInsurancecoy
        [HttpGet]
        public IEnumerable<CategoryandInsurancecoy> Get()
        {
            return _service.GetAll();
        }

        // GET: api/CategoryandInsurancecoy/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // GET: api/CategoryandInsurancecoy/ByCategoryId/5
        [HttpGet("ByCategoryId/{categoryId}")]
        public Task<List<CategoryandInsurancecoyDTO>> GetByCategoryId(int categoryId)
        {
            return _service.GetByCategoryId(categoryId);
        }

        // GET: api/CategoryandInsurancecoy/ByInsuranceCoyId/5
        [HttpGet("ByInsuranceCoyId/{insuranceCoyId}")]
        public Task<List<CategoryandInsurancecoyDTO>> GetByInsuranceCoyId(int insuranceCoyId)
        {
            return _service.GetByInsuranceCoyId(insuranceCoyId);
        }

        #endregion

        #region POST Operations

        // POST: api/CategoryandInsurancecoy
        [HttpPost]
        public IActionResult Post([FromBody] CategoryandInsurancecoy item)
        {
            _service.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        #endregion

        #region PUT Operations

        // PUT: api/CategoryandInsurancecoy/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryandInsurancecoy item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            _service.Update(item);
            return NoContent();
        }

        #endregion

        #region DELETE Operations

        // DELETE: api/CategoryandInsurancecoy/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingItem = _service.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }
            _service.Delete(id);
            return NoContent();
        }

        #endregion
    }
}