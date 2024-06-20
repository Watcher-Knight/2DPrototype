using UnityEngine;


[CreateAssetMenu(fileName = "Dasher", menuName = AssetPaths.DasherData, order = 0)]
public class DasherData : ScriptableObject
{
    [field:Header("Dashing")]

    [Range(1.1f, 10f)]
    [SerializeField] 
    public float DashForce = 5f;
    [Range(0.1f,0.99f)]
    [SerializeField]
    public float DragForce = 0.75f;

    [Range(0.0f, 10.0f)]
    [SerializeField]
    public float dashCooldown = 5.0f;

    [Range(0.1f,100f)]
    [SerializeField]
    public float dashVelocity = 6.0f;

    [Header("Animator Data")]
    [SerializeField]
    public float distanceForAfterImage = 0.5f;
}

