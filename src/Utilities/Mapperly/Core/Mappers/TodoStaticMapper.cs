using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riok.Mapperly.Abstractions;
using Utilities.Mapperly.Core.Dto;
using Utilities.Mapperly.Core.Models;

namespace Utilities.Mapperly.Core.Mappers;

[Mapper]
public static partial class TodoStaticMapper
{
    public static partial TodoDto MapToDto(this Todo todo);
}
