using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;
using Tekhub.Identity.Repositories.Interfaces;

namespace Tekhub.Identity.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly IUserManagementDbContext _context;

        public UserRepository(IUserManagementDbContext context)
        {
            _context = context;
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<Permission, PermissionDto>();
            Mapper.CreateMap<UserDto, User>();
        }

        public long Add(UserDto dto)
        {
            dto.Id = 0;
            var user = Mapper.Map<User>(dto);

            user = _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public IReadOnlyList<long> AddRange(IEnumerable<UserDto> dtos)
        {
            throw new NotImplementedException();
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public UserDto Get(long id)
        {
            var user = _context.Users.Find(id);
            return user == null
                ? null
                : Mapper.Map<UserDto>(user);
        }

        public IReadOnlyList<UserDto> GetRange(IEnumerable<long> ids)
        {
            throw new NotImplementedException();
        }

        public void Update(UserDto dto)
        {
            var user = Mapper.Map<User>(dto);

            _context.Users.AddOrUpdate(u => new { u.Id }, user);
            _context.SaveChanges();
        }

        public void Delete(UserDto dto)
        {
            var user = Mapper.Map<User>(dto);

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public UserType GetUserType(int userTypeId)
        {
            return _context.UserTypes.SingleOrDefault(u => u.Id == userTypeId);
        }

        public UserDto GetUserByEmail(string email)
        {
            var user =
                _context.Users.Include("Permissions")
                    .SingleOrDefault(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            return user == null
                ? null
                : Mapper.Map<UserDto>(user);
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }


        public List<User> GetUsers(int userTypeId)
        {
            return _context.Users.Where(u => u.UserType.Id == userTypeId).ToList();
        }


        public void ChangeUserStatus(int userId, bool isActive)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            user.IsActive = isActive;

            _context.Users.AddOrUpdate(u => new { u.Id }, user);
            _context.SaveChanges();
        }
    }
}
