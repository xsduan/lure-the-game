// TODO: why does this work? Unity code is spectacularly unhelpful in explaining this magic
inline float2 randomVector (float2 UV, float offset) {
    float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
    UV = frac(sin(mul(UV, m)) * 46839.32);
    return float2(sin(UV.y * +offset) * 0.5 + 0.5, cos(UV.x * offset) * 0.5 + 0.5);
}

// for more info: https://en.wikipedia.org/wiki/Chebyshev_distance
inline float chebyshevDistance (float2 first, float2 second) {
    return max(abs(second.x - first.x), abs(second.y - first.y));
}

// taken from https://github.com/Unity-Technologies/ShaderGraph/blob/master/com.unity.shadergraph/Editor/Data/Nodes/Procedural/Noise/VoronoiNode.cs
// stupid CodeFunctionNode was privated :(
void ChebyshevNoise_float (float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells) {
    float2 g = floor(UV * CellDensity);
    float2 f = frac(UV * CellDensity);
    float t = 8.0;
    float3 res = float3(8.0, 0.0, 0.0);

    for(int y = -1; y <= 1; y++) {
        for(int x = -1; x <= 1; x++) {
            float2 lattice = float2(x, y);
            float2 offset = randomVector(lattice + g, AngleOffset);

            float d = chebyshevDistance(lattice + offset, f);
            if(d < res.x) {
                res = float3(d, offset.x, offset.y);
                Out = res.x;
                Cells = res.y;
            }
        }
    }
}