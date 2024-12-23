using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [Header("opponemt Movement")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    public CharacterController characterController;
    public Animator animator;

    [Header("opponent Attack")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private string[] attackAnimations = { "Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation" };
    [SerializeField] private float dodgeDistance = 2f;
    [SerializeField] private int attackCount = 0;
    [SerializeField] private int randomNumber;
    [SerializeField] private float attackRadius = 2f;
    public CharacterFighterController[] CharacterFighterControllers;
    public Transform[] players;
    private bool isTakingDamage;
    private float lastAttackTime;
    [Space]

    [Header("Effect and Sound")]

    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;


    [Space]

    public AudioClip[] hitSound;

    [Header("Health")]
   [SerializeField] private int maxHealth = 100;
  public int currentHealth;
    [SerializeField] private HealthBarScript healthBarScript;


    private void Awake()
    {
        currentHealth = maxHealth;
        healthBarScript.GiveFullHealth(currentHealth);
        createRandomNumber();
    }

    private void Update()
    {

        //if(attackCount == randomNumber)
        //{
        //    attackCount = 0;
        //    createRandomNumber();
        //}


        for (int i = 0; i < CharacterFighterControllers.Length; i++)
        {
            if (players[i].gameObject.activeSelf && Vector3.Distance(transform.position, players[i].position) <= attackRadius)
            {
                animator.SetBool("Walking", false);

                if (Time.time - lastAttackTime > attackCooldown)
                {
                    int randomAttackIndex= Random.Range(0, attackAnimations.Length);

                    if (!isTakingDamage)
                    {
                        PerformAttack(randomAttackIndex);
                    }

                    CharacterFighterControllers[i].StartCoroutine(CharacterFighterControllers[i].PlayerHitDamage(attackDamage));
                }
            }
            else
            {
                if (players[i].gameObject.activeSelf)
                {
                    Vector3 direction = (players[i].position - transform.position).normalized;
                    characterController.Move(direction * movementSpeed * Time.deltaTime);

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                    animator.SetBool("Walking", true);

                }
            }
           

        }
    }

    private void PerformAttack(int attackIndex)
    {
     
        
    
            
                animator.Play(attackAnimations[attackIndex]);
                int damage = attackDamage;
                Debug.Log($"Performing attack {attackIndex + 1}, dealing {attackDamage} damage.");
                lastAttackTime = Time.time;
         
        
     
    }
    private void PerformDodge()
    {
        
        
            animator.Play("DodgeFrontAnimation");

            Vector3 dodgeDirection = -transform.forward * dodgeDistance;

            characterController.SimpleMove(dodgeDirection);
        
    }

    void createRandomNumber()
    {
        randomNumber = Random.Range(1, 5);
    }


    public IEnumerator PlayerHitDamage(int damage)
    {
        yield return new WaitForSeconds(0.5f);

        //random hitsound
        if (hitSound != null && hitSound.Length > 0)
        {
            int randomHitIndex = Random.Range(0, hitSound.Length);
            AudioSource.PlayClipAtPoint(hitSound[randomHitIndex], transform.position);
        }

        //Descrease Health

        currentHealth -= damage;
        healthBarScript.setHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        animator.Play("HitDamageAnimation");



    }


    void Die()
    {
        Debug.Log("Opponent Die");
    }


    public void Attack1Effect()
    {
        attack1Effect.Play();
    }

    public void Attack2Effect()
    {
        attack2Effect.Play();
    }

    public void Attack3Effect()
    {
        attack3Effect.Play();
    }

    public void Attack4Effect()
    {
        attack4Effect.Play();
    }



}
