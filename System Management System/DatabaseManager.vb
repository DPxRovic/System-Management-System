' ==========================================
' FILENAME: /Data/DatabaseManager.vb
' PURPOSE: Handles database connection configuration and management
' AUTHOR: System
' DATE: 2025-10-14
' Edited By Rovic
' For Future users please do not remove this header
' ==========================================

Imports MySql.Data.MySqlClient
Imports System.Configuration

Public Class DatabaseManager
    ' Database connection configuration
    Private Shared _server As String = "localhost"
    Private Shared _port As String = "3306"
    Private Shared _database As String = "student_management_db"
    Private Shared _username As String = "root"
    Private Shared _password As String = ""

    ' Connection string builder
    Private Shared _connectionString As String = ""

    ''' <summary>
    ''' Gets the connection string for the database
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
    ''' Gets the connection string without database (for initial DB creation)
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
    ''' Builds the connection string with all parameters
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

            _connectionString = builder.ToString()
        Catch ex As Exception
            Logger.LogError("Error building connection string", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Tests the database connection
    ''' </summary>
    ''' <returns>True if connection successful, False otherwise</returns>
    Public Shared Function TestConnection() As Boolean
        Try
            Using conn As New MySqlConnection(ConnectionString)
                conn.Open()
                conn.Close()
                Return True
            End Using
        Catch ex As Exception
            Logger.LogError("Database connection test failed", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Creates a new database connection
    ''' </summary>
    Public Shared Function GetConnection() As MySqlConnection
        Try
            Return New MySqlConnection(ConnectionString)
        Catch ex As Exception
            Logger.LogError("Error creating database connection", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Creates a connection without specifying database (for initial setup)
    ''' </summary>
    Public Shared Function GetConnectionWithoutDatabase() As MySqlConnection
        Try
            Return New MySqlConnection(ConnectionStringWithoutDatabase)
        Catch ex As Exception
            Logger.LogError("Error creating database connection without database", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Updates database configuration
    ''' </summary>
    Public Shared Sub UpdateConfiguration(server As String, port As String, database As String, username As String, password As String)
        _server = server
        _port = port
        _database = database
        _username = username
        _password = password
        _connectionString = ""
        BuildConnectionString()
    End Sub

    ''' <summary>
    ''' Gets the database name
    ''' </summary>
    Public Shared ReadOnly Property DatabaseName As String
        Get
            Return _database
        End Get
    End Property

End Class