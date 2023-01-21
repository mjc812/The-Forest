using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public void ItemPickup()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit raycastHit, 80f))
        {
            if (raycastHit.transform.tag == "Item")
            {
                Item item = raycastHit.transform.gameObject.GetComponent<Item>();
                item.PickUp();
            }
        }
    }
}
