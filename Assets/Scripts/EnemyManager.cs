using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float enemySpeed;

    public int wacManLives;

    public GameObject wacMan;

    [SerializeField] private GameObject wacManIcon01;
    [SerializeField] private GameObject wacManIcon02;
    [SerializeField] private GameObject wacManIcon03;

    private void Update()
    {
        if (wacManLives <= 0)
        {
            wacMan.gameObject.SetActive(false);
            Toolbox.Instance.m_PlayerManager.playerWon = true;
            Toolbox.Instance.m_PlayerManager.GameOver();
        }

        enemySpeed = Toolbox.Instance.m_enemy.enemySpeed;

        //UI
        if(wacManLives == 3)
        {
            wacManIcon01.gameObject.SetActive(true);
            wacManIcon02.gameObject.SetActive(true);
            wacManIcon03.gameObject.SetActive(true);
        }
        else if (wacManLives == 2)
        {
            wacManIcon01.gameObject.SetActive(false);
            wacManIcon02.gameObject.SetActive(true);
            wacManIcon03.gameObject.SetActive(true);
        }
        else if (wacManLives == 1)
        {
            wacManIcon01.gameObject.SetActive(false);
            wacManIcon02.gameObject.SetActive(false);
            wacManIcon03.gameObject.SetActive(true);
        }
        else if (wacManLives <= 0)
        {
            wacManIcon01.gameObject.SetActive(false);
            wacManIcon02.gameObject.SetActive(false);
            wacManIcon03.gameObject.SetActive(false);
        }
    }
}
