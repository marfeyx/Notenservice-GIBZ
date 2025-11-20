using API.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Notenservice.API.Service;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtUtils _jwtUtils;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork uow, IJwtUtils jwtUtils, IPasswordHasher<User> passwordHasher, IMapper mapper)
    {
        _unitOfWork = uow ?? throw new ArgumentNullException(nameof(uow));
        _jwtUtils = jwtUtils ?? throw new ArgumentNullException(nameof(jwtUtils));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<string> RegisterAsync(RegisterRequestDTO request)
    {
        if (await _unitOfWork.Users.GetByEmailAsync(request.Email) != null)
        {
            throw new Exception("Email already in use");
        }

        if (await _unitOfWork.Users.GetByUsernameAsync(request.Username) != null)
        {
            throw new Exception("Username already in use.");
        }

        User newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            CreatedAt = DateTime.Now
        };

        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);

        Role customerRole = await _unitOfWork.Roles.GetByNameAsync("Customer");

        if (customerRole == null)
            throw new Exception("Customer role not found.");

        newUser.RoleId = customerRole.Id;

        await _unitOfWork.Users.AddAsync(newUser);
        await _unitOfWork.CompleteAsync();

        return _jwtUtils.GenerateJwtToken(newUser);
    }

    public async Task<string> LoginAsync(LoginRequestDTO request)
    {
        User user = await _unitOfWork.Users.GetByUsernameAsync(request.Username);

        if (user == null)
            throw new Exception("Invalid username");

        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid username or password");

        if (result == PasswordVerificationResult.Success)
            return _jwtUtils.GenerateJwtToken(user);

        throw new Exception("Unexpected failure during login process.");
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        IEnumerable<User> users = await _unitOfWork.Users.GetAllIncludingAsync(o => o.Role);
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        User user = await _unitOfWork.Users.GetByIdIncludingAsync(id, o => o.Role);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> UpdateUserAsync(int id, UserUpdateDTO userUpdateDto)
    {
        User user = await _unitOfWork.Users.GetByIdIncludingAsync(id, o => o.Role);
        if (user == null)
        {
            return null;
        }

        _mapper.Map(userUpdateDto, user);
        _unitOfWork.Users.Update(user);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        User user = await _unitOfWork.Users.GetByIdIncludingAsync(id, o => o.Role);
        if (user == null)
        {
            return false;
        }

        _unitOfWork.Users.Remove(user);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
