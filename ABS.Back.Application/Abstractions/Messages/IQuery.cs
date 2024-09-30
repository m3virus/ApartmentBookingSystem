using ABS.Back.Domain.Abstractions;
using MediatR;

namespace ABS.Back.Application.Abstractions.Messages;

public interface IQuery<TResponse>: IRequest<Result<TResponse>>
{
    
}