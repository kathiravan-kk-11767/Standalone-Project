using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoho.UWP.Tasks
{
    public interface ITasksNetHandler
    {

        Task<string> FetchTagsFromServerAsync(string ownerZuid);

    }
}
