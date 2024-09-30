using ABS.Back.Domain.Abstractions;

namespace ABS.Back.Domain.Users;

public sealed class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "The user was not found");
    
}