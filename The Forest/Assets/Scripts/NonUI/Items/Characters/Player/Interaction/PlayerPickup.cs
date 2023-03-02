using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private GameObject UICanvas;
    public GameObject centerText;

    private void Awake() {
        UICanvas = GameObject.FindWithTag("UICanvas");
    }

    private void Update() {
        RaycastPickup();
    }

    public void RaycastPickup()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit raycastHit, 2f) && raycastHit.transform.tag == "Item")
        {
            Item item = raycastHit.transform.gameObject.GetComponent<Item>();
            if (Input.GetKey(KeyCode.F)) 
            {
                item.PickUp();
            } else 
            {
                centerText.SetActive(true);
            }
        } else {
            DeactivateText();
        }
    }

    private void DeactivateText() 
    {
        if (centerText.activeInHierarchy) {
            centerText.SetActive(false);
        }
    }
}
