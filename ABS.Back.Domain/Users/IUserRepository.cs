﻿namespace ABS.Back.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync (Guid id, CancellationToken cancellationToken = default);
    void Add(User user);
}