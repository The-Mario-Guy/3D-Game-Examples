using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    private struct PointInSpace
    {
        public Vector3 Position;
        public float Time;
    }

    [SerializeField]
    [Tooltip("The transform to follow")]
    private Transform target;
    private Transform objective;

    [SerializeField]
    [Tooltip("The offset between the target and the camera")]
    private Vector3 offset;

    [Tooltip("The delay before the camera starts to follow the target")]
    [SerializeField]
    private float delay = 0.5f;

    [SerializeField]
    [Tooltip("The speed used in the lerp function when the camera follows the target")]
    private float speed = 5;

    ///<summary>
    /// Contains the positions of the target for the last X seconds
    ///</summary>
    private Queue<PointInSpace> pointsInSpace = new Queue<PointInSpace>();

    private void Start()
    {
        
    }

    void LateUpdate()
    {
        // Add the current target position to the list of positions
        pointsInSpace.Enqueue(new PointInSpace() { Position = target.position, Time = Time.time });

        // Move the camera to the position of the target X seconds ago 
        while (pointsInSpace.Count > 0 && pointsInSpace.Peek().Time <= Time.time - delay + Mathf.Epsilon)
        {
            transform.position = Vector3.Lerp(transform.position, pointsInSpace.Dequeue().Position + offset, Time.deltaTime * speed);
        }
    }
}