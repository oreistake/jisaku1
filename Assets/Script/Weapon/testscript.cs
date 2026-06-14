using System.Collections;
using UnityEngine;

public class testscript : MonoBehaviour
{

    void Update()
    {
        ststs();
    }

    IEnumerator SizeChange()
    {

        Vector3 bible = gameObject.transform.localScale;
        //bible += Vector3.one;
        if (bible.x < 0) bible += Vector3.one;

        yield return new WaitForSeconds(1);

        if (bible.x >= 1) bible -= Vector3.one;
        
        yield return new WaitForSeconds(1);
        

    }
    void ststs()
    {
        Vector3 bible = gameObject.transform.localScale;
        bible += Vector3.one;
        if (bible.x < 0) bible += Vector3.one;
        if (bible.x >= 1) bible -= Vector3.one;
    }

}
