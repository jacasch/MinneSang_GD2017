using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSplatter : MonoBehaviour {

    ParticleSystem ps;
    public Color[] colors;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}

    public void Paint(Vector2 position, Color col)
    {
        //pick color and calculate gradient
        //Color paintCol = colors[(int)Random.RandomRange(0, colors.Length)];
        //ps.startColor = paintCol;

        transform.position = new Vector3(position.x, position.y, 0f);
        ps.startColor = col;
        ps.Emit(30);
    }
}
