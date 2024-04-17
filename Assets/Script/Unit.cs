using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Unit : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float moveSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAccelerration = 10f;

    [SerializeField , Range(1f, 5f)]
    float CheckPoint = 1f;

    [Space(20)]
    //이동, 바라보는 방향
    [SerializeField]
    Vector3 velocity,
        Direction;

    enum eDirection
    {
        Up,
        Down,
        Left,
        Right,

    }

    //상호작용 
    bool IsInteraction;

    // Update is called once per frame
    public void Updated()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            IsInteraction = false;
        }

        //상호작용 버튼
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RaycastHit2D hit =
                Physics2D.Raycast(transform.position, Direction, this.CheckPoint,LayerMask.GetMask("NPC"));

            if(hit.collider != null)
            {
                IsInteraction = true;
                velocity = Vector3.zero;
            }
        }

        if(IsInteraction == true)
        {
            return;
        }

        Move();
    }

    void Move()
    {
        Vector2 playerInput;
        
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");


        //삼항 연산자안에 삼항연산자 가능 04/15
        Vector3 directX =
            playerInput.x > 0 ?
            Vector3.right : playerInput.x < 0 ?
            Vector3.left : Vector3.zero;
        Vector3 directY =
            playerInput.y > 0 ?
            Vector3.up : playerInput.y < 0 ?
            Vector3.down : Vector3.zero;

        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        Vector3 desiredVelocity =
            new Vector3(playerInput.x, playerInput.y, 0f) * moveSpeed;

        float maxSpeedChage = maxAccelerration * Time.deltaTime;

        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChage);
        velocity.y =
            Mathf.MoveTowards(velocity.y, desiredVelocity.y, maxSpeedChage);

        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;


        Direction = directX + directY;
        //Debug.Log(Direction);
        transform.localPosition = newPosition;
    }


    /// <summary>
    /// 상호작용 여부 확인
    /// </summary>
    public void CheckInteraction()
    {

    }
}
