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

    private bool playerFocused = false;

    // Use this for initialization
    void Start() {
        cam = GetComponent<Camera>();
        targetDistance = maxPlayerDistance;
    }

    // Update is called once per frame
    void LateUpdate() {
        if (!playerFocused)
        {
            //player = player = GameObject.FindGameObjectWithTag("Player");
            playerFocused = true;
        }

        LerpZoom();
        lookModifier = new Vector2(Input.GetAxis("LookHorizontal") * 16f * lookDistance, -Input.GetAxis("LookVertical") * 16f * lookDistance);
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        if (interactionMode == true)
        {
            targetPos = playerPos + ((new Vector2(npcPos.x, playerPos.y) - playerPos) / 2) + new Vector2(0, 1.15f) + (new Vector2(0, Mathf.Abs(npcPos.y - playerPos.y) / 3f));
            targetZoom = 25 + (npcPos.y - playerPos.y) * 3;
        }
        else {
            targetPos = playerPos + lookModifier;
        }

        float distance = (pos - targetPos).magnitude;

        if (distance > targetDistance) {
            //move camera towards player by amount bigger than max dist
            Vector2 movement = (pos - targetPos).normalized * (targetDistance - distance);
            Vector3 movement3D = new Vector3(movement.x, movement.y, 0f);
            transform.position = Vector3.Lerp(transform.position + movement3D, transform.position, lerpWeight * Time.deltaTime * 150);

        }
    }

    private void LerpZoom() {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetZoom, zoomSpeed * Time.deltaTime * 40);
    }

    public void ZoomIn(Vector2 npcPos) {
        interactionMode = true;
        targetDistance = 0.1f;
        targetZoom = 25;
        this.npcPos = npcPos;
    }

    public void ZoomOut() {
        targetDistance = maxPlayerDistance;
        targetDistance = 1f;
        targetZoom = 65;
        interactionMode = false;
    }
}
