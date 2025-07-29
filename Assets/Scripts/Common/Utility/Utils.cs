using UnityEngine;

namespace Common
{
    public static class Utils
    {
        public static void Destroy( Object obj, float time = 0 )
        {
            if ( obj != null ) Object.Destroy( obj, time );
        }

        public static Object Instantiate(Object original)
        {
            return GameObject.Instantiate( original );
        }
        
        public static T Instantiate<T>(T original) where T : UnityEngine.Object
        {
            return GameObject.Instantiate( original );
        }
    }
}