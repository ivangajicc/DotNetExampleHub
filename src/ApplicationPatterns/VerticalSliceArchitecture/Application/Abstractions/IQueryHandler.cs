using MediatR;
using SharedKernel.Result;

namespace VerticalSliceArchitecture.Application.Abstractions;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
