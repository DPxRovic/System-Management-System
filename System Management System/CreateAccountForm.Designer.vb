' ==========================================
' FILENAME: /Forms/CreateAccountForm.Designer.vb
' PURPOSE: Designer file for CreateAccountForm
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CreateAccountForm
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
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges11 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges12 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(components)
        pnlMain = New Guna.UI2.WinForms.Guna2Panel()
        chkShowPassword = New Guna.UI2.WinForms.Guna2CheckBox()
        btnCancel = New Guna.UI2.WinForms.Guna2Button()
        btnSave = New Guna.UI2.WinForms.Guna2Button()
        cmbRole = New Guna.UI2.WinForms.Guna2ComboBox()
        Label4 = New Label()
        txtFullName = New Guna.UI2.WinForms.Guna2TextBox()
        Label3 = New Label()
        txtPassword = New Guna.UI2.WinForms.Guna2TextBox()
        Label2 = New Label()
        txtUsername = New Guna.UI2.WinForms.Guna2TextBox()
        Label1 = New Label()
        lblTitle = New Label()
        Guna2ShadowForm1 = New Guna.UI2.WinForms.Guna2ShadowForm(components)
        pnlMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 15
        Guna2Elipse1.TargetControl = Me
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = SystemColors.Menu
        pnlMain.BorderRadius = 15
        pnlMain.Controls.Add(chkShowPassword)
        pnlMain.Controls.Add(btnCancel)
        pnlMain.Controls.Add(btnSave)
        pnlMain.Controls.Add(cmbRole)
        pnlMain.Controls.Add(Label4)
        pnlMain.Controls.Add(txtFullName)
        pnlMain.Controls.Add(Label3)
        pnlMain.Controls.Add(txtPassword)
        pnlMain.Controls.Add(Label2)
        pnlMain.Controls.Add(txtUsername)
        pnlMain.Controls.Add(Label1)
        pnlMain.Controls.Add(lblTitle)
        pnlMain.CustomizableEdges = CustomizableEdges13
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Margin = New Padding(4, 5, 4, 5)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(40, 46, 40, 46)
        pnlMain.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        pnlMain.Size = New Size(667, 846)
        pnlMain.TabIndex = 0
        ' 
        ' chkShowPassword
        ' 
        chkShowPassword.AutoSize = True
        chkShowPassword.CheckedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        chkShowPassword.CheckedState.BorderRadius = 0
        chkShowPassword.CheckedState.BorderThickness = 0
        chkShowPassword.CheckedState.FillColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        chkShowPassword.Font = New Font("Segoe UI", 9.0F)
        chkShowPassword.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        chkShowPassword.Location = New Point(67, 444)
        chkShowPassword.Margin = New Padding(4, 5, 4, 5)
        chkShowPassword.Name = "chkShowPassword"
        chkShowPassword.Size = New Size(132, 24)
        chkShowPassword.TabIndex = 11
        chkShowPassword.Text = "Show Password"
        chkShowPassword.UncheckedState.BorderColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        chkShowPassword.UncheckedState.BorderRadius = 0
        chkShowPassword.UncheckedState.BorderThickness = 0
        chkShowPassword.UncheckedState.FillColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        ' 
        ' btnCancel
        ' 
        btnCancel.BorderRadius = 8
        btnCancel.CustomizableEdges = CustomizableEdges1
        btnCancel.DisabledState.BorderColor = Color.DarkGray
        btnCancel.DisabledState.CustomBorderColor = Color.DarkGray
        btnCancel.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnCancel.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnCancel.FillColor = Color.FromArgb(CByte(149), CByte(165), CByte(166))
        btnCancel.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        btnCancel.ForeColor = Color.White
        btnCancel.HoverState.FillColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        btnCancel.Location = New Point(333, 723)
        btnCancel.Margin = New Padding(4, 5, 4, 5)
        btnCancel.Name = "btnCancel"
        btnCancel.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        btnCancel.Size = New Size(267, 69)
        btnCancel.TabIndex = 10
        btnCancel.Text = "Cancel"
        ' 
        ' btnSave
        ' 
        btnSave.BorderRadius = 8
        btnSave.CustomizableEdges = CustomizableEdges3
        btnSave.DisabledState.BorderColor = Color.DarkGray
        btnSave.DisabledState.CustomBorderColor = Color.DarkGray
        btnSave.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnSave.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnSave.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnSave.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        btnSave.ForeColor = Color.White
        btnSave.HoverState.FillColor = Color.FromArgb(CByte(39), CByte(118), CByte(74))
        btnSave.Location = New Point(67, 723)
        btnSave.Margin = New Padding(4, 5, 4, 5)
        btnSave.Name = "btnSave"
        btnSave.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        btnSave.Size = New Size(253, 69)
        btnSave.TabIndex = 9
        btnSave.Text = "Create"
        ' 
        ' cmbRole
        ' 
        cmbRole.BackColor = Color.Transparent
        cmbRole.BorderColor = Color.FromArgb(CByte(213), CByte(218), CByte(223))
        cmbRole.BorderRadius = 8
        cmbRole.CustomizableEdges = CustomizableEdges5
        cmbRole.DrawMode = DrawMode.OwnerDrawFixed
        cmbRole.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRole.FocusedColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        cmbRole.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        cmbRole.Font = New Font("Segoe UI", 10.0F)
        cmbRole.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        cmbRole.ItemHeight = 30
        cmbRole.Location = New Point(67, 635)
        cmbRole.Margin = New Padding(4, 5, 4, 5)
        cmbRole.Name = "cmbRole"
        cmbRole.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        cmbRole.Size = New Size(532, 36)
        cmbRole.TabIndex = 8
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label4.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label4.Location = New Point(67, 591)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(45, 23)
        Label4.TabIndex = 7
        Label4.Text = "Role"
        ' 
        ' txtFullName
        ' 
        txtFullName.BorderRadius = 8
        txtFullName.Cursor = Cursors.IBeam
        txtFullName.CustomizableEdges = CustomizableEdges7
        txtFullName.DefaultText = ""
        txtFullName.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtFullName.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtFullName.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtFullName.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtFullName.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtFullName.Font = New Font("Segoe UI", 10.0F)
        txtFullName.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtFullName.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtFullName.Location = New Point(66, 512)
        txtFullName.Margin = New Padding(5, 8, 5, 8)
        txtFullName.Name = "txtFullName"
        txtFullName.PlaceholderText = "Enter full name"
        txtFullName.SelectedText = ""
        txtFullName.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        txtFullName.Size = New Size(533, 62)
        txtFullName.TabIndex = 6
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label3.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label3.Location = New Point(65, 481)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(91, 23)
        Label3.TabIndex = 5
        Label3.Text = "Full Name"
        ' 
        ' txtPassword
        ' 
        txtPassword.BorderRadius = 8
        txtPassword.Cursor = Cursors.IBeam
        txtPassword.CustomizableEdges = CustomizableEdges9
        txtPassword.DefaultText = ""
        txtPassword.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtPassword.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtPassword.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPassword.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtPassword.Font = New Font("Segoe UI", 10.0F)
        txtPassword.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtPassword.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtPassword.Location = New Point(67, 369)
        txtPassword.Margin = New Padding(5, 8, 5, 8)
        txtPassword.Name = "txtPassword"
        txtPassword.PasswordChar = "●"c
        txtPassword.PlaceholderText = "Enter password"
        txtPassword.SelectedText = ""
        txtPassword.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        txtPassword.Size = New Size(533, 62)
        txtPassword.TabIndex = 4
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label2.Location = New Point(67, 323)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(85, 23)
        Label2.TabIndex = 3
        Label2.Text = "Password"
        ' 
        ' txtUsername
        ' 
        txtUsername.BorderRadius = 8
        txtUsername.Cursor = Cursors.IBeam
        txtUsername.CustomizableEdges = CustomizableEdges11
        txtUsername.DefaultText = ""
        txtUsername.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtUsername.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtUsername.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtUsername.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtUsername.Font = New Font("Segoe UI", 10.0F)
        txtUsername.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtUsername.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtUsername.Location = New Point(67, 246)
        txtUsername.Margin = New Padding(5, 8, 5, 8)
        txtUsername.Name = "txtUsername"
        txtUsername.PlaceholderText = "Enter username"
        txtUsername.SelectedText = ""
        txtUsername.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        txtUsername.Size = New Size(533, 62)
        txtUsername.TabIndex = 2
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        Label1.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label1.Location = New Point(67, 200)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(89, 23)
        Label1.TabIndex = 1
        Label1.Text = "Username"
        ' 
        ' lblTitle
        ' 
        lblTitle.Dock = DockStyle.Top
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblTitle.Location = New Point(40, 46)
        lblTitle.Margin = New Padding(4, 0, 4, 0)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(587, 123)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Create New Account"
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Guna2ShadowForm1
        ' 
        Guna2ShadowForm1.TargetForm = Me
        ' 
        ' CreateAccountForm
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(247))
        ClientSize = New Size(667, 846)
        Controls.Add(pnlMain)
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(4, 5, 4, 5)
        Name = "CreateAccountForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Create Account"
        pnlMain.ResumeLayout(False)
        pnlMain.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents pnlMain As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtUsername As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPassword As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtFullName As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cmbRole As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents btnSave As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCancel As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents chkShowPassword As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents Guna2ShadowForm1 As Guna.UI2.WinForms.Guna2ShadowForm
End Class