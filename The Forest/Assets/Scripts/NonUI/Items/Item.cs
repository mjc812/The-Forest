using UnityEngine;

public interface Item
{
    int ID { get; }
    string Description { get; }
    Sprite Sprite { get; }

    bool Use();
    void PickUp();
    void Drop();
}
