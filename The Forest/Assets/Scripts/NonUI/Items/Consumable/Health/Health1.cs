using UnityEngine;

public class Health1 : Consumable
{
    Sprite spriteToReturn;

    public void Awake()  // look into virtual and move this up to item interface
    {
        Texture2D texure2D = Resources.Load<Texture2D>("UI/Sprites/Items/Consumables/Coke");
        spriteToReturn = Sprite.Create(
            texure2D,
            new Rect(0, 0, texure2D.width, texure2D.height),
            new Vector2(0.5f, 0.5f));
    }

    public override int ID
    {
        get => 0;
    }

    public override int maxStackSize
    {
        get => 1;
    }

    public override string Description
    {
        get => "Health Lv.1";
    }

    public override Sprite Sprite
    {
        get => spriteToReturn;
    }

    public override bool Use()
    {
        Debug.Log(Description + "used");
        return true;
    }
}
