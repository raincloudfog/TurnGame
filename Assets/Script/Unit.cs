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
    //�̵�, �ٶ󺸴� ����
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

    //��ȣ�ۿ� 
    bool IsInteraction;

    // Update is called once per frame
    public void Updated()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            IsInteraction = false;
        }

        //��ȣ�ۿ� ��ư
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


        //���� �����ھȿ� ���׿����� ���� 04/15
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
    /// ��ȣ�ۿ� ���� Ȯ��
    /// </summary>
    public void CheckInteraction()
    {

    }
}
