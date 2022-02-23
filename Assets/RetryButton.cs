using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        SpawnManager.countEnemyDeath = 0;
        SceneManager.LoadScene(0);
    }
}
