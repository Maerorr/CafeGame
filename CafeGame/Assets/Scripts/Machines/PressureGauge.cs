using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressureGauge : MonoBehaviour
{
    [SerializeField]
    Transform needle_transform;
    
    [SerializeField, Tooltip("Minimal angle, in degrees, that the needle can rotate to.")]
    float min_angle = -150f;
    
    [SerializeField, Tooltip("Minimal angle, in degrees, that the needle can rotate to.")]
    float max_angle = 150f;

    float value = 0.0f;
    
    Coroutine needle_coroutine;
    
    private void Start()
    {
        min_angle = -min_angle;
        max_angle = -max_angle;
        needle_transform.rotation = Quaternion.Euler(0, 0, min_angle);
    }

    public void SetValue(float new_value)
    {
        bool up = new_value > value;
        value = Mathf.Clamp(new_value, 0.0f, 1.0f);
        if(needle_coroutine != null)
            StopCoroutine(needle_coroutine);
        needle_coroutine = StartCoroutine(MoveNeedle(up));
    }
    
    private void UpdateNeedle()
    {
        float angle = Mathf.Lerp(min_angle, max_angle, value);
        needle_transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator MoveNeedle(bool up)
    {
        // rotate needle with a speed of 1 degree per second
        var current_angle = needle_transform.rotation.eulerAngles.z;
        var end_angle = Mathf.Lerp(min_angle, max_angle, value);
        // calculate degrees difference between current angle and end angle
        var angle_difference = Mathf.Abs(current_angle - end_angle);
        Debug.Log(current_angle + " " + end_angle);
        var lerp_time = 0.0f;
        while (true)
        {
            if (up)
            {
                needle_transform.RotateAround(needle_transform.position, Vector3.forward, -angle_difference * Time.deltaTime);
            }
            else
            {
                needle_transform.RotateAround(needle_transform.position, Vector3.forward, angle_difference * Time.deltaTime);
            }
            
            lerp_time += Time.deltaTime;
            if (lerp_time >= 1.0f)
            {
                break;
            }
            yield return null;
        }

        if (value > 0.2f)
        {
            while (true)
            {
                // add a little bit of randomness to the needle's rotation
                var random_angle = UnityEngine.Random.Range(-3f, 3f);
                needle_transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(min_angle, max_angle, value) + random_angle);
                yield return null;
            }
        }
    }
    
    void MoveLeft(float deg_value)
    {
        var angle = Mathf.Lerp(min_angle, max_angle, deg_value);
        needle_transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    void MoveRight(float deg_value)
    {
        var angle = Mathf.Lerp(min_angle, max_angle, deg_value);
        needle_transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
}
