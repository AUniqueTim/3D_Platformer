using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : MonoBehaviour
{
    public Bullet m_Bullet;
    public BulletTrap m_BulletTrap;
    public PickUps m_pickUps;
    public PlatformerController m_PlatformerController;
    public PlayerManager m_PlayerManager;
    public CameraController m_CameraController;
    public Enemy m_enemy;
    public EnemyManager m_EnemyManager;
    public MainCanvasUI m_MainCanvasUI;
    public HighScore m_HighScore;
    public int cameraState;
    //public int pelletCount;
    //public int pickUpCount;

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
        cameraState = m_CameraController.CameraState;
        
    }
    public void Update()
    {

        cameraState = m_CameraController.CameraState;
        //pelletCount = m_pickUps.pelletCount;
        //pickUpCount = m_pickUps.pickUps;
        playerPoweredUp = m_PlayerManager.playerPoweredUp;
        wacManPoweredUp = m_PlayerManager.wacManPoweredUp;
    }
}
