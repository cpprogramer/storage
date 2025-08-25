using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common
{
    public static class Utils
    {
        public static void Destroy( Object obj, float time = 0 )
        {
            if ( obj != null ) Object.Destroy( obj, time );
        }

        public static Object Instantiate( Object original ) => GameObject.Instantiate( original );

        public static T Instantiate< T >( T original )
            where T : Object =>
            GameObject.Instantiate( original );

        public static Type[] GetDerivedTypes( Type baseType )
        {
            // Получаем текущую сборку
            var assembly = Assembly.GetExecutingAssembly();

            // Находим все типы, которые наследуются от baseType
            return assembly.GetTypes().Where( t => t != baseType && baseType.IsAssignableFrom( t ) ).ToArray();
        }
    }
}