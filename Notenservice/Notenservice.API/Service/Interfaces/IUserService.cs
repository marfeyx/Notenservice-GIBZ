using API.DTOs.Authentication;
using API.DTOs.User;
using Shared.DTOs;
using SHARED.DTOs.User;

namespace API.Services.Abstract;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterRequestDTO request);
    Task<string> LoginAsync(LoginRequestDTO request);
    Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    Task<UserDTO> GetUserByIdAsync(int id);
    Task<UserDTO> UpdateUserAsync(int id, UserUpdateDTO userUpdateDto);
    Task<bool> DeleteUserAsync(int id);
}
