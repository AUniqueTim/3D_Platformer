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

    //Audio
    
    public AudioListener audioListener;
    public AudioSource audioSource;
    //public AudioSource oneUpSound;
    //public AudioSource pelletPickUpSound;
    //public AudioSource powerUpPickUpSound;
    //public AudioSource playerDeathSound;
    //public AudioSource wacManDeathSound;
    //public AudioSource music;
    //public AudioSource bulletFireSound;
    //public AudioSource bulletHitSound;
    //public AudioSource gameOverSound;
    //public AudioSource winSound;

    public AudioClip oneUpSound;
    public AudioClip pelletPickUpSound;
    public AudioClip powerUpPickUpSound;
    public AudioClip playerDeathSound;
    public AudioClip wacManDeathSound;
    public AudioClip music;
    public AudioClip bulletFireSound;
    public AudioClip bulletHitSound;
    public AudioClip gameOverSound;
    public AudioClip winSound;

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
        audioSource = GetComponent<AudioSource>();

    }
    public void Update()
    {
        cameraState = m_CameraController.CameraState;
        playerPoweredUp = Toolbox.Instance.m_pickUps.playerPoweredUp;
        wacManPoweredUp = Toolbox.Instance.m_pickUps.wacManPoweredUp;


        //Bullet Sounds
        if (m_BulletTrap.bulletFired == true)
        {
            audioSource.PlayOneShot(bulletFireSound);
            m_BulletTrap.bulletFired = false;
        }
        else if (m_Bullet.bulletHit == true)
        {
            audioSource.PlayOneShot(bulletHitSound);
            m_Bullet.bulletHit = false;
        }
        else
        {
            m_BulletTrap.bulletFired = false;
            m_Bullet.bulletHit = false;
        }
        //PowerUp Sounds
        if (!m_PlayerManager.playedPowerUpSound && m_PlayerManager.playerPoweredUp)
        {
            audioSource.PlayOneShot(powerUpPickUpSound, .5f);
            m_PlayerManager.playedPowerUpSound = true;
           
        }
        else if (m_PlayerManager.playedPowerUpSound)
        {
            m_PlayerManager.playedPowerUpSound = false;
            audioSource.PlayOneShot(powerUpPickUpSound, 0f);
        }
        if (m_enemy.wacManPoweredUp)
        {
            audioSource.PlayOneShot(powerUpPickUpSound, .5f);
        }
        else if (!m_enemy.wacManPoweredUp)
        {
            return;
        }

    }
}
