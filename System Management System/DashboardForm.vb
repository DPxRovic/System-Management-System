' ==========================================
' FILENAME: /Forms/DashboardForm.vb
' PURPOSE: Main dashboard form with role-based navigation
' AUTHOR: System
' DATE: 2025-10-14
' ==========================================
Imports Guna.UI2.WinForms
Imports System.Data

Public Class DashboardForm
    Private currentUser As User
    Private activeButton As Guna.UI2.WinForms.Guna2Button = Nothing

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
    ''' Load dashboard content based on role
    ''' </summary>
    Private Sub LoadDashboardContent()
        Try
            ' Clear existing content
            pnlContent.Controls.Clear()

            ' Create dashboard summary panel
            Dim summaryPanel As New Panel With {
                .Dock = DockStyle.Fill,
                .BackColor = Color.White,
                .Padding = New Padding(20)
            }

            ' Add summary labels
            Dim yPos As Integer = 20

            Select Case currentUser.Role.ToUpper()
                Case "SUPERADMIN", "ADMIN"
                    ' Show statistics for admin users
                    AddStatCard(summaryPanel, "Total Students", GetStudentCount().ToString(), 20, yPos)
                    AddStatCard(summaryPanel, "Total Courses", GetCourseCount().ToString(), 220, yPos)
                    AddStatCard(summaryPanel, "Total Faculty", GetFacultyCount().ToString(), 420, yPos)

                    yPos += 150
                    AddStatCard(summaryPanel, "Today's Attendance", GetTodayAttendanceCount().ToString(), 20, yPos)
                    AddStatCard(summaryPanel, "Active Users", GetActiveUserCount().ToString(), 220, yPos)

                Case "FACULTY"
                    ' Show faculty-specific statistics
                    AddStatCard(summaryPanel, "My Courses", GetFacultyCourseCount().ToString(), 20, yPos)
                    AddStatCard(summaryPanel, "Today's Classes", "0", 220, yPos)
                    AddStatCard(summaryPanel, "Total Students", GetStudentCount().ToString(), 420, yPos)

                Case "STUDENT"
                    ' Show student-specific statistics
                    AddStatCard(summaryPanel, "My Courses", "0", 20, yPos)
                    AddStatCard(summaryPanel, "Attendance Rate", "0%", 220, yPos)
                    AddStatCard(summaryPanel, "Pending Tasks", "0", 420, yPos)
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
    Private Sub AddStatCard(parent As Panel, title As String, value As String, x As Integer, y As Integer)
        Dim card As New Guna.UI2.WinForms.Guna2Panel With {
            .Size = New Size(180, 120),
            .Location = New Point(x, y),
            .BorderRadius = 10,
            .FillColor = Color.FromArgb(46, 139, 87)
        }
        card.ShadowDecoration.Enabled = True
        card.ShadowDecoration.Depth = 5

        Dim lblTitle As New Label With {
            .Text = title,
            .Font = New Font("Segoe UI", 10, FontStyle.Regular),
            .ForeColor = Color.White,
            .Location = New Point(15, 20),
            .Size = New Size(150, 25),
            .BackColor = Color.Transparent
        }

        Dim lblValue As New Label With {
            .Text = value,
            .Font = New Font("Segoe UI", 24, FontStyle.Bold),
            .ForeColor = Color.White,
            .Location = New Point(15, 50),
            .Size = New Size(150, 50),
            .BackColor = Color.Transparent
        }

        card.Controls.Add(lblTitle)
        card.Controls.Add(lblValue)
        parent.Controls.Add(card)
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
            Dim query As String = "SELECT COUNT(*) FROM faculty WHERE status = 'Active'"
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

    ' Navigation button click events
    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        SetActiveButton(btnDashboard)
        LoadDashboardContent()
    End Sub

    Private Sub btnStudents_Click(sender As Object, e As EventArgs) Handles btnStudents.Click
        SetActiveButton(btnStudents)
        ShowMessage("Students Management", "Student management module coming soon...")
    End Sub

    Private Sub btnCourses_Click(sender As Object, e As EventArgs) Handles btnCourses.Click
        SetActiveButton(btnCourses)
        ShowMessage("Course Management", "Course management module coming soon...")
    End Sub

    Private Sub btnAttendance_Click(sender As Object, e As EventArgs) Handles btnAttendance.Click
        SetActiveButton(btnAttendance)
        ShowMessage("Attendance Tracking", "Attendance tracking module coming soon...")
    End Sub

    Private Sub btnFaculty_Click(sender As Object, e As EventArgs) Handles btnFaculty.Click
        SetActiveButton(btnFaculty)
        ShowMessage("Faculty Management", "Faculty management module coming soon...")
    End Sub

    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        SetActiveButton(btnUsers)
        ShowMessage("User Management", "User management module coming soon...")
    End Sub

    Private Sub btnReports_Click(sender As Object, e As EventArgs) Handles btnReports.Click
        SetActiveButton(btnReports)
        ShowMessage("Reports", "Reports module coming soon...")
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        SetActiveButton(btnSettings)
        ShowMessage("Settings", "Settings module coming soon...")
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Try
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Logger.LogInfo($"User {currentUser.Username} logged out")
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

End Class