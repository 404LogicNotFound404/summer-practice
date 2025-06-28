public interface ISpaceship
{
    void MoveForward();      
    void Rotate(int angle);  
    void Fire();             
    int Speed { get; }       
    int FirePower { get; }
}

public class Fighter : ISpaceship
{
    public int Speed { get; } = 100;
    public int FirePower { get; } = 50;
    public int Position { get; set; } = 0;
    public int HealthPoints { get; set; } = 500;
    public int Angle { get; set; } = 0;
    public int NumberOfShells { get; set; } = 4;
    public ISpaceship? Target { get; set; }

    public void MoveForward() => Position += Speed;

    public void Fire()
    {
        if (Target is Cruiser cruiser && NumberOfShells > 0 && (cruiser.HealthPoints - 50) >= 0)
        {
            cruiser.HealthPoints -= FirePower;
            NumberOfShells -= 1;
        }
        else
        {
            throw new InvalidOperationException("Error");
        }
    }

    public void Rotate(int angle)
    {
        Angle += angle;
        Angle %= 360;

        if (Angle < 0) Angle += 360;
    }
}

public class Cruiser : ISpaceship
{
    public int Speed { get; } = 50;
    public int FirePower { get; } = 100;
    public int Position { get; set; } = 0;
    public int HealthPoints { get; set; } = 1000;
    public int Angle { get; set; } = 0;
    public int NumberOfShells { get; set; } = 8;
    public ISpaceship? Target { get; set; }

    public void MoveForward() => Position += Speed;

    public void Fire()
    {
        if (Target is Fighter fighter && NumberOfShells > 0 && (fighter.HealthPoints - 100)>=0)
        {
            fighter.HealthPoints -= FirePower;
            NumberOfShells -= 1;
        }
        return;
    }

    public void Rotate(int angle)
    {
        Angle += angle;
        Angle %= 360;

        if (Angle < 0) Angle += 360;
    }
}
