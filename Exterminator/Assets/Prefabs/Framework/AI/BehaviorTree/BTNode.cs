
public enum NodeResult
{
    Success,
    Failure,
    InProgress
}

public abstract class BTNode
{

    public NodeResult UpdateNode()
    {
        // one off thing
        if (!started)
        {
            started = true;
            NodeResult executeResult = Execute();
            if (executeResult != NodeResult.InProgress)
            {
                EndNode();
                return executeResult;
            }
        }
        //time based
        NodeResult updateResult = Update();
        if (updateResult != NodeResult.InProgress)
        {
            EndNode();
        }
        return updateResult;
    }

    //override in child
    protected virtual NodeResult Execute()
    {
        // one off thing

        return NodeResult.Success;
    }

    protected virtual NodeResult Update()
    {
        //time based
        return NodeResult.Success;
    }

    protected virtual void End()
    {
        // clean up
    }

    private void EndNode()
    {
        started = false;
        End();
    }


    bool started = false;
}
