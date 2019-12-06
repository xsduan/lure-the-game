using UnityEngine;

public static class MathUtils {
    /// <summary>
    /// Hyperbolic secant. This is equivalent to `f(x) = 2 / (e^x + e^-x)`.
    /// </summary>
    /// <param name="x">Input value.</param>
    /// <returns>sech(x)</returns>
    public static float Sech(float x) {
        float exp = Mathf.Exp(x);
        return 2 / (exp + (1 / exp));
    }
}
