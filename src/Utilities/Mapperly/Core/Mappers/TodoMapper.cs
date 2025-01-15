using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;
using Utilities.Mapperly.Core.Dto;
using Utilities.Mapperly.Core.Mappers.Interfaces;
using Utilities.Mapperly.Core.Models;

namespace Utilities.Mapperly.Core.Mappers;

[Mapper]
public partial class TodoMapper : ITodoMapper
{
    public partial TodoDto MapToDto(Todo todo);
}
