// TestsLazyJsonObject.cs
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
    public class TestsLazyJsonObject
    {
        [TestMethod]
        public void Constructor_WithoutParameter_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonObject jsonObject = new LazyJsonObject();

            // Assert
            Assert.AreEqual(jsonObject.Type, LazyJsonType.Object);
            Assert.AreEqual(jsonObject.Count, 0);
        }

        [TestMethod]
        public void Add_Null_Single_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();

            // Act
            jsonObject.Add(null);

            // Assert
            Assert.AreEqual(jsonObject.Count, 0);
        }

        [TestMethod]
        public void Add_Valued_OneProperty_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonObject jsonObject = new LazyJsonObject();

            // Act
            jsonObject.Add(new LazyJsonProperty(propName, new LazyJsonString(propValue)));

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, propName);
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, propValue);
        }

        [TestMethod]
        public void Add_Valued_TwoProperties_Success()
        {
            // Arrange
            String prop1Name = "Solution";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();

            // Act
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));
            jsonObject.Add(new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value)));

            // Assert
            Assert.AreEqual(jsonObject.Count, 2);
            Assert.AreEqual(jsonObject[0].Name, prop1Name);
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, prop1Value);
            Assert.AreEqual(jsonObject[1].Name, prop2Name);
            Assert.AreEqual(((LazyJsonDecimal)jsonObject[1].Token).Value, prop2Value);
        }

        [TestMethod]
        public void Add_Valued_DuplicatedProperty_Success()
        {
            // Arrange
            String prop1Name = "AnyData";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();

            // Act
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));
            jsonObject.Add(new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value)));

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, prop2Name);
            Assert.AreEqual(((LazyJsonDecimal)jsonObject[0].Token).Value, prop2Value);
        }

        [TestMethod]
        public void Remove_Null_Single_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(propName, new LazyJsonString(propValue)));

            // Act
            jsonObject.Remove(null);

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, propName);
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, propValue);
        }

        [TestMethod]
        public void Remove_Valued_OneProperty_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(propName, new LazyJsonString(propValue)));

            // Act
            jsonObject.Remove(propName);

            // Assert
            Assert.AreEqual(jsonObject.Count, 0);
        }

        [TestMethod]
        public void Remove_Valued_TwoProperties_Success()
        {
            // Arrange
            String prop1Name = "Solution";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));
            jsonObject.Add(new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value)));

            // Act
            jsonObject.Remove(prop1Name);

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, prop2Name);
            Assert.AreEqual(((LazyJsonDecimal)jsonObject[0].Token).Value, prop2Value);
        }

        [TestMethod]
        public void Remove_Valued_UnkownProperty_Success()
        {
            // Arrange
            String prop1Name = "Solution";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));
            jsonObject.Add(new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value)));

            // Act
            jsonObject.Remove("Unknown");

            // Assert
            Assert.AreEqual(jsonObject.Count, 2);
            Assert.AreEqual(jsonObject[0].Name, prop1Name);
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, prop1Value);
            Assert.AreEqual(jsonObject[1].Name, prop2Name);
            Assert.AreEqual(((LazyJsonDecimal)jsonObject[1].Token).Value, prop2Value);
        }

        [TestMethod]
        public void GetIndexer_Index_Overflow_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(propName, new LazyJsonString(propValue)));

            // Act
            LazyJsonProperty jsonProperty1 = jsonObject[-1];
            LazyJsonProperty jsonProperty2 = jsonObject[1];

            // Assert
            Assert.IsNull(jsonProperty1);
            Assert.IsNull(jsonProperty2);
        }

        [TestMethod]
        public void GetIndexer_PropertyName_Null_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(propName, new LazyJsonString(propValue)));

            // Act
            LazyJsonProperty jsonProperty = jsonObject[null];

            // Assert
            Assert.IsNull(jsonProperty);
        }

        [TestMethod]
        public void GetIndexer_PropertyName_UnkownName_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(propName, new LazyJsonString(propValue)));

            // Act
            LazyJsonProperty jsonProperty = jsonObject["Unknown"];

            // Assert
            Assert.IsNull(jsonProperty);
        }

        [TestMethod]
        public void GetIndexer_PropertyName_ValidName_Success()
        {
            // Arrange
            String prop1Name = "Solution";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));
            jsonObject.Add(new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value)));

            // Act
            LazyJsonProperty jsonProperty1 = jsonObject[prop1Name];
            LazyJsonProperty jsonProperty2 = jsonObject[prop2Name];

            // Assert
            Assert.AreEqual(jsonProperty1.Name, prop1Name);
            Assert.AreEqual(((LazyJsonString)jsonProperty1.Token).Value, prop1Value);
            Assert.AreEqual(jsonProperty2.Name, prop2Name);
            Assert.AreEqual(((LazyJsonDecimal)jsonProperty2.Token).Value, prop2Value);
        }

        [TestMethod]
        public void SetIndexer_PropertyName_NullName_Success()
        {
            // Arrange
            String prop1Name = "Solution";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));

            // Act
            jsonObject[null] = new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value));

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, prop1Name);
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, prop1Value);
        }

        [TestMethod]
        public void SetIndexer_PropertyName_NullValue_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(propName, new LazyJsonString(propValue)));

            // Act
            jsonObject[propName] = null;

            // Assert
            Assert.AreEqual(jsonObject.Count, 0);
        }

        [TestMethod]
        public void SetIndexer_PropertyName_InvalidNameAndNullValue_Success()
        {
            // Arrange
            String propName = "Solution";
            String propValue = "Lazy Vinke Tests Json";
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(propName, new LazyJsonString(propValue)));

            // Act
            jsonObject["AnyData"] = null;

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, propName);
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, propValue);
        }

        [TestMethod]
        public void SetIndexer_PropertyName_ValidName_Success()
        {
            // Arrange
            String prop1Name = "Solution";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));

            // Act
            jsonObject[prop2Name] = new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value));

            // Assert
            Assert.AreEqual(jsonObject.Count, 2);
            Assert.AreEqual(jsonObject[0].Name, prop1Name);
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, prop1Value);
            Assert.AreEqual(jsonObject[1].Name, prop2Name);
            Assert.AreEqual(((LazyJsonDecimal)jsonObject[1].Token).Value, prop2Value);
        }

        [TestMethod]
        public void SetIndexer_PropertyName_ExistingName_Success()
        {
            // Arrange
            String prop1Name = "AnyData";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));

            // Act
            jsonObject[prop2Name] = new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value));

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, prop2Name);
            Assert.AreEqual(((LazyJsonDecimal)jsonObject[0].Token).Value, prop2Value);
        }

        [TestMethod]
        public void SetIndexer_PropertyName_ReplaceProperty_Success()
        {
            // Arrange
            String prop1Name = "Solution";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));

            // Act
            jsonObject[prop1Name] = new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value));

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, prop2Name);
            Assert.AreEqual(((LazyJsonDecimal)jsonObject[0].Token).Value, prop2Value);
        }

        [TestMethod]
        public void SetIndexer_PropertyName_ReplaceExistingProperty_Success()
        {
            // Arrange
            String prop1Name = "Solution";
            String prop1Value = "Lazy Vinke Tests Json";
            String prop2Name = "AnyData";
            Decimal prop2Value = Decimal.MaxValue;
            String prop3Name = "AnyData";
            Boolean prop3Value = true;
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty(prop1Name, new LazyJsonString(prop1Value)));
            jsonObject.Add(new LazyJsonProperty(prop2Name, new LazyJsonDecimal(prop2Value)));

            // Act
            jsonObject[prop1Name] = new LazyJsonProperty(prop3Name, new LazyJsonBoolean(prop3Value));

            // Assert
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, prop3Name);
            Assert.AreEqual(((LazyJsonBoolean)jsonObject[0].Token).Value, true);
        }
    }
}
