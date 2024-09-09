using Application.Common;
using Application.Dtos;
using Application.Interfaces.Content;
using Application.Interfaces.Content.Brands;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Content
{
    [Route("api/insurance-coys")]
    [ApiController]
    
    public class InsuranceCoyController : BaseController
    {
        private readonly IInsuranceCoy _insuranceCoyService;

        public InsuranceCoyController(IInsuranceCoy insuranceCoyService)
        {
            _insuranceCoyService = insuranceCoyService;
        }

        #region Get All Insurance coy Endpoint

        // GET: api/InsuranceCompany
        [HttpGet]
        public async Task<ActionResult<ApiResult<List<InsuranceCoyDto>>>> GetAllInsuranceCoy(int pageNumber = 1, int pageSize = 10)
        {
            var insuranceCoy = await _insuranceCoyService.GetAll(pageNumber, pageSize);
            return HandleOperationResult(insuranceCoy);
        }

        #endregion Get All Brands Endpoint

        #region Get Insurance Coy Data

        // GET: api/InsuranceCompany/5
        [HttpGet("{id}")]
       // [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<ApiResult<InsuranceCoyDetailDto>>> GetInsuranceCoy(int id)
        {
            var insuranceCoy = await _insuranceCoyService.GetInsuranceCoyDetailById(id);

            return HandleOperationResult(insuranceCoy);

        }

        #endregion

        #region Create Insurance Coy Endpoint

        // POST: api/InsuranceCompany
        [HttpPost]
        public async Task<ActionResult<ApiResult<InsuranceCoyDto>>> PostInsuranceCoy([FromForm] InsuranceCoyDto insuranceCoy, IFormFile logoImageFile, IFormFile displayImageFile, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (await _insuranceCoyService.CoyIsExist(insuranceCoy.CoyName))
            {
                return BadRequest($"InsuranceCompany name: {insuranceCoy.CoyName} is already registered");
            }
            // Save logo image file to wwwroot/Images folder
            string wwwrootPath = webHostEnvironment.WebRootPath;
            string imagesFolder = Path.Combine(wwwrootPath, "images");
            string logoImageName = $"{Guid.NewGuid()}_logo_{logoImageFile.FileName}";
            string logoImagePath = Path.Combine(imagesFolder, logoImageName);

            using (var stream = new FileStream(logoImagePath, FileMode.Create))
            {
                await logoImageFile.CopyToAsync(stream);
            }

            // Set logo image URL
            insuranceCoy.CoyLogo = $"~/images/{logoImageName}";

            // Save display image file to wwwroot/Images folder
            string displayImageName = $"{Guid.NewGuid()}_display_{displayImageFile.FileName}";
            string displayImagePath = Path.Combine(imagesFolder, displayImageName);

            using (var stream = new FileStream(displayImagePath, FileMode.Create))
            {
                await displayImageFile.CopyToAsync(stream);
            }

            // Set display image URL
            insuranceCoy.CoyImage = $"~/images/{displayImageName}";

           
            return HandleOperationResult( await _insuranceCoyService.Add_Coy(insuranceCoy));
        }

        #endregion Create Brand Endpoint

        #region Update Insurance Coy
        // PUT: api/InsuranceCompany/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<InsuranceCoyDto>>> UpdateInsuranceCoy(int id,[FromForm]  InsuranceCoy model)
        {
            var insuranceCoy = await _insuranceCoyService.GetByInsuranceCoyId(id);

            if (insuranceCoy == null)
                return NotFound($"insuranceCoy: {model.CoyName} was not found");

            if (await _insuranceCoyService.CoyIsExist(model.CoyName))
                return BadRequest(" this InsuranceCompany name is already registred");


            insuranceCoy.CoyName = model.CoyName;
            await _insuranceCoyService.Update_Coy(insuranceCoy);

            return HandleOperationResult(ApiResult<InsuranceCoyDto>.Successful(null));
        }
        #endregion
        #region update only logo
        [HttpPut("{id}/logo")]
        public async Task<IActionResult> UpdateLogoImage(int id, [FromForm]IFormFile logoImageFile, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var insuranceCoy = (await _insuranceCoyService.GetByInsuranceCoyId(id));
            if (insuranceCoy == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(insuranceCoy.CoyLogo))
            {
                string wwwrootPathold = webHostEnvironment.WebRootPath;
                string logoImagePathold = Path.Combine(wwwrootPathold, insuranceCoy.CoyLogo.TrimStart('~', '/'));
                if (System.IO.File.Exists(logoImagePathold))
                {
                    System.IO.File.Delete(logoImagePathold);
                }
            }

            // Save logo image file to wwwroot/Images folder
            string wwwrootPath = webHostEnvironment.WebRootPath;
            string imagesFolder = Path.Combine(wwwrootPath, "images");
            string logoImageName = $"{Guid.NewGuid()}_logo_{logoImageFile.FileName}";
            string logoImagePath = Path.Combine(imagesFolder, logoImageName);

            using (var stream = new FileStream(logoImagePath, FileMode.Create))
            {
                await logoImageFile.CopyToAsync(stream);
            }

            // Set logo image URL
            insuranceCoy.CoyLogo = $"~/images/{logoImageName}";

            // Update InsuranceCoy in the database with the new logo image URL
            await _insuranceCoyService.Update_Coy(insuranceCoy);

            return NoContent();
        }

        #endregion

        #region Delete Category
        // DELETE: api/InsuranceCompany/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<InsuranceCoy>>> DeleteBrand(int id)
        {
            var insuranceCoy = (await _insuranceCoyService.GetByInsuranceCoyId(id));
            if (insuranceCoy == null)
            {
                return NotFound($"insuranceCoy:{insuranceCoy.CoyName}  has not found");
            }

            return HandleOperationResult(await _insuranceCoyService.Delete_Coy(insuranceCoy));

        }
        #endregion
    }
}