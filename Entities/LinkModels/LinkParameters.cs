using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;


namespace Entities.LinkModels
{
    public record LinkParameters(EmployeeParameters EmployeeParameters, HttpContext Context);
}
