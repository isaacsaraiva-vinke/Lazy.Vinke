// LazyJsonSerializerOptions.cs
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
    public class LazyJsonSerializerOptions
    {
        #region Variables

        private Dictionary<Type, Object> serializerOptionsDictionary;

        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptions()
        {
            this.serializerOptionsDictionary = new Dictionary<Type, Object>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Retrieves the serializer options from the collection
        /// </summary>
        /// <typeparam name="T">The serializer options type</typeparam>
        /// <returns>The serializer options instance</returns>
        public T Item<T>() where T : LazyJsonSerializerOptionsBase
        {
            if (this.serializerOptionsDictionary.ContainsKey(typeof(T)) == false)
                this.serializerOptionsDictionary.Add(typeof(T), Activator.CreateInstance(typeof(T)));

            return (T)this.serializerOptionsDictionary[typeof(T)];
        }

        /// <summary>
        /// Retrieves the serializer options from the collection if contains
        /// </summary>
        /// <typeparam name="T">The serializer options type</typeparam>
        /// <returns>The serializer options instance</returns>
        public T ItemIfContains<T>() where T : LazyJsonSerializerOptionsBase
        {
            if (this.serializerOptionsDictionary.ContainsKey(typeof(T)) == true)
                return (T)this.serializerOptionsDictionary[typeof(T)];

            return null;
        }

        /// <summary>
        /// Verify if the serializer options is on the collection
        /// </summary>
        /// <typeparam name="T">The serializer options type</typeparam>
        /// <returns>The serializer options existence</returns>
        public Boolean Contains<T>() where T : LazyJsonSerializerOptionsBase
        {
            return this.serializerOptionsDictionary.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Retrieves the serializer options from the collection if contains or new instance if not
        /// </summary>
        /// <typeparam name="T">The serializer options type</typeparam>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The serializer options instance</returns>
        public static T CurrentOrNew<T>(LazyJsonSerializerOptions jsonSerializerOptions) where T : LazyJsonSerializerOptionsBase
        {
            if (jsonSerializerOptions != null)
            {
                T item = jsonSerializerOptions.ItemIfContains<T>();

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
