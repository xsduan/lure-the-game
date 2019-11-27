using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator 
{

    //TODO: create methods for each wave generation method so we can have a varied sea for different regions.
	public static float SinXWave(
		Vector3 position, 
		float speed, 
		float scale,
		float waveDistance,
		float noiseStrength, 
		float noiseWalk,
        float timeSinceStart) 
	{
        float x = position.x;
        float y = 0f;
        float z = position.z;

        //Using only x or z will produce straight waves
		//Using only y will produce an up/down movement
		//x + y + z rolling waves
		//x * z produces a moving sea without rolling waves

		float waveType = 1;

        y += Mathf.Sin((timeSinceStart * speed + waveType) / waveDistance) * scale;

        //Add noise to make it more realistic
        y += Mathf.PerlinNoise(x + noiseWalk, y + Mathf.Sin(timeSinceStart * 0.1f)) * noiseStrength;

        return y;
	}
}	