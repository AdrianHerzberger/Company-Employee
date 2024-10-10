using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetEmployeesFromCompany(Guid companyId)
        {
            var employee = _service.EmployeeService.GetEmployees(companyId, trackChanges:false);
            return Ok (employee);
        }

        [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges:false);
            return Ok (employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
            {
                return BadRequest("EmployeeForCreationDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var employeeToReturn = _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            _service.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id,
            [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee is null)
            {
                return BadRequest("EmployeeForUpdateDto object is null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            _service.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee,
                    compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public IActionResult PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id,
            [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
            {
                return BadRequest("patchDoc object sent from client is null.");
            }

            var result = _service.EmployeeService.GetEmployeeForPatch(companyId, id, compTrackChanges: false, empTrackChanges: true);

            patchDoc.ApplyTo(result.employeeToPatch, ModelState);

            if (!TryValidateModel(result.employeeToPatch))
            {
                return UnprocessableEntity(ModelState);
            }

            _service.EmployeeService.SaveChangesForPatch(result.employeeToPatch, result.employeeEntity);

            return NoContent();
        }


    }
}
