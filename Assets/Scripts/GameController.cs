using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Transform startBasket;
    public event Action<Transform> OnNewBasketScored;
    public event Action OnFail;
    public event Action<bool> OnBackgroundChange;
    internal bool isPlayable = true;
    internal SortingGroup basketsSortingLayer;

    public void ChangeBasketsSortingLayer(int sortingLayer) => basketsSortingLayer.sortingOrder = sortingLayer;

    private int lastBasketHashcode = 0;
    public void InvokeOnNewBasketScored(Transform transform)
    {
        if (transform.GetHashCode() == lastBasketHashcode || lastBasketHashcode == 0) return;
        lastBasketHashcode = transform.GetHashCode();
        OnNewBasketScored?.Invoke(transform);
    }

    public void InvokeOnBackgroundChange(bool isLightTheme)
    {
        OnBackgroundChange?.Invoke(isLightTheme);
    }

    public void InvokeOnFail() => OnFail?.Invoke();
    public void SetStartBasket(Transform transform) => startBasket = transform;
    public void ReloadScene()
    {
        isPlayable = true;
        OnNewBasketScored = null;
        OnFail = null;
        OnBackgroundChange = null;
        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        loading.completed += Loading_completed;
    }

    private void Loading_completed(AsyncOperation obj)
    {
        TrajectoryPrediction.Instance.CreateParallelScene();
        GameData.Instance.Load();
        GameData.Instance.Resubscribe();
        MainUI.Instance.Start();
    }

    private void Start()
    {
        lastBasketHashcode = startBasket.GetHashCode();
    }

    internal void SetBasketHashCode(int hashCode) => lastBasketHashcode = hashCode;

}
