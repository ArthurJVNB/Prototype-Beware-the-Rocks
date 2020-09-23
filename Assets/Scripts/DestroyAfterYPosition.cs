using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterYPosition : MonoBehaviour
{
    public int y = -11;

    private void Update()
    {
        if (transform.position.y < -11)
        {
            Destroy(gameObject);
        }
    }
}
