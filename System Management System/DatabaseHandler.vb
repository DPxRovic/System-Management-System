' ==========================================
' FILENAME: /Data/DatabaseHandler.vb
' PURPOSE: Safe SQL execution wrapper with error handling
' AUTHOR: System
' DATE: 2025-10-14
' ==========================================

Imports MySql.Data.MySqlClient
Imports System.Data

Public Class DatabaseHandler

    ''' <summary>
    ''' Executes a non-query SQL command (INSERT, UPDATE, DELETE)
    ''' </summary>
    ''' <param name="query">SQL query to execute</param>
    ''' <param name="parameters">Dictionary of parameters</param>
    ''' <returns>Number of rows affected</returns>
    Public Shared Function ExecuteNonQuery(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As Integer
        Dim rowsAffected As Integer = 0

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    ' Add parameters if provided
                    If parameters IsNot Nothing Then
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, If(param.Value, DBNull.Value))
                        Next
                    End If

                    rowsAffected = cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"ExecuteNonQuery failed. Query: {query}", ex)
            Throw
        End Try

        Return rowsAffected
    End Function

    ''' <summary>
    ''' Executes a query and returns a DataTable
    ''' </summary>
    ''' <param name="query">SQL query to execute</param>
    ''' <param name="parameters">Dictionary of parameters</param>
    ''' <returns>DataTable with results</returns>
    Public Shared Function ExecuteReader(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As DataTable
        Dim dataTable As New DataTable()

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    ' Add parameters if provided
                    If parameters IsNot Nothing Then
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, If(param.Value, DBNull.Value))
                        Next
                    End If

                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dataTable)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"ExecuteReader failed. Query: {query}", ex)
            Throw
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Executes a query and returns a single value
    ''' </summary>
    ''' <param name="query">SQL query to execute</param>
    ''' <param name="parameters">Dictionary of parameters</param>
    ''' <returns>Single scalar value</returns>
    Public Shared Function ExecuteScalar(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As Object
        Dim result As Object = Nothing

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using cmd As New MySqlCommand(query, conn)
                    ' Add parameters if provided
                    If parameters IsNot Nothing Then
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, If(param.Value, DBNull.Value))
                        Next
                    End If

                    result = cmd.ExecuteScalar()
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"ExecuteScalar failed. Query: {query}", ex)
            Throw
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Executes multiple queries in a transaction
    ''' </summary>
    ''' <param name="queries">List of queries with their parameters</param>
    ''' <returns>True if all queries executed successfully</returns>
    Public Shared Function ExecuteTransaction(queries As List(Of Tuple(Of String, Dictionary(Of String, Object)))) As Boolean
        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        For Each queryData In queries
                            Using cmd As New MySqlCommand(queryData.Item1, conn, transaction)
                                ' Add parameters if provided
                                If queryData.Item2 IsNot Nothing Then
                                    For Each param In queryData.Item2
                                        cmd.Parameters.AddWithValue(param.Key, If(param.Value, DBNull.Value))
                                    Next
                                End If

                                cmd.ExecuteNonQuery()
                            End Using
                        Next

                        transaction.Commit()
                        Return True
                    Catch ex As Exception
                        transaction.Rollback()
                        Logger.LogError("Transaction failed and was rolled back", ex)
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError("ExecuteTransaction failed", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if a table exists in the database
    ''' </summary>
    ''' <param name="tableName">Name of the table</param>
    ''' <returns>True if table exists</returns>
    Public Shared Function TableExists(tableName As String) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = @database AND table_name = @tableName"
            Dim params As New Dictionary(Of String, Object) From {
                {"@database", DatabaseManager.DatabaseName},
                {"@tableName", tableName}
            }

            Dim result As Object = ExecuteScalar(query, params)
            Return Convert.ToInt32(result) > 0
        Catch ex As Exception
            Logger.LogError($"Error checking if table {tableName} exists", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if database exists
    ''' </summary>
    ''' <param name="databaseName">Name of the database</param>
    ''' <returns>True if database exists</returns>
    Public Shared Function DatabaseExists(databaseName As String) As Boolean
        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnectionWithoutDatabase()
                conn.Open()

                Dim query As String = "SELECT COUNT(*) FROM information_schema.schemata WHERE schema_name = @database"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@database", databaseName)
                    Dim result As Object = cmd.ExecuteScalar()
                    Return Convert.ToInt32(result) > 0
                End Using
            End Using
        Catch ex As Exception
            Logger.LogError($"Error checking if database {databaseName} exists", ex)
            Return False
        End Try
    End Function

End Class