using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UrbanSpork.Common.DataTransferObjects.Permission;
using Xunit;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Events;

namespace UrbanSpork.Tests.Permission
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
                Assert.True(@event.IsActive);
            });

            Assert.NotEqual(Guid.Empty, agg.Id);
            Assert.Equal("test", agg.Name);
            Assert.Equal("test", agg.Description);
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

            //apply
            agg.DisablePermission();

            //assert
            var changes = agg.GetUncommittedChanges();
            Assert.Equal(2, changes.Count());
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
    }
}
