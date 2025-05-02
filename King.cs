using static System.Math;
using UnityEngine;

public class King : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
    private float kingMoveAmount = 10f;
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
                        transform.Rotate(rotationAmount, 0, 0);
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

                    float kingMoveUD = ((Abs(movement) / 5f) + 1f) / 2f;
                    float kingStepUD = kingMoveUD - kingMoveUD % 1f;

                    float kingMoveLR = ((Abs(way) / 5f) + 1f) / 2f;
                    float kingStepLR = kingMoveLR - kingMoveLR % 1f;

                    if (Abs(way) < 5 && Abs(movement) > 5 && Abs(movement) < 15)
                    {
                        transform.rotation = startRotation;
                        if (movement > 0)
                            transform.position += new Vector3(0, -moveAmount, kingMoveAmount);
                        else
                            transform.position += new Vector3(0, -moveAmount, -kingMoveAmount);
                        isRotate = false;
                    }
                    else if (Abs(movement) < 5 && Abs(way) > 5 && Abs(way) < 15)
                    {
                        transform.rotation = startRotation;
                        if (way > 0)
                            transform.position += new Vector3(kingMoveAmount, -moveAmount, 0);
                        else
                            transform.position += new Vector3(-kingMoveAmount, -moveAmount, 0);
                        isRotate = false;
                    }
                    else if(kingStepUD == kingStepLR)
                    {
                        transform.rotation = startRotation;
                        if (way > 0 && movement > 0)
                            transform.position += new Vector3(kingMoveAmount, -moveAmount, kingMoveAmount);
                        else if (way < 0 && movement > 0)
                            transform.position += new Vector3(-kingMoveAmount, -moveAmount, kingMoveAmount);
                        else if (way > 0 && movement < 0)
                            transform.position += new Vector3(kingMoveAmount, -moveAmount, -kingMoveAmount);
                        else
                            transform.position += new Vector3(-kingMoveAmount, -moveAmount, -kingMoveAmount);
                        isRotate = false;
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