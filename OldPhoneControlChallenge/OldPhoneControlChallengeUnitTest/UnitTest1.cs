using OldPhoneControlChallenge;

namespace OldPhoneControlChallengeUnitTest
{
    public class OldPhonePadTests
    {
        [Theory]

        // Demo Test Cases
        [InlineData("33#", "E")]
        [InlineData("227*#", "B")]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("8 88777444666*664#", "TURING")]
        // wrap on 7(4 letters) & 9(4 letters), others(3)
        [InlineData("7777#", "S")]   // 7->PQRS (4 taps -> S)
        [InlineData("77777#", "P")]  // wrap back to P
        [InlineData("9999#", "Z")]
        [InlineData("99999#", "W")]
        // zero should repeat spaces
        [InlineData("0#", " ")]
        [InlineData("00#", "  ")]
        // backspace at start / extra backspaces
        [InlineData("*#", "")]
        [InlineData("2***#", "")]
        // ignore after '#'
        [InlineData("2#222", "A")]
        // symbols on '1'
        [InlineData("1#", "&")]
        [InlineData("11#", "'")]
        [InlineData("111#", "(")]
        [InlineData("1111#", "&")] // wrap
        public void OldPhonePad_ReturnsExpected(string input, string expected)
        {
            string actual = Program.OldPhonePad(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]                 // Empty input
        [InlineData(" ")]                // Only space
        [InlineData("2a2#")]             // Contains alphabet
        [InlineData("2,2#")]             // Contains punctuation
        [InlineData("22\n22#")]          // Newline
        [InlineData("!@#$")]             // Special symbols
        [InlineData("abc")]              // Non-numeric
        [InlineData("2😊2#")]            // Emoji
        public void OldPhonePad_ReturnsInvalid(string input)
        {
            var result = Program.OldPhonePad(input);
            Assert.Equal("INVALID", result);
        }
    }
}