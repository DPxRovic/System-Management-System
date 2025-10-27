' ==========================================
' FILENAME: /Data/DatabaseManager.vb
' PURPOSE: Enhanced database connection configuration and management with health monitoring
' AUTHOR: System
' DATE: 2025-10-14
' LAST UPDATED: 2025-10-26 - Added connection health monitoring and validation
' Edited By Rovic
' For Future users please do not remove this header
' ==========================================

Imports MySql.Data.MySqlClient
Imports System.Configuration

Public Class DatabaseManager
    ' Database connection configuration (PRESERVED - Original values)
    Private Shared _server As String = "localhost"
    Private Shared _port As String = "3306"
    Private Shared _database As String = "student_management_db"
    Private Shared _username As String = "root"
    Private Shared _password As String = ""

    ' Connection string builder
    Private Shared _connectionString As String = ""

    ' NEW: Connection health tracking
    Private Shared _lastConnectionCheck As DateTime = DateTime.MinValue
    Private Shared _connectionHealthy As Boolean = True
    Private Shared ReadOnly _healthCheckInterval As TimeSpan = TimeSpan.FromMinutes(5)

    ' NEW: Connection statistics
    Private Shared _connectionAttempts As Integer = 0
    Private Shared _connectionFailures As Integer = 0
    Private Shared _lastFailureTime As DateTime = DateTime.MinValue

    ''' <summary>
    ''' Gets the connection string for the database (ENHANCED - Preserves original signature)
    ''' </summary>
    Public Shared ReadOnly Property ConnectionString As String
        Get
            If String.IsNullOrEmpty(_connectionString) Then
                BuildConnectionString()
            End If
            Return _connectionString
        End Get
    End Property

    ''' <summary>
    ''' Gets the connection string without database (for initial DB creation) (ENHANCED - Preserves original signature)
    ''' </summary>
    Public Shared ReadOnly Property ConnectionStringWithoutDatabase As String
        Get
            Dim builder As New MySqlConnectionStringBuilder()
            builder.Server = _server
            builder.Port = Convert.ToUInt32(_port)
            builder.UserID = _username
            builder.Password = _password
            builder.CharacterSet = "utf8mb4"
            builder.SslMode = MySqlSslMode.None
            builder.AllowUserVariables = True
            Return builder.ToString()
        End Get
    End Property

    ''' <summary>
    ''' Builds the connection string with all parameters (ENHANCED - Preserves original signature)
    ''' </summary>
    Private Shared Sub BuildConnectionString()
        Try
            Dim builder As New MySqlConnectionStringBuilder()
            builder.Server = _server
            builder.Port = Convert.ToUInt32(_port)
            builder.Database = _database
            builder.UserID = _username
            builder.Password = _password
            builder.CharacterSet = "utf8mb4"
            builder.SslMode = MySqlSslMode.None
            builder.AllowUserVariables = True
            builder.Pooling = True
            builder.MinimumPoolSize = 0
            builder.MaximumPoolSize = 100
            builder.ConnectionTimeout = 30

            ' NEW: Enhanced connection parameters for resilience
            builder.ConnectionReset = False ' Reuse connections without reset for better performance
            builder.DefaultCommandTimeout = 30
            builder.Keepalive = 60 ' Keep connection alive with pings every 60 seconds

            _connectionString = builder.ToString()

            Logger.LogInfo("Database connection string built successfully")

        Catch ex As Exception
            Logger.LogError("Error building connection string", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Tests the database connection (ENHANCED - Preserves original signature)
    ''' </summary>
    ''' <returns>True if connection successful, False otherwise</returns>
    Public Shared Function TestConnection() As Boolean
        _connectionAttempts += 1

        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()

                ' NEW: Verify connection with a simple query
                Using cmd As New MySqlCommand("SELECT 1", conn)
                    cmd.CommandTimeout = 5 ' Quick timeout for test
                    Dim result As Object = cmd.ExecuteScalar()

                    If result IsNot Nothing AndAlso Convert.ToInt32(result) = 1 Then
                        _connectionHealthy = True
                        _lastConnectionCheck = DateTime.Now
                        Logger.LogInfo("Database connection test successful")
                        Return True
                    End If
                End Using
            End Using

            _connectionFailures += 1
            _lastFailureTime = DateTime.Now
            _connectionHealthy = False
            Logger.LogWarning("Database connection test failed - query returned invalid result")
            Return False

        Catch ex As Exception
            _connectionFailures += 1
            _lastFailureTime = DateTime.Now
            _connectionHealthy = False
            Logger.LogDatabaseError("TestConnection", "SELECT 1", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Creates a new database connection (ENHANCED - Preserves original signature)
    ''' </summary>
    Public Shared Function GetConnection() As MySqlConnection
        Try
            ' NEW: Periodic health check
            If (DateTime.Now - _lastConnectionCheck) > _healthCheckInterval Then
                PerformHealthCheck()
            End If

            Dim conn As New MySqlConnection(ConnectionString)

            ' NEW: Add connection event handlers for monitoring
            AddHandler conn.StateChange, AddressOf OnConnectionStateChange

            Return conn

        Catch ex As Exception
            _connectionFailures += 1
            _lastFailureTime = DateTime.Now
            Logger.LogError("Error creating database connection", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Creates a connection without specifying database (for initial setup) (ENHANCED - Preserves original signature)
    ''' </summary>
    Public Shared Function GetConnectionWithoutDatabase() As MySqlConnection
        Try
            Dim conn As New MySqlConnection(ConnectionStringWithoutDatabase)

            ' NEW: Add connection event handlers for monitoring
            AddHandler conn.StateChange, AddressOf OnConnectionStateChange

            Return conn

        Catch ex As Exception
            _connectionFailures += 1
            _lastFailureTime = DateTime.Now
            Logger.LogError("Error creating database connection without database", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Updates database configuration (ENHANCED - Preserves original signature)
    ''' </summary>
    Public Shared Sub UpdateConfiguration(server As String, port As String, database As String, username As String, password As String)
        Try
            _server = server
            _port = port
            _database = database
            _username = username
            _password = password
            _connectionString = ""

            BuildConnectionString()

            ' NEW: Test new configuration
            If Not TestConnection() Then
                Logger.LogWarning("Updated database configuration failed connection test")
            Else
                Logger.LogInfo($"Database configuration updated successfully: {server}:{port}/{database}")
            End If

        Catch ex As Exception
            Logger.LogError("Error updating database configuration", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Gets the database name (ENHANCED - Preserves original signature)
    ''' </summary>
    Public Shared ReadOnly Property DatabaseName As String
        Get
            Return _database
        End Get
    End Property

#Region "NEW - Connection Health Monitoring"

    ''' <summary>
    ''' NEW: Performs periodic health check on database connection
    ''' </summary>
    Private Shared Sub PerformHealthCheck()
        Try
            _connectionHealthy = TestConnection()
            _lastConnectionCheck = DateTime.Now

            If Not _connectionHealthy Then
                Logger.LogWarning("Periodic health check failed - database connection unhealthy")
            End If

        Catch ex As Exception
            _connectionHealthy = False
            Logger.LogWarning($"Health check error: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Gets current connection health status
    ''' </summary>
    ''' <returns>True if connection is healthy</returns>
    Public Shared Function IsConnectionHealthy() As Boolean
        ' If health check is stale, perform new check
        If (DateTime.Now - _lastConnectionCheck) > _healthCheckInterval Then
            PerformHealthCheck()
        End If

        Return _connectionHealthy
    End Function

    ''' <summary>
    ''' NEW: Gets connection statistics
    ''' </summary>
    ''' <returns>Dictionary with connection statistics</returns>
    Public Shared Function GetConnectionStats() As Dictionary(Of String, Object)
        Dim stats As New Dictionary(Of String, Object) From {
            {"TotalAttempts", _connectionAttempts},
            {"TotalFailures", _connectionFailures},
            {"LastFailureTime", _lastFailureTime},
            {"IsHealthy", _connectionHealthy},
            {"LastHealthCheck", _lastConnectionCheck},
            {"SuccessRate", If(_connectionAttempts > 0, CDbl((_connectionAttempts - _connectionFailures) / _connectionAttempts * 100), 100.0)},
            {"Server", _server},
            {"Port", _port},
            {"Database", _database}
        }

        Return stats
    End Function

    ''' <summary>
    ''' NEW: Resets connection statistics
    ''' </summary>
    Public Shared Sub ResetConnectionStats()
        _connectionAttempts = 0
        _connectionFailures = 0
        _lastFailureTime = DateTime.MinValue
        Logger.LogInfo("Connection statistics reset")
    End Sub

    ''' <summary>
    ''' NEW: Event handler for connection state changes
    ''' </summary>
    Private Shared Sub OnConnectionStateChange(sender As Object, e As StateChangeEventArgs)
        Try
            If e.CurrentState = ConnectionState.Open Then
                Logger.Log(Logger.LogLevel.Debug, $"Connection opened", "Database")
            ElseIf e.CurrentState = ConnectionState.Closed AndAlso e.OriginalState = ConnectionState.Open Then
                Logger.Log(Logger.LogLevel.Debug, $"Connection closed", "Database")
            ElseIf e.CurrentState = ConnectionState.Broken Then
                Logger.LogWarning("Connection broken detected")
                _connectionHealthy = False
            End If

        Catch ex As Exception
            ' Don't let event handler errors propagate
            Logger.LogWarning($"Error in connection state change handler: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "NEW - Connection Validation"

    ''' <summary>
    ''' NEW: Validates connection string format
    ''' </summary>
    ''' <returns>True if connection string is valid</returns>
    Public Shared Function ValidateConnectionString() As Boolean
        Try
            Dim builder As New MySqlConnectionStringBuilder(ConnectionString)

            ' Check required properties
            If String.IsNullOrEmpty(builder.Server) Then
                Logger.LogWarning("Connection string validation failed: Server not specified")
                Return False
            End If

            If String.IsNullOrEmpty(builder.Database) Then
                Logger.LogWarning("Connection string validation failed: Database not specified")
                Return False
            End If

            If String.IsNullOrEmpty(builder.UserID) Then
                Logger.LogWarning("Connection string validation failed: UserID not specified")
                Return False
            End If

            Logger.LogInfo("Connection string validation passed")
            Return True

        Catch ex As Exception
            Logger.LogError("Connection string validation error", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' NEW: Validates database configuration parameters
    ''' </summary>
    ''' <param name="server">Server address</param>
    ''' <param name="port">Port number</param>
    ''' <param name="database">Database name</param>
    ''' <param name="username">Username</param>
    ''' <returns>Validation result with error message if invalid</returns>
    Public Shared Function ValidateConfiguration(server As String, port As String, database As String, username As String) As Tuple(Of Boolean, String)
        Try
            ' Validate server
            If String.IsNullOrWhiteSpace(server) Then
                Return Tuple.Create(False, "Server address is required")
            End If

            ' Validate port
            Dim portNumber As Integer
            If Not Integer.TryParse(port, portNumber) OrElse portNumber < 1 OrElse portNumber > 65535 Then
                Return Tuple.Create(False, "Port must be a number between 1 and 65535")
            End If

            ' Validate database name
            If String.IsNullOrWhiteSpace(database) Then
                Return Tuple.Create(False, "Database name is required")
            End If

            ' Check for invalid characters in database name
            If database.Contains(" ") OrElse database.Contains(";") OrElse database.Contains("'") Then
                Return Tuple.Create(False, "Database name contains invalid characters")
            End If

            ' Validate username
            If String.IsNullOrWhiteSpace(username) Then
                Return Tuple.Create(False, "Username is required")
            End If

            Return Tuple.Create(True, "Configuration is valid")

        Catch ex As Exception
            Logger.LogError("Configuration validation error", ex)
            Return Tuple.Create(False, $"Validation error: {ex.Message}")
        End Try
    End Function

#End Region

#Region "NEW - Connection Pool Management"

    ''' <summary>
    ''' NEW: Clears the connection pool (use with caution)
    ''' </summary>
    Public Shared Sub ClearConnectionPool()
        Try
            MySqlConnection.ClearAllPools()
            Logger.LogInfo("Connection pool cleared")

            ' Reset health status
            _connectionHealthy = False
            _lastConnectionCheck = DateTime.MinValue

        Catch ex As Exception
            Logger.LogError("Error clearing connection pool", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Gets connection pool information
    ''' </summary>
    ''' <returns>Dictionary with pool information</returns>
    Public Shared Function GetConnectionPoolInfo() As Dictionary(Of String, Object)
        Dim info As New Dictionary(Of String, Object)

        Try
            Using conn As MySqlConnection = GetConnection()
                conn.Open()

                ' Parse connection string to get pool settings
                Dim builder As New MySqlConnectionStringBuilder(ConnectionString)
                info("MinPoolSize") = builder.MinimumPoolSize
                info("MaxPoolSize") = builder.MaximumPoolSize
                info("ConnectionTimeout") = builder.ConnectionTimeout
                info("Pooling") = builder.Pooling

                ' Get current connection count from server
                Using cmd As New MySqlCommand("SHOW STATUS WHERE Variable_name = 'Threads_connected'", conn)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            info("ActiveConnections") = reader.GetString(1)
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            Logger.LogWarning($"Error getting connection pool info: {ex.Message}")
            info("Error") = ex.Message
        End Try

        Return info
    End Function

#End Region

#Region "NEW - Diagnostic Tools"

    ''' <summary>
    ''' NEW: Runs comprehensive database diagnostics
    ''' </summary>
    ''' <returns>Dictionary with diagnostic results</returns>
    Public Shared Function RunDiagnostics() As Dictionary(Of String, Object)
        Dim diagnostics As New Dictionary(Of String, Object)

        Try
            diagnostics("Timestamp") = DateTime.Now

            ' Test basic connectivity
            diagnostics("ConnectionTest") = TestConnection()

            ' Validate connection string
            diagnostics("ConnectionStringValid") = ValidateConnectionString()

            ' Get connection statistics
            diagnostics("ConnectionStats") = GetConnectionStats()

            ' Get pool information
            diagnostics("PoolInfo") = GetConnectionPoolInfo()

            ' Test query execution
            Try
                Using conn As MySqlConnection = GetConnection()
                    conn.Open()

                    ' Test simple query
                    Dim startTime As DateTime = DateTime.Now
                    Using cmd As New MySqlCommand("SELECT VERSION()", conn)
                        Dim version As String = cmd.ExecuteScalar()?.ToString()
                        diagnostics("ServerVersion") = version
                        diagnostics("QueryResponseTime") = (DateTime.Now - startTime).TotalMilliseconds
                    End Using

                    ' Check database permissions
                    Using cmd As New MySqlCommand("SHOW GRANTS", conn)
                        Dim grants As New List(Of String)
                        Using reader As MySqlDataReader = cmd.ExecuteReader()
                            While reader.Read()
                                grants.Add(reader.GetString(0))
                            End While
                        End Using
                        diagnostics("Permissions") = grants
                    End Using
                End Using

                diagnostics("QueryTest") = "Passed"

            Catch ex As Exception
                diagnostics("QueryTest") = $"Failed: {ex.Message}"
            End Try

            Logger.LogInfo("Database diagnostics completed")

        Catch ex As Exception
            diagnostics("Error") = ex.Message
            Logger.LogError("Error running database diagnostics", ex)
        End Try

        Return diagnostics
    End Function

    ''' <summary>
    ''' NEW: Generates diagnostic report as formatted string
    ''' </summary>
    ''' <returns>Formatted diagnostic report</returns>
    Public Shared Function GenerateDiagnosticReport() As String
        Dim report As New System.Text.StringBuilder()

        Try
            report.AppendLine("=== DATABASE DIAGNOSTIC REPORT ===")
            report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            report.AppendLine()

            Dim diagnostics = RunDiagnostics()

            For Each kvp In diagnostics
                If TypeOf kvp.Value Is Dictionary(Of String, Object) Then
                    report.AppendLine($"{kvp.Key}:")
                    Dim subDict = DirectCast(kvp.Value, Dictionary(Of String, Object))
                    For Each subKvp In subDict
                        report.AppendLine($"  {subKvp.Key}: {subKvp.Value}")
                    Next
                ElseIf TypeOf kvp.Value Is List(Of String) Then
                    report.AppendLine($"{kvp.Key}:")
                    Dim list = DirectCast(kvp.Value, List(Of String))
                    For Each item In list
                        report.AppendLine($"  - {item}")
                    Next
                Else
                    report.AppendLine($"{kvp.Key}: {kvp.Value}")
                End If
            Next

            report.AppendLine()
            report.AppendLine("=== END OF REPORT ===")

        Catch ex As Exception
            report.AppendLine($"Error generating report: {ex.Message}")
        End Try

        Return report.ToString()
    End Function

#End Region

End Class