using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayRemoval : MonoBehaviour
{
    public float delaySeconds = 2f;
    public float scaleAmount = 0.95f;
    public bool shrinkMode = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        delaySeconds -= Time.deltaTime;
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                if(shrinkMode)
                {
                    child.transform.localScale = Vector3.Scale(child.transform.localScale, new Vector3(scaleAmount, scaleAmount, scaleAmount));
                }
                if (delaySeconds <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
