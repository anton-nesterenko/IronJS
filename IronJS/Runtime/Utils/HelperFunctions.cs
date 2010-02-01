﻿using System;
using IronJS.Runtime.Js;
using System.Reflection;
using IronJS.Compiler.Ast;

namespace IronJS.Runtime.Utils
{
    public static class HelperFunctions
    {
        static public object PrintLine(object obj)
        {
            Console.WriteLine(JsTypeConverter.ToString(obj));
            return obj;
        }

        static public void Timer(IjsFunc func, IjsObj globals)
        {
            var start = DateTime.Now;
            func.MethodInfo.Invoke(null, new object[] { globals });
            var stop = DateTime.Now;
            var total = stop.Subtract(start);
            Console.WriteLine(total.TotalMilliseconds);
        }
    }
}