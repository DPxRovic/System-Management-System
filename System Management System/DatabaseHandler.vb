' ==========================================
' FILENAME: /Data/DatabaseHandler.vb
' PURPOSE: Enhanced SQL execution wrapper with retry logic and error handling
' AUTHOR: System
' DATE: 2025-10-14
' LAST UPDATED: 2025-10-26 - Added retry logic and connection resilience
' Edited By Rovic
' For Future users please do not remove this header
' ==========================================

Imports MySql.Data.MySqlClient
Imports System.Data

Public Class DatabaseHandler

#Region "Configuration"

    ' Retry configuration
    Private Shared ReadOnly MaxRetries As Integer = 3
    Private Shared ReadOnly RetryDelayMs As Integer = 500
    Private Shared ReadOnly CommandTimeout As Integer = 30 ' seconds

    ' Connection pool settings (already configured in DatabaseManager)
    ' These are documented here for reference
    ' - MinPoolSize: 0
    ' - MaxPoolSize: 100
    ' - ConnectionTimeout: 30 seconds

#End Region

#Region "Core Execute Methods (ENHANCED - Preserves Original Signatures)"

    ''' <summary>
    ''' Executes a non-query SQL command (INSERT, UPDATE, DELETE) with retry logic
    ''' </summary>
    ''' <param name="query">SQL query to execute</param>
    ''' <param name="parameters">Dictionary of parameters</param>
    ''' <returns>Number of rows affected</returns>
    Public Shared Function ExecuteNonQuery(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As Integer
        Return ExecuteWithRetry(
            Function() ExecuteNonQueryInternal(query, parameters),
            "ExecuteNonQuery",
            query)
    End Function

    ''' <summary>
    ''' Internal implementation of ExecuteNonQuery
    ''' </summary>
    Private Shared Function ExecuteNonQueryInternal(query As String, parameters As Dictionary(Of String, Object)) As Integer
        Dim rowsAffected As Integer = 0
        Dim startTime As DateTime = DateTime.Now

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using cmd As New MySqlCommand(query, conn) With {.CommandTimeout = CommandTimeout}
                    ' Add parameters if provided
                    If parameters IsNot Nothing Then
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, If(param.Value, DBNull.Value))
                        Next
                    End If

                    rowsAffected = cmd.ExecuteNonQuery()
                End Using
            End Using

            ' Log performance if slow
            Dim duration As Long = CLng((DateTime.Now - startTime).TotalMilliseconds)
            Logger.LogPerformance("ExecuteNonQuery", duration, $"Rows affected: {rowsAffected}")

        Catch ex As Exception
            Logger.LogDatabaseError("ExecuteNonQuery", query, ex)
            Throw
        End Try

        Return rowsAffected
    End Function

    ''' <summary>
    ''' Executes a query and returns a DataTable with retry logic
    ''' </summary>
    ''' <param name="query">SQL query to execute</param>
    ''' <param name="parameters">Dictionary of parameters</param>
    ''' <returns>DataTable with results</returns>
    Public Shared Function ExecuteReader(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As DataTable
        Return ExecuteWithRetry(
            Function() ExecuteReaderInternal(query, parameters),
            "ExecuteReader",
            query)
    End Function

    ''' <summary>
    ''' Internal implementation of ExecuteReader
    ''' </summary>
    Private Shared Function ExecuteReaderInternal(query As String, parameters As Dictionary(Of String, Object)) As DataTable
        Dim dataTable As New DataTable()
        Dim startTime As DateTime = DateTime.Now

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using cmd As New MySqlCommand(query, conn) With {.CommandTimeout = CommandTimeout}
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

            ' Log performance if slow
            Dim duration As Long = CLng((DateTime.Now - startTime).TotalMilliseconds)
            Logger.LogPerformance("ExecuteReader", duration, $"Rows returned: {dataTable.Rows.Count}")

        Catch ex As Exception
            Logger.LogDatabaseError("ExecuteReader", query, ex)
            Throw
        End Try

        Return dataTable
    End Function

    ''' <summary>
    ''' Executes a query and returns a single value with retry logic
    ''' </summary>
    ''' <param name="query">SQL query to execute</param>
    ''' <param name="parameters">Dictionary of parameters</param>
    ''' <returns>Single scalar value</returns>
    Public Shared Function ExecuteScalar(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As Object
        Return ExecuteWithRetry(
            Function() ExecuteScalarInternal(query, parameters),
            "ExecuteScalar",
            query)
    End Function

    ''' <summary>
    ''' Internal implementation of ExecuteScalar
    ''' </summary>
    Private Shared Function ExecuteScalarInternal(query As String, parameters As Dictionary(Of String, Object)) As Object
        Dim result As Object = Nothing
        Dim startTime As DateTime = DateTime.Now

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using cmd As New MySqlCommand(query, conn) With {.CommandTimeout = CommandTimeout}
                    ' Add parameters if provided
                    If parameters IsNot Nothing Then
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, If(param.Value, DBNull.Value))
                        Next
                    End If

                    result = cmd.ExecuteScalar()
                End Using
            End Using

            ' Log performance if slow
            Dim duration As Long = CLng((DateTime.Now - startTime).TotalMilliseconds)
            Logger.LogPerformance("ExecuteScalar", duration, $"Result: {If(result IsNot Nothing, "Value returned", "NULL")}")

        Catch ex As Exception
            Logger.LogDatabaseError("ExecuteScalar", query, ex)
            Throw
        End Try

        Return result
    End Function

    ''' <summary>
    ''' Executes multiple queries in a transaction (ENHANCED - preserves original signature)
    ''' </summary>
    ''' <param name="queries">List of queries with their parameters</param>
    ''' <returns>True if all queries executed successfully</returns>
    Public Shared Function ExecuteTransaction(queries As List(Of Tuple(Of String, Dictionary(Of String, Object)))) As Boolean
        ' Transactions should not be retried automatically - they need manual intervention
        ' So we don't use ExecuteWithRetry here
        Dim startTime As DateTime = DateTime.Now

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        For Each queryData In queries
                            Using cmd As New MySqlCommand(queryData.Item1, conn, transaction) With {.CommandTimeout = CommandTimeout}
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

                        ' Log successful transaction
                        Dim duration As Long = CLng((DateTime.Now - startTime).TotalMilliseconds)
                        Logger.LogPerformance("ExecuteTransaction", duration, $"Queries executed: {queries.Count}")

                        Return True

                    Catch ex As Exception
                        Try
                            transaction.Rollback()
                            Logger.LogWarning($"Transaction rolled back due to error: {ex.Message}")
                        Catch rollbackEx As Exception
                            Logger.LogError("Transaction rollback failed", rollbackEx)
                        End Try

                        Logger.LogDatabaseError("ExecuteTransaction", $"{queries.Count} queries", ex)
                        Throw
                    End Try
                End Using
            End Using

        Catch ex As Exception
            Logger.LogError("ExecuteTransaction failed", ex)
            Return False
        End Try
    End Function

#End Region

#Region "Retry Logic"

    ''' <summary>
    ''' Executes a database operation with automatic retry on transient failures
    ''' </summary>
    ''' <typeparam name="T">Return type</typeparam>
    ''' <param name="operation">Operation to execute</param>
    ''' <param name="operationName">Name of operation for logging</param>
    ''' <param name="query">SQL query for logging</param>
    ''' <returns>Result of operation</returns>
    Private Shared Function ExecuteWithRetry(Of T)(operation As Func(Of T), operationName As String, query As String) As T
        Dim lastException As Exception = Nothing
        Dim attempt As Integer = 0

        While attempt < MaxRetries
            Try
                If attempt > 0 Then
                    ' Log retry attempt
                    Logger.LogInfo($"Retrying {operationName} (attempt {attempt + 1}/{MaxRetries})")
                End If

                Return operation()

            Catch ex As MySqlException When IsTransientError(ex) AndAlso attempt < MaxRetries - 1
                ' Transient error - can retry
                lastException = ex
                attempt += 1

                ' Exponential backoff
                Dim delay As Integer = RetryDelayMs * CInt(Math.Pow(2, attempt - 1))
                Logger.LogWarning($"Transient error in {operationName}, retrying in {delay}ms: {ex.Message}")
                Threading.Thread.Sleep(delay)

            Catch ex As Exception
                ' Non-transient error or final retry - throw immediately
                Logger.LogDatabaseError(operationName, query, ex)
                Throw
            End Try
        End While

        ' All retries exhausted
        Logger.LogError($"All {MaxRetries} retry attempts exhausted for {operationName}", lastException)
        Throw New Exception($"Database operation failed after {MaxRetries} attempts", lastException)
    End Function

    ''' <summary>
    ''' Determines if a MySQL exception represents a transient error that can be retried
    ''' </summary>
    ''' <param name="ex">MySQL exception</param>
    ''' <returns>True if error is transient and retryable</returns>
    Private Shared Function IsTransientError(ex As MySqlException) As Boolean
        ' MySQL error codes that typically indicate transient failures
        Select Case ex.Number
            Case 1040 ' Too many connections
                Return True
            Case 1205 ' Lock wait timeout
                Return True
            Case 1213 ' Deadlock found
                Return True
            Case 1614 ' Transaction branch was rolled back: deadlock
                Return True
            Case 2002 ' Can't connect to server
                Return True
            Case 2003 ' Can't connect to server on socket
                Return True
            Case 2006 ' Server has gone away
                Return True
            Case 2013 ' Lost connection during query
                Return True
            Case Else
                ' Check message for additional transient indicators
                Dim message As String = ex.Message.ToLower()
                Return message.Contains("timeout") OrElse
                       message.Contains("connection") OrElse
                       message.Contains("deadlock") OrElse
                       message.Contains("network")
        End Select
    End Function

#End Region

#Region "Helper Methods (ENHANCED - Preserves Original Signatures)"

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
                Using cmd As New MySqlCommand(query, conn) With {.CommandTimeout = CommandTimeout}
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

#End Region

#Region "NEW - Batch Operations"

    ''' <summary>
    ''' NEW: Executes batch insert for improved performance
    ''' </summary>
    ''' <param name="tableName">Table name</param>
    ''' <param name="columns">Column names</param>
    ''' <param name="rows">List of row data</param>
    ''' <returns>Number of rows inserted</returns>
    Public Shared Function ExecuteBatchInsert(tableName As String, columns As List(Of String), rows As List(Of Dictionary(Of String, Object))) As Integer
        If rows Is Nothing OrElse rows.Count = 0 Then
            Return 0
        End If

        Dim startTime As DateTime = DateTime.Now
        Dim totalInserted As Integer = 0

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                ' Use transaction for batch operations
                Using transaction As MySqlTransaction = conn.BeginTransaction()
                    Try
                        ' Build batch insert query
                        Dim columnList As String = String.Join(", ", columns.Select(Function(c) $"`{c}`"))
                        Dim baseQuery As String = $"INSERT INTO `{tableName}` ({columnList}) VALUES "

                        ' Process in batches of 100 to avoid query size limits
                        Const batchSize As Integer = 100
                        Dim currentBatch As Integer = 0

                        While currentBatch < rows.Count
                            Dim batchRows As List(Of Dictionary(Of String, Object)) = rows.Skip(currentBatch).Take(batchSize).ToList()

                            ' Build values clause
                            Dim valueClauses As New List(Of String)
                            Dim batchParams As New Dictionary(Of String, Object)

                            For i As Integer = 0 To batchRows.Count - 1
                                Dim paramNames As New List(Of String)
                                For Each col In columns
                                    Dim paramName As String = $"@p{i}_{col}"
                                    paramNames.Add(paramName)
                                    batchParams(paramName) = If(batchRows(i).ContainsKey(col), batchRows(i)(col), DBNull.Value)
                                Next
                                valueClauses.Add($"({String.Join(", ", paramNames)})")
                            Next

                            Dim query As String = baseQuery & String.Join(", ", valueClauses)

                            Using cmd As New MySqlCommand(query, conn, transaction) With {.CommandTimeout = CommandTimeout}
                                For Each param In batchParams
                                    cmd.Parameters.AddWithValue(param.Key, param.Value)
                                Next

                                totalInserted += cmd.ExecuteNonQuery()
                            End Using

                            currentBatch += batchSize
                        End While

                        transaction.Commit()

                        ' Log performance
                        Dim duration As Long = CLng((DateTime.Now - startTime).TotalMilliseconds)
                        Logger.LogPerformance("ExecuteBatchInsert", duration, $"Inserted {totalInserted} rows into {tableName}")

                    Catch ex As Exception
                        transaction.Rollback()
                        Logger.LogDatabaseError("ExecuteBatchInsert", $"Table: {tableName}", ex)
                        Throw
                    End Try
                End Using
            End Using

        Catch ex As Exception
            Logger.LogError($"Batch insert failed for table {tableName}", ex)
            Throw
        End Try

        Return totalInserted
    End Function

#End Region

#Region "NEW - Connection Health Check"

    ''' <summary>
    ''' NEW: Checks if database connection is healthy
    ''' </summary>
    ''' <returns>True if connection is healthy</returns>
    Public Shared Function CheckConnectionHealth() As Boolean
        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                Using cmd As New MySqlCommand("SELECT 1", conn) With {.CommandTimeout = 5}
                    Dim result As Object = cmd.ExecuteScalar()
                    Return result IsNot Nothing AndAlso Convert.ToInt32(result) = 1
                End Using
            End Using

        Catch ex As Exception
            Logger.LogWarning($"Connection health check failed: {ex.Message}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' NEW: Gets database connection statistics
    ''' </summary>
    ''' <returns>Dictionary with connection statistics</returns>
    Public Shared Function GetConnectionStatistics() As Dictionary(Of String, Object)
        Dim stats As New Dictionary(Of String, Object)

        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnection()
                conn.Open()

                ' Get connection pool statistics (if available)
                stats("State") = conn.State.ToString()
                stats("ServerVersion") = conn.ServerVersion
                stats("Database") = conn.Database
                stats("DataSource") = conn.DataSource

                ' Get server status variables
                Using cmd As New MySqlCommand("SHOW STATUS WHERE Variable_name IN ('Threads_connected', 'Max_used_connections', 'Aborted_connects')", conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            stats(reader.GetString(0)) = reader.GetString(1)
                        End While
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Logger.LogWarning($"Failed to get connection statistics: {ex.Message}")
            stats("Error") = ex.Message
        End Try

        Return stats
    End Function

#End Region

End Class