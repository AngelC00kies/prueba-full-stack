using PruebaTecnica.Application.Common;
using PruebaTecnica.Application.DTOs.Auth;

namespace PruebaTecnica.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(LoginDto dto);
    Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto dto);
}
