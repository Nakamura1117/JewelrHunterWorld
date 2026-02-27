using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rBody;
    float axisH = 0.0f;
    float axisV = 0.0f;
    public float speed = 3.0f;
    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGraund = false;

    Animator animator;
    public string idolAnime = "Idol";
    public string runAnime = "Run";
    public string jumpAnime = "Jump";
    public string goalAnime = "Goal";
    public string deadAnime = "Dead";
    string nowAnime = "";
    string oldAnime = "";

    public int score = 0;
    public static int playerLife = 10;
    private bool inDamage = false;

    public float shootSpeed = 12.0f;
    public float shootDelay = 0.25f;
    public GameObject arrowPrefab;
    public GameObject gate;
    public static int hasArrows = 0;
    InputAction attackAction;

    InputAction moveAction;
    InputAction jumpAction;
    PlayerInput input;

    GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody = this.GetComponent<Rigidbody2D>();

        animator = this.GetComponent<Animator>();
        nowAnime = idolAnime;
        oldAnime = idolAnime;

        input = this.GetComponent<PlayerInput>();
        moveAction = input.currentActionMap.FindAction("Move");
        jumpAction = input.currentActionMap.FindAction("Jump");

        gm = GameObject.FindFirstObjectByType<GameManager>();

        InputActionMap uiMap = input.actions.FindActionMap("UI");
        uiMap.Disable();

        playerLife = GameManager.defaultPlayerLife;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != GameState.InGame || inDamage == true) 
        {
            if (inDamage)
            {
                float val = Mathf.Sin(Time.time * 50);
                if (val > 0)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }
                else 
                { 
                    GetComponent<SpriteRenderer>().enabled = false; 
                }
            }

            return; 
        }

        onGraund = Physics2D.CircleCast(transform.position,
                                      0.1f,
                                      Vector2.down,
                                      0.0f,
                                      groundLayer
                                    );

        if (axisH > 0.0f)
        {
            rBody.transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            rBody.transform.localScale = new Vector3(-1, 1);
        }

        if (onGraund)
        {
            if (axisH == 0)
            {
                nowAnime = idolAnime;
            }
            else
            {
                nowAnime = runAnime;
            }
        }
        else 
        { 
            nowAnime = jumpAnime;
        } 

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.gameState != GameState.InGame || inDamage==true)
        {
            return;
        }

        if (onGraund || axisH != 0)
        {
            rBody.linearVelocity = new Vector2(axisH * speed, rBody.linearVelocity.y);

        }

        if (onGraund && goJump)
        {
                Vector2 jumpPow = new Vector2(0, jump);
                rBody.AddForce(jumpPow, ForceMode2D.Impulse);
                goJump = false;
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            Dead();
        }
        else if(collision.gameObject.tag == "ScoreItem")
        {
            
            ScoreItem item = collision.gameObject.GetComponent<ScoreItem>();
            score = item.itemdata.value;
            UIController ui = Object.FindFirstObjectByType<UIController>();

            if (ui != null)
            {
                ui.UpdateScore(score);
            }
            score = 0;
            collision.gameObject.GetComponent<ScoreItem>().meDestoroy();
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            if (!inDamage)
            {
                GetDamage(collision.gameObject);
            }
        }
    }

    void OnSubmit(InputValue input)
    {
        if(GameManager.gameState != GameState.InGame)
        {
            gm.GameEnd();
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 moveIn = value.Get<Vector2>();
        axisH = moveIn.x;
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            goJump = true;
        }
    }

    private void OnAttack(InputValue value)
    {
        if(GameManager.arrows > 0)
        {
            ShootArrow();
        }
    }

    public void Goal ()
    {
        GameManager.gameState = GameState.GameClear;
        animator.Play(goalAnime);
        GameStop();
        
    }

    public void Dead()
    {
        GameManager.gameState = GameState.GameOver;
        animator.Play(deadAnime);
        GameStop();

        GetComponent<CapsuleCollider2D>().enabled = false;
        rBody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        Destroy(gameObject, 2.0f);
    }
    
   private void GameStop()
    {
        rBody.linearVelocity = new Vector2(0, 0);

        input.currentActionMap.Disable();
        input.SwitchCurrentActionMap("UI");
        input.currentActionMap.Enable();
    }
    
    public float GetAxisH()
    {
        return this.axisH;
    }

    public static void PlayerRecovery(int life)
    {
        playerLife += life;
        if(playerLife > 10) playerLife = 10;
    }

    private void GetDamage(GameObject target)
    {
        if (GameManager.gameState != GameState.InGame) return;

        playerLife -= 1;
        SoundManager.currentSoundManager.PlaySE(SEType.GetDamage);
        if (playerLife > 0)
        {
            rBody.linearVelocity = Vector2.zero;
            Vector3 v = (transform.position - target.transform.position).normalized;
            rBody.AddForce(new Vector2(v.x * 4, v.y * 4), ForceMode2D.Impulse);
            inDamage = true;
            Invoke("DamageEnd", 0.5f);
        }
        else
        {
            Dead();
        }
    }

    private void DamageEnd()
    {
        inDamage = false;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void ShootArrow()
    {
        Quaternion r;
        GameManager.arrows--;

        SoundManager.currentSoundManager.PlaySE(SEType.Shoot);

        if(transform.localScale.x > 0)
        {
            r = Quaternion.Euler(0, 0, 0);
        }else
        {
            r = Quaternion.Euler(0, 0, 180);
        }

        GameObject arrowObj = Instantiate(
            arrowPrefab, 
            gate.transform.position,
            r);

        Rigidbody2D arrowRbody = arrowObj.GetComponent<Rigidbody2D>();
        arrowRbody.AddForce(
            new Vector2(transform.localScale.x * shootSpeed, 0),
            ForceMode2D.Impulse
            );

    }

}
