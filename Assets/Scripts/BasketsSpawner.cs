using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketsSpawner : MonoBehaviour
{
    [SerializeField] private Queue<GameObject> baskets;

    void Start()
    {

    }

    private void OnDestroy()
    {
        
    }
}
