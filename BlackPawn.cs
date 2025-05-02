using UnityEngine;

public class BlackPawn : MonoBehaviour
{
    private float rotationAmount = -20f;
    private float moveAmount = 1f;
    private float pawnMoveAmount = -10f;
    private bool isRotate = false;
    private int stepCount;
    private Quaternion startRotation;
    private Vector3 startPosition;
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        stepCount = 0;
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
                    if (movement < 5f && movement > 2f * (pawnMoveAmount - 5f))
                    {
                        if (movement > pawnMoveAmount - 5f)
                        {
                            stepCount++;
                            transform.rotation = startRotation;
                            transform.position += new Vector3(0, -moveAmount, pawnMoveAmount);
                            isRotate = false;
                        }
                        else
                        {
                            if (stepCount == 0)
                            {
                                stepCount++;
                                transform.rotation = startRotation;
                                transform.position += new Vector3(0, -moveAmount, 2f * pawnMoveAmount);
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
