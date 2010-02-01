﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS.Runtime.Js;

namespace IronJS.Compiler
{
    public static class IjsTypes
    {
        public static readonly Type String = typeof(string);
        public static readonly Type Integer = typeof(int);
        public static readonly Type Double = typeof(double);
        public static readonly Type Boolean = typeof(bool);
        public static readonly Type Null = typeof(object);
        public static readonly Type Undefined = typeof(Undefined);
        public static readonly Type Object = typeof(IjsObj);
        public static readonly Type Dynamic = typeof(object);
        public static readonly Type Action = typeof(Action);

        public static Type CreateFuncType(params Type[] types)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var systemCore = assemblies.First(x => x.FullName.StartsWith("System.Core"));

            if (types.Length == 0)
            {
                return systemCore.GetType("System.Action");
            }
            else
            {
                var type = systemCore.GetType("System.Func`" + types.Length);
                return type.MakeGenericType(types);
            }
        }
    }
}