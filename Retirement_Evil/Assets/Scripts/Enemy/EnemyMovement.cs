using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState {
    Patrolling,
    Chasing,
    Attacking,
    Waiting
}

public class Enemy : MonoBehaviour {
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";
    public Transform[] waypoints;
    public float speed;
    public float waitTime;
    private int targetPoint;
    private Transform player;
    private EnemyState currentState;
    private float waitTimer;

    private BoxCollider2D bodyCollider;
    private Rigidbody2D rb;

    public float fovAngle;
    public float fovRange;
    public int damage;
    private Vector2 lookDirection; // direzione del nemico

    private Animator animator;

    

    void Start() {
        targetPoint = 0;
        currentState = EnemyState.Patrolling;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bodyCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        switch(currentState) {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Waiting:
                Wait();
                break;
        }
    }

    void Patrol() {
        if (Vector2.Distance(transform.position, waypoints[targetPoint].position) < 0.1f) {
            targetPoint = (targetPoint + 1) % waypoints.Length;
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[targetPoint].position, speed * Time.deltaTime);
        UpdateLookDirection(waypoints[targetPoint].position);
        // Controlla se il giocatore è nel FOV del nemico
        if (IsTargetInsideFOV(player)) {
            currentState = EnemyState.Chasing;
            FindObjectOfType<AudioManager>().Play("Zombie");
        }
    }

    void Chase() {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        UpdateLookDirection(player.position);
        if(!IsTargetInsideFOV(player)) {
            currentState = EnemyState.Patrolling;
        }
        if (Vector2.Distance(transform.position, player.position) < 0.5f) {
            currentState = EnemyState.Attacking;
            waitTimer = waitTime;
        }
    }

    void Attack() {
        rb.isKinematic = true;
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        FindObjectOfType<AudioManager>().Play("Zombie");
        currentState = EnemyState.Waiting;
    }

    void Wait() {
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y); 
        Vector2 direction = position2D.normalized;
        lookDirection = new Vector2(direction.x, direction.y);
        animator.SetFloat(lastHorizontal, lookDirection.x);
        animator.SetFloat(lastVertical, lookDirection.y);
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0) {
            rb.isKinematic = false;
            currentState = EnemyState.Patrolling;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && currentState == EnemyState.Chasing) {
            currentState = EnemyState.Attacking;
            waitTimer = waitTime;
        }
    }

     private void UpdateLookDirection(Vector2 targetPosition) {
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y); 
        Vector2 direction = (targetPosition - position2D).normalized;
        lookDirection = new Vector2(direction.x, direction.y);
        animator.SetFloat(horizontal, direction.x);
        animator.SetFloat(vertical, direction.y);
        if (speed <= 0f) {
            animator.SetFloat(lastHorizontal, direction.x);
            animator.SetFloat(lastVertical, direction.y);
        }
    }

    public bool IsTargetInsideFOV(Transform target) {
        Vector2 directionToTarget = (target.position - transform.position).normalized;

        float angleTotarget = Vector2.Angle(lookDirection, directionToTarget);

        if(angleTotarget < fovAngle / 2) {
            float distance = Vector2.Distance(target.position, transform.position);
            return distance < fovRange;
        }

        return false;
    }
}