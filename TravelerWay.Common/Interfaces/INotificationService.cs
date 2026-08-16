using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerWay.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync<T>(string userId, string context, T body, CancellationToken cancellationToken = default);
    }
}
