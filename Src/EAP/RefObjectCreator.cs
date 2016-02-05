using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace EAP
{
    public class RefObjectCreator : MarshalByRefObject
    {
        static Dictionary<string, string> assembly = new Dictionary<string, string>();
        object sync = new object();

        /// <summary>
        /// 创建(远程)对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Create<T>()
        {
            Type type = typeof(T);
            if (type.IsInterface)
            {
                if (!assembly.ContainsKey(type.FullName))
                {
                    lock (sync)
                    {
                        if (!assembly.ContainsKey(type.FullName))
                        {
                            string setting = ConfigurationManager.AppSettings[type.FullName];
                            if (setting.IsNotEmpty())
                            {
                                string[] arr = setting.Split(',');
                                assembly.Add(type.FullName, setting);
                                return (T)Assembly.Load(arr[0]).CreateInstance(arr[1]);
                            }
                            string typeName = type.FullName.Remove(type.FullName.LastIndexOf(type.Name), 1);
                            string assembleName = type.Assembly.GetName().Name;
                            assembly.Add(type.FullName, assembleName + "," + typeName);
                            return (T)Assembly.Load(assembleName).CreateInstance(typeName);
                        }
                    }
                }
                string[] array = assembly[type.FullName].Split(',');
                return (T)Assembly.Load(array[0]).CreateInstance(array[1]);
            }
            else
                return (T)type.Assembly.CreateInstance(type.FullName);
        }
    }
}
