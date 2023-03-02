using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusPageInteraction : MonoBehaviour
{
    InventoryPanelSlotsRowSlot donorSlot;
    InventoryPanelSlotsRowSlot recipientSlot;
    private StatusPage statusPage;

    private float onPointerDownTime = 0;

    void Awake()
    {
        statusPage = GameObject.FindWithTag("StatusPage").GetComponent<StatusPage>();
    }

    void Update()
    {
        if (statusPage.isActive()) {
            if(Input.GetMouseButtonDown(0))
            {
                SetDonorSlot();
            } else if (Input.GetMouseButton(0))
            {
                // place translucent icon image on mouse position
            } else if (Input.GetMouseButtonUp(0))
            {
                if (donorSlot.isSlotTaken()) {
                    SetRecipientSlot();
                }
            }
        }
    }

    private void SetDonorSlot() {
        onPointerDownTime = Time.time;
        donorSlot = FindSlot();
    }

    private void SetRecipientSlot() {
        float clickFinishTime = Time.time;
        float clickTime = clickFinishTime - onPointerDownTime;

        bool isClicked = clickTime < 0.1;
        if (!isClicked && donorSlot != null) {
            recipientSlot = FindSlot();
            if (donorSlot.isSlotTaken() && recipientSlot == null)
            {
                PerformDrop();
            } else if(donorSlot.isSlotTaken() && !recipientSlot.isSlotTaken())
            {
                PerformMove();
            } else if(recipientSlot.GetItem().ID != donorSlot.GetItem().ID) //check if both contain items
            {
                PerformSwap();
            }
            donorSlot = null;
            recipientSlot = null;
            //if IDs match, it should stack
        }
    }

    private void PerformDrop()
    {
        donorSlot.GetItem().Drop();
    }

    private void PerformMove()
    {
        recipientSlot.AddItem(donorSlot.GetItem(), donorSlot.GetItemCount());
        donorSlot.ClearSlot();
    }

    private void PerformSwap()
    {
        Consumable recipientItem = recipientSlot.GetItem();
        int recipientItemCount = recipientSlot.GetItemCount();
        
        recipientSlot.ClearSlot();
        recipientSlot.AddItem(donorSlot.GetItem(), donorSlot.GetItemCount());
        donorSlot.ClearSlot();
        donorSlot.AddItem(recipientItem, recipientItemCount);
    }

    private InventoryPanelSlotsRowSlot FindSlot()
    {
        GraphicRaycaster graphicRaycaster = GetComponent<GraphicRaycaster>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        graphicRaycaster.Raycast(pointerEventData, raycastResults);

        foreach(RaycastResult result in raycastResults)
        {
            if(result.gameObject.tag == "InventoryPanelSlotsRowSlot")
            {
                return result.gameObject.GetComponent<InventoryPanelSlotsRowSlot>();
            }
        }
        return null;
    }
}
