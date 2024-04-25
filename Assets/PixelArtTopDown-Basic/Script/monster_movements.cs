using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Mathematics.math;


namespace PixelArtTopDown_Basic.Script
{
    public class MonsterMovements : MonoBehaviour
    {
        public GameObject exclamationMark;
        public CalculationPopUp calculationPopUp;

        public GameObject menuTuto;

        public float speed = 5f;
        public float detectionDistance = 0.5f;
        public float changeDirectionTime = 2f;
        public float PlayerDetectionRadius = 4f;
        public float PlayerCalcDistance = 1f;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private Vector2[] directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        private int currentDirectionIndex = 0;
        private bool PlayerDetected = false;
        private bool isMonsterDead = true;
        
        
        private float timer;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            if (_animator == null || _rigidbody == null)
            {
                Debug.LogError("Animator or Rigidbody2D component is missing!");
                return;
            }
            if (exclamationMark == null || calculationPopUp == null)
            {
                Debug.LogError("exclamationMark or calculationPopUp is not set!");
                return;
            }
            timer = changeDirectionTime;
            exclamationMark.SetActive(false);
        }

        private void Update()
        {
            Vector2 moveDirection = directions[currentDirectionIndex]; ;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, PlayerDetectionRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.name == "PF Player")
                {
                    exclamationMark.SetActive(true);
                    
                    if(SceneManager.GetActiveScene().name == "tuto" && isMonsterDead)
                    {
                        menuTuto.SetActive(true);
                        isMonsterDead = false;
                        Time.timeScale = 0;
                    }
   

                    float distance = Vector2.Distance(transform.position, hit.transform.position);

                    if(!calculationPopUp.IsPopUpActive && distance <= PlayerCalcDistance)
                    {
                        calculationPopUp.GenerateCalculation();
                        moveDirection = Vector2.zero;
                        this.gameObject.SetActive(false);
                    }
                    else
                    {
                        // Calculate the direction to the player
                        moveDirection = (hit.transform.position - transform.position).normalized;
                    }

                    PlayerDetected = true;

                    break;
                }
            }

            if (!PlayerDetected)
            {
                exclamationMark.SetActive(false);
                moveDirection = directions[currentDirectionIndex];
            }

            MoveMonster(moveDirection);
            AnimateMonster(moveDirection);
        }

        private bool IsObstacleInFront()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[currentDirectionIndex], detectionDistance);
            return hit.collider != null && hit.collider.gameObject != gameObject;
        }

        private void ChooseRandNewDirection()
        {
            currentDirectionIndex = (currentDirectionIndex + 2) % directions.Length;
        }

        private void MoveMonster(Vector2 dir)
        {
            _rigidbody.velocity = speed * dir.normalized;
        }

        private void AnimateMonster(Vector2 dir)
        {
            if (dir.magnitude > 0)
            {
                _animator.SetBool(IsMoving, true);
                if (dir.x > 0)
                    _animator.SetInteger(Direction, 2);//R
                else if (dir.x < 0)
                    _animator.SetInteger(Direction, 3);//L
                else if (dir.y > 0)
                    _animator.SetInteger(Direction, 1);//Up
                else if (dir.y < 0)
                    _animator.SetInteger(Direction, 0);//Down
            }
            else
            {
                _animator.SetBool(IsMoving, false);
                _animator.SetInteger(Direction, 4);//Idle
            }
        }

        public void Resume()
        {
            menuTuto.SetActive(false);
            Time.timeScale = 1;
        }
        
    }
}
