using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagelFactory.Systems.Collisions.Interactions
{
    public class VanishInteraction : MonoBehaviour
    {
        public void Vanish()
        {
            Destroy(gameObject);
        }
    }
}
