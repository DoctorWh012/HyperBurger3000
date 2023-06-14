using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovement", menuName = "RUSP/PlayerMovement", order = 0)]
public class PlayerMovementSettings : ScriptableObject
{
    // The use of a scriptable object for the movement settings is because this script is used in my multiplayer game that has 2 types of player, it makes it easier to ajust the movement for both

    //----MOVEMENT SETTINGS----
    [Space]
    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed = 13;
    [SerializeField] public float crouchedSpeedMultiplier = 1.2f;
    [SerializeField] public float groundDrag = 5;
    [SerializeField] public float airDrag = 0;
    [SerializeField] public float jumpForce = 15;
    [SerializeField] public float maxSlopeAngle = 45;
    [SerializeField] public float gravity = 10;
    [SerializeField] public float coyoteTime = 0.2f;
    [SerializeField] public float jumpBufferTime = 0.1f;

    //----OTHER SETTINGS----
    [Space]
    [Header("Other Settings")]
    [SerializeField] public float groundCheckHeight = 0.2f;
    [SerializeField] public float jumpCooldown = 0.3f;
    [SerializeField] public float airMultiplier = 4;
    [SerializeField] public float wallDistance = 1;

}