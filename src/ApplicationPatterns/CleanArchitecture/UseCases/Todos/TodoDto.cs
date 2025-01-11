using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.UseCases.Todos;

public record TodoDto(Guid Id, bool IsResolved, string Description);
