using UnityEngine;
using System;

public class Player : MonoBehaviour
{ Rigidbody2D rig;
    internal bool platJump = false;


    [Header("Move Settings")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float iceSpeed;
    private float speed;
    delegate void MoveDelegate();
    MoveDelegate move;

    [Header("Jump Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float normalJumpForce;
    [SerializeField] private float springJumpForce;
    [SerializeField] private float groundCheckDistance;
    private float jumpForce;
    public static Action OnJump;
    [SerializeField] private string nextLevel;
    SpriteRenderer spr;

    [Header("Anim")]
    [SerializeField] private float speedXAnimOffset;
    Animator anim;
    bool inGround = true;
    [SerializeField] private Color fireColor;
    SoundManager soundManager;
    [SerializeField] GameObject endPanel;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        soundManager = GetComponent<SoundManager>();
        jumpForce = normalJumpForce;
        move = NormalSpeed;
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    

    private void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {
        anim.SetFloat("SpeedY", rig.velocity.y);
        Move();

        inGround = GroundCheck();

        anim.SetBool("Ground", inGround);
    }

    private void Move()
    {
        move();

        if (Input.GetAxisRaw("Horizontal") == 0) {
            anim.SetBool("Idle", true);
        } else {
            anim.SetBool("Idle", false);
        }
        Flip(rig.velocity);
    }

    private void NormalSpeed()
    {
        Vector2 velocity = rig.velocity;

        velocity.x = Input.GetAxis("Horizontal") * normalSpeed * Time.deltaTime;
        rig.velocity = velocity;
    }
    private void IceSpeed() {
        print("Ice Move");

        Vector3 rigSpeed = rig.velocity;
        speed = rigSpeed.x;

        speed += (Input.GetAxis("Horizontal") * iceSpeed * Time.deltaTime);

        speed = Math.Clamp(speed, -maxSpeed, maxSpeed);

        rigSpeed.x = speed;

        rig.velocity = rigSpeed;
    }

    private void Flip(Vector2 velocity)
    {
        if (velocity.x > 0)
        {
            spr.flipX = false;
        }
        else if (velocity.x < 0)
        {
            spr.flipX = true;
        }
    }

    private void Jump() {
        if (inGround && Input.GetButtonDown("Jump")) {
            OnJump?.Invoke();
            Jump(jumpForce);
        }
    }
    private void Jump(float _jumpForce) {
        
        rig.AddForce(Vector2.up * _jumpForce);
        soundManager.PlayOneShotAudio("Jump");
    }
    private bool GroundCheck() {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        return ray;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
    }

    internal void SetJumpForce() {
        if (!platJump)
        {
            platJump = true;
            Invoke(nameof(SetPlatJump), 1f);
            rig.velocity = Vector2.zero;
            Jump(springJumpForce);
        }
    }
    void SetPlatJump() => platJump = false;
    internal void Fire()
    {
        SetJumpForce();
        SetColorSpr(fireColor);
    }
    internal void SetColorSpr(Color color) => spr.color = color;
    internal void SetAccereration(bool newAccereration) {
        if (newAccereration) {
            move = NormalSpeed;
        } else {
            if (IsInvoking(nameof(ExitIce)))
            {
                CancelInvoke(nameof(ExitIce));
            }

            move = IceSpeed;
        }
    }
    internal void ExitIce()
    {
        ForceExitIce();
    }
    internal void ForceExitIce()
    {
        SetAccereration(true);
    }
    internal void Dead(bool fall){

        rig.velocity = Vector3.zero;
        rig.isKinematic = true;
        rig.gravityScale = 0;
        anim.SetTrigger("Death");

        GameManager.OnDead?.Invoke();

        if (fall) soundManager.PlayAudio("Fall");
        else soundManager.PlayAudio("Dead");

        Invoke(nameof(Reload), 2f);
    }

    void Reload(){
        GameManager.OnDead?.Invoke();
        SceneController.Reload();
    }
    internal void EndLevel(){
        endPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
