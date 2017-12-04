using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public float maxPlayerDistance = 10f;
    [Range(0f, 1f)]
    public float lerpWeight = 0.9f;
    [Range(0f, 1f)]
    public float zoomSpeed = 0.1f;
    public GameObject player;

    private float targetDistance;
    private Camera cam;
    private Vector2 targetPos;
    private float targetZoom = 65f;
    private Vector2 npcPos;
    private bool interactionMode = false;
    private Vector2 lookModifier;
    private float lookDistance = 0.4f;

    // Use this for initialization
    void Start() {
        cam = GetComponent<Camera>();
        targetDistance = maxPlayerDistance;
    }

    // Update is called once per frame
    void Update() {
        LerpZoom();
        lookModifier = new Vector2(Input.GetAxis("LookHorizontal") * 16f * lookDistance, -Input.GetAxis("LookVertical") * 16f * lookDistance);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        if (interactionMode == true)
        {
            targetPos = playerPos + (npcPos - playerPos) / 2 + new Vector2(0, 1.7f);
        }
        else {
            targetPos = playerPos + lookModifier;
        }

        float distance = (pos - targetPos).magnitude;

        if (distance > targetDistance) {
            //move camera towards player by amount bigger than max dist
            Vector2 movement = (pos - targetPos).normalized * (targetDistance - distance);
            Vector3 movement3D = new Vector3(movement.x, movement.y, 0f);
            transform.position = Vector3.Lerp(transform.position + movement3D, transform.position, lerpWeight);

        }
    }

    private void LerpZoom() {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, zoomSpeed);
    }

    public void ZoomIn(Vector2 npcPos) {
        interactionMode = true;
        targetDistance = 0.1f;
        targetZoom = 30;
        this.npcPos = npcPos;
    }

    public void ZoomOut() {
        targetDistance = maxPlayerDistance;
        targetZoom = 65;
        interactionMode = false;
    }
}
