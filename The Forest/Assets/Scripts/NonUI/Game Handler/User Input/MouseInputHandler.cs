using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    private WeaponHolderController weaponHolder;
    private BuildSystem buildSystem;
    private AimSystem aimSystem;
    private KeyInputHandler keyInputHandler;
    private PlayerLook playerLook;
    private StatusPage statusPage;

    void Awake()
    {
        weaponHolder = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponHolderController>();
        buildSystem = GameObject.FindWithTag("Player").GetComponent<BuildSystem>();
        aimSystem = GameObject.FindWithTag("FPCamera").GetComponent<AimSystem>();
        keyInputHandler = GameObject.FindWithTag("Player").GetComponent<KeyInputHandler>();
        playerLook = GameObject.FindWithTag("Player").GetComponent<PlayerLook>();
        statusPage = GameObject.FindWithTag("StatusPage").GetComponent<StatusPage>();
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (!statusPage.isActive()) {
            // if (Input.GetMouseButton(0))
            // {
            //     weaponHolder.UseItem();
            // }
            if (Input.GetMouseButtonDown(1))
            {
                aimSystem.AimIn();
            }
            if (Input.GetMouseButtonUp(1))
            {
                aimSystem.AimOut();
            }
            if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
            {
                playerLook.Look(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
                buildSystem.UpdateSnapThresholdAccumulators(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            }
        }
    }
}


//if (Input.GetMouseButtonDown(0))
        //{
        //    if (buildSystem.IsBuilding())
        //    {
        //        buildSystem.DoBuild();
        //    } else
        //    {
        //        weaponHolder.UseItem();
        //    }
        //}