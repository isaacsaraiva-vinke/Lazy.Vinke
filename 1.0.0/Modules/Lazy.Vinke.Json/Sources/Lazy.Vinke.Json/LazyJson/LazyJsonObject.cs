// LazyJsonObject.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, September 23

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonObject : LazyJsonToken
    {
        #region Variables

        private List<LazyJsonProperty> propertyList;

        #endregion Variables

        #region Constructors

        public LazyJsonObject()
        {
            this.propertyList = new List<LazyJsonProperty>();
            this.Type = LazyJsonType.Object;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add a new property to the object
        /// </summary>
        /// <param name="jsonProperty">The json property</param>
        public void Add(LazyJsonProperty jsonProperty)
        {
            if (jsonProperty != null)
                this[jsonProperty.Name] = jsonProperty;
        }

        /// <summary>
        /// Remove a property from the object
        /// </summary>
        /// <param name="propertyName">The property name</param>
        public void Remove(String propertyName)
        {
            if (propertyName != null)
            {
                for (Int32 index = 0; index < this.propertyList.Count; index++)
                {
                    if (this.propertyList[index].Name == propertyName)
                    {
                        this.propertyList.RemoveAt(index);
                        break;
                    }
                }
            }
        }

        #endregion Methods

        #region Properties

        public Int32 Count { get { return this.propertyList.Count; } }

        #endregion Properties

        #region Indexers

        public LazyJsonProperty this[Int32 index]
        {
            get
            {
                if (index >= 0 && index < this.propertyList.Count)
                    return this.propertyList[index];

                return null;
            }
        }

        public LazyJsonProperty this[String propertyName]
        {
            get
            {
                if (propertyName != null)
                {
                    foreach (LazyJsonProperty jsonProperty in this.propertyList)
                    {
                        if (jsonProperty.Name == propertyName)
                            return jsonProperty;
                    }
                }

                return null;
            }
            set
            {
                if (propertyName != null)
                {
                    Int32 index = 0;

                    if (value == null || value.Name != propertyName)
                    {
                        /* Remove the property with "propertyName" */
                        for (index = 0; index < this.propertyList.Count; index++)
                        {
                            if (this.propertyList[index].Name == propertyName)
                            {
                                this.propertyList.RemoveAt(index);
                                break;
                            }
                        }
                    }

                    if (value != null)
                    {
                        /* Replace the property with "value.Name" */
                        for (index = 0; index < this.propertyList.Count; index++)
                        {
                            if (this.propertyList[index].Name == value.Name)
                            {
                                this.propertyList[index] = value;
                                break;
                            }
                        }

                        /* Add "value" property if not replaced before */
                        if (index == this.propertyList.Count)
                            this.propertyList.Add(value);
                    }
                }
            }
        }

        #endregion Indexers
    }
}
