using UnityEngine;

namespace Utils
{
    public class DestroyByTime : MonoBehaviour
    {
        public float TimeToDestroy;

        private void Start()
        {
            Destroy(gameObject, TimeToDestroy);
        }
    }
}

