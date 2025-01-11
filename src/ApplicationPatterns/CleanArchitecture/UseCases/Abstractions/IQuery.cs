using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
