' ==========================================
' FILENAME: /Forms/StudentFormDialog.Designer.vb
' PURPOSE: Designer file for Student Form Dialog
' AUTHOR: System
' DATE: 2025-10-18
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StudentFormDialog
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
        Dim CustomizableEdges21 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges22 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
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
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges15 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges16 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges17 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges18 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges19 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges20 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(components)
        pnlMain = New Guna.UI2.WinForms.Guna2Panel()
        dtpDateOfBirth = New Guna.UI2.WinForms.Guna2DateTimePicker()
        btnCancel = New Guna.UI2.WinForms.Guna2Button()
        btnSave = New Guna.UI2.WinForms.Guna2Button()
        cmbStatus = New Guna.UI2.WinForms.Guna2ComboBox()
        Label9 = New Label()
        dtpEnrollmentDate = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Label8 = New Label()
        chkNoDOB = New Guna.UI2.WinForms.Guna2CheckBox()
        Label7 = New Label()
        txtCourse = New Guna.UI2.WinForms.Guna2TextBox()
        Label6 = New Label()
        txtPhoneNumber = New Guna.UI2.WinForms.Guna2TextBox()
        Label5 = New Label()
        txtEmail = New Guna.UI2.WinForms.Guna2TextBox()
        Label4 = New Label()
        txtName = New Guna.UI2.WinForms.Guna2TextBox()
        Label3 = New Label()
        txtStudentId = New Guna.UI2.WinForms.Guna2TextBox()
        Label2 = New Label()
        lblTitle = New Label()
        pnlMain.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 20
        Guna2Elipse1.TargetControl = Me
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = SystemColors.ActiveCaption
        pnlMain.BorderRadius = 15
        pnlMain.Controls.Add(dtpDateOfBirth)
        pnlMain.Controls.Add(btnCancel)
        pnlMain.Controls.Add(btnSave)
        pnlMain.Controls.Add(cmbStatus)
        pnlMain.Controls.Add(Label9)
        pnlMain.Controls.Add(dtpEnrollmentDate)
        pnlMain.Controls.Add(Label8)
        pnlMain.Controls.Add(chkNoDOB)
        pnlMain.Controls.Add(Label7)
        pnlMain.Controls.Add(txtCourse)
        pnlMain.Controls.Add(Label6)
        pnlMain.Controls.Add(txtPhoneNumber)
        pnlMain.Controls.Add(Label5)
        pnlMain.Controls.Add(txtEmail)
        pnlMain.Controls.Add(Label4)
        pnlMain.Controls.Add(txtName)
        pnlMain.Controls.Add(Label3)
        pnlMain.Controls.Add(txtStudentId)
        pnlMain.Controls.Add(Label2)
        pnlMain.Controls.Add(lblTitle)
        pnlMain.CustomizableEdges = CustomizableEdges21
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Margin = New Padding(3, 2, 3, 2)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(26, 22, 26, 22)
        pnlMain.ShadowDecoration.CustomizableEdges = CustomizableEdges22
        pnlMain.Size = New Size(612, 638)
        pnlMain.TabIndex = 0
        ' 
        ' dtpDateOfBirth
        ' 
        dtpDateOfBirth.BorderRadius = 8
        dtpDateOfBirth.Checked = True
        dtpDateOfBirth.CustomizableEdges = CustomizableEdges1
        dtpDateOfBirth.FillColor = Color.White
        dtpDateOfBirth.Font = New Font("Segoe UI", 9F)
        dtpDateOfBirth.Format = DateTimePickerFormat.Short
        dtpDateOfBirth.Location = New Point(52, 449)
        dtpDateOfBirth.Margin = New Padding(3, 2, 3, 2)
        dtpDateOfBirth.MaxDate = New Date(2025, 12, 31, 0, 0, 0, 0)
        dtpDateOfBirth.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        dtpDateOfBirth.Name = "dtpDateOfBirth"
        dtpDateOfBirth.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        dtpDateOfBirth.Size = New Size(245, 30)
        dtpDateOfBirth.TabIndex = 12
        dtpDateOfBirth.Value = New Date(2005, 1, 1, 0, 0, 0, 0)
        ' 
        ' btnCancel
        ' 
        btnCancel.BorderRadius = 8
        btnCancel.CustomizableEdges = CustomizableEdges3
        btnCancel.DisabledState.BorderColor = Color.DarkGray
        btnCancel.DisabledState.CustomBorderColor = Color.DarkGray
        btnCancel.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnCancel.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnCancel.FillColor = Color.FromArgb(CByte(149), CByte(165), CByte(166))
        btnCancel.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        btnCancel.ForeColor = Color.White
        btnCancel.Location = New Point(315, 570)
        btnCancel.Margin = New Padding(3, 2, 3, 2)
        btnCancel.Name = "btnCancel"
        btnCancel.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        btnCancel.Size = New Size(245, 41)
        btnCancel.TabIndex = 19
        btnCancel.Text = "Cancel"
        ' 
        ' btnSave
        ' 
        btnSave.BorderRadius = 8
        btnSave.CustomizableEdges = CustomizableEdges5
        btnSave.DisabledState.BorderColor = Color.DarkGray
        btnSave.DisabledState.CustomBorderColor = Color.DarkGray
        btnSave.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnSave.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnSave.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnSave.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)
        btnSave.ForeColor = Color.White
        btnSave.HoverState.FillColor = Color.FromArgb(CByte(39), CByte(118), CByte(74))
        btnSave.Location = New Point(52, 570)
        btnSave.Margin = New Padding(3, 2, 3, 2)
        btnSave.Name = "btnSave"
        btnSave.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        btnSave.Size = New Size(245, 41)
        btnSave.TabIndex = 18
        btnSave.Text = "Save"
        ' 
        ' cmbStatus
        ' 
        cmbStatus.BackColor = Color.Transparent
        cmbStatus.BorderRadius = 8
        cmbStatus.CustomizableEdges = CustomizableEdges7
        cmbStatus.DrawMode = DrawMode.OwnerDrawFixed
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus.FocusedColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        cmbStatus.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        cmbStatus.Font = New Font("Segoe UI", 10F)
        cmbStatus.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        cmbStatus.ItemHeight = 30
        cmbStatus.Location = New Point(315, 518)
        cmbStatus.Margin = New Padding(3, 2, 3, 2)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        cmbStatus.Size = New Size(246, 36)
        cmbStatus.TabIndex = 17
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label9.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label9.Location = New Point(315, 495)
        Label9.Name = "Label9"
        Label9.Size = New Size(49, 19)
        Label9.TabIndex = 16
        Label9.Text = "Status"
        ' 
        ' dtpEnrollmentDate
        ' 
        dtpEnrollmentDate.BorderRadius = 8
        dtpEnrollmentDate.Checked = True
        dtpEnrollmentDate.CustomizableEdges = CustomizableEdges9
        dtpEnrollmentDate.FillColor = Color.White
        dtpEnrollmentDate.Font = New Font("Segoe UI", 9F)
        dtpEnrollmentDate.Format = DateTimePickerFormat.Short
        dtpEnrollmentDate.Location = New Point(52, 517)
        dtpEnrollmentDate.Margin = New Padding(3, 2, 3, 2)
        dtpEnrollmentDate.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        dtpEnrollmentDate.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        dtpEnrollmentDate.Name = "dtpEnrollmentDate"
        dtpEnrollmentDate.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        dtpEnrollmentDate.Size = New Size(245, 30)
        dtpEnrollmentDate.TabIndex = 15
        dtpEnrollmentDate.Value = New Date(2025, 10, 18, 0, 0, 0, 0)
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label8.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label8.Location = New Point(52, 493)
        Label8.Name = "Label8"
        Label8.Size = New Size(116, 19)
        Label8.TabIndex = 14
        Label8.Text = "Enrollment Date"
        ' 
        ' chkNoDOB
        ' 
        chkNoDOB.AutoSize = True
        chkNoDOB.CheckedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        chkNoDOB.CheckedState.BorderRadius = 0
        chkNoDOB.CheckedState.BorderThickness = 0
        chkNoDOB.CheckedState.FillColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        chkNoDOB.Font = New Font("Segoe UI", 9F)
        chkNoDOB.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        chkNoDOB.Location = New Point(315, 449)
        chkNoDOB.Margin = New Padding(3, 2, 3, 2)
        chkNoDOB.Name = "chkNoDOB"
        chkNoDOB.Size = New Size(147, 19)
        chkNoDOB.TabIndex = 13
        chkNoDOB.Text = "No Date of Birth on file"
        chkNoDOB.UncheckedState.BorderColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        chkNoDOB.UncheckedState.BorderRadius = 0
        chkNoDOB.UncheckedState.BorderThickness = 0
        chkNoDOB.UncheckedState.FillColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label7.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label7.Location = New Point(52, 428)
        Label7.Name = "Label7"
        Label7.Size = New Size(94, 19)
        Label7.TabIndex = 11
        Label7.Text = "Date of Birth"
        ' 
        ' txtCourse
        ' 
        txtCourse.BorderRadius = 8
        txtCourse.Cursor = Cursors.IBeam
        txtCourse.CustomizableEdges = CustomizableEdges11
        txtCourse.DefaultText = ""
        txtCourse.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtCourse.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtCourse.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtCourse.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtCourse.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtCourse.Font = New Font("Segoe UI", 10F)
        txtCourse.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtCourse.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtCourse.Location = New Point(52, 382)
        txtCourse.Margin = New Padding(4)
        txtCourse.Name = "txtCourse"
        txtCourse.PlaceholderText = "e.g., Bachelor of Science in Computer Science"
        txtCourse.SelectedText = ""
        txtCourse.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        txtCourse.Size = New Size(508, 38)
        txtCourse.TabIndex = 10
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label6.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label6.Location = New Point(52, 360)
        Label6.Name = "Label6"
        Label6.Size = New Size(121, 19)
        Label6.TabIndex = 9
        Label6.Text = "Course/Program"
        ' 
        ' txtPhoneNumber
        ' 
        txtPhoneNumber.BorderRadius = 8
        txtPhoneNumber.Cursor = Cursors.IBeam
        txtPhoneNumber.CustomizableEdges = CustomizableEdges13
        txtPhoneNumber.DefaultText = ""
        txtPhoneNumber.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtPhoneNumber.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtPhoneNumber.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPhoneNumber.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPhoneNumber.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtPhoneNumber.Font = New Font("Segoe UI", 10F)
        txtPhoneNumber.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtPhoneNumber.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtPhoneNumber.Location = New Point(315, 311)
        txtPhoneNumber.Margin = New Padding(4)
        txtPhoneNumber.Name = "txtPhoneNumber"
        txtPhoneNumber.PlaceholderText = "e.g., 09171234567"
        txtPhoneNumber.SelectedText = ""
        txtPhoneNumber.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        txtPhoneNumber.Size = New Size(245, 38)
        txtPhoneNumber.TabIndex = 8
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label5.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label5.Location = New Point(315, 289)
        Label5.Name = "Label5"
        Label5.Size = New Size(110, 19)
        Label5.TabIndex = 7
        Label5.Text = "Phone Number"
        ' 
        ' txtEmail
        ' 
        txtEmail.BorderRadius = 8
        txtEmail.Cursor = Cursors.IBeam
        txtEmail.CustomizableEdges = CustomizableEdges15
        txtEmail.DefaultText = ""
        txtEmail.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtEmail.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtEmail.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtEmail.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtEmail.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtEmail.Font = New Font("Segoe UI", 10F)
        txtEmail.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtEmail.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtEmail.Location = New Point(52, 311)
        txtEmail.Margin = New Padding(4)
        txtEmail.Name = "txtEmail"
        txtEmail.PlaceholderText = "e.g., student@example.com"
        txtEmail.SelectedText = ""
        txtEmail.ShadowDecoration.CustomizableEdges = CustomizableEdges16
        txtEmail.Size = New Size(245, 38)
        txtEmail.TabIndex = 6
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label4.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label4.Location = New Point(52, 289)
        Label4.Name = "Label4"
        Label4.Size = New Size(103, 19)
        Label4.TabIndex = 5
        Label4.Text = "Email Address"
        ' 
        ' txtName
        ' 
        txtName.BorderRadius = 8
        txtName.Cursor = Cursors.IBeam
        txtName.CustomizableEdges = CustomizableEdges17
        txtName.DefaultText = ""
        txtName.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtName.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtName.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtName.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtName.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtName.Font = New Font("Segoe UI", 10F)
        txtName.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtName.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtName.Location = New Point(52, 240)
        txtName.Margin = New Padding(4)
        txtName.Name = "txtName"
        txtName.PlaceholderText = "Enter full name"
        txtName.SelectedText = ""
        txtName.ShadowDecoration.CustomizableEdges = CustomizableEdges18
        txtName.Size = New Size(508, 38)
        txtName.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label3.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label3.Location = New Point(52, 218)
        Label3.Name = "Label3"
        Label3.Size = New Size(76, 19)
        Label3.TabIndex = 3
        Label3.Text = "Full Name"
        ' 
        ' txtStudentId
        ' 
        txtStudentId.BorderRadius = 8
        txtStudentId.Cursor = Cursors.IBeam
        txtStudentId.CustomizableEdges = CustomizableEdges19
        txtStudentId.DefaultText = ""
        txtStudentId.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtStudentId.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtStudentId.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtStudentId.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtStudentId.FocusedState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtStudentId.Font = New Font("Segoe UI", 10F)
        txtStudentId.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtStudentId.HoverState.BorderColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        txtStudentId.Location = New Point(52, 169)
        txtStudentId.Margin = New Padding(4)
        txtStudentId.Name = "txtStudentId"
        txtStudentId.PlaceholderText = "Enter student ID"
        txtStudentId.SelectedText = ""
        txtStudentId.ShadowDecoration.CustomizableEdges = CustomizableEdges20
        txtStudentId.Size = New Size(508, 38)
        txtStudentId.TabIndex = 2
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label2.Location = New Point(52, 146)
        Label2.Name = "Label2"
        Label2.Size = New Size(78, 19)
        Label2.TabIndex = 1
        Label2.Text = "Student ID"
        ' 
        ' lblTitle
        ' 
        lblTitle.Dock = DockStyle.Top
        lblTitle.Font = New Font("Segoe UI", 18F, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblTitle.Location = New Point(26, 22)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(560, 75)
        lblTitle.TabIndex = 0
        lblTitle.Text = "Add New Student"
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' StudentFormDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(247))
        ClientSize = New Size(612, 638)
        Controls.Add(pnlMain)
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(3, 2, 3, 2)
        Name = "StudentFormDialog"
        StartPosition = FormStartPosition.CenterParent
        Text = "Student Form"
        pnlMain.ResumeLayout(False)
        pnlMain.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents pnlMain As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents txtStudentId As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtName As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtEmail As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtPhoneNumber As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtCourse As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents dtpDateOfBirth As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Label7 As Label
    Friend WithEvents chkNoDOB As Guna.UI2.WinForms.Guna2CheckBox
    Friend WithEvents dtpEnrollmentDate As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents cmbStatus As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents btnSave As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnCancel As Guna.UI2.WinForms.Guna2Button
End Class