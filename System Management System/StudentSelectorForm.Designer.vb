' ==========================================
' FILENAME: /Forms/StudentSelectorForm.Designer.vb
' PURPOSE: Designer file for Student Selector Form
' AUTHOR: System
' DATE: 2025-10-26
' ==========================================

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class StudentSelectorForm
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
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
        dgvStudents = New Guna.UI2.WinForms.Guna2DataGridView()
        pnlBottom = New Guna.UI2.WinForms.Guna2Panel()
        btnViewPortal = New Guna.UI2.WinForms.Guna2Button()
        lblStudentCount = New Label()
        pnlTop = New Guna.UI2.WinForms.Guna2Panel()
        btnRefresh = New Guna.UI2.WinForms.Guna2Button()
        btnClearSearch = New Guna.UI2.WinForms.Guna2Button()
        btnSearch = New Guna.UI2.WinForms.Guna2Button()
        txtSearch = New Guna.UI2.WinForms.Guna2TextBox()
        Label2 = New Label()
        Label1 = New Label()
        pnlMain.SuspendLayout()
        CType(dgvStudents, ComponentModel.ISupportInitialize).BeginInit()
        pnlBottom.SuspendLayout()
        pnlTop.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 15
        Guna2Elipse1.TargetControl = Me
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(247))
        pnlMain.Controls.Add(dgvStudents)
        pnlMain.Controls.Add(pnlBottom)
        pnlMain.Controls.Add(pnlTop)
        pnlMain.CustomizableEdges = CustomizableEdges15
        pnlMain.Dock = DockStyle.Fill
        pnlMain.Location = New Point(0, 0)
        pnlMain.Margin = New Padding(4, 5, 4, 5)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(27, 31, 27, 31)
        pnlMain.ShadowDecoration.CustomizableEdges = CustomizableEdges16
        pnlMain.Size = New Size(1600, 1077)
        pnlMain.TabIndex = 0
        ' 
        ' dgvStudents
        ' 
        dgvStudents.AllowUserToAddRows = False
        dgvStudents.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.White
        dgvStudents.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(100), CByte(88), CByte(255))
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvStudents.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvStudents.ColumnHeadersHeight = 40
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.White
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        DataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvStudents.DefaultCellStyle = DataGridViewCellStyle3
        dgvStudents.Dock = DockStyle.Fill
        dgvStudents.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvStudents.Location = New Point(27, 262)
        dgvStudents.Margin = New Padding(4, 5, 4, 5)
        dgvStudents.MultiSelect = False
        dgvStudents.Name = "dgvStudents"
        dgvStudents.ReadOnly = True
        dgvStudents.RowHeadersVisible = False
        dgvStudents.RowHeadersWidth = 51
        dgvStudents.RowTemplate.Height = 40
        dgvStudents.Size = New Size(1546, 661)
        dgvStudents.TabIndex = 2
        dgvStudents.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White
        dgvStudents.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        dgvStudents.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty
        dgvStudents.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty
        dgvStudents.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty
        dgvStudents.ThemeStyle.BackColor = Color.White
        dgvStudents.ThemeStyle.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvStudents.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(CByte(100), CByte(88), CByte(255))
        dgvStudents.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None
        dgvStudents.ThemeStyle.HeaderStyle.Font = New Font("Segoe UI", 9F)
        dgvStudents.ThemeStyle.HeaderStyle.ForeColor = Color.White
        dgvStudents.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvStudents.ThemeStyle.HeaderStyle.Height = 40
        dgvStudents.ThemeStyle.ReadOnly = True
        dgvStudents.ThemeStyle.RowsStyle.BackColor = Color.White
        dgvStudents.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvStudents.ThemeStyle.RowsStyle.Font = New Font("Segoe UI", 9F)
        dgvStudents.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        dgvStudents.ThemeStyle.RowsStyle.Height = 40
        dgvStudents.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvStudents.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        ' 
        ' pnlBottom
        ' 
        pnlBottom.BackColor = Color.White
        pnlBottom.BorderRadius = 10
        pnlBottom.Controls.Add(btnViewPortal)
        pnlBottom.Controls.Add(lblStudentCount)
        pnlBottom.CustomizableEdges = CustomizableEdges3
        pnlBottom.Dock = DockStyle.Bottom
        pnlBottom.Location = New Point(27, 923)
        pnlBottom.Margin = New Padding(4, 5, 4, 5)
        pnlBottom.Name = "pnlBottom"
        pnlBottom.Padding = New Padding(27, 31, 27, 31)
        pnlBottom.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        pnlBottom.Size = New Size(1546, 123)
        pnlBottom.TabIndex = 1
        ' 
        ' btnViewPortal
        ' 
        btnViewPortal.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnViewPortal.BorderRadius = 8
        btnViewPortal.CustomizableEdges = CustomizableEdges1
        btnViewPortal.Enabled = False
        btnViewPortal.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnViewPortal.Font = New Font("Segoe UI", 10F, FontStyle.Bold)
        btnViewPortal.ForeColor = Color.White
        btnViewPortal.Location = New Point(1252, 23)
        btnViewPortal.Margin = New Padding(4, 5, 4, 5)
        btnViewPortal.Name = "btnViewPortal"
        btnViewPortal.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        btnViewPortal.Size = New Size(267, 77)
        btnViewPortal.TabIndex = 1
        btnViewPortal.Text = "📂 Open Student Portal"
        ' 
        ' lblStudentCount
        ' 
        lblStudentCount.AutoSize = True
        lblStudentCount.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        lblStudentCount.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        lblStudentCount.Location = New Point(27, 46)
        lblStudentCount.Margin = New Padding(4, 0, 4, 0)
        lblStudentCount.Name = "lblStudentCount"
        lblStudentCount.Size = New Size(219, 25)
        lblStudentCount.TabIndex = 0
        lblStudentCount.Text = "Total Active Students: 0"
        ' 
        ' pnlTop
        ' 
        pnlTop.BackColor = Color.White
        pnlTop.BorderRadius = 10
        pnlTop.Controls.Add(btnRefresh)
        pnlTop.Controls.Add(btnClearSearch)
        pnlTop.Controls.Add(btnSearch)
        pnlTop.Controls.Add(txtSearch)
        pnlTop.Controls.Add(Label2)
        pnlTop.Controls.Add(Label1)
        pnlTop.CustomizableEdges = CustomizableEdges13
        pnlTop.Dock = DockStyle.Top
        pnlTop.Location = New Point(27, 31)
        pnlTop.Margin = New Padding(4, 5, 4, 5)
        pnlTop.Name = "pnlTop"
        pnlTop.Padding = New Padding(27, 31, 27, 31)
        pnlTop.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        pnlTop.Size = New Size(1546, 231)
        pnlTop.TabIndex = 0
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefresh.BorderRadius = 8
        btnRefresh.CustomizableEdges = CustomizableEdges5
        btnRefresh.FillColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        btnRefresh.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnRefresh.ForeColor = Color.White
        btnRefresh.Location = New Point(1263, 131)
        btnRefresh.Margin = New Padding(4, 5, 4, 5)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        btnRefresh.Size = New Size(160, 69)
        btnRefresh.TabIndex = 5
        btnRefresh.Text = "🔄 Refresh"
        ' 
        ' btnClearSearch
        ' 
        btnClearSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnClearSearch.BorderRadius = 8
        btnClearSearch.CustomizableEdges = CustomizableEdges7
        btnClearSearch.FillColor = Color.FromArgb(CByte(149), CByte(165), CByte(166))
        btnClearSearch.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnClearSearch.ForeColor = Color.White
        btnClearSearch.Location = New Point(1076, 131)
        btnClearSearch.Margin = New Padding(4, 5, 4, 5)
        btnClearSearch.Name = "btnClearSearch"
        btnClearSearch.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        btnClearSearch.Size = New Size(160, 69)
        btnClearSearch.TabIndex = 4
        btnClearSearch.Text = "✖ Clear"
        ' 
        ' btnSearch
        ' 
        btnSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnSearch.BorderRadius = 8
        btnSearch.CustomizableEdges = CustomizableEdges9
        btnSearch.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        btnSearch.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSearch.ForeColor = Color.White
        btnSearch.Location = New Point(886, 131)
        btnSearch.Margin = New Padding(4, 5, 4, 5)
        btnSearch.Name = "btnSearch"
        btnSearch.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        btnSearch.Size = New Size(160, 69)
        btnSearch.TabIndex = 3
        btnSearch.Text = "🔍 Search"
        ' 
        ' txtSearch
        ' 
        txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtSearch.BorderRadius = 8
        txtSearch.Cursor = Cursors.IBeam
        txtSearch.CustomizableEdges = CustomizableEdges11
        txtSearch.DefaultText = ""
        txtSearch.Font = New Font("Segoe UI", 10F)
        txtSearch.Location = New Point(31, 125)
        txtSearch.Margin = New Padding(4, 6, 4, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.PlaceholderText = "Enter Student ID, Name, Course......."
        txtSearch.SelectedText = ""
        txtSearch.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        txtSearch.Size = New Size(627, 69)
        txtSearch.TabIndex = 2
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 9F)
        Label2.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        Label2.Location = New Point(31, 77)
        Label2.Margin = New Padding(4, 0, 4, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(747, 20)
        Label2.TabIndex = 1
        Label2.Text = "Select a student from the list below to access their portal. You can search by Student ID, Name, Course, or Email."
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 14F, FontStyle.Bold)
        Label1.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        Label1.Location = New Point(27, 31)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(376, 32)
        Label1.TabIndex = 0
        Label1.Text = "🎓 Select Student Portal Access"
        ' 
        ' StudentSelectorForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1600, 1077)
        Controls.Add(pnlMain)
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(4, 5, 4, 5)
        Name = "StudentSelectorForm"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Select Student - Student Portal Access"
        pnlMain.ResumeLayout(False)
        CType(dgvStudents, ComponentModel.ISupportInitialize).EndInit()
        pnlBottom.ResumeLayout(False)
        pnlBottom.PerformLayout()
        pnlTop.ResumeLayout(False)
        pnlTop.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents pnlMain As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlTop As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnSearch As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnClearSearch As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnRefresh As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents dgvStudents As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents pnlBottom As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblStudentCount As Label
    Friend WithEvents btnViewPortal As Guna.UI2.WinForms.Guna2Button

End Class