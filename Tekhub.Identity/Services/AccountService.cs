using System;
using System.Collections.Generic;
using AutoMapper;
using Tekhub.Framework;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;
using Tekhub.Identity.Repositories.Interfaces;
using Tekhub.Identity.Services.Interfaces;
using TekhubSocialInterfaces = Tekhub.Social.Facebook.Repository.Common.Interfaces;

namespace Tekhub.Identity.Services
{
    public class AccountService: IAccountService
    {
        private readonly IHashService _hashService;
        private readonly IPasswordService _passwordService;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly TekhubSocialInterfaces.IUserRepository _fbUserRepository;

        private User AddUser(User user, int userTypeId)
        {
            if (_userService.UserExists(user.Email))
            {
                throw new ApplicationException("User already registered");
            }

            user.UserType = _userRepository.GetUserType(userTypeId);

            user.Id = _userRepository.Add(Mapper.Map<User, UserDto>(user));
            _permissionRepository.AddUserPermissions(user.Id, user.UserType);

            return user;
        }

        public AccountService(IHashService hashService,
                                IPasswordService passwordService,
                                IUserService userService,
                                IUserRepository userRepository,
                                IPermissionRepository permissionRepository,
                                TekhubSocialInterfaces.IUserRepository fbUserRepository)
        {
            _hashService = hashService;
            _passwordService = passwordService;
            _userService = userService;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _fbUserRepository = fbUserRepository;

            Mapper.CreateMap<PermissionDto, Permission>();
        }

        public void Activate(string linkKey)
        {
            var userId = _passwordService.ActivatePassword(linkKey);
            _userService.UpdateRegistrationStatus(userId, RegistrationStatus.EmailVerified);
        }

        public User RegisterUser(User user, int userTypeId)
        {
            user.Password = _hashService.CreateHash(user.Password);
            return AddUser(user, userTypeId);
        }

        public User RegisterFacebookUser(string userFbAuthToken, User user, int userTypeId)
        {
            if (!_fbUserRepository.IsValidAuthToken(userFbAuthToken, user.Email))
            {
                throw new ApplicationException("User's authentication token does not match.");
            }

            return AddUser(user, userTypeId);
        }

        public List<PermissionDto> LogInFacebookUser(string userFbAuthToken, string email, int userTypeId)
        {
            if (!_fbUserRepository.IsValidAuthToken(userFbAuthToken, email))
            {
                throw new ApplicationException("User's authentication token does not match.");
            }

            var checkUser = _userService.GetUser(email);

            if (checkUser == null)
            {
                var fbUserDetails = _fbUserRepository.GetUserDetails(userFbAuthToken, new[] {"name, email"});
                var user = new User
                {
                    Email = fbUserDetails.Email,
                    Password = string.Empty,
                    RegistrationStatus = RegistrationStatus.EmailVerified,
                    Name = fbUserDetails.Name,
                    PhoneNumber = string.Empty,
                    IsFacebookUser = true
                };
                
                RegisterFacebookUser(userFbAuthToken, user, userTypeId);
                checkUser = _userService.GetUser(email);
            }

            if (checkUser.RegistrationStatus == RegistrationStatus.Guest)
            {
                throw new ApplicationException("User is not registered with us. Please register before login");
            }

            return Mapper.Map<List<PermissionDto>>(checkUser.Permissions);
        }

        public List<PermissionDto> LogIn(string email, string password)
        {
            var checkUser = _userService.GetUser(email);
            if (checkUser == null)
                throw new ApplicationException("UserNotfound");

            if (checkUser.RegistrationStatus == RegistrationStatus.Guest)
            {
                throw new ApplicationException("User is not registered with us. Please register before login");
            }


            var isPwdValid = _hashService.ValidateClearText(password, checkUser.Password);

            if (!isPwdValid)
                throw new ApplicationException("IncorrectAuthenticationDetails");

            return Mapper.Map<List<PermissionDto>>(checkUser.Permissions);
        }

        public void LogOut()
        {
            //Does nothing at the moment
            return;
        }

        public bool ConfirmEmail(string email)
        {
            var result = _userService.UserExists(email);
            if (!result)
            {
                return result;
            }

            var user = _userService.GetUser(email);
            _userService.UpdateRegistrationStatus(user.Id, RegistrationStatus.EmailVerified);
            return result;
        }
    }
}
