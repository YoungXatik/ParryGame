using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;
using Random = System.Random;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] private Transform playerTarget;
   [SerializeField] private float speed;
   [SerializeField] private Animator enemyAnimator;
   [SerializeField] public float seeDistance;
   [SerializeField] private float attackDistance;
   [SerializeField] public bool stunned;
   [SerializeField] public int enemyHealth;
   [SerializeField] public int enemyDamage;
   [SerializeField] public Player player;
   [SerializeField] public bool canAttack;
   [SerializeField] public bool isArmed;
   [SerializeField] public bool isBoss;
   [SerializeField] public bool isNinja;

   [Header("PlayerShieldAndWeapon")]
   [SerializeField] public ShieldScript shieldScript;

   [SerializeField] public WeaponScript weaponScript;

   [SerializeField] public NavMeshAgent enemy;
   [SerializeField] private GameObject attackButton;
   [SerializeField] private GameObject hitText;
   [SerializeField] private GameObject parryText;
   [SerializeField] public KickIt hitTextAnim;

   [Header("Enemies")]
   [SerializeField] public GameObject nextEnemy;

   [SerializeField] public bool isLastEnemy;
   [SerializeField] public LevelMovement levelMovement;

   [Header("Effect")]
   [SerializeField] public GameObject stunEffect;
   [SerializeField] public GameObject deadEffect;
   [SerializeField] private GameObject slowMotionEffect;
   [SerializeField] public AudioSource hitSource;
   [SerializeField] public AudioClip hitClip;
   [SerializeField] public AudioClip walkClip;
   [SerializeField] public AudioClip punchHitClip;
   [SerializeField] public AudioClip givenHitClip;

   [Header("0 - 1")]
   [SerializeField] private float timeScale;

   [SerializeField] private GameObject backgroundMusic;
   [SerializeField] private float backgroundMusicVolumeDown;


   private void Awake()
   {
      hitTextAnim = FindObjectOfType<KickIt>();
   }

   private void Start()
   {
      backgroundMusic = GameObject.FindWithTag("BackGroundMusic");
      backgroundMusicVolumeDown = 0.2f;
      
      enemy.stoppingDistance = 2f;
      speed = 3f;
      attackDistance = 3f;
      
      slowMotionEffect.SetActive(false);
      
      if (isArmed)
      {
         enemyAnimator.SetBool("SwordIdle",true);
      }

      if (isNinja)
      {
         enemyAnimator.SetBool("NinjaIdle",true);
      }
      
      enemyAnimator = GetComponent<Animator>();
      playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      shieldScript = FindObjectOfType<ShieldScript>();
      weaponScript = FindObjectOfType<WeaponScript>();
      player = FindObjectOfType<Player>();

      hitSource = GetComponent<AudioSource>();
      stunEffect.SetActive(false);
      hitText.SetActive(false);
      parryText.SetActive(false);
   }

   private void Update()
   {
      if (enemyHealth <= 0)
      {
         if (isArmed == false)
         {
            if (isNinja)
            {
               enemyAnimator.SetBool("NinjaDie", true);
               hitTextAnim.EndSecondState();
               hitText.SetActive(false);
               shieldScript.canAttack = false;
            }
            else
            {
               enemyAnimator.SetBool("Dead", true);
               hitTextAnim.EndSecondState();
               hitText.SetActive(false);
               shieldScript.canAttack = false;
            }
         }
         else if(isArmed)
         {
            enemyAnimator.SetBool("SwordDead", true);
            hitTextAnim.EndSecondState();
            hitText.SetActive(false);
            shieldScript.canAttack = false;
         }
         
      }

      if (Vector3.Distance(transform.position, playerTarget.position) < seeDistance)
      {
         enemyAnimator.SetBool("Walk", true);
            enemy.SetDestination(playerTarget.position);
            if (Vector3.Distance(transform.position, playerTarget.position) < attackDistance)
            {
               canAttack = true;
            }

      }

      if (canAttack)
      {
         Invoke("StartCombatPose",0.5f);
      }
      else
      {
         return;
      }

      if (stunned)
      {
         attackButton.SetActive(true);
         parryText.SetActive(false);
      }
      else
      {
         attackButton.SetActive(false);
      }
      
      stunEffect.SetActive(stunned);
      
   }

   public void StartCombatPose()
   {
      if (isNinja)
      {
         enemyAnimator.SetBool("NinjaCombatPose",true);
         parryText.SetActive(true);
      }
      else if (isArmed == false)
      {
         enemyAnimator.SetBool("CombatPose", true);
         parryText.SetActive(true);
      }
      else if(isArmed)
      {
         enemyAnimator.SetBool("SwordCombatPose", true);
         parryText.SetActive(true);
      }
   }

   public void EnemyPunch()
   {
      if(isArmed == false)
      {
         if (isNinja)
         {
            Random rand = new Random();
            int numberOfPunch = rand.Next(1, 4);
      
            if (numberOfPunch == 1)
            {
               enemyAnimator.SetBool("NinjaPunch1",true);
            }
            else if(numberOfPunch == 2)
            {
               enemyAnimator.SetBool("NinjaPunch2",true);
            }
            else if(numberOfPunch == 3)
            {
               enemyAnimator.SetBool("NinjaPunch3",true);
            }
         }
         else if(isNinja == false)
         {
            Random rand = new Random();
            int numberOfPunch = rand.Next(1, 4);
      
            if (numberOfPunch == 1)
            {
               enemyAnimator.SetBool("Punch",true);
            }
            else if(numberOfPunch == 2)
            {
               enemyAnimator.SetBool("Punch1",true);
            }
            else if(numberOfPunch == 3)
            {
               enemyAnimator.SetBool("Punch2",true);
            }
         }
         
      }
      else if(isArmed)
      {
         enemyAnimator.SetBool("SwordSlash",true);
      }
      
      canAttack = false;
      gameObject.GetComponent<LookAtConstraint>().enabled = false;
   }

   public void PunchEvent()
   {
      if (isArmed == false)
      {
         if (shieldScript.Block)
         {
            enemyAnimator.SetBool("GetHit",true);
            player.blockEffect.gameObject.SetActive(true);
            player.blockEffect.Play();
            hitSource.PlayOneShot(hitClip);
            stunned = true;
            hitText.SetActive(true);
            hitTextAnim.EndParrySecondState();
            parryText.SetActive(false);
            slowMotionEffect.SetActive(false);
            SlowMotionEnd();
            shieldScript.canAttack = false;
         }
         else
         {
            enemyAnimator.SetBool("GetHit",false);
            player.TakeDamage(enemyDamage);
            player.deadAnimator.SetBool("GetHit",true);
            player.Invoke("CameraBack",1f);
            hitSource.PlayOneShot(givenHitClip);
            parryText.SetActive(false);
            hitTextAnim.EndParrySecondState();
            slowMotionEffect.SetActive(false);
            SlowMotionEnd();
         }
      }
      else
      {
         if (shieldScript.Block)
         {
            enemyAnimator.SetBool("SwordGetHit",true);
            player.blockEffect.gameObject.SetActive(true);
            player.blockEffect.Play();
            player.blockEffect.Play();
            hitSource.PlayOneShot(hitClip);
            stunned = true;
            hitText.SetActive(true);
            hitTextAnim.EndParrySecondState();
            parryText.SetActive(false);
            slowMotionEffect.SetActive(false);
            SlowMotionEnd();
            shieldScript.canAttack = false;
         }
         else
         {
            enemyAnimator.SetBool("SwordGetHit",false);
            player.TakeDamage(enemyDamage);
            player.deadAnimator.SetBool("GetHit",true);
            player.Invoke("CameraBack",1f);
            hitSource.PlayOneShot(givenHitClip);
            hitTextAnim.EndParrySecondState();
            parryText.SetActive(false);
            slowMotionEffect.SetActive(false);
            SlowMotionEnd();
         }
      }
      
      
   }

   public void EndStunnedState()
   {
      enemyAnimator.SetBool("Stunned",false);
      stunned = false;
   }

   public void TakeDamage(int damage)
   {
      enemyHealth -= damage;
   }

   public void Dead()
   {
      if (isBoss)
      {
         Debug.Log("Boss defeated");
         parryText.SetActive(false);
         Destroy(parryText);
      }
      
      if (!isLastEnemy)
      {
         nextEnemy.GetComponent<EnemyMovement>().seeDistance =
            Vector3.Distance(nextEnemy.transform.position, playerTarget.position) + 1;
      }

      stunned = false;
      levelMovement.MoveNext();
      attackButton.SetActive(false);
      Destroy(gameObject,4f);
      gameObject.transform.DOMove(
         new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 3,
            gameObject.transform.position.z), 8f);
      
   }
   

   public void PlayDeadEffect()
   {
      //deadEffect.SetActive(true);
   }

   public void PlayWalkSound()
   {
      hitSource.PlayOneShot(walkClip);
   }

   public void PlayHitSound()
   {
      hitSource.PlayOneShot(punchHitClip);
   }

   public void StunnedIdlePlay()
   {
      enemyAnimator.SetBool("Stunned",true);
      stunned = true;
      player.blockEffect.gameObject.SetActive(false);
      shieldScript.canAttack = true;
   }

   public void SlowMotionStart()
   {
      Time.timeScale = timeScale;
      backgroundMusic.GetComponent<AudioSource>().volume -= backgroundMusicVolumeDown;
      slowMotionEffect.SetActive(true);
   }

   public void SlowMotionEnd()
   {
      Time.timeScale = 1f;
      backgroundMusic.GetComponent<AudioSource>().volume += backgroundMusicVolumeDown;
      slowMotionEffect.SetActive(false);
   }
   
   
}
