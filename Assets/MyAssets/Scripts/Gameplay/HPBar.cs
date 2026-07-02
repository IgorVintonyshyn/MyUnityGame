using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
