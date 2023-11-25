using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform left_x_bound;
    
    [SerializeField]
    Transform right_x_bound;
    
    [SerializeField]
    float speed = 1.0f;

    private Coroutine move_coroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartMovingRight()
    {
        Debug.Log("StartMovingRight");
        move_coroutine = StartCoroutine(Move(speed));
    }
    
    public void StopMovingRight()
    {
        StopCoroutine(move_coroutine);
    }
    
    public void StartMovingLeft()
    {
        Debug.Log("StartMovingLeft");
        move_coroutine = StartCoroutine(Move(-speed));
    }

    public void StopMovingLeft()
    {
        StopCoroutine(move_coroutine);
    }
    
    IEnumerator Move(float speed)
    {
        while (true)
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
            yield return null;
        }
    }
}
