// LazyJsonDeserializerBase.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 07

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public abstract class LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Constructors
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Deserialize the json property to an object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="jsonProperty">The json property</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserialized object</returns>
        public T Deserialize<T>(LazyJsonProperty jsonProperty, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            return (T)Deserialize(jsonProperty, typeof(T), jsonDeserializerOptions);
        }

        /// <summary>
        /// Deserialize the json token to an object
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="jsonToken">The json token</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserialized object</returns>
        public T Deserialize<T>(LazyJsonToken jsonToken, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            return (T)Deserialize(jsonToken, typeof(T), jsonDeserializerOptions);
        }

        /// <summary>
        /// Deserialize the json property to an object
        /// </summary>
        /// <param name="jsonProperty">The json property</param>
        /// <param name="dataType">The type of the object</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserialized object</returns>
        public virtual Object Deserialize(LazyJsonProperty jsonProperty, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (jsonProperty != null)
                return Deserialize(jsonProperty.Token, dataType, jsonDeserializerOptions);

            return null;
        }

        /// <summary>
        /// Deserialize the json token to an object
        /// </summary>
        /// <param name="jsonToken">The json token</param>
        /// <param name="dataType">The type of the object</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserialized object</returns>
        public abstract Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null);

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}
