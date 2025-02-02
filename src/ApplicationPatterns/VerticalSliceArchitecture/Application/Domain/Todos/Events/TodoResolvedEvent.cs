using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VerticalSliceArchitecture.Application.Domain.Todos.Events;

internal record TodoResolvedEvent(Guid TodoId) : INotification;
