using UnityEngine;
using UnityEngine.UI;

public class GameOverUIGroup : UIGroup
{

    [SerializeField] private Text stageText;
    [SerializeField] private Text scoreText;
    [SerializeField] private AudioClip loseClip;
    public override void Show()
    {
        AudioManager.Instance.PlaySFX(loseClip);
        stageText.text = "STAGE " + LevelManager.Instance.GetStage().ToString();
        scoreText.text = LevelManager.Instance.GetScore().ToString();
        base.Show();
    }


    
}
