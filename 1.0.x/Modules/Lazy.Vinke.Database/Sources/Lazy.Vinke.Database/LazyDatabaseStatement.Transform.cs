// LazyDatabaseStatement.Transform.cs
//
// This file is integrated part of "Lazy Vinke Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 22

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Lazy.Vinke.Database
{
    public partial class LazyDatabaseStatement
    {
        public static class Transform
        {
            #region Variables
            #endregion Variables

            #region Methods

            /// <summary>
            /// Replace the old value with the new value on the sql statement
            /// </summary>
            /// <param name="sql">The sql statement</param>
            /// <param name="oldValue">The old value</param>
            /// <param name="newValue">The new value</param>
            /// <returns>The sql statement with the new value</returns>
            public static String Replace(String sql, Char oldValue, Char newValue)
            {
                if (String.IsNullOrEmpty(sql) == false)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    for (int index = 0; index < sql.Length; index++)
                    {
                        if (sql[index] == '\'')
                        {
                            index++;
                            while (index < sql.Length && sql[index] != '\'')
                                index++;
                            continue;
                        }

                        if (sql[index] == '\"')
                        {
                            index++;
                            while (index < sql.Length && sql[index] != '\"')
                                index++;
                            continue;
                        }

                        if ((sql.Length - index) >= 1)
                        {
                            if (sql[index] == oldValue)
                            {
                                stringBuilder.Append(sql.Substring(0, index));
                                stringBuilder.Append(newValue);

                                sql = sql.Substring(index + 1, sql.Length - (index + 1));

                                index = 0;
                            }
                        }
                    }

                    stringBuilder.Append(sql);
                    return stringBuilder.ToString();
                }

                return sql;
            }

            /// <summary>
            /// Replace the old value with the new value on the sql statement
            /// </summary>
            /// <param name="sql">The sql statement</param>
            /// <param name="oldValue">The old value</param>
            /// <param name="newValue">The new value</param>
            /// <returns>The sql statement with the new value</returns>
            public static String Replace(String sql, String oldValue, String newValue)
            {
                if (String.IsNullOrEmpty(sql) == false && String.IsNullOrEmpty(oldValue) == false)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    for (int index = 0; index < sql.Length; index++)
                    {
                        if (sql[index] == '\'')
                        {
                            index++;
                            while (index < sql.Length && sql[index] != '\'')
                                index++;
                            continue;
                        }

                        if (sql[index] == '\"')
                        {
                            index++;
                            while (index < sql.Length && sql[index] != '\"')
                                index++;
                            continue;
                        }

                        if (oldValue.Length <= (sql.Length - index))
                        {
                            if (sql.Substring(index, oldValue.Length) == oldValue)
                            {
                                stringBuilder.Append(sql.Substring(0, index));
                                stringBuilder.Append(newValue != null ? newValue : String.Empty);

                                sql = sql.Substring(index + oldValue.Length, sql.Length - (index + oldValue.Length));

                                index = 0;
                            }
                        }
                    }

                    stringBuilder.Append(sql);
                    return stringBuilder.ToString();
                }

                return sql;
            }

            /// <summary>
            /// Replace the old values with the new values on the sql statement
            /// </summary>
            /// <param name="sql">The sql statement</param>
            /// <param name="oldValues">The old values collection</param>
            /// <param name="newValues">The new values collection</param>
            /// <returns>The sql statement with the new values</returns>
            public static String Replace(String sql, String[] oldValues, String[] newValues)
            {
                if (String.IsNullOrEmpty(sql) == false && oldValues != null && newValues != null && oldValues.Length == newValues.Length && newValues.Length > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    for (int index = 0; index < sql.Length; index++)
                    {
                        if (sql[index] == '\'')
                        {
                            index++;
                            while (index < sql.Length && sql[index] != '\'')
                                index++;
                            continue;
                        }

                        if (sql[index] == '\"')
                        {
                            index++;
                            while (index < sql.Length && sql[index] != '\"')
                                index++;
                            continue;
                        }

                        for (int valueIndex = 0; valueIndex < oldValues.Length; valueIndex++)
                        {
                            if (oldValues[valueIndex] != null && oldValues[valueIndex].Length <= (sql.Length - index))
                            {
                                if (sql.Substring(index, oldValues[valueIndex].Length) == oldValues[valueIndex])
                                {
                                    stringBuilder.Append(sql.Substring(0, index));
                                    stringBuilder.Append(newValues[valueIndex] != null ? newValues[valueIndex] : String.Empty);

                                    sql = sql.Substring(index + oldValues[valueIndex].Length, sql.Length - (index + oldValues[valueIndex].Length));

                                    index = 0;

                                    break;
                                }
                            }
                        }
                    }

                    stringBuilder.Append(sql);
                    return stringBuilder.ToString();
                }

                return sql;
            }

            #endregion Methods

            #region Properties
            #endregion Properties
        }
    }
}
