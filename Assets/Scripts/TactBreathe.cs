using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TactBreathe : MonoBehaviour
{
    bool isScaling = false;
    Vector3 small = new Vector3(80, 80, 80);
    Vector3 big = new Vector3(120, 120, 120);
    float time = 4;
    bool isbig = false;

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        
        if (isbig && !isScaling)
        {
            StartCoroutine(scaleOverTime(small, time));
            isbig = false;
        }
        else if (!isbig && !isScaling)
        {
            StartCoroutine(scaleOverTime(big, time));
            isbig = true;
        }
        
    }


    IEnumerator scaleOverTime(Vector3 toScale, float duration)
    {
        if (isScaling)
        {
            yield break;
        }
        isScaling = true;

        float counter = 0;

        Vector3 startScaleSize = this.transform.localScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            this.transform.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            yield return null;
        }
        yield return new WaitForSeconds(time);
        isScaling = false;
    }
}
