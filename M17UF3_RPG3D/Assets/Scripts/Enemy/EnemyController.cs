using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    const string PlayerTag = "Player";
    const float ReceiveDamage = 10;

    private HealthController _healthC;
    private MovementBehaviour _movementB;
    private VisionBehaviour _visionB;
    private StateSO _currentNode;
    public List<StateSO> Nodes;
    public GameObject Target;
    public bool OnRange = false, OnAttackRange = false;

    public HealthController HealthC { get => _healthC; set => _healthC = value; }
    public MovementBehaviour MovementB { get => _movementB; set => _movementB = value; }
    public VisionBehaviour VisionB { get => _visionB; set => _visionB = value; }

    void Start()
    {
        InitializeComponent();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            OnRange = _visionB.IsInVision(Target);
            CheckEndingConditions();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            OnRange = false;
            CheckEndingConditions();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            OnAttackRange = true;
            CheckEndingConditions();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        OnAttackRange = false;
        CheckEndingConditions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _healthC.TakeDamage(ReceiveDamage);
        }
        Debug.Log(_currentNode.name);
        _currentNode.OnStateUpdate(this);
    }

    private void InitializeComponent()
    {
        _healthC = GetComponent<HealthController>();
        _movementB = GetComponent<MovementBehaviour>();
        _visionB = GetComponent<VisionBehaviour>();

        _currentNode = Nodes[Nodes.Count - 1];
    }

    public void CheckEndingConditions()
    {
        foreach (ConditionSO condition in _currentNode.EndConditions)
            if (condition.CheckCondition(this) == condition.answer) ExitCurrentNode();
    }

    public void ExitCurrentNode()
    {
        foreach (StateSO stateSO in Nodes)
        {
            if (stateSO.StartCondition == null)
            {
                EnterNewState(stateSO);
                break;
            }
            else
            {
                if (stateSO.StartCondition.CheckCondition(this) == stateSO.StartCondition.answer)
                {
                    EnterNewState(stateSO);
                    break;
                }
            }
        }
        _currentNode.OnStateEnter(this);
    }

    private void EnterNewState(StateSO state)
    {
        _currentNode.OnStateExit(this);
        _currentNode = state;
        _currentNode.OnStateEnter(this);
    }
}
