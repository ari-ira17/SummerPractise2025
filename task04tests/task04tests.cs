using task04;
using Xunit;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldMovesForwardCorrectly()
    {
        Cruiser cruiser = new Cruiser();
        cruiser.MoveForward();
        Assert.Equal(50, cruiser.Self_X);
        Assert.Equal(0, cruiser.Self_Y);
    }

    [Fact]
    public void Cruiser_ShouldRotatesCorrectly()
    {
        Cruiser cruiser = new Cruiser();
        cruiser.Rotate(90);
        Assert.Equal(90, cruiser.Self_Angle);
    }
    
    [Fact]
    public void Cruiser_ShouldFiresRocketsCorrectly()
    {
        Cruiser cruiser = new Cruiser();
        cruiser.Fire();
        Assert.Equal(100, cruiser.Self_Rockets);
    }

    [Fact]
    public void Fighter_ShouldMovesForwardCorrectly()
    {
        Fighter fighter = new Fighter();
        fighter.MoveForward();
        Assert.Equal(100, fighter.Self_X);
        Assert.Equal(0, fighter.Self_Y);
    }

    [Fact]
    public void Fighter_ShouldRotatesCorrectly()
    {
        Fighter fighter = new Fighter();
        fighter.Rotate(-90);
        Assert.Equal(270, fighter.Self_Angle);
    }

    [Fact]
    public void Fighter_ShouldFiresRocketsCorrectly()
    {
        Fighter fighter = new Fighter();
        fighter.Fire();
        Assert.Equal(50, fighter.Self_Rockets);
    }
    
    [Fact]
    public void Cruiser_ShouldHasCorrectStats()
    {
        ISpaceship cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
    }
    
    [Fact]
    public void Fighter_ShouldHasCorrectStats()
    {
        ISpaceship fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(50, fighter.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Cruiser_ShouldBeMorePowerfulThanFighter()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(cruiser.FirePower > fighter.FirePower);
    }
}
