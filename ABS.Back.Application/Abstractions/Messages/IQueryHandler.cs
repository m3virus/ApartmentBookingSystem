using ABS.Back.Domain.Abstractions;
using MediatR;

namespace ABS.Back.Application.Abstractions.Messages;

public interface IQueryHandler<TQuery,TResponse>:IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{
    
}