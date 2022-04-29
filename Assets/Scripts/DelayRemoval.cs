using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayRemoval : MonoBehaviour
{
    public float delaySeconds = 2f;
    public float scaleAmount = 0.95f;

    // Update is called once per frame
    void FixedUpdate()
    {
        delaySeconds -= Time.deltaTime;
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                Debug.Log(child.name);
                child.transform.localScale = Vector3.Scale(child.transform.localScale, new Vector3(scaleAmount, scaleAmount, scaleAmount));
                if (delaySeconds <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
