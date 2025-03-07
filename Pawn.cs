using UnityEngine;

public class Pawn : MonoBehaviour
{
    public float rotationAmount = 20f;
    public float moveAmount = 0.5f;
    private bool isRotate = false;
    private Quaternion startRotation;
    void Start()
    {
        startRotation = transform.rotation;
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
                        transform.position += new Vector3(0, moveAmount,0);
                        isRotate = true;
                    }
                    else
                    {
                        transform.rotation = startRotation;
                        isRotate = false;
                    }
                }
                else if (!isRotate)
                {
                    return;
                }
                else
                {
                    transform.rotation = startRotation;
                    Vector3 newPosition = hit.point;
                    newPosition.y = transform.position.y;
                    transform.position = newPosition;
                    isRotate = false;
                }
            }
        }

    }

}
