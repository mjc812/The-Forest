using UnityEngine;

public class Health2 : Consumable
{
    Sprite spriteToReturn;

    public void Awake()
    {                                                 //this path starts from Resources. Unity automatically starts the path assuming Resources exists
        Texture2D texure2D = Resources.Load<Texture2D>("UI/Sprites/Items/Consumables/Sprite");
        spriteToReturn = Sprite.Create(
            texure2D,
            new Rect(0, 0, texure2D.width, texure2D.height),
            new Vector2(0.5f, 0.5f));
    }

    public override int ID
    {
        get => 1;
    }

    public override int maxStackSize
    {
        get => 5;
    }

    public override string Description
    {
        get => "Health Lv.2";
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
