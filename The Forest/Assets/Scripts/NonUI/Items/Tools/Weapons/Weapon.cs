using UnityEngine;

public class Weapon : MonoBehaviour, Item
{
    private WeaponHolderController weaponHolderController;
    private ParticleSystem flashParticleSystem;
    private AudioSource audioSource;
    private BoxCollider boxCollider;

    //eventually move to object pool. Do not want to reference prefabs in random places everywhere, should only be done through object pool handlers
    //public GameObject spark;

    public AudioClip fire;

    private float fireRate = 13f;
    private float nextTimeToFire;

    public int ID {
        get => 0;
    }

    public string Description {
        get => "Test Weapon";
    }

    public Sprite Sprite
    {
        get => null;
    }

    void Start()
    {
        nextTimeToFire = 0;
        boxCollider = transform.GetComponent<BoxCollider>();
        audioSource = transform.GetComponent<AudioSource>();
        Transform muzzleFlash = gameObject.transform.Find("MuzzleFlash");
        Transform flare = muzzleFlash.Find("Flash");
        flashParticleSystem = flare.GetComponent<ParticleSystem>();
    }

    public void PickUp()
    {
        boxCollider.enabled = false;
        weaponHolderController = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponHolderController>();
        weaponHolderController.HoldItem(this);
        SetChildrenWithTag(transform, "FP");
    }

    public void Drop()
    {
        boxCollider.enabled = true;
        SetChildrenWithTag(transform, "Default");
    }

    public bool Use()
    {
        if (Time.time > nextTimeToFire)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 80f))
            {
                RaycastTargetHit(hit);
            }
            audioSource.PlayOneShot(fire, 1f);
            nextTimeToFire = Time.time + 1f / fireRate;
            flashParticleSystem.Play();
            return true;
        } else
        {
            return false;
        }
    }

    private void RaycastTargetHit(RaycastHit hit)
    {
        if (hit.transform.tag == "Cannibal")
        {
            hit.transform.GetComponent<Health>().ApplyDamage(25f);
        }
        //GameObject spark2 = Instantiate(spark, hit.point, Quaternion.LookRotation(hit.normal));
        //Destroy(spark2, 0.5f);
    }

    private void SetChildrenWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.layer = LayerMask.NameToLayer(tag);
            SetChildrenWithTag(child, tag);
        }
    }
}
