using System;

namespace Hedgehog.JsonWebToken
{
    public class JwtOptions
    {
        public string Jti { get; set; }

        public JwtOptions()
        {
            Jti = CreateJti();
        }

        protected static string CreateJti()
        {
            return Guid.NewGuid().ToString();
        }

        public static DateTime GetIssuedAt()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetExpiration(TimeSpan validFor)
        {
            return GetIssuedAt().Add(validFor);
        }

        public static DateTime GetNotBefore()
        {
            return DateTime.UtcNow;
        }

        public static TimeSpan GetValidFor(int minutes)
        {
            return TimeSpan.FromMinutes(120);
        }
    }
}
