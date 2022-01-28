using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDestroy : MonoBehaviour
{
    IEnumerator timeToDestroy()
    {
        while(true)
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeToDestroy());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
