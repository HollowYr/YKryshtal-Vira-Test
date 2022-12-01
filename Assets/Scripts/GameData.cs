using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    private int score = 0;
    private int stars = 0;
    private bool isLightTheme = true;
    // Start is called before the first frame update
    void Start()
    {
        Application.quitting += Application_quitting;
        GameController.Instance.OnNewBasketScored += Instance_OnNewBasketScored;
        MainUI.Instance.scoreUI.SetScoreText(0);
    }

    private void Application_quitting()
    {
        GameController.Instance.OnNewBasketScored -= Instance_OnNewBasketScored;
    }

    private void Instance_OnNewBasketScored(Transform obj)
    {
        score += 1;
        //Debug.Log("Score: " + score);
        MainUI.Instance.scoreUI.SetScoreText(score);
        //throw new System.NotImplementedException();
    }
}
