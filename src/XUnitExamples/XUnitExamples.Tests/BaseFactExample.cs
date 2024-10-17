namespace XUnitExamples.Tests;

public class BaseFactExample
{
    [Fact]
    public void Number_ShouldBeEqual_WhenEqual()
    {
        const int result = 2;
        const int expected = 2;

        result.Should().Be(expected);
    }
}
