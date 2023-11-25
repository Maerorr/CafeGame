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
    
    bool move_flag = false;
    
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
            move_flag = true;
            var cam_width_world_units = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            if (speed < 0.0f && transform.position.x <= (left_x_bound.position.x + (cam_width_world_units / 2.0)))
            {
                Debug.Log($"reached left bound {transform.position.x} {left_x_bound.position.x} {cam_width_world_units / 2.0}");
                move_flag = false;
            }
            if (speed > 0.0 && transform.position.x >= (right_x_bound.position.x - (cam_width_world_units / 2.0)))
            {
                Debug.Log($"reached right bound {transform.position.x} {right_x_bound.position.x} {cam_width_world_units / 2.0}");
                move_flag = false;
            }
            
            if (move_flag) {
                transform.position += speed * Time.deltaTime * Vector3.right;
            }
            yield return null;
        }
    }
}
