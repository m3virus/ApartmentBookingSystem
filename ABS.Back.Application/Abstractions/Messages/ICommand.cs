using ABS.Back.Domain.Abstractions;
using MediatR;

namespace ABS.Back.Application.Abstractions.Messages;

public interface ICommand : IRequest<Result>, IBaseCommand
{
    
}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
    
}

public interface IBaseCommand
{

}