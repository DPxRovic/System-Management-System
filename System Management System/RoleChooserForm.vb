Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Guna.UI2.WinForms

Public Class RoleChooserForm
    Inherits Form

    Private currentUser As User

    Private pnlRoot As Guna2Panel
    Private lblGreeting As Label
    Private lblRole As Label
    Private btnAdmin As Guna2Button
    Private btnDashboard As Guna2Button
    Private btnCancel As Guna2Button

    Public Sub New(user As User)
        Me.currentUser = user
        InitializeComponent()
        ApplyRoleRules()
    End Sub

    Private Sub InitializeComponent()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterParent
        Me.BackColor = Color.FromArgb(244, 246, 247)
        Me.ClientSize = New Size(700, 360)
        Me.MinimumSize = New Size(500, 320)
        Me.Padding = New Padding(12)

        pnlRoot = New Guna2Panel() With {
            .Dock = DockStyle.Fill,
            .BackColor = Color.White,
            .BorderRadius = 12,
            .Padding = New Padding(20)
        }

        lblGreeting = New Label() With {
            .AutoSize = False,
            .Dock = DockStyle.Top,
            .Height = 40,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI", 14.0F, FontStyle.Bold),
            .ForeColor = Color.FromArgb(44, 62, 80),
            .Text = $"Welcome, {If(currentUser?.FullName, "User")}"
        }

        lblRole = New Label() With {
            .AutoSize = False,
            .Dock = DockStyle.Top,
            .Height = 22,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Font = New Font("Segoe UI", 9.0F),
            .ForeColor = Color.FromArgb(127, 140, 141),
            .Text = $"Role: {If(currentUser?.Role, "Unknown")}"
        }

        ' Responsive TableLayoutPanel for buttons
        Dim table As New TableLayoutPanel() With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 2,
            .RowCount = 1,
            .BackColor = Color.White,
            .Padding = New Padding(10)
        }
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        table.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        table.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        btnAdmin = New Guna2Button() With {
            .Dock = DockStyle.Fill,
            .Margin = New Padding(10),
            .BorderRadius = 12,
            .FillColor = Color.FromArgb(46, 139, 87),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
            .Text = "🔐" & vbCrLf & "Admin Panel",
            .TextAlign = HorizontalAlignment.Center
        }
        AddHandler btnAdmin.Click, AddressOf BtnAdmin_Click

        btnDashboard = New Guna2Button() With {
            .Dock = DockStyle.Fill,
            .Margin = New Padding(10),
            .BorderRadius = 12,
            .FillColor = Color.FromArgb(52, 152, 219),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 13.0F, FontStyle.Bold),
            .Text = "📊" & vbCrLf & "Dashboard",
            .TextAlign = HorizontalAlignment.Center
        }
        AddHandler btnDashboard.Click, AddressOf BtnDashboard_Click

        table.Controls.Add(btnAdmin, 0, 0)
        table.Controls.Add(btnDashboard, 1, 0)

        btnCancel = New Guna2Button() With {
            .Size = New Size(120, 40),
            .BorderRadius = 8,
            .FillColor = Color.FromArgb(149, 165, 166),
            .ForeColor = Color.White,
            .Font = New Font("Segoe UI", 9.0F),
            .Text = "Cancel",
            .Anchor = AnchorStyles.Right
        }
        AddHandler btnCancel.Click, Sub(s, e)
                                        Me.DialogResult = DialogResult.Cancel
                                        Me.Close()
                                    End Sub

        Dim pnlHeader As New Panel() With {
            .Dock = DockStyle.Top,
            .Height = 72,
            .BackColor = Color.White
        }
        pnlHeader.Controls.Add(lblRole)
        pnlHeader.Controls.Add(lblGreeting)

        Dim pnlFooter As New Panel() With {
            .Dock = DockStyle.Bottom,
            .Height = 56,
            .BackColor = Color.White
        }
        pnlFooter.Padding = New Padding(10)
        pnlFooter.Controls.Add(btnCancel)
        btnCancel.Location = New Point(pnlFooter.Width - btnCancel.Width - 10, 8)
        AddHandler pnlFooter.Resize, Sub(s, e)
                                         btnCancel.Location = New Point(Math.Max(8, pnlFooter.ClientSize.Width - btnCancel.Width - 10), 8)
                                     End Sub

        pnlRoot.Controls.Add(table)
        pnlRoot.Controls.Add(pnlHeader)
        pnlRoot.Controls.Add(pnlFooter)

        Me.Controls.Add(pnlRoot)
    End Sub

    Private Sub ApplyRoleRules()
        Dim role = If(currentUser?.Role, "").ToUpperInvariant()
        ' Enable admin button only for Admin / SuperAdmin
        btnAdmin.Enabled = (role = "ADMIN" OrElse role = "SUPERADMIN")
        If Not btnAdmin.Enabled Then
            btnAdmin.FillColor = Color.FromArgb(189, 195, 199) ' disabled color
            btnAdmin.ForeColor = Color.FromArgb(100, 100, 100)
        End If
    End Sub

    Private Sub BtnAdmin_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.Yes  ' Admin choice
        Me.Close()
    End Sub

    Private Sub BtnDashboard_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.No   ' Dashboard choice
        Me.Close()
    End Sub
End Class