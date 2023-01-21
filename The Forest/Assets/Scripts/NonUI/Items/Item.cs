using UnityEngine;

public interface Item
{
    int ID { get; }

    string Description { get; }

    bool Use();
    void PickUp();
    void Drop();
    Sprite Sprite { get; }
}
