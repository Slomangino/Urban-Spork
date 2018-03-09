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
    [Migration("20180308134505_new projections")]
    partial class newprojections
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("UrbanSpork.DataAccess.Projections.PendingRequestsProjection", b =>
                {
                    b.Property<Guid>("PermissionId");

                    b.Property<Guid>("ForId");

                    b.Property<string>("RequestType");

                    b.Property<Guid>("ById");

                    b.Property<DateTime>("DateOfRequest")
                        .HasColumnType("timestamp");

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
