using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager1 : MonoBehaviour
{
    public float enemySpeed;

    public int wacManLives;

    public GameObject wacMan;

    private void Update()
    {
        if (wacManLives <= 0)
        {
            wacMan.gameObject.SetActive(false);
        }
        enemySpeed = Toolbox.Instance.m_enemy.enemySpeed;
    }
}
