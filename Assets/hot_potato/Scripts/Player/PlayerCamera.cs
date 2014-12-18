using UnityEngine;

public class PlayerCamera : BoltSingletonPrefab<PlayerCamera>
{
    public Transform cam;

    public new Camera camera
    {
        get { return cam.camera; }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //transform.position = new Vector3(0, 0, -20);
        cam.position = new Vector3(0, 0, -20);
    }

}