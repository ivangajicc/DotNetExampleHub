using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Utilities.AutoMapper.Core.Dto;
using Utilities.AutoMapper.Core.Models;

namespace Utilities.AutoMapper.Core.MappingProfiles;

// Here for simplicity we merged domain and app layer. Those mapping profiles and auto mapper reference itself is recommanded to leave in application layer.
public class TodoMappingProfile : Profile
{
    public TodoMappingProfile() => CreateMap<Todo, TodoDto>();
}
