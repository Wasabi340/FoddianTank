using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    public LevelController levelController;
    public PlayerController pc;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && pc.active)
        {
            levelController.IncreaseLevel();
            levelController.UpdateLevels();
            levelController.SetEnd();
            pc.Explode(false);
        }
    }

  /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            levelController.IncreaseLevel();
            levelController.UpdateLevels();
            levelController.SetEnd();
            pc.Explode(false);
        }
    }*/
}
