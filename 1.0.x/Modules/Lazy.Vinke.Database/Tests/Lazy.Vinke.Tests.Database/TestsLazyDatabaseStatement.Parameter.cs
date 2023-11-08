// TestsLazyDatabaseStatement.Parameter.cs
//
// This file is integrated part of "Lazy Vinke Tests Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 22

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;

namespace Lazy.Vinke.Tests.Database
{
    public partial class TestsLazyDatabaseStatement
    {
        [TestClass]
        public class Parameter
        {
            [TestMethod]
            public void Extract_Parameter_Null_Success()
            {
                // Arrange

                // Act
                String[] parameterArray1 = LazyDatabaseStatement.Parameter.Extract(null);
                String[] parameterArray2 = LazyDatabaseStatement.Parameter.Extract(" ");

                // Assert
                Assert.IsNull(parameterArray1);
                Assert.IsNull(parameterArray2);
            }

            [TestMethod]
            public void Extract_Parameter_OneWithoutQuotes_Success()
            {
                // Arrange

                // Act
                String[] parameterArray = LazyDatabaseStatement.Parameter.Extract("select * from sometable where id = @id");

                // Assert
                Assert.AreEqual(parameterArray.Length, 1);
                Assert.AreEqual(parameterArray[0], "id");
            }

            [TestMethod]
            public void Extract_Parameter_TwoWithoutQuotes_Success()
            {
                // Arrange

                // Act
                String[] parameterArray = LazyDatabaseStatement.Parameter.Extract("select * from sometable where id = @1 and code = @2");

                // Assert
                Assert.AreEqual(parameterArray.Length, 2);
                Assert.AreEqual(parameterArray[0], "1");
                Assert.AreEqual(parameterArray[1], "2");
            }

            [TestMethod]
            public void Extract_Parameter_FourWithQuotes_Success()
            {
                // Arrange

                // Act
                String[] parameterArray = LazyDatabaseStatement.Parameter.Extract("insert into sometable (id, code, name, desc) values (@id, @27, '@name', \"@desc\")");

                // Assert
                Assert.AreEqual(parameterArray.Length, 2);
                Assert.AreEqual(parameterArray[0], "id");
                Assert.AreEqual(parameterArray[1], "27");
            }

            [TestMethod]
            public void Extract_Parameter_OneInnerJoin_Success()
            {
                // Arrange

                // Act
                String[] parameterArray = LazyDatabaseStatement.Parameter.Extract("select * from tableA inner join tableB on tableA.id = @id and tableA.code = tableB.code");

                // Assert
                Assert.AreEqual(parameterArray.Length, 1);
                Assert.AreEqual(parameterArray[0], "id");
            }

            [TestMethod]
            public void Extract_Parameter_TwoSubQuery_Success()
            {
                // Arrange

                // Act
                String[] parameterArray = LazyDatabaseStatement.Parameter.Extract("select * from tableA where id in (select distinct id from tableB where code = @code and desc like '%'+@desc+'%')");

                // Assert
                Assert.AreEqual(parameterArray.Length, 2);
                Assert.AreEqual(parameterArray[0], "code");
                Assert.AreEqual(parameterArray[1], "desc");
            }

            [TestMethod]
            public void Extract_Parameter_TwoSubQueryOtherChar_Success()
            {
                // Arrange

                // Act
                String[] parameterArray = LazyDatabaseStatement.Parameter.Extract("select * from tableA where id in (select distinct id from tableB where code = :code and desc like '%'+:desc+'%')", ':');

                // Assert
                Assert.AreEqual(parameterArray.Length, 2);
                Assert.AreEqual(parameterArray[0], "code");
                Assert.AreEqual(parameterArray[1], "desc");
            }
        }
    }
}
