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

    public float Duration = 1.0f;

    public float CurrentDuration
    {
        get
        {
            return currentDuration;
        }
    }

    public bool IsOnCoolDown
    {
        get
        {
            return isOnCoolDown;
        }
    }

    private bool isOnCoolDown = false;

    private float currentDuration = 0f;


    private Coroutine coroutine;

    public void StartCooldown()
    {
        if (CurrentProgress is Progress.Started or Progress.InProgress)
            return;

        coroutine = CoroutineHost.Instance.StartCoroutine(DoCoolDown());
    }

    public void StopCooldown()
    {
        if (coroutine != null)
            CoroutineHost.Instance.StopCoroutine(coroutine);

        currentDuration = 0f;
        isOnCoolDown = false;
        CurrentProgress = Progress.Ready;
    }

    IEnumerator DoCoolDown()
    {
        CurrentProgress = Progress.Started;
        currentDuration = Duration;
        isOnCoolDown = true;

        while (currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
            CurrentProgress = Progress.InProgress;

            yield return null;
        }

        currentDuration = 0f;
        isOnCoolDown = false;

        CurrentProgress = Progress.Finished;
    }





}
