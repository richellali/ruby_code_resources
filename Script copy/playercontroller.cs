using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the movement of characters, blood and animation
/// </summary>

public class playercontroller : MonoBehaviour {
    public float speed = 5f; // speed of movement

    private int maxHealth = 5; // max HP

    private int currentHealth; // current HP

    public int MyMaxHealth { get { return maxHealth; } }

    public int MyCurrentHealth { get { return currentHealth; } }

    private float invincibleTime = 2f;

    private float invincibleTimer;

    private bool isInvincible;

    public GameObject bulletPrefab; // bullet

    // =================== Sound effect ================
    public AudioClip hitClip;

    public AudioClip launchClip;

    // public AudioClip walkClip;

    // =================== Direction ====================
    private Vector2 lookDirection = new Vector2(1, 0); // default: right

    // ===================== bullet count ===============
    [SerializeField]
    private int maxBulletCount = 99;
    private int curBulletCount;

    public int MyCurBulletCount { get { return curBulletCount; } }
    public int MyMaxBulletCount { get { return maxBulletCount; } }


    Rigidbody2D rbody;

    Animator anim;

    

    // Start is called before the first frame update
    void Start() {
        currentHealth = 2;
        invincibleTimer = 0;
        curBulletCount = 2;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        UIManager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }

    // Update is called once per frame
    void Update() {

        // Horizontal movement A:-1, D: 1 0
        float moveX = Input.GetAxisRaw("Horizontal"); 
        
        // Vertical movement W: 1 S:-1 0
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveVector = new Vector2(moveX, moveY);
        if (moveVector.x != 0 || moveVector.y != 0)
        {
            lookDirection = moveVector;
            
        }
        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", moveVector.magnitude);

        // ================ movement ================
        Vector2 position = rbody.position;
        position += moveVector * speed * Time.deltaTime;
        
        rbody.MovePosition(position);

        // ================= INVICIBLE TIMER
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        // ======= press 'J' for attack enemies ==================
        if (Input.GetKeyDown(KeyCode.J) && curBulletCount > 0)
        {
            ChangeBulletCount(-1);
            anim.SetTrigger("Launch");

            audioManager.instance.AudioPlay(launchClip); // lauch sound

            GameObject bullet = Instantiate(bulletPrefab,
                rbody.position + Vector2.up * 0.5f,
                Quaternion.identity);
            bulletController bc = bullet.GetComponent<bulletController>();
             if (bc != null)
            {
                bc.Move(lookDirection, 300);
            }
        }

        // ============ press 'K' for npc interaction ===============
        if (Input.GetKeyDown(KeyCode.K))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position,
                lookDirection, 2f, LayerMask.GetMask("npc"));
            if (hit.collider != null)
            {
                NPCManager npc = hit.collider.GetComponent<NPCManager>();

                if (npc != null)
                {
                    npc.ShowDialog();
                }

            }
        }
    }

    /// <summary>
    /// Change HP
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        // Damage

        if (amount < 0)
        {
            if (isInvincible == true) return;
            isInvincible = true;
            anim.SetTrigger("Hit");
            audioManager.instance.AudioPlay(hitClip); // hit sound
            invincibleTimer = invincibleTime;
        }

        Debug.Log(currentHealth + "/" + maxHealth);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        // update health bar
        UIManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0,
            maxBulletCount);
        UIManager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
    }
}
