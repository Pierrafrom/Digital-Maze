using UnityEngine;

namespace PixelArtTopDown_Basic.Script
{
    public class MonsterMovements : MonoBehaviour
    {
        public float speed;

        private Animator _animator;
        private static readonly int Direction = Animator.StringToHash("Direction");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private int i = 1;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }


        private void Update()
        {
            i++;
            int temps = i%20000;
            Vector2 dir = Vector2.zero;
            if(temps <= 5000){
                dir.x = 1;
            }
            else if (5000 <temps && temps <=10000){
                dir.x = -1;
            }
            else if(10000 < temps && temps <=15000){
                dir.y = 1;
            }
            else{
                dir.y = -1;
            }
            if (dir.x == -1)
            {
                _animator.SetInteger(Direction, 3);
            }
            else if (dir.x == 1)
            {
                _animator.SetInteger(Direction, 2);
            }

            else if (dir.y == 1)
            {
                
                _animator.SetInteger(Direction, 1);
            }
            else if (dir.y == -1)
            {
                _animator.SetInteger(Direction, 0);
            }
            else{
                _animator.SetInteger(Direction,4);
            }

            dir.Normalize();
            _animator.SetBool(IsMoving, dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }

        
    }
}
