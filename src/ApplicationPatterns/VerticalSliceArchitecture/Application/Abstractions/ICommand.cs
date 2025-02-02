using MediatR;
using SharedKernel.Result;

namespace VerticalSliceArchitecture.Application.Abstractions;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
