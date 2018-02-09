using CQRSlite.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanSpork.DataAccess
{
    public interface IProjection
    {
        void ListenForEvents(IEvent @event);
    }
}
