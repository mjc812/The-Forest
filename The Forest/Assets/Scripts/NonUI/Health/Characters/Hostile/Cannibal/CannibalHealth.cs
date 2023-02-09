public class CannibalHealth : Health
{
    protected override int startingHealth
    {
        get => 100;
    }

    protected override bool regenHealth
    {
        get => false;
    }
}
