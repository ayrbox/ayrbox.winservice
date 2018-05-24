using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ayrbox.winservice.Utils {
    public class TypeFinder {

        public static IEnumerable<T> FindObjectOfType<T>(params object[] args) where T : class, IComparable<T> {

            List<T> objects = new List<T>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T)))) {
                objects.Add((T)Activator.CreateInstance(type, args));
            }
            objects.Sort();
            return objects;
        }
    }
}
