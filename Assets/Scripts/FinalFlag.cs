using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FinalFlag : MonoBehaviour
{
    public PlayerController pc;
    public GameObject player;
    public GameObject endPanel;
    public GameObject statsPannel;
    public Text finalTime;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && pc.active)
        {
            pc.Explode(false);
            player.SetActive(false);
            PauseMenu.gameEnded = true;
            PauseMenu.gameStarted = false;
            statsPannel.SetActive(false);

            float currentTime = Time.timeSinceLevelLoad;

            currentTime = currentTime / 3600f;

            int hours = (int)currentTime;

            currentTime = currentTime - hours;
            currentTime = currentTime * 60;

            int minutes = (int)currentTime;

            currentTime = currentTime - minutes;
            currentTime = currentTime * 60;

            int seconds = (int)currentTime;

            finalTime.text = "TIME : " + hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");

            endPanel.SetActive(true);
        }
    }
}
