using MediatR;
using SharedKernel.Result;

namespace VerticalSliceArchitecture.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
