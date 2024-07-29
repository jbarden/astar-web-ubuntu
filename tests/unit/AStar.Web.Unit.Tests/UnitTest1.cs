namespace AStar.Web.Unit.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var healthStatusText = AStar.Web.Constants.Healthy;

        healthStatusText.Should().Be("Healthy");
    }
}