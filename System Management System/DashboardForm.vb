' ==========================================
' FILENAME: /Forms/DashboardForm.vb
' PURPOSE: Main dashboard form with role-based navigation - MERGED VERSION
' AUTHOR: System
' DATE: 2025-10-17
' Edited By Rovic
' For Future users please do not remove this header
' ==========================================
Imports Guna.UI2.WinForms
Imports System.Data

Public Class DashboardForm
    Private currentUser As User
    Private activeButton As Guna.UI2.WinForms.Guna2Button = Nothing
    Private currentForm As Form = Nothing

    ''' <summary>
    ''' Constructor with user parameter
    ''' </summary>
    Public Sub New(user As User)
        InitializeComponent()
        currentUser = user
    End Sub

    ''' <summary>
    ''' Form load event - Setup dashboard based on user role
    ''' </summary>
    Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Display user information
            lblWelcome.Text = $"Welcome, {currentUser.FullName}"
            lblRole.Text = $"Role: {currentUser.Role}"

            ' Setup navigation based on role
            SetupNavigation()

            ' Set button permissions based on user role
            SetUserPermissions()

            ' Enable auto-scaling for child forms
            Me.AutoScaleMode = AutoScaleMode.Dpi

            ' Load initial dashboard content
            LoadDashboardContent()

            Logger.LogInfo($"Dashboard loaded for user: {currentUser.Username}")

        Catch ex As Exception
            Logger.LogError("Error loading dashboard", ex)
            MessageBox.Show($"Error loading dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Setup navigation buttons based on user role
    ''' </summary>
    Private Sub SetupNavigation()
        Try
            ' Hide all navigation buttons initially
            btnDashboard.Visible = True
            btnStudents.Visible = False
            btnCourses.Visible = False
            btnAttendance.Visible = False
            btnFaculty.Visible = False
            btnUsers.Visible = False
            btnReports.Visible = False
            btnSettings.Visible = False

            ' Show navigation based on role
            Select Case currentUser.Role.ToUpper()
                Case "SUPERADMIN"
                    ' SuperAdmin sees everything
                    btnStudents.Visible = True
                    btnCourses.Visible = True
                    btnAttendance.Visible = True
                    btnFaculty.Visible = True
                    btnUsers.Visible = True
                    btnReports.Visible = True
                    btnSettings.Visible = True

                Case "ADMIN"
                    ' Admin sees most features except some system settings
                    btnStudents.Visible = True
                    btnCourses.Visible = True
                    btnAttendance.Visible = True
                    btnFaculty.Visible = True
                    btnUsers.Visible = True
                    btnReports.Visible = True
                    btnSettings.Visible = True

                Case "FACULTY"
                    ' Faculty sees courses, attendance, and reports
                    btnCourses.Visible = True
                    btnAttendance.Visible = True
                    btnReports.Visible = True

                Case "STUDENT"
                    ' Student sees limited options
                    btnAttendance.Visible = True
                    btnReports.Visible = True
            End Select

            ' Set Dashboard as active by default
            SetActiveButton(btnDashboard)

        Catch ex As Exception
            Logger.LogError("Error setting up navigation", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set button permissions based on user role
    ''' </summary>
    Private Sub SetUserPermissions()
        Try
            Select Case currentUser.Role.ToLower()
                Case "admin", "superadmin"
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
    ''' Load dashboard content based on role with enhanced statistics
    ''' </summary>
    Private Sub LoadDashboardContent()
        Try
            ' Clear existing content
            pnlContent.Controls.Clear()

            ' Dispose of current form if exists
            If currentForm IsNot Nothing Then
                currentForm.Close()
                currentForm.Dispose()
                currentForm = Nothing
            End If

            ' Update page title if it exists
            If lblPageTitle IsNot Nothing Then
                lblPageTitle.Text = "Dashboard"
            End If

            ' Create dashboard summary panel
            Dim summaryPanel As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White,
                .Padding = New Padding(20),
                .AutoScroll = True
            }

            ' Add summary labels
            Dim yPos As Integer = 20

            Select Case currentUser.Role.ToUpper()
                Case "SUPERADMIN", "ADMIN"
                    ' Show statistics for admin users
                    AddStatCard(summaryPanel, "Total Students", GetStudentCount().ToString(), 20, yPos, Color.FromArgb(52, 152, 219))
                    AddStatCard(summaryPanel, "Total Courses", GetCourseCount().ToString(), 240, yPos, Color.FromArgb(46, 139, 87))
                    AddStatCard(summaryPanel, "Total Faculty", GetFacultyCount().ToString(), 460, yPos, Color.FromArgb(155, 89, 182))
                    AddStatCard(summaryPanel, "Active Users", GetActiveUserCount().ToString(), 680, yPos, Color.FromArgb(243, 156, 18))

                    yPos += 170

                    ' Add attendance statistics section
                    AddSectionLabel(summaryPanel, "Today's Attendance Overview", 20, yPos)
                    yPos += 40

                    AddStatCard(summaryPanel, "Today's Attendance", GetTodayAttendanceCount().ToString(), 20, yPos, Color.FromArgb(52, 152, 219))

                    yPos += 170

                    ' Add quick action buttons
                    AddSectionLabel(summaryPanel, "Quick Actions", 20, yPos)
                    yPos += 40

                    AddQuickActionButton(summaryPanel, "View Reports", 20, yPos, Sub() btnReports.PerformClick())
                    AddQuickActionButton(summaryPanel, "Take Attendance", 240, yPos, Sub() btnAttendance.PerformClick())
                    AddQuickActionButton(summaryPanel, "Manage Users", 460, yPos, Sub() btnUsers.PerformClick())

                Case "FACULTY"
                    ' Show faculty-specific statistics
                    AddStatCard(summaryPanel, "My Courses", GetFacultyCourseCount().ToString(), 20, yPos, Color.FromArgb(46, 139, 87))
                    AddStatCard(summaryPanel, "Today's Classes", "0", 240, yPos, Color.FromArgb(52, 152, 219))
                    AddStatCard(summaryPanel, "Total Students", GetStudentCount().ToString(), 460, yPos, Color.FromArgb(155, 89, 182))

                    yPos += 170

                    AddSectionLabel(summaryPanel, "Quick Actions", 20, yPos)
                    yPos += 40

                    AddQuickActionButton(summaryPanel, "View Reports", 20, yPos, Sub() btnReports.PerformClick())
                    AddQuickActionButton(summaryPanel, "Take Attendance", 240, yPos, Sub() btnAttendance.PerformClick())

                Case "STUDENT"
                    ' Show student-specific statistics
                    AddStatCard(summaryPanel, "My Courses", "0", 20, yPos, Color.FromArgb(46, 139, 87))
                    AddStatCard(summaryPanel, "Attendance Rate", "0%", 240, yPos, Color.FromArgb(52, 152, 219))
                    AddStatCard(summaryPanel, "Pending Tasks", "0", 460, yPos, Color.FromArgb(155, 89, 182))

                    yPos += 170

                    AddQuickActionButton(summaryPanel, "View My Reports", 20, yPos, Sub() btnReports.PerformClick())
            End Select

            pnlContent.Controls.Add(summaryPanel)

        Catch ex As Exception
            Logger.LogError("Error loading dashboard content", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Add a statistics card to the panel
    ''' </summary>
    Private Sub AddStatCard(parent As Panel, title As String, value As String, x As Integer, y As Integer, color As Color)
        Dim card As New Guna.UI2.WinForms.Guna2Panel With {
            .Size = New Size(200, 140),
            .Location = New Point(x, y),
            .BorderRadius = 10,
            .FillColor = color
        }
        card.ShadowDecoration.Enabled = True
        card.ShadowDecoration.Depth = 5

        Dim lblTitle As New Label With {
            .Text = title,
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.White,
            .Location = New Point(15, 20),
            .Size = New Size(170, 30),
            .BackColor = Color.Transparent
        }

        Dim lblValue As New Label With {
            .Text = value,
            .Font = New Font("Segoe UI", 28, FontStyle.Bold),
            .ForeColor = Color.White,
            .Location = New Point(15, 55),
            .Size = New Size(170, 60),
            .BackColor = Color.Transparent,
            .TextAlign = ContentAlignment.MiddleLeft
        }

        card.Controls.Add(lblTitle)
        card.Controls.Add(lblValue)
        parent.Controls.Add(card)
    End Sub

    ''' <summary>
    ''' Add a section label to the panel
    ''' </summary>
    Private Sub AddSectionLabel(parent As Panel, text As String, x As Integer, y As Integer)
        Dim lblSection As New Label With {
            .Text = text,
            .Font = New Font("Segoe UI", 14, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .Location = New Point(x, y),
            .AutoSize = True
        }
        parent.Controls.Add(lblSection)
    End Sub

    ''' <summary>
    ''' Add a quick action button to the panel
    ''' </summary>
    Private Sub AddQuickActionButton(parent As Panel, text As String, x As Integer, y As Integer, onClick As Action)
        Dim btn As New Guna.UI2.WinForms.Guna2Button With {
            .Text = text,
            .Size = New Size(200, 60),
            .Location = New Point(x, y),
            .BorderRadius = 10,
            .FillColor = Color.FromArgb(46, 139, 87),
            .Font = New Font("Segoe UI", 10, FontStyle.Bold),
            .ForeColor = Color.White
        }
        btn.ShadowDecoration.Enabled = True
        btn.ShadowDecoration.Depth = 5

        AddHandler btn.Click, Sub(s, e) onClick()

        parent.Controls.Add(btn)
    End Sub

    ''' <summary>
    ''' Get total student count
    ''' </summary>
    Private Function GetStudentCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM students WHERE status = 'Active'"
            Return Convert.ToInt32(DatabaseHandler.ExecuteScalar(query))
        Catch ex As Exception
            Logger.LogError("Error getting student count", ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Get total course count
    ''' </summary>
    Private Function GetCourseCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM courses WHERE status = 'Active'"
            Return Convert.ToInt32(DatabaseHandler.ExecuteScalar(query))
        Catch ex As Exception
            Logger.LogError("Error getting course count", ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Get total faculty count
    ''' </summary>
    Private Function GetFacultyCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM courses WHERE status = 'Active'"
            Return Convert.ToInt32(DatabaseHandler.ExecuteScalar(query))
        Catch ex As Exception
            Logger.LogError("Error getting faculty count", ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Get today's attendance count
    ''' </summary>
    Private Function GetTodayAttendanceCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM attendance WHERE date = CURDATE()"
            Return Convert.ToInt32(DatabaseHandler.ExecuteScalar(query))
        Catch ex As Exception
            Logger.LogError("Error getting today's attendance count", ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Get active user count
    ''' </summary>
    Private Function GetActiveUserCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM users"
            Return Convert.ToInt32(DatabaseHandler.ExecuteScalar(query))
        Catch ex As Exception
            Logger.LogError("Error getting active user count", ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Get faculty course count
    ''' </summary>
    Private Function GetFacultyCourseCount() As Integer
        Try
            Dim query As String = "SELECT COUNT(*) FROM courses WHERE faculty_id = @faculty_id AND status = 'Active'"
            Dim params As New Dictionary(Of String, Object) From {
                {"@faculty_id", currentUser.Id}
            }
            Return Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, params))
        Catch ex As Exception
            Logger.LogError("Error getting faculty course count", ex)
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Set active navigation button
    ''' </summary>
    Private Sub SetActiveButton(btn As Guna.UI2.WinForms.Guna2Button)
        ' Reset previous active button
        If activeButton IsNot Nothing Then
            activeButton.FillColor = Color.Transparent
            activeButton.ForeColor = Color.FromArgb(44, 62, 80)
        End If

        ' Set new active button
        activeButton = btn
        activeButton.FillColor = Color.FromArgb(26, 188, 156)
        activeButton.ForeColor = Color.White
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
            If lblPageTitle IsNot Nothing Then
                lblPageTitle.Text = pageTitle
            End If

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
    ''' Loads a form into the content panel - Alternative method
    ''' </summary>
    ''' <param name="childForm">Form to load</param>
    Private Sub LoadForm(childForm As Form)
        Try
            ' Clear existing content
            pnlContent.Controls.Clear()

            ' Configure child form
            childForm.TopLevel = False
            childForm.FormBorderStyle = FormBorderStyle.None
            childForm.Dock = DockStyle.Fill

            ' Add form to content panel
            pnlContent.Controls.Add(childForm)
            childForm.Show()

            Logger.LogInfo($"Form {childForm.Name} loaded into dashboard")
        Catch ex As Exception
            Logger.LogError("Error loading form into dashboard", ex)
            MessageBox.Show($"Error loading module: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Navigation button click events
    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        SetActiveButton(btnDashboard)
        LoadDashboardContent()
    End Sub

    ''' <summary>
    ''' Students button click - NOW LOADS DEDICATED STUDENT MANAGEMENT
    ''' </summary>
    Private Sub btnStudents_Click(sender As Object, e As EventArgs) Handles btnStudents.Click
        Try
            SetActiveButton(btnStudents)
            ' Check if user has permission to access student management
            If currentUser.Role.ToUpper() = "ADMIN" OrElse currentUser.Role.ToUpper() = "SUPERADMIN" OrElse currentUser.Role.ToUpper() = "FACULTY" Then
                ' Open dedicated StudentManagementForm
                Dim studentMgmtForm As New StudentManagementForm(currentUser)
                LoadChildForm(studentMgmtForm, "Student Management")
                Logger.LogInfo($"Student Management opened by {currentUser.Username}")
            Else
                MessageBox.Show("You don't have permission to access student management.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            Logger.LogError("Error opening student management", ex)
            MessageBox.Show($"Error loading student management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCourses_Click(sender As Object, e As EventArgs) Handles btnCourses.Click
        Try
            SetActiveButton(btnCourses)

            ' Check if user has permission to access course management
            If currentUser.Role.ToUpper() = "ADMIN" OrElse currentUser.Role.ToUpper() = "SUPERADMIN" OrElse currentUser.Role.ToUpper() = "FACULTY" Then
                ' Open AdminForm focused on Courses tab
                Dim adminForm As New AdminForm(currentUser)
                LoadChildForm(adminForm, "Course Management")

                ' Switch to Courses tab after form loads
                adminForm.SwitchToTab("Courses")

                Logger.LogInfo($"Course management opened by {currentUser.Username}")
            Else
                MessageBox.Show("You don't have permission to access course management.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            Logger.LogError("Error opening course management", ex)
            MessageBox.Show($"Error loading course management: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAttendance_Click(sender As Object, e As EventArgs) Handles btnAttendance.Click
        Try
            SetActiveButton(btnAttendance)
            ' Create and load Attendance form
            Dim attendanceForm As New AttendanceForm(currentUser)
            LoadChildForm(attendanceForm, "Attendance Management")

            Logger.LogInfo("Attendance form loaded successfully")
        Catch ex As Exception
            Logger.LogError("Error opening attendance form", ex)
            MessageBox.Show($"Error loading attendance form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnFaculty_Click(sender As Object, e As EventArgs) Handles btnFaculty.Click
        SetActiveButton(btnFaculty)
        LoadForm(New FacultyForm(currentUser))
    End Sub

    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        Try
            Dim role = If(currentUser?.Role, "").ToUpperInvariant()

            If role = "ADMIN" OrElse role = "SUPERADMIN" Then
                ' Open AdminForm inside the content panel
                Dim adminForm As New AdminForm(currentUser)
                LoadChildForm(adminForm, "Administration")
                SetActiveButton(btnUsers)
                Logger.LogInfo($"Admin panel opened by {currentUser.Username}")
            Else
                SetActiveButton(btnUsers)
                ShowMessage("User Management", "User management module coming soon...")
            End If
        Catch ex As Exception
            Logger.LogError("Error opening admin/users panel", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        Try
            SetActiveButton(btnReports)

            ' Create and load Reports form
            Dim reportsForm As New ReportsForm(currentUser)
            LoadChildForm(reportsForm, "Attendance Reports")

            Logger.LogInfo("Reports form loaded successfully")

        Catch ex As Exception
            Logger.LogError("Error opening reports form", ex)
            MessageBox.Show($"Error loading reports: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        SetActiveButton(btnSettings)
        ShowMessage("Settings", "Settings module coming soon...")
    End Sub

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
            Me.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Helper method to show messages
    ''' </summary>
    Private Sub ShowMessage(title As String, message As String)
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information)
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