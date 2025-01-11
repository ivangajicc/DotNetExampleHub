using MediatR;
using SharedKernel.Result;

namespace CleanArchitecture.UseCases.Abstractions;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
