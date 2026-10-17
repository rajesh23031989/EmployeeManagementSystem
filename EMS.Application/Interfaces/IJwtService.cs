using EMS.Domain.Entities;

public interface IJwtService
{
    string GenerateToken(AppUser user);
}