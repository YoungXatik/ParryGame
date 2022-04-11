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
   [SerializeField] private float seeDistance;
   [SerializeField] private float attackDistance;
   [SerializeField] public bool stunned;
   [SerializeField] public int enemyHealth;
   [SerializeField] public int enemyDamage;
   [SerializeField] public Player player;
   [SerializeField] public bool canAttack;

   [Header("PlayerShieldAndWeapon")]
   [SerializeField] public ShieldScript shieldScript;

   [SerializeField] public WeaponScript weaponScript;

   [SerializeField] public NavMeshAgent enemy;
   [SerializeField] private GameObject attackButton;
   [SerializeField] private GameObject hitText;
   [SerializeField] public KickIt hitTextAnim;

   [Header("Enemies")]
   [SerializeField] public GameObject nextEnemy;

   [SerializeField] public bool isLastEnemy;
   [SerializeField] public LevelMovement levelMovement;

   [Header("Effect")]
   [SerializeField] public GameObject stunEffect;
   [SerializeField] public GameObject deadEffect;
   [SerializeField] public AudioSource hitSource;
   [SerializeField] public AudioClip hitClip;
   [SerializeField] public AudioClip walkClip;
   [SerializeField] public AudioClip punchHitClip;
   [SerializeField] public AudioClip givenHitClip;


   private void Awake()
   {
      hitTextAnim = FindObjectOfType<KickIt>();
   }

   private void Start()
   {
      enemyAnimator = GetComponent<Animator>();
      playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
      shieldScript = FindObjectOfType<ShieldScript>();
      weaponScript = FindObjectOfType<WeaponScript>();
      enemyAnimator.SetBool("Walk",false);
      player = FindObjectOfType<Player>();

      hitSource = GetComponent<AudioSource>();
      stunEffect.SetActive(false);
      hitText.SetActive(false);
      
      
      
   }

   private void Update()
   {
      if (enemyHealth <= 0)
      {
         enemyAnimator.SetBool("Dead",true);
         hitTextAnim.EndSecondState();
         hitText.SetActive(false);
      }

      if (Vector3.Distance(transform.position, playerTarget.position) < seeDistance)
      {
         enemyAnimator.SetBool("Walk",true);
         enemy.SetDestination(playerTarget.position);
         //transform.DOMove(playerTarget.position, speed);
         //Invoke("EnemyPunch",speed - 1);
         if (Vector3.Distance(transform.position, playerTarget.position) < attackDistance)
         {
            canAttack = true;
         }
      }

      if (canAttack)
      {
         Invoke("StartCombatPose",0.1f);
      }
      else
      {
         return;
      }

      if (stunned)
      {
         attackButton.SetActive(true);
      }
      else
      {
         attackButton.SetActive(false);
      }
      
      stunEffect.SetActive(stunned);
      
   }

   public void StartCombatPose()
   {
      enemyAnimator.SetBool("CombatPose",true);
   }

   public void EnemyPunch()
   {
      canAttack = false;
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
      gameObject.GetComponent<LookAtConstraint>().enabled = false;
   }

   public void PunchEvent()
   {
      if (shieldScript.Block)
      {
         enemyAnimator.SetBool("GetHit",true);
         player.blockEffect.SetActive(true);
         hitSource.PlayOneShot(hitClip);
         SlowMotionStart();
         stunned = true;
         hitText.SetActive(true);
      }
      else
      {
         enemyAnimator.SetBool("GetHit",false);
         player.TakeDamage(enemyDamage);
         player.deadAnimator.SetBool("GetHit",true);
         player.Invoke("CameraBack",1f);
         hitSource.PlayOneShot(givenHitClip);
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
      if (!isLastEnemy)
      {
         nextEnemy.GetComponent<EnemyMovement>().seeDistance =
            Vector3.Distance(nextEnemy.transform.position, playerTarget.position) + 1;
      }
      
      levelMovement.MoveNext();
      attackButton.SetActive(false);
      Destroy(gameObject);
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
      Time.timeScale = 1f;
      enemyAnimator.SetBool("Stunned",true);
      stunned = true;
      player.blockEffect.SetActive(false);
   }

   public void SlowMotionStart()
   {
      if (shieldScript.Block)
      {
         Time.timeScale = 0.3f;
         Invoke("SlowMotionEnd",1f);
      }
   }

   public void SlowMotionEnd()
   {
      Time.timeScale = 1f;
   }
   
}
