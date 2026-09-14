using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(Usuario usuario);
}