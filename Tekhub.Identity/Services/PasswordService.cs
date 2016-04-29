using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Tekhub.Framework;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;
using Tekhub.Identity.Services.Interfaces;

namespace Tekhub.Identity.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordResetService _passwordResetService;
        private readonly IUserService _userService;
        private readonly IHashService _hashService;

        public PasswordService(IPasswordResetService passwordResetService, IUserService userService, IHashService hashService)
        {
            _passwordResetService = passwordResetService;
            _userService = userService;
            _hashService = hashService;

            Mapper.CreateMap<User, UserDto>();
        }

        public void UpdatePassword(string newPassword, string passwordResetEntryKey)
        {
            var passwordLink = _passwordResetService.GetPasswordLink(passwordResetEntryKey);

            if (!_passwordResetService.IsKeyValid(passwordLink))
                throw new ApplicationException("Password key is invalid");

            var user = passwordLink.User;
            user.Password = _hashService.CreateHash(newPassword);

            var userDto = Mapper.Map<UserDto>(user);

            _userService.Update(user.Id, password:user.Password);
            _passwordResetService.UpdateLinkActivatedDate(passwordLink);

        }

        public long ActivatePassword(string activateLinkKey)
        {
            var passwordLink = _passwordResetService.GetPasswordLink(activateLinkKey);

            if (!_passwordResetService.IsKeyValid(passwordLink))
                throw new ApplicationException("Password key is invalid");

            _passwordResetService.UpdateLinkActivatedDate(passwordLink);

            return passwordLink.UserId;
        }

        public string RequestReset(string email, int passwordExpireInDays)
        {
            var theUser = _userService.GetUser(email);
            if (theUser == null)
                throw new ApplicationException("User with the email does not exist");

            return _passwordResetService.CreatePasswordResetForUser(theUser.Id, passwordExpireInDays).Key;
        }
    }
}
