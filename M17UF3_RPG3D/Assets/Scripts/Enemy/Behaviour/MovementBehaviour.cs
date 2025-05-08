using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementBehaviour : MonoBehaviour
{
    const float Double = 2f;

    public float Speed = 1f;
    public List<Transform> Waypoints;
    public float MinDistanceToWaypoint;
    public float WaitTime;

    private EnemyAnimationController _animationController;
    private NavMeshAgent _agent;
    private bool _isPatrolling = false;
    private Vector3[] _circlePoints;

    private void Awake()
    {
        _animationController = GetComponent<EnemyAnimationController>();
        _agent = GetComponent<NavMeshAgent>();
    }

    public void StartAgent()
    {
        _isPatrolling = false;
        _circlePoints = GetPatrolPoints(_agent.destination, 2f);

        _agent.speed = Speed;

        _animationController.ActiveWalk();
        _animationController.DeactiveChase();
    }

    public void StopAgent()
    {
        _agent.ResetPath();
        _animationController.DeactiveWalk();
        _animationController.DeactiveChase();
    }

    public void Patrol()
    {
        if (HasArrivedToWaypoint() && !_isPatrolling)
        {
            _animationController.DeactiveWalk();

            Transform destination = GetRandomWaypoint();
            StartCoroutine(WaitAndSetDestination(destination.position, WaitTime));
        }
    }

    public void PatrolASpecificPoint()
    {
        if (HasArrivedToWaypoint() && !_isPatrolling)
        {
            _animationController.DeactiveWalk();

            int randomIndex = Random.Range(0, _circlePoints.Length);
            Vector3 destination = _circlePoints[randomIndex];

            StartCoroutine(WaitAndSetDestination(destination, WaitTime/Double));
        }
    }
    private Vector3[] GetPatrolPoints(Vector3 center, float distance)
    {
        Vector3[] points = new Vector3[8];

        points[0] = center + new Vector3(0, 0, distance);           // Arriba
        points[1] = center + new Vector3(distance, 0, 0);           // Derecha
        points[2] = center + new Vector3(0, 0, -distance);          // Abajo
        points[3] = center + new Vector3(-distance, 0, 0);          // Izquierda
        points[4] = center + new Vector3(distance, 0, distance);    // Arriba-Derecha
        points[5] = center + new Vector3(-distance, 0, distance);   // Arriba-Izquierda
        points[6] = center + new Vector3(distance, 0, -distance);   // Abajo-Derecha
        points[7] = center + new Vector3(-distance, 0, -distance);  // Abajo-Izquierda

        return points;
    }

    private bool HasArrivedToWaypoint()
    {
        return _agent.remainingDistance < MinDistanceToWaypoint;
    }

    private IEnumerator WaitAndSetDestination(Vector3 destination, float time)
    {
        _isPatrolling = true;

        yield return new WaitForSeconds(time);

        _agent.SetDestination(destination);
        _animationController.ActiveWalk();

        _isPatrolling = false;
    }

    private Transform GetRandomWaypoint()
    {
        return Waypoints[Random.Range(0, Waypoints.Count)];
    }

    public void Run(Transform target)
    {
        Vector3 direction = (transform.position - target.position).normalized;
        direction.y = 0;

        _agent.SetDestination( transform.position + direction * 10f );

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * Speed * Double);
    }

    public void Chase(Transform target)
    {
        _animationController.ActiveChase();
        _agent.SetDestination(target.position);

        float chaseSpeed = Speed * Double;
        _agent.speed = _agent.speed == chaseSpeed ? _agent.speed : chaseSpeed;
    }
}
