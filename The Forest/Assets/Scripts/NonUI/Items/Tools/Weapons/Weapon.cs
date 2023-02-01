using UnityEngine;
using CameraShake;

public abstract class Weapon : MonoBehaviour, Item
{
    public abstract int ID { get; }
    public abstract string Description { get; }
    public abstract Sprite Sprite { get; }
    public abstract Vector3 weaponHolderPosition { get; }
    public abstract Vector3 weaponHolderRotation { get; }

    public abstract bool Use();

    protected WeaponHolderController weaponHolderController;
    protected Transform particleEffects;
    protected ParticleSystem muzzleFlash;
    protected AudioSource audioSource;
    protected BoxCollider boxCollider;

    protected float fireRate;
    protected float nextTimeToFire;

    public void PickUp()
    {
        boxCollider.enabled = false;
        weaponHolderController = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponHolderController>();
        weaponHolderController.HoldItem(this);
        gameObject.layer = LayerMask.NameToLayer("FP");
        SetChildrenWithTag(transform, "FP");
    }

    public void Drop()
    {
        boxCollider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Default");
        SetChildrenWithTag(transform, "Default");
    }

    protected void RaycastTargetHit(RaycastHit hit)
    {
        if (hit.transform.tag == "Cannibal")
        {
            hit.transform.GetComponent<Health>().ApplyDamage(25f);
        }
    }

    private void SetChildrenWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.layer = LayerMask.NameToLayer(tag);
            SetChildrenWithTag(child, tag);
        }
    }

    public bool IsReadyForUse() {
        return Time.time > nextTimeToFire;
    }
}
