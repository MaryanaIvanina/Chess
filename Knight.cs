using static System.Math;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
    private float knightMoveAmount1 = 20f;
    private float knightMoveAmount2 = 10f;
    private bool isRotate = false;
    private Quaternion startRotation;
    private Vector3 startPosition;
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (!isRotate)
                    {
                        transform.Rotate(0, 0, -rotationAmount);
                        transform.position += new Vector3(0, moveAmount, 0);
                        isRotate = true;
                    }
                    else
                    {
                        transform.rotation = startRotation;
                        transform.position -= new Vector3(0, moveAmount, 0);
                        isRotate = false;
                    }
                }
                else if (!isRotate)
                {
                    return;
                }
                else
                {
                    Vector3 newPosition = hit.point;
                    float movement = newPosition.z - transform.position.z;
                    float way = newPosition.x - transform.position.x;
                    if(Abs(way)<15 && Abs(way) >5 && Abs(movement) <25 && Abs(movement) > 15)
                    {
                        transform.rotation = startRotation;
                        isRotate = false;
                        if(movement > 0 && way > 0)
                        {
                            transform.position += new Vector3(knightMoveAmount2, -moveAmount, knightMoveAmount1);
                        }
                        else if (movement > 0 && way < 0)
                        {
                            transform.position += new Vector3(-knightMoveAmount2, -moveAmount, knightMoveAmount1);
                        }
                        else if (movement < 0 && way > 0)
                        {
                            transform.position += new Vector3(knightMoveAmount2, -moveAmount, -knightMoveAmount1);
                        }
                        else if (movement < 0 && way < 0)
                        {
                            transform.position += new Vector3(-knightMoveAmount2, -moveAmount, -knightMoveAmount1);
                        }
                    }
                    else if(Abs(way) > 15 && Abs(way) < 25 && Abs(movement) > 5 && Abs(movement) < 15)
                    {
                        transform.rotation = startRotation;
                        isRotate = false;
                        if (movement > 0 && way > 0)
                        {
                            transform.position += new Vector3(knightMoveAmount1, -moveAmount, knightMoveAmount2);
                        }
                        else if (movement > 0 && way < 0)
                        {
                            transform.position += new Vector3(-knightMoveAmount1, -moveAmount, knightMoveAmount2);
                        }
                        else if (movement < 0 && way > 0)
                        {
                            transform.position += new Vector3(knightMoveAmount1, -moveAmount, -knightMoveAmount2);
                        }
                        else if (movement < 0 && way < 0)
                        {
                            transform.position += new Vector3(-knightMoveAmount1, -moveAmount, -knightMoveAmount2);
                        }
                    }
                    else
                    {
                        transform.rotation = startRotation;
                        transform.position -= new Vector3(0, moveAmount, 0);
                        isRotate = false;
                    }
                }
            }
        }
    }
}
