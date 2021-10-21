using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int currentLevel = 0;
    public GameObject spawn;
    public GameObject levels;
    public ProgressCalculator progress;

    private void Start()
    {
        UpdateLevels();
    }
    public void UpdateLevels() {
        Debug.Log("Level is now " + currentLevel);

        progress.UpdateProgress(currentLevel);

        Vector3 spawnPoint = levels.transform.GetChild(currentLevel).transform.Find("Start").transform.position;

        spawn.transform.position = spawnPoint;
        for (int i = 0; i < levels.transform.childCount; i++)
        {
            levels.transform.GetChild(i).gameObject.SetActive(i <= currentLevel);
        }
    }

    public void SetEnd() {
        for (int i = 0; i < levels.transform.childCount; i++)
        {
            levels.transform.GetChild(i).transform.Find("End").gameObject.SetActive(i == currentLevel);
        }
    }

    public void IncreaseLevel() {
        currentLevel++;
        UpdateLevels();
    }

    public void DecreaseLevel() {
        currentLevel--;
        if (currentLevel < 0) {
            currentLevel = 0;
        }
        UpdateLevels();
    }

    public void DisableCurrentLevel() {
        if (currentLevel > 0) {
            levels.transform.GetChild(currentLevel).gameObject.SetActive(false);
        }
    }
}
