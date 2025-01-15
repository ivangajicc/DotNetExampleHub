using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Mapperly.Core.Dto;
using Utilities.Mapperly.Core.Models;

namespace Utilities.Mapperly.Core.Mappers.Interfaces;

public interface ITodoMapper
{
    public TodoDto MapToDto(Todo todo);
}
