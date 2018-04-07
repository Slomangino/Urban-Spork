using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.Common.ExceptionHandling.Exceptions;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Events;
using UrbanSpork.DataAccess.Events.Users;
using Xunit;

namespace UrbanSpork.Tests.User
{
    public class UserAggregateTests
    {
        // Assemble
        // Apply
        // Assert
        [Fact]
        public void given_create_user_should_have_user_created_event_with_non_empty_id()
        {
            // Assemble
            CreateUserInputDTO input = new CreateUserInputDTO
            {
                FirstName = "test",
                LastName = "testLastName",
                Email = "testEmail",
                Position = "testPosition",
                Department = "testDepartment",
                IsAdmin = true,
                IsActive = true,
                PermissionList = new Dictionary<Guid, PermissionDetails>()
            };

            // Apply
            var agg = UserAggregate.CreateNewUser(input);
            
            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Single(changes);
            Assert.Collection(changes, (e) =>
            {
                Assert.IsType<UserCreatedEvent>(e);
                var @event = (UserCreatedEvent) e;
                Assert.Equal(1, @event.Version);
                Assert.NotEqual(Guid.Empty, @event.Id);

                Assert.Equal(input.FirstName, @event.FirstName);
                Assert.Equal(input.LastName, @event.LastName);
                Assert.Equal(input.Email, @event.Email);
                Assert.Equal(input.Position, @event.Position);
                Assert.Equal(input.Department, @event.Department);
                Assert.True(@event.IsAdmin);
                Assert.True(@event.IsActive);
            });

            Assert.NotEqual(Guid.Empty, agg.Id);
            Assert.Equal(input.FirstName, agg.FirstName);
            Assert.Equal(input.LastName, agg.LastName);
            Assert.Equal(input.Email, agg.Email);
            Assert.Equal(input.Position, agg.Position);
            Assert.Equal(input.Department, agg.Department);
            Assert.True(agg.IsAdmin);
            Assert.True(agg.IsActive);
        }

        
        
        [Fact]
        public void given_create_user_with_bad_input_should_throw_CreateUserDataNotFoundException()
        {
            // Assemble
            CreateUserInputDTO input = new CreateUserInputDTO();
            UserAggregate agg;

            
            // Apply / Assert
            Assert.Throws<CreateUserDataNotFoundException>(() => agg = UserAggregate.CreateNewUser(input));
        }

        [Fact]
        public void given_new_user_should_have_correct_values_after_UserUpdatedEvent()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            UpdateUserInformationDTO input = new UpdateUserInformationDTO
            {
                ForID = agg.Id,
                FirstName = "ChangedFName",
                LastName = "ChangedLName",
                Email = "ChangedEmail",
                Position = "ChangedPosition",
                Department = "ChangedDepartment",
                IsAdmin = false
            };


            // Apply
            agg.UpdateUserInfo(input);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserUpdatedEvent>(e);
                    var @event = (UserUpdatedEvent) e;
                    Assert.Equal(2, @event.Version);
                    Assert.NotEqual(Guid.Empty, @event.Id);

                    Assert.Equal(@event.FirstName, agg.FirstName);
                    Assert.Equal(@event.LastName, agg.LastName);
                    Assert.Equal(@event.Email, agg.Email);
                    Assert.Equal(@event.Position, agg.Position);
                    Assert.Equal(@event.Department, agg.Department);
                    Assert.Equal(@event.IsAdmin, agg.IsAdmin);
                });

            Assert.Equal(2, agg.Version);
            Assert.Equal(input.FirstName, agg.FirstName);
            Assert.Equal(input.LastName, agg.LastName);
            Assert.Equal(input.Email, agg.Email);
            Assert.Equal(input.Position, agg.Position);
            Assert.Equal(input.Department, agg.Department);
            Assert.Equal(input.IsAdmin, agg.IsAdmin);
        }

        [Fact]
        public void given_active_user_should_be_inactive_after_UserDisabledEvent()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();

            // Apply
            agg.DisableSingleUser(agg);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserDisabledEvent>(e);
                    var @event = (UserDisabledEvent) e;

                    Assert.Equal(2, @event.Version);
                    Assert.NotEqual(Guid.Empty, @event.Id);

                    Assert.False(@event.IsActive);
                    Assert.Equal(@event.ByAgg, agg.Id);

                });

            Assert.False(agg.IsActive);
        }

        [Fact]
        public void given_inactive_user_should_be_active_after_UserEnabledEvent()
        {
            // Assemble
            var testAgg = UserAggregateMockAggregate.SetupAdminUser();
            var adminAgg = UserAggregateMockAggregate.SetupAdminUser();

            // Apply
            testAgg.DisableSingleUser(adminAgg);
            testAgg.EnableSingleUser(adminAgg);

            // Assert
            var changes = testAgg.FlushUncommitedChanges();
            Assert.Equal(3, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserDisabledEvent>(e);
                    var @event = (UserDisabledEvent)e;

                    Assert.Equal(2, @event.Version);
                    Assert.NotEqual(Guid.Empty, @event.Id);

                    Assert.False(@event.IsActive);
                    Assert.Equal(@event.ByAgg, adminAgg.Id);

                },
                (e) =>
                {
                    Assert.IsType<UserEnabledEvent>(e);
                    var @event = (UserEnabledEvent)e;

                    Assert.Equal(3, @event.Version);
                    Assert.NotEqual(Guid.Empty, @event.Id);

                    Assert.True(@event.IsActive);
                    Assert.Equal(@event.ByAgg, adminAgg.Id);
                });

            Assert.True(testAgg.IsActive);
        }

        [Fact]
        public void user_permission_list_should_contain_requested_permission_after_UserPermissionRequestedEvent()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            var permissionAgg = UserAggregateMockAggregate.SetupTestPermission();

            RequestUserPermissionsDTO requestInput = new RequestUserPermissionsDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                Requests = new Dictionary<Guid, PermissionDetails>
                {
                    {permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "TestReason"
                        }
                    }
                },
            };

            // Apply
            agg.UserRequestedPermissions(new List<PermissionAggregate> { permissionAgg }, requestInput);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserPermissionsRequestedEvent>(e);
                    var @event = (UserPermissionsRequestedEvent) e;

                    Assert.NotEqual(Guid.Empty, @event.Id);
                    Assert.Equal(2, @event.Version);
                    Assert.NotEqual(new DateTime(), @event.TimeStamp);

                    Assert.True(@event.Requests.Any());
                    Assert.True(@event.Requests.ContainsKey(permissionAgg.Id));
                    Assert.Equal(@event.Requests[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionsRequestedEvent).FullName));
                    Assert.Equal(@event.Requests[permissionAgg.Id].Reason, requestInput.Requests[permissionAgg.Id].Reason);
                    Assert.True(@event.Requests[permissionAgg.Id].IsPending);
                    //Assert.NotEqual(@event.Requests[permissionAgg.Id].RequestDate, new DateTime());
                    Assert.Equal(@event.Requests[permissionAgg.Id].RequestedBy, agg.Id);
                    Assert.Equal(@event.Requests[permissionAgg.Id].RequestedFor, agg.Id);
                });

            Assert.True(agg.PermissionList.ContainsKey(permissionAgg.Id));
            Assert.Equal(agg.PermissionList[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionsRequestedEvent).FullName));
        }

        [Fact]
        public void user_permission_list_should_not_contain_requested_permission_after_UserPermissionRequestedEvent_with_inactive_permission()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            var permissionAgg = UserAggregateMockAggregate.SetupTestPermission();
            permissionAgg.DisablePermission(agg);

            RequestUserPermissionsDTO requestInput = new RequestUserPermissionsDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                Requests = new Dictionary<Guid, PermissionDetails>
                {
                    {permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "TestReason"
                        }
                    }
                },
            };

            // Apply
            agg.UserRequestedPermissions(new List<PermissionAggregate> { permissionAgg }, requestInput);

            // Assert
            Assert.False(agg.PermissionList.Any());
        }

        [Fact]
        public void user_permission_list_should_have_permission_request_denied_after_UserPermissionRequestDeniedEvent()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            var permissionAgg = UserAggregateMockAggregate.SetupTestPermission();

            DenyUserPermissionRequestDTO input = new DenyUserPermissionRequestDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                PermissionsToDeny = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "test reason"
                        }
                    }
                }
            };

            // Apply
            agg.DenyPermissionRequest(agg, new List<PermissionAggregate>{permissionAgg}, input);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserPermissionRequestDeniedEvent>(e);
                    var @event = (UserPermissionRequestDeniedEvent)e;

                    Assert.NotEqual(Guid.Empty, @event.Id);
                    Assert.Equal(2, @event.Version);
                    Assert.Equal(agg.Id, @event.ForId);
                    Assert.Equal(agg.Id, @event.ById);
                    Assert.True(@event.PermissionsToDeny.ContainsKey(permissionAgg.Id));
                    Assert.Equal(@event.PermissionsToDeny[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionRequestDeniedEvent).FullName));

                }
            );

            Assert.Equal(agg.PermissionList[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionRequestDeniedEvent).FullName));
        }

        [Fact]
        public void user_permission_list_should_have_granted_permission_after_UserPermissionGrantedEvent()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            var permissionAgg = UserAggregateMockAggregate.SetupTestPermission();

            GrantUserPermissionDTO input = new GrantUserPermissionDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                PermissionsToGrant = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "test Reason"
                        }
                    }
                }
            };

            // Apply
            agg.GrantPermission(agg, new List<PermissionAggregate>{ permissionAgg }, input);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserPermissionGrantedEvent>(e);
                    var @event = (UserPermissionGrantedEvent) e;
                    Assert.NotEqual(Guid.Empty, @event.Id);
                    Assert.Equal(2, @event.Version);

                    Assert.Equal(agg.Id, @event.ForId);
                    Assert.Equal(agg.Id, @event.ById);
                    Assert.True(@event.PermissionsToGrant.ContainsKey(permissionAgg.Id));
                    Assert.Equal(@event.PermissionsToGrant[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionGrantedEvent).FullName));
                }
            );

            Assert.True(agg.PermissionList.ContainsKey(permissionAgg.Id));
            Assert.Equal(agg.PermissionList[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionGrantedEvent).FullName));
        }

        [Fact]
        public void user_permission_list_should_not_have_granted_permission_after_UserPermissionGrantedEvent_with_inactive_permission()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            var permissionAgg = UserAggregateMockAggregate.SetupTestPermission();
            permissionAgg.DisablePermission(agg);

            GrantUserPermissionDTO input = new GrantUserPermissionDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                PermissionsToGrant = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "test Reason"
                        }
                    }
                }
            };

            // Apply
            agg.GrantPermission(agg, new List<PermissionAggregate> {permissionAgg}, input);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Single(changes);
            Assert.False(agg.PermissionList.Any());
        }

        [Fact]
        public void user_permission_list_should_have_revoked_permission_after_UserPermissionRevokedEvent_when_permission_was_previously_granted()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            var permissionAgg = UserAggregateMockAggregate.SetupTestPermission();

            GrantUserPermissionDTO input = new GrantUserPermissionDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                PermissionsToGrant = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "test grant Reason"
                        }
                    }
                }
            };
            agg.GrantPermission(agg, new List<PermissionAggregate> { permissionAgg }, input);

            RevokeUserPermissionDTO revokeInput = new RevokeUserPermissionDTO()
            {
                ForId = agg.Id,
                ById = agg.Id,
                PermissionsToRevoke = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "test revoke reason"
                        }
                    }
                }
            };
            // Apply
            agg.RevokePermission(agg, revokeInput);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Equal(3, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserPermissionGrantedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserPermissionRevokedEvent>(e);
                    var @event = (UserPermissionRevokedEvent) e;
                    Assert.NotEqual(Guid.Empty, @event.Id);
                    Assert.Equal(3, @event.Version);

                    Assert.Equal(agg.Id, @event.ForId);
                    Assert.Equal(agg.Id, @event.ById);
                    Assert.True(@event.PermissionsToRevoke.ContainsKey(permissionAgg.Id));
                    Assert.Equal(@event.PermissionsToRevoke[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionRevokedEvent).FullName));

                });

            Assert.True(agg.PermissionList.ContainsKey(permissionAgg.Id));
            Assert.Equal(agg.PermissionList[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionRevokedEvent).FullName));
        }

        /*
         * This test is pretty hacky and might break when some better validation goes into revoking permisison, currently
         * if you get to revoke a permission that isnt in the Users's Permission list, it will throw a keyNotFound exception -__-
         */
        [Fact]
        public void user_permission_list_should_not_have_revoked_permission_after_UserPermissionRevokedEvent_when_permission_was_not_previously_granted()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            var permissionAgg = UserAggregateMockAggregate.SetupTestPermission();

            RevokeUserPermissionDTO revokeInput = new RevokeUserPermissionDTO()
            {
                ForId = agg.Id,
                ById = agg.Id,
                PermissionsToRevoke = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "test revoke reason"
                        }
                    }
                }
            };

            RequestUserPermissionsDTO requestInput = new RequestUserPermissionsDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                Requests = new Dictionary<Guid, PermissionDetails>
                {
                    {permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "TestReason"
                        }
                    }
                },
            };
            
            agg.UserRequestedPermissions(new List<PermissionAggregate> { permissionAgg }, requestInput);

            // Apply
            agg.RevokePermission(agg, revokeInput);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserPermissionsRequestedEvent>(e);
                }
            );

            Assert.True(agg.PermissionList.ContainsKey(permissionAgg.Id));
            Assert.Equal(agg.PermissionList[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionsRequestedEvent).FullName));
        }

        [Fact]
        public void user_permission_list_should_not_have_revoked_permission_after_UserPermissionRevokedEvent_when_byAgg_is_not_an_admin()
        {
            // Assemble
            var agg = UserAggregateMockAggregate.SetupAdminUser();
            var nonAdminAgg = UserAggregateMockAggregate.SetupNonAdminUser();
            var permissionAgg = UserAggregateMockAggregate.SetupTestPermission();

            GrantUserPermissionDTO input = new GrantUserPermissionDTO
            {
                ForId = agg.Id,
                ById = agg.Id,
                PermissionsToGrant = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "test grant Reason"
                        }
                    }
                }
            };

            agg.GrantPermission(agg, new List<PermissionAggregate> { permissionAgg }, input);

            RevokeUserPermissionDTO revokeInput = new RevokeUserPermissionDTO()
            {
                ForId = agg.Id,
                ById = nonAdminAgg.Id,
                PermissionsToRevoke = new Dictionary<Guid, PermissionDetails>
                {
                    {
                        permissionAgg.Id, new PermissionDetails
                        {
                            Reason = "test revoke reason"
                        }
                    }
                }
            };

            // Apply
            agg.RevokePermission(nonAdminAgg, revokeInput);

            // Assert
            var changes = agg.FlushUncommitedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<UserCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<UserPermissionGrantedEvent>(e);
                }
            );

            Assert.True(agg.PermissionList.ContainsKey(permissionAgg.Id));
            Assert.Equal(agg.PermissionList[permissionAgg.Id].EventType, JsonConvert.SerializeObject(typeof(UserPermissionGrantedEvent).FullName));
        }
    }
}
// Assemble
// Apply
// Assert