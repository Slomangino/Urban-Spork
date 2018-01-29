using UrbanSpork.DataAccess.DataTransferObjects;
using UrbanSpork.Domain.Interfaces.ReadModel;

namespace UrbanSpork.DataAccess.ReadModel.QueryCommands
{
    public class GetUserByIdQuery : IQuery<UserDTO>
    {
        internal int Id;

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
