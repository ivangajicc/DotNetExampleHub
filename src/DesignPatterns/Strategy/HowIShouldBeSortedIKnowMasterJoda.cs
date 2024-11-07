namespace DesignPatterns.Strategy;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1210:\"Equals\" and the comparison operators should be overridden when implementing \"IComparable\"", Justification = "No time, forgive me.")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "No time, forgive me.")]
public class HowIShouldBeSortedIKnowMasterJoda : IComparable<HowIShouldBeSortedIKnowMasterJoda>
{
    public int JediLevel { get; set; }

    public int CompareTo(HowIShouldBeSortedIKnowMasterJoda? compareTo) => JediLevel.CompareTo(compareTo?.JediLevel);
}
