using UnityEngine;
using CameraShake;

public class Weapon : MonoBehaviour, Item
{
    public BounceShake.Params shakeParams;
    public AudioClip fire;

    private WeaponHolderController weaponHolderController;
    private Transform particleEffects;
    private ParticleSystem muzzleFlash;
    private AudioSource audioSource;
    private BoxCollider boxCollider;

    private float fireRate = 1f;
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
        particleEffects = transform.Find("Particle Effects");
        muzzleFlash = particleEffects.Find("Muzzle Flash").GetComponent<ParticleSystem>();
    }

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

    public bool Use()
    {
        if ((Time.time > nextTimeToFire) && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 80f))
            {
                RaycastTargetHit(hit);
            }
            //muzzleFlash.Play();
            audioSource.PlayOneShot(fire, 1f);
            nextTimeToFire = Time.time + 1f / fireRate;
            

            Vector3 sourcePosition = transform.position;
            CameraShaker.Shake(new BounceShake(shakeParams, sourcePosition));


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
