using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelerWay.Common.Exceptions
{

    public class NotificationException : Exception
    {
        public int? StatusCode { get; }
        public string? Name { get; }
        public string? Details { get; }

        public NotificationException(int? statusCode, string? name, string? details)
            : base($"{name} returned {details}")
        {
            StatusCode = statusCode;
            Name = name;
            Details = details;
        }
    }


}
