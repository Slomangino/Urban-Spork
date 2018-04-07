using System;
using System.Collections.Generic;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.DataAccess;

namespace UrbanSpork.Tests.PermissionAggregateTests
{
    public static class PermissionAggregateMockAggregate
    {
        public static UserAggregate SetupAdminUser()
        {

            var dto = new CreateUserInputDTO
            {
                FirstName = "Stephen",
                LastName = "Lomangino",
                Email = "stephen@test.com",
                Position = "API Lead",
                Department = "Development",
                IsAdmin = true,
                IsActive = true,

                PermissionList = { }
            };

            var agg = UserAggregate.CreateNewUser(dto);
            return agg;
        }
    }
}
