using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rowing : MonoBehaviour {
    #region Inspector Variables
    public float RowInterval;
    public float StrokeStrength;
    public int StrokeFrames;
    public GameObject LeftOar;
    public GameObject RightOar;
    #endregion

    private Rigidbody rb;
    private Coroutine rowCo;
    private bool complete = true;

    private void Start() => rb = GetComponent<Rigidbody>();

    private void FixedUpdate() {
        bool idle = Mathf.Approximately(
            Mathf.Abs(Input.GetAxis("Vertical")) +
            Mathf.Abs(Input.GetAxis("Horizontal")), 0);

        if (idle && rowCo != null) {
            rowCo = null;
        } else if (!idle && complete && rowCo == null) {
            complete = false;
            rowCo = StartCoroutine(Row());
        }
    }

    /// <summary>
    /// Rowing behavior. Essentially, the direction the player inputs on the joystick is converted into
    /// how much force each oar should provide, and is applied slowly over some frames, using a bell curve like function.
    /// </summary>
    private IEnumerator Row() {
        do {
            // TODO: grab oar animation
            Coroutine rowStrokeCo = StartCoroutine(RowStroke());
            yield return new WaitForSeconds(RowInterval);
            yield return rowStrokeCo; // ensure rowing coroutines are complete, we can sacrifice a couple frames
        } while (rowCo != null);
        complete = true;
    }

    private IEnumerator RowStroke() {
        // TODO: trigger oar animations
        for (int t = 0; t < StrokeFrames; t++) {
            (float left, float right) force = GetRowForceFromAxis(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
            ForceOar(t, force.left, LeftOar);
            ForceOar(t, force.right, RightOar);
            yield return null;
        }

        void ForceOar(float t, float scale, GameObject location) {
            // need a roughly bell shaped curve
            // gaussian is just a bit more complex so we go with hyperbolic secant
            float curve = MathUtils.Sech((t - (StrokeFrames / 2)) / StrokeFrames * 4);
            AddForwardForce(curve * scale * StrokeStrength, location);
        }
    }

    /// <summary>
    /// Get row force from input axes. TODO: joystick/polar rotation
    /// </summary>
    /// <returns>Converted forces to paddles.</returns>
    private (float left, float right) GetRowForceFromAxis(float forward, float rotation) {
        bool hasForward = !Mathf.Approximately(forward, 0);
        bool hasRotation = !Mathf.Approximately(rotation, 0);

        float left = 0;
        float right = 0;

        // this is a little stupid, because there's no great way for wasd to translate into
        // a proper oar push directly.
        if (hasForward) {
            left = forward;
            right = forward;

            if (hasRotation) {
                if (rotation < 0) {
                    left = 0;
                } else {
                    right = 0;
                }
            }
        } else if (hasRotation) {
            // if there's no forward movement, then cancel forward movement
            left = Mathf.Sign(rotation);
            right = -Mathf.Sign(rotation);
        }
        // ignore otherwise
        return (left, right);
    }

    private void AddForwardForce(float scale, GameObject location) {
        // "forward" is negative right because the imported model is rotated weird.
        rb.AddForceAtPosition(-transform.right * scale, location.transform.position, ForceMode.Force);
    }
}
