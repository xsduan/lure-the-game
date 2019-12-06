using UnityEngine;

/// <summary>
/// Simplified bobbing behavior - this doesn't take into account mass, surface area
/// submerged, etc.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour {
    #region Inspector Variables
    public float WobbleSize;
    public float WobbleStrength;
    #endregion

    private readonly float wobbleScale = 0.001f;

    private float ScaledSize => WobbleSize * wobbleScale;
    private float ScaledStrength => WobbleStrength * wobbleScale;

    private Rigidbody rb;
    private float originalHeight;
    private float lastScale;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        originalHeight = transform.position.y;
        lastScale = 1 - ScaledStrength;
    }

    private void FixedUpdate() => rb.AddForce(-Physics.gravity * CalculateBuoyantForce(), ForceMode.Acceleration);

    private float CalculateBuoyantForce() {
        float heightDiff = originalHeight - transform.position.y;
        if (Mathf.Abs(heightDiff) >= ScaledSize) {
            lastScale = 1 + Mathf.Sign(heightDiff) * ScaledStrength;
        }
        return lastScale;
    }
}
