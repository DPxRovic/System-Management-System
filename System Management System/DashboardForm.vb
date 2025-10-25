' ==========================================
' FILENAME: /Forms/DashboardForm.vb
' PURPOSE: Main dashboard form with role-based navigation - ENHANCED WITH STUDENT PORTAL
' AUTHOR: System
' DATE: 2025-10-17
' LAST UPDATED: 2025-10-21 - Added Student Portal Integration
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

            ' Check if user is a student and redirect to student portal
            If IsStudentUser() Then
                Logger.LogInfo($"Student user detected: {currentUser.Username}. Loading Student Portal...")
                LoadStudentPortal()
                Return
            End If

            ' Apply consistent navigation button appearance before anything else
            InitializeNavigationButtonsAppearance()

            ' Setup navigation based on role for non-student users
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
    ''' Checks if the current user is a student
    ''' </summary>
    ''' <returns>True if user role is Student</returns>
    Private Function IsStudentUser() As Boolean
        Try
            Return currentUser.Role.Trim().Equals("STUDENT", StringComparison.OrdinalIgnoreCase)
        Catch ex As Exception
            Logger.LogError("Error checking if user is student", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Loads the student portal for student users
    ''' </summary>
    Private Sub LoadStudentPortal()
        Try
            ' Validate student access BEFORE creating/loading the portal form to avoid creating a form
            ' that will immediately close itself and leave the dashboard with a disposed child.
            Dim studentId = If(currentUser?.Username, "")
            If String.IsNullOrWhiteSpace(studentId) OrElse Not StudentPortalRepository.ValidateStudentAccess(studentId) Then
                Logger.LogWarning($"Student portal access denied for user: {If(currentUser?.Username, "")}")
                MessageBox.Show("Unable to access student portal. Please contact administration.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim profile = StudentPortalRepository.GetStudentProfile(studentId)
            If profile Is Nothing Then
                Logger.LogWarning($"Student profile not found for user: {studentId}")
                MessageBox.Show("Student profile not found. Please contact administration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Hide all navigation buttons for student users
            HideAllNavigationButtons()

            ' Only show logout button
            btnLogout.Visible = True

            ' Update page title
            If lblPageTitle IsNot Nothing Then
                lblPageTitle.Text = "Student Portal"
            End If

            ' Create and load student portal form (safe now — access/profile validated)
            Dim studentPortalForm As New StudentPortalForm(currentUser)
            LoadChildForm(studentPortalForm, "Student Portal")

            Logger.LogInfo($"Student Portal loaded successfully for {currentUser.Username}")

        Catch ex As Exception
            Logger.LogError("Error loading student portal", ex)
            MessageBox.Show($"Error loading Student Portal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Hides all navigation buttons
    ''' </summary>
    Private Sub HideAllNavigationButtons()
        Try
            btnDashboard.Visible = False
            btnStudents.Visible = False
            btnCourses.Visible = False
            btnAttendance.Visible = False
            btnFaculty.Visible = False
            btnUsers.Visible = False
            btnReports.Visible = False
            btnSettings.Visible = False
        Catch ex As Exception
            Logger.LogError("Error hiding navigation buttons", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Setup navigation buttons based on user role
    ''' - Show all buttons but restrict access by Enabled. This ensures the choices are visible immediately.
    ''' </summary>
    Private Sub SetupNavigation()
        Try
            ' Show all navigation buttons initially so the user sees options at start
            btnDashboard.Visible = True
            btnStudents.Visible = True
            btnCourses.Visible = True
            btnAttendance.Visible = True
            btnFaculty.Visible = True
            btnUsers.Visible = True
            btnReports.Visible = True
            btnSettings.Visible = True

            ' Default: disable everything then enable as required
            btnDashboard.Enabled = False
            btnStudents.Enabled = False
            btnCourses.Enabled = False
            btnAttendance.Enabled = False
            btnFaculty.Enabled = False
            btnUsers.Enabled = False
            btnReports.Enabled = False
            btnSettings.Enabled = False

            ' Show navigation based on role (use Enabled to control access)
            Select Case currentUser.Role.ToUpper()
                Case "SUPERADMIN"
                    ' SuperAdmin sees everything
                    btnDashboard.Enabled = True
                    btnStudents.Enabled = True
                    btnCourses.Enabled = True
                    btnAttendance.Enabled = True
                    btnFaculty.Enabled = True
                    btnUsers.Enabled = True
                    btnReports.Enabled = True
                    btnSettings.Enabled = True

                Case "ADMIN"
                    ' Admin sees most features
                    btnDashboard.Enabled = True
                    btnStudents.Enabled = True
                    btnCourses.Enabled = True
                    btnAttendance.Enabled = True
                    btnFaculty.Enabled = True
                    btnUsers.Enabled = True
                    btnReports.Enabled = True
                    btnSettings.Enabled = True

                Case "FACULTY"
                    ' Faculty sees courses, attendance, and reports
                    btnDashboard.Enabled = True
                    btnCourses.Enabled = True
                    btnAttendance.Enabled = True
                    btnReports.Enabled = True

                Case "STUDENT"
                    ' Student users are handled separately in LoadStudentPortal()
                    HideAllNavigationButtons()
            End Select

            ' Set Dashboard as active by default (this will highlight it)
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

                Case "student"
                    ' Student users - all navigation disabled (handled by student portal)
                    btnDashboard.Enabled = False
                    btnStudents.Enabled = False
                    btnCourses.Enabled = False
                    btnAttendance.Enabled = False
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
                    ' Student dashboard - should not reach here as students are redirected to portal
                    ' But included for safety
                    AddSectionLabel(summaryPanel, "Student Portal", 20, yPos)
                    yPos += 40

                    Dim lblInfo As New Label With {
                        .Text = "You have been redirected to the Student Portal.",
                        .Font = New Font("Segoe UI", 11),
                        .ForeColor = Color.FromArgb(44, 62, 80),
                        .Location = New Point(20, yPos),
                        .AutoSize = True
                    }
                    summaryPanel.Controls.Add(lblInfo)
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
    ''' Set active navigation button - resets all and highlights the selected button
    ''' </summary>
    Private Sub SetActiveButton(btn As Guna.UI2.WinForms.Guna2Button)
        Try
            If btn Is Nothing Then Return
            ' Reset all buttons first
            ResetButtonColors()

            ' Set new active button
            activeButton = btn
            activeButton.FillColor = Color.FromArgb(26, 188, 156)
            activeButton.ForeColor = Color.White
        Catch ex As Exception
            Logger.LogError("Error setting active button", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Reset all navigation button colors
    ''' </summary>
    Private Sub ResetButtonColors()
        Try
            btnDashboard.FillColor = Color.Transparent
            btnStudents.FillColor = Color.Transparent
            btnCourses.FillColor = Color.Transparent
            btnAttendance.FillColor = Color.Transparent
            btnFaculty.FillColor = Color.Transparent
            btnUsers.FillColor = Color.Transparent
            btnReports.FillColor = Color.Transparent
            btnSettings.FillColor = Color.Transparent

            ' Reset text colors (keep white for good contrast on dark sidebar)
            btnDashboard.ForeColor = Color.White
            btnStudents.ForeColor = Color.White
            btnCourses.ForeColor = Color.White
            btnAttendance.ForeColor = Color.White
            btnFaculty.ForeColor = Color.White
            btnUsers.ForeColor = Color.White
            btnReports.ForeColor = Color.White
            btnSettings.ForeColor = Color.White
        Catch ex As Exception
            Logger.LogError("Error resetting button colors", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Highlight the active button (kept for compatibility; calls ResetButtonColors then apply highlight)
    ''' </summary>
    Private Sub HighlightButton(btn As Guna.UI2.WinForms.Guna2Button)
        ResetButtonColors()
        If btn Is Nothing Then Return
        btn.FillColor = Color.FromArgb(26, 188, 156)
        btn.ForeColor = Color.White
    End Sub

    ''' <summary>
    ''' Initialize appearance for navigation buttons (rounded, hover color, text color)
    ''' </summary>
    Private Sub InitializeNavigationButtonsAppearance()
        Try
            Dim navButtons = New Guna2Button() {
                btnDashboard, btnStudents, btnCourses, btnAttendance,
                btnFaculty, btnUsers, btnReports, btnSettings
            }

            For Each b In navButtons
                If b Is Nothing Then Continue For

                b.FillColor = Color.Transparent
                b.ForeColor = Color.White
                b.BorderRadius = 8

                ' Slight shadow if needed
                b.ShadowDecoration.Enabled = False
                b.ShadowDecoration.Depth = 3

                ' Alignment / spacing improvements
                Try
                    b.ImageAlign = HorizontalAlignment.Left
                    b.TextAlign = HorizontalAlignment.Left
                Catch
                    ' Some properties may not be available depending on designer config; ignore safely
                End Try

                ' Hover and pressed states
                Try
                    b.HoverState.FillColor = Color.FromArgb(26, 188, 156)
                    b.HoverState.ForeColor = Color.White
                    b.PressedColor = Color.FromArgb(22, 160, 133)
                Catch
                End Try
            Next
        Catch ex As Exception
            Logger.LogError("Error initializing nav button appearance", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load a child form into the content panel
    ''' NOTE: removed ResetButtonColors() call so active highlight persists after loading child form.
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
    ''' Students button click - LOADS DEDICATED STUDENT MANAGEMENT
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
        Try
            SetActiveButton(btnSettings)

            ' Create and load simple settings form
            Dim settingsForm As New SettingsForm(currentUser)
            LoadChildForm(settingsForm, "Settings")

            Logger.LogInfo($"Settings opened by {currentUser.Username}")

        Catch ex As Exception
            Logger.LogError("Error opening settings", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
                Me.Close()
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