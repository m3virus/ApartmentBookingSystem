using System.Diagnostics.CodeAnalysis;

namespace ABS.Back.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }
        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        Error = error;
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<TEntity> Success<TEntity>(TEntity entity) => new(entity, true, Error.None);
    public static Result<TEntity> Failure<TEntity>(Error error) => new(default!, false, error);

    public static Result<TEntity> Create<TEntity>(TEntity entity) =>
        entity is not null ? Success(entity) : Failure<TEntity>(Error.NullVallue);
}

public class Result<TEntity> : Result
{
    private readonly TEntity _entity;
    protected internal Result(TEntity entity, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _entity = entity;
    }

    [NotNull]
    public TEntity Entity => IsSuccess ? _entity! :
        throw new InvalidOperationException("The Value Cannot Be Acceptable");

    public static implicit operator Result<TEntity>(TEntity entity) => Create(entity);
}