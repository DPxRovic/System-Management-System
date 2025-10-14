' ==========================================
' FILENAME: /Forms/DashboardForm.Designer.vb
' PURPOSE: Designer file for DashboardForm with side navigation
' AUTHOR: System
' DATE: 2025-10-14
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DashboardForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.pnlSidebar = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnLogout = New Guna.UI2.WinForms.Guna2Button()
        Me.btnSettings = New Guna.UI2.WinForms.Guna2Button()
        Me.btnReports = New Guna.UI2.WinForms.Guna2Button()
        Me.btnUsers = New Guna.UI2.WinForms.Guna2Button()
        Me.btnFaculty = New Guna.UI2.WinForms.Guna2Button()
        Me.btnAttendance = New Guna.UI2.WinForms.Guna2Button()
        Me.btnCourses = New Guna.UI2.WinForms.Guna2Button()
        Me.btnStudents = New Guna.UI2.WinForms.Guna2Button()
        Me.btnDashboard = New Guna.UI2.WinForms.Guna2Button()
        Me.pnlUserInfo = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblRole = New System.Windows.Forms.Label()
        Me.lblWelcome = New System.Windows.Forms.Label()
        Me.pnlTop = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblPageTitle = New System.Windows.Forms.Label()
        Me.btnMinimize = New Guna.UI2.WinForms.Guna2Button()
        Me.btnMaximize = New Guna.UI2.WinForms.Guna2Button()
        Me.btnClose = New Guna.UI2.WinForms.Guna2Button()
        Me.pnlContent = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2DragControl1 = New Guna.UI2.WinForms.Guna2DragControl(Me.components)
        Me.Guna2ShadowForm1 = New Guna.UI2.WinForms.Guna2ShadowForm(Me.components)
        Me.pnlSidebar.SuspendLayout()
        Me.pnlUserInfo.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.TargetControl = Me
        Me.Guna2Elipse1.BorderRadius = 15
        '
        'pnlSidebar
        '
        Me.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.pnlSidebar.Controls.Add(Me.btnLogout)
        Me.pnlSidebar.Controls.Add(Me.btnSettings)
        Me.pnlSidebar.Controls.Add(Me.btnReports)
        Me.pnlSidebar.Controls.Add(Me.btnUsers)
        Me.pnlSidebar.Controls.Add(Me.btnFaculty)
        Me.pnlSidebar.Controls.Add(Me.btnAttendance)
        Me.pnlSidebar.Controls.Add(Me.btnCourses)
        Me.pnlSidebar.Controls.Add(Me.btnStudents)
        Me.pnlSidebar.Controls.Add(Me.btnDashboard)
        Me.pnlSidebar.Controls.Add(Me.pnlUserInfo)
        Me.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSidebar.Location = New System.Drawing.Point(0, 0)
        Me.pnlSidebar.Name = "pnlSidebar"
        Me.pnlSidebar.Size = New System.Drawing.Size(250, 700)
        Me.pnlSidebar.TabIndex = 0
        '
        'btnLogout
        '
        Me.btnLogout.BorderRadius = 5
        Me.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnLogout.FillColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnLogout.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnLogout.ForeColor = System.Drawing.Color.White
        Me.btnLogout.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(43, Byte), Integer))
        Me.btnLogout.Location = New System.Drawing.Point(0, 650)
        Me.btnLogout.Name = "btnLogout"
        Me.btnLogout.Size = New System.Drawing.Size(250, 50)
        Me.btnLogout.TabIndex = 9
        Me.btnLogout.Text = "Logout"
        '
        'btnSettings
        '
        Me.btnSettings.BorderRadius = 5
        Me.btnSettings.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnSettings.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnSettings.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnSettings.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnSettings.FillColor = System.Drawing.Color.Transparent
        Me.btnSettings.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnSettings.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnSettings.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnSettings.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnSettings.Location = New System.Drawing.Point(10, 550)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(230, 45)
        Me.btnSettings.TabIndex = 8
        Me.btnSettings.Text = "Settings"
        Me.btnSettings.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnReports
        '
        Me.btnReports.BorderRadius = 5
        Me.btnReports.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnReports.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnReports.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnReports.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnReports.FillColor = System.Drawing.Color.Transparent
        Me.btnReports.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnReports.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnReports.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnReports.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnReports.Location = New System.Drawing.Point(10, 495)
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(230, 45)
        Me.btnReports.TabIndex = 7
        Me.btnReports.Text = "Reports"
        Me.btnReports.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnUsers
        '
        Me.btnUsers.BorderRadius = 5
        Me.btnUsers.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnUsers.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnUsers.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnUsers.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnUsers.FillColor = System.Drawing.Color.Transparent
        Me.btnUsers.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnUsers.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnUsers.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnUsers.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnUsers.Location = New System.Drawing.Point(10, 440)
        Me.btnUsers.Name = "btnUsers"
        Me.btnUsers.Size = New System.Drawing.Size(230, 45)
        Me.btnUsers.TabIndex = 6
        Me.btnUsers.Text = "Users"
        Me.btnUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnFaculty
        '
        Me.btnFaculty.BorderRadius = 5
        Me.btnFaculty.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnFaculty.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnFaculty.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnFaculty.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnFaculty.FillColor = System.Drawing.Color.Transparent
        Me.btnFaculty.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnFaculty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnFaculty.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnFaculty.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnFaculty.Location = New System.Drawing.Point(10, 385)
        Me.btnFaculty.Name = "btnFaculty"
        Me.btnFaculty.Size = New System.Drawing.Size(230, 45)
        Me.btnFaculty.TabIndex = 5
        Me.btnFaculty.Text = "Faculty"
        Me.btnFaculty.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnAttendance
        '
        Me.btnAttendance.BorderRadius = 5
        Me.btnAttendance.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnAttendance.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnAttendance.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnAttendance.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnAttendance.FillColor = System.Drawing.Color.Transparent
        Me.btnAttendance.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnAttendance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnAttendance.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnAttendance.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnAttendance.Location = New System.Drawing.Point(10, 330)
        Me.btnAttendance.Name = "btnAttendance"
        Me.btnAttendance.Size = New System.Drawing.Size(230, 45)
        Me.btnAttendance.TabIndex = 4
        Me.btnAttendance.Text = "Attendance"
        Me.btnAttendance.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnCourses
        '
        Me.btnCourses.BorderRadius = 5
        Me.btnCourses.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnCourses.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnCourses.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnCourses.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnCourses.FillColor = System.Drawing.Color.Transparent
        Me.btnCourses.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnCourses.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnCourses.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnCourses.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnCourses.Location = New System.Drawing.Point(10, 275)
        Me.btnCourses.Name = "btnCourses"
        Me.btnCourses.Size = New System.Drawing.Size(230, 45)
        Me.btnCourses.TabIndex = 3
        Me.btnCourses.Text = "Courses"
        Me.btnCourses.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnStudents
        '
        Me.btnStudents.BorderRadius = 5
        Me.btnStudents.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnStudents.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnStudents.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnStudents.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnStudents.FillColor = System.Drawing.Color.Transparent
        Me.btnStudents.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnStudents.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnStudents.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnStudents.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnStudents.Location = New System.Drawing.Point(10, 220)
        Me.btnStudents.Name = "btnStudents"
        Me.btnStudents.Size = New System.Drawing.Size(230, 45)
        Me.btnStudents.TabIndex = 2
        Me.btnStudents.Text = "Students"
        Me.btnStudents.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'btnDashboard
        '
        Me.btnDashboard.BorderRadius = 5
        Me.btnDashboard.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnDashboard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnDashboard.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnDashboard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnDashboard.FillColor = System.Drawing.Color.Transparent
        Me.btnDashboard.Font = New System.Drawing.Font("Segoe UI", 10.5!)
        Me.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.btnDashboard.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnDashboard.HoverState.ForeColor = System.Drawing.Color.White
        Me.btnDashboard.Location = New System.Drawing.Point(10, 165)
        Me.btnDashboard.Name = "btnDashboard"
        Me.btnDashboard.Size = New System.Drawing.Size(230, 45)
        Me.btnDashboard.TabIndex = 1
        Me.btnDashboard.Text = "Dashboard"
        Me.btnDashboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'pnlUserInfo
        '
        Me.pnlUserInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.pnlUserInfo.Controls.Add(Me.lblRole)
        Me.pnlUserInfo.Controls.Add(Me.lblWelcome)
        Me.pnlUserInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlUserInfo.Location = New System.Drawing.Point(0, 0)
        Me.pnlUserInfo.Name = "pnlUserInfo"
        Me.pnlUserInfo.Size = New System.Drawing.Size(250, 120)
        Me.pnlUserInfo.TabIndex = 0
        '
        'lblRole
        '
        Me.lblRole.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblRole.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblRole.ForeColor = System.Drawing.Color.White
        Me.lblRole.Location = New System.Drawing.Point(0, 90)
        Me.lblRole.Name = "lblRole"
        Me.lblRole.Size = New System.Drawing.Size(250, 30)
        Me.lblRole.TabIndex = 1
        Me.lblRole.Text = "Role: Admin"
        Me.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWelcome
        '
        Me.lblWelcome.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblWelcome.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblWelcome.ForeColor = System.Drawing.Color.White
        Me.lblWelcome.Location = New System.Drawing.Point(0, 0)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(250, 80)
        Me.lblWelcome.TabIndex = 0
        Me.lblWelcome.Text = "Welcome, User"
        Me.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.Color.White
        Me.pnlTop.Controls.Add(Me.lblPageTitle)
        Me.pnlTop.Controls.Add(Me.btnMinimize)
        Me.pnlTop.Controls.Add(Me.btnMaximize)
        Me.pnlTop.Controls.Add(Me.btnClose)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(250, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.ShadowDecoration.Depth = 5
        Me.pnlTop.ShadowDecoration.Enabled = True
        Me.pnlTop.Size = New System.Drawing.Size(950, 60)
        Me.pnlTop.TabIndex = 1
        '
        'lblPageTitle
        '
        Me.lblPageTitle.AutoSize = True
        Me.lblPageTitle.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblPageTitle.Location = New System.Drawing.Point(20, 17)
        Me.lblPageTitle.Name = "lblPageTitle"
        Me.lblPageTitle.Size = New System.Drawing.Size(104, 25)
        Me.lblPageTitle.TabIndex = 3
        Me.lblPageTitle.Text = "Dashboard"
        '
        'btnMinimize
        '
        Me.btnMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMinimize.BorderRadius = 5
        Me.btnMinimize.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnMinimize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnMinimize.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnMinimize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnMinimize.FillColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(196, Byte), Integer), CType(CType(15, Byte), Integer))
        Me.btnMinimize.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnMinimize.ForeColor = System.Drawing.Color.White
        Me.btnMinimize.Location = New System.Drawing.Point(810, 15)
        Me.btnMinimize.Name = "btnMinimize"
        Me.btnMinimize.Size = New System.Drawing.Size(35, 35)
        Me.btnMinimize.TabIndex = 2
        Me.btnMinimize.Text = "-"
        '
        'btnMaximize
        '
        Me.btnMaximize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMaximize.BorderRadius = 5
        Me.btnMaximize.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnMaximize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnMaximize.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnMaximize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnMaximize.FillColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(113, Byte), Integer))
        Me.btnMaximize.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnMaximize.ForeColor = System.Drawing.Color.White
        Me.btnMaximize.Location = New System.Drawing.Point(855, 15)
        Me.btnMaximize.Name = "btnMaximize"
        Me.btnMaximize.Size = New System.Drawing.Size(35, 35)
        Me.btnMaximize.TabIndex = 1
        Me.btnMaximize.Text = "□"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BorderRadius = 5
        Me.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnClose.FillColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Location = New System.Drawing.Point(900, 15)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(35, 35)
        Me.btnClose.TabIndex = 0
        Me.btnClose.Text = "X"
        '
        'pnlContent
        '
        Me.pnlContent.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(250, 60)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlContent.Size = New System.Drawing.Size(950, 640)
        Me.pnlContent.TabIndex = 2
        '
        'Guna2DragControl1
        '
        Me.Guna2DragControl1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2DragControl1.TargetControl = Me.pnlTop
        Me.Guna2DragControl1.UseTransparentDrag = True
        '
        'Guna2ShadowForm1
        '
        Me.Guna2ShadowForm1.TargetForm = Me
        '
        'DashboardForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 700)
        Me.Controls.Add(Me.pnlContent)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlSidebar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DashboardForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Dashboard - Student Management System"
        Me.pnlSidebar.ResumeLayout(False)
        Me.pnlUserInfo.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents pnlSidebar As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlUserInfo As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblWelcome As Label
    Friend WithEvents lblRole As Label
    Friend WithEvents btnDashboard As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnStudents As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCourses As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnAttendance As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnFaculty As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnUsers As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnReports As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnSettings As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnLogout As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlTop As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblPageTitle As Label
    Friend WithEvents btnClose As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnMaximize As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnMinimize As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlContent As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Guna2DragControl1 As Guna.UI2.WinForms.Guna2DragControl
    Friend WithEvents Guna2ShadowForm1 As Guna.UI2.WinForms.Guna2ShadowForm
End Class