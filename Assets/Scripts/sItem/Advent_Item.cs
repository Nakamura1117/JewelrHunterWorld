using UnityEngine;
using UnityEngine.SceneManagement;

public class Advent_Item : MonoBehaviour
{
    public enum AdventItemType
    {
        None,
        Arrow,
        Key,
        Life
    }

    public AdventItemType type;
    public int numberOfArrow = 10;
    public int recoveryValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case AdventItemType.Arrow:
                    GameManager.arrows += numberOfArrow;
                    break;

                case AdventItemType.Key:
                    GameManager.keys++;
                    string sceneName = SceneManager.GetActiveScene().name;

                    if (GameManager.keyGot.ContainsKey(sceneName))
                    {
                        GameManager.keyGot[sceneName] = true;
                    }
                    else
                    {
                        GameManager.keyGot.Add(sceneName, true);
                    }
                    break;

                case AdventItemType.Life:
                    Player.PlayerRecovery(1);
                    break;

                case AdventItemType.None:
                default:
                    break;
            }

            SoundManager.currentSoundManager.PlaySE(SEType.ItemGet);
            GetComponent<CircleCollider2D>().enabled = false;
            Rigidbody2D rbody = GetComponent<Rigidbody2D>();
            rbody.gravityScale = 1.0f;
            rbody.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
            Destroy(this.gameObject, 0.2f);
        }
    }

}
