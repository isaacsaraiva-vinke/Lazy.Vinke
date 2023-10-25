// LazyJsonSerializerTuple.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 19

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerTuple : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Constructors
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The json token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            if (data != null)
            {
                Type dataType = data.GetType();

                if (dataType.IsGenericType == true && dataType.IsAssignableTo(typeof(ITuple)) == true)
                {
                    LazyJsonArray jsonArray = new LazyJsonArray();

                    for (int index = 0; index < dataType.GenericTypeArguments.Length; index++)
                    {
                        /* Tuples<> uses Properties while ValueTuples<> uses Fields */
                        MemberInfo memberInfo = dataType.GetMembers().First(x => x.Name == "Item" + (index + 1));
                        MethodInfo methodInfoGetValue = memberInfo.GetType().GetMethods().First(x => x.Name == "GetValue" && x.GetParameters().Length == 1);

                        LazyJsonSerializerBase jsonSerializer = null;
                        LazyJsonSerializeTokenEventHandler jsonSerializeTokenEventHandler = null;
                        LazyJsonSerializer.SelectSerializeTokenEventHandler(dataType.GenericTypeArguments[index], out jsonSerializer, out jsonSerializeTokenEventHandler, jsonSerializerOptions);

                        jsonArray.Add(jsonSerializeTokenEventHandler(methodInfoGetValue.Invoke(memberInfo, new Object[] { data }), jsonSerializerOptions));
                    }

                    return jsonArray;
                }
            }

            return new LazyJsonNull();
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}
