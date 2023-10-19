// LazyJsonDeserializerOptionsGlobal.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 10

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerOptionsGlobal : LazyJsonDeserializerOptionsBase
    {
        #region Variables

        private Dictionary<Type, Type> jsonTypeDeserializerDictionary;

        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptionsGlobal()
        {
            this.jsonTypeDeserializerDictionary = new Dictionary<Type, Type>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add the json deserializer to the desired type
        /// </summary>
        /// <typeparam name="TJsonDeserializer">The json deserializer</typeparam>
        /// <param name="type">The desired type</param>
        public void Add<TJsonDeserializer>(Type type) where TJsonDeserializer : LazyJsonDeserializerBase
        {
            if (type != null && this.jsonTypeDeserializerDictionary.ContainsKey(type) == false)
                this.jsonTypeDeserializerDictionary.Add(type, typeof(TJsonDeserializer));
        }

        /// <summary>
        /// Remove the json deserializer from the desired type
        /// </summary>
        /// <param name="type">The desired type</param>
        public void Remove(Type type)
        {
            if (type != null && this.jsonTypeDeserializerDictionary.ContainsKey(type) == true)
                this.jsonTypeDeserializerDictionary.Remove(type);
        }

        /// <summary>
        /// Verify if the json deserializer is assigned to the desired type
        /// </summary>
        /// <param name="type">The desired type</param>
        /// <returns></returns>
        public Boolean Contains(Type type)
        {
            return type != null && this.jsonTypeDeserializerDictionary.ContainsKey(type);
        }

        /// <summary>
        /// Get the json deserializer assigned to the desired type
        /// </summary>
        /// <param name="type">The desired type</param>
        /// <returns></returns>
        public Type Get(Type type)
        {
            if (type != null && this.jsonTypeDeserializerDictionary.ContainsKey(type) == true)
                return this.jsonTypeDeserializerDictionary[type];

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}
