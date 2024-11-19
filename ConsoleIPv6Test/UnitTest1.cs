using ConsoleIPv6UnitTests;

namespace ConsoleIPv6Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestExpandAddressWithDoubleColon()
        {
            // Arrange 
            string address = "2001:0db8:0f61:alff::80";
            // Act 
            string actual = IPv6Helper.ExpandAddress(address);
            // Assert 
            string expected = "2001:0db8:0f61:alff:0000:0000:0000:0080";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestExpandAddressWithOneZero()
        {
            string address = "2001:0db8:0f61:alff:0:baff:fe24:a9c6";
            string actual = IPv6Helper.ExpandAddress(address);
            string expected = "2001:0db8:0f61:alff:0000:baff:fe24:a9c6";
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestExpandAddressWithSomeZeros()
        {
            string address = "2001:0b8:01:00:000a:ff0:000:006";
            string actual = IPv6Helper.ExpandAddress(address);
            string expected = "2001:00b8:0001:0000:000a:0ff0:0000:0006";
            Assert.Equal(expected, actual);
        }
    }
}