using System;
using System.Collections.Generic;
using System.Reflection;

namespace Common.Editor
{

    public static class Utils
    {
        public static object[] GetCtorArgs( ConstructorInfo ctor )
        {
            return ctor.GetParameters();
        }

        public static IEnumerable< Type > FindTypes( Func< Type, bool > predicate )
        {
            if ( predicate == null )
                throw new ArgumentNullException( nameof(predicate) );

            foreach ( var assembly in AppDomain.CurrentDomain.GetAssemblies() )
            {
                if ( !assembly.IsDynamic )
                {
                    Type[] exportedTypes = null;
                    try
                    {
                        exportedTypes = assembly.GetExportedTypes();
                    }
                    catch ( ReflectionTypeLoadException e )
                    {
                        exportedTypes = e.Types;
                    }

                    if ( exportedTypes != null )
                    {
                        foreach ( var type in exportedTypes )
                        {
                            if ( predicate( type ) )
                                yield return type;
                        }
                    }
                }
            }
        }
    }
}