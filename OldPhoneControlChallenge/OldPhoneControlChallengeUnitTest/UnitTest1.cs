using OldPhoneControlChallenge;

namespace OldPhoneControlChallengeUnitTest
{
    public class OldPhonePadTests
    {
        [Theory]
        
        [InlineData("33#", "E")]
        [InlineData("227*#", "B")]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("8 88777444666*664#", "TURING")]
        [InlineData("84426655099966688#", "THANK YOU")]
        [InlineData("22 2 222#", "BAC")]
        [InlineData("**22 2 222#", "BAC")]
        [InlineData("2222#", "A")]

        [InlineData("", "INVALID")]
        [InlineData("12a#", "INVALID")]
        public void OldPhonePad_ReturnsExpected(string input, string expected)
        {
            string actual = Program.OldPhonePad(input);
            Assert.Equal(expected, actual);
        }
    }
}