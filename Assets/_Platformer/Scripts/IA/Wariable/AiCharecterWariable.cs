using UnityEngine;
using MBT;
using System;
using UnityEngine.TextCore.Text;

public class AiCharecterWariable : Variable<AICharecter>
{
    protected override bool ValueEquals(AICharecter val1, AICharecter val2)
    {
        {
            return val1 == val2;
        }
    }
}
[Serializable]
public class AICharacterReference : VariableReference<AiCharecterWariable, AICharecter>
{
    public AICharacterReference(VarRefMode mode = VarRefMode.EnableConstant)
    {
        SetMode(mode);
    }

    public AICharacterReference(AICharecter defaultConstant)
    {
        useConstant = true;
        constantValue = defaultConstant;
    }

    public AICharecter Value
    {
        get
        {
            return (useConstant) ? constantValue : this.GetVariable().Value;
        }
        set
        {
            if (useConstant)
            {
                constantValue = value;
            }
            else
            {
                this.GetVariable().Value = value;
            }
        }
    }
}