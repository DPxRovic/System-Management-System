' ==========================================
' FILENAME: /Forms/AdminForm.vb
' PURPOSE: Admin management module with tabs for Users and Courses
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Imports System.Data
Imports Guna.UI2.WinForms

Public Class AdminForm
    Private currentUser As User
    Private isSuperAdmin As Boolean = False

    ''' <summary>
    ''' Constructor with user parameter
    ''' </summary>
    Public Sub New(user As User)
        InitializeComponent()
        currentUser = user
        isSuperAdmin = (user.Role.ToUpper() = "SUPERADMIN")
    End Sub

    ''' <summary>
    ''' Form load event
    ''' </summary>
    Private Sub AdminForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Setup based on user role
            SetupPermissions()

            ' Load data for both tabs
            LoadUsersData()
            LoadCoursesData()

            ' Apply theme
            ApplyTheme()

            ' Apply slide-in animation
            ThemeManager.SlideIn(pnlMain, "LEFT", 300)

            Logger.LogInfo($"Admin form loaded for {currentUser.Username}")

        Catch ex As Exception
            Logger.LogError("Error loading admin form", ex)
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Setup permissions based on user role
    ''' </summary>
    Private Sub SetupPermissions()
        Try
            If isSuperAdmin Then
                ' SuperAdmin has full access
                btnAddUser.Visible = True
                btnEditUser.Visible = True
                btnDeleteUser.Visible = True
                btnArchiveUser.Visible = True

                btnAddCourse.Visible = True
                btnEditCourse.Visible = True
                btnDeleteCourse.Visible = True
                btnArchiveCourse.Visible = True

                lblPermissionNote.Visible = False
            Else
                ' Regular Admin has limited access
                btnAddUser.Visible = False
                btnEditUser.Visible = False
                btnDeleteUser.Visible = False
                btnArchiveUser.Visible = True ' Can archive only

                btnAddCourse.Visible = False
                btnEditCourse.Visible = False
                btnDeleteCourse.Visible = False
                btnArchiveCourse.Visible = True ' Can archive only

                lblPermissionNote.Visible = True
                lblPermissionNote.Text = "⚠ Limited Access: Only SuperAdmin can Add/Edit/Delete"
            End If

        Catch ex As Exception
            Logger.LogError("Error setting up permissions", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Apply theme to form
    ''' </summary>
    Private Sub ApplyTheme()
        Try
            ThemeManager.StyleDataGridView(dgvUsers)
            ThemeManager.StyleDataGridView(dgvCourses)
        Catch ex As Exception
            Logger.LogError("Error applying theme", ex)
        End Try
    End Sub

#Region "Users Tab"

    ''' <summary>
    ''' Load users data into grid
    ''' </summary>
    Private Sub LoadUsersData()
        Try
            Dim includeArchived As Boolean = chkShowArchivedUsers.Checked
            Dim dt As DataTable = AdminRepository.GetAllUsers(includeArchived)

            dgvUsers.DataSource = dt

            ' Format grid columns
            If dgvUsers.Columns.Count > 0 Then
                If dgvUsers.Columns.Contains("id") Then
                    dgvUsers.Columns("id").HeaderText = "ID"
                    dgvUsers.Columns("id").Width = 50
                End If
                If dgvUsers.Columns.Contains("username") Then
                    dgvUsers.Columns("username").HeaderText = "Username"
                    dgvUsers.Columns("username").Width = 120
                End If
                If dgvUsers.Columns.Contains("password") Then
                    dgvUsers.Columns("password").HeaderText = "Password"
                    dgvUsers.Columns("password").Width = 100
                End If
                If dgvUsers.Columns.Contains("role") Then
                    dgvUsers.Columns("role").HeaderText = "Role"
                    dgvUsers.Columns("role").Width = 100
                End If
                If dgvUsers.Columns.Contains("fullname") Then
                    dgvUsers.Columns("fullname").HeaderText = "Full Name"
                    dgvUsers.Columns("fullname").Width = 200
                End If
                If dgvUsers.Columns.Contains("created_at") Then
                    dgvUsers.Columns("created_at").HeaderText = "Created At"
                    dgvUsers.Columns("created_at").Width = 150
                End If
                If dgvUsers.Columns.Contains("is_archived") Then
                    dgvUsers.Columns("is_archived").HeaderText = "Archived"
                    dgvUsers.Columns("is_archived").Width = 80
                End If
            End If

            ' Update count label
            lblUsersCount.Text = $"Total Users: {dt.Rows.Count}"

            ' Prevent unintended automatic selection (user clicked Archive without selecting)
            Try
                dgvUsers.ClearSelection()
                If dgvUsers.Rows.Count > 0 Then
                    dgvUsers.CurrentCell = Nothing
                End If
            Catch ex As Exception
                ' swallow any DataGridView focus exceptions
            End Try

        Catch ex As Exception
            Logger.LogError("Error loading users data", ex)
            MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' When user toggles Show Archived — reload users
    ''' </summary>
    Private Sub chkShowArchivedUsers_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowArchivedUsers.CheckedChanged
        Try
            LoadUsersData()
        Catch ex As Exception
            Logger.LogError("Error handling chkShowArchivedUsers_CheckedChanged", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Add user button click
    ''' </summary>
    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Try
            Dim createForm As New CreateAccountForm(currentUser.Role)

            If createForm.ShowDialog() = DialogResult.OK Then
                LoadUsersData() ' Refresh grid
                MessageBox.Show("User created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            Logger.LogError("Error opening create user form", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Edit user button click
    ''' </summary>
    Private Sub btnEditUser_Click(sender As Object, e As EventArgs) Handles btnEditUser.Click
        Try
            If dgvUsers.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a user to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim userId As Integer = Convert.ToInt32(dgvUsers.SelectedRows(0).Cells("id").Value)
            Dim user As User = AdminRepository.GetUserById(userId)

            If user IsNot Nothing Then
                Dim editForm As New CreateAccountForm(user, currentUser.Role)
                If editForm.ShowDialog() = DialogResult.OK Then
                    LoadUsersData()
                    MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Selected user not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Logger.LogError("Error editing user", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Delete user button click
    ''' </summary>
    Private Sub btnDeleteUser_Click(sender As Object, e As EventArgs) Handles btnDeleteUser.Click
        Try
            If dgvUsers.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a user to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim userId As Integer = Convert.ToInt32(dgvUsers.SelectedRows(0).Cells("id").Value)
            Dim result = MessageBox.Show("Are you sure you want to permanently delete the selected user?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                AdminRepository.DeleteUser(userId)
                LoadUsersData()
                MessageBox.Show("User deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            Logger.LogError("Error deleting user", ex)
            MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Archive/unarchive selected user (with user confirmation)
    ''' </summary>
    Private Sub btnArchiveUser_Click(sender As Object, e As EventArgs) Handles btnArchiveUser.Click
        Try
            If dgvUsers.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a user to archive/unarchive.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row = dgvUsers.SelectedRows(0)
            Dim userId As Integer = Convert.ToInt32(row.Cells("id").Value)
            Dim isArchived As Boolean = False

            If dgvUsers.Columns.Contains("is_archived") Then
                Dim val = row.Cells("is_archived").Value
                isArchived = If(IsDBNull(val), False, Convert.ToBoolean(val))
            End If

            Dim usernameDisplay As String = If(dgvUsers.Columns.Contains("username"), row.Cells("username").Value.ToString(), $"ID {userId}")
            Dim prompt As String = If(isArchived,
                                      $"Do you want to restore user '{usernameDisplay}'?",
                                      $"Are you sure you want to archive user '{usernameDisplay}'?")

            Dim result = MessageBox.Show(prompt, If(isArchived, "Confirm Restore", "Confirm Archive"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result <> DialogResult.Yes Then
                Return
            End If

            Dim success As Boolean = False
            If isArchived Then
                success = AdminRepository.RestoreUser(userId)
                If success Then MessageBox.Show($"User '{usernameDisplay}' restored.", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                success = AdminRepository.ArchiveUser(userId)
                If success Then MessageBox.Show($"User '{usernameDisplay}' archived.", "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If success Then
                LoadUsersData()
            Else
                MessageBox.Show("Operation failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Logger.LogError("Error archiving/unarchiving user", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Courses Tab"

    ''' <summary>
    ''' Load courses - include archived when checkbox checked
    ''' </summary>
    Private Sub LoadCoursesData()
        Try
            Dim includeArchived As Boolean = chkShowArchivedCourses.Checked
            Dim dt As DataTable = AdminRepository.GetAllCourses(includeArchived)
            dgvCourses.DataSource = dt

            ' Simple column formatting if present
            If dgvCourses.Columns.Count > 0 Then
                If dgvCourses.Columns.Contains("id") Then dgvCourses.Columns("id").Width = 50
                If dgvCourses.Columns.Contains("status") Then
                    dgvCourses.Columns("status").HeaderText = "Status"
                    dgvCourses.Columns("status").Width = 100
                End If
            End If

            lblCoursesCount.Text = $"Total Courses: {dt.Rows.Count}"

            ' Prevent unintended automatic selection
            Try
                dgvCourses.ClearSelection()
                If dgvCourses.Rows.Count > 0 Then
                    dgvCourses.CurrentCell = Nothing
                End If
            Catch ex As Exception
                ' swallow any DataGridView focus exceptions
            End Try

        Catch ex As Exception
            Logger.LogError("Error loading courses", ex)
        End Try
    End Sub

    ''' <summary>
    ''' When user toggles Show Archived in Courses — reload courses
    ''' </summary>
    Private Sub chkShowArchivedCourses_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowArchivedCourses.CheckedChanged
        Try
            LoadCoursesData()
        Catch ex As Exception
            Logger.LogError("Error handling chkShowArchivedCourses_CheckedChanged", ex)
        End Try
    End Sub

    Private Sub btnAddCourse_Click(sender As Object, e As EventArgs) Handles btnAddCourse.Click
        MessageBox.Show("Add Course - not implemented in this build", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnEditCourse_Click(sender As Object, e As EventArgs) Handles btnEditCourse.Click
        MessageBox.Show("Edit Course - not implemented in this build", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnDeleteCourse_Click(sender As Object, e As EventArgs) Handles btnDeleteCourse.Click
        MessageBox.Show("Delete Course - not implemented in this build", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnArchiveCourse_Click(sender As Object, e As EventArgs) Handles btnArchiveCourse.Click
        MessageBox.Show("Archive Course - not implemented in this build", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub lblPermissionNote_Click(sender As Object, e As EventArgs) Handles lblPermissionNote.Click

    End Sub

#End Region

End Class