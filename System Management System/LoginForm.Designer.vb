' ==========================================
' FILENAME: /Forms/LoginForm.Designer.vb
' PURPOSE: Designer file for LoginForm with Guna.UI2 controls
' AUTHOR: System
' DATE: 2025-10-14
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LoginForm
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
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges11 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges12 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(components)
        pnlMain = New Guna.UI2.WinForms.Guna2Panel()
        pnlLogin = New Guna.UI2.WinForms.Guna2Panel()
        btnExit = New Guna.UI2.WinForms.Guna2Button()
        btnLogin = New Guna.UI2.WinForms.Guna2Button()
        txtPassword = New Guna.UI2.WinForms.Guna2TextBox()
        txtUsername = New Guna.UI2.WinForms.Guna2TextBox()
        lblSubtitle = New Label()
        lblTitle = New Label()
        pnlHeader = New Guna.UI2.WinForms.Guna2Panel()
        lblAppName = New Label()
        Guna2DragControl1 = New Guna.UI2.WinForms.Guna2DragControl(components)
        Guna2ShadowForm1 = New Guna.UI2.WinForms.Guna2ShadowForm(components)
        chkShowPassword = New Guna.UI2.WinForms.Guna2CheckBox()
        pnlMain.SuspendLayout()
        pnlLogin.SuspendLayout()
        pnlHeader.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 20
        Guna2Elipse1.TargetControl = Me
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(247))
        pnlMain.Controls.Add(pnlLogin)
        pnlMain.Controls.Add(pnlHeader)
        pnlMain.CustomizableEdges = CustomizableEdges13
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Margin = New Padding(4, 5, 4, 5)
        pnlMain.Name = "pnlMain"
        pnlMain.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        pnlMain.Size = New Size(600, 923)
        pnlMain.TabIndex = 0
        ' 
        ' pnlLogin
        ' 
        pnlLogin.BackColor = Color.Transparent
        pnlLogin.BorderRadius = 15
        pnlLogin.Controls.Add(chkShowPassword)
        pnlLogin.Controls.Add(btnExit)
        pnlLogin.Controls.Add(btnLogin)
        pnlLogin.Controls.Add(txtPassword)
        pnlLogin.Controls.Add(txtUsername)
        pnlLogin.Controls.Add(lblSubtitle)
        pnlLogin.Controls.Add(lblTitle)
        pnlLogin.CustomizableEdges = CustomizableEdges9
        pnlLogin.Location = New Point(53, 277)
        pnlLogin.Margin = New Padding(4, 5, 4, 5)
        pnlLogin.Name = "pnlLogin"
        pnlLogin.ShadowDecoration.BorderRadius = 15
        pnlLogin.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        pnlLogin.ShadowDecoration.Depth = 10
        pnlLogin.ShadowDecoration.Enabled = True
        pnlLogin.Size = New Size(493, 585)
        pnlLogin.TabIndex = 1
        ' 
        ' btnExit
        ' 
        btnExit.BorderRadius = 8
        btnExit.CustomizableEdges = CustomizableEdges1
        btnExit.DisabledState.BorderColor = Color.DarkGray
        btnExit.DisabledState.CustomBorderColor = Color.DarkGray
        btnExit.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnExit.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnExit.FillColor = Color.FromArgb(CByte(231), CByte(76), CByte(60))
        btnExit.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        btnExit.ForeColor = Color.White
        btnExit.Location = New Point(40, 485)
        btnExit.Margin = New Padding(4, 5, 4, 5)
        btnExit.Name = "btnExit"
        btnExit.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        btnExit.Size = New Size(413, 62)
        btnExit.TabIndex = 4
        btnExit.Text = "Exit"
        ' 
        ' btnLogin
        ' 
        btnLogin.BorderRadius = 8
        btnLogin.CustomizableEdges = CustomizableEdges3
        btnLogin.DisabledState.BorderColor = Color.DarkGray
        btnLogin.DisabledState.CustomBorderColor = Color.DarkGray
        btnLogin.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnLogin.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnLogin.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnLogin.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        btnLogin.ForeColor = Color.White
        btnLogin.HoverState.FillColor = Color.FromArgb(CByte(39), CByte(118), CByte(74))
        btnLogin.Location = New Point(40, 400)
        btnLogin.Margin = New Padding(4, 5, 4, 5)
        btnLogin.Name = "btnLogin"
        btnLogin.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        btnLogin.Size = New Size(413, 69)
        btnLogin.TabIndex = 3
        btnLogin.Text = "Login"
        ' 
        ' txtPassword
        ' 
        txtPassword.BorderRadius = 8
        txtPassword.Cursor = Cursors.IBeam
        txtPassword.CustomizableEdges = CustomizableEdges5
        txtPassword.DefaultText = ""
        txtPassword.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtPassword.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtPassword.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPassword.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtPassword.Font = New Font("Segoe UI", 10.5F)
        txtPassword.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtPassword.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtPassword.Location = New Point(40, 292)
        txtPassword.Margin = New Padding(5, 8, 5, 8)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "●"c
        txtPassword.PlaceholderForeColor = Color.Silver
        txtPassword.PlaceholderText = "Password"
        txtPassword.SelectedText = ""
        txtPassword.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        txtPassword.Size = New Size(413, 69)
        txtPassword.TabIndex = 2
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' txtUsername
        ' 
        txtUsername.BorderRadius = 8
        txtUsername.Cursor = Cursors.IBeam
        txtUsername.CustomizableEdges = CustomizableEdges7
        txtUsername.DefaultText = ""
        txtUsername.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtUsername.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtUsername.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtUsername.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtUsername.Font = New Font("Segoe UI", 10.5F)
        txtUsername.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtUsername.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtUsername.Location = New Point(40, 185)
        txtUsername.Margin = New Padding(5, 8, 5, 8)
        txtUsername.Name = "txtUsername"
        txtUsername.PlaceholderForeColor = Color.Silver
        txtUsername.PlaceholderText = "Username"
        txtUsername.SelectedText = ""
        txtUsername.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        txtUsername.Size = New Size(413, 69)
        txtUsername.TabIndex = 1
        ' 
        ' lblSubtitle
        ' 
        lblSubtitle.Font = New Font("Segoe UI", 9.75F)
        lblSubtitle.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblSubtitle.Location = New Point(36, 108)
        lblSubtitle.Margin = New Padding(4, 0, 4, 0)
        lblSubtitle.Name = "lblSubtitle"
        lblSubtitle.Size = New Size(421, 38)
        lblSubtitle.TabIndex = 1
        lblSubtitle.Text = "Enter your credentials to continue"
        lblSubtitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblTitle
        ' 
        lblTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblTitle.Location = New Point(33, 46)
        lblTitle.Margin = New Padding(4, 0, 4, 0)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(427, 62)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Welcome Back!"
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BackColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        pnlHeader.Controls.Add(lblAppName)
        pnlHeader.CustomizableEdges = CustomizableEdges11
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Margin = New Padding(4, 5, 4, 5)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        pnlHeader.Size = New Size(600, 185)
        pnlHeader.TabIndex = 0
        ' 
        ' lblAppName
        ' 
        lblAppName.Dock = DockStyle.Fill
        lblAppName.Font = New Font("Segoe UI", 24F, FontStyle.Bold)
        lblAppName.ForeColor = Color.White
        lblAppName.Location = New Point(0, 0)
        lblAppName.Margin = New Padding(4, 0, 4, 0)
        lblAppName.Name = "lblAppName"
        lblAppName.Size = New Size(600, 185)
        lblAppName.TabIndex = 0
        lblAppName.Text = "Student Management" & vbCrLf & "System"
        lblAppName.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Guna2DragControl1
        ' 
        Guna2DragControl1.DockIndicatorTransparencyValue = 0.6R
        Guna2DragControl1.TargetControl = pnlHeader
        Guna2DragControl1.UseTransparentDrag = True
        ' 
        ' Guna2ShadowForm1
        ' 
        Guna2ShadowForm1.TargetForm = Me
        ' 
        ' chkShowPassword
        ' 
        chkShowPassword.AutoSize = True
        chkShowPassword.CheckedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        chkShowPassword.CheckedState.BorderRadius = 0
        chkShowPassword.CheckedState.BorderThickness = 0
        chkShowPassword.CheckedState.FillColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        chkShowPassword.Font = New Font("Segoe UI", 9F)
        chkShowPassword.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        chkShowPassword.Location = New Point(40, 366)
        chkShowPassword.Margin = New Padding(4, 5, 4, 5)
        chkShowPassword.Name = "chkShowPassword"
        chkShowPassword.Size = New Size(132, 24)
        chkShowPassword.TabIndex = 12
        chkShowPassword.Text = "Show Password"
        chkShowPassword.UncheckedState.BorderColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        chkShowPassword.UncheckedState.BorderRadius = 0
        chkShowPassword.UncheckedState.BorderThickness = 0
        chkShowPassword.UncheckedState.FillColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        ' 
        ' LoginForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(600, 923)
        Controls.Add(pnlMain)
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(4, 5, 4, 5)
        Name = "LoginForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Login - Student Management System"
        pnlMain.ResumeLayout(False)
        pnlLogin.ResumeLayout(False)
        pnlLogin.PerformLayout()
        pnlHeader.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents pnlMain As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlLogin As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents txtUsername As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txtPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnLogin As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnExit As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents pnlHeader As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblAppName As Label
    Friend WithEvents Guna2DragControl1 As Guna.UI2.WinForms.Guna2DragControl
    Friend WithEvents Guna2ShadowForm1 As Guna.UI2.WinForms.Guna2ShadowForm
    Friend WithEvents chkShowPassword As Guna.UI2.WinForms.Guna2CheckBox
End Class