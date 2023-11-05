using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTree : MonoBehaviour
{
    BTNode Root;

    Blackboard blackboard = new Blackboard();

    public Blackboard Blackboard { get { return blackboard; } }

    void Start()
    {
        ConstructTree(out Root);
    }

    protected abstract void ConstructTree(out BTNode root);


    void Update()
    {
        Root.UpdateNode();
    }
}
