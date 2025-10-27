' ==========================================
' FILENAME: /Utils/Logger.vb
' PURPOSE: Enhanced logging with audit trail and structured logging
' AUTHOR: System
' DATE: 2025-10-14
' LAST UPDATED: 2025-10-26 - Added audit logging and structured logging
' Edited By Rovic
' For Future users please do not remove this header
' ==========================================

Imports System.IO
Imports System.Text
Imports System.Collections.Concurrent

Public Class Logger
    Private Shared ReadOnly _lockObject As New Object()
    Private Shared _logDirectory As String = Path.Combine(Application.StartupPath, "logs")
    Private Shared _logFileName As String = "errors.log"
    Private Shared _auditFileName As String = "audit.log"
    Private Shared _performanceFileName As String = "performance.log"

    ' Queue for async logging (optional enhancement)
    Private Shared _logQueue As New ConcurrentQueue(Of LogEntry)
    Private Shared _isProcessing As Boolean = False

    ''' <summary>
    ''' Log entry structure for structured logging
    ''' </summary>
    Private Class LogEntry
        Public Property Timestamp As DateTime
        Public Property Level As LogLevel
        Public Property Message As String
        Public Property Exception As Exception
        Public Property Category As String
        Public Property Username As String
        Public Property AdditionalData As Dictionary(Of String, Object)
    End Class

    ''' <summary>
    ''' Log levels
    ''' </summary>
    Public Enum LogLevel
        Debug
        Info
        Warning
        [Error]
        Critical
        Audit
        Performance
    End Enum

    ''' <summary>
    ''' Gets the full path to the log file
    ''' </summary>
    Private Shared ReadOnly Property LogFilePath As String
        Get
            Return Path.Combine(_logDirectory, _logFileName)
        End Get
    End Property

    ''' <summary>
    ''' Gets the full path to the audit log file
    ''' </summary>
    Private Shared ReadOnly Property AuditLogFilePath As String
        Get
            Return Path.Combine(_logDirectory, _auditFileName)
        End Get
    End Property

    ''' <summary>
    ''' Gets the full path to the performance log file
    ''' </summary>
    Private Shared ReadOnly Property PerformanceLogFilePath As String
        Get
            Return Path.Combine(_logDirectory, _performanceFileName)
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

            ' Create initial log files if they don't exist
            If Not File.Exists(LogFilePath) Then
                File.WriteAllText(LogFilePath, $"=== Error Log Initialized: {DateTime.Now:yyyy-MM-dd HH:mm:ss} ==={Environment.NewLine}{Environment.NewLine}")
            End If

            If Not File.Exists(AuditLogFilePath) Then
                File.WriteAllText(AuditLogFilePath, $"=== Audit Log Initialized: {DateTime.Now:yyyy-MM-dd HH:mm:ss} ==={Environment.NewLine}{Environment.NewLine}")
            End If

        Catch ex As Exception
            ' If we can't create the log directory, we can't log this error
            ' Show a message box as fallback
            MessageBox.Show($"Failed to initialize logger: {ex.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Logs an error with exception details (ENHANCED - preserves original signature)
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

            WriteToFile(LogFilePath, logEntry.ToString())

        Catch logEx As Exception
            ' If logging fails, show a message box
            MessageBox.Show($"Failed to log error: {logEx.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Logs an error message without exception (ENHANCED - preserves original signature)
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

            WriteToFile(LogFilePath, logEntry.ToString())

        Catch logEx As Exception
            MessageBox.Show($"Failed to log error: {logEx.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Logs an informational message (ENHANCED - preserves original signature)
    ''' </summary>
    ''' <param name="message">Info message</param>
    Public Shared Sub LogInfo(message As String)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}")

            WriteToFile(LogFilePath, logEntry.ToString())

        Catch logEx As Exception
            ' Silently fail for info logs
        End Try
    End Sub

    ''' <summary>
    ''' Logs a warning message (ENHANCED - preserves original signature)
    ''' </summary>
    ''' <param name="message">Warning message</param>
    Public Shared Sub LogWarning(message As String)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine($"[WARNING] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}")

            WriteToFile(LogFilePath, logEntry.ToString())

        Catch logEx As Exception
            ' Silently fail for warning logs
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Logs an audit event for sensitive operations
    ''' </summary>
    ''' <param name="action">Action performed (e.g., "Portal Access", "Data Export")</param>
    ''' <param name="username">User who performed the action</param>
    ''' <param name="details">Additional details about the action</param>
    ''' <param name="targetEntity">Entity affected (e.g., student ID, record ID)</param>
    Public Shared Sub LogAudit(action As String, username As String, details As String, Optional targetEntity As String = "")
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine($"[AUDIT] {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            logEntry.AppendLine($"Action: {action}")
            logEntry.AppendLine($"User: {username}")
            logEntry.AppendLine($"Details: {details}")

            If Not String.IsNullOrEmpty(targetEntity) Then
                logEntry.AppendLine($"Target: {targetEntity}")
            End If

            logEntry.AppendLine($"IP: {GetLocalIPAddress()}")
            logEntry.AppendLine($"Machine: {Environment.MachineName}")
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine()

            WriteToFile(AuditLogFilePath, logEntry.ToString())

        Catch logEx As Exception
            ' Critical: audit logs should not fail silently in production
            ' Log to error log as fallback
            LogError($"Failed to write audit log: {logEx.Message}", logEx)
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Logs performance metrics for slow operations
    ''' </summary>
    ''' <param name="operation">Operation name</param>
    ''' <param name="durationMs">Duration in milliseconds</param>
    ''' <param name="details">Additional details</param>
    Public Shared Sub LogPerformance(operation As String, durationMs As Long, Optional details As String = "")
        Try
            ' Only log if operation took longer than threshold (500ms)
            If durationMs < 500 Then Return

            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine($"[PERFORMANCE] {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            logEntry.AppendLine($"Operation: {operation}")
            logEntry.AppendLine($"Duration: {durationMs}ms")

            If Not String.IsNullOrEmpty(details) Then
                logEntry.AppendLine($"Details: {details}")
            End If

            logEntry.AppendLine()

            WriteToFile(PerformanceLogFilePath, logEntry.ToString())

        Catch
            ' Silently fail for performance logs
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Logs a critical error that requires immediate attention
    ''' </summary>
    ''' <param name="message">Critical error message</param>
    ''' <param name="ex">Exception object</param>
    Public Shared Sub LogCritical(message As String, ex As Exception)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine($"[CRITICAL ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            logEntry.AppendLine($"!!! IMMEDIATE ATTENTION REQUIRED !!!")
            logEntry.AppendLine($"Message: {message}")
            logEntry.AppendLine($"Exception Type: {ex.GetType().Name}")
            logEntry.AppendLine($"Exception Message: {ex.Message}")
            logEntry.AppendLine($"Stack Trace: {ex.StackTrace}")

            If ex.InnerException IsNot Nothing Then
                logEntry.AppendLine($"Inner Exception: {ex.InnerException.Message}")
                logEntry.AppendLine($"Inner Stack Trace: {ex.InnerException.StackTrace}")
            End If

            logEntry.AppendLine($"Machine: {Environment.MachineName}")
            logEntry.AppendLine($"User: {Environment.UserName}")
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine()

            WriteToFile(LogFilePath, logEntry.ToString())

            ' Also show immediate notification
            MessageBox.Show(
                $"A critical error has occurred: {message}{Environment.NewLine}{Environment.NewLine}Please contact system administrator.",
                "Critical Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        Catch logEx As Exception
            MessageBox.Show($"Critical logging failure: {logEx.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Logs database operation errors with connection details
    ''' </summary>
    ''' <param name="operation">Database operation (e.g., "ExecuteReader", "ExecuteNonQuery")</param>
    ''' <param name="query">SQL query (sanitized)</param>
    ''' <param name="ex">Exception object</param>
    Public Shared Sub LogDatabaseError(operation As String, query As String, ex As Exception)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine($"[DATABASE ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            logEntry.AppendLine($"Operation: {operation}")
            logEntry.AppendLine($"Query: {SanitizeQuery(query)}")
            logEntry.AppendLine($"Exception Type: {ex.GetType().Name}")
            logEntry.AppendLine($"Exception Message: {ex.Message}")

            ' Check for common database error patterns
            If ex.Message.Contains("timeout") Then
                logEntry.AppendLine("*** TIMEOUT ERROR - Consider query optimization ***")
            ElseIf ex.Message.Contains("deadlock") Then
                logEntry.AppendLine("*** DEADLOCK DETECTED - Review transaction isolation ***")
            ElseIf ex.Message.Contains("connection") Then
                logEntry.AppendLine("*** CONNECTION ERROR - Check database availability ***")
            End If

            logEntry.AppendLine($"Stack Trace: {ex.StackTrace}")
            logEntry.AppendLine("=" & New String("="c, 80))
            logEntry.AppendLine()

            WriteToFile(LogFilePath, logEntry.ToString())

        Catch logEx As Exception
            MessageBox.Show($"Failed to log database error: {logEx.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Structured logging with additional context
    ''' </summary>
    ''' <param name="level">Log level</param>
    ''' <param name="message">Log message</param>
    ''' <param name="category">Category (e.g., "Database", "UI", "Security")</param>
    ''' <param name="additionalData">Additional structured data</param>
    Public Shared Sub Log(level As LogLevel, message As String, Optional category As String = "", Optional additionalData As Dictionary(Of String, Object) = Nothing)
        Try
            Initialize()

            Dim logEntry As New StringBuilder()
            logEntry.AppendLine($"[{level.ToString().ToUpper()}] {DateTime.Now:yyyy-MM-dd HH:mm:ss}")

            If Not String.IsNullOrEmpty(category) Then
                logEntry.AppendLine($"Category: {category}")
            End If

            logEntry.AppendLine($"Message: {message}")

            If additionalData IsNot Nothing AndAlso additionalData.Count > 0 Then
                logEntry.AppendLine("Additional Data:")
                For Each kvp In additionalData
                    logEntry.AppendLine($"  {kvp.Key}: {kvp.Value}")
                Next
            End If

            logEntry.AppendLine()

            ' Write to appropriate log file based on level
            Dim targetFile As String = LogFilePath
            If level = LogLevel.Audit Then
                targetFile = AuditLogFilePath
            ElseIf level = LogLevel.Performance Then
                targetFile = PerformanceLogFilePath
            End If

            WriteToFile(targetFile, logEntry.ToString())

        Catch logEx As Exception
            ' Silently fail unless critical
            If level = LogLevel.Critical Then
                MessageBox.Show($"Failed to log critical message: {logEx.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Writes log entry to file (ENHANCED with better error handling)
    ''' </summary>
    ''' <param name="filePath">Path to log file</param>
    ''' <param name="logEntry">Log entry text</param>
    Private Shared Sub WriteToFile(filePath As String, logEntry As String)
        SyncLock _lockObject
            Dim retryCount As Integer = 0
            Const maxRetries As Integer = 3

            While retryCount < maxRetries
                Try
                    Using writer As New StreamWriter(filePath, True, Encoding.UTF8)
                        writer.Write(logEntry)
                    End Using
                    Return ' Success

                Catch ex As IOException When retryCount < maxRetries - 1
                    ' File might be locked, wait and retry
                    retryCount += 1
                    Threading.Thread.Sleep(100 * retryCount) ' Exponential backoff

                Catch ex As Exception
                    ' Last resort - show message box if file write fails after retries
                    If retryCount = maxRetries - 1 Then
                        MessageBox.Show($"Failed to write to log file after {maxRetries} attempts: {ex.Message}", "Logger Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    Exit While
                End Try

                retryCount += 1
            End While
        End SyncLock
    End Sub

    ''' <summary>
    ''' Clears the log file (ENHANCED - preserves original signature)
    ''' </summary>
    Public Shared Sub ClearLog()
        Try
            If File.Exists(LogFilePath) Then
                File.Delete(LogFilePath)
                Initialize() ' Recreate with header
            End If
        Catch ex As Exception
            LogError("Failed to clear log file", ex)
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Clears the audit log file
    ''' </summary>
    Public Shared Sub ClearAuditLog()
        Try
            If File.Exists(AuditLogFilePath) Then
                File.Delete(AuditLogFilePath)
                Initialize() ' Recreate with header
            End If
        Catch ex As Exception
            LogError("Failed to clear audit log file", ex)
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Clears the performance log file
    ''' </summary>
    Public Shared Sub ClearPerformanceLog()
        Try
            If File.Exists(PerformanceLogFilePath) Then
                File.Delete(PerformanceLogFilePath)
                Initialize() ' Recreate with header
            End If
        Catch ex As Exception
            LogError("Failed to clear performance log file", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the log file content (ENHANCED - preserves original signature)
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

    ''' <summary>
    ''' NEW: Gets the audit log file content
    ''' </summary>
    ''' <returns>Audit log file content as string</returns>
    Public Shared Function GetAuditLogContent() As String
        Try
            If File.Exists(AuditLogFilePath) Then
                Return File.ReadAllText(AuditLogFilePath, Encoding.UTF8)
            End If
            Return "Audit log file is empty."
        Catch ex As Exception
            Return $"Error reading audit log file: {ex.Message}"
        End Try
    End Function

    ''' <summary>
    ''' NEW: Gets the performance log file content
    ''' </summary>
    ''' <returns>Performance log file content as string</returns>
    Public Shared Function GetPerformanceLogContent() As String
        Try
            If File.Exists(PerformanceLogFilePath) Then
                Return File.ReadAllText(PerformanceLogFilePath, Encoding.UTF8)
            End If
            Return "Performance log file is empty."
        Catch ex As Exception
            Return $"Error reading performance log file: {ex.Message}"
        End Try
    End Function

    ''' <summary>
    ''' NEW: Archives old logs (keeps logs under control)
    ''' </summary>
    ''' <param name="daysToKeep">Number of days to keep logs</param>
    Public Shared Sub ArchiveOldLogs(Optional daysToKeep As Integer = 30)
        Try
            Dim archiveDir As String = Path.Combine(_logDirectory, "archive")
            If Not Directory.Exists(archiveDir) Then
                Directory.CreateDirectory(archiveDir)
            End If

            ' Archive main log if older than specified days
            ArchiveLogFile(LogFilePath, archiveDir, daysToKeep)
            ArchiveLogFile(AuditLogFilePath, archiveDir, daysToKeep)
            ArchiveLogFile(PerformanceLogFilePath, archiveDir, daysToKeep)

        Catch ex As Exception
            LogError("Failed to archive old logs", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Helper method to archive a single log file
    ''' </summary>
    Private Shared Sub ArchiveLogFile(filePath As String, archiveDir As String, daysToKeep As Integer)
        Try
            If File.Exists(filePath) Then
                Dim fileInfo As New FileInfo(filePath)
                If fileInfo.LastWriteTime < DateTime.Now.AddDays(-daysToKeep) Then
                    Dim archiveName As String = $"{Path.GetFileNameWithoutExtension(filePath)}_{fileInfo.LastWriteTime:yyyyMMdd}{Path.GetExtension(filePath)}"
                    Dim archivePath As String = Path.Combine(archiveDir, archiveName)
                    File.Move(filePath, archivePath)
                    Initialize() ' Create new log file
                End If
            End If
        Catch ex As Exception
            ' Silently fail archiving
        End Try
    End Sub

    ''' <summary>
    ''' Helper method to get local IP address for audit logs
    ''' </summary>
    Private Shared Function GetLocalIPAddress() As String
        Try
            Dim host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            For Each ip In host.AddressList
                If ip.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    Return ip.ToString()
                End If
            Next
            Return "Unknown"
        Catch
            Return "Unknown"
        End Try
    End Function

    ''' <summary>
    ''' Helper method to sanitize SQL queries for logging (remove sensitive data)
    ''' </summary>
    Private Shared Function SanitizeQuery(query As String) As String
        Try
            ' Remove potential passwords or sensitive data
            Dim sanitized As String = query

            ' Replace password parameters
            sanitized = System.Text.RegularExpressions.Regex.Replace(sanitized, "@password\s*=\s*'[^']*'", "@password = '***'", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            sanitized = System.Text.RegularExpressions.Regex.Replace(sanitized, "@password\s*,\s*'[^']*'", "@password, '***'", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' Truncate very long queries
            If sanitized.Length > 500 Then
                sanitized = sanitized.Substring(0, 500) & "... (truncated)"
            End If

            Return sanitized
        Catch
            Return "Error sanitizing query"
        End Try
    End Function

End Class