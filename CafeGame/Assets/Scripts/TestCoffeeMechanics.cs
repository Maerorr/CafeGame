using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoffeeMechanics : MonoBehaviour
{
    bool isPouring = false;
    // Start is called before the first frame update
    
    [SerializeField]
    Transform espressoMask;

    private float finalMaskY = 7.56f;
    
    Coroutine pourRoutine;
    
    void Start()
    {
        var yscale = espressoMask.localScale;
        yscale.y = 0;
        espressoMask.localScale = yscale;
    }

    public void StartPouring()
    {
        if (isPouring)
        {
            return;
        }
        isPouring = true;
        pourRoutine = StartCoroutine(PouringCoroutine());
    }
    
    public void StopPouring()
    {
        if (!isPouring)
        {
            return;
        }
        isPouring = false;
        StopCoroutine(pourRoutine);
    }

    IEnumerator PouringCoroutine()
    {
        while (true)
        {
            var yscale = espressoMask.localScale;
            yscale.y += 2.0f * Time.deltaTime;
            if (yscale.y >= finalMaskY)
            {
                break;
            }
            espressoMask.localScale = yscale;
            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
