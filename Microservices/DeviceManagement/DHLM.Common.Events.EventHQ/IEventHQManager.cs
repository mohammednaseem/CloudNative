using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DHLM.Common.Events.EventHQ
{
    public interface IEventHQManager
    {
        event EventHandler NotifyOnMessage;

//      event EventHandler NotifyOnError;

 //     event EventHandler NotifyCloseConnection;

        string Register(Dictionary<string,string> connectionstring);

    }
}