using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryPanelSlotsRowSlot : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    private bool taken;
    private int itemCount = 0;
    private Consumable item;
    private Text slotDisplayAmount;
    private Image slotImage;
    private GameObject itemsContainer;
    private GameObject icon;
    private float onPointerDownTime = 0;

    private void Awake()
    {
        icon = transform.GetChild(0).gameObject;
        slotDisplayAmount = transform.GetChild(1).GetComponent<Text>();
        itemsContainer = transform.GetChild(2).gameObject;
        slotImage = icon.GetComponent<Image>();
        icon.SetActive(false);
        taken = false;
    }

    public bool AddItem(Consumable consumable, int quantity)
    {
        if (item == null)
        {
            taken = true;
            item = consumable;
            itemCount = quantity;
            SetDescripionAndCount();
            SetSprite(consumable);
            return true;
        } else if (item.ID == consumable.ID)
        {
            itemCount += quantity;
            SetDescripionAndCount();
            return true;
        } else
        {
            return false;
        }
    }

    public void ClearSlot() {
        taken = false;
        item = null;
        itemCount = 0;
        SetDescripionAndCount();
        SetSprite(null);
    }

    public Consumable GetItem()
    {
        return item;
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    public bool isSlotTaken() {
        return taken;
    }

    private void SetSprite(Consumable consumable)
    {
        if (consumable)
        {
            icon.SetActive(true);
            Sprite sprite = consumable.Sprite;
            slotImage.sprite = sprite;
        } else
        {
            icon.SetActive(false);
            slotImage.sprite = null;
        }
    }

    private void SetDescripionAndCount()
    {
        if (item)
        {
            slotDisplayAmount.text = itemCount.ToString();
        }
        else
        {
            slotDisplayAmount.text = "";
        }
    }

    public void SetItemCount(int countToSet)
    {
        itemCount = countToSet;
        SetDescripionAndCount();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        onPointerDownTime = Time.time;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        float clickFinishTime = Time.time;
        float clickTime = clickFinishTime - onPointerDownTime;

        bool isClicked = clickTime < 0.1;
        bool isSlotTaken = item != null;

        if (isClicked && isSlotTaken)
        {
            item.Use();
        }
    }
}
