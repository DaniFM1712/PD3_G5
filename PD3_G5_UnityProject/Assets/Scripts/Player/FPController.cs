using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FPController : MonoBehaviour
{

    [Header("Mouse Debug")]

    [SerializeField]
    public KeyCode angleLockKey = KeyCode.I;

    [SerializeField]
    public KeyCode mouseLockKey = KeyCode.O;


    [SerializeField] KeyCode menuKey = KeyCode.Escape;
    [SerializeField] GameObject menuPause;


    private bool angleLocked = false;

    float yaw = 0;
    float pitch = 0;
    Vector3 direction = new Vector3(0, 0, 0);
    private Quaternion initialRotation;

    [Header("Rotation")]
    [SerializeField] float yawSpeed = 5.0f;
    [SerializeField] float pitchSpeed = 5.0f;
    [SerializeField] bool invertPitch;
    [SerializeField] bool invertYaw;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;
    [SerializeField] Transform pitchController;

    [Header("Planar Movement")]
    [SerializeField] CharacterController characterController;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] KeyCode forwardKey = KeyCode.W;
    [SerializeField] KeyCode leftKey = KeyCode.A;
    [SerializeField] KeyCode backwardsKey = KeyCode.S;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    float currSpeed;

    [Header("Vertical Movement")]
    float verticalSpeed = 0;
    float gravity;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    float jumpSpeed;
    [SerializeField] float maxHeightJump;
    [SerializeField] float jumpTime;
    [SerializeField] bool onGround;
    bool onCeiling;

    [Header("Weapons")]
    [SerializeField] GameObject weaponParent;
    [SerializeField] List<GameObject> weapons;

    [Header("Dash Properties")]
    [SerializeField] UnityEvent<float> startDashCooldown;
    private bool startCooldown = false;
    [SerializeField] float speed = 10.0f;
    [SerializeField] float dashLength = 0.15f;
    [SerializeField] float dashSpeed = 1000.0f;
    [SerializeField] float dashCooldown = 1.0f;

    [Header("Dash Conditions")]
    private Vector3 dashMove;
    private float dashing = 0f;
    private float dashingTime = 0f;
    private bool canDash = true;
    private bool dashingNow = false;
    private bool dashReset = true;
    private bool dashAllowed = false;
    private Vector3 moved = new Vector3(0, 0, 0);
    //----------DASH----------//
    private TwoChargeBlessingScript twoChargeBlessing;
    private DashIncreasesDamageBlessingScript dashIncreasesDamageBlessing;
    public int currentDashCharges;

    private void Awake()
    {
        Cursor.lockState =  CursorLockMode.Locked;
        yaw = transform.rotation.eulerAngles.y;
        pitch = transform.rotation.eulerAngles.x;

        gravity = (-2 * maxHeightJump) / (jumpTime * jumpTime);
        jumpSpeed = -gravity * jumpTime;

        initialRotation = transform.rotation;
    }

    private void Start()
    {
        twoChargeBlessing = GetComponent<TwoChargeBlessingScript>();
        dashIncreasesDamageBlessing = GetComponent<DashIncreasesDamageBlessingScript>();

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
        currentDashCharges = PlayerStatsScript.playerStatsInstance.currentMaxDashCharges;
        ChangeWeapon();

        PlayerStatsScript.playerStatsInstance.ActivateBlessings();
        
        menuPause.SetActive(false);

    }

    private void FixedUpdate()
    {
        if (!angleLocked)
            rotate();
        move();
    }

    void Update()
    {
        moved = updateMoveStats();
        inputUpdate();
        checkDash();
        updateLockKeyState();
    }

    void updateLockKeyState()
    {
        if (Input.GetKeyDown(angleLockKey))
            angleLocked = !angleLocked;

        if (Input.GetKeyDown(mouseLockKey))
            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void rotate()
    {
        float xMouse = Input.GetAxis("Mouse X");
        yaw += xMouse * yawSpeed * (invertYaw ? -1 : 1);

        float yMouse = Input.GetAxis("Mouse Y");
        pitch -= yMouse * pitchSpeed * (invertPitch ? -1 : 1);

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);


        transform.rotation = Quaternion.Euler(0, yaw, 0);
        pitchController.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    void move()
    {
        verticalSpeed += gravity * Time.deltaTime;

        Vector3 movement = direction.normalized * Time.deltaTime * currSpeed;

        movement.y = verticalSpeed * Time.deltaTime;

        CollisionFlags colFlags = characterController.Move(movement);
        onGround = (colFlags & CollisionFlags.Below) != 0;
        onCeiling = (colFlags & CollisionFlags.Above) != 0;

        if (onGround || onCeiling) verticalSpeed = gravity * Time.deltaTime;

    }

    void inputUpdate()
    {
        Vector3 forward = getForward();
        Vector3 right = getRight();
        direction = new Vector3(0, 0, 0);
        if (Input.GetKey(forwardKey))
        {
            direction += forward;
        }
        if (Input.GetKey(leftKey))
        {
            direction -= right;
        }
        if (Input.GetKey(rightKey))
        {
            direction += right;
        }
        if (Input.GetKey(backwardsKey))
        {
            direction -= forward;
        }
        /*
        if (Input.GetKey(runKey))
        {
            currSpeed = runSpeed + playerStats.currentSpeedBonus;
        }*/
        else
        {
            currSpeed = walkSpeed + PlayerStatsScript.playerStatsInstance.currentSpeedBonus;
        }

        if (Input.GetKeyDown(jumpKey) && onGround && Time.timeScale == 1f)
        {
            verticalSpeed = jumpSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) == true && dashAllowed && Time.timeScale == 1f)
        {
            dashIncreasesDamageBlessing.StartDamageTimer();
            dashReset = currentDashCharges > 1;

            if (dashReset)
                currentDashCharges--;
            else
            {
                startCooldown = true;
                currentDashCharges = PlayerStatsScript.playerStatsInstance.currentMaxDashCharges;

            }

            dashMove = moved;
            canDash = false;
            dashingNow = true;
        }

        if (Input.GetKeyDown(menuKey))
        {
            if (Time.timeScale == 1f)
            {
                ShowMenuUI();
            }
            else
            {
                HideMenuUI();
            }
            

        }

    }
    Vector3 updateMoveStats()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moved = transform.right * moveX + transform.forward * moveZ;

        if (moved.magnitude > 1)
        {
            moved = moved.normalized;
        }
        return moved;
    }

    void checkDash()
    {
        dashAllowed = dashing < dashLength && dashingTime < dashCooldown && dashReset && canDash ;

        if (dashingNow && dashing < dashLength)
        {
            characterController.Move(dashMove * dashSpeed * Time.deltaTime);
            dashing += Time.deltaTime;
        }

        dashingNow = (dashing >= dashLength) ? false : dashingNow;

        if (dashing >= dashLength && startCooldown)
        {
            startCooldown = false;
            startDashCooldown.Invoke(dashCooldown);
        }


        if (!dashingNow)
        {
            characterController.Move(moved * speed * Time.deltaTime);
        }

        dashingTime += (dashReset == false) ? Time.deltaTime : 0;

        if (onGround && !canDash && dashing >= dashLength)
        {
            canDash = true;
            dashing = 0f;
        }

        if (dashingTime >= dashCooldown && !dashReset)
        {
            dashReset = true;
            dashingTime = 0f;
        }
    }

    public void ChangeWeapon() 
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        if(PlayerStatsScript.playerStatsInstance.currentWeaponIndex == 1)
        {
            PlayerStatsScript.playerStatsInstance.currentWeapon = weapons[0].GetComponent<ProjectileShootingScript>();
            weapons[0].SetActive(true);
        }
        if (PlayerStatsScript.playerStatsInstance.currentWeaponIndex == 2)
        {
            PlayerStatsScript.playerStatsInstance.currentWeapon = weapons[1].GetComponent<ProjectileShootingScript>();
            weapons[1].SetActive(true);
        }
    }

    Vector3 getForward()
    {
        return new Vector3(Mathf.Sin(yaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(yaw * Mathf.Deg2Rad));
    }

    Vector3 getRight()
    {
        return new Vector3(Mathf.Sin((yaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos((yaw + 90.0f) * Mathf.Deg2Rad));
    }

    public void ActivateBlessing(string blessingName)
    {
        ParentBlessing blessing = GetComponent(blessingName) as ParentBlessing;
        blessing.enabled = true;
    
    }


    public void ShowMenuUI()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
    public void HideMenuUI()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void ShowSettingsUI()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoToMainMenu()
    {
        PlayerStatsScript.playerStatsInstance.ResetStats();
        InventoryManagerScript.InventoryInstance.ResetInventory();
        HideMenuUI();
        LevelManager.levelManagerInstance.GoToMainMenu();
        
    }
}
