using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.DataAccess;

namespace UrbanSpork.Tests.User
{
    public static class UserAggregateMockAggregate
    {
        public static UserAggregate SetupAdminUser()
        {

            var dto = new CreateUserInputDTO
            {
                FirstName = "test",
                LastName = "testLastName",
                Email = "test@email.com",
                Position = "testPosition",
                Department = "testDeapartment",
                IsAdmin = true,
                IsActive = true,

                PermissionList = new Dictionary<Guid, PermissionDetails>()
            };

            var agg = UserAggregate.CreateNewUser(dto);
            return agg;
        }

        public static UserAggregate SetupNonAdminUser()
        {

            var dto = new CreateUserInputDTO
            {
                FirstName = "test",
                LastName = "testLastName",
                Email = "test@email.com",
                Position = "testPosition",
                Department = "testDeapartment",
                IsAdmin = false,
                IsActive = true,

                PermissionList = new Dictionary<Guid, PermissionDetails>()
            };

            var agg = UserAggregate.CreateNewUser(dto);
            return agg;
        }

        public static PermissionAggregate SetupTestPermission()
        {
            CreateNewPermissionDTO input = new CreateNewPermissionDTO
            {
                Name = "test",
                Description = "test",
                IsActive = true
            };
            return PermissionAggregate.CreateNewPermission(input);
        }
    }
}
