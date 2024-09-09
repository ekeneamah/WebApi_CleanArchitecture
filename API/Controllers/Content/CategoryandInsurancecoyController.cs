using Application.Common;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Content.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.Content
{
    [Route("api/category-and-insurancecoys")]
    [ApiController]
    public class CategoryandInsurancecoyController : BaseController
    {
        private readonly ICategoryandInsurancecoy _service;

        public CategoryandInsurancecoyController(ICategoryandInsurancecoy service)
        {
            _service = service;
        }

        #region GET Operations

        // GET: api/CategoryandInsurancecoy
        [HttpGet]
        public  ActionResult<ApiResult<List<CategoryandInsurancecoy>>> Get()
        {
            return  HandleOperationResult(_service.GetAll());
        }

        // GET: api/CategoryandInsurancecoy/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<ApiResult<CategoryandInsurancecoy>> Get(int id)
        {
            var item = _service.GetById(id);
            return HandleOperationResult(item);

        }
        


        // GET: api/CategoryandInsurancecoy/ByCategoryId/5
        [HttpGet("by-category-id/{categoryId}")]
        public async Task<ActionResult<ApiResult<List<CategoryandInsurancecoyDto>>>> GetByCategoryId([FromRoute] int categoryId)
        {
           var response = await _service.GetByCategoryId(categoryId);
            return HandleOperationResult(response);
        }

        // GET: api/CategoryandInsurancecoy/ByInsuranceCoyId/5
        [HttpGet("by-insurance-coy-id/{coyId}")]
        public async Task<ActionResult<ApiResult<List<CategoryandInsurancecoyDto>>>> GetByInsuranceCoyId([FromRoute] int coyId)
        {
            var response = await _service.GetByInsuranceCoyId(coyId);
            return HandleOperationResult(response);

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
