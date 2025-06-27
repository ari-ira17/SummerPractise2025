namespace task04;

public interface ISpaceship
{
    void MoveForward();   
    void Rotate(int angle);  
    void Fire();            
    int Speed { get; }     
    int FirePower { get; }  
}       

public class Cruiser : ISpaceship
{
    public double Self_X;
    public double Self_Y;
    public int Self_Angle;
    public int Self_Rockets;

    public int Speed { get; } = 50;
    public int FirePower { get; } = 100;  

    public Cruiser(double x = 0, double y = 0, int angle = 0, int rockets = 0)
    {
        Self_X = x;
        Self_Y = y;
        Self_Angle = angle;
        Self_Rockets = rockets;
    }

    public void MoveForward()
    {
        double AngleRadians = Self_Angle * Math.PI / 180.0;

        double X_New = Speed * Math.Cos(AngleRadians);
        double Y_New = Speed * Math.Sin(AngleRadians);

        Self_X = Math.Round(Self_X + X_New, 4); 
        Self_Y = Math.Round(Self_Y + Y_New, 4);
    }

    public void Rotate(int angle)
    {
        Self_Angle = (Self_Angle + angle + 360) % 360;
    }

    public void Fire()
    {
        Self_Rockets += 100;
    }     
}

public class Fighter : ISpaceship
{
    public double Self_X;
    public double Self_Y;
    public int Self_Angle;
    public int Self_Rockets;

    public int Speed { get; } = 100;
    public int FirePower { get; } = 50;

    public Fighter(double x = 0, double y = 0, int angle = 0, int rockets = 0)
    {
        Self_X = x;
        Self_Y = y;
        Self_Angle = angle;
        Self_Rockets = rockets;
    }

    public void MoveForward()
    {
        double AngleRadians = Self_Angle * Math.PI / 180.0;

        double X_New = Speed * Math.Cos(AngleRadians);
        double Y_New = Speed * Math.Sin(AngleRadians);

        Self_X = Math.Round(Self_X + X_New, 4);
        Self_Y = Math.Round(Self_Y + Y_New, 4);
    }

    public void Rotate(int angle)
    {
        Self_Angle = (Self_Angle + angle + 360) % 360;
    }

    public void Fire()
    {
        Self_Rockets += 50;
    }
}
