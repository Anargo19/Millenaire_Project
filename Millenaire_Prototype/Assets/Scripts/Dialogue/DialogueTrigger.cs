using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] string action;
        [SerializeField] bool iscomplex;
        [SerializeField] ResourcesScriptable resource;
        [SerializeField] int nbResources;
        [SerializeField] UnityEvent onTrigger;
        [System.Serializable]
        public class MyEvent : UnityEvent<ResourcesScriptable, int> { }
        [SerializeField] public MyEvent TestEvent;

        public void Trigger(string actionToTrigger)
        {
            if (action == actionToTrigger)
            {
                if (!iscomplex)
                {
                    onTrigger.Invoke();
                }
                else
                {

                    TestEvent.Invoke(resource, nbResources);
                }
            }
        }

    }
}
