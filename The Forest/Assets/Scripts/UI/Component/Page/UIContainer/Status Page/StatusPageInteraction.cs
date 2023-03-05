using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusPageInteraction : MonoBehaviour
{
    private InventoryPanelSlotsRowSlot donorSlot;
    private InventoryPanelSlotsRowSlot recipientSlot;
    public GameObject mouseImageGameObject;
    private Image mouseImage;
    private StatusPage statusPage;

    private float onPointerDownTime = 0;

    void Awake()
    {
        mouseImage = mouseImageGameObject.GetComponent<Image>();
        statusPage = GameObject.FindWithTag("StatusPage").GetComponent<StatusPage>();

        mouseImageGameObject.SetActive(false);
    }

    void Update()
    {
        if (statusPage.isActive()) {
            if(Input.GetMouseButtonDown(0))
            {
                SetDonorSlot();
            } else if (Input.GetMouseButton(0))
            {
                MoveMouseImage();
            } else if (Input.GetMouseButtonUp(0))
            {
                if (donorSlot && donorSlot.isSlotTaken()) {
                    SetRecipientSlot();
                }
            }
        }
    }

    private void SetSpriteOnMouseImage() {
        if (donorSlot && donorSlot.isSlotTaken()) {
            mouseImageGameObject.SetActive(true);
            Sprite sprite = donorSlot.GetSprite();
            mouseImage.sprite = sprite;
        }
    }

    private void ClearSpriteOnMouseImage() {
        mouseImageGameObject.SetActive(false);
        mouseImage.sprite = null;
    }

    private void MoveMouseImage() {
        if (donorSlot && donorSlot.isSlotTaken()) {
            mouseImage.transform.position = Input.mousePosition;
        }
    }

    private void SetDonorSlot() {
        onPointerDownTime = Time.time;
        donorSlot = FindSlot();
        SetSpriteOnMouseImage();
    }

    private void SetRecipientSlot() {
        float clickFinishTime = Time.time;
        float clickTime = clickFinishTime - onPointerDownTime;

        bool isClicked = clickTime < 0.1;
        if (!isClicked && donorSlot != null) {
            recipientSlot = FindSlot();
            if (donorSlot.isSlotTaken() && recipientSlot == null)
            {
                Drop();
            } else if(donorSlot.isSlotTaken() && !recipientSlot.isSlotTaken())
            {
                Move();
            } else if(recipientSlot.GetItem().ID != donorSlot.GetItem().ID)
            {
                Swap();
            } else if (recipientSlot.GetItem().ID == donorSlot.GetItem().ID && recipientSlot != donorSlot) 
            {
                Stack();
            }
            donorSlot = null;
            recipientSlot = null;
        }
        ClearSpriteOnMouseImage();
    }

    private void Stack() {
        GameObject recipientGameObject = recipientSlot.GetItemGameObject();
        GameObject donorGameObject = donorSlot.GetItemGameObject();

        int donorSlotCount = donorSlot.GetItemCount();
        int recipientSlotCount = recipientSlot.GetItemCount();
        int itemMax = recipientSlot.GetItem().maxStackSize;

        if (donorSlotCount + recipientSlotCount > itemMax) {
            donorSlot.SubtractCount(itemMax - recipientSlotCount);
            recipientSlot.SetCount(itemMax);
        } else {
            donorSlot.ClearSlot();
            recipientSlot.AddCount(donorSlotCount);
        }
    }

    private void Drop()
    {
        donorSlot.GetItem().Drop();
    }

    private void Move()
    {
        GameObject itemGameObject = GameObject.Instantiate(donorSlot.GetItemGameObject());
        recipientSlot.AddItem(itemGameObject, donorSlot.GetItem(), 1);
        itemGameObject.SetActive(false);
        donorSlot.SubtractCount(1);
    }

    private void Swap()
    {
        Consumable recipientItem = recipientSlot.GetItem();
        int recipientItemCount = recipientSlot.GetItemCount();
        
        GameObject recipientGameObject = recipientSlot.GetItemGameObject();
        GameObject donorGameObject = donorSlot.GetItemGameObject();

        recipientSlot.ClearSlot();
        recipientSlot.AddItem(donorGameObject, donorSlot.GetItem(), donorSlot.GetItemCount());
        donorSlot.ClearSlot();
        donorSlot.AddItem(recipientGameObject, recipientItem, recipientItemCount);
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
