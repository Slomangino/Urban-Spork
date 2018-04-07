using System;
using System.Collections.Generic;
using Moq;
using UrbanSpork.API.Controllers;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.CQRS.WriteModel.Command;
using UrbanSpork.DataAccess.Projections;
using UrbanSpork.ReadModel.QueryCommands;
using UrbanSpork.WriteModel.Commands;
using UrbanSpork.WriteModel.Commands.PermissionTemplates;

namespace UrbanSpork.Tests.Controllers.Permission
{
    public class PermissionControllerMockAggregate
    {
        #region Mocks
        private static readonly Mock<ICommandDispatcher> CommandDispatcherMock = new Mock<ICommandDispatcher>();

        public static readonly Mock<IQueryProcessor> QueryProcessorMock = new Mock<IQueryProcessor>();

        #endregion

        #region Objects

        public readonly ICommandDispatcher CommandDispatcher = CommandDispatcherMock.Object;

        public readonly IQueryProcessor QueryProcessor = QueryProcessorMock.Object;

        #endregion

        #region Commands & Queries

        public ICommand<PermissionDTO> PermissionCommand;

        public ICommand<PermissionTemplateDTO> PermissionTemplateCommand;

        public ICommand<string> DeletePermissionTemplateCommand;

        public IQuery<PermissionDTO> PermissionQuery;

        public IQuery<List<PermissionDTO>> GetAllPermissionsQuery;

        public IQuery<List<SystemDropdownProjection>> GetSystemDropdownProjectionQuery;

        public IQuery<List<PendingRequestsProjection>> GetPendingRequestQuery;

        #endregion


        #region Setup Methods

        public void setup_processor_to_verify_getPermissionQueries_are_the_same(Guid id)
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetPermissionByIdQuery>()))
                .Callback<IQuery<PermissionDTO>>((a) => { PermissionQuery = a; })
                .ReturnsAsync(new PermissionDTO
                {
                    Id = id
                });
        }

        public void setup_processor_to_verify_getAllPermissionQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetAllPermissionsQuery>()))
                .Callback<IQuery<List<PermissionDTO>>>((a) => { GetAllPermissionsQuery = a; })
                .ReturnsAsync(new List<PermissionDTO>());
        }

        public void setup_processor_to_verify_getSystemDropdownProjectionQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetSystemDropDownProjectionQuery>()))
                .Callback<IQuery<List<SystemDropdownProjection>>>((a) => { GetSystemDropdownProjectionQuery = a; })
                .ReturnsAsync(new List<SystemDropdownProjection>());
        }

        public void setup_processor_to_verify_getPendingRequestsQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetPendingRequestsProjectionQuery>()))
                .Callback<IQuery<List<PendingRequestsProjection>>>((a) => { GetPendingRequestQuery = a; })
                .ReturnsAsync(new List<PendingRequestsProjection>());
        }

        public void setup_dispatcher_to_verify_createPermissionCommands_are_the_same()
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePermissionCommand>()))
                .Callback<ICommand<PermissionDTO>>((a) => {  PermissionCommand = (CreatePermissionCommand) a; })
                .ReturnsAsync(new PermissionDTO()
                {
                    Name = "testName",
                    Description = "testDesc",
                    IsActive = true,
                    Image = "testImage"
                });
        }

        public void setup_dispatcher_to_verify_updatePermissionCommands_are_the_same()
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<UpdatePermissionInfoCommand>()))
                .Callback<ICommand<PermissionDTO>>((a) => { PermissionCommand = (UpdatePermissionInfoCommand) a; })
                .ReturnsAsync(new PermissionDTO()
                {
                    Name = "testName",
                    Description = "testDesc",
                    Image = "testImage"
                });
        }

        public void setup_dispatcher_to_verify_disablePermissionCommands_are_the_same(Guid PermissionId)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<DisablePermissionCommand>()))
                .Callback<ICommand<PermissionDTO>>((a) => { PermissionCommand = (DisablePermissionCommand)a; })
                .ReturnsAsync(new PermissionDTO()
                {
                    Id = PermissionId
                });
        }

        public void setup_dispatcher_to_verify_enablePermissionCommands_are_the_same(Guid PermissionId)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<EnablePermissionCommand>()))
                .Callback<ICommand<PermissionDTO>>((a) => { PermissionCommand = (EnablePermissionCommand)a; })
                .ReturnsAsync(new PermissionDTO()
                {
                    Id = PermissionId
                });
        }

        public void setup_dispatcher_to_verify_createPermissionTemplateCommands_are_the_same(CreatePermissionTemplateInputDTO dto)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePermissionTemplateCommand>()))
                .Callback<ICommand<PermissionTemplateDTO>>((a) => { PermissionTemplateCommand = (CreatePermissionTemplateCommand)a; })
                .ReturnsAsync(new PermissionTemplateDTO()
                {
                    Name = dto.Name,
                    TemplatePermissions = dto.TemplatePermissions
                });
        }

        public void setup_dispatcher_to_verify_editPermissionTemplateCommands_are_the_same(EditPermissionTemplateInputDTO dto)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<EditPermissionTemplateCommand>()))
                .Callback<ICommand<PermissionTemplateDTO>>((a) => { PermissionTemplateCommand = (EditPermissionTemplateCommand)a; })
                .ReturnsAsync(new PermissionTemplateDTO()
                {
                    Name = dto.Name,
                    TemplatePermissions = dto.TemplatePermissions
                });
        }

        public void setup_dispatcher_to_verify_deletePermissionTemplateCommands_are_the_same(DeletePermissionTemplateInputDTO dto)
        {
            CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<DeletePermissionTemplateCommand>()))
                .Callback<ICommand<string>>((a) => { DeletePermissionTemplateCommand = (DeletePermissionTemplateCommand) a; })
                .ReturnsAsync("");
        }

        #endregion

        public PermissionController PermissionControllerFactory()
        {
            return new PermissionController(QueryProcessor, CommandDispatcher);
        }

        //public void setup_dispatcher_to_verify_createPermissionCommands_are_the_same()
        //{
        //    CommandDispatcherMock.Setup(a => a.Execute(It.IsAny<CreatePermissionCommand>()))
        //        .Callback<ICommand<PermissionDTO>>((a) => { CreatePermissionCommand = a; })
        //        .ReturnsAsync(new PermissionDTO());
        //}
    }
}
