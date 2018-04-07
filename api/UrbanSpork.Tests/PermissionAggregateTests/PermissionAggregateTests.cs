using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using Xunit;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Events;

namespace UrbanSpork.Tests.PermissionAggregateTests
{
    public class PermissionAggregateTests
    {

        [Fact]
        public void given_new_permission_should_have_permission_created_event_with_non_empty_id ()
        {
            //assemble
            CreateNewPermissionDTO input = new CreateNewPermissionDTO
            {
                Name = "test",
                Description = "test",
                IsActive = true
            };

            //apply
            var agg = PermissionAggregate.CreateNewPermission(input);

            //assert
            var changes = agg.GetUncommittedChanges();
            Assert.Single(changes);
            Assert.Collection(changes, (e) =>
            {
                Assert.IsType<PermissionCreatedEvent>(e);
                var @event = (PermissionCreatedEvent) e;
                Assert.Equal("test", @event.Name);
                Assert.Equal("test", @event.Description);
            });

            Assert.NotEqual(Guid.Empty, agg.Id);
            Assert.Equal("test", agg.Name);
            Assert.Equal("test", agg.Description);

        }

        [Fact]
        public void given_new_permission_should_be_enabled_after_permission_created_event()
        {
            //assemble
            CreateNewPermissionDTO input = new CreateNewPermissionDTO
            {
                Name = "test",
                Description = "test",
                IsActive = true
            };

            //apply
            var agg = PermissionAggregate.CreateNewPermission(input);

            //assert
            var changes = agg.GetUncommittedChanges();
            Assert.Single(changes);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<PermissionCreatedEvent>(e);
                    var @event = (PermissionCreatedEvent)e;
                    Assert.True(@event.IsActive);
                });
            Assert.True(agg.IsActive);
        }

        [Fact]
        public void given_new_permission_should_be_disabled_after_permission_disabled_event()
        {
            //assemble
            CreateNewPermissionDTO input = new CreateNewPermissionDTO
            {
                Name = "test",
                Description = "test",
                IsActive = true
            };
            var agg = PermissionAggregate.CreateNewPermission(input);
            var adminUser = PermissionAggregateMockAggregate.SetupAdminUser();

            //apply
            agg.DisablePermission(adminUser);

            //assert
            var changes = agg.GetUncommittedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes, 
                (e) =>
                {
                    Assert.IsType<PermissionCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<PermissionDisabledEvent>(e);
                    var @event = (PermissionDisabledEvent) e;
                    Assert.False(@event.IsActive);
                });
            Assert.False(agg.IsActive);
        }

        [Fact]
        public void given_disabled_permission_should_be_enabled_after_permission_enabled_event()
        {
            //assemble
            CreateNewPermissionDTO input = new CreateNewPermissionDTO
            {
                Name = "test",
                Description = "test",
                IsActive = false
            };
            var agg = PermissionAggregate.CreateNewPermission(input);
            var adminUser = PermissionAggregateMockAggregate.SetupAdminUser();

            //apply
            agg.EnablePermission(adminUser);

            //Assert
            var changes = agg.GetUncommittedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<PermissionCreatedEvent>(e);
                    var @event = (PermissionCreatedEvent) e;
                    Assert.False(@event.IsActive);
                },
                (e) =>
                {
                    Assert.IsType<PermissionEnabledEvent>(e);
                    var @event = (PermissionEnabledEvent) e;
                    Assert.True(@event.IsActive);
                });
            Assert.True(agg.IsActive);
        }

        [Fact]
        public void given_new_permission_should_have_correct_values_after_update_permission_event()
        {
            //assemble
            CreateNewPermissionDTO input = new CreateNewPermissionDTO
            {
                Name = "test",
                Description = "test",
                IsActive = true
            };
            var agg = PermissionAggregate.CreateNewPermission(input);
            var userAgg = PermissionAggregateMockAggregate.SetupAdminUser();

            var updateInfo = new UpdatePermissionInfoDTO
            {
                Id = agg.Id,
                UpdatedById = userAgg.Id,
                Name = "New Name",
                Description = "New Description",
            };

            //apply
            agg.UpdatePermissionInfo(updateInfo);

            //assert
            var changes = agg.GetUncommittedChanges();
            Assert.Equal(2, changes.Length);
            Assert.Collection(changes,
                (e) =>
                {
                    Assert.IsType<PermissionCreatedEvent>(e);
                },
                (e) =>
                {
                    Assert.IsType<PermissionInfoUpdatedEvent>(e);
                    var @event = (PermissionInfoUpdatedEvent)e;
                    Assert.Equal(@event.Name, updateInfo.Name);
                    Assert.Equal(@event.Description, updateInfo.Description);
                });
            Assert.Equal(agg.Name, updateInfo.Name);
            Assert.Equal(agg.Description, updateInfo.Description);
        }
    }
}
