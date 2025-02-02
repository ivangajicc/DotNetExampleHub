using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerticalSliceArchitecture.Application.Features.Todos;

public record TodoResponseDto(Guid Id, bool IsResolved, string Description);
