using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    public Bullet m_Bullet;
    public PickUps m_pickUps;
    public PlatformerController m_PlatformerController;
    public PlayerManager m_PlayerManager;
    public CameraController m_CameraController;
    public Enemy m_enemy;
    public EnemyManager m_EnemyManager;
    public int cameraState;

    public bool playerPoweredUp;
    public bool wacManPoweredUp;
    

    public static Toolbox instance;
    public static Toolbox Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Toolbox>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.AddComponent<Toolbox>();
                    singleton.name = "(Singleton) Toolbox";
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        cameraState = CameraController.CameraState;
        
    }
    public void Update()
    {
        playerPoweredUp = m_pickUps.playerPoweredUp;
        wacManPoweredUp = m_pickUps.wacManPoweredUp;
    }
}
