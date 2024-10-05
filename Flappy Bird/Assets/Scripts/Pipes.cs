using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] private float velocity = 2f;
    [SerializeField] private float destroyPosition = -5f;

    private void Update()
    {
        transform.position -= new Vector3(velocity, 0, 0) * Time.deltaTime;

        if (transform.position.x < destroyPosition)
            Destroy(gameObject);
    }
}
