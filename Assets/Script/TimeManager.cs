using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float SlowdownFactor = .1f;
    public float SlowdownLength = 2;

    [HideInInspector] public float customScale;
    float originalDeltaTime;

    float lastShot;
    public float CurrentTime;
    float NextShot;

    private void Start()
    {
        originalDeltaTime = Time.fixedDeltaTime;
    }

    public void ChangeScale (float Scale)
    {
        Time.timeScale = Scale;
        Time.fixedDeltaTime = Time.timeScale* originalDeltaTime;
    }

    public void SlowdownTime()
    {
        ChangeScale(SlowdownFactor);
        customScale = SlowdownFactor;
    }
    
    public void ScaleToNormal()
    {
        ChangeScale(1);
    }


}
