using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject monster;
    public int monsterNum = 3;
    public int stageNum = 1;

    void Update()
    {
        if (monsterNum == 0)
        {
            stageNum += 1;
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
            SceneManager.LoadScene(stageNum);
        if (stageNum == 5)
            SceneManager.LoadScene(1);
    }
}
