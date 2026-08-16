using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelerWay.Common.Payloads;
using Flurl.Http;
using Flurl;
using TravelerWay.Common.Exceptions;

namespace TravelerWay.Common.Interfaces.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendNotificationAsync<T>(string userId, string context, T body, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("userId is required", nameof(userId));
                if (body == null) throw new ArgumentNullException(nameof(body));


                var url = _configuration["Notifications:Url"];
                var request = new NotificationRequest<T>
                {
                    Context = context,
                    Data = body

                };

                var response = await url
                     .SetQueryParam("userId", userId)
                     .PostJsonAsync(request, cancellationToken: cancellationToken)
                     .ReceiveJson<IDictionary<string, object>>();

            }
            catch (FlurlHttpException ex)
            {
                var errorResponse = await ex.GetResponseStringAsync();
                throw new NotificationException(ex.StatusCode, "Notification API", ex.Message);
            }
        }
    }
}
