using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollisionDetect : MonoBehaviour
{
    private BoxCollider2D hitbox;
    private BoatSpawnDamageController bsdc;

    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
        bsdc = FindObjectOfType<BoatSpawnDamageController>();
    }

    private void OnMouseUp()
    {
        //Debug.Log(this.name);
        bsdc.UpdateSelected(gameObject);
    }
}
