using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using UrbanSpork.API.Controllers;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.CQRS.Queries;
using UrbanSpork.CQRS.Queries.Query;
using UrbanSpork.CQRS.WriteModel;
using UrbanSpork.ReadModel.QueryCommands;

namespace UrbanSpork.Tests.Controllers.User
{
    class UserControllerMockAggregate
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

        public IQuery<UserDetailProjectionDTO> UserQuery;

        public IQuery<List<UserDetailProjectionDTO>> UserCollectionQuery;

        public IQuery<OffBoardUserDTO> OffboardQuery;


        #endregion

        #region Factories

        public UserController CreateUserController()
        {
            return new UserController(QueryProcessor, CommandDispatcher);
        }
        
        #endregion

        #region Setup Methods

        public void setup_processor_to_verify_getUserQueries_are_the_same(Guid id)
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetUserDetailByIdQuery>()))
                .Callback<IQuery<UserDetailProjectionDTO>>((a) => { UserQuery = a; })
                .ReturnsAsync(new UserDetailProjectionDTO
                {
                    UserId = id
                });
        }

        public void setup_processor_to_verify_getUserCollectionQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetUserCollectionQuery>()))
                .Callback<IQuery<List<UserDetailProjectionDTO>>>((a) => { UserCollectionQuery = a; })
                .ReturnsAsync(new List<UserDetailProjectionDTO>());
        }

        public void setup_processor_to_verify_getUserOffboardingQueries_are_the_same()
        {
            QueryProcessorMock.Setup(a => a.Process(It.IsAny<GetOffboardUserPermissionsQuery>()))
                .Callback<IQuery<OffBoardUserDTO>>((a) => { OffboardQuery = a; })
                .ReturnsAsync(new OffBoardUserDTO());
        }

        #endregion
    }
}
