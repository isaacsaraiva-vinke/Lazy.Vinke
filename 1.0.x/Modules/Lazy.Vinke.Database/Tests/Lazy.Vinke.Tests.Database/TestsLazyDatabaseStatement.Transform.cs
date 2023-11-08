// TestsLazyDatabaseStatement.Transform.cs
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
        public class Transform
        {
            [TestMethod]
            public void Replace_SingleChar_Null_Success()
            {
                // Arrange

                // Act
                String sql1 = LazyDatabaseStatement.Transform.Replace(null, '@', ':');

                // Assert
                Assert.IsNull(sql1);
            }

            [TestMethod]
            public void Replace_SingleChar_NoMatch_Success()
            {
                // Arrange
                String sql = "select * from sometable where name = @name";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, ':', '@');

                // Assert
                Assert.AreEqual(sql, "select * from sometable where name = @name");
            }

            [TestMethod]
            public void Replace_SingleChar_OneMatch_Success()
            {
                // Arrange
                String sql = "select * from sometable where id = @id";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, '@', ':');

                // Assert
                Assert.AreEqual(sql, "select * from sometable where id = :id");
            }

            [TestMethod]
            public void Replace_SingleChar_TwoMatches_Success()
            {
                // Arrange
                String sql = "select * from sometable where code = @code or code in (select code from someothertable where code = @code) or code like %'@code'% ";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, '@', ':');

                // Assert
                Assert.AreEqual(sql, "select * from sometable where code = :code or code in (select code from someothertable where code = :code) or code like %'@code'% ");
            }

            [TestMethod]
            public void Replace_SingleValue_Null_Success()
            {
                // Arrange
                String sql = "select * from sometable where id = @id";

                // Act
                String sql1 = LazyDatabaseStatement.Transform.Replace(null, "someOldValue", "someNewValue");
                String sql2 = LazyDatabaseStatement.Transform.Replace("", "someOldValue", "someNewValue");
                String sql3 = LazyDatabaseStatement.Transform.Replace(sql, null, "someNewValue");

                // Assert
                Assert.IsNull(sql1);
                Assert.AreEqual(sql2, "");
                Assert.AreEqual(sql3, sql);
            }

            [TestMethod]
            public void Replace_SingleValue_NoMatch_Success()
            {
                // Arrange
                String sql = "select * from sometable where name = @name";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, "@nameS", ":name");

                // Assert
                Assert.AreEqual(sql, "select * from sometable where name = @name");
            }

            [TestMethod]
            public void Replace_SingleValue_OneMatch_Success()
            {
                // Arrange
                String sql = "select * from sometable where id = @id";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, "@id", ":id");

                // Assert
                Assert.AreEqual(sql, "select * from sometable where id = :id");
            }

            [TestMethod]
            public void Replace_SingleValue_OneMatchNull_Success()
            {
                // Arrange
                String sql = "select * from sometable where id = @id /*and*/";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, "/*and*/", null);

                // Assert
                Assert.AreEqual(sql, "select * from sometable where id = @id ");
            }

            [TestMethod]
            public void Replace_SingleValue_TwoMatches_Success()
            {
                // Arrange
                String sql = "select * from sometable where code = @code or code in (select code from someothertable where code = @code) or code like %'@code'% ";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, "@code", ":code");

                // Assert
                Assert.AreEqual(sql, "select * from sometable where code = :code or code in (select code from someothertable where code = :code) or code like %'@code'% ");
            }

            [TestMethod]
            public void Replace_SingleValue_ThreeMatches_Success()
            {
                // Arrange
                String sql = "select * from tableA inner join tableB on tableA.id = tableB.id and tableA.desc = @text where tableB.notes like '%'+@text+'%' and tableB.desc not in (@text)";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, "@text", ":text");

                // Assert
                Assert.AreEqual(sql, "select * from tableA inner join tableB on tableA.id = tableB.id and tableA.desc = :text where tableB.notes like '%'+:text+'%' and tableB.desc not in (:text)");
            }

            [TestMethod]
            public void Replace_MultipleValues_Null_Success()
            {
                // Arrange
                String sql = "select * from sometable where id = @id";

                // Act
                String sql1 = LazyDatabaseStatement.Transform.Replace(null, new String[] { "@id" }, new String[] { "@id" });
                String sql2 = LazyDatabaseStatement.Transform.Replace("", new String[] { "@id" }, new String[] { "@id" });
                String sql3 = LazyDatabaseStatement.Transform.Replace(sql, null, new String[] { "@id" });

                // Assert
                Assert.IsNull(sql1);
                Assert.AreEqual(sql2, "");
                Assert.AreEqual(sql3, sql);
            }

            [TestMethod]
            public void Replace_MultipleValues_NoMatch_Success()
            {
                // Arrange
                String sql = "select * from sometable where id = @id and name = @name";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, new String[] { "@idS", "@nameS" }, new String[] { ":id", ":name" });

                // Assert
                Assert.AreEqual(sql, "select * from sometable where id = @id and name = @name");
            }

            [TestMethod]
            public void Replace_MultipleValues_OneMatch_Success()
            {
                // Arrange
                String sql = "select * from sometable where id = @id and name = @name";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, new String[] { "@id", "@nameS" }, new String[] { ":id", ":name" });

                // Assert
                Assert.AreEqual(sql, "select * from sometable where id = :id and name = @name");
            }

            [TestMethod]
            public void Replace_MultipleValues_OneMatchNull_Success()
            {
                // Arrange
                String sql = "select * from sometable where id = @id and name = @name /*and*/";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, new String[] { "/*and*/" }, new String[] { null });

                // Assert
                Assert.AreEqual(sql, "select * from sometable where id = @id and name = @name ");
            }

            [TestMethod]
            public void Replace_MultipleValues_TwoMatches_Success()
            {
                // Arrange
                String sql = "select * from sometable where code = @code or name in (select name from someothertable where name = @name)";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, new String[] { "@code", "@name" }, new String[] { ":code", ":name" });

                // Assert
                Assert.AreEqual(sql, "select * from sometable where code = :code or name in (select name from someothertable where name = :name)");
            }

            [TestMethod]
            public void Replace_MultipleValues_ThreeMatchesNull_Success()
            {
                // Arrange
                String sql = "select * from sometable where code = @code or name in (select name from someothertable where name = @name) or name like %'@name'% /*and*/";

                // Act
                sql = LazyDatabaseStatement.Transform.Replace(sql, new String[] { "@code", "@name", "/*and*/" }, new String[] { ":code", ":name", null });

                // Assert
                Assert.AreEqual(sql, "select * from sometable where code = :code or name in (select name from someothertable where name = :name) or name like %'@name'% ");
            }
        }
    }
}
