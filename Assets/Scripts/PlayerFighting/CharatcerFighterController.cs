using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFighterController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotationSpeed = 10f;
    private CharacterController characterController;
    private Animator animator;

    [Header("Player Attack")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private string[] attackAnimations = { "Attack1Animation", "Attack2Animation", "Attack3Animation", "Attack4Animation" };
    [SerializeField] private float dodgeDistance = 2f;
    [SerializeField] private float AttackRadius = 2.2f;
    public Transform[] Opponentplayers;
    private float lastAttackTime;

    [Space]

    [Header("Effect and Sound")]

    public ParticleSystem attack1Effect;
    public ParticleSystem attack2Effect;
    public ParticleSystem attack3Effect;
    public ParticleSystem attack4Effect;

    [Space]

  

    [Header("Health")]
  [SerializeField]  private int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private HealthBarScript healthBarScript;

    [Space]

    [Header("audio")]
    public AudioClip[] hitSound;
    [SerializeField] private AudioSource attacksound;
    [SerializeField] private AudioClip dodgeClip;







    private void Awake()
    {
        currentHealth = maxHealth;
        healthBarScript.GiveFullHealth(currentHealth);
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PerformMovement();
        PerformDodge();

        if (Input.GetKeyDown(KeyCode.K))
        {
            PerformAttack(0);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PerformAttack(1);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PerformAttack(2);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            PerformAttack(3);
        }
    }

    private void PerformMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-verticalInput, 0f, horizontalInput);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        characterController.Move(movement * movementSpeed * Time.deltaTime);
    }

    private void PerformAttack(int attackIndex)
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            if (attackIndex >= 0 && attackIndex < attackAnimations.Length)
            {
                animator.Play(attackAnimations[attackIndex]);
                int damage = attackDamage;
                Debug.Log($"Performing attack {attackIndex + 1}, dealing {attackDamage} damage.");
                lastAttackTime = Time.time;

                foreach (Transform Opponentplayers in Opponentplayers)
                {
                    if(Vector3.Distance(transform.position,Opponentplayers.position)<= AttackRadius)
                    {
                        Opponentplayers.GetComponent<OpponentAI>().StartCoroutine(Opponentplayers.GetComponent<OpponentAI>().PlayerHitDamage(attackDamage));
                    }
                }

            }
            else
            {
                Debug.LogError("Invalid attack index!");
            }
        }
        else
        {
            Debug.Log("Cannot perform attack: Attack is on cooldown.");
        }
    }

    private void PerformDodge()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("DodgeFrontAnimation");
            attacksound.clip = dodgeClip;
            attacksound.Play();

            Vector3 dodgeDirection = transform.forward * dodgeDistance;

            characterController.Move(dodgeDirection);
        }
    }

    public IEnumerator PlayerHitDamage(int damage)
    {
        yield return new WaitForSeconds(0.5f);

        //random hitsound
        if (hitSound != null && hitSound.Length > 0)
        {
            int randomHitIndex= Random.Range(0,hitSound.Length);
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
        Debug.Log("Player Die");
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
