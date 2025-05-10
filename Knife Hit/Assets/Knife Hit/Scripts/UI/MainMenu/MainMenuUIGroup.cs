using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIGroup : UIGroup
{

    [SerializeField] private Text highScore;
    [SerializeField] private Text highStage;

    public override void Show()
    {
        highScore.text = "SCORE " + PlayerPrefs.GetInt("highscore").ToString();
        highStage.text = "STAGE " + PlayerPrefs.GetInt("highstage").ToString();
        base.Show();
    }



}
