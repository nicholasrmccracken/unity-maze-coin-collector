using UnityEngine;
using UnityEngine.InputSystem;

namespace ShareefSoftware
{
    public class MazeRotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 30f;

        private PlayerInput playerInput;
        private Vector3 mazeCenter;
        private bool isRotating = false;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();

            playerInput.actions["Rotate"].performed += _ => isRotating = !isRotating;
        }

        private void Start()
        {
            mazeCenter = GetMazeCenter();
        }

        private void Update()
        {
            if (!isRotating) return;

            transform.RotateAround(mazeCenter, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        private Vector3 GetMazeCenter()
        {
            Vector3 center = Vector3.zero;
            foreach (Transform child in transform)
            {
                center += child.position;
            }
            center /= transform.childCount;
            return center;
        }
    }
}
