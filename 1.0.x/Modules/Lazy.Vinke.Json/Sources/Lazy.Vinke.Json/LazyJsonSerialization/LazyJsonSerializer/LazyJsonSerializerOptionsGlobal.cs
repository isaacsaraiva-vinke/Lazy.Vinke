// LazyJsonSerializerOptionsGlobal.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 09

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerOptionsGlobal : LazyJsonSerializerOptionsBase
    {
        #region Variables

        private Dictionary<Type, Type> jsonTypeSerializerDictionary;

        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptionsGlobal()
        {
            this.jsonTypeSerializerDictionary = new Dictionary<Type, Type>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add the json serializer to the desired type
        /// </summary>
        /// <typeparam name="TJsonSerializer">The json serializer</typeparam>
        /// <param name="type">The desired type</param>
        public void Add<TJsonSerializer>(Type type) where TJsonSerializer : LazyJsonSerializerBase
        {
            if (type != null && this.jsonTypeSerializerDictionary.ContainsKey(type) == false)
                this.jsonTypeSerializerDictionary.Add(type, typeof(TJsonSerializer));
        }

        /// <summary>
        /// Remove the json serializer from the desired type
        /// </summary>
        /// <param name="type">The desired type</param>
        public void Remove(Type type)
        {
            if (type != null && this.jsonTypeSerializerDictionary.ContainsKey(type) == true)
                this.jsonTypeSerializerDictionary.Remove(type);
        }

        /// <summary>
        /// Verify if the json serializer is assigned to the desired type
        /// </summary>
        /// <param name="type">The desired type</param>
        /// <returns></returns>
        public Boolean Contains(Type type)
        {
            return type != null && this.jsonTypeSerializerDictionary.ContainsKey(type);
        }

        /// <summary>
        /// Get the json serializer assigned to the desired type
        /// </summary>
        /// <param name="type">The desired type</param>
        /// <returns></returns>
        public Type Get(Type type)
        {
            if (type != null && this.jsonTypeSerializerDictionary.ContainsKey(type) == true)
                return this.jsonTypeSerializerDictionary[type];

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}
