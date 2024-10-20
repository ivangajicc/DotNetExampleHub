using System.Collections;

namespace XUnitExamples.Tests;

public static class DataDrivenTestCases
{
    /* Use inline data for constant values or smaller sets of values */
    public class InlineDataTestExamples
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(9, 9)]
        [InlineData(11, 11)]
        public void Number_ShouldBeEqual_WhenEqual(int value1, int value2) => value1.Should().Be(value2);
    }

    /* Use member data attributes to simplify test setup when dealing with large sets of data */
    public class MemberDataTestExamples
    {
        public static IEnumerable<object[]> Data => new[]
        {
            new object[]
            {
                "true",
                "false",
                false,
            },
            new object[]
            {
                "true",
                "true",
                true,
            },
            new object[]
            {
                "false",
                "false",
                true,
            },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void StringEquality_ShouldBeAsExpectedResult(string value1, string value2, bool expectedResult)
        {
            var equality = value1.Equals(value2, StringComparison.Ordinal);

            equality.Should().Be(expectedResult);
        }

#pragma warning disable S4144
        [Theory]
        [MemberData(nameof(ExternalDataForMemberData.TypedData), MemberType = typeof(ExternalDataForMemberData))]
        public void StringEquality_ShouldBeAsExpectedResult_TakingInputFromExternalClassProperty(
            string value1,
            string value2,
            bool expectedResult)
        {
            var equality = value1.Equals(value2, StringComparison.Ordinal);

            equality.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(ExternalDataForMemberData.TypedDataWithInput), "firstPart", MemberType = typeof(ExternalDataForMemberData))]
        public void StringEquality_ShouldBeAsExpectedResult_TakingInputFromExternalClassMethodWithProvidingParameter(
            string value1,
            string value2,
            bool expectedResult)
        {
            var equality = value1.Equals(value2, StringComparison.Ordinal);

            equality.Should().Be(expectedResult);
        }
    }

    /* Similar usage as for member data but we use class to define inputs */
    public class ClassDataTestExamples()
    {
        [Theory]
        [ClassData(typeof(TheoryDataClass))]
        [ClassData(typeof(TheoryTypedDataClass))]
        public void StringEquality_ShouldBeAsExpectedResult_UsingClassAsTheoryInput(
            string value1,
            string value2,
            bool expectedResult)
        {
            var equality = value1.Equals(value2, StringComparison.Ordinal);

            equality.Should().Be(expectedResult);
        }

        private class TheoryDataClass : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    "true",
                    "false",
                    false,
                };
                yield return new object[]
                {
                    "true",
                    "true",
                    true,
                };
                yield return new object[]
                {
                    "false",
                    "false",
                    true,
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class TheoryTypedDataClass : TheoryData<string, string, bool>
        {
            public TheoryTypedDataClass()
            {
                Add("true", "false", false);
                Add("true", "true", true);
                Add("false", "false", true);
            }
        }
    }

    private static class ExternalDataForMemberData
    {
        public static TheoryData<string, string, bool> TypedData => new()
        {
            { "ExternalData", "ExternalData", true }, { "ExternalData", "ExternalDataDiff", false },
        };

        public static TheoryData<string, string, bool> TypedDataWithInput(string firstPart) => new()
        {
            { firstPart, firstPart, true },
            { firstPart, "DifferentString", false },
            { firstPart + "secondPart", firstPart + "secondPart", true },
        };
    }
}
