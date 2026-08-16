using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerWay.Common.Payloads
{
    public record NotificationRequest<T>
    {
        public string Context { get; set; } = string.Empty;
        public T? Data { get; set; }

    }


}
