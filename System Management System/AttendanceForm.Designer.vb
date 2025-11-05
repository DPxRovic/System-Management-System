' ==========================================
' FILENAME: /Forms/AttendanceForm.Designer.vb
' PURPOSE: Enhanced Designer file for AttendanceForm with scrolling and additional features
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AttendanceForm
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
        components = New ComponentModel.Container()
        Dim CustomizableEdges15 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges16 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges11 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges12 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim CustomizableEdges35 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges36 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges17 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges18 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges19 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges20 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges21 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges22 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges23 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges24 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges25 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges26 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges27 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges28 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges29 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges30 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges31 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges32 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges33 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges34 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(components)
        pnlScrollContainer = New Panel()
        pnlContent = New Panel()
        pnlBottom = New Guna.UI2.WinForms.Guna2Panel()
        pnlAttendanceStats = New Guna.UI2.WinForms.Guna2Panel()
        lblLateCount = New Label()
        lblAbsentCount = New Label()
        lblPresentCount = New Label()
        Label9 = New Label()
        Label8 = New Label()
        Label7 = New Label()
        Label6 = New Label()
        pnlDateFilter = New Guna.UI2.WinForms.Guna2Panel()
        btnApplyFilter = New Guna.UI2.WinForms.Guna2Button()
        dtpEndDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        dtpStartDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Label11 = New Label()
        Label10 = New Label()
        lblTodayCount = New Label()
        btnExport = New Guna.UI2.WinForms.Guna2Button()
        btnRefresh = New Guna.UI2.WinForms.Guna2Button()
        dgvTodayAttendance = New Guna.UI2.WinForms.Guna2DataGridView()
        Label5 = New Label()
        pnlAttendance = New Guna.UI2.WinForms.Guna2Panel()
        pnlToast = New Guna.UI2.WinForms.Guna2Panel()
        lblToastMessage = New Label()
        lblStatus = New Label()
        txtRemarks = New Guna.UI2.WinForms.Guna2TextBox()
        Label4 = New Label()
        btnClear = New Guna.UI2.WinForms.Guna2Button()
        btnExcused = New Guna.UI2.WinForms.Guna2Button()
        btnLate = New Guna.UI2.WinForms.Guna2Button()
        btnAbsent = New Guna.UI2.WinForms.Guna2Button()
        btnPresent = New Guna.UI2.WinForms.Guna2Button()
        lblCourse = New Label()
        lblStudentName = New Label()
        cmbCourse = New Guna.UI2.WinForms.Guna2ComboBox()
        Label3 = New Label()
        txtStudentId = New Guna.UI2.WinForms.Guna2TextBox()
        Label2 = New Label()
        Label1 = New Label()
        tmrStudentIdDebounce = New Timer(components)
        tmrToast = New Timer(components)
        tmrResetForm = New Timer(components)
        pnlScrollContainer.SuspendLayout()
        pnlContent.SuspendLayout()
        pnlBottom.SuspendLayout()
        pnlAttendanceStats.SuspendLayout()
        pnlDateFilter.SuspendLayout()
        CType(dgvTodayAttendance, ComponentModel.ISupportInitialize).BeginInit()
        pnlAttendance.SuspendLayout()
        pnlToast.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 15
        Guna2Elipse1.TargetControl = Me
        ' 
        ' pnlScrollContainer
        ' 
        pnlScrollContainer.AutoScroll = True
        pnlScrollContainer.AutoScrollMinSize = New Size(1200, 1400)
        pnlScrollContainer.BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(247))
        pnlScrollContainer.Controls.Add(pnlContent)
        pnlScrollContainer.Dock = DockStyle.Fill
        pnlScrollContainer.Location = New Point(0, 0)
        pnlScrollContainer.Margin = New Padding(3, 2, 3, 2)
        pnlScrollContainer.Name = "pnlScrollContainer"
        pnlScrollContainer.Padding = New Padding(18, 15, 18, 15)
        pnlScrollContainer.Size = New Size(1166, 591)
        pnlScrollContainer.TabIndex = 0
        ' 
        ' pnlContent
        ' 
        pnlContent.AutoSize = True
        pnlContent.Controls.Add(pnlBottom)
        pnlContent.Controls.Add(pnlAttendance)
        pnlContent.Dock = DockStyle.Top
        pnlContent.Location = New Point(18, 15)
        pnlContent.Margin = New Padding(3, 2, 3, 2)
        pnlContent.MinimumSize = New Size(1094, 0)
        pnlContent.Name = "pnlContent"
        pnlContent.Size = New Size(1200, 1047)
        pnlContent.TabIndex = 0
        ' 
        ' pnlBottom
        ' 
        pnlBottom.BackColor = Color.Transparent
        pnlBottom.BorderRadius = 10
        pnlBottom.Controls.Add(pnlAttendanceStats)
        pnlBottom.Controls.Add(pnlDateFilter)
        pnlBottom.Controls.Add(lblTodayCount)
        pnlBottom.Controls.Add(btnExport)
        pnlBottom.Controls.Add(btnRefresh)
        pnlBottom.Controls.Add(dgvTodayAttendance)
        pnlBottom.Controls.Add(Label5)
        pnlBottom.CustomizableEdges = CustomizableEdges15
        pnlBottom.Dock = DockStyle.Top
        pnlBottom.Location = New Point(0, 488)
        pnlBottom.Margin = New Padding(0, 15, 0, 0)
        pnlBottom.Name = "pnlBottom"
        pnlBottom.Padding = New Padding(18, 15, 18, 15)
        pnlBottom.ShadowDecoration.BorderRadius = 10
        pnlBottom.ShadowDecoration.CustomizableEdges = CustomizableEdges16
        pnlBottom.ShadowDecoration.Depth = 5
        pnlBottom.ShadowDecoration.Enabled = True
        pnlBottom.Size = New Size(1200, 559)
        pnlBottom.TabIndex = 1
        ' 
        ' pnlAttendanceStats
        ' 
        pnlAttendanceStats.BackColor = Color.Transparent
        pnlAttendanceStats.BorderColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        pnlAttendanceStats.BorderRadius = 8
        pnlAttendanceStats.BorderThickness = 1
        pnlAttendanceStats.Controls.Add(lblLateCount)
        pnlAttendanceStats.Controls.Add(lblAbsentCount)
        pnlAttendanceStats.Controls.Add(lblPresentCount)
        pnlAttendanceStats.Controls.Add(Label9)
        pnlAttendanceStats.Controls.Add(Label8)
        pnlAttendanceStats.Controls.Add(Label7)
        pnlAttendanceStats.Controls.Add(Label6)
        pnlAttendanceStats.CustomizableEdges = CustomizableEdges1
        pnlAttendanceStats.Location = New Point(21, 56)
        pnlAttendanceStats.Margin = New Padding(3, 2, 3, 2)
        pnlAttendanceStats.Name = "pnlAttendanceStats"
        pnlAttendanceStats.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        pnlAttendanceStats.Size = New Size(332, 112)
        pnlAttendanceStats.TabIndex = 8
        ' 
        ' lblLateCount
        ' 
        lblLateCount.AutoSize = True
        lblLateCount.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblLateCount.ForeColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        lblLateCount.Location = New Point(245, 71)
        lblLateCount.Name = "lblLateCount"
        lblLateCount.Size = New Size(26, 30)
        lblLateCount.TabIndex = 6
        lblLateCount.Text = "0"
        ' 
        ' lblAbsentCount
        ' 
        lblAbsentCount.AutoSize = True
        lblAbsentCount.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblAbsentCount.ForeColor = Color.FromArgb(CByte(231), CByte(76), CByte(60))
        lblAbsentCount.Location = New Point(149, 71)
        lblAbsentCount.Name = "lblAbsentCount"
        lblAbsentCount.Size = New Size(26, 30)
        lblAbsentCount.TabIndex = 5
        lblAbsentCount.Text = "0"
        ' 
        ' lblPresentCount
        ' 
        lblPresentCount.AutoSize = True
        lblPresentCount.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblPresentCount.ForeColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        lblPresentCount.Location = New Point(52, 71)
        lblPresentCount.Name = "lblPresentCount"
        lblPresentCount.Size = New Size(26, 30)
        lblPresentCount.TabIndex = 4
        lblPresentCount.Text = "0"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 9F)
        Label9.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        Label9.Location = New Point(232, 45)
        Label9.Name = "Label9"
        Label9.Size = New Size(29, 15)
        Label9.TabIndex = 3
        Label9.Text = "Late"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 9F)
        Label8.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        Label8.Location = New Point(136, 45)
        Label8.Name = "Label8"
        Label8.Size = New Size(44, 15)
        Label8.TabIndex = 2
        Label8.Text = "Absent"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 9F)
        Label7.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        Label7.Location = New Point(35, 45)
        Label7.Name = "Label7"
        Label7.Size = New Size(46, 15)
        Label7.TabIndex = 1
        Label7.Text = "Present"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label6.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label6.Location = New Point(13, 11)
        Label6.Name = "Label6"
        Label6.Size = New Size(128, 19)
        Label6.TabIndex = 0
        Label6.Text = "Today's Summary"
        ' 
        ' pnlDateFilter
        ' 
        pnlDateFilter.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnlDateFilter.BackColor = Color.Transparent
        pnlDateFilter.BorderColor = Color.FromArgb(CByte(224), CByte(224), CByte(224))
        pnlDateFilter.BorderRadius = 8
        pnlDateFilter.BorderThickness = 1
        pnlDateFilter.Controls.Add(btnApplyFilter)
        pnlDateFilter.Controls.Add(dtpEndDate)
        pnlDateFilter.Controls.Add(dtpStartDate)
        pnlDateFilter.Controls.Add(Label11)
        pnlDateFilter.Controls.Add(Label10)
        pnlDateFilter.CustomizableEdges = CustomizableEdges9
        pnlDateFilter.Location = New Point(675, 56)
        pnlDateFilter.Margin = New Padding(3, 2, 3, 2)
        pnlDateFilter.Name = "pnlDateFilter"
        pnlDateFilter.Padding = New Padding(13, 11, 13, 11)
        pnlDateFilter.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        pnlDateFilter.Size = New Size(508, 112)
        pnlDateFilter.TabIndex = 7
        ' 
        ' btnApplyFilter
        ' 
        btnApplyFilter.BorderRadius = 5
        btnApplyFilter.CustomizableEdges = CustomizableEdges3
        btnApplyFilter.DisabledState.BorderColor = Color.DarkGray
        btnApplyFilter.DisabledState.CustomBorderColor = Color.DarkGray
        btnApplyFilter.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnApplyFilter.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnApplyFilter.FillColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        btnApplyFilter.Font = New Font("Segoe UI", 9F)
        btnApplyFilter.ForeColor = Color.White
        btnApplyFilter.Location = New Point(385, 71)
        btnApplyFilter.Margin = New Padding(3, 2, 3, 2)
        btnApplyFilter.Name = "btnApplyFilter"
        btnApplyFilter.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        btnApplyFilter.Size = New Size(105, 30)
        btnApplyFilter.TabIndex = 4
        btnApplyFilter.Text = "Apply Filter"
        ' 
        ' dtpEndDate
        ' 
        dtpEndDate.BorderRadius = 5
        dtpEndDate.Checked = True
        dtpEndDate.CustomizableEdges = CustomizableEdges5
        dtpEndDate.FillColor = Color.White
        dtpEndDate.Font = New Font("Segoe UI", 9F)
        dtpEndDate.Format = DateTimePickerFormat.Short
        dtpEndDate.Location = New Point(262, 45)
        dtpEndDate.Margin = New Padding(3, 2, 3, 2)
        dtpEndDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        dtpEndDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        dtpEndDate.Name = "dtpEndDate"
        dtpEndDate.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        dtpEndDate.Size = New Size(228, 27)
        dtpEndDate.TabIndex = 3
        dtpEndDate.Value = New Date(2025, 10, 15, 0, 0, 0, 0)
        ' 
        ' dtpStartDate
        ' 
        dtpStartDate.BorderRadius = 5
        dtpStartDate.Checked = True
        dtpStartDate.CustomizableEdges = CustomizableEdges7
        dtpStartDate.FillColor = Color.White
        dtpStartDate.Font = New Font("Segoe UI", 9F)
        dtpStartDate.Format = DateTimePickerFormat.Short
        dtpStartDate.Location = New Point(18, 45)
        dtpStartDate.Margin = New Padding(3, 2, 3, 2)
        dtpStartDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        dtpStartDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        dtpStartDate.Name = "dtpStartDate"
        dtpStartDate.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        dtpStartDate.Size = New Size(228, 27)
        dtpStartDate.TabIndex = 2
        dtpStartDate.Value = New Date(2025, 10, 15, 0, 0, 0, 0)
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Segoe UI", 9F)
        Label11.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        Label11.Location = New Point(262, 26)
        Label11.Name = "Label11"
        Label11.Size = New Size(54, 15)
        Label11.TabIndex = 1
        Label11.Text = "End Date"
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Font = New Font("Segoe UI", 9F)
        Label10.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        Label10.Location = New Point(18, 26)
        Label10.Name = "Label10"
        Label10.Size = New Size(58, 15)
        Label10.TabIndex = 0
        Label10.Text = "Start Date"
        ' 
        ' lblTodayCount
        ' 
        lblTodayCount.AutoSize = True
        lblTodayCount.Font = New Font("Segoe UI", 9F)
        lblTodayCount.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblTodayCount.Location = New Point(21, 182)
        lblTodayCount.Name = "lblTodayCount"
        lblTodayCount.Size = New Size(165, 15)
        lblTodayCount.TabIndex = 6
        lblTodayCount.Text = "Today's Attendance: 0 records"
        ' 
        ' btnExport
        ' 
        btnExport.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnExport.BorderRadius = 5
        btnExport.CustomizableEdges = CustomizableEdges11
        btnExport.DisabledState.BorderColor = Color.DarkGray
        btnExport.DisabledState.CustomBorderColor = Color.DarkGray
        btnExport.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnExport.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnExport.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnExport.Font = New Font("Segoe UI", 9F)
        btnExport.ForeColor = Color.White
        btnExport.Location = New Point(970, 15)
        btnExport.Margin = New Padding(3, 2, 3, 2)
        btnExport.Name = "btnExport"
        btnExport.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        btnExport.Size = New Size(105, 34)
        btnExport.TabIndex = 5
        btnExport.Text = "Export CSV"
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefresh.BorderRadius = 5
        btnRefresh.CustomizableEdges = CustomizableEdges13
        btnRefresh.DisabledState.BorderColor = Color.DarkGray
        btnRefresh.DisabledState.CustomBorderColor = Color.DarkGray
        btnRefresh.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnRefresh.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnRefresh.FillColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        btnRefresh.Font = New Font("Segoe UI", 9F)
        btnRefresh.ForeColor = Color.White
        btnRefresh.Location = New Point(1080, 15)
        btnRefresh.Margin = New Padding(3, 2, 3, 2)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        btnRefresh.Size = New Size(99, 34)
        btnRefresh.TabIndex = 4
        btnRefresh.Text = "Refresh"
        ' 
        ' dgvTodayAttendance
        ' 
        dgvTodayAttendance.AllowUserToAddRows = False
        dgvTodayAttendance.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.White
        dgvTodayAttendance.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvTodayAttendance.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvTodayAttendance.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvTodayAttendance.ColumnHeadersHeight = 40
        dgvTodayAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.White
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        DataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvTodayAttendance.DefaultCellStyle = DataGridViewCellStyle3
        dgvTodayAttendance.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvTodayAttendance.Location = New Point(21, 206)
        dgvTodayAttendance.Margin = New Padding(3, 2, 3, 2)
        dgvTodayAttendance.Name = "dgvTodayAttendance"
        dgvTodayAttendance.ReadOnly = True
        dgvTodayAttendance.RowHeadersVisible = False
        dgvTodayAttendance.RowHeadersWidth = 51
        dgvTodayAttendance.RowTemplate.Height = 22
        dgvTodayAttendance.Size = New Size(1158, 338)
        dgvTodayAttendance.TabIndex = 3
        dgvTodayAttendance.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White
        dgvTodayAttendance.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        dgvTodayAttendance.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty
        dgvTodayAttendance.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty
        dgvTodayAttendance.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty
        dgvTodayAttendance.ThemeStyle.BackColor = Color.White
        dgvTodayAttendance.ThemeStyle.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvTodayAttendance.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        dgvTodayAttendance.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None
        dgvTodayAttendance.ThemeStyle.HeaderStyle.Font = New Font("Segoe UI", 9F)
        dgvTodayAttendance.ThemeStyle.HeaderStyle.ForeColor = Color.White
        dgvTodayAttendance.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        dgvTodayAttendance.ThemeStyle.HeaderStyle.Height = 40
        dgvTodayAttendance.ThemeStyle.ReadOnly = True
        dgvTodayAttendance.ThemeStyle.RowsStyle.BackColor = Color.White
        dgvTodayAttendance.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvTodayAttendance.ThemeStyle.RowsStyle.Font = New Font("Segoe UI", 9F)
        dgvTodayAttendance.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        dgvTodayAttendance.ThemeStyle.RowsStyle.Height = 22
        dgvTodayAttendance.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvTodayAttendance.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        Label5.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label5.Location = New Point(18, 15)
        Label5.Name = "Label5"
        Label5.Size = New Size(162, 21)
        Label5.TabIndex = 0
        Label5.Text = "Attendance Records"
        ' 
        ' pnlAttendance
        ' 
        pnlAttendance.BackColor = Color.Transparent
        pnlAttendance.BorderRadius = 10
        pnlAttendance.Controls.Add(pnlToast)
        pnlAttendance.Controls.Add(lblStatus)
        pnlAttendance.Controls.Add(txtRemarks)
        pnlAttendance.Controls.Add(Label4)
        pnlAttendance.Controls.Add(btnClear)
        pnlAttendance.Controls.Add(btnExcused)
        pnlAttendance.Controls.Add(btnLate)
        pnlAttendance.Controls.Add(btnAbsent)
        pnlAttendance.Controls.Add(btnPresent)
        pnlAttendance.Controls.Add(lblCourse)
        pnlAttendance.Controls.Add(lblStudentName)
        pnlAttendance.Controls.Add(cmbCourse)
        pnlAttendance.Controls.Add(Label3)
        pnlAttendance.Controls.Add(txtStudentId)
        pnlAttendance.Controls.Add(Label2)
        pnlAttendance.Controls.Add(Label1)
        pnlAttendance.CustomizableEdges = CustomizableEdges35
        pnlAttendance.Dock = DockStyle.Top
        pnlAttendance.Location = New Point(0, 0)
        pnlAttendance.Margin = New Padding(3, 2, 3, 2)
        pnlAttendance.Name = "pnlAttendance"
        pnlAttendance.Padding = New Padding(18, 15, 18, 15)
        pnlAttendance.ShadowDecoration.BorderRadius = 10
        pnlAttendance.ShadowDecoration.CustomizableEdges = CustomizableEdges36
        pnlAttendance.ShadowDecoration.Depth = 5
        pnlAttendance.ShadowDecoration.Enabled = True
        pnlAttendance.Size = New Size(1200, 488)
        pnlAttendance.TabIndex = 0
        ' 
        ' pnlToast
        ' 
        pnlToast.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        pnlToast.BackColor = Color.FromArgb(CByte(46), CByte(204), CByte(113))
        pnlToast.BorderRadius = 8
        pnlToast.Controls.Add(lblToastMessage)
        pnlToast.CustomizableEdges = CustomizableEdges17
        pnlToast.Location = New Point(858, 15)
        pnlToast.Margin = New Padding(3, 2, 3, 2)
        pnlToast.Name = "pnlToast"
        pnlToast.ShadowDecoration.CustomizableEdges = CustomizableEdges18
        pnlToast.Size = New Size(315, 49)
        pnlToast.TabIndex = 15
        pnlToast.Visible = False
        ' 
        ' lblToastMessage
        ' 
        lblToastMessage.Dock = DockStyle.Fill
        lblToastMessage.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        lblToastMessage.ForeColor = Color.White
        lblToastMessage.Location = New Point(0, 0)
        lblToastMessage.Name = "lblToastMessage"
        lblToastMessage.Size = New Size(315, 49)
        lblToastMessage.TabIndex = 0
        lblToastMessage.Text = "Attendance Recorded"
        lblToastMessage.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblStatus
        ' 
        lblStatus.Font = New Font("Segoe UI", 9F, FontStyle.Italic)
        lblStatus.ForeColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        lblStatus.Location = New Point(197, 305)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(700, 19)
        lblStatus.TabIndex = 14
        lblStatus.Text = "✓ Attendance already recorded today"
        lblStatus.TextAlign = ContentAlignment.MiddleCenter
        lblStatus.Visible = False
        ' 
        ' txtRemarks
        ' 
        txtRemarks.BorderRadius = 8
        txtRemarks.Cursor = Cursors.IBeam
        txtRemarks.CustomizableEdges = CustomizableEdges19
        txtRemarks.DefaultText = ""
        txtRemarks.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtRemarks.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtRemarks.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtRemarks.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtRemarks.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtRemarks.Font = New Font("Segoe UI", 10F)
        txtRemarks.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtRemarks.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtRemarks.Location = New Point(221, 380)
        txtRemarks.Margin = New Padding(4, 3, 4, 3)
        txtRemarks.Multiline = True
        txtRemarks.Name = "txtRemarks"
        txtRemarks.PlaceholderText = "Optional remarks..."
        txtRemarks.SelectedText = ""
        txtRemarks.ShadowDecoration.CustomizableEdges = CustomizableEdges20
        txtRemarks.Size = New Size(700, 90)
        txtRemarks.TabIndex = 13
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 10F)
        Label4.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label4.Location = New Point(224, 353)
        Label4.Name = "Label4"
        Label4.Size = New Size(61, 19)
        Label4.TabIndex = 12
        Label4.Text = "Remarks"
        ' 
        ' btnClear
        ' 
        btnClear.BorderRadius = 8
        btnClear.CustomizableEdges = CustomizableEdges21
        btnClear.DisabledState.BorderColor = Color.DarkGray
        btnClear.DisabledState.CustomBorderColor = Color.DarkGray
        btnClear.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnClear.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnClear.FillColor = Color.FromArgb(CByte(149), CByte(165), CByte(166))
        btnClear.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnClear.ForeColor = Color.White
        btnClear.HoverState.FillColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        btnClear.Location = New Point(781, 161)
        btnClear.Margin = New Padding(3, 2, 3, 2)
        btnClear.Name = "btnClear"
        btnClear.ShadowDecoration.CustomizableEdges = CustomizableEdges22
        btnClear.Size = New Size(140, 49)
        btnClear.TabIndex = 11
        btnClear.Text = "Clear"
        ' 
        ' btnExcused
        ' 
        btnExcused.BorderRadius = 8
        btnExcused.CustomizableEdges = CustomizableEdges23
        btnExcused.DisabledState.BorderColor = Color.DarkGray
        btnExcused.DisabledState.CustomBorderColor = Color.DarkGray
        btnExcused.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnExcused.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnExcused.Enabled = False
        btnExcused.FillColor = Color.FromArgb(CByte(155), CByte(89), CByte(182))
        btnExcused.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnExcused.ForeColor = Color.White
        btnExcused.HoverState.FillColor = Color.FromArgb(CByte(142), CByte(68), CByte(173))
        btnExcused.Location = New Point(343, 161)
        btnExcused.Margin = New Padding(3, 2, 3, 2)
        btnExcused.Name = "btnExcused"
        btnExcused.ShadowDecoration.CustomizableEdges = CustomizableEdges24
        btnExcused.Size = New Size(140, 49)
        btnExcused.TabIndex = 10
        btnExcused.Text = "Excused"
        ' 
        ' btnLate
        ' 
        btnLate.BorderRadius = 8
        btnLate.CustomizableEdges = CustomizableEdges25
        btnLate.DisabledState.BorderColor = Color.DarkGray
        btnLate.DisabledState.CustomBorderColor = Color.DarkGray
        btnLate.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnLate.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnLate.Enabled = False
        btnLate.FillColor = Color.FromArgb(CByte(243), CByte(156), CByte(18))
        btnLate.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnLate.ForeColor = Color.White
        btnLate.HoverState.FillColor = Color.FromArgb(CByte(211), CByte(84), CByte(0))
        btnLate.Location = New Point(635, 161)
        btnLate.Margin = New Padding(3, 2, 3, 2)
        btnLate.Name = "btnLate"
        btnLate.ShadowDecoration.CustomizableEdges = CustomizableEdges26
        btnLate.Size = New Size(140, 49)
        btnLate.TabIndex = 9
        btnLate.Text = "Late"
        ' 
        ' btnAbsent
        ' 
        btnAbsent.BorderRadius = 8
        btnAbsent.CustomizableEdges = CustomizableEdges27
        btnAbsent.DisabledState.BorderColor = Color.DarkGray
        btnAbsent.DisabledState.CustomBorderColor = Color.DarkGray
        btnAbsent.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnAbsent.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnAbsent.Enabled = False
        btnAbsent.FillColor = Color.FromArgb(CByte(231), CByte(76), CByte(60))
        btnAbsent.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnAbsent.ForeColor = Color.White
        btnAbsent.HoverState.FillColor = Color.FromArgb(CByte(192), CByte(57), CByte(43))
        btnAbsent.Location = New Point(489, 161)
        btnAbsent.Margin = New Padding(3, 2, 3, 2)
        btnAbsent.Name = "btnAbsent"
        btnAbsent.ShadowDecoration.CustomizableEdges = CustomizableEdges28
        btnAbsent.Size = New Size(140, 49)
        btnAbsent.TabIndex = 8
        btnAbsent.Text = "Absent"
        ' 
        ' btnPresent
        ' 
        btnPresent.BorderRadius = 8
        btnPresent.CustomizableEdges = CustomizableEdges29
        btnPresent.DisabledState.BorderColor = Color.DarkGray
        btnPresent.DisabledState.CustomBorderColor = Color.DarkGray
        btnPresent.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnPresent.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnPresent.Enabled = False
        btnPresent.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnPresent.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnPresent.ForeColor = Color.White
        btnPresent.HoverState.FillColor = Color.FromArgb(CByte(39), CByte(118), CByte(74))
        btnPresent.Location = New Point(197, 161)
        btnPresent.Margin = New Padding(3, 2, 3, 2)
        btnPresent.Name = "btnPresent"
        btnPresent.ShadowDecoration.CustomizableEdges = CustomizableEdges30
        btnPresent.Size = New Size(140, 49)
        btnPresent.TabIndex = 7
        btnPresent.Text = "Present"
        ' 
        ' lblCourse
        ' 
        lblCourse.Font = New Font("Segoe UI", 9.5F)
        lblCourse.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblCourse.Location = New Point(197, 286)
        lblCourse.Name = "lblCourse"
        lblCourse.Size = New Size(700, 19)
        lblCourse.TabIndex = 6
        lblCourse.Text = "Course: Computer Science"
        lblCourse.TextAlign = ContentAlignment.MiddleCenter
        lblCourse.Visible = False
        ' 
        ' lblStudentName
        ' 
        lblStudentName.Font = New Font("Segoe UI", 16F, FontStyle.Bold)
        lblStudentName.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblStudentName.Location = New Point(197, 256)
        lblStudentName.Name = "lblStudentName"
        lblStudentName.Size = New Size(700, 30)
        lblStudentName.TabIndex = 5
        lblStudentName.Text = "Enter Student ID"
        lblStudentName.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' cmbCourse
        ' 
        cmbCourse.BackColor = Color.Transparent
        cmbCourse.BorderColor = Color.FromArgb(CByte(213), CByte(218), CByte(223))
        cmbCourse.BorderRadius = 8
        cmbCourse.CustomizableEdges = CustomizableEdges31
        cmbCourse.DrawMode = DrawMode.OwnerDrawFixed
        cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCourse.FocusedColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        cmbCourse.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        cmbCourse.Font = New Font("Segoe UI", 10F)
        cmbCourse.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        cmbCourse.ItemHeight = 30
        cmbCourse.Location = New Point(560, 98)
        cmbCourse.Margin = New Padding(3, 2, 3, 2)
        cmbCourse.Name = "cmbCourse"
        cmbCourse.ShadowDecoration.CustomizableEdges = CustomizableEdges32
        cmbCourse.Size = New Size(350, 36)
        cmbCourse.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 10F)
        Label3.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label3.Location = New Point(560, 71)
        Label3.Name = "Label3"
        Label3.Size = New Size(52, 19)
        Label3.TabIndex = 3
        Label3.Text = "Course"
        ' 
        ' txtStudentId
        ' 
        txtStudentId.BorderRadius = 8
        txtStudentId.Cursor = Cursors.IBeam
        txtStudentId.CustomizableEdges = CustomizableEdges33
        txtStudentId.DefaultText = ""
        txtStudentId.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtStudentId.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtStudentId.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtStudentId.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtStudentId.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtStudentId.Font = New Font("Segoe UI", 10F)
        txtStudentId.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtStudentId.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtStudentId.Location = New Point(210, 98)
        txtStudentId.Margin = New Padding(4, 3, 4, 3)
        txtStudentId.Name = "txtStudentId"
        txtStudentId.PlaceholderText = "Enter student ID..."
        txtStudentId.SelectedText = ""
        txtStudentId.ShadowDecoration.CustomizableEdges = CustomizableEdges34
        txtStudentId.Size = New Size(324, 45)
        txtStudentId.TabIndex = 2
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 10F)
        Label2.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label2.Location = New Point(210, 71)
        Label2.Name = "Label2"
        Label2.Size = New Size(75, 19)
        Label2.TabIndex = 1
        Label2.Text = "Student ID"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        Label1.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label1.Location = New Point(18, 15)
        Label1.Name = "Label1"
        Label1.Size = New Size(182, 25)
        Label1.TabIndex = 0
        Label1.Text = "Record Attendance"
        ' 
        ' tmrStudentIdDebounce
        ' 
        tmrStudentIdDebounce.Interval = 500
        ' 
        ' tmrToast
        ' 
        tmrToast.Interval = 3000
        ' 
        ' tmrResetForm
        ' 
        tmrResetForm.Interval = 2000
        ' 
        ' AttendanceForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1166, 591)
        Controls.Add(pnlScrollContainer)
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(3, 2, 3, 2)
        Name = "AttendanceForm"
        Text = "Attendance - Student Management System"
        pnlScrollContainer.ResumeLayout(False)
        pnlScrollContainer.PerformLayout()
        pnlContent.ResumeLayout(False)
        pnlBottom.ResumeLayout(False)
        pnlBottom.PerformLayout()
        pnlAttendanceStats.ResumeLayout(False)
        pnlAttendanceStats.PerformLayout()
        pnlDateFilter.ResumeLayout(False)
        pnlDateFilter.PerformLayout()
        CType(dgvTodayAttendance, ComponentModel.ISupportInitialize).EndInit()
        pnlAttendance.ResumeLayout(False)
        pnlAttendance.PerformLayout()
        pnlToast.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents pnlScrollContainer As Panel
    Friend WithEvents pnlContent As Panel
    Friend WithEvents pnlAttendance As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtStudentId As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbCourse As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents lblStudentName As Label
    Friend WithEvents lblCourse As Label
    Friend WithEvents btnPresent As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnAbsent As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnLate As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnExcused As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnClear As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtRemarks As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents pnlToast As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblToastMessage As Label
    Friend WithEvents pnlBottom As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents dgvTodayAttendance As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents btnRefresh As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnExport As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblTodayCount As Label
    Friend WithEvents pnlDateFilter As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents dtpStartDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents dtpEndDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents btnApplyFilter As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlAttendanceStats As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lblPresentCount As Label
    Friend WithEvents lblAbsentCount As Label
    Friend WithEvents lblLateCount As Label
    Friend WithEvents tmrStudentIdDebounce As Timer
    Friend WithEvents tmrToast As Timer
    Friend WithEvents tmrResetForm As Timer
End Class