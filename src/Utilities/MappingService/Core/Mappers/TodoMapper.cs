using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MappingService.Core.Dto;
using Utilities.MappingService.Core.Models;

namespace Utilities.MappingService.Core.Mappers;

public class TodoMapper : IMapper<Todo, TodoDto>
{
    public TodoDto Map(Todo entity) => new(entity.Description, entity.Priority);
}
