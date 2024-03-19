using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Shiva_Enterprise_APIs.Entities;
using Shiva_Enterprise_APIs.Model;

namespace Shiva_Enterprise_APIs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomerController : ControllerBase
    {
        private ShivaEnterpriseContext _shivaEnterpriseContext;
        public CustomerController(ShivaEnterpriseContext shivaEnterpriseContext)
        {
            _shivaEnterpriseContext = shivaEnterpriseContext;
        }

        [HttpGet]
        [Route("GetCustomer")]
        public async Task<ActionResult> GetCustomer()
        {
            var customer = await _shivaEnterpriseContext.Customers.ToListAsync();

            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpGet]
        [Route("GetCustomerById")]
        public async Task<ActionResult> GetCustomerById(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            var customerData = await _shivaEnterpriseContext.Customers.FindAsync(customerId);
            if (customerData == null)
            {
                return BadRequest("No Customer Find");
            }
            return Ok(customerData);
        }


        [HttpPost]
        [Route("AddCustomer")]
        public async Task<ActionResult<Customer>> AddCustomer(CustomerModel customer)
        {
            try
            {
                if (customer is null)
                {
                    throw new ArgumentNullException(nameof(customer));
                }
                var CustomerDetail = new Customer()
                {
                    CustomerCode = customer.CustomerCode,
                    CustomerName = customer.CustomerName,
                    CustomerType = customer.CustomerType,
                    CustomerAddress = customer.CustomerAddress,
                    Phoneno = customer.Phoneno,
                    Email=customer.Email,
                    ContractStartDate=customer.ContractStartDate,
                    ContractEndDate=customer.ContractEndDate,
                    Remark=customer.Remark,
                    IsActive=customer.IsActive,
                    CustomerDiscount=customer.CustomerDiscount,
                    CreatedBy = customer.CreatedBy,
                    CreatedDateTime = customer.CreatedDateTime,
                };
                _shivaEnterpriseContext.Customers.Add(CustomerDetail);
                await _shivaEnterpriseContext.SaveChangesAsync();
                return Ok("Added Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("DeleteCustomer")]
        public async Task<ActionResult<ApiResponseFormat>> DeleteCustomer(Guid customerId)
        {
            var deleteCustomer = _shivaEnterpriseContext.Customers.Find(customerId);
            if (deleteCustomer != null)
            {
                _shivaEnterpriseContext.Entry(deleteCustomer).State = EntityState.Deleted;
                _shivaEnterpriseContext.SaveChanges();
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Something Went Wrong");
            }
            return StatusCode(StatusCodes.Status200OK, "Successfully deleted");
        }

        [HttpPut]
        [Route("EditCustomer")]
        public async Task<IActionResult> EditCustomerDetail(Guid id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _shivaEnterpriseContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _shivaEnterpriseContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shivaEnterpriseContext.Customers.Any(x => x.CustomerId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }
    }
}


