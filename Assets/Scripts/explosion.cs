using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{

    private float maxLifeTime = 1.0f;
    private void Awake()
    {
        Destroy(this.gameObject, this.maxLifeTime);
    }

}
