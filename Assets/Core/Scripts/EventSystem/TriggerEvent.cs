using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual.Core
{
    /// <summary>
    /// Raises an event on trigger collision
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerEvent : MonoBehaviour
    {
        const string k_PlayerTag = "Player";

        [SerializeField]
        AbstractGameEvent m_Event;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(k_PlayerTag))
            {
                if (m_Event != null)
                {
                    m_Event.Raise();
                }
            }
        }
    }
}
