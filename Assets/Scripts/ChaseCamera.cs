using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{

    public static Transform player;
    [SerializeField] float distance = 1f;
    [SerializeField] float height = 1f;
    [SerializeField] Vector3 offset = new Vector3(0, 1, 0);

    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float rotSpeed = 15f;

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 lookPos = player.position + offset;
        Vector3 relativePos = lookPos - transform.position;
        Quaternion rot = Quaternion.LookRotation(relativePos);

        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.fixedDeltaTime * rotSpeed);

        Vector3 targetPos = player.position + player.up * height - player.forward * distance;

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * moveSpeed);
    }
}

/*using Cinemachine;
using Mirror;
using UnityEngine;

    public class PlayerCameraController : NetworkBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
        [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
        [SerializeField] private Transform playerTransform = null;
        [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

        private Controls controls;
        private Controls Controls
        {
            get
            {
                if (controls != null) { return controls; }
                return controls = new Controls();
            }
        }

        private CinemachineTransposer transposer;

        public override void OnStartAuthority()
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

            virtualCamera.gameObject.SetActive(true);

            enabled = true;

            Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
        }

        [ClientCallback]
        private void OnEnable() => Controls.Enable();
        [ClientCallback]
        private void OnDisable() => Controls.Disable();

        private void Look(Vector2 lookAxis)
        {
            float deltaTime = Time.deltaTime;

            transposer.m_FollowOffset.y = Mathf.Clamp(
                transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * deltaTime),
                maxFollowOffset.x,
                maxFollowOffset.y);

            playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * deltaTime, 0f);
        }
    }

*/