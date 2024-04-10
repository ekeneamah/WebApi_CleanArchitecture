using Application.Dtos;
using Application.Interfaces.Content;
using Application.Interfaces.Content.Brands;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Content
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsuranceCoyController : ControllerBase
    {
        private readonly IInsuranceCoy _insuranceCoyService;

        public InsuranceCoyController(IInsuranceCoy insuranceCoyService)
        {
            _insuranceCoyService = insuranceCoyService;
        }

        #region Get All Brands Endpoint

        // GET: api/InsuranceCompany
        [HttpGet]
        public async Task<ActionResult<List<InsuranceCoy>>> GetBrands()
        {
            var brands = await _insuranceCoyService.GetAll();

            if (brands is null)
                return BadRequest("No Data here !");

            return Ok(brands);
        }

        #endregion Get All Brands Endpoint

        #region Get Brand Endpoint

        // GET: api/InsuranceCompany/5
        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<InsuranceCoyDTO>> GetInsuranceCoy(int id)
        {
            var brand = await _insuranceCoyService.GetById(id);

            if (brand == null)
            {
                return NotFound($"No insurenceCoy has found with this {id} ");
            }

            return brand;
        }

        #endregion Get Brand Endpoint

        #region Create Brand Endpoint

        // POST: api/InsuranceCompany
        [HttpPost]
        public async Task<ActionResult<InsuranceCoy>> PostInsuranceCoy( InsuranceCoyDTO insurenceCoy)
        {
            if (await _insuranceCoyService.CoyIsExist(insurenceCoy.Coy_Name))
            {
                return BadRequest($"InsuranceCompany name: {insurenceCoy.Coy_Name} is already registered");
            }

            var result = await _insuranceCoyService.Add_Coy(insurenceCoy);
            return Ok(await _insuranceCoyService.GetAll());
        }

        #endregion Create Brand Endpoint

        #region Update Category
        // PUT: api/InsuranceCompany/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id, InsuranceCoy model)
        {
            var brand = await _insuranceCoyService.GetById(id);

            if (brand == null)
                return NotFound($"insurenceCoy: {model.Coy_Name} was not found");

            if (await _insuranceCoyService.CoyIsExist(model.Coy_Name))
                return BadRequest(" this InsuranceCompany name is already registred");


            brand.Coy_Name = model.Coy_Name;
            await _insuranceCoyService.Update_Coy(brand);

            return Ok(brand);
        }
        #endregion

        #region Delete Category
        // DELETE: api/InsuranceCompany/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _insuranceCoyService.GetById(id);
            if (brand == null)
            {
                return NotFound($"insurenceCoy:{brand.Coy_Name}  has not found");
            }

            await _insuranceCoyService.Delete_Coy(brand);

            return Ok($"InsuranceCompany : {brand.Coy_Name} with Id : ({brand.Coy_id}) is deleted");
        }
        #endregion
    }
}