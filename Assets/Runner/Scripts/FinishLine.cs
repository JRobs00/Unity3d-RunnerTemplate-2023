using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// Ends the game on collision, forcing a win state.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class FinishLine : Spawnable
    {
        const string k_PlayerTag = "Player";
        
        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag(k_PlayerTag))
            {
                GameManager.Instance.Win();
            }
        }
    }
}