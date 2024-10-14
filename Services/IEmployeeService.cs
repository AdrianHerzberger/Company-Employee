using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<ShapedEntity> employees, MetaData metaData)> GetEmployeesAsync(Guid
             companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<EmployeeDto> GetEmployeeAsync(Guid employeeId, Guid id, bool trackChanges);
        Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges);
        Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges);
        Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, 
            EmployeeForUpdateDto employeeForUpdateDto, bool compTrackChanges, bool empTrackChanges);
        Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
            (Guid employeeId, Guid id, bool compTrackChanges, bool empTrackChanges);
        Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeForUpdate, Employee employeeEntity);
    }
}
