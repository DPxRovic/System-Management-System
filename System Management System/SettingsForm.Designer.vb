' ==========================================
' FILENAME: /Forms/SimpleSettingsForm.Designer.vb
' PURPOSE: Designer file for SimpleSettingsForm with modern Guna controls
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SettingsForm
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
        Dim CustomizableEdges23 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges24 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges15 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges16 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges11 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges12 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges21 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges22 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges17 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges18 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges19 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges20 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        pnlMain = New Guna.UI2.WinForms.Guna2Panel()
        pnlContent = New Guna.UI2.WinForms.Guna2Panel()
        grpNotifications = New Guna.UI2.WinForms.Guna2GroupBox()
        chkEnableNotifications = New Guna.UI2.WinForms.Guna2CheckBox()
        lblNotificationDuration = New Label()
        nudNotificationDuration = New Guna.UI2.WinForms.Guna2NumericUpDown()
        btnTestNotification = New Guna.UI2.WinForms.Guna2Button()
        grpDatabase = New Guna.UI2.WinForms.Guna2GroupBox()
        chkAutoBackup = New Guna.UI2.WinForms.Guna2CheckBox()
        lblBackupInterval = New Label()
        nudBackupInterval = New Guna.UI2.WinForms.Guna2NumericUpDown()
        lblLastBackup = New Label()
        btnBackupNow = New Guna.UI2.WinForms.Guna2Button()
        grpPreferences = New Guna.UI2.WinForms.Guna2GroupBox()
        chkRememberUsername = New Guna.UI2.WinForms.Guna2CheckBox()
        pnlBottom = New Guna.UI2.WinForms.Guna2Panel()
        btnSave = New Guna.UI2.WinForms.Guna2Button()
        btnCancel = New Guna.UI2.WinForms.Guna2Button()
        lblTitle = New Label()
        Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(components)
        Guna2ShadowForm1 = New Guna.UI2.WinForms.Guna2ShadowForm(components)
        pnlMain.SuspendLayout()
        pnlContent.SuspendLayout()
        grpNotifications.SuspendLayout()
        CType(nudNotificationDuration, ComponentModel.ISupportInitialize).BeginInit()
        grpDatabase.SuspendLayout()
        CType(nudBackupInterval, ComponentModel.ISupportInitialize).BeginInit()
        grpPreferences.SuspendLayout()
        pnlBottom.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.White
        pnlMain.BorderRadius = 10
        pnlMain.Controls.Add(pnlContent)
        pnlMain.Controls.Add(pnlBottom)
        pnlMain.Controls.Add(lblTitle)
        pnlMain.CustomizableEdges = CustomizableEdges23
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(20, 20)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(20)
        pnlMain.ShadowDecoration.CustomizableEdges = CustomizableEdges24
        pnlMain.Size = New Size(960, 840)
        pnlMain.TabIndex = 0
        ' 
        ' pnlContent
        ' 
        pnlContent.AutoScroll = True
        pnlContent.BackColor = Color.Transparent
        pnlContent.Controls.Add(grpNotifications)
        pnlContent.Controls.Add(grpDatabase)
        pnlContent.Controls.Add(grpPreferences)
        pnlContent.CustomizableEdges = CustomizableEdges15
        pnlContent.Dock = DockStyle.Fill
        pnlContent.Location = New Point(20, 81)
        pnlContent.Name = "pnlContent"
        pnlContent.ShadowDecoration.CustomizableEdges = CustomizableEdges16
        pnlContent.Size = New Size(920, 659)
        pnlContent.TabIndex = 1
        ' 
        ' grpNotifications
        ' 
        grpNotifications.BorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        grpNotifications.BorderRadius = 10
        grpNotifications.BorderThickness = 2
        grpNotifications.Controls.Add(chkEnableNotifications)
        grpNotifications.Controls.Add(lblNotificationDuration)
        grpNotifications.Controls.Add(nudNotificationDuration)
        grpNotifications.Controls.Add(btnTestNotification)
        grpNotifications.CustomBorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        grpNotifications.CustomizableEdges = CustomizableEdges5
        grpNotifications.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        grpNotifications.ForeColor = Color.White
        grpNotifications.Location = New Point(20, 20)
        grpNotifications.Name = "grpNotifications"
        grpNotifications.Padding = New Padding(10)
        grpNotifications.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        grpNotifications.Size = New Size(860, 170)
        grpNotifications.TabIndex = 0
        grpNotifications.Text = "🔔 Notifications"
        ' 
        ' chkEnableNotifications
        ' 
        chkEnableNotifications.AutoSize = True
        chkEnableNotifications.CheckedState.BorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        chkEnableNotifications.CheckedState.BorderRadius = 2
        chkEnableNotifications.CheckedState.BorderThickness = 0
        chkEnableNotifications.CheckedState.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        chkEnableNotifications.Font = New Font("Segoe UI", 10.5F)
        chkEnableNotifications.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        chkEnableNotifications.Location = New Point(25, 55)
        chkEnableNotifications.Name = "chkEnableNotifications"
        chkEnableNotifications.Size = New Size(191, 29)
        chkEnableNotifications.TabIndex = 0
        chkEnableNotifications.Text = "Enable Notifications"
        chkEnableNotifications.UncheckedState.BorderColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        chkEnableNotifications.UncheckedState.BorderRadius = 2
        chkEnableNotifications.UncheckedState.BorderThickness = 0
        chkEnableNotifications.UncheckedState.FillColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        ' 
        ' lblNotificationDuration
        ' 
        lblNotificationDuration.AutoSize = True
        lblNotificationDuration.Font = New Font("Segoe UI", 10.5F)
        lblNotificationDuration.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblNotificationDuration.Location = New Point(25, 95)
        lblNotificationDuration.Name = "lblNotificationDuration"
        lblNotificationDuration.Size = New Size(197, 25)
        lblNotificationDuration.TabIndex = 1
        lblNotificationDuration.Text = "Duration (milliseconds):"
        ' 
        ' nudNotificationDuration
        ' 
        nudNotificationDuration.BackColor = Color.Transparent
        nudNotificationDuration.BorderRadius = 8
        nudNotificationDuration.Cursor = Cursors.IBeam
        nudNotificationDuration.CustomizableEdges = CustomizableEdges1
        nudNotificationDuration.Font = New Font("Segoe UI", 10.5F)
        nudNotificationDuration.Location = New Point(230, 90)
        nudNotificationDuration.Margin = New Padding(3, 4, 3, 4)
        nudNotificationDuration.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        nudNotificationDuration.Minimum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudNotificationDuration.Name = "nudNotificationDuration"
        nudNotificationDuration.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        nudNotificationDuration.Size = New Size(120, 36)
        nudNotificationDuration.TabIndex = 2
        nudNotificationDuration.Value = New Decimal(New Integer() {3000, 0, 0, 0})
        ' 
        ' btnTestNotification
        ' 
        btnTestNotification.BorderRadius = 8
        btnTestNotification.CustomizableEdges = CustomizableEdges3
        btnTestNotification.DisabledState.BorderColor = Color.DarkGray
        btnTestNotification.DisabledState.CustomBorderColor = Color.DarkGray
        btnTestNotification.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnTestNotification.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnTestNotification.FillColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        btnTestNotification.Font = New Font("Segoe UI", 10.5F)
        btnTestNotification.ForeColor = Color.White
        btnTestNotification.HoverState.FillColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnTestNotification.Location = New Point(380, 90)
        btnTestNotification.Name = "btnTestNotification"
        btnTestNotification.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        btnTestNotification.Size = New Size(180, 38)
        btnTestNotification.TabIndex = 3
        btnTestNotification.Text = ChrW(55358) & ChrW(56810) & " Test Notification"
        btnTestNotification.TextOffset = New Point(0, 3)
        ' 
        ' grpDatabase
        ' 
        grpDatabase.BorderColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        grpDatabase.BorderRadius = 10
        grpDatabase.BorderThickness = 2
        grpDatabase.Controls.Add(chkAutoBackup)
        grpDatabase.Controls.Add(lblBackupInterval)
        grpDatabase.Controls.Add(nudBackupInterval)
        grpDatabase.Controls.Add(lblLastBackup)
        grpDatabase.Controls.Add(btnBackupNow)
        grpDatabase.CustomBorderColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        grpDatabase.CustomizableEdges = CustomizableEdges11
        grpDatabase.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        grpDatabase.ForeColor = Color.White
        grpDatabase.Location = New Point(20, 210)
        grpDatabase.Name = "grpDatabase"
        grpDatabase.Padding = New Padding(10)
        grpDatabase.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        grpDatabase.Size = New Size(860, 200)
        grpDatabase.TabIndex = 1
        grpDatabase.Text = "💾 Database"
        ' 
        ' chkAutoBackup
        ' 
        chkAutoBackup.AutoSize = True
        chkAutoBackup.CheckedState.BorderColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        chkAutoBackup.CheckedState.BorderRadius = 2
        chkAutoBackup.CheckedState.BorderThickness = 0
        chkAutoBackup.CheckedState.FillColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        chkAutoBackup.Font = New Font("Segoe UI", 10.5F)
        chkAutoBackup.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        chkAutoBackup.Location = New Point(25, 55)
        chkAutoBackup.Name = "chkAutoBackup"
        chkAutoBackup.Size = New Size(235, 29)
        chkAutoBackup.TabIndex = 0
        chkAutoBackup.Text = "Enable Automatic Backup"
        chkAutoBackup.UncheckedState.BorderColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        chkAutoBackup.UncheckedState.BorderRadius = 2
        chkAutoBackup.UncheckedState.BorderThickness = 0
        chkAutoBackup.UncheckedState.FillColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        ' 
        ' lblBackupInterval
        ' 
        lblBackupInterval.AutoSize = True
        lblBackupInterval.Font = New Font("Segoe UI", 10.5F)
        lblBackupInterval.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblBackupInterval.Location = New Point(25, 95)
        lblBackupInterval.Name = "lblBackupInterval"
        lblBackupInterval.Size = New Size(188, 25)
        lblBackupInterval.TabIndex = 1
        lblBackupInterval.Text = "Backup Interval (days):"
        ' 
        ' nudBackupInterval
        ' 
        nudBackupInterval.BackColor = Color.Transparent
        nudBackupInterval.BorderRadius = 8
        nudBackupInterval.Cursor = Cursors.IBeam
        nudBackupInterval.CustomizableEdges = CustomizableEdges7
        nudBackupInterval.Font = New Font("Segoe UI", 10.5F)
        nudBackupInterval.Location = New Point(230, 90)
        nudBackupInterval.Margin = New Padding(3, 4, 3, 4)
        nudBackupInterval.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
        nudBackupInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudBackupInterval.Name = "nudBackupInterval"
        nudBackupInterval.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        nudBackupInterval.Size = New Size(120, 36)
        nudBackupInterval.TabIndex = 2
        nudBackupInterval.Value = New Decimal(New Integer() {7, 0, 0, 0})
        ' 
        ' lblLastBackup
        ' 
        lblLastBackup.AutoSize = True
        lblLastBackup.Font = New Font("Segoe UI", 9.0F, FontStyle.Italic)
        lblLastBackup.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblLastBackup.Location = New Point(25, 140)
        lblLastBackup.Name = "lblLastBackup"
        lblLastBackup.Size = New Size(129, 20)
        lblLastBackup.TabIndex = 3
        lblLastBackup.Text = "Last backup: Never"
        ' 
        ' btnBackupNow
        ' 
        btnBackupNow.BorderRadius = 8
        btnBackupNow.CustomizableEdges = CustomizableEdges9
        btnBackupNow.DisabledState.BorderColor = Color.DarkGray
        btnBackupNow.DisabledState.CustomBorderColor = Color.DarkGray
        btnBackupNow.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnBackupNow.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnBackupNow.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnBackupNow.Font = New Font("Segoe UI", 10.5F)
        btnBackupNow.ForeColor = Color.White
        btnBackupNow.HoverState.FillColor = Color.FromArgb(CByte(39), CByte(118), CByte(74))
        btnBackupNow.Location = New Point(380, 88)
        btnBackupNow.Name = "btnBackupNow"
        btnBackupNow.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        btnBackupNow.Size = New Size(180, 40)
        btnBackupNow.TabIndex = 4
        btnBackupNow.Text = "💾 Backup Now"
        ' 
        ' grpPreferences
        ' 
        grpPreferences.BorderColor = Color.FromArgb(CByte(155), CByte(89), CByte(182))
        grpPreferences.BorderRadius = 10
        grpPreferences.BorderThickness = 2
        grpPreferences.Controls.Add(chkRememberUsername)
        grpPreferences.CustomBorderColor = Color.FromArgb(CByte(155), CByte(89), CByte(182))
        grpPreferences.CustomizableEdges = CustomizableEdges13
        grpPreferences.Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        grpPreferences.ForeColor = Color.White
        grpPreferences.Location = New Point(20, 430)
        grpPreferences.Name = "grpPreferences"
        grpPreferences.Padding = New Padding(10)
        grpPreferences.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        grpPreferences.Size = New Size(860, 110)
        grpPreferences.TabIndex = 2
        grpPreferences.Text = "👤 Preferences"
        ' 
        ' chkRememberUsername
        ' 
        chkRememberUsername.AutoSize = True
        chkRememberUsername.CheckedState.BorderColor = Color.FromArgb(CByte(155), CByte(89), CByte(182))
        chkRememberUsername.CheckedState.BorderRadius = 2
        chkRememberUsername.CheckedState.BorderThickness = 0
        chkRememberUsername.CheckedState.FillColor = Color.FromArgb(CByte(155), CByte(89), CByte(182))
        chkRememberUsername.Font = New Font("Segoe UI", 10.5F)
        chkRememberUsername.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        chkRememberUsername.Location = New Point(25, 55)
        chkRememberUsername.Name = "chkRememberUsername"
        chkRememberUsername.Size = New Size(279, 29)
        chkRememberUsername.TabIndex = 0
        chkRememberUsername.Text = "Remember Username on Login"
        chkRememberUsername.UncheckedState.BorderColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        chkRememberUsername.UncheckedState.BorderRadius = 2
        chkRememberUsername.UncheckedState.BorderThickness = 0
        chkRememberUsername.UncheckedState.FillColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        ' 
        ' pnlBottom
        ' 
        pnlBottom.Controls.Add(btnSave)
        pnlBottom.Controls.Add(btnCancel)
        pnlBottom.CustomizableEdges = CustomizableEdges21
        pnlBottom.Dock = DockStyle.Bottom
        pnlBottom.Location = New Point(20, 740)
        pnlBottom.Name = "pnlBottom"
        pnlBottom.Padding = New Padding(0, 20, 0, 0)
        pnlBottom.ShadowDecoration.CustomizableEdges = CustomizableEdges22
        pnlBottom.Size = New Size(920, 80)
        pnlBottom.TabIndex = 2
        ' 
        ' btnSave
        ' 
        btnSave.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSave.BorderRadius = 8
        btnSave.CustomizableEdges = CustomizableEdges17
        btnSave.DisabledState.BorderColor = Color.DarkGray
        btnSave.DisabledState.CustomBorderColor = Color.DarkGray
        btnSave.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnSave.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnSave.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnSave.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        btnSave.ForeColor = Color.White
        btnSave.HoverState.FillColor = Color.FromArgb(CByte(39), CByte(118), CByte(74))
        btnSave.Location = New Point(750, 25)
        btnSave.Name = "btnSave"
        btnSave.ShadowDecoration.CustomizableEdges = CustomizableEdges18
        btnSave.Size = New Size(150, 45)
        btnSave.TabIndex = 0
        btnSave.Text = "💾 Save"
        ' 
        ' btnCancel
        ' 
        btnCancel.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnCancel.BorderRadius = 8
        btnCancel.CustomizableEdges = CustomizableEdges19
        btnCancel.DisabledState.BorderColor = Color.DarkGray
        btnCancel.DisabledState.CustomBorderColor = Color.DarkGray
        btnCancel.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnCancel.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnCancel.FillColor = Color.FromArgb(CByte(149), CByte(165), CByte(166))
        btnCancel.Font = New Font("Segoe UI", 10.5F)
        btnCancel.ForeColor = Color.White
        btnCancel.HoverState.FillColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        btnCancel.Location = New Point(580, 25)
        btnCancel.Name = "btnCancel"
        btnCancel.ShadowDecoration.CustomizableEdges = CustomizableEdges20
        btnCancel.Size = New Size(150, 45)
        btnCancel.TabIndex = 1
        btnCancel.Text = "Cancel"
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Dock = DockStyle.Top
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblTitle.Location = New Point(20, 20)
        lblTitle.Name = "lblTitle"
        lblTitle.Padding = New Padding(0, 0, 0, 20)
        lblTitle.Size = New Size(184, 61)
        lblTitle.TabIndex = 0
        lblTitle.Text = "⚙ Settings"
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 15
        Guna2Elipse1.TargetControl = Me
        ' 
        ' Guna2ShadowForm1
        ' 
        Guna2ShadowForm1.TargetForm = Me
        ' 
        ' SettingsForm
        ' 
        AutoScaleDimensions = New SizeF(9.0F, 23.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(247))
        ClientSize = New Size(1000, 880)
        Controls.Add(pnlMain)
        Font = New Font("Segoe UI", 10.5F)
        FormBorderStyle = FormBorderStyle.None
        Name = "SettingsForm"
        Padding = New Padding(20)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Settings"
        pnlMain.ResumeLayout(False)
        pnlMain.PerformLayout()
        pnlContent.ResumeLayout(False)
        grpNotifications.ResumeLayout(False)
        grpNotifications.PerformLayout()
        CType(nudNotificationDuration, ComponentModel.ISupportInitialize).EndInit()
        grpDatabase.ResumeLayout(False)
        grpDatabase.PerformLayout()
        CType(nudBackupInterval, ComponentModel.ISupportInitialize).EndInit()
        grpPreferences.ResumeLayout(False)
        grpPreferences.PerformLayout()
        pnlBottom.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents pnlMain As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlContent As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents grpNotifications As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents chkEnableNotifications As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents lblNotificationDuration As Label
    Friend WithEvents nudNotificationDuration As Guna.UI2.WinForms.Guna2NumericUpDown
    Friend WithEvents btnTestNotification As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents grpDatabase As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents chkAutoBackup As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents lblBackupInterval As Label
    Friend WithEvents nudBackupInterval As Guna.UI2.WinForms.Guna2NumericUpDown
    Friend WithEvents lblLastBackup As Label
    Friend WithEvents btnBackupNow As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents grpPreferences As Guna.UI2.WinForms.Guna2GroupBox
    Friend WithEvents chkRememberUsername As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents pnlBottom As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnSave As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCancel As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents Guna2ShadowForm1 As Guna.UI2.WinForms.Guna2ShadowForm

End Class