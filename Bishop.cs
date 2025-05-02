using static System.Math;
using UnityEngine;

public class Bishop : MonoBehaviour
{
    private float rotationAmount = 10f;
    private float moveAmount = 1f;
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

                    float bishopMoveUD = ((Abs(movement) / 5f) + 1f) / 2f;
                    float bishopStepUD = bishopMoveUD - bishopMoveUD % 1f;

                    float bishopMoveLR = ((Abs(way) / 5f) + 1f) / 2f;
                    float bishopStepLR = bishopMoveLR - bishopMoveLR % 1f;

                    if (bishopStepUD == bishopStepLR)
                    {
                        if (movement > 0 && way > 0)
                        {
                            transform.rotation = startRotation;
                            transform.position += new Vector3(bishopStepLR * 10f, -moveAmount, bishopStepUD * 10f);
                            isRotate = false;
                        }
                        else if (movement > 0 && way < 0)
                        {
                            transform.rotation = startRotation;
                            transform.position += new Vector3(-bishopStepLR * 10f, -moveAmount, bishopStepUD * 10f);
                            isRotate = false;
                        }
                        else if (movement < 0 && way > 0)
                        {
                            transform.rotation = startRotation;
                            transform.position += new Vector3(bishopStepLR * 10f, -moveAmount, -bishopStepUD * 10f);
                            isRotate = false;
                        }
                        else if (movement < 0 && way < 0)
                        {
                            transform.rotation = startRotation;
                            transform.position += new Vector3(-bishopStepLR * 10f, -moveAmount, -bishopStepUD * 10f);
                            isRotate = false;
                        }
                    }
                    else
                    {
                        transform.rotation = startRotation;
                        transform.position -= new Vector3(0, moveAmount, 0);
                        isRotate = false; ;
                    }
                }
            }
        }
    }
}
