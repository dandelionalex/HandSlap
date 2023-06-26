using UnityEngine;

namespace Utils
{
    public class DestroyInvisible : MonoBehaviour
    {
        private Transform cachedTransform;

        void Start()
        {
            cachedTransform = transform;
        }
    
        void Update()
        {
            if (cachedTransform.position.x < -1) //TODO remove all object in one place
            {
                Destroy(gameObject);
            }
        }
    }
}