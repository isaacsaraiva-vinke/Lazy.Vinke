// TestsLazyJsonProperty.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, September 24

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Tests.Json
{
    [TestClass]
    public class TestsLazyJsonProperty
    {
        [TestMethod]
        public void Constructor_WithParameter_Null_Success()
        {
            // Arrange

            // Act
            LazyJsonProperty jsonProperty = new LazyJsonProperty(null, null);

            // Assert
            Assert.AreEqual(jsonProperty.Name, LazyJsonProperty.UNNAMED_PROPERTY);
            Assert.AreEqual(jsonProperty.Token.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Constructor_WithParameter_Valued_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";

            // Act
            LazyJsonProperty jsonProperty = new LazyJsonProperty(propName, new LazyJsonString(propValue));

            // Assert
            Assert.AreEqual(jsonProperty.Name, propName);
            Assert.AreEqual(jsonProperty.Token.Type, LazyJsonType.String);
            Assert.AreEqual(((LazyJsonString)jsonProperty.Token).Value, propValue);
        }

        [TestMethod]
        public void Constructor_WithParameter_ValuedAllTypes_Success()
        {
            // Arrange

            // Act
            LazyJsonProperty jsonProperty1 = new LazyJsonProperty("Property1", new LazyJsonArray());
            LazyJsonProperty jsonProperty2 = new LazyJsonProperty("Property2", new LazyJsonBoolean(true));
            LazyJsonProperty jsonProperty3 = new LazyJsonProperty("Property3", new LazyJsonDecimal(Decimal.MinValue));
            LazyJsonProperty jsonProperty4 = new LazyJsonProperty("Property4", new LazyJsonInteger(Int64.MinValue));
            LazyJsonProperty jsonProperty5 = new LazyJsonProperty("Property5", new LazyJsonNull());
            LazyJsonProperty jsonProperty6 = new LazyJsonProperty("Property6", new LazyJsonObject());
            LazyJsonProperty jsonProperty7 = new LazyJsonProperty("Property7", new LazyJsonString("Lazy Vinke Tests Json"));

            // Assert
            Assert.AreEqual(jsonProperty1.Token.Type, LazyJsonType.Array);
            Assert.AreEqual(jsonProperty2.Token.Type, LazyJsonType.Boolean);
            Assert.AreEqual(jsonProperty3.Token.Type, LazyJsonType.Decimal);
            Assert.AreEqual(jsonProperty4.Token.Type, LazyJsonType.Integer);
            Assert.AreEqual(jsonProperty5.Token.Type, LazyJsonType.Null);
            Assert.AreEqual(jsonProperty6.Token.Type, LazyJsonType.Object);
            Assert.AreEqual(jsonProperty7.Token.Type, LazyJsonType.String);
        }

        [TestMethod]
        public void SetPropertyName_Null_Single_Success()
        {
            // Arrange
            String propName = "Solution";
            LazyJsonProperty jsonProperty = new LazyJsonProperty(propName, null);

            // Act
            jsonProperty.Name = null;

            // Assert
            Assert.AreEqual(jsonProperty.Name, LazyJsonProperty.UNNAMED_PROPERTY);
        }

        [TestMethod]
        public void SetPropertyName_Valued_Single_Success()
        {
            // Arrange
            String propName = "Solution";
            LazyJsonProperty jsonProperty = new LazyJsonProperty(null, null);

            // Act
            jsonProperty.Name = propName;

            // Assert
            Assert.AreEqual(jsonProperty.Name, propName);
        }

        [TestMethod]
        public void SetPropertyToken_Null_Single_Success()
        {
            // Arrange
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonProperty jsonProperty = new LazyJsonProperty(null, new LazyJsonString(propValue));

            // Act
            jsonProperty.Token = null;

            // Assert
            Assert.AreEqual(jsonProperty.Token.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void SetPropertyToken_Valued_Single_Success()
        {
            // Arrange
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonProperty jsonProperty = new LazyJsonProperty(null, null);

            // Act
            jsonProperty.Token = new LazyJsonString(propValue);

            // Assert
            Assert.AreEqual(jsonProperty.Token.Type, LazyJsonType.String);
            Assert.AreEqual(((LazyJsonString)jsonProperty.Token).Value, propValue);
        }
    }
}
