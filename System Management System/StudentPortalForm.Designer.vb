' ==========================================
' FILENAME: /Forms/StudentPortalForm.Designer.vb
' PURPOSE: Designer file for Student Portal Form - COMPLETE FILE
' AUTHOR: System
' DATE: 2025-10-20
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StudentPortalForm
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
        Me.pnlMain = New Guna.UI2.WinForms.Guna2Panel()
        Me.tabControl = New Guna.UI2.WinForms.Guna2TabControl()
        Me.tabProfile = New System.Windows.Forms.TabPage()
        Me.pnlProfile = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblStatusValue = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblEnrollmentValue = New System.Windows.Forms.Label()
        Me.lblEnrollment = New System.Windows.Forms.Label()
        Me.lblDOBValue = New System.Windows.Forms.Label()
        Me.lblDOB = New System.Windows.Forms.Label()
        Me.lblCourseValue = New System.Windows.Forms.Label()
        Me.lblCourse = New System.Windows.Forms.Label()
        Me.lblPhoneValue = New System.Windows.Forms.Label()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.lblEmailValue = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblNameValue = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblStudentIdValue = New System.Windows.Forms.Label()
        Me.lblStudentId = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlStats = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblStatDaysEnrolled = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblStatOverallRate = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblStatTotalAttendance = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblStatEnrolledCourses = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tabAttendance = New System.Windows.Forms.TabPage()
        Me.pnlAttendanceContent = New Guna.UI2.WinForms.Guna2Panel()
        Me.dgvAttendance = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.flpCourseAttendance = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlAttendanceStats = New Guna.UI2.WinForms.Guna2Panel()
        Me.pbAttendanceRate = New Guna.UI2.WinForms.Guna2ProgressBar()
        Me.lblAttendanceRate = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblExcusedCount = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblLateCount = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblAbsentCount = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblPresentCount = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pnlAttendanceControls = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblAttendanceCount = New System.Windows.Forms.Label()
        Me.btnResetFilter = New Guna.UI2.WinForms.Guna2Button()
        Me.btnApplyFilter = New Guna.UI2.WinForms.Guna2Button()
        Me.dtpEndDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpStartDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tabCourses = New System.Windows.Forms.TabPage()
        Me.dgvCourses = New Guna.UI2.WinForms.Guna2DataGridView()
        Me.pnlCoursesHeader = New Guna.UI2.WinForms.Guna2Panel()
        Me.lblCoursesCount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tabReports = New System.Windows.Forms.TabPage()
        Me.pnlReportsContent = New Guna.UI2.WinForms.Guna2Panel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.btnPrintReport = New Guna.UI2.WinForms.Guna2Button()
        Me.btnExportReport = New Guna.UI2.WinForms.Guna2Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlHeader = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnRefreshAll = New Guna.UI2.WinForms.Guna2Button()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.tabControl.SuspendLayout()
        Me.tabProfile.SuspendLayout()
        Me.pnlProfile.SuspendLayout()
        Me.pnlStats.SuspendLayout()
        Me.tabAttendance.SuspendLayout()
        Me.pnlAttendanceContent.SuspendLayout()
        CType(Me.dgvAttendance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAttendanceStats.SuspendLayout()
        Me.pnlAttendanceControls.SuspendLayout()
        Me.tabCourses.SuspendLayout()
        CType(Me.dgvCourses, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCoursesHeader.SuspendLayout()
        Me.tabReports.SuspendLayout()
        Me.pnlReportsContent.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.BorderRadius = 15
        Me.Guna2Elipse1.TargetControl = Me
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.tabControl)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlMain.Size = New System.Drawing.Size(1200, 800)
        Me.pnlMain.TabIndex = 0
        '
        'tabControl
        '
        Me.tabControl.Alignment = System.Windows.Forms.TabAlignment.Top
        Me.tabControl.Controls.Add(Me.tabProfile)
        Me.tabControl.Controls.Add(Me.tabAttendance)
        Me.tabControl.Controls.Add(Me.tabCourses)
        Me.tabControl.Controls.Add(Me.tabReports)
        Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl.ItemSize = New System.Drawing.Size(200, 50)
        Me.tabControl.Location = New System.Drawing.Point(20, 120)
        Me.tabControl.Name = "tabControl"
        Me.tabControl.SelectedIndex = 0
        Me.tabControl.Size = New System.Drawing.Size(1160, 660)
        Me.tabControl.TabIndex = 1
        '
        'tabProfile
        '
        Me.tabProfile.BackColor = System.Drawing.Color.White
        Me.tabProfile.Controls.Add(Me.pnlProfile)
        Me.tabProfile.Controls.Add(Me.pnlStats)
        Me.tabProfile.Location = New System.Drawing.Point(4, 54)
        Me.tabProfile.Name = "tabProfile"
        Me.tabProfile.Padding = New System.Windows.Forms.Padding(15)
        Me.tabProfile.Size = New System.Drawing.Size(1152, 602)
        Me.tabProfile.TabIndex = 0
        Me.tabProfile.Text = "👤 My Profile"
        '
        'pnlProfile
        '
        Me.pnlProfile.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pnlProfile.BorderRadius = 10
        Me.pnlProfile.BorderThickness = 1
        Me.pnlProfile.Controls.Add(Me.lblStatusValue)
        Me.pnlProfile.Controls.Add(Me.lblStatus)
        Me.pnlProfile.Controls.Add(Me.lblEnrollmentValue)
        Me.pnlProfile.Controls.Add(Me.lblEnrollment)
        Me.pnlProfile.Controls.Add(Me.lblDOBValue)
        Me.pnlProfile.Controls.Add(Me.lblDOB)
        Me.pnlProfile.Controls.Add(Me.lblCourseValue)
        Me.pnlProfile.Controls.Add(Me.lblCourse)
        Me.pnlProfile.Controls.Add(Me.lblPhoneValue)
        Me.pnlProfile.Controls.Add(Me.lblPhone)
        Me.pnlProfile.Controls.Add(Me.lblEmailValue)
        Me.pnlProfile.Controls.Add(Me.lblEmail)
        Me.pnlProfile.Controls.Add(Me.lblNameValue)
        Me.pnlProfile.Controls.Add(Me.lblName)
        Me.pnlProfile.Controls.Add(Me.lblStudentIdValue)
        Me.pnlProfile.Controls.Add(Me.lblStudentId)
        Me.pnlProfile.Controls.Add(Me.Label1)
        Me.pnlProfile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlProfile.Location = New System.Drawing.Point(15, 135)
        Me.pnlProfile.Name = "pnlProfile"
        Me.pnlProfile.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlProfile.Size = New System.Drawing.Size(1122, 452)
        Me.pnlProfile.TabIndex = 1
        '
        'lblStatusValue
        '
        Me.lblStatusValue.AutoSize = True
        Me.lblStatusValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblStatusValue.Location = New System.Drawing.Point(250, 385)
        Me.lblStatusValue.Name = "lblStatusValue"
        Me.lblStatusValue.Size = New System.Drawing.Size(51, 19)
        Me.lblStatusValue.TabIndex = 16
        Me.lblStatusValue.Text = "Active"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblStatus.Location = New System.Drawing.Point(40, 385)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(52, 19)
        Me.lblStatus.TabIndex = 15
        Me.lblStatus.Text = "Status:"
        '
        'lblEnrollmentValue
        '
        Me.lblEnrollmentValue.AutoSize = True
        Me.lblEnrollmentValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblEnrollmentValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblEnrollmentValue.Location = New System.Drawing.Point(250, 345)
        Me.lblEnrollmentValue.Name = "lblEnrollmentValue"
        Me.lblEnrollmentValue.Size = New System.Drawing.Size(105, 19)
        Me.lblEnrollmentValue.TabIndex = 14
        Me.lblEnrollmentValue.Text = "Not Available"
        '
        'lblEnrollment
        '
        Me.lblEnrollment.AutoSize = True
        Me.lblEnrollment.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblEnrollment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblEnrollment.Location = New System.Drawing.Point(40, 345)
        Me.lblEnrollment.Name = "lblEnrollment"
        Me.lblEnrollment.Size = New System.Drawing.Size(119, 19)
        Me.lblEnrollment.TabIndex = 13
        Me.lblEnrollment.Text = "Enrollment Date:"
        '
        'lblDOBValue
        '
        Me.lblDOBValue.AutoSize = True
        Me.lblDOBValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDOBValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblDOBValue.Location = New System.Drawing.Point(250, 305)
        Me.lblDOBValue.Name = "lblDOBValue"
        Me.lblDOBValue.Size = New System.Drawing.Size(80, 19)
        Me.lblDOBValue.TabIndex = 12
        Me.lblDOBValue.Text = "Not on file"
        '
        'lblDOB
        '
        Me.lblDOB.AutoSize = True
        Me.lblDOB.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblDOB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblDOB.Location = New System.Drawing.Point(40, 305)
        Me.lblDOB.Name = "lblDOB"
        Me.lblDOB.Size = New System.Drawing.Size(95, 19)
        Me.lblDOB.TabIndex = 11
        Me.lblDOB.Text = "Date of Birth:"
        '
        'lblCourseValue
        '
        Me.lblCourseValue.AutoSize = True
        Me.lblCourseValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCourseValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblCourseValue.Location = New System.Drawing.Point(250, 265)
        Me.lblCourseValue.Name = "lblCourseValue"
        Me.lblCourseValue.Size = New System.Drawing.Size(114, 19)
        Me.lblCourseValue.TabIndex = 10
        Me.lblCourseValue.Text = "Not Registered"
        '
        'lblCourse
        '
        Me.lblCourse.AutoSize = True
        Me.lblCourse.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblCourse.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblCourse.Location = New System.Drawing.Point(40, 265)
        Me.lblCourse.Name = "lblCourse"
        Me.lblCourse.Size = New System.Drawing.Size(118, 19)
        Me.lblCourse.TabIndex = 9
        Me.lblCourse.Text = "Course/Program:"
        '
        'lblPhoneValue
        '
        Me.lblPhoneValue.AutoSize = True
        Me.lblPhoneValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPhoneValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblPhoneValue.Location = New System.Drawing.Point(250, 225)
        Me.lblPhoneValue.Name = "lblPhoneValue"
        Me.lblPhoneValue.Size = New System.Drawing.Size(103, 19)
        Me.lblPhoneValue.TabIndex = 8
        Me.lblPhoneValue.Text = "Not provided"
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblPhone.Location = New System.Drawing.Point(40, 225)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(108, 19)
        Me.lblPhone.TabIndex = 7
        Me.lblPhone.Text = "Phone Number:"
        '
        'lblEmailValue
        '
        Me.lblEmailValue.AutoSize = True
        Me.lblEmailValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblEmailValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblEmailValue.Location = New System.Drawing.Point(250, 185)
        Me.lblEmailValue.Name = "lblEmailValue"
        Me.lblEmailValue.Size = New System.Drawing.Size(103, 19)
        Me.lblEmailValue.TabIndex = 6
        Me.lblEmailValue.Text = "Not provided"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblEmail.Location = New System.Drawing.Point(40, 185)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(103, 19)
        Me.lblEmail.TabIndex = 5
        Me.lblEmail.Text = "Email Address:"
        '
        'lblNameValue
        '
        Me.lblNameValue.AutoSize = True
        Me.lblNameValue.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblNameValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblNameValue.Location = New System.Drawing.Point(250, 145)
        Me.lblNameValue.Name = "lblNameValue"
        Me.lblNameValue.Size = New System.Drawing.Size(119, 21)
        Me.lblNameValue.TabIndex = 4
        Me.lblNameValue.Text = "Student Name"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(40, 145)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(76, 19)
        Me.lblName.TabIndex = 3
        Me.lblName.Text = "Full Name:"
        '
        'lblStudentIdValue
        '
        Me.lblStudentIdValue.AutoSize = True
        Me.lblStudentIdValue.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblStudentIdValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblStudentIdValue.Location = New System.Drawing.Point(250, 105)
        Me.lblStudentIdValue.Name = "lblStudentIdValue"
        Me.lblStudentIdValue.Size = New System.Drawing.Size(100, 21)
        Me.lblStudentIdValue.TabIndex = 2
        Me.lblStudentIdValue.Text = "0000-0000"
        '
        'lblStudentId
        '
        Me.lblStudentId.AutoSize = True
        Me.lblStudentId.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStudentId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblStudentId.Location = New System.Drawing.Point(40, 105)
        Me.lblStudentId.Name = "lblStudentId"
        Me.lblStudentId.Size = New System.Drawing.Size(80, 19)
        Me.lblStudentId.TabIndex = 1
        Me.lblStudentId.Text = "Student ID:"
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(20, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1082, 50)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "📋 Personal Information"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlStats
        '
        Me.pnlStats.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pnlStats.BorderRadius = 10
        Me.pnlStats.BorderThickness = 1
        Me.pnlStats.Controls.Add(Me.lblStatDaysEnrolled)
        Me.pnlStats.Controls.Add(Me.Label17)
        Me.pnlStats.Controls.Add(Me.lblStatOverallRate)
        Me.pnlStats.Controls.Add(Me.Label15)
        Me.pnlStats.Controls.Add(Me.lblStatTotalAttendance)
        Me.pnlStats.Controls.Add(Me.Label13)
        Me.pnlStats.Controls.Add(Me.lblStatEnrolledCourses)
        Me.pnlStats.Controls.Add(Me.Label11)
        Me.pnlStats.Controls.Add(Me.Label10)
        Me.pnlStats.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlStats.Location = New System.Drawing.Point(15, 15)
        Me.pnlStats.Name = "pnlStats"
        Me.pnlStats.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlStats.Size = New System.Drawing.Size(1122, 120)
        Me.pnlStats.TabIndex = 0
        '
        'lblStatDaysEnrolled
        '
        Me.lblStatDaysEnrolled.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatDaysEnrolled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblStatDaysEnrolled.Location = New System.Drawing.Point(850, 45)
        Me.lblStatDaysEnrolled.Name = "lblStatDaysEnrolled"
        Me.lblStatDaysEnrolled.Size = New System.Drawing.Size(100, 30)
        Me.lblStatDaysEnrolled.TabIndex = 8
        Me.lblStatDaysEnrolled.Text = "0"
        Me.lblStatDaysEnrolled.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(850, 75)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(100, 20)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "Days Enrolled"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatOverallRate
        '
        Me.lblStatOverallRate.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatOverallRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblStatOverallRate.Location = New System.Drawing.Point(600, 45)
        Me.lblStatOverallRate.Name = "lblStatOverallRate"
        Me.lblStatOverallRate.Size = New System.Drawing.Size(120, 30)
        Me.lblStatOverallRate.TabIndex = 6
        Me.lblStatOverallRate.Text = "0%"
        Me.lblStatOverallRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(600, 75)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 20)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "Overall Rate"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatTotalAttendance
        '
        Me.lblStatTotalAttendance.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatTotalAttendance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblStatTotalAttendance.Location = New System.Drawing.Point(350, 45)
        Me.lblStatTotalAttendance.Name = "lblStatTotalAttendance"
        Me.lblStatTotalAttendance.Size = New System.Drawing.Size(120, 30)
        Me.lblStatTotalAttendance.TabIndex = 4
        Me.lblStatTotalAttendance.Text = "0"
        Me.lblStatTotalAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(350, 75)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 20)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Total Attendance"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatEnrolledCourses
        '
        Me.lblStatEnrolledCourses.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatEnrolledCourses.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblStatEnrolledCourses.Location = New System.Drawing.Point(100, 45)
        Me.lblStatEnrolledCourses.Name = "lblStatEnrolledCourses"
        Me.lblStatEnrolledCourses.Size = New System.Drawing.Size(120, 30)
        Me.lblStatEnrolledCourses.TabIndex = 2
        Me.lblStatEnrolledCourses.Text = "0"
        Me.lblStatEnrolledCourses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(100, 75)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 20)
        Me.Label11.TabIndex = 1
        Me.Label11.Text = "Enrolled Courses"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(20, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(1082, 25)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "📊 Quick Statistics"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabAttendance
        '
        Me.tabAttendance.BackColor = System.Drawing.Color.White
        Me.tabAttendance.Controls.Add(Me.pnlAttendanceContent)
        Me.tabAttendance.Controls.Add(Me.pnlAttendanceStats)
        Me.tabAttendance.Controls.Add(Me.pnlAttendanceControls)
        Me.tabAttendance.Location = New System.Drawing.Point(4, 54)
        Me.tabAttendance.Name = "tabAttendance"
        Me.tabAttendance.Padding = New System.Windows.Forms.Padding(15)
        Me.tabAttendance.Size = New System.Drawing.Size(1152, 602)
        Me.tabAttendance.TabIndex = 1
        Me.tabAttendance.Text = "📅 My Attendance"
        '
        'pnlAttendanceContent
        '
        Me.pnlAttendanceContent.Controls.Add(Me.dgvAttendance)
        Me.pnlAttendanceContent.Controls.Add(Me.flpCourseAttendance)
        Me.pnlAttendanceContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlAttendanceContent.Location = New System.Drawing.Point(15, 245)
        Me.pnlAttendanceContent.Name = "pnlAttendanceContent"
        Me.pnlAttendanceContent.Size = New System.Drawing.Size(1122, 342)
        Me.pnlAttendanceContent.TabIndex = 2
        '
        'dgvAttendance
        '
        Me.dgvAttendance.AllowUserToAddRows = False
        Me.dgvAttendance.AllowUserToDeleteRows = False
        Me.dgvAttendance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvAttendance.Location = New System.Drawing.Point(0, 160)
        Me.dgvAttendance.Name = "dgvAttendance"
        Me.dgvAttendance.ReadOnly = True
        Me.dgvAttendance.RowHeadersVisible = False
        Me.dgvAttendance.RowTemplate.Height = 35
        Me.dgvAttendance.Size = New System.Drawing.Size(1122, 182)
        Me.dgvAttendance.TabIndex = 1
        '
        'flpCourseAttendance
        '
        Me.flpCourseAttendance.AutoScroll = True
        Me.flpCourseAttendance.Dock = System.Windows.Forms.DockStyle.Top
        Me.flpCourseAttendance.Location = New System.Drawing.Point(0, 0)
        Me.flpCourseAttendance.Name = "flpCourseAttendance"
        Me.flpCourseAttendance.Padding = New System.Windows.Forms.Padding(5)
        Me.flpCourseAttendance.Size = New System.Drawing.Size(1122, 160)
        Me.flpCourseAttendance.TabIndex = 0
        '
        'pnlAttendanceStats
        '
        Me.pnlAttendanceStats.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pnlAttendanceStats.BorderRadius = 10
        Me.pnlAttendanceStats.BorderThickness = 1
        Me.pnlAttendanceStats.Controls.Add(Me.pbAttendanceRate)
        Me.pnlAttendanceStats.Controls.Add(Me.lblAttendanceRate)
        Me.pnlAttendanceStats.Controls.Add(Me.Label26)
        Me.pnlAttendanceStats.Controls.Add(Me.lblExcusedCount)
        Me.pnlAttendanceStats.Controls.Add(Me.Label24)
        Me.pnlAttendanceStats.Controls.Add(Me.lblLateCount)
        Me.pnlAttendanceStats.Controls.Add(Me.Label22)
        Me.pnlAttendanceStats.Controls.Add(Me.lblAbsentCount)
        Me.pnlAttendanceStats.Controls.Add(Me.Label20)
        Me.pnlAttendanceStats.Controls.Add(Me.lblPresentCount)
        Me.pnlAttendanceStats.Controls.Add(Me.Label18)
        Me.pnlAttendanceStats.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAttendanceStats.Location = New System.Drawing.Point(15, 115)
        Me.pnlAttendanceStats.Name = "pnlAttendanceStats"
        Me.pnlAttendanceStats.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlAttendanceStats.Size = New System.Drawing.Size(1122, 130)
        Me.pnlAttendanceStats.TabIndex = 1
        '
        'pbAttendanceRate
        '
        Me.pbAttendanceRate.BorderRadius = 5
        Me.pbAttendanceRate.Location = New System.Drawing.Point(750, 75)
        Me.pbAttendanceRate.Name = "pbAttendanceRate"
        Me.pbAttendanceRate.Size = New System.Drawing.Size(200, 20)
        Me.pbAttendanceRate.TabIndex = 10
        '
        'lblAttendanceRate
        '
        Me.lblAttendanceRate.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblAttendanceRate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblAttendanceRate.Location = New System.Drawing.Point(750, 45)
        Me.lblAttendanceRate.Name = "lblAttendanceRate"
        Me.lblAttendanceRate.Size = New System.Drawing.Size(200, 25)
        Me.lblAttendanceRate.TabIndex = 9
        Me.lblAttendanceRate.Text = "0%"
        Me.lblAttendanceRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(750, 100)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(200, 20)
        Me.Label26.TabIndex = 8
        Me.Label26.Text = "Attendance Rate"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblExcusedCount
        '
        Me.lblExcusedCount.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblExcusedCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(155, Byte), Integer), CType(CType(89, Byte), Integer), CType(CType(182, Byte), Integer))
        Me.lblExcusedCount.Location = New System.Drawing.Point(600, 45)
        Me.lblExcusedCount.Name = "lblExcusedCount"
        Me.lblExcusedCount.Size = New System.Drawing.Size(100, 30)
        Me.lblExcusedCount.TabIndex = 7
        Me.lblExcusedCount.Text = "0"
        Me.lblExcusedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(600, 75)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(100, 20)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "Excused"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLateCount
        '
        Me.lblLateCount.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblLateCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(156, Byte), Integer), CType(CType(18, Byte), Integer))
        Me.lblLateCount.Location = New System.Drawing.Point(450, 45)
        Me.lblLateCount.Name = "lblLateCount"
        Me.lblLateCount.Size = New System.Drawing.Size(100, 30)
        Me.lblLateCount.TabIndex = 5
        Me.lblLateCount.Text = "0"
        Me.lblLateCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(450, 75)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(100, 20)
        Me.Label22.TabIndex = 4
        Me.Label22.Text = "Late"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbsentCount
        '
        Me.lblAbsentCount.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblAbsentCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.lblAbsentCount.Location = New System.Drawing.Point(300, 45)
        Me.lblAbsentCount.Name = "lblAbsentCount"
        Me.lblAbsentCount.Size = New System.Drawing.Size(100, 30)
        Me.lblAbsentCount.TabIndex = 3
        Me.lblAbsentCount.Text = "0"
        Me.lblAbsentCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(300, 75)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(100, 20)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Absent"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPresentCount
        '
        Me.lblPresentCount.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblPresentCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblPresentCount.Location = New System.Drawing.Point(150, 45)
        Me.lblPresentCount.Name = "lblPresentCount"
        Me.lblPresentCount.Size = New System.Drawing.Size(100, 30)
        Me.lblPresentCount.TabIndex = 1
        Me.lblPresentCount.Text = "0"
        Me.lblPresentCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(150, 75)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(100, 20)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Present"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlAttendanceControls
        '
        Me.pnlAttendanceControls.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pnlAttendanceControls.BorderRadius = 10
        Me.pnlAttendanceControls.BorderThickness = 1
        Me.pnlAttendanceControls.Controls.Add(Me.lblAttendanceCount)
        Me.pnlAttendanceControls.Controls.Add(Me.btnResetFilter)
        Me.pnlAttendanceControls.Controls.Add(Me.btnApplyFilter)
        Me.pnlAttendanceControls.Controls.Add(Me.dtpEndDate)
        Me.pnlAttendanceControls.Controls.Add(Me.Label9)
        Me.pnlAttendanceControls.Controls.Add(Me.dtpStartDate)
        Me.pnlAttendanceControls.Controls.Add(Me.Label8)
        Me.pnlAttendanceControls.Controls.Add(Me.Label2)
        Me.pnlAttendanceControls.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlAttendanceControls.Location = New System.Drawing.Point(15, 15)
        Me.pnlAttendanceControls.Name = "pnlAttendanceControls"
        Me.pnlAttendanceControls.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlAttendanceControls.Size = New System.Drawing.Size(1122, 100)
        Me.pnlAttendanceControls.TabIndex = 0
        '
        'lblAttendanceCount
        '
        Me.lblAttendanceCount.AutoSize = True
        Me.lblAttendanceCount.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAttendanceCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblAttendanceCount.Location = New System.Drawing.Point(850, 60)
        Me.lblAttendanceCount.Name = "lblAttendanceCount"
        Me.lblAttendanceCount.Size = New System.Drawing.Size(85, 19)
        Me.lblAttendanceCount.TabIndex = 7
        Me.lblAttendanceCount.Text = "Records: 0"
        '
        'btnResetFilter
        '
        Me.btnResetFilter.BorderRadius = 8
        Me.btnResetFilter.FillColor = System.Drawing.Color.FromArgb(CType(CType(149, Byte), Integer), CType(CType(165, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnResetFilter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnResetFilter.ForeColor = System.Drawing.Color.White
        Me.btnResetFilter.Location = New System.Drawing.Point(720, 50)
        Me.btnResetFilter.Name = "btnResetFilter"
        Me.btnResetFilter.Size = New System.Drawing.Size(100, 40)
        Me.btnResetFilter.TabIndex = 6
        Me.btnResetFilter.Text = "Reset"
        '
        'btnApplyFilter
        '
        Me.btnApplyFilter.BorderRadius = 8
        Me.btnApplyFilter.FillColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnApplyFilter.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnApplyFilter.ForeColor = System.Drawing.Color.White
        Me.btnApplyFilter.Location = New System.Drawing.Point(600, 50)
        Me.btnApplyFilter.Name = "btnApplyFilter"
        Me.btnApplyFilter.Size = New System.Drawing.Size(100, 40)
        Me.btnApplyFilter.TabIndex = 5
        Me.btnApplyFilter.Text = "Apply"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.BorderRadius = 8
        Me.dtpEndDate.Checked = True
        Me.dtpEndDate.FillColor = System.Drawing.Color.White
        Me.dtpEndDate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpEndDate.Location = New System.Drawing.Point(420, 50)
        Me.dtpEndDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(150, 40)
        Me.dtpEndDate.TabIndex = 4
        Me.dtpEndDate.Value = New Date(2025, 10, 20, 0, 0, 0, 0)
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(340, 60)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 15)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "To:"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.BorderRadius = 8
        Me.dtpStartDate.Checked = True
        Me.dtpStartDate.FillColor = System.Drawing.Color.White
        Me.dtpStartDate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpStartDate.Location = New System.Drawing.Point(170, 50)
        Me.dtpStartDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.dtpStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(150, 40)
        Me.dtpStartDate.TabIndex = 2
        Me.dtpStartDate.Value = New Date(2025, 10, 20, 0, 0, 0, 0)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(80, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 15)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "From:"
        '
        'Label2
        '
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(20, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1082, 25)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "📅 Filter by Date Range"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabCourses
        '
        Me.tabCourses.BackColor = System.Drawing.Color.White
        Me.tabCourses.Controls.Add(Me.dgvCourses)
        Me.tabCourses.Controls.Add(Me.pnlCoursesHeader)
        Me.tabCourses.Location = New System.Drawing.Point(4, 54)
        Me.tabCourses.Name = "tabCourses"
        Me.tabCourses.Padding = New System.Windows.Forms.Padding(15)
        Me.tabCourses.Size = New System.Drawing.Size(1152, 602)
        Me.tabCourses.TabIndex = 2
        Me.tabCourses.Text = "📚 My Courses"
        '
        'dgvCourses
        '
        Me.dgvCourses.AllowUserToAddRows = False
        Me.dgvCourses.AllowUserToDeleteRows = False
        Me.dgvCourses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCourses.Location = New System.Drawing.Point(15, 95)
        Me.dgvCourses.Name = "dgvCourses"
        Me.dgvCourses.ReadOnly = True
        Me.dgvCourses.RowHeadersVisible = False
        Me.dgvCourses.RowTemplate.Height = 35
        Me.dgvCourses.Size = New System.Drawing.Size(1122, 492)
        Me.dgvCourses.TabIndex = 1
        'pnlCoursesHeader
        '
        Me.pnlCoursesHeader.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pnlCoursesHeader.BorderRadius = 10
        Me.pnlCoursesHeader.BorderThickness = 1
        Me.pnlCoursesHeader.Controls.Add(Me.lblCoursesCount)
        Me.pnlCoursesHeader.Controls.Add(Me.Label3)
        Me.pnlCoursesHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlCoursesHeader.Location = New System.Drawing.Point(15, 15)
        Me.pnlCoursesHeader.Name = "pnlCoursesHeader"
        Me.pnlCoursesHeader.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlCoursesHeader.Size = New System.Drawing.Size(1122, 80)
        Me.pnlCoursesHeader.TabIndex = 0
        '
        'lblCoursesCount
        '
        Me.lblCoursesCount.AutoSize = True
        Me.lblCoursesCount.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCoursesCount.ForeColor = Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblCoursesCount.Location = New System.Drawing.Point(23, 50)
        Me.lblCoursesCount.Name = "lblCoursesCount"
        Me.lblCoursesCount.Size = New System.Drawing.Size(138, 19)
        Me.lblCoursesCount.TabIndex = 1
        Me.lblCoursesCount.Text = "Enrolled Courses: 0"
        '
        'Label3
        '
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(20, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1082, 25)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "📚 Your Enrolled Courses"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabReports
        '
        Me.tabReports.BackColor = System.Drawing.Color.White
        Me.tabReports.Controls.Add(Me.pnlReportsContent)
        Me.tabReports.Location = New System.Drawing.Point(4, 54)
        Me.tabReports.Name = "tabReports"
        Me.tabReports.Padding = New System.Windows.Forms.Padding(15)
        Me.tabReports.Size = New System.Drawing.Size(1152, 602)
        Me.tabReports.TabIndex = 3
        Me.tabReports.Text = "📊 Reports"
        '
        'pnlReportsContent
        '
        Me.pnlReportsContent.BorderColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(218, Byte), Integer), CType(CType(223, Byte), Integer))
        Me.pnlReportsContent.BorderRadius = 10
        Me.pnlReportsContent.BorderThickness = 1
        Me.pnlReportsContent.Controls.Add(Me.Label28)
        Me.pnlReportsContent.Controls.Add(Me.Label27)
        Me.pnlReportsContent.Controls.Add(Me.btnPrintReport)
        Me.pnlReportsContent.Controls.Add(Me.btnExportReport)
        Me.pnlReportsContent.Controls.Add(Me.Label7)
        Me.pnlReportsContent.Controls.Add(Me.Label6)
        Me.pnlReportsContent.Controls.Add(Me.Label5)
        Me.pnlReportsContent.Controls.Add(Me.Label4)
        Me.pnlReportsContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReportsContent.Location = New System.Drawing.Point(15, 15)
        Me.pnlReportsContent.Name = "pnlReportsContent"
        Me.pnlReportsContent.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlReportsContent.Size = New System.Drawing.Size(1122, 572)
        Me.pnlReportsContent.TabIndex = 0
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(120, 280)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(293, 15)
        Me.Label28.TabIndex = 7
        Me.Label28.Text = "Print or preview your attendance report for records"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label27.Location = New System.Drawing.Point(120, 195)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(341, 15)
        Me.Label27.TabIndex = 6
        Me.Label27.Text = "Export your attendance data to CSV or HTML for external use"
        '
        'btnPrintReport
        '
        Me.btnPrintReport.BorderRadius = 8
        Me.btnPrintReport.FillColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(152, Byte), Integer), CType(CType(219, Byte), Integer))
        Me.btnPrintReport.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnPrintReport.ForeColor = System.Drawing.Color.White
        Me.btnPrintReport.Location = New System.Drawing.Point(100, 310)
        Me.btnPrintReport.Name = "btnPrintReport"
        Me.btnPrintReport.Size = New System.Drawing.Size(200, 50)
        Me.btnPrintReport.TabIndex = 5
        Me.btnPrintReport.Text = "🖨️ Print Report"
        '
        'btnExportReport
        '
        Me.btnExportReport.BorderRadius = 8
        Me.btnExportReport.FillColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnExportReport.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnExportReport.ForeColor = System.Drawing.Color.White
        Me.btnExportReport.Location = New System.Drawing.Point(100, 225)
        Me.btnExportReport.Name = "btnExportReport"
        Me.btnExportReport.Size = New System.Drawing.Size(200, 50)
        Me.btnExportReport.TabIndex = 4
        Me.btnExportReport.Text = "📥 Export Report"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(40, 130)
        Me.Label7.MaximumSize = New System.Drawing.Size(600, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(583, 30)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Generate and download reports of your attendance data. You can export to CSV or HTML format, or print directly for physical records."
        '
        'Label6
        '
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(20, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(1082, 25)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Generate Your Reports"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(40, 60)
        Me.Label5.MaximumSize = New System.Drawing.Size(600, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(565, 30)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Access and download your attendance reports. Reports include detailed attendance records, statistics, and summaries based on the date range selected in the Attendance tab."
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(20, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1082, 30)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "📊 Attendance Reports"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.White
        Me.pnlHeader.Controls.Add(Me.btnRefreshAll)
        Me.pnlHeader.Controls.Add(Me.lblSubtitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(20, 20)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1160, 100)
        Me.pnlHeader.TabIndex = 0
        '
        'btnRefreshAll
        '
        Me.btnRefreshAll.BorderRadius = 8
        Me.btnRefreshAll.FillColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(139, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnRefreshAll.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnRefreshAll.ForeColor = System.Drawing.Color.White
        Me.btnRefreshAll.Location = New System.Drawing.Point(1000, 30)
        Me.btnRefreshAll.Name = "btnRefreshAll"
        Me.btnRefreshAll.Size = New System.Drawing.Size(140, 45)
        Me.btnRefreshAll.TabIndex = 2
        Me.btnRefreshAll.Text = "🔄 Refresh All"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(20, 55)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(380, 19)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "View your profile, attendance records, courses, and reports"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(15, 20)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(179, 32)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Student Portal"
        '
        'StudentPortalForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 800)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "StudentPortalForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Student Portal"
        Me.pnlMain.ResumeLayout(False)
        Me.tabControl.ResumeLayout(False)
        Me.tabProfile.ResumeLayout(False)
        Me.pnlProfile.ResumeLayout(False)
        Me.pnlProfile.PerformLayout()
        Me.pnlStats.ResumeLayout(False)
        Me.tabAttendance.ResumeLayout(False)
        Me.pnlAttendanceContent.ResumeLayout(False)
        CType(Me.dgvAttendance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAttendanceStats.ResumeLayout(False)
        Me.pnlAttendanceControls.ResumeLayout(False)
        Me.pnlAttendanceControls.PerformLayout()
        Me.tabCourses.ResumeLayout(False)
        CType(Me.dgvCourses, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCoursesHeader.ResumeLayout(False)
        Me.pnlCoursesHeader.PerformLayout()
        Me.tabReports.ResumeLayout(False)
        Me.pnlReportsContent.ResumeLayout(False)
        Me.pnlReportsContent.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents pnlMain As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlHeader As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents btnRefreshAll As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents tabControl As Guna.UI2.WinForms.Guna2TabControl
    Friend WithEvents tabProfile As TabPage
    Friend WithEvents tabAttendance As TabPage
    Friend WithEvents tabCourses As TabPage
    Friend WithEvents tabReports As TabPage
    Friend WithEvents pnlStats As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlProfile As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblStatEnrolledCourses As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents lblStatDaysEnrolled As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents lblStatOverallRate As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents lblStatTotalAttendance As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lblStudentId As Label
    Friend WithEvents lblStudentIdValue As Label
    Friend WithEvents lblNameValue As Label
    Friend WithEvents lblName As Label
    Friend WithEvents lblEmailValue As Label
    Friend WithEvents lblEmail As Label
    Friend WithEvents lblPhoneValue As Label
    Friend WithEvents lblPhone As Label
    Friend WithEvents lblCourseValue As Label
    Friend WithEvents lblCourse As Label
    Friend WithEvents lblDOBValue As Label
    Friend WithEvents lblDOB As Label
    Friend WithEvents lblEnrollmentValue As Label
    Friend WithEvents lblEnrollment As Label
    Friend WithEvents lblStatusValue As Label
    Friend WithEvents lblStatus As Label
    Friend WithEvents pnlAttendanceControls As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpStartDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents dtpEndDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents btnApplyFilter As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnResetFilter As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblAttendanceCount As Label
    Friend WithEvents pnlAttendanceStats As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlAttendanceContent As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents dgvAttendance As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents flpCourseAttendance As FlowLayoutPanel
    Friend WithEvents lblPresentCount As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents lblExcusedCount As Label
    Friend WithEvents Label24 As Label
    Friend WithEvents lblLateCount As Label
    Friend WithEvents Label22 As Label
    Friend WithEvents lblAbsentCount As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents pbAttendanceRate As Guna.UI2.WinForms.Guna2ProgressBar
    Friend WithEvents lblAttendanceRate As Label
    Friend WithEvents Label26 As Label
    Friend WithEvents dgvCourses As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents pnlCoursesHeader As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblCoursesCount As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents pnlReportsContent As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents btnExportReport As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnPrintReport As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label27 As Label
    Friend WithEvents Label28 As Label

End Class