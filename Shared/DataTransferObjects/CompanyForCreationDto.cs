using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CompanyForCreationDto : CompanyForManipulationDto
    {
        [Required(ErrorMessage = "Company name is required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Address is 100 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Address is a required field.")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "Country is a required field.")]
        [MaxLength(3, ErrorMessage = "Maximum length for the country code is 3 characters.")]
        public string? Country { get; init; }

        public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
    }
        
}
