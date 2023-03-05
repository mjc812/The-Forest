using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventoryPanelSlotsRowSlot : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    private bool taken;
    private int itemCount;
    private GameObject itemGameObject;
    private Consumable item;
    private Text slotDisplayAmount;
    private Image slotImage;
    private GameObject itemsContainer;
    private GameObject icon;
    private Sprite sprite;
    private float onPointerDownTime = 0;

    private void Awake()
    {
        itemCount = 0;
        icon = transform.GetChild(0).gameObject;
        slotDisplayAmount = transform.GetChild(1).GetComponent<Text>();
        itemsContainer = transform.GetChild(2).gameObject;
        slotImage = icon.GetComponent<Image>();
        icon.SetActive(false);
        taken = false;
    }

    public bool AddItem(GameObject gameObject, Consumable consumable, int quantity)
    {
        if (item == null)
        {
            taken = true;
            item = consumable;
            itemCount = quantity;
            SetDescripionAndCount();
            SetSprite(consumable);
            AddItemGameObject(gameObject);
            return true;
        } else if ((item.ID == consumable.ID) && (itemCount < item.maxStackSize))
        {
            itemCount += quantity;
            SetDescripionAndCount();
            AddItemGameObject(gameObject);
            return true;
        } else
        {
            return false;
        }
    }

    public void AddCount(int count) {
        itemCount += count;
        SetDescripionAndCount();
    }

    public void SubtractCount(int count) {
        itemCount = itemCount - count;
        if (itemCount <= 0) {
            ClearSlot();
        } else {
            SetDescripionAndCount();
        }
    }

    public bool SetCount(int count) {
        if (count > item.maxStackSize) {
            itemCount = item.maxStackSize;
            SetDescripionAndCount();
            return false;
        } else {
            itemCount = count;
            SetDescripionAndCount();
            return true;
        }
    }

    private void AddItemGameObject(GameObject itemObject) {
        if (itemGameObject == null) {
            itemGameObject = itemObject;
            itemObject.transform.parent = itemsContainer.transform;
        }
    }

    public void ClearSlot() {
        taken = false;
        item = null;
        itemCount = 0;
        itemGameObject.transform.parent = null;
        itemGameObject = null;
        SetDescripionAndCount();
        SetSprite(null);
    }

    public Consumable GetItem()
    {
        return item;
    }

    public GameObject GetItemGameObject() {
        return itemGameObject;
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
            sprite = consumable.Sprite;
            slotImage.sprite = sprite;
        } else
        {
            icon.SetActive(false);
            slotImage.sprite = null;
        }
    }

    public Sprite GetSprite() {
        return sprite;
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
