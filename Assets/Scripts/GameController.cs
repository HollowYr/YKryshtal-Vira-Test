using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Transform startBasket;
    public event Action<Transform> OnNewBasketScored;
    public event Action OnFail;


    private int lastBasketHashcode = 0;
    public void InvokeOnNewBasketScored(Transform transform)
    {
        if (transform.GetHashCode() == lastBasketHashcode || lastBasketHashcode == 0) return;
        lastBasketHashcode = transform.GetHashCode();
        OnNewBasketScored?.Invoke(transform);
    }

    public void InvokeOnFail() => OnFail?.Invoke();
    public void SetStartBasket(Transform transform) => startBasket = transform;
    public void ReloadScene()
    {
        OnNewBasketScored = null;
        OnFail = null;
        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        loading.completed += Loading_completed;
    }

    private void Loading_completed(AsyncOperation obj)
    {
        TrajectoryPrediction.Instance.CreateParallelScene();
        GameData.Instance.Load();
        GameData.Instance.Resubscribe();
        MainUI.Instance.Start();
        //lastBasketHashcode = transform.GetHashCode();
    }

    private void Start()
    {
        lastBasketHashcode = startBasket.GetHashCode();
    }

    internal void SetBasketHashCode(int hashCode) => lastBasketHashcode = hashCode;

}
