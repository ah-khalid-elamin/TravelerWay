using System.Net;

namespace TravelerWay.Common.Exceptions
{
    public class DuffelException : Exception
    {
        public int? StatusCode { get; }
        public string? Name { get; }
        public string? Details { get; }

        public DuffelException(int? statusCode, string? name, string? details)
            : base($"{name} returned {details}")
        {
            StatusCode = statusCode;
            Name = name;
            Details = details;
        }
    }
}
