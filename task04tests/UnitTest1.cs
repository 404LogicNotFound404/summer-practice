using Xunit;
using static System.Net.Mime.MediaTypeNames;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        var cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(100, cruiser.FirePower);
        Assert.Equal(0, cruiser.Position);
        Assert.Equal(1000, cruiser.HealthPoints);
        Assert.Equal(0, cruiser.Angle);
        Assert.Equal(8, cruiser.NumberOfShells);

    }

    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        var fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(50, fighter.FirePower);
        Assert.Equal(0, fighter.Position);
        Assert.Equal(500, fighter.HealthPoints);
        Assert.Equal(0, fighter.Angle);
        Assert.Equal(4, fighter.NumberOfShells);
    }

    [Fact]
    public void MoveForward_ReturnNewPosition()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();

        fighter.MoveForward();
        var expectedPositionFighter = 100;
        Assert.Equal(fighter.Position, expectedPositionFighter);

        cruiser.MoveForward();
        var expectedPositionCruiser = 50;
        Assert.Equal(cruiser.Position, expectedPositionCruiser);
    }

    [Fact]
    public void Fire_ReturnNewHP()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();

        fighter.Target = cruiser;
        cruiser.Target = fighter;

        fighter.Fire();
        var expectedCruiserdHP = 950;
        var expectedFighterNumberOfShells = 3;
        Assert.Equal(cruiser.HealthPoints, expectedCruiserdHP);
        Assert.Equal(fighter.NumberOfShells, expectedFighterNumberOfShells);

        cruiser.Fire();
        var expectedFighterHP = 400;
        var expectedCruiserNumberOfShells = 7;
        Assert.Equal(expectedFighterHP, fighter.HealthPoints);
        Assert.Equal(cruiser.NumberOfShells, expectedCruiserNumberOfShells);
    }

    [Fact]
    public void Rotate_ReturnNewAngle()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();

        fighter.Rotate(300);
        var expectedAngle = 300;
        Assert.Equal(expectedAngle, fighter.Angle);

        cruiser.Rotate(600);
        var expectedAngleCruiser = 240;
        Assert.Equal(expectedAngleCruiser, cruiser.Angle);
    }

    [Fact]
    public void Rotate_NormalizeAngle_NegativeInput()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();

        fighter.Rotate(-10);
        var expectedAngle = 350;
        Assert.Equal(expectedAngle, fighter.Angle);

        cruiser.Rotate(-40);
        var expectedAngleCruiser = 320;
        Assert.Equal(expectedAngleCruiser, cruiser.Angle);
    }

    [Fact]
    public void FireFighter_CheckingForExceptionsHP()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        cruiser.HealthPoints = 49;

        fighter.Target = cruiser;
        var exception = Assert.Throws<InvalidOperationException>(() => fighter.Fire());
        Assert.Equal("Error", exception.Message);
    }

    [Fact]
    public void FireСruiser_CheckingForExceptionsHP()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.HealthPoints = 49;

        cruiser.Target = cruiser;
        var exception = Assert.Throws<InvalidOperationException>(() => fighter.Fire());
        Assert.Equal("Error", exception.Message);
    }

    [Fact]
    public void FireFighter_CheckingForExceptionsNumberOfShells()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.NumberOfShells = 0;

        fighter.Target = cruiser;
        var exception = Assert.Throws<InvalidOperationException>(() => fighter.Fire());
        Assert.Equal("Error", exception.Message);
    }

    [Fact]
    public void FireСruiser_CheckingForExceptionsNumberOfShells()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        cruiser.NumberOfShells = 0;

        cruiser.Target = cruiser;
        var exception = Assert.Throws<InvalidOperationException>(() => fighter.Fire());
        Assert.Equal("Error", exception.Message);
    }


    [Fact]
    public void Cruiser_MoreDamageThanFighter()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(cruiser.FirePower > fighter.FirePower);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }
}
