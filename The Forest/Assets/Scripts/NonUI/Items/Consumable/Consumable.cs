using UnityEngine;

public abstract class Consumable : MonoBehaviour, Item
{
    private Inventory inventory;

    public abstract int ID { get; }
    public abstract int maxStackSize { get; }
    public abstract string Description { get; }
    public abstract Sprite Sprite { get; }

    public abstract bool Use();

    public void PickUp()
    {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        if (inventory.AddItem(gameObject, this))
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void Drop()
    {
        Debug.Log(Description + "Dropped");
    }
}
