using UnityEngine;

public class MobileUI : MonoBehaviour
{
    [SerializeField] private GameObject leftJoystick;
    [SerializeField] private GameObject rightJoystick;

    [SerializeField] private bool forceMobileControls;

    private void Awake()
    {
        bool mobile = Application.isMobilePlatform || forceMobileControls;

        leftJoystick.SetActive(mobile);
        rightJoystick.SetActive(mobile);
    }
}
