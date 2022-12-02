using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject basketToSpawn;
    [SerializeField] private Transform camera;
    Vector3 LeftBasket;
    Vector3 RightBasket;

    void Start()
    {
        GameController.Instance.OnNewBasketScored += Instance_OnNewBasketScored;
        Application.quitting += Application_quitting;
        foreach (Transform item in transform)
        {
            if (item.position.x < 0) LeftBasket = camera.InverseTransformPoint(item.position);
            if (item.position.x > 0) RightBasket = camera.InverseTransformPoint(item.position);
        }
    }

    private void Application_quitting()
    {
        GameController.Instance.OnNewBasketScored -= Instance_OnNewBasketScored;
    }

    private void Instance_OnNewBasketScored(Transform obj)
    {
        Vector3 position = (obj.position.x > 0) ? LeftBasket : RightBasket;
        position = camera.TransformPoint(position);
        position.y = obj.position.y + Mathf.Abs(LeftBasket.y - RightBasket.y);
        position.z = 0;

        GameObject basket = Instantiate(basketToSpawn, position, Quaternion.identity, transform);
        BasketVariations basketVariation = basket.GetComponent<BasketVariations>();
        float random = Random.Range(0, 100);
        if (random < 30f) basketVariation.AddStar();
        Debug.Log("random num: " + random);
    }
}
