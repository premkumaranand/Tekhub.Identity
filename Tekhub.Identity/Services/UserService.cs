using System.Collections.Generic;
using System.Data.Entity.Core;
using AutoMapper;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;
using Tekhub.Identity.Repositories.Interfaces;
using Tekhub.Identity.Services.Interfaces;

namespace Tekhub.Identity.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;

        public UserService(IUserRepository userRepository, IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;

            Mapper.CreateMap<PermissionDto, Permission>();
            Mapper.CreateMap<UserDto, User>();
        }

        public User GetUser(long userId)
        {
            var userDto = _userRepository.Get(userId);
            return Mapper.Map<User>(userDto);
        }

        public User GetUser(string email)
        {
            var userDto = _userRepository.GetUserByEmail(email);
            return Mapper.Map<User>(userDto);
        }

        public bool IsGuest(User user)
        {
            return user.RegistrationStatus == RegistrationStatus.Guest;
        }

        public void UpdateRegistrationStatus(long userId, RegistrationStatus registrationStatus)
        {
            var user = _userRepository.Get(userId);

            user.RegistrationStatus = registrationStatus;

            _userRepository.Update(user);
        }

        public bool UserExists(string email)
        {
            return _userRepository.UserExists(email);
        }

        public void Update(UserDto dto)
        {
            _userRepository.Update(dto);
        }

        public void Update(long userId, string name = null, string email = null, string password = null,
            RegistrationStatus? registrationStatus = null, UserType userType = null, string phoneNumber = null)
        {
            var userDto = _userRepository.Get(userId);
            if (userDto == null)
                throw new ObjectNotFoundException();

            userDto.Name = name ?? userDto.Name;
            userDto.Email = email ?? userDto.Email;
            userDto.Password = password ?? userDto.Password;
            userDto.RegistrationStatus = registrationStatus ?? userDto.RegistrationStatus;
            userDto.UserType = userType ?? userDto.UserType;
            userDto.PhoneNumber = phoneNumber ?? userDto.PhoneNumber;

            _userRepository.Update(userDto);

            if (userDto.UserType == null && userType != null)
            {
                _permissionRepository.AddUserPermissions(userId, userType);
            }
        }

        public List<User> GetUsers(int userTypeId)
        {
            return _userRepository.GetUsers(userTypeId);
        }

        public void DeactivateUser(int userId)
        {
            _userRepository.ChangeUserStatus(userId, isActive: false);
        }

        public void ActivateUser(int userId)
        {
            _userRepository.ChangeUserStatus(userId, isActive: true);
        }
    }
}
