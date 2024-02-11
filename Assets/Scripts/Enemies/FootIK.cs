using UnityEngine;

public class FootIK : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform body;
    [SerializeField] private FootIK otherFoot;
    [SerializeField] private Vector3 footOffset = Vector3.zero;
    [SerializeField] private float stepDistance = 1f;
    [SerializeField] private float stepHeight = 0.1f;
    [SerializeField] private float speed = 1f;

    private Vector3 oldPosition, currentPosition, newPosition;
    private float footSpacing;
    private float lerp = 1f;

    private void Start()
    {
        oldPosition = currentPosition = newPosition = transform.position;
        footSpacing = transform.localPosition.x;
    }

    private void Update()
    {
        transform.position = currentPosition;

        if (Physics.Raycast(body.position + body.right * footSpacing + body.forward * (stepDistance / 2f), Vector3.down, out RaycastHit hitInfo, 10f, groundMask))
        {
            if (Vector3.Distance(hitInfo.point, newPosition) >= stepDistance && !otherFoot.IsMoving() && !IsMoving())
            {
                lerp = 0f;
                newPosition = hitInfo.point + body.forward * stepDistance + footOffset;
            }
        }

        if (IsMoving())
        {
            Vector3 footPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            footPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = footPosition;
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPosition = newPosition;
        }
    }

    public bool IsMoving()
    {
        return lerp < 1f;
    }
}
