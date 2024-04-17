using UnityEngine;
using System.Collections;

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

            if(i!=0){
                i++;
            }
            int temps = i%20000;
            Vector2 dir = Vector2.zero;
            RaycastHit2D hit;

            if(temps <= 5000 && temps >0){
                dir.x = 1;
            }
            else if (5000 <temps && temps <=10000){
                dir.x = -1;
            }
            else if(10000 < temps && temps <=15000){
                dir.y = 1;
            }
            else if(15000< temps && temps <=20000){
                dir.y = -1;
            }
            else{
                dir.x = 0;
                dir.y = 0;
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

            hit = Physics2D.Raycast(transform.position,transform.right,10f);
            if(hit.collider.name == "PF Player"){
                Debug.DrawRay(transform.position,hit.point,Color.red);
                Debug.Log(hit.collider.name);
                i = 0;
                //problème à résoudre
            }
            else{
                Debug.DrawRay(transform.position,transform.position+transform.right*10f,Color.yellow);
            }

            dir.Normalize();

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }

        
    }
}
