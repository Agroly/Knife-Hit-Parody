using UnityEngine;
public enum KnifeSkinUnlockType
{
    AppleCost,
    BossReward
}

[CreateAssetMenu(fileName = "KnifeSkin", menuName = "GameData/Knife Skin")]
public class KnifeSkin : ScriptableObject
{
    public Sprite knifeSprite;
    public string skinName;
    public KnifeSkinUnlockType unlockType;
    public int appleCost;
    public string bossName; 
}
