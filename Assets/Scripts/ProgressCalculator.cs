using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCalculator : MonoBehaviour
{
    public Text progress;
    public Text projectiles;
    public Text time;
    public Text deaths;
    public Text falls;

    private int projectileCount;
    private int deathCount;
    private int fallCount;

    // Start is called before the first frame update
    void Start()
    {
        projectileCount = 0;
        deathCount = 0;
        fallCount = 0;
        progress.text = "0 / 10";
        time.text = "[ 00:00:00 ]";
        projectiles.text = "SHOTS FIRED : " + projectileCount;
        deaths.text = "EXPLOSIONS : " + deathCount;
        falls.text = "FALLS : " + fallCount;
    }

    public void UpdateProgress(int _level) {
        progress.text = _level + " / 10";
    }

    public void UpdateProjectileCount() {
        projectileCount++;
        projectiles.text = "SHOTS FIRED : " + projectileCount;
    }

    public void UpdateDeathCount()
    {
        deathCount++;
        deaths.text = "EXPLOSIONS : " + deathCount;
    }

    public void UpdateFallCount()
    {
        fallCount++;
        falls.text = "FALLS : " + fallCount;
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.timeSinceLevelLoad;

        currentTime = currentTime / 3600f;

        int hours = (int)currentTime;

        currentTime = currentTime - hours;
        currentTime = currentTime * 60;

        int minutes = (int)currentTime;

        currentTime = currentTime - minutes;
        currentTime = currentTime * 60;

        int seconds = (int)currentTime;

        time.text = "[ " + hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2") + " ]";
    }
}
