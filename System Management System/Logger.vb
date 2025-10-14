' ==========================================
' FILENAME: /Utils/Logger.vb
' PURPOSE: Writes error logs to file for debugging and monitoring
' AUTHOR: System
' DATE: 2025-10-14
' ==========================================

Imports System.IO
Imports System.Text

Public Class Logger
    Private Shared ReadOnly _lockObject As New Object()
    Private Shared _logDirectory As String = Path.Combine(Application.StartupPath, "logs")
    Private Shared _logFileName As String = "errors.log"

    ''' <summary>
    ''' Gets the full path to the log file
    ''' </summary>
    Private Shared ReadOnly Property LogFilePath As String
        Get
            Return Path.Combine(_logDirectory, _logFileName)
        End Get
    End Property

    ''' <summary>
    ''' Initializes the logger and creates log directory if it doesn't exist
    ''' </summary>
    Public Shared Sub Initialize()
        Try
            If Not Directory.Exists(_logDirectory) Then
                Directory.CreateDirectory(_logDirectory)
            End If
        Catch ex As Exception
            ' If we can't create the log directory, we can't log this error
            ' Show a message box as fallback
            MessageBox.Show($"Failed to initialize logger: {ex.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Logs an error with exception details
    ''' </summary>
    ''' <param name="message">Error message</param>
    ''' <param name="ex">Exception object</param>
    Public Shared Sub LogError(message As String, ex As Exception)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            logEntry.AppendLine($"Message: {message}")
            logEntry.AppendLine($"Exception Type: {ex.GetType().Name}")
            logEntry.AppendLine($"Exception Message: {ex.Message}")
            logEntry.AppendLine($"Stack Trace: {ex.StackTrace}")

            If ex.InnerException IsNot Nothing Then
                logEntry.AppendLine($"Inner Exception: {ex.InnerException.Message}")
                logEntry.AppendLine($"Inner Stack Trace: {ex.InnerException.StackTrace}")
            End If

            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine()

            WriteToFile(logEntry.ToString())
        Catch logEx As Exception
            ' If logging fails, show a message box
            MessageBox.Show($"Failed to log error: {logEx.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Logs an error message without exception
    ''' </summary>
    ''' <param name="message">Error message</param>
    Public Shared Sub LogError(message As String)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            logEntry.AppendLine($"Message: {message}")
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine()

            WriteToFile(logEntry.ToString())
        Catch logEx As Exception
            MessageBox.Show($"Failed to log error: {logEx.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Logs an informational message
    ''' </summary>
    ''' <param name="message">Info message</param>
    Public Shared Sub LogInfo(message As String)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}")

            WriteToFile(logEntry.ToString())
        Catch logEx As Exception
            ' Silently fail for info logs
        End Try
    End Sub

    ''' <summary>
    ''' Logs a warning message
    ''' </summary>
    ''' <param name="message">Warning message</param>
    Public Shared Sub LogWarning(message As String)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine($"[WARNING] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}")

            WriteToFile(logEntry.ToString())
        Catch logEx As Exception
            ' Silently fail for warning logs
        End Try
    End Sub

    ''' <summary>
    ''' Writes log entry to file
    ''' </summary>
    ''' <param name="logEntry">Log entry text</param>
    Private Shared Sub WriteToFile(logEntry As String)
        SyncLock _lockObject
            Try
                Using writer As New StreamWriter(LogFilePath, True, Encoding.UTF8)
                    writer.Write(logEntry)
                End Using
            Catch ex As Exception
                ' Last resort - show message box if file write fails
                MessageBox.Show($"Failed to write to log file: {ex.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End SyncLock
    End Sub

    ''' <summary>
    ''' Clears the log file
    ''' </summary>
    Public Shared Sub ClearLog()
        Try
            If File.Exists(LogFilePath) Then
                File.Delete(LogFilePath)
            End If
        Catch ex As Exception
            LogError("Failed to clear log file", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the log file content
    ''' </summary>
    ''' <returns>Log file content as string</returns>
    Public Shared Function GetLogContent() As String
        Try
            If File.Exists(LogFilePath) Then
                Return File.ReadAllText(LogFilePath, Encoding.UTF8)
            End If
            Return "Log file is empty."
        Catch ex As Exception
            Return $"Error reading log file: {ex.Message}"
        End Try
    End Function

End Class