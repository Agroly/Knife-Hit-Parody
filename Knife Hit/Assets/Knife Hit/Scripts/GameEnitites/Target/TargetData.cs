using UnityEngine;

public enum TargetType
{
    Regular,
    Boss
}

[CreateAssetMenu(fileName = "TargetData", menuName = "GameData/Target Data")]
public class TargetData : ScriptableObject
{
    public Sprite targetSprite;           
    public float rotationSpeed = 50f;     
    public int knifeHitsRequired = 3;      
    public string targetName;            
    public TargetType targetType;         
}
