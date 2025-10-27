' ==========================================
' FILENAME: /Utils/ErrorHandler.vb
' PURPOSE: Centralized error handling and user-friendly error messaging
' AUTHOR: System
' DATE: 2025-10-26
' For Future users please do not remove this header
' ==========================================

Imports System.Text
Imports System.Windows.Forms

Public Class ErrorHandler

    ''' <summary>
    ''' Error severity levels
    ''' </summary>
    Public Enum ErrorSeverity
        Info
        Warning
        [Error]
        Critical
    End Enum

    ''' <summary>
    ''' Error context information
    ''' </summary>
    Public Class ErrorContext
        Public Property Source As String
        Public Property Operation As String
        Public Property Username As String
        Public Property Timestamp As DateTime = DateTime.Now
        Public Property AdditionalInfo As Dictionary(Of String, Object)
    End Class

    ''' <summary>
    ''' Shows a user-friendly error message and logs the error
    ''' </summary>
    ''' <param name="userMessage">User-friendly message to display</param>
    ''' <param name="ex">Exception that occurred</param>
    ''' <param name="severity">Severity level</param>
    ''' <param name="context">Error context for logging</param>
    Public Shared Sub Handle(userMessage As String, ex As Exception, Optional severity As ErrorSeverity = ErrorSeverity.Error, Optional context As ErrorContext = Nothing)
        Try
            ' Log the error with full details
            If context IsNot Nothing Then
                Dim logMessage As String = BuildContextualLogMessage(userMessage, context)
                Logger.LogError(logMessage, ex)
            Else
                Logger.LogError(userMessage, ex)
            End If

            ' Show user-friendly message
            Dim icon As MessageBoxIcon = GetIconForSeverity(severity)
            Dim title As String = GetTitleForSeverity(severity)

            ' Build user message
            Dim displayMessage As String = BuildUserMessage(userMessage, ex, severity)

            MessageBox.Show(displayMessage, title, MessageBoxButtons.OK, icon)

        Catch logEx As Exception
            ' Fallback if error handling itself fails
            MessageBox.Show($"An error occurred: {userMessage}{Environment.NewLine}{Environment.NewLine}Additionally, error logging failed.",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Shows a user-friendly error message without exception
    ''' </summary>
    ''' <param name="userMessage">User-friendly message to display</param>
    ''' <param name="severity">Severity level</param>
    Public Shared Sub Handle(userMessage As String, Optional severity As ErrorSeverity = ErrorSeverity.Error)
        Try
            ' Log the error
            Select Case severity
                Case ErrorSeverity.Info
                    Logger.LogInfo(userMessage)
                Case ErrorSeverity.Warning
                    Logger.LogWarning(userMessage)
                Case ErrorSeverity.Error, ErrorSeverity.Critical
                    Logger.LogError(userMessage)
            End Select

            ' Show user-friendly message
            Dim icon As MessageBoxIcon = GetIconForSeverity(severity)
            Dim title As String = GetTitleForSeverity(severity)

            MessageBox.Show(userMessage, title, MessageBoxButtons.OK, icon)

        Catch logEx As Exception
            ' Fallback
            MessageBox.Show(userMessage, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    ''' <summary>
    ''' Handles database errors with retry option
    ''' </summary>
    ''' <param name="userMessage">User-friendly message</param>
    ''' <param name="ex">Database exception</param>
    ''' <param name="context">Error context</param>
    ''' <returns>True if user wants to retry</returns>
    Public Shared Function HandleDatabaseError(userMessage As String, ex As Exception, Optional context As ErrorContext = Nothing) As Boolean
        Try
            ' Resolve operation name safely (avoid C#-style ?/??)
            Dim operationName As String = "Unknown"
            If context IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(context.Operation) Then
                operationName = context.Operation
            End If

            ' Log database error
            Logger.LogDatabaseError(operationName, String.Empty, ex)

            ' Determine if error is retryable
            Dim isRetryable As Boolean = IsRetryableError(ex)

            Dim message As String = BuildDatabaseErrorMessage(userMessage, ex, isRetryable)

            If isRetryable Then
                Dim result As DialogResult = MessageBox.Show(
                message,
                "Database Error",
                MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Warning)
                Return result = DialogResult.Retry
            Else
                MessageBox.Show(message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

        Catch
            MessageBox.Show(userMessage, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Handles validation errors
    ''' </summary>
    ''' <param name="fieldName">Field that failed validation</param>
    ''' <param name="validationMessage">Validation error message</param>
    ''' <param name="control">Control to focus (optional)</param>
    Public Shared Sub HandleValidationError(fieldName As String, validationMessage As String, Optional control As Control = Nothing)
        Try
            Logger.LogWarning($"Validation error - {fieldName}: {validationMessage}")

            Dim message As String = $"Validation Error:{Environment.NewLine}{Environment.NewLine}{validationMessage}"

            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            ' Focus the control if provided
            If control IsNot Nothing AndAlso control.CanFocus Then
                control.Focus()
            End If

        Catch
            MessageBox.Show(validationMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ''' <summary>
    ''' Shows a confirmation dialog for potentially dangerous operations
    ''' </summary>
    ''' <param name="message">Confirmation message</param>
    ''' <param name="title">Dialog title</param>
    ''' <param name="defaultButton">Default button</param>
    ''' <returns>True if user confirmed</returns>
    Public Shared Function Confirm(message As String, Optional title As String = "Confirm Action", Optional defaultButton As MessageBoxDefaultButton = MessageBoxDefaultButton.Button2) As Boolean
        Try
            Dim result As DialogResult = MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                defaultButton)

            Return result = DialogResult.Yes

        Catch
            ' Default to safe option (No)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Shows an informational message
    ''' </summary>
    ''' <param name="message">Information message</param>
    ''' <param name="title">Dialog title</param>
    Public Shared Sub ShowInfo(message As String, Optional title As String = "Information")
        Try
            Logger.LogInfo(message)
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    ''' <summary>
    ''' Shows a success message
    ''' </summary>
    ''' <param name="message">Success message</param>
    ''' <param name="title">Dialog title</param>
    Public Shared Sub ShowSuccess(message As String, Optional title As String = "Success")
        Try
            Logger.LogInfo($"Success: {message}")
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    ''' <summary>
    ''' Shows a warning message
    ''' </summary>
    ''' <param name="message">Warning message</param>
    ''' <param name="title">Dialog title</param>
    Public Shared Sub ShowWarning(message As String, Optional title As String = "Warning")
        Try
            Logger.LogWarning(message)
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ''' <summary>
    ''' Wraps an operation with error handling and optional retry logic
    ''' </summary>
    ''' <param name="operation">Operation to execute</param>
    ''' <param name="operationName">Name of the operation for logging</param>
    ''' <param name="userMessage">User-friendly error message</param>
    ''' <param name="maxRetries">Maximum number of retries for retryable errors</param>
    ''' <returns>True if operation succeeded</returns>
    Public Shared Function ExecuteWithErrorHandling(operation As Action, operationName As String, userMessage As String, Optional maxRetries As Integer = 0) As Boolean
        Dim attempts As Integer = 0

        While attempts <= maxRetries
            Try
                operation()
                Return True

            Catch ex As Exception
                attempts += 1

                Dim context As New ErrorContext With {
                    .Operation = operationName,
                    .Timestamp = DateTime.Now
                }

                If attempts > maxRetries OrElse Not IsRetryableError(ex) Then
                    ' Final attempt failed or error is not retryable
                    Handle(userMessage, ex, ErrorSeverity.Error, context)
                    Return False
                Else
                    ' Log retry attempt
                    Logger.LogWarning($"Retrying operation '{operationName}' (attempt {attempts + 1}/{maxRetries + 1})")
                    Threading.Thread.Sleep(500 * attempts) ' Exponential backoff
                End If
            End Try
        End While

        Return False
    End Function

    ''' <summary>
    ''' Wraps a function with error handling and returns result or default value
    ''' </summary>
    ''' <typeparam name="T">Return type</typeparam>
    ''' <param name="operation">Function to execute</param>
    ''' <param name="operationName">Name of the operation for logging</param>
    ''' <param name="userMessage">User-friendly error message</param>
    ''' <param name="defaultValue">Default value to return on error</param>
    ''' <returns>Result of operation or default value</returns>
    Public Shared Function ExecuteWithErrorHandling(Of T)(operation As Func(Of T), operationName As String, userMessage As String, defaultValue As T) As T
        Try
            Return operation()

        Catch ex As Exception
            Dim context As New ErrorContext With {
                .Operation = operationName,
                .Timestamp = DateTime.Now
            }

            Handle(userMessage, ex, ErrorSeverity.Error, context)
            Return defaultValue
        End Try
    End Function

#Region "Helper Methods"

    ''' <summary>
    ''' Gets the appropriate icon for the severity level
    ''' </summary>
    Private Shared Function GetIconForSeverity(severity As ErrorSeverity) As MessageBoxIcon
        Select Case severity
            Case ErrorSeverity.Info
                Return MessageBoxIcon.Information
            Case ErrorSeverity.Warning
                Return MessageBoxIcon.Warning
            Case ErrorSeverity.Error
                Return MessageBoxIcon.Error
            Case ErrorSeverity.Critical
                Return MessageBoxIcon.Error
            Case Else
                Return MessageBoxIcon.Information
        End Select
    End Function

    ''' <summary>
    ''' Gets the appropriate title for the severity level
    ''' </summary>
    Private Shared Function GetTitleForSeverity(severity As ErrorSeverity) As String
        Select Case severity
            Case ErrorSeverity.Info
                Return "Information"
            Case ErrorSeverity.Warning
                Return "Warning"
            Case ErrorSeverity.Error
                Return "Error"
            Case ErrorSeverity.Critical
                Return "Critical Error"
            Case Else
                Return "Message"
        End Select
    End Function

    ''' <summary>
    ''' Builds a user-friendly error message
    ''' </summary>
    Private Shared Function BuildUserMessage(userMessage As String, ex As Exception, severity As ErrorSeverity) As String
        Dim sb As New StringBuilder()

        sb.AppendLine(userMessage)

        ' Add additional details for developers in debug mode
#If DEBUG Then
        sb.AppendLine()
        sb.AppendLine("--- Technical Details (Debug Mode) ---")
        sb.AppendLine($"Exception: {ex.GetType().Name}")
        sb.AppendLine($"Message: {ex.Message}")

        If ex.InnerException IsNot Nothing Then
            sb.AppendLine($"Inner: {ex.InnerException.Message}")
        End If
#End If

        ' Add suggestions based on error type
        If severity = ErrorSeverity.Critical Then
            sb.AppendLine()
            sb.AppendLine("Please contact your system administrator.")
        End If

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Builds a contextual log message with full details
    ''' </summary>
    Private Shared Function BuildContextualLogMessage(userMessage As String, context As ErrorContext) As String
        Dim sb As New StringBuilder()

        sb.AppendLine(userMessage)

        ' Resolve context fields safely (VB-compatible)
        Dim sourceStr As String = "Unknown"
        Dim operationStr As String = "Unknown"
        Dim usernameStr As String = "Unknown"

        If context IsNot Nothing Then
            If Not String.IsNullOrWhiteSpace(context.Source) Then
                sourceStr = context.Source
            End If
            If Not String.IsNullOrWhiteSpace(context.Operation) Then
                operationStr = context.Operation
            End If
            If Not String.IsNullOrWhiteSpace(context.Username) Then
                usernameStr = context.Username
            End If
        End If

        sb.AppendLine("Source: " & sourceStr)
        sb.AppendLine("Operation: " & operationStr)
        sb.AppendLine("Username: " & usernameStr)
        sb.AppendLine($"Timestamp: {If(context IsNot Nothing, context.Timestamp, DateTime.Now):yyyy-MM-dd HH:mm:ss}")

        If context IsNot Nothing AndAlso context.AdditionalInfo IsNot Nothing Then
            sb.AppendLine("Additional Info:")
            For Each kvp In context.AdditionalInfo
                sb.AppendLine($"  {kvp.Key}: {kvp.Value}")
            Next
        End If

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Builds a database error message with helpful information
    ''' </summary>
    Private Shared Function BuildDatabaseErrorMessage(userMessage As String, ex As Exception, isRetryable As Boolean) As String
        Dim sb As New StringBuilder()

        sb.AppendLine(userMessage)
        sb.AppendLine()

        ' Add specific guidance based on error type
        If ex.Message.Contains("timeout") Then
            sb.AppendLine("The operation took too long to complete.")
            sb.AppendLine("This may be due to high server load or network issues.")
        ElseIf ex.Message.Contains("connection") OrElse ex.Message.Contains("network") Then
            sb.AppendLine("Unable to connect to the database.")
            sb.AppendLine("Please check your network connection.")
        ElseIf ex.Message.Contains("duplicate") OrElse ex.Message.Contains("unique") Then
            sb.AppendLine("This record already exists in the database.")
            sb.AppendLine("Please use different values.")
        ElseIf ex.Message.Contains("foreign key") OrElse ex.Message.Contains("constraint") Then
            sb.AppendLine("This operation would violate data integrity rules.")
            sb.AppendLine("Please check related records first.")
        End If

        If isRetryable Then
            sb.AppendLine()
            sb.AppendLine("Click Retry to try again, or Cancel to abort.")
        End If

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Determines if an error is potentially retryable
    ''' </summary>
    Private Shared Function IsRetryableError(ex As Exception) As Boolean
        Dim message As String = ex.Message.ToLower()

        ' Timeout errors
        If message.Contains("timeout") Then Return True

        ' Connection errors
        If message.Contains("connection") AndAlso Not message.Contains("login") Then Return True

        ' Network errors
        If message.Contains("network") Then Return True

        ' Temporary unavailability
        If message.Contains("unavailable") Then Return True

        ' Deadlock
        If message.Contains("deadlock") Then Return True

        ' Transaction errors
        If message.Contains("transaction") AndAlso message.Contains("rollback") Then Return True

        Return False
    End Function

#End Region

#Region "Form State Preservation"

    ''' <summary>
    ''' Saves form state before showing error (helps with recovery)
    ''' </summary>
    ''' <param name="form">Form to save state for</param>
    ''' <returns>Dictionary with form state</returns>
    Public Shared Function SaveFormState(form As Form) As Dictionary(Of String, Object)
        Dim state As New Dictionary(Of String, Object)

        Try
            state("FormName") = form.Name
            state("WindowState") = form.WindowState

            ' Save control values
            Dim controls As New Dictionary(Of String, Object)
            SaveControlStates(form.Controls, controls)
            state("Controls") = controls

        Catch ex As Exception
            Logger.LogWarning($"Failed to save form state: {ex.Message}")
        End Try

        Return state
    End Function

    ''' <summary>
    ''' Recursively saves control states
    ''' </summary>
    Private Shared Sub SaveControlStates(controls As Control.ControlCollection, state As Dictionary(Of String, Object))
        For Each ctrl As Control In controls
            Try
                Dim key As String = $"{ctrl.Name}_{ctrl.GetType().Name}"

                If TypeOf ctrl Is TextBox Then
                    state(key) = DirectCast(ctrl, TextBox).Text
                ElseIf TypeOf ctrl Is ComboBox Then
                    state(key) = DirectCast(ctrl, ComboBox).SelectedIndex
                ElseIf TypeOf ctrl Is CheckBox Then
                    state(key) = DirectCast(ctrl, CheckBox).Checked
                ElseIf TypeOf ctrl Is DateTimePicker Then
                    state(key) = DirectCast(ctrl, DateTimePicker).Value
                End If

                ' Recurse into child controls
                If ctrl.HasChildren Then
                    SaveControlStates(ctrl.Controls, state)
                End If

            Catch
                ' Skip controls that can't be saved
            End Try
        Next
    End Sub

    ''' <summary>
    ''' Attempts to restore form state after error recovery
    ''' </summary>
    ''' <param name="form">Form to restore state for</param>
    ''' <param name="state">Saved state dictionary</param>
    Public Shared Sub RestoreFormState(form As Form, state As Dictionary(Of String, Object))
        Try
            If state Is Nothing OrElse state.Count = 0 Then Return

            If state.ContainsKey("Controls") Then
                Dim controlStates As Dictionary(Of String, Object) = DirectCast(state("Controls"), Dictionary(Of String, Object))
                RestoreControlStates(form.Controls, controlStates)
            End If

        Catch ex As Exception
            Logger.LogWarning($"Failed to restore form state: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Recursively restores control states
    ''' </summary>
    Private Shared Sub RestoreControlStates(controls As Control.ControlCollection, state As Dictionary(Of String, Object))
        For Each ctrl As Control In controls
            Try
                Dim key As String = $"{ctrl.Name}_{ctrl.GetType().Name}"

                If state.ContainsKey(key) Then
                    If TypeOf ctrl Is TextBox Then
                        DirectCast(ctrl, TextBox).Text = state(key).ToString()
                    ElseIf TypeOf ctrl Is ComboBox Then
                        DirectCast(ctrl, ComboBox).SelectedIndex = Convert.ToInt32(state(key))
                    ElseIf TypeOf ctrl Is CheckBox Then
                        DirectCast(ctrl, CheckBox).Checked = Convert.ToBoolean(state(key))
                    ElseIf TypeOf ctrl Is DateTimePicker Then
                        DirectCast(ctrl, DateTimePicker).Value = Convert.ToDateTime(state(key))
                    End If
                End If

                ' Recurse into child controls
                If ctrl.HasChildren Then
                    RestoreControlStates(ctrl.Controls, state)
                End If

            Catch
                ' Skip controls that can't be restored
            End Try
        Next
    End Sub

#End Region

End Class