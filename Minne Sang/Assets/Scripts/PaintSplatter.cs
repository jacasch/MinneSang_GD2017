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

    private void Update()
    {
        if (Input.GetButtonDown("Fire3")) {
            print("test");
            Paint(new Vector2(-10, -3));
        }
    }

    public void Paint(Vector2 position)
    {
        //pick color and calculate gradient
        //Color paintCol = colors[(int)Random.RandomRange(0, colors.Length)];
        //ps.startColor = paintCol;

        transform.position = new Vector3(position.x, position.y, 0f);
        ps.Emit(30);
    }
}
