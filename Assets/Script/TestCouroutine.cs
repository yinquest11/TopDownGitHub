using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCouroutine : MonoBehaviour
{
    public float Duration = 1f;

    private float currentDuration = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("DoCoroutine");
    }

    

    IEnumerator DoCoroutine()
    {
        currentDuration = Duration;
        Debug.Log("Start");

        while(currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
            Debug.Log(currentDuration);

            yield return null;
        }

        Debug.Log("Ended");
    }





}
