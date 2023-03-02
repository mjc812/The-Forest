using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    private PlayerPickup playerPickup;
    private Inventory inventory;
    private WeaponHolderController weaponHolderController;
    private BuildSystem buildSystem;
    private PlayerMovement playerMovement;
    private GameObject UICanvas;
    private MouseInputHandler mouseInputHandler;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerPickup = GameObject.FindWithTag("Player").GetComponent<PlayerPickup>();
        buildSystem = GameObject.FindWithTag("Player").GetComponent<BuildSystem>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        mouseInputHandler = GameObject.FindWithTag("Player").GetComponent<MouseInputHandler>();
        weaponHolderController = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponHolderController>();
        inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
        UICanvas = GameObject.FindWithTag("UICanvas");
    }

    void Update()
    {
        HandleKeyInput();
    }

    void HandleKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            //playerPickup.ItemPickup();
        } else if(Input.GetKeyDown(KeyCode.Q))
        {
            weaponHolderController.DropItem();
        } else if(Input.GetKeyDown(KeyCode.H))
        {
            if(!buildSystem.IsBuilding())
            {
                buildSystem.BuildFoundation();
            }
        } else if(Input.GetKeyDown(KeyCode.J))
        {
            if(!buildSystem.IsBuilding())
            {
                buildSystem.BuildWall();
            }
        } else if(Input.GetKeyDown(KeyCode.G))
        {
            if(buildSystem.IsBuilding())
            {
                buildSystem.CancelBuild();
            }
        }
    }
}
