using UnityEngine;

[ExecuteInEditMode]
public class FaceToCamera : MonoBehaviour
{

    [SerializeField]
    private Camera _mainCamera;
    private Transform _transform;
    
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.LookAt(_mainCamera.transform.position);
    }
}
