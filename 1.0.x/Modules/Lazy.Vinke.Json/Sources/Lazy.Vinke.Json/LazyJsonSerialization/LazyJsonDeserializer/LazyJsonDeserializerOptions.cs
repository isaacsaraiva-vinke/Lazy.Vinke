// LazyJsonDeserializerOptions.cs
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
    public class LazyJsonDeserializerOptions
    {
        #region Variables

        private Dictionary<Type, Object> deserializerOptionsDictionary;

        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptions()
        {
            this.deserializerOptionsDictionary = new Dictionary<Type, Object>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Retrieves the deserializer options from the collection
        /// </summary>
        /// <typeparam name="T">The deserializer options type</typeparam>
        /// <returns>The deserializer options instance</returns>
        public T Item<T>() where T : LazyJsonDeserializerOptionsBase
        {
            if (this.deserializerOptionsDictionary.ContainsKey(typeof(T)) == false)
                this.deserializerOptionsDictionary.Add(typeof(T), Activator.CreateInstance(typeof(T)));

            return (T)this.deserializerOptionsDictionary[typeof(T)];
        }

        /// <summary>
        /// Retrieves the deserializer options from the collection if contains
        /// </summary>
        /// <typeparam name="T">The deserializer options type</typeparam>
        /// <returns>The deserializer options instance</returns>
        public T ItemIfContains<T>() where T : LazyJsonDeserializerOptionsBase
        {
            if (this.deserializerOptionsDictionary.ContainsKey(typeof(T)) == true)
                return (T)this.deserializerOptionsDictionary[typeof(T)];

            return null;
        }

        /// <summary>
        /// Verify if the deserializer options is on the collection
        /// </summary>
        /// <typeparam name="T">The deserializer options type</typeparam>
        /// <returns>The deserializer options existence</returns>
        public Boolean Contains<T>() where T : LazyJsonDeserializerOptionsBase
        {
            return this.deserializerOptionsDictionary.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Retrieves the deserializer options from the collection if contains or new instance if not
        /// </summary>
        /// <typeparam name="T">The deserializer options type</typeparam>
        /// <param name="jsonSerializerOptions">The json deserializer options</param>
        /// <returns>The deserializer options instance</returns>
        public static T CurrentOrNew<T>(LazyJsonDeserializerOptions jsonDeserializerOptions) where T : LazyJsonDeserializerOptionsBase
        {
            if (jsonDeserializerOptions != null)
            {
                T item = jsonDeserializerOptions.ItemIfContains<T>();

                if (item != null)
                    return item;
            }

            return (T)Activator.CreateInstance(typeof(T));
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}
