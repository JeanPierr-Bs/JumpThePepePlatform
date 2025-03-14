using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAnyTimer : MonoBehaviour
{
    public float lifeTime;

    private void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
