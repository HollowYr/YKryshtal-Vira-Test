using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    private int highestScore = 0;
    private int score = 0;
    private int stars = 0;
    private bool isLightTheme = true;
    // Start is called before the first frame update
    void Start()
    {
        Load();
        Application.quitting += Application_quitting;
        GameController.Instance.OnNewBasketScored += Instance_OnNewBasketScored;
        SaveSystem.Init();
        RefreshStarsCount();
    }

    public void Resubscribe()
    {
        Application.quitting += Application_quitting;
        GameController.Instance.OnNewBasketScored += Instance_OnNewBasketScored;
    }

    public void Save()
    {
        SaveData saveData = new SaveData
        {
            highestScore = highestScore,
            stars = stars,
            isLightTheme = isLightTheme
        };
        SaveSystem.Save(JsonUtility.ToJson(saveData));
    }

    public void Load()
    {
        SaveData data = JsonUtility.FromJson<SaveData>(SaveSystem.Load());
        highestScore = data.highestScore;
        stars = data.stars;
        isLightTheme = data.isLightTheme;
        RefreshStarsCount();
        score = 0;
        RefreshScoreCount();
    }

    public void AddStar(int count)
    {
        stars += count;
        RefreshStarsCount();
    }

    public int GetHighestScore() => highestScore;

    private void RefreshStarsCount()
    {
        MainUI.Instance.scoreUI.SetStarText(stars);
    }

    private void Application_quitting()
    {
        Save();
        GameController.Instance.OnNewBasketScored -= Instance_OnNewBasketScored;
    }

    private void Instance_OnNewBasketScored(Transform obj)
    {

        Debug.Log("adding score");
        score += 1;
        if (score > highestScore) highestScore = score;
        //Debug.Log("Score: " + score);
        RefreshScoreCount();
        //throw new System.NotImplementedException();
    }

    private void RefreshScoreCount()
    {

        MainUI.Instance.scoreUI.SetScoreText(score);
    }

    private class SaveData
    {
        public int highestScore;
        public int stars;
        public bool isLightTheme;
    }
}
