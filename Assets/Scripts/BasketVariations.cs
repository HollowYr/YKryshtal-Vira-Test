using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketVariations : MonoBehaviour
{
    [SerializeField] private Transform star;
    private void Awake()
    {
        star.gameObject.SetActive(false);
    }

    public void AddStar()
    {
        star.gameObject.SetActive(true);
    }
}
