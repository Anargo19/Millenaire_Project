using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition
{
    [SerializeField] string predicator;
    [SerializeField] string[] parameters;

    public bool Check(IEnumerable<IPredicateEvaluator> evaluators)
    {
        foreach(var evaluator in evaluators)
        {
            bool? result = evaluator.Evaluate(predicator, parameters);

            if (result == null) continue;

            if (result == false) return false;
        }

        return true;
    }
}
