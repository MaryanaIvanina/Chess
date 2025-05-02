using static System.Math;
using UnityEngine;

public class Queen : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
    private float queenMoveAmount = 10f;
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

                    float queenMoveUD = ((Abs(movement) / 5f) + 1f) / 2f;
                    float queenStepUD = queenMoveUD - queenMoveUD % 1f;

                    float queenMoveLR = ((Abs(way) / 5f) + 1f) / 2f;
                    float queenStepLR = queenMoveLR - queenMoveLR % 1f;

                    if (Abs(way) < 5 && Abs(movement) > 5)
                    {
                        float queenMove = ((Abs(movement) / 5f) + 1f) / 2f;
                        float queenStep = queenMove - queenMove % 1f;
                        float Steps = queenStep * queenMoveAmount;
                        transform.rotation = startRotation;
                        if (movement > 0)
                            transform.position += new Vector3(0, -moveAmount, Steps);
                        else
                            transform.position += new Vector3(0, -moveAmount, -Steps);
                        isRotate = false;
                    }
                    else if (Abs(movement) < 5 && Abs(way) > 5)
                    {
                        float queenMove = ((Abs(way) / 5f) + 1f) / 2f;
                        float queenStep = queenMove - queenMove % 1f;
                        float Steps = queenStep * queenMoveAmount;
                        transform.rotation = startRotation;
                        if (way > 0)
                            transform.position += new Vector3(Steps, -moveAmount, 0);
                        else
                            transform.position += new Vector3(-Steps, -moveAmount, 0);
                        isRotate = false;
                    }
                    else if (queenStepUD == queenStepLR)
                    {
                        if (movement > 0 && way > 0)
                        {
                            transform.rotation = startRotation;
                            transform.position += new Vector3(queenStepLR * 10f, -moveAmount, queenStepUD * 10f);
                            isRotate = false;
                        }
                        else if (movement > 0 && way < 0)
                        {
                            transform.rotation = startRotation;
                            transform.position += new Vector3(-queenStepLR * 10f, -moveAmount, queenStepUD * 10f);
                            isRotate = false;
                        }
                        else if (movement < 0 && way > 0)
                        {
                            transform.rotation = startRotation;
                            transform.position += new Vector3(queenStepLR * 10f, -moveAmount, -queenStepUD * 10f);
                            isRotate = false;
                        }
                        else if (movement < 0 && way < 0)
                        {
                            transform.rotation = startRotation;
                            transform.position += new Vector3(-queenStepLR * 10f, -moveAmount, -queenStepUD * 10f);
                            isRotate = false;
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
