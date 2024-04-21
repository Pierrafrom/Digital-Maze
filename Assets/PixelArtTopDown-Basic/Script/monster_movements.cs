using UnityEngine;

namespace PixelArtTopDown_Basic.Script
{
    public class MonsterMovements : MonoBehaviour
    {
        public GameObject exclamationMark;
        public CalculationPopUp calculationPopUp;

        public float speed = 5f;
        public float detectionDistance = 0.5f;
        public float changeDirectionTime = 5f;
        public float PlayerDetectionRadius = 2f;
        public float PlayerCalcDistance = 1f;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private Vector2[] directions = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        private int currentDirectionIndex = 0;
        private bool PlayerDetected = false;
        
        
        private float timer;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            timer = changeDirectionTime;
            exclamationMark.SetActive(false);
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            PlayerDetected = false;

            if (timer <= 0)
            {
                currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;
                timer = changeDirectionTime;
            }

            if (IsObstacleInFront())
                ChooseRandNewDirection();

            Vector2 moveDirection = directions[currentDirectionIndex];

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, PlayerDetectionRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.name == "PF Player")
                {
                    exclamationMark.SetActive(true);

                    float distance = Vector2.Distance(transform.position, hit.transform.position);

                    if(!calculationPopUp.IsPopUpActive && distance <= PlayerCalcDistance)
                    {
                        calculationPopUp.GenerateCalculation();
                        moveDirection = Vector2.zero;
                    }
                    
                    PlayerDetected = true;

                    break;
                }
            }
            if (PlayerDetected == false)
                exclamationMark.SetActive(false);

            MoveMonster(moveDirection);
            AnimateMonster(moveDirection);
        }

        private bool IsObstacleInFront()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[currentDirectionIndex], detectionDistance);
            return hit.collider != null;
        }

        private void ChooseRandNewDirection()
        {
            int newDirectionIndex;
            do
            {
                newDirectionIndex = Random.Range(0, directions.Length);
            }
            while (newDirectionIndex == currentDirectionIndex);

            currentDirectionIndex = newDirectionIndex;
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
    }
}
