using Mafi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoI.Mod.Better.Utilities
{
    public static class ReflectionUtility
    {
        public static IEnumerable<FieldInfo> GetAllFields(Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<FieldInfo>();
            }

            BindingFlags flags = BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;

            return type.GetFields(flags).Union(GetAllFields(type.BaseType));
        }

        public static IEnumerable<PropertyInfo> GetAllProperty(Type type)
        {
            if (type == null)
            {
                return Enumerable.Empty<PropertyInfo>();
            }

            BindingFlags flags = BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;

            return type.GetProperties(flags).Union(GetAllProperty(type.BaseType));
        }



        public static void PrintAllProperties<T>(DependencyResolver resolver) where T : class
        {
            try
            {
                if (resolver.TryGetResolvedDependency<T>(out T result))
                {
                    Debug.Log("BetterMod(V: " + BetterMod.MyVersion + "): " + typeof(T).Name);
                    foreach (PropertyInfo field in GetAllProperty(typeof(T)))
                    {
                        if (field == null || result == null)
                            continue;

                        Debug.Log(" - " + field.Name + ": " + field.GetValue(result).ToString());
                    }
                }
            }

#if DEBUG
            catch (Exception e)
            {
                Debug.LogException(e);
#else            
            catch (Exception)
            {
                Debug.Log("BetterMod(V: " + BetterMod.MyVersion + "): Class cannot reading. >> " + typeof(T).Name);
#endif
            }
        }
    }
}
