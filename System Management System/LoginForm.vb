' ==========================================
' FILENAME: /Forms/LoginForm.vb
' PURPOSE: Modern login form with database authentication
' AUTHOR: System
' DATE: 2025-10-14
' ==========================================

Imports System.Data
Imports Microsoft.VisualBasic.ApplicationServices

Public Class LoginForm
    Private currentUser As User = Nothing

    ''' <summary>
    ''' Form load event - Initialize database
    ''' </summary>
    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize logger
            Logger.Initialize()
            Logger.LogInfo("Application started")

            ' Initialize database
            If Not DatabaseInitializer.Initialize() Then
                MessageBox.Show("Failed to initialize database. Please check the logs.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
            End If

            ' Set focus to username textbox
            txtUsername.Focus()

        Catch ex As Exception
            Logger.LogError("Error loading login form", ex)
            MessageBox.Show($"An error occurred while loading the application: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End Try
    End Sub

    ''' <summary>
    ''' Login button click event
    ''' </summary>
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Try
            ' Validate inputs
            If String.IsNullOrWhiteSpace(txtUsername.Text) Then
                MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtUsername.Focus()
                Return
            End If

            If String.IsNullOrWhiteSpace(txtPassword.Text) Then
                MessageBox.Show("Please enter your password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Focus()
                Return
            End If

            ' Show loading cursor
            Me.Cursor = Cursors.WaitCursor
            btnLogin.Enabled = False

            ' Authenticate user
            Dim user As User = AuthenticateUser(txtUsername.Text.Trim(), txtPassword.Text)

            If user IsNot Nothing Then
                Logger.LogInfo($"User {user.Username} logged in successfully with role {user.Role}")

                ' Store current user
                currentUser = user

                ' Show success message
                MessageBox.Show($"Welcome, {user.FullName}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Open dashboard form
                Dim dashboard As New DashboardForm(user)
                Me.Hide()
                dashboard.ShowDialog()

                ' Clear password field when returning to login
                txtPassword.Clear()
                txtUsername.Clear()
                txtUsername.Focus()
                Me.Show()
            Else
                Logger.LogWarning($"Failed login attempt for username: {txtUsername.Text}")
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPassword.Clear()
                txtPassword.Focus()
            End If

        Catch ex As Exception
            Logger.LogError("Error during login", ex)
            MessageBox.Show($"An error occurred during login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Reset cursor and button
            Me.Cursor = Cursors.Default
            btnLogin.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' Authenticates user against database
    ''' </summary>
    ''' <param name="username">Username</param>
    ''' <param name="password">Password</param>
    ''' <returns>User object if authenticated, Nothing otherwise</returns>
    Private Function AuthenticateUser(username As String, password As String) As User
        Try
            Dim query As String = "SELECT id, username, password, role, fullname, created_at FROM users WHERE username = @username AND password = @password"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@username", username},
                {"@password", password}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                Dim user As New User With {
                    .Id = Convert.ToInt32(row("id")),
                    .username = row("username").ToString(),
                    .password = row("password").ToString(),
                    .Role = row("role").ToString(),
                    .FullName = row("fullname").ToString(),
                    .CreatedAt = Convert.ToDateTime(row("created_at"))
                }

                Return user
            End If

            Return Nothing

        Catch ex As Exception
            Logger.LogError("Error authenticating user", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Exit button click event
    ''' </summary>
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Logger.LogInfo("Application closed by user")
                Application.Exit()
            End If
        Catch ex As Exception
            Logger.LogError("Error during exit", ex)
            Application.Exit()
        End Try
    End Sub

    ''' <summary>
    ''' Handle Enter key press in username textbox
    ''' </summary>
    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtPassword.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handle Enter key press in password textbox
    ''' </summary>
    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            btnLogin.PerformClick()
        End If
    End Sub

    ''' <summary>
    ''' Password textbox hover effect
    ''' </summary>
    Private Sub txtPassword_MouseEnter(sender As Object, e As EventArgs) Handles txtPassword.MouseEnter
        txtPassword.BorderColor = Color.FromArgb(26, 188, 156) ' Accent color
    End Sub

    Private Sub txtPassword_MouseLeave(sender As Object, e As EventArgs) Handles txtPassword.MouseLeave
        If Not txtPassword.Focused Then
            txtPassword.BorderColor = Color.FromArgb(213, 218, 223)
        End If
    End Sub

    ''' <summary>
    ''' Username textbox hover effect
    ''' </summary>
    Private Sub txtUsername_MouseEnter(sender As Object, e As EventArgs) Handles txtUsername.MouseEnter
        txtUsername.BorderColor = Color.FromArgb(26, 188, 156) ' Accent color
    End Sub

    Private Sub txtUsername_MouseLeave(sender As Object, e As EventArgs) Handles txtUsername.MouseLeave
        If Not txtUsername.Focused Then
            txtUsername.BorderColor = Color.FromArgb(213, 218, 223)
        End If
    End Sub

End Class