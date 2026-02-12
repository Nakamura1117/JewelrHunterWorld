using UnityEngine;

[CreateAssetMenu(menuName = "Item/ScoreItem", fileName  = "ScoreItem")]

public class ItemData: ScriptableObject
{
    public int value = 0;
    public string itemName = "";
    public Sprite itemSprite;
}
