using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Abstractions;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
