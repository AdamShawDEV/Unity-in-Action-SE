using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public void ReactToHit()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        WanderingAI ai = GetComponent<WanderingAI>();
        if (ai != null)
        {
            ai.SetAlive(false);
        }
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
