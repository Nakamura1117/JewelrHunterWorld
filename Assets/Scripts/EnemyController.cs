using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;          // 移動速度
    public bool isToRight = false;      // true=右向き　false=左向き
    public float revTime = 0;           // 反転時間
    public LayerMask groundLayer;       // 地面レイヤー
    bool onGround = false;              // 地面フラグ
    float time = 0;

    public float enemyLife = 1;
    bool inDamage = false;
    float damageTime = 1.0f;
    Rigidbody2D rbody;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        inDamage = false;

        if (isToRight)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);// 向きの変更
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 地上判定
        onGround = Physics2D.CircleCast(transform.position,    // 発射位置
                                        0.2f,                  // 円の半径
                                        Vector2.down,          // 発射方向
                                        0.0f,                  // 発射距離
                                        groundLayer);          // 検出するレイヤー


        if (revTime > 0)
        {
            time += Time.deltaTime;
            if (time >= revTime)
            {
                time = 0;                   //タイマーを初期化
                returnDirect();
            }
        }


        if (GameManager.gameState != GameState.InGame || inDamage == true)
        {
            if (inDamage)
            {
                float val = Mathf.Sin(Time.time * 40);
                if (val > 0)
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                }
            }

        }


    }
    void FixedUpdate()
    {
        if (onGround)
        {
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            if (isToRight)
            {
                rbody.linearVelocity = new Vector2(speed, rbody.linearVelocity.y);
            }
            else
            {
                rbody.linearVelocity = new Vector2(-speed, rbody.linearVelocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            returnDirect();
            time = 0;                   //タイマーを初期化
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            if (!inDamage)
            {
                inDamage = true;
                ArrowController arrow = collision.gameObject.GetComponent<ArrowController>();
                enemyLife -= arrow.attackPower;
                rbody.linearVelocity = Vector2.zero;
                Vector3 v = (transform.position - collision.transform.position).normalized;
                rbody.AddForce(new Vector2(v.x * 100, v.y * 4), ForceMode2D.Impulse);
                Invoke("DamageEnd", damageTime);

                if (enemyLife <= 0)
                {
                    rbody.linearVelocity = Vector2.zero;
                    GetComponent<CircleCollider2D>().enabled = false;
                    GetComponent<BoxCollider2D>().enabled = false;
                    SoundManager.currentSoundManager.PlaySE(SEType.Enemykilled);
                    Destroy(this.gameObject, 0.25f);
                }
            }
        }
    }

    private void DamageEnd()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        inDamage = false;
    }

    public void returnDirect()
    {
        isToRight = !isToRight;
        transform.localScale = new Vector3(
            transform.localScale.x * -1,
            transform.localScale.y,
            transform.localScale.z
            );
    }
}
