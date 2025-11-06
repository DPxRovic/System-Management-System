Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProfessorControl
    Inherits UserControl

    'UserControl overrides dispose to clean up the component list.
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
    Private components As IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New Container()
        Dim CustomizableEdges19 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges20 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges15 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges16 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges11 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges12 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges17 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Dim CustomizableEdges18 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Suite.CustomizableEdges()
        Guna2Elipse1 = New Guna2Elipse(components)
        Guna2ShadowForm1 = New Guna2ShadowForm(components)
        pnlMain = New Guna2Panel()
        pnlContent = New Guna2Panel()
        dgvStudents = New Guna2DataGridView()
        pnlControls = New Guna2Panel()
        btnViewDetails = New Guna2Button()
        btnRefresh = New Guna2Button()
        pnlFilters = New Guna2Panel()
        lblSearchIcon = New Guna2HtmlLabel()
        txtSearch = New Guna2TextBox()
        lblSectionLabel = New Guna2HtmlLabel()
        cboSection = New Guna2ComboBox()
        lblCourseLabel = New Guna2HtmlLabel()
        cboCourse = New Guna2ComboBox()
        pnlHeader = New Guna2Panel()
        Guna2Separator1 = New Guna2Separator()
        lblSubtitle = New Guna2HtmlLabel()
        lblTitle = New Guna2HtmlLabel()
        pnlMain.SuspendLayout()
        pnlContent.SuspendLayout()
        CType(dgvStudents, ISupportInitialize).BeginInit()
        pnlControls.SuspendLayout()
        pnlFilters.SuspendLayout()
        pnlHeader.SuspendLayout()
        SuspendLayout()
        ' 
        ' Guna2Elipse1
        ' 
        Guna2Elipse1.BorderRadius = 12
        Guna2Elipse1.TargetControl = Me
        ' 
        ' pnlMain
        ' 
        pnlMain.BackColor = Color.Transparent
        pnlMain.BorderRadius = 12
        pnlMain.Controls.Add(pnlContent)
        pnlMain.Controls.Add(pnlControls)
        pnlMain.Controls.Add(pnlFilters)
        pnlMain.Controls.Add(pnlHeader)
        pnlMain.CustomizableEdges = CustomizableEdges19
        pnlMain.Dock = DockStyle.Fill
        pnlMain.FillColor = Color.White
        pnlMain.Location = New Point(0, 0)
        pnlMain.Margin = New Padding(4, 5, 4, 5)
        pnlMain.Name = "pnlMain"
        pnlMain.Padding = New Padding(27, 31, 27, 31)
        pnlMain.ShadowDecoration.BorderRadius = 12
        pnlMain.ShadowDecoration.Color = Color.FromArgb(CByte(90), CByte(90), CByte(90))
        pnlMain.ShadowDecoration.CustomizableEdges = CustomizableEdges20
        pnlMain.ShadowDecoration.Depth = 10
        pnlMain.ShadowDecoration.Enabled = True
        pnlMain.Size = New Size(1467, 1077)
        pnlMain.TabIndex = 0
        ' 
        ' pnlContent
        ' 
        pnlContent.BackColor = Color.Transparent
        pnlContent.BorderRadius = 8
        pnlContent.Controls.Add(dgvStudents)
        pnlContent.CustomizableEdges = CustomizableEdges1
        pnlContent.Dock = DockStyle.Fill
        pnlContent.FillColor = Color.White
        pnlContent.Location = New Point(27, 400)
        pnlContent.Margin = New Padding(4, 5, 4, 5)
        pnlContent.Name = "pnlContent"
        pnlContent.Padding = New Padding(3)
        pnlContent.ShadowDecoration.BorderRadius = 8
        pnlContent.ShadowDecoration.Color = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        pnlContent.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        pnlContent.ShadowDecoration.Depth = 8
        pnlContent.ShadowDecoration.Enabled = True
        pnlContent.Size = New Size(1413, 554)
        pnlContent.TabIndex = 3
        ' 
        ' dgvStudents
        ' 
        dgvStudents.AllowUserToAddRows = False
        dgvStudents.AllowUserToDeleteRows = False
        dgvStudents.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(247), CByte(248), CByte(249))
        DataGridViewCellStyle1.Font = New Font("Segoe UI", 9.0F)
        DataGridViewCellStyle1.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        DataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(CByte(239), CByte(241), CByte(243))
        DataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        dgvStudents.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.Padding = New Padding(8, 0, 8, 0)
        DataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvStudents.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvStudents.ColumnHeadersHeight = 45
        dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.White
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 9.0F)
        DataGridViewCellStyle3.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        DataGridViewCellStyle3.Padding = New Padding(8, 0, 8, 0)
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(239), CByte(241), CByte(243))
        DataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        dgvStudents.DefaultCellStyle = DataGridViewCellStyle3
        dgvStudents.Dock = DockStyle.Fill
        dgvStudents.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvStudents.Location = New Point(3, 3)
        dgvStudents.Margin = New Padding(4, 5, 4, 5)
        dgvStudents.MultiSelect = False
        dgvStudents.Name = "dgvStudents"
        dgvStudents.ReadOnly = True
        dgvStudents.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvStudents.RowHeadersVisible = False
        dgvStudents.RowHeadersWidth = 51
        dgvStudents.RowTemplate.Height = 38
        dgvStudents.Size = New Size(1407, 548)
        dgvStudents.TabIndex = 0
        dgvStudents.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White
        dgvStudents.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        dgvStudents.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty
        dgvStudents.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty
        dgvStudents.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty
        dgvStudents.ThemeStyle.BackColor = Color.White
        dgvStudents.ThemeStyle.GridColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvStudents.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        dgvStudents.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None
        dgvStudents.ThemeStyle.HeaderStyle.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        dgvStudents.ThemeStyle.HeaderStyle.ForeColor = Color.White
        dgvStudents.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        dgvStudents.ThemeStyle.HeaderStyle.Height = 45
        dgvStudents.ThemeStyle.ReadOnly = True
        dgvStudents.ThemeStyle.RowsStyle.BackColor = Color.White
        dgvStudents.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvStudents.ThemeStyle.RowsStyle.Font = New Font("Segoe UI", 9.0F)
        dgvStudents.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        dgvStudents.ThemeStyle.RowsStyle.Height = 38
        dgvStudents.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(CByte(231), CByte(229), CByte(255))
        dgvStudents.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(CByte(71), CByte(69), CByte(94))
        ' 
        ' pnlControls
        ' 
        pnlControls.Controls.Add(btnViewDetails)
        pnlControls.Controls.Add(btnRefresh)
        pnlControls.CustomizableEdges = CustomizableEdges7
        pnlControls.Dock = DockStyle.Bottom
        pnlControls.FillColor = Color.White
        pnlControls.Location = New Point(27, 954)
        pnlControls.Margin = New Padding(4, 5, 4, 5)
        pnlControls.Name = "pnlControls"
        pnlControls.Padding = New Padding(0, 23, 0, 0)
        pnlControls.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        pnlControls.Size = New Size(1413, 92)
        pnlControls.TabIndex = 2
        ' 
        ' btnViewDetails
        ' 
        btnViewDetails.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnViewDetails.BackColor = Color.Transparent
        btnViewDetails.BorderRadius = 8
        btnViewDetails.CustomizableEdges = CustomizableEdges3
        btnViewDetails.DisabledState.BorderColor = Color.DarkGray
        btnViewDetails.DisabledState.CustomBorderColor = Color.DarkGray
        btnViewDetails.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnViewDetails.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnViewDetails.FillColor = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        btnViewDetails.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnViewDetails.ForeColor = Color.White
        btnViewDetails.HoverState.FillColor = Color.FromArgb(CByte(41), CByte(128), CByte(185))
        btnViewDetails.ImageAlign = HorizontalAlignment.Left
        btnViewDetails.ImageSize = New Size(22, 22)
        btnViewDetails.Location = New Point(1173, 23)
        btnViewDetails.Margin = New Padding(4, 5, 4, 5)
        btnViewDetails.Name = "btnViewDetails"
        btnViewDetails.ShadowDecoration.BorderRadius = 8
        btnViewDetails.ShadowDecoration.Color = Color.FromArgb(CByte(52), CByte(152), CByte(219))
        btnViewDetails.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        btnViewDetails.ShadowDecoration.Depth = 8
        btnViewDetails.ShadowDecoration.Enabled = True
        btnViewDetails.Size = New Size(240, 69)
        btnViewDetails.TabIndex = 1
        btnViewDetails.Text = "📋 View Details"
        ' 
        ' btnRefresh
        ' 
        btnRefresh.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnRefresh.BackColor = Color.Transparent
        btnRefresh.BorderRadius = 8
        btnRefresh.CustomizableEdges = CustomizableEdges5
        btnRefresh.DisabledState.BorderColor = Color.DarkGray
        btnRefresh.DisabledState.CustomBorderColor = Color.DarkGray
        btnRefresh.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnRefresh.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnRefresh.FillColor = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        btnRefresh.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnRefresh.ForeColor = Color.White
        btnRefresh.HoverState.FillColor = Color.FromArgb(CByte(22), CByte(160), CByte(133))
        btnRefresh.ImageAlign = HorizontalAlignment.Left
        btnRefresh.ImageSize = New Size(22, 22)
        btnRefresh.Location = New Point(912, 23)
        btnRefresh.Margin = New Padding(4, 5, 4, 5)
        btnRefresh.Name = "btnRefresh"
        btnRefresh.ShadowDecoration.BorderRadius = 8
        btnRefresh.ShadowDecoration.Color = Color.FromArgb(CByte(26), CByte(188), CByte(156))
        btnRefresh.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        btnRefresh.ShadowDecoration.Depth = 8
        btnRefresh.ShadowDecoration.Enabled = True
        btnRefresh.Size = New Size(240, 69)
        btnRefresh.TabIndex = 0
        btnRefresh.Text = "🔄 Refresh"
        ' 
        ' pnlFilters
        ' 
        pnlFilters.BackColor = Color.Transparent
        pnlFilters.BorderRadius = 8
        pnlFilters.Controls.Add(lblSearchIcon)
        pnlFilters.Controls.Add(txtSearch)
        pnlFilters.Controls.Add(lblSectionLabel)
        pnlFilters.Controls.Add(cboSection)
        pnlFilters.Controls.Add(lblCourseLabel)
        pnlFilters.Controls.Add(cboCourse)
        pnlFilters.CustomizableEdges = CustomizableEdges15
        pnlFilters.Dock = DockStyle.Top
        pnlFilters.FillColor = Color.FromArgb(CByte(247), CByte(248), CByte(249))
        pnlFilters.Location = New Point(27, 185)
        pnlFilters.Margin = New Padding(4, 5, 4, 5)
        pnlFilters.Name = "pnlFilters"
        pnlFilters.Padding = New Padding(20, 23, 20, 23)
        pnlFilters.ShadowDecoration.BorderRadius = 8
        pnlFilters.ShadowDecoration.Color = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        pnlFilters.ShadowDecoration.CustomizableEdges = CustomizableEdges16
        pnlFilters.ShadowDecoration.Depth = 5
        pnlFilters.ShadowDecoration.Enabled = True
        pnlFilters.Size = New Size(1413, 215)
        pnlFilters.TabIndex = 1
        ' 
        ' lblSearchIcon
        ' 
        lblSearchIcon.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblSearchIcon.BackColor = Color.Transparent
        lblSearchIcon.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblSearchIcon.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblSearchIcon.Location = New Point(951, 28)
        lblSearchIcon.Margin = New Padding(4, 5, 4, 5)
        lblSearchIcon.Name = "lblSearchIcon"
        lblSearchIcon.Size = New Size(132, 22)
        lblSearchIcon.TabIndex = 5
        lblSearchIcon.Text = "🔍 Search Student"
        ' 
        ' txtSearch
        ' 
        txtSearch.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        txtSearch.BackColor = Color.Transparent
        txtSearch.BorderRadius = 8
        txtSearch.Cursor = Cursors.IBeam
        txtSearch.CustomizableEdges = CustomizableEdges9
        txtSearch.DefaultText = ""
        txtSearch.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtSearch.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtSearch.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtSearch.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtSearch.FocusedState.BorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        txtSearch.Font = New Font("Segoe UI", 9.5F)
        txtSearch.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        txtSearch.HoverState.BorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        txtSearch.Location = New Point(951, 65)
        txtSearch.Margin = New Padding(4, 6, 4, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.PlaceholderText = "Search by name or ID..."
        txtSearch.SelectedText = ""
        txtSearch.ShadowDecoration.BorderRadius = 8
        txtSearch.ShadowDecoration.Color = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        txtSearch.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        txtSearch.ShadowDecoration.Depth = 5
        txtSearch.ShadowDecoration.Enabled = True
        txtSearch.Size = New Size(443, 69)
        txtSearch.TabIndex = 4
        txtSearch.TextOffset = New Point(5, 0)
        ' 
        ' lblSectionLabel
        ' 
        lblSectionLabel.BackColor = Color.Transparent
        lblSectionLabel.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblSectionLabel.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblSectionLabel.Location = New Point(485, 28)
        lblSectionLabel.Margin = New Padding(4, 5, 4, 5)
        lblSectionLabel.Name = "lblSectionLabel"
        lblSectionLabel.Size = New Size(123, 22)
        lblSectionLabel.TabIndex = 3
        lblSectionLabel.Text = "📚 Select Section"
        ' 
        ' cboSection
        ' 
        cboSection.BackColor = Color.Transparent
        cboSection.BorderRadius = 8
        cboSection.CustomizableEdges = CustomizableEdges11
        cboSection.DrawMode = DrawMode.OwnerDrawFixed
        cboSection.DropDownStyle = ComboBoxStyle.DropDownList
        cboSection.FocusedColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        cboSection.FocusedState.BorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        cboSection.Font = New Font("Segoe UI", 9.5F)
        cboSection.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        cboSection.HoverState.BorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        cboSection.IntegralHeight = False
        cboSection.ItemHeight = 35
        cboSection.Location = New Point(469, 65)
        cboSection.Margin = New Padding(4, 5, 4, 5)
        cboSection.Name = "cboSection"
        cboSection.ShadowDecoration.BorderRadius = 8
        cboSection.ShadowDecoration.Color = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        cboSection.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        cboSection.ShadowDecoration.Depth = 5
        cboSection.ShadowDecoration.Enabled = True
        cboSection.Size = New Size(421, 41)
        cboSection.TabIndex = 2
        cboSection.TextOffset = New Point(5, 0)
        ' 
        ' lblCourseLabel
        ' 
        lblCourseLabel.BackColor = Color.Transparent
        lblCourseLabel.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        lblCourseLabel.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblCourseLabel.Location = New Point(20, 28)
        lblCourseLabel.Margin = New Padding(4, 5, 4, 5)
        lblCourseLabel.Name = "lblCourseLabel"
        lblCourseLabel.Size = New Size(120, 22)
        lblCourseLabel.TabIndex = 1
        lblCourseLabel.Text = "📖 Select Course"
        ' 
        ' cboCourse
        ' 
        cboCourse.BackColor = Color.Transparent
        cboCourse.BorderRadius = 8
        cboCourse.CustomizableEdges = CustomizableEdges13
        cboCourse.DrawMode = DrawMode.OwnerDrawFixed
        cboCourse.DropDownStyle = ComboBoxStyle.DropDownList
        cboCourse.FocusedColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        cboCourse.FocusedState.BorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        cboCourse.Font = New Font("Segoe UI", 9.5F)
        cboCourse.ForeColor = Color.FromArgb(CByte(44), CByte(62), CByte(80))
        cboCourse.HoverState.BorderColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        cboCourse.IntegralHeight = False
        cboCourse.ItemHeight = 35
        cboCourse.Location = New Point(20, 65)
        cboCourse.Margin = New Padding(4, 5, 4, 5)
        cboCourse.Name = "cboCourse"
        cboCourse.ShadowDecoration.BorderRadius = 8
        cboCourse.ShadowDecoration.Color = Color.FromArgb(CByte(200), CByte(200), CByte(200))
        cboCourse.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        cboCourse.ShadowDecoration.Depth = 5
        cboCourse.ShadowDecoration.Enabled = True
        cboCourse.Size = New Size(407, 41)
        cboCourse.TabIndex = 0
        cboCourse.TextOffset = New Point(5, 0)
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BorderRadius = 8
        pnlHeader.Controls.Add(Guna2Separator1)
        pnlHeader.Controls.Add(lblSubtitle)
        pnlHeader.Controls.Add(lblTitle)
        pnlHeader.CustomizableEdges = CustomizableEdges17
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.FillColor = Color.White
        pnlHeader.Location = New Point(27, 31)
        pnlHeader.Margin = New Padding(4, 5, 4, 5)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Padding = New Padding(20, 18, 20, 18)
        pnlHeader.ShadowDecoration.CustomizableEdges = CustomizableEdges18
        pnlHeader.Size = New Size(1413, 154)
        pnlHeader.TabIndex = 0
        ' 
        ' Guna2Separator1
        ' 
        Guna2Separator1.Dock = DockStyle.Bottom
        Guna2Separator1.FillColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        Guna2Separator1.FillThickness = 2
        Guna2Separator1.Location = New Point(20, 133)
        Guna2Separator1.Margin = New Padding(4, 5, 4, 5)
        Guna2Separator1.Name = "Guna2Separator1"
        Guna2Separator1.Size = New Size(1373, 3)
        Guna2Separator1.TabIndex = 2
        ' 
        ' lblSubtitle
        ' 
        lblSubtitle.BackColor = Color.Transparent
        lblSubtitle.Font = New Font("Segoe UI", 9.5F)
        lblSubtitle.ForeColor = Color.FromArgb(CByte(127), CByte(140), CByte(141))
        lblSubtitle.Location = New Point(24, 80)
        lblSubtitle.Margin = New Padding(4, 5, 4, 5)
        lblSubtitle.Name = "lblSubtitle"
        lblSubtitle.Size = New Size(365, 23)
        lblSubtitle.TabIndex = 1
        lblSubtitle.Text = "View and manage students in your assigned courses"
        ' 
        ' lblTitle
        ' 
        lblTitle.BackColor = Color.Transparent
        lblTitle.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(CByte(46), CByte(139), CByte(87))
        lblTitle.Location = New Point(24, 23)
        lblTitle.Margin = New Padding(4, 5, 4, 5)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(296, 43)
        lblTitle.TabIndex = 0
        lblTitle.Text = " Professor Dashboard"
        ' 
        ' ProfessorControl
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(247))
        Controls.Add(pnlMain)
        Margin = New Padding(4, 5, 4, 5)
        Name = "ProfessorControl"
        Size = New Size(1467, 1077)
        pnlMain.ResumeLayout(False)
        pnlContent.ResumeLayout(False)
        CType(dgvStudents, ISupportInitialize).EndInit()
        pnlControls.ResumeLayout(False)
        pnlFilters.ResumeLayout(False)
        pnlFilters.PerformLayout()
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents Guna2ShadowForm1 As Guna.UI2.WinForms.Guna2ShadowForm
    Friend WithEvents pnlMain As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents pnlHeader As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTitle As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblSubtitle As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents pnlFilters As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents cboCourse As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents cboSection As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents txtSearch As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents pnlContent As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents dgvStudents As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents pnlControls As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnRefresh As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnViewDetails As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblCourseLabel As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblSectionLabel As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblSearchIcon As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Guna2Separator1 As Guna.UI2.WinForms.Guna2Separator
End Class