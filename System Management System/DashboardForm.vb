' ==========================================
' FILENAME: /Forms/DashboardForm.vb
' PURPOSE: Main dashboard form with navigation - UPDATED
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Public Class DashboardForm
    Private currentUser As User
    Private currentForm As Form = Nothing

    ''' <summary>
    ''' Constructor with user parameter
    ''' </summary>
    Public Sub New(user As User)
        InitializeComponent()
        currentUser = user
    End Sub

    ''' <summary>
    ''' Form load event
    ''' </summary>
    Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Display user information
            lblWelcome.Text = $"Welcome, {currentUser.FullName}"
            lblRole.Text = $"Role: {currentUser.Role}"

            ' Set button permissions based on user role
            SetUserPermissions()

            ' Enable auto-scaling for child forms
            Me.AutoScaleMode = AutoScaleMode.Dpi

            ' Load default dashboard content
            LoadDashboard()

            Logger.LogInfo($"Dashboard loaded for user: {currentUser.Username}")
        Catch ex As Exception
            Logger.LogError("Error loading dashboard", ex)
            MessageBox.Show($"Error loading dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Set button permissions based on user role
    ''' </summary>
    Private Sub SetUserPermissions()
        Try
            Select Case currentUser.Role.ToLower()
                Case "admin"
                    ' Admin has access to everything
                    btnDashboard.Enabled = True
                    btnStudents.Enabled = True
                    btnCourses.Enabled = True
                    btnAttendance.Enabled = True
                    btnFaculty.Enabled = True
                    btnUsers.Enabled = True
                    btnReports.Enabled = True
                    btnSettings.Enabled = True

                Case "teacher", "faculty"
                    ' Teachers have limited access
                    btnDashboard.Enabled = True
                    btnStudents.Enabled = True
                    btnCourses.Enabled = True
                    btnAttendance.Enabled = True
                    btnFaculty.Enabled = False
                    btnUsers.Enabled = False
                    btnReports.Enabled = True
                    btnSettings.Enabled = False

                Case "staff"
                    ' Staff have basic access
                    btnDashboard.Enabled = True
                    btnStudents.Enabled = True
                    btnCourses.Enabled = False
                    btnAttendance.Enabled = True
                    btnFaculty.Enabled = False
                    btnUsers.Enabled = False
                    btnReports.Enabled = False
                    btnSettings.Enabled = False

                Case Else
                    ' Default: minimal access
                    btnDashboard.Enabled = True
                    btnStudents.Enabled = False
                    btnCourses.Enabled = False
                    btnAttendance.Enabled = True
                    btnFaculty.Enabled = False
                    btnUsers.Enabled = False
                    btnReports.Enabled = False
                    btnSettings.Enabled = False
            End Select

        Catch ex As Exception
            Logger.LogError("Error setting user permissions", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load a child form into the content panel
    ''' </summary>
    Private Sub LoadChildForm(childForm As Form, pageTitle As String)
        Try
            ' Dispose of current form if exists
            If currentForm IsNot Nothing Then
                currentForm.Close()
                currentForm.Dispose()
                currentForm = Nothing
            End If

            ' Clear the panel
            pnlContent.Controls.Clear()

            ' Update page title
            lblPageTitle.Text = pageTitle

            ' Configure the child form for responsive behavior
            childForm.TopLevel = False
            childForm.FormBorderStyle = FormBorderStyle.None
            childForm.Dock = DockStyle.Fill
            childForm.AutoScaleMode = AutoScaleMode.Dpi
            childForm.AutoSize = False

            ' Enable anchor/dock settings to work properly
            childForm.Width = pnlContent.Width
            childForm.Height = pnlContent.Height

            ' Add to panel and show
            pnlContent.Controls.Add(childForm)
            childForm.BringToFront()
            childForm.Show()

            ' Store reference
            currentForm = childForm

            ' Reset all button colors
            ResetButtonColors()

            ' Force layout update
            pnlContent.PerformLayout()
            childForm.PerformLayout()

        Catch ex As Exception
            Logger.LogError("Error loading child form", ex)
            MessageBox.Show($"Error loading page: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Reset all navigation button colors
    ''' </summary>
    Private Sub ResetButtonColors()
        btnDashboard.FillColor = Color.Transparent
        btnStudents.FillColor = Color.Transparent
        btnCourses.FillColor = Color.Transparent
        btnAttendance.FillColor = Color.Transparent
        btnFaculty.FillColor = Color.Transparent
        btnUsers.FillColor = Color.Transparent
        btnReports.FillColor = Color.Transparent
        btnSettings.FillColor = Color.Transparent

        ' Reset text colors
        btnDashboard.ForeColor = Color.White
        btnStudents.ForeColor = Color.White
        btnCourses.ForeColor = Color.White
        btnAttendance.ForeColor = Color.White
        btnFaculty.ForeColor = Color.White
        btnUsers.ForeColor = Color.White
        btnReports.ForeColor = Color.White
        btnSettings.ForeColor = Color.White
    End Sub

    ''' <summary>
    ''' Highlight the active button
    ''' </summary>
    Private Sub HighlightButton(btn As Guna.UI2.WinForms.Guna2Button)
        ResetButtonColors()
        btn.FillColor = Color.FromArgb(26, 188, 156)
        btn.ForeColor = Color.White
    End Sub

    ''' <summary>
    ''' Dashboard button click
    ''' </summary>
    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        LoadDashboard()
        HighlightButton(btnDashboard)
    End Sub

    ''' <summary>
    ''' Load dashboard content
    ''' </summary>
    Private Sub LoadDashboard()
        Try
            ' For now, create a simple dashboard view
            ' You can replace this with an actual DashboardContentForm later
            pnlContent.Controls.Clear()

            If currentForm IsNot Nothing Then
                currentForm.Close()
                currentForm.Dispose()
                currentForm = Nothing
            End If

            lblPageTitle.Text = "Dashboard"
            HighlightButton(btnDashboard)

            ' Create a simple welcome panel
            Dim welcomeLabel As New Label With {
                .Text = $"Welcome to Student Management System, {currentUser.FullName}!",
                .Font = New Font("Segoe UI", 16, FontStyle.Bold),
                .ForeColor = Color.FromArgb(44, 62, 80),
                .Dock = DockStyle.Top,
                .Height = 100,
                .TextAlign = ContentAlignment.MiddleCenter
            }
            pnlContent.Controls.Add(welcomeLabel)

        Catch ex As Exception
            Logger.LogError("Error loading dashboard", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Students button click
    ''' </summary>
    Private Sub btnStudents_Click(sender As Object, e As EventArgs) Handles btnStudents.Click
        Try
            ' You need to create StudentManagementForm
            ' LoadChildForm(New StudentManagementForm(), "Students Management")
            HighlightButton(btnStudents)
            MessageBox.Show("Students Management form coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError("Error opening students form", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Courses button click
    ''' </summary>
    Private Sub btnCourses_Click(sender As Object, e As EventArgs) Handles btnCourses.Click
        Try
            ' You need to create CoursesManagementForm
            ' LoadChildForm(New CoursesManagementForm(), "Courses Management")
            HighlightButton(btnCourses)
            MessageBox.Show("Courses Management form coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError("Error opening courses form", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Attendance button click - UPDATED TO USE NEW FORM
    ''' </summary>
    Private Sub btnAttendance_Click(sender As Object, e As EventArgs) Handles btnAttendance.Click
        Try
            ' IMPORTANT: This creates a NEW instance of AttendanceForm with the updated designer
            Dim attendanceForm As New AttendanceForm(currentUser)
            LoadChildForm(attendanceForm, "Attendance Management")
            HighlightButton(btnAttendance)

            Logger.LogInfo("Attendance form loaded - using updated designer")
        Catch ex As Exception
            Logger.LogError("Error opening attendance form", ex)
            MessageBox.Show($"Error loading attendance form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Faculty button click
    ''' </summary>
    Private Sub btnFaculty_Click(sender As Object, e As EventArgs) Handles btnFaculty.Click
        Try
            ' You need to create FacultyManagementForm
            ' LoadChildForm(New FacultyManagementForm(), "Faculty Management")
            HighlightButton(btnFaculty)
            MessageBox.Show("Faculty Management form coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError("Error opening faculty form", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Users button click
    ''' </summary>
    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        Try
            ' You need to create UserManagementForm
            ' LoadChildForm(New UserManagementForm(), "User Management")
            HighlightButton(btnUsers)
            MessageBox.Show("User Management form coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError("Error opening users form", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Reports button click
    ''' </summary>
    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        Try
            ' You need to create ReportsForm
            ' LoadChildForm(New ReportsForm(), "Reports")
            HighlightButton(btnReports)
            MessageBox.Show("Reports form coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError("Error opening reports form", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Settings button click
    ''' </summary>
    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Try
            ' You need to create SettingsForm
            ' LoadChildForm(New SettingsForm(), "Settings")
            HighlightButton(btnSettings)
            MessageBox.Show("Settings form coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            Logger.LogError("Error opening settings form", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Logout button click
    ''' </summary>
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Try
            Dim result As DialogResult = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Logger.LogInfo($"User {currentUser.Username} logged out")

                ' Close current form
                If currentForm IsNot Nothing Then
                    currentForm.Close()
                    currentForm.Dispose()
                End If

                ' Show login form
                Dim loginForm As New LoginForm()
                loginForm.Show()

                ' Close this form
                Me.Close()
            End If

        Catch ex As Exception
            Logger.LogError("Error during logout", ex)
            MessageBox.Show($"Error during logout: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Close button click
    ''' </summary>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Dim result As DialogResult = MessageBox.Show(
                "Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Application.Exit()
            End If
        Catch ex As Exception
            Logger.LogError("Error closing application", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Maximize/Restore button click
    ''' </summary>
    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
        Try
            If Me.WindowState = FormWindowState.Normal Then
                Me.WindowState = FormWindowState.Maximized
                btnMaximize.Text = "❐"
            Else
                Me.WindowState = FormWindowState.Normal
                btnMaximize.Text = "□"
            End If
        Catch ex As Exception
            Logger.LogError("Error maximizing window", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Minimize button click
    ''' </summary>
    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Try
            Me.WindowState = FormWindowState.Minimized
        Catch ex As Exception
            Logger.LogError("Error minimizing window", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Form closing event
    ''' </summary>
    Private Sub DashboardForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            ' Clean up current form
            If currentForm IsNot Nothing Then
                currentForm.Close()
                currentForm.Dispose()
                currentForm = Nothing
            End If

            Logger.LogInfo("Dashboard form closing")
        Catch ex As Exception
            Logger.LogError("Error during form closing", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handle window resize to ensure child forms adjust properly
    ''' </summary>
    Private Sub DashboardForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            ' Force child form to resize with the content panel
            If currentForm IsNot Nothing AndAlso pnlContent.Controls.Contains(currentForm) Then
                currentForm.Width = pnlContent.Width
                currentForm.Height = pnlContent.Height
                currentForm.Refresh()
            End If
        Catch ex As Exception
            ' Silently handle resize errors
            Logger.LogError("Error during form resize", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handle content panel resize
    ''' </summary>
    Private Sub pnlContent_Resize(sender As Object, e As EventArgs) Handles pnlContent.Resize
        Try
            ' Ensure child form adjusts to panel size
            If currentForm IsNot Nothing Then
                currentForm.Size = pnlContent.ClientSize
                currentForm.Invalidate()
            End If
        Catch ex As Exception
            ' Silently handle resize errors
        End Try
    End Sub

End Class