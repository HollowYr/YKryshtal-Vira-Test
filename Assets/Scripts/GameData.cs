using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        SaveSystem.Init();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) Save();
        if (Input.GetKeyDown(KeyCode.E)) Load();
    }
    public void Save()
    {
        SaveData saveData = new SaveData
        {
            score = score,
            stars = stars,
            isLightTheme = isLightTheme
        };
        SaveSystem.Save(JsonUtility.ToJson(saveData));
    }

    public void Load()
    {
        SaveData data = JsonUtility.FromJson<SaveData>(SaveSystem.Load());
        score = data.score;
        stars = data.stars;
        isLightTheme = data.isLightTheme;
        RefreshStarsCount();
        RefreshScoreCount();
    }

    public void AddStar(int count)
    {
        stars += count;
        RefreshStarsCount();
    }

    private void RefreshStarsCount()
    {
        MainUI.Instance.scoreUI.SetStarText(stars);
    }

    private void Application_quitting()
    {
        GameController.Instance.OnNewBasketScored -= Instance_OnNewBasketScored;
    }

    private void Instance_OnNewBasketScored(Transform obj)
    {
        score += 1;
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
        public int score;
        public int stars;
        public bool isLightTheme;
    }
}
