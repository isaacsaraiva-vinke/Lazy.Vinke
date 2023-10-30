// LazyDatabaseStatement.Parameter.cs
//
// This file is integrated part of "Lazy Vinke Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 22

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Database
{
    public partial class LazyDatabaseStatement
    {
        public static class Parameter
        {
            #region Consts

            public const char DefaultParameterChar = '@';

            #endregion Consts

            #region Variables
            #endregion Variables

            #region Methods

            /// <summary>
            /// Extract parameters from the sql statement
            /// </summary>
            /// <param name="sql">The sql statement</param>
            /// <param name="sqlParameterChar">The parameter character identifier</param>
            /// <returns>The extracted parameters</returns>
            public static String[] Extract(String sql, Char sqlParameterChar = DefaultParameterChar)
            {
                if (String.IsNullOrWhiteSpace(sql) == false)
                {
                    List<String> parameterList = new List<String>();

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

                        if (sql[index] == sqlParameterChar)
                        {
                            index++;
                            if (index < sql.Length && Char.IsLetterOrDigit(sql[index]) == true)
                            {
                                Int32 endIndex = index + 1;
                                while (endIndex < sql.Length && Char.IsLetterOrDigit(sql[endIndex]) == true)
                                    endIndex++;

                                String parameter = sql.Substring(index, endIndex - index);
                                if (parameterList.Contains(parameter) == false)
                                    parameterList.Add(parameter);

                                index = endIndex;
                            }
                        }
                    }

                    if (parameterList.Count > 0)
                        return parameterList.ToArray();
                }

                return null;
            }

            #endregion Methods

            #region Properties
            #endregion Properties
        }
    }
}
