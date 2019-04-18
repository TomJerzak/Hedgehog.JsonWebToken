using FluentAssertions;
using Xunit;

namespace Hedgehog.JsonWebToken.Tests
{
    public class JwtOptionsTests
    {
        [Fact]
        public void create_unique_jti()
        {
            var jti1 = new JwtOptions().Jti;
            var jti2 = new JwtOptions().Jti;

            jti1.Length.Should().Be(36);
            jti2.Length.Should().Be(36);
            jti1.Should().NotBe(jti2);
        }
    }
}
