using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;
using Tekhub.Identity.Repositories.Interfaces;

namespace Tekhub.Identity.Repositories
{
    public class PermissionRepository: IPermissionRepository
    {
        internal readonly IUserManagementDbContext Context;

        public PermissionRepository(IUserManagementDbContext context)
        {
            Context = context;

            Mapper.CreateMap<Permission, PermissionDto>();
        }

        public List<PermissionDto> GetByUserType(UserType userType)
        {
            return
                Mapper.Map<List<Permission>, List<PermissionDto>>(
                    Context.Permissions.Where(p => p.UserType.Id == userType.Id).ToList());
        }

        public void AddUserPermissions(long userId, UserType userType)
        {
            var theUser = Context.Users.Include("Permissions").SingleOrDefault(u => u.Id == userId);
            if (theUser.Permissions == null)
            {
                theUser.Permissions = new List<Permission>();
            }

            var newPermissions = Context.Permissions.Where(p => p.UserType.Id == userType.Id).ToList();
            theUser.Permissions.AddRange(newPermissions);

            Context.SaveChanges();
        }

        public int Add(PermissionDto dto)
        {
            throw new System.NotImplementedException();
        }

        public List<Permission> Get(List<int> permissionIds)
        {
            return Context.Permissions.Where(p => permissionIds.Contains(p.Id)).ToList();
        }

        public List<PermissionDto> Get()
        {
            return Mapper.Map<List<Permission>, List<PermissionDto>>(Context.Permissions.ToList());
        }

        public IReadOnlyList<int> AddRange(IEnumerable<PermissionDto> dtos)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public PermissionDto Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<PermissionDto> GetRange(IEnumerable<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public void Update(PermissionDto dto)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(PermissionDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
