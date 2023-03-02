using UnityEngine;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    private GameObject UICanvas;
    public GameObject centerText;
    private TMP_Text TMP_Text;

    private void Awake() {
        UICanvas = GameObject.FindWithTag("UICanvas");
        TMP_Text = centerText.GetComponent<TMP_Text>();
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
                TMP_Text.text = "Press (F) to pickup" + " " + item.Description;
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
