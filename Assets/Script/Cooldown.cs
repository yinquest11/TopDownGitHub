using System;
using System.Collections;
using UnityEngine;



[Serializable]
public class Cooldown
{
    public enum Progress
    {
        Ready, // == 0
        Started, // == 1
        InProgress, // == 2
        Finished // == 3
    }

    public Progress CurrentProgress = Progress.Ready;

    public float Duration = 0.01f;
    public float CurrentDuration
    {
        get
        {
            return currentDuration;
        }
    }

    private float currentDuration = 0f;
    public bool IsOnCoolDown
    {
        get
        {
            return isOnCoolDown;
        }
    }

    private bool isOnCoolDown = false;

    private Coroutine coroutine;


    //StartCooldown public function
    public void StartCooldown()
    {
      
        if (CurrentProgress is Progress.Started or Progress.InProgress)
            return;

        //Use CoroutineHost.Instance to start a Coroutine
        coroutine = CoroutineHost.Instance.StartCoroutine(DoCoolDown());
    }


    //StopCooldown public funtion
    public void StopCooldown()
    {
        if (coroutine != null)
        {
            //Use CoroutineHost.Instance to stop a coroutine
            CoroutineHost.Instance.StopCoroutine(coroutine);
        }
            
        currentDuration = 0f;
        isOnCoolDown = false;
        CurrentProgress = Progress.Ready;
    }



    //Coroutine
    IEnumerator DoCoolDown()
    {
        
        CurrentProgress = Progress.Started;

        currentDuration = Duration;
        isOnCoolDown = true;

        while (currentDuration > 0)
        {
            //deduct by Time.deltaTime every frame
            currentDuration -= Time.deltaTime;
            CurrentProgress = Progress.InProgress;

            //Coroutine stop until next frame and continue do from here
            //Prevent the loop end in a frame
            yield return null;
            
        }

        currentDuration = 0f;
        isOnCoolDown = false;
        CurrentProgress = Progress.Finished;
    }





}
