using RiptideNetworking;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    private int smooth_Steps = 10;

    [SerializeField]
    private float smooth_Weight = 0.4f;

    [SerializeField]
    private float roll_angle = 10f;

    [SerializeField]
    private Vector2 look_Limit = new Vector2(-70f, 80f);

    [SerializeField]
    private float Roll_Speed = 3f;

    private Vector2 look_Angles;
    private Vector2 current_Mouse_Look;
    private Vector2 smooth_Move;
    private float current_Roll_Angle;
    private int last_Look_Frame;
    


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CursorLockAndUnlocked();
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            SendRotation();
            LookAround();
        }
    }
    void CursorLockAndUnlocked()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    void LookAround()
    {
        current_Mouse_Look = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));

        look_Angles.x += current_Mouse_Look.x * sensitivity * (invert ? 1f : -1f);
        look_Angles.y += current_Mouse_Look.y * sensitivity;
        look_Angles.x = Mathf.Clamp(look_Angles.x, look_Limit.x, look_Limit.y);
        current_Roll_Angle = Mathf.Lerp(current_Roll_Angle, Input.GetAxisRaw("Mouse X") * roll_angle, Time.deltaTime * Roll_Speed);
        lookRoot.localRotation = Quaternion.Euler(look_Angles.x, 0f, current_Roll_Angle);
        playerRoot.localRotation = Quaternion.Euler(0f, look_Angles.y, 0f);
    }
    private void SendRotation()
    {
        Message message = Message.Create(MessageSendMode.unreliable, ClientToServerId.playerRotation);
        message.AddQuaternion(lookRoot.localRotation);
        message.AddQuaternion(playerRoot.localRotation);
        NetworkManager.instance.client.Send(message);
    }
}
