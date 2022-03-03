using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime *0.3f);

    }
}
