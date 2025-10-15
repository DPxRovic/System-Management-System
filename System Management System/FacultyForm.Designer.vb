' ==========================================
' FILENAME: /Forms/FacultyForm.Designer.vb
' PURPOSE: Designer file for FacultyForm
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FacultyForm
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
        Me.pnlContent = New Guna.UI2.WinForms.Guna2Panel()
        Me.flpCourses = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlHeader = New Guna.UI2.WinForms.Guna2Panel()
        Me.btnRefresh = New Guna.UI2.WinForms.Guna2Button()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.lblCoursesCount = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.TargetControl = Me
        Me.Guna2Elipse1.BorderRadius = 15
        '
        'pnlMain
        '
        Me.pnlMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlMain.Size = New System.Drawing.Size(1000, 700)
        Me.pnlMain.TabIndex = 0
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.BackColor = System.Drawing.Color.White
        Me.pnlContent.BorderRadius = 10
        Me.pnlContent.Controls.Add(Me.flpCourses)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(20, 100)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Padding = New System.Windows.Forms.Padding(15)
        Me.pnlContent.ShadowDecoration.BorderRadius = 10
        Me.pnlContent.ShadowDecoration.Depth = 5
        Me.pnlContent.ShadowDecoration.Enabled = True
        Me.pnlContent.Size = New System.Drawing.Size(960, 580)
        Me.pnlContent.TabIndex = 1
        '
        'flpCourses
        '
        Me.flpCourses.AutoScroll = True
        Me.flpCourses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpCourses.Location = New System.Drawing.Point(15, 15)
        Me.flpCourses.Name = "flpCourses"
        Me.flpCourses.Padding = New System.Windows.Forms.Padding(10)
        Me.flpCourses.Size = New System.Drawing.Size(930, 550)
        Me.flpCourses.TabIndex = 0
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.btnRefresh)
        Me.pnlHeader.Controls.Add(Me.lblSubtitle)
        Me.pnlHeader.Controls.Add(Me.lblCoursesCount)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(20, 20)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(960, 80)
        Me.pnlHeader.TabIndex = 0
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BorderRadius = 8
        Me.btnRefresh.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnRefresh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnRefresh.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnRefresh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnRefresh.FillColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(188, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.HoverState.FillColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(133, Byte), Integer))
        Me.btnRefresh.Location = New System.Drawing.Point(820, 20)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(120, 45)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.Text = "🔄 Refresh"
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(10, 50)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(331, 19)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "Manage your courses, attendance, and student records"
        '
        'lblCoursesCount
        '
        Me.lblCoursesCount.AutoSize = True
        Me.lblCoursesCount.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblCoursesCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblCoursesCount.Location = New System.Drawing.Point(8, 15)
        Me.lblCoursesCount.Name = "lblCoursesCount"
        Me.lblCoursesCount.Size = New System.Drawing.Size(155, 32)
        Me.lblCoursesCount.TabIndex = 0
        Me.lblCoursesCount.Text = "My Courses"
        '
        'FacultyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1000, 700)
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FacultyForm"
        Me.Text = "Faculty Dashboard"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents pnlMain As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlHeader As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblCoursesCount As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents pnlContent As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents flpCourses As FlowLayoutPanel
    Friend WithEvents btnRefresh As Guna.UI2.WinForms.Guna2Button
End Class