using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSpork.DataAccess
{
    public interface IProjection
    {
        void ListenForEvents(IEvent @event);
    }
}
