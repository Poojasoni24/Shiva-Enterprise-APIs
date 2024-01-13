using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public CompanyController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
           _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetCompanies")]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _shivaEnterpriseContext.Companies.ToListAsync();
            if (companies == null) { return NotFound(); }

            return Ok(companies);
        }

        [HttpGet]
        [Route("GetCompanyById")]
        public async Task<ActionResult<Company>> GetCompanyById(Guid companyId)
        {
            var companies = await _shivaEnterpriseContext.Companies.FindAsync(companyId);
            if (companies == null)
            {
                return BadRequest("No company Find");
            }
            return Ok(companies);
        }

        [HttpPost]
        [Route("AddCompany")]
        public async Task<ActionResult<Company>> AddCompany(Company company)
        {
            var companyDetail = new Company()
            {
                Company_Code = company.Company_Code,
                Company_Name = company.Company_Name,
                Company_Startyear = company.Company_Startyear,
                Company_Endyear = company.Company_Endyear,
                IsActive = company.IsActive,
            };

            _shivaEnterpriseContext.Companies.Add(companyDetail);
            await _shivaEnterpriseContext.SaveChangesAsync();
            if (companyDetail.Company_Id == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        //[HttpPut]
        //[Route("updateCompany")]
        //public async Task<ActionResult<Company>> UpdateCompany(Company company)
        //{
        //    var companyData = companies.Find(x => x.Id == company.Id);
        //    if (companyData == null)
        //    {
        //        return BadRequest("No company Find");
        //    }
        //    companyData.Id = company.Id;
        //    companyData.CompanyCode = company.CompanyCode;
        //    companyData.CompanyName = company.CompanyName;
        //    companyData.BillingName = company.BillingName;
        //    companyData.IsActive = company.IsActive;
        //    companyData.IsDeleted = company.IsActive;
        //    return Ok(companyData);
        //}

        [HttpPost]
        [Route("DeleteCompany")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteCompany(Guid companyId)
        {
            var company = _shivaEnterpriseContext.Companies.Find(companyId);
            if (company != null)
            {
                _shivaEnterpriseContext.Entry(company).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditCompany")]
        public async Task<IActionResult> EditCompany(Guid id, Company company)
        {
            if (id != company.Company_Id)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(company).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Companies.Any(x=>x.Company_Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    }
}
