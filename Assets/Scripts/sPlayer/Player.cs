using UnityEngine;

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
    //bool isJump = true;
    //bool isUpArrow=true;
    
    //bool atRight = false;
    //bool atLeft = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody = this.GetComponent<Rigidbody2D>();

        animator = this.GetComponent<Animator>();
        nowAnime = idolAnime;
        oldAnime = idolAnime;
        //atRight = false;
        //atLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != GameState.InGame) 
        { 
            return; 
        }


        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");

        onGraund = Physics2D.CircleCast(transform.position,
                                      0.1f,
                                      Vector2.down,
                                      0.0f,
                                      groundLayer
                                      );

        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log("downjump");
            goJump=true;
        }
        if (axisH > 0.0f)
        {
            Debug.Log("右向き");
            rBody.transform.localScale = new Vector2(1, 1);
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("左向き");
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

        //if (Input.GetKeyDown(KeyCode.RightArrow)) {  atRight = true; }
        //if (Input.GetKeyUp(KeyCode.RightArrow)) {  atRight = false; }

        //if (Input.GetKeyDown(KeyCode.LeftArrow)) {  atLeft = true; }
        //if (Input.GetKeyUp(KeyCode.LeftArrow)) {  atLeft = false; }

        //if (!atRight) { Console.WriteLine("右が押されています"); }
        //if (atLeft) { Console.WriteLine("左が押されています"); }

        //if (Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    isUpArrow = false;
        //}
    }

    private void FixedUpdate()
    {
        if (GameManager.gameState != GameState.InGame)
        {
            return;
        }

        //Debug.Log(isJump);
        //if (axisV > 0.0f && isJump && !(isUpArrow))
        //{
        //    Debug.Log("ジャンプ");
        //    rBody.linearVelocity = new Vector2(axisH * speed, axisV * jump);
        //    isJump = false;
        //    isUpArrow = true;
        //}else
        //{
        //rBody.linearVelocity = new Vector2(axisH * speed, rBody.linearVelocity.y);
        //}

        if (onGraund || axisH != 0)
        {
            rBody.linearVelocity = new Vector2(axisH * speed, rBody.linearVelocity.y);

        }
        //Debug.Log("onground" + onGraund);
        //Debug.Log("onjump" + goJump);

        if (onGraund && goJump)
        {
                Debug.Log("jump");
                Vector2 jumpPow = new Vector2(0, jump);
                rBody.AddForce(jumpPow, ForceMode2D.Impulse);
                goJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("Goal");
            Goal();
        }
        else if (collision.gameObject.tag == "Dead")
        {
            Debug.Log("Dead");
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
            Destroy(collision.gameObject);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 6)
        //{
        //    isJump = true;
        //}
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
    }
    
    public float GetAxisH()
    {
        return this.axisH;
    }
}
