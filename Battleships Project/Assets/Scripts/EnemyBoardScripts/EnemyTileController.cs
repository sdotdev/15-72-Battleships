using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTileController : MonoBehaviour
{
    EnemyBrain eb;

    void Start()
    {
        eb = FindObjectOfType<EnemyBrain>();
    }

    void Update()
    {
        
    }

    void OnMouseUp()
    {
        eb.UpdateSelected(gameObject);
    }
}
