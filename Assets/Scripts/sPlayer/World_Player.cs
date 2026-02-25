using UnityEngine;
using UnityEngine.InputSystem;

public class World_Player : MonoBehaviour
{
    public enum Direction {
        none,
        left,
        right
    }

    public float speed = 3.0f;
    private Vector2 moveVec = Vector2.zero;
    float angleZ = 0.0f;
    Rigidbody2D rbody;
    Animator animator;


    bool isActionButtonPressed;
    public bool IsActionButtonPressed
    {
        get { return isActionButtonPressed; }
        set { isActionButtonPressed = value; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        angleZ = GetAngle();
        Direction dir = AngleToDirection();

        if (dir == Direction.left)
        {
            transform.localScale = new Vector2(
                Mathf.Abs(transform.localScale.x) * -1,
                transform.localScale.y
                );
        }
        else if (dir == Direction.right)
        {
            transform.localScale = new Vector2(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y
                );
        }

        if (moveVec != Vector2.zero)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

    }

    void FixedUpdate()
    {
        rbody.linearVelocity = new Vector2(moveVec.x * speed, moveVec.y * speed);
    }

    void OnMove(InputValue input)
    {
        moveVec = input.Get<Vector2>();
    }

    void OnActionButton(InputValue value)
    {
        IsActionButtonPressed = value.isPressed;
    }

    

    private float GetAngle()
    {
        float angle = angleZ;

        if (moveVec != Vector2.zero)
        {
            float rad = Mathf.Atan2(moveVec.y, moveVec.x);
            angle = rad * Mathf.Rad2Deg;
        }

        // 入力によって-91～-180か、180～269になるか不安定なため、-91～-179を180～269に平準化
        if(angle < -90)
        {
            angle += 360;
        }
        // 上記の不具合から、-1～-89か、271～395になるか不安定になると予想される？ため
        // 271～395を-1～-89へ変換
        else if (angle > 270)
        {
            angle -= 360;
        }

        return angle;
    }

    private Direction AngleToDirection()
    {
        Direction dir;
        if (angleZ >= -89 && angleZ <= 89)
        {
            dir = Direction.right;
        }
        else if (angleZ >= 91 && angleZ <= 269)
        {
            dir = Direction.left;
        }
        else
        {
            dir = Direction.none;
        }
        return dir;
    }
}
