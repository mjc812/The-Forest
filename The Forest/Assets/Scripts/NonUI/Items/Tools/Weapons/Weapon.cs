using UnityEngine;
using CameraShake;

public abstract class Weapon : MonoBehaviour, Item
{
    public GameObject[] fleshHitEffects;
    
    public abstract int ID { get; }
    public abstract string Description { get; }
    public abstract Sprite Sprite { get; }
    public abstract Vector3 weaponHolderPosition { get; }
    public abstract Vector3 weaponHolderRotation { get; }
    public abstract Vector3 fpCameraAimPosition { get; }
    public abstract Vector3 fpCameraAimRotation { get; }

    public abstract bool Use();

    protected WeaponHolderController weaponHolderController;
    protected Transform particleEffects;
    protected ParticleSystem muzzleFlash;
    protected ParticleSystem muzzleSmoke;
    protected ParticleSystem cartridgeEject;
    protected ParticleSystem cartridgeSmoke;
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
        particleEffects = transform.Find("Particle Effects");
        muzzleFlash = particleEffects.Find("Muzzle Flash").GetComponent<ParticleSystem>();
        muzzleSmoke = particleEffects.Find("Muzzle Smoke").GetComponent<ParticleSystem>();
        cartridgeEject = particleEffects.Find("Cartridge Eject").GetComponent<ParticleSystem>();
        cartridgeSmoke = particleEffects.Find("Cartridge Smoke").GetComponent<ParticleSystem>();
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
        if (hit.transform.tag == "Character")
        {
            SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
            hit.transform.GetComponent<Limb>().ApplyDamage(1f);
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

    private void SpawnDecal(RaycastHit hit, GameObject prefab)
	{
		GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
		spawnedDecal.transform.SetParent(hit.collider.transform);
	}
}
