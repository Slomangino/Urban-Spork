﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using UrbanSpork.DataAccess.DataAccess;

namespace UrbanSpork.DataAccess.Migrations
{
    [DbContext(typeof(UrbanDbContext))]
    partial class UrbanDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("UrbanSpork.DataAccess.DataAccess.EventStoreDataRow", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<int>("Version");

                    b.Property<string>("EventData");

                    b.Property<string>("EventType");

                    b.HasKey("Id", "Version");

                    b.ToTable("events");
                });

            modelBuilder.Entity("UrbanSpork.DataAccess.Projections.ApproverActivityProjection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ApproverId");

                    b.Property<string>("ForFullName");

                    b.Property<Guid>("ForId");

                    b.Property<string>("PermissionName");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("TruncatedEventType");

                    b.HasKey("Id");

                    b.ToTable("approveractivityprojection");
                });

            modelBuilder.Entity("UrbanSpork.DataAccess.Projections.PendingRequestsProjection", b =>
                {
                    b.Property<Guid>("PermissionId");

                    b.Property<Guid>("ForId");

                    b.Property<string>("RequestType");

                    b.Property<string>("ByFirstName");

                    b.Property<string>("ByFullName");

                    b.Property<Guid>("ById");

                    b.Property<string>("ByLastName");

                    b.Property<DateTime>("DateOfRequest")
                        .HasColumnType("timestamp");

                    b.Property<string>("ForFirstName");

                    b.Property<string>("ForFullName");

                    b.Property<string>("ForLastName");

                    b.Property<string>("PermissionName");

                    b.HasKey("PermissionId", "ForId", "RequestType");

                    b.ToTable("pendingrequestsprojection");
                });

            modelBuilder.Entity("UrbanSpork.DataAccess.Projections.PermissionDetailProjection", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("PermissionId");

                    b.ToTable("permissiondetailprojection");
                });

            modelBuilder.Entity("UrbanSpork.DataAccess.Projections.SystemActivityProjection", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ByFullName");

                    b.Property<Guid>("ById");

                    b.Property<string>("EventType");

                    b.Property<string>("ForFullName");

                    b.Property<Guid>("ForId");

                    b.Property<Guid>("PermissionId");

                    b.Property<string>("PermissionName");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("systemactivityprojection");
                });

            modelBuilder.Entity("UrbanSpork.DataAccess.Projections.SystemDropdownProjection", b =>
                {
                    b.Property<Guid>("PermissionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PermissionName");

                    b.HasKey("PermissionID");

                    b.ToTable("systemdropdownprojection");
                });

            modelBuilder.Entity("UrbanSpork.DataAccess.Projections.UserDetailProjection", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp");

                    b.Property<string>("Department");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName");

                    b.Property<string>("PermissionList")
                        .HasColumnType("json");

                    b.Property<string>("Position");

                    b.HasKey("UserId");

                    b.ToTable("userdetailprojection");
                });

            modelBuilder.Entity("UrbanSpork.DataAccess.Projections.UserManagementProjection", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Department");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName");

                    b.Property<string>("Position");

                    b.HasKey("UserId");

                    b.ToTable("usermanagementprojection");
                });
#pragma warning restore 612, 618
        }
    }
}
