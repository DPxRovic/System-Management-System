' ==========================================
' FILENAME: /Forms/CreateAccountForm.vb
' PURPOSE: Dialog form for creating/editing user accounts
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Imports Guna.UI2.WinForms

Public Class CreateAccountForm
    Private editMode As Boolean = False
    Private userId As Integer = 0
    Private currentUserRole As String = ""

    ''' <summary>
    ''' Constructor for creating new user
    ''' </summary>
    Public Sub New(userRole As String)
        InitializeComponent()
        editMode = False
        currentUserRole = userRole
    End Sub

    ''' <summary>
    ''' Constructor for editing existing user
    ''' </summary>
    Public Sub New(user As User, userRole As String)
        InitializeComponent()
        editMode = True
        userId = user.Id
        currentUserRole = userRole
        LoadUserData(user)
    End Sub

    ''' <summary>
    ''' Form load event
    ''' </summary>
    Private Sub CreateAccountForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Setup role dropdown
            SetupRoleDropdown()

            ' Set form title
            If editMode Then
                lblTitle.Text = "Edit User Account"
                btnSave.Text = "Update"
            Else
                lblTitle.Text = "Create New Account"
                btnSave.Text = "Create"
            End If

            ' Apply theme
            ApplyTheme()

        Catch ex As Exception
            Logger.LogError("Error loading create account form", ex)
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Setup role dropdown based on current user's role
    ''' </summary>
    Private Sub SetupRoleDropdown()
        Try
            cmbRole.Items.Clear()

            If currentUserRole.ToUpper() = "SUPERADMIN" Then
                ' SuperAdmin can assign all roles
                cmbRole.Items.AddRange(New String() {"Student", "Faculty", "Admin", "SuperAdmin"})
            Else
                ' Admin can only assign Student, Faculty, Admin
                cmbRole.Items.AddRange(New String() {"Student", "Faculty", "Admin"})
            End If

            If cmbRole.Items.Count > 0 Then
                cmbRole.SelectedIndex = 0
            End If

        Catch ex As Exception
            Logger.LogError("Error setting up role dropdown", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load user data for editing
    ''' </summary>
    Private Sub LoadUserData(user As User)
        Try
            txtUsername.Text = user.Username
            txtPassword.Text = user.Password
            txtFullName.Text = user.FullName

            ' Set role
            Dim roleIndex As Integer = cmbRole.FindString(user.Role)
            If roleIndex >= 0 Then
                cmbRole.SelectedIndex = roleIndex
            End If

        Catch ex As Exception
            Logger.LogError("Error loading user data", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Apply theme to form controls
    ''' </summary>
    Private Sub ApplyTheme()
        Try
            Me.BackColor = ThemeManager.BackgroundColor
            pnlMain.FillColor = ThemeManager.WhiteColor
            ThemeManager.ApplyShadow(pnlMain, ThemeManager.ShadowDepthHeavy)

        Catch ex As Exception
            Logger.LogError("Error applying theme", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Validate form inputs
    ''' </summary>
    Private Function ValidateInputs() As Boolean
        Try
            ' Validate username
            If String.IsNullOrWhiteSpace(txtUsername.Text) Then
                MessageBox.Show("Please enter a username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtUsername.Focus()
                Return False
            End If

            If txtUsername.Text.Length < 4 Then
                MessageBox.Show("Username must be at least 4 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtUsername.Focus()
                Return False
            End If

            ' Validate password
            If String.IsNullOrWhiteSpace(txtPassword.Text) Then
                MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Focus()
                Return False
            End If

            If txtPassword.Text.Length < 6 Then
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPassword.Focus()
                Return False
            End If

            ' Validate full name
            If String.IsNullOrWhiteSpace(txtFullName.Text) Then
                MessageBox.Show("Please enter the full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtFullName.Focus()
                Return False
            End If

            ' Validate role
            If cmbRole.SelectedIndex < 0 Then
                MessageBox.Show("Please select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbRole.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Logger.LogError("Error validating inputs", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Save button click
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Validate inputs
            If Not ValidateInputs() Then
                Return
            End If

            ' Show loading cursor
            Me.Cursor = Cursors.WaitCursor
            btnSave.Enabled = False

            Dim success As Boolean = False

            If editMode Then
                ' Update existing user
                success = AdminRepository.UpdateUser(
                    userId,
                    txtUsername.Text.Trim(),
                    txtPassword.Text,
                    cmbRole.SelectedItem.ToString(),
                    txtFullName.Text.Trim()
                )
            Else
                ' Create new user
                success = AdminRepository.CreateUser(
                    txtUsername.Text.Trim(),
                    txtPassword.Text,
                    cmbRole.SelectedItem.ToString(),
                    txtFullName.Text.Trim()
                )
            End If

            If success Then
                MessageBox.Show($"User {If(editMode, "updated", "created")} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show($"Failed to {If(editMode, "update", "create")} user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Logger.LogError($"Error {If(editMode, "updating", "creating")} user", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            btnSave.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' Cancel button click
    ''' </summary>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' Show password checkbox changed
    ''' </summary>
    Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        ' Toggle password masking — when checked, show plain text; when unchecked, mask.
        Try
            If chkShowPassword.Checked Then
                ' Show plain text
                txtPassword.UseSystemPasswordChar = False
                txtPassword.PasswordChar = ChrW(0)
            Else
                ' Mask again
                txtPassword.UseSystemPasswordChar = True
                txtPassword.PasswordChar = "●"c
            End If
        Catch ex As Exception
            Logger.LogError("Error toggling show password", ex)
        End Try
    End Sub

End Class