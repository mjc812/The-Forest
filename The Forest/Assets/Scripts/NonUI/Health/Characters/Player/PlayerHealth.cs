public class PlayerHealth : Health
{
    protected override int startingHealth
    {
        get => 100;
    }

    protected override bool regenHealth
    {
        get => true;
    }
}
