namespace XUnitExamples.Tests;

public class TripleAExplanation
{
    [Fact]
    public void Number_ShouldBeEqual_WhenEqual()
    {
        // Arrange - define your setup and dependencies
        const string hello = "Hello";
        const string world = "World!";
        const string expectedResult = "Hello World!";

        // Act - invoke operation that you are testing. Act should be always one action.
        var result = ConcatenateStringsWithWhiteSpaceBetween(hello, world);

        // Assert - verify that result is expected
        result.Should().Be(expectedResult);
    }

    private string ConcatenateStringsWithWhiteSpaceBetween(string string1, string string2) => $"{string1} {string2}";
}
