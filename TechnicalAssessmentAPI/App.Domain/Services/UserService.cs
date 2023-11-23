using App.Config.Classes.Base;
using App.Config.DTO;
using App.Config.Helpers;
using App.Domain.Interfaces;
using App.Infrastructure.Database.Entities;
using App.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace App.Domain.Services
{
    public class UserService : BaseService<UsersDTO, Users>, IUsersService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public UserService(
            IUserRepository repository,
            IMapper mapper,
            IConfiguration configuration
        )
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<UsersDTO>> GetAllUsers()
        {
            var result = await _repository.GetAllAsync();
            if (result == null)
                return new List<UsersDTO>();

            List<UsersDTO> users = _mapper.Map<List<UsersDTO>>(result);

            users = users
                .Select(
                    x =>
                        new UsersDTO()
                        {
                            Id = x.Id,
                            UserName = x.UserName,
                            RegisterDate = x.RegisterDate
                        }
                )
                .ToList();

            users = users.OrderBy(x => x.Id).ToList();

            return users;
        }
        public UsersDTO LoginUser(LoginDTO login)
        {
            if (login == null)
                throw new ArgumentException("El DTO no puede ser nulo");

            if (
                string.IsNullOrWhiteSpace(login.UserName)
                || string.IsNullOrWhiteSpace(login.Password)
            )
                throw new ArgumentException("El usuario o contraseña no pueden ser nulos");

            Users result = _repository.LoginUser(login.UserName);
            if (result == null || result.Id == 0)
                throw new ArgumentException("El usuario ingresado no existe");

            UsersDTO user = _mapper.Map<UsersDTO>(result);
            if (user.Id != 0)
            {
                bool validatePassword = ValidatePassword(
                    login.Password,
                    (user.Password ?? string.Empty)
                );
                if (validatePassword)
                    user.Token = ValidateToken.ValidateTokenUser(user, _configuration);
                else
                    throw new ArgumentException("La contraseña o el usuario son incorrectos");
            }

            return user;
        }
        public async Task<int> CreateUser(UsersDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
                throw new ArgumentException("El nombre de usuario no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("La contraseña del usuario no puede ser nula.");

            user.Password = GenerateHashPassword(user.Password);

            var userEntity = _mapper.Map<Users>(user);
            return await _repository.CreateAsync(userEntity);
        }
        private string GenerateHashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword ?? string.Empty;
        }
        private bool ValidatePassword(string password, string passwordBD)
        {
            bool passwordValidate = BCrypt.Net.BCrypt.Verify(password, passwordBD);
            return passwordValidate;
        }
    }
}
