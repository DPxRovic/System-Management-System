' ==========================================
' FILENAME: /Forms/StudentManagementForm.vb
' PURPOSE: Comprehensive student academic records management interface
' ==========================================

Imports System.Data
Imports Guna.UI2.WinForms

Public Class StudentManagementForm
    Private currentUser As User
    Private currentStudentData As DataTable
    Private selectedStudentId As Integer = 0

    ' Timer for debounce typing
    Private searchTimer As New Timer() With {.Interval = 500}

    ''' <summary>
    ''' Constructor with user parameter
    ''' </summary>
    Public Sub New(user As User)
        InitializeComponent()
        currentUser = user
    End Sub

    ''' <summary>
    ''' Form load event
    ''' </summary>
    Private Sub StudentManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Set auto-scaling for responsive design
            Me.AutoScaleMode = AutoScaleMode.Dpi

            ' Setup permissions based on user role
            SetupPermissions()

            ' Load initial data
            LoadStudents()
            LoadCourseFilter()
            LoadStatusFilter()

            ' Load statistics
            LoadStatistics()

            ' Apply theme
            ApplyTheme()
            ThemeManager.SlideIn(pnlMain, "LEFT", 300)

            Logger.LogInfo($"Student Management form loaded by {currentUser.Username}")

        Catch ex As Exception
            Logger.LogError("Error loading Student Management form", ex)
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Setup permissions based on user role
    ''' </summary>
    Private Sub SetupPermissions()
        Try
            Select Case currentUser.Role.ToUpper()
                Case "SUPERADMIN"
                    ' Full access
                    btnAddStudent.Visible = True
                    btnEditStudent.Visible = True
                    btnDeleteStudent.Visible = True
                    btnArchiveStudent.Visible = True
                    lblPermissionNote.Visible = False

                Case "ADMIN"
                    ' Limited access
                    btnAddStudent.Visible = True
                    btnEditStudent.Visible = True
                    btnDeleteStudent.Visible = False
                    btnArchiveStudent.Visible = True
                    lblPermissionNote.Visible = True
                    lblPermissionNote.Text = "⚠ Limited Access: Only SuperAdmin can permanently delete students"

                Case "FACULTY"
                    ' View-only access
                    btnAddStudent.Visible = False
                    btnEditStudent.Visible = False
                    btnDeleteStudent.Visible = False
                    btnArchiveStudent.Visible = False
                    lblPermissionNote.Visible = True
                    lblPermissionNote.Text = "👁 View-Only Access: Faculty can view student records only"

                Case Else
                    ' No access
                    btnAddStudent.Visible = False
                    btnEditStudent.Visible = False
                    btnDeleteStudent.Visible = False
                    btnArchiveStudent.Visible = False
                    lblPermissionNote.Visible = True
                    lblPermissionNote.Text = "🚫 No Access: Insufficient permissions"
            End Select

        Catch ex As Exception
            Logger.LogError("Error setting up permissions", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Apply theme to form controls
    ''' </summary>
    Private Sub ApplyTheme()
        Try
            ThemeManager.StyleDataGridView(dgvStudents)
        Catch ex As Exception
            Logger.LogError("Error applying theme", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load all students into grid
    ''' </summary>
    Private Sub LoadStudents()
        Try
            Dim includeArchived As Boolean = chkShowArchived.Checked
            currentStudentData = StudentRepository.GetAllStudents(includeArchived)

            dgvStudents.DataSource = currentStudentData

            ' Format columns
            FormatStudentColumns()

            ' Update record count
            lblRecordCount.Text = $"Total Students: {currentStudentData.Rows.Count}"

            ' Clear selection
            Try
                dgvStudents.ClearSelection()
                If dgvStudents.Rows.Count > 0 Then
                    dgvStudents.CurrentCell = Nothing
                End If
            Catch ex As Exception
                ' Swallow selection exceptions
            End Try

        Catch ex As Exception
            Logger.LogError("Error loading students", ex)
            MessageBox.Show($"Error loading students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Format student grid columns
    ''' </summary>
    Private Sub FormatStudentColumns()
        Try
            If dgvStudents.Columns.Count > 0 Then
                ' Hide ID column
                If dgvStudents.Columns.Contains("id") Then
                    dgvStudents.Columns("id").Visible = False
                End If

                ' Format visible columns
                If dgvStudents.Columns.Contains("student_id") Then
                    dgvStudents.Columns("student_id").HeaderText = "Student ID"
                    dgvStudents.Columns("student_id").Width = 120
                End If

                If dgvStudents.Columns.Contains("name") Then
                    dgvStudents.Columns("name").HeaderText = "Full Name"
                    dgvStudents.Columns("name").Width = 200
                End If

                If dgvStudents.Columns.Contains("email") Then
                    dgvStudents.Columns("email").HeaderText = "Email"
                    dgvStudents.Columns("email").Width = 200
                End If

                If dgvStudents.Columns.Contains("phone_number") Then
                    dgvStudents.Columns("phone_number").HeaderText = "Phone Number"
                    dgvStudents.Columns("phone_number").Width = 130
                End If

                If dgvStudents.Columns.Contains("course") Then
                    dgvStudents.Columns("course").HeaderText = "Course/Program"
                    dgvStudents.Columns("course").Width = 250
                End If

                If dgvStudents.Columns.Contains("date_of_birth") Then
                    dgvStudents.Columns("date_of_birth").HeaderText = "Date of Birth"
                    dgvStudents.Columns("date_of_birth").Width = 120
                End If

                If dgvStudents.Columns.Contains("enrollment_date") Then
                    dgvStudents.Columns("enrollment_date").HeaderText = "Enrollment Date"
                    dgvStudents.Columns("enrollment_date").Width = 130
                End If

                If dgvStudents.Columns.Contains("status") Then
                    dgvStudents.Columns("status").HeaderText = "Status"
                    dgvStudents.Columns("status").Width = 100
                End If

                If dgvStudents.Columns.Contains("created_at") Then
                    dgvStudents.Columns("created_at").HeaderText = "Created At"
                    dgvStudents.Columns("created_at").Width = 150
                End If
            End If

            ' Color code status column
            ColorCodeStatusColumn()

        Catch ex As Exception
            Logger.LogError("Error formatting student columns", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Color code the status column
    ''' </summary>
    Private Sub ColorCodeStatusColumn()
        Try
            If dgvStudents.Columns.Contains("status") Then
                For Each row As DataGridViewRow In dgvStudents.Rows
                    If row.Cells("status").Value IsNot Nothing Then
                        Dim status As String = row.Cells("status").Value.ToString().ToLower()
                        Select Case status
                            Case "active"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(46, 139, 87)
                                row.Cells("status").Style.Font = New Font(dgvStudents.Font, FontStyle.Bold)
                            Case "inactive"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(243, 156, 18)
                                row.Cells("status").Style.Font = New Font(dgvStudents.Font, FontStyle.Bold)
                            Case "archived"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(149, 165, 166)
                                row.Cells("status").Style.Font = New Font(dgvStudents.Font, FontStyle.Bold)
                            Case "graduated"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(52, 152, 219)
                                row.Cells("status").Style.Font = New Font(dgvStudents.Font, FontStyle.Bold)
                        End Select
                    End If
                Next
            End If
        Catch ex As Exception
            Logger.LogError("Error color coding status column", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load course filter dropdown
    ''' </summary>
    Private Sub LoadCourseFilter()
        Try
            Dim dt As DataTable = StudentRepository.GetAllCourses()

            cmbCourseFilter.Items.Clear()
            cmbCourseFilter.Items.Add("All Courses")

            For Each row As DataRow In dt.Rows
                If Not IsDBNull(row("course")) AndAlso Not String.IsNullOrWhiteSpace(row("course").ToString()) Then
                    cmbCourseFilter.Items.Add(row("course").ToString())
                End If
            Next

            cmbCourseFilter.SelectedIndex = 0

        Catch ex As Exception
            Logger.LogError("Error loading course filter", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load status filter dropdown
    ''' </summary>

    Private Sub LoadStatusFilter()
        Try
            ' Clear existing items
            cmbStatusFilter.Items.Clear()

            ' Add valid status options
            Dim validStatuses As String() = {"All Status", "Active", "Inactive", "Graduated", "Archived"}
            cmbStatusFilter.Items.AddRange(validStatuses)

            ' Default to "All Status"
            cmbStatusFilter.SelectedIndex = 0

        Catch ex As Exception
            Logger.LogError("Error loading status filter", ex)
        End Try
    End Sub


    ''' <summary>
    ''' Load student statistics
    ''' </summary>
    Private Sub LoadStatistics()
        Try
            Dim stats As Dictionary(Of String, Integer) = StudentRepository.GetStudentStatistics()

            lblTotalStudents.Text = stats("TotalStudents").ToString()
            lblInactiveStudents.Text = stats("InactiveStudents").ToString()
            lblArchivedStudents.Text = stats("ArchivedStudents").ToString()
            lblGraduatedStudents.Text = stats("GraduatedStudents").ToString()
            lblTotalCourses.Text = stats("TotalCourses").ToString()

        Catch ex As Exception
            Logger.LogError("Error loading statistics", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Add student button click
    ''' </summary>
    Private Sub btnAddStudent_Click(sender As Object, e As EventArgs) Handles btnAddStudent.Click
        Try
            Dim addForm As New StudentFormDialog()
            If addForm.ShowDialog() = DialogResult.OK Then
                LoadStudents()
                LoadStatistics()
                MessageBox.Show("Student added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            Logger.LogError("Error opening add student form", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Edit student button click
    ''' </summary>
    Private Sub btnEditStudent_Click(sender As Object, e As EventArgs) Handles btnEditStudent.Click
        Try
            If dgvStudents.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a student to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim studentId As Integer = Convert.ToInt32(dgvStudents.SelectedRows(0).Cells("id").Value)
            Dim student As Student = StudentRepository.GetStudentById(studentId)

            If student IsNot Nothing Then
                Dim editForm As New StudentFormDialog(student)
                If editForm.ShowDialog() = DialogResult.OK Then
                    LoadStudents()
                    LoadStatistics()
                    MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Selected student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Logger.LogError("Error editing student", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Delete student button click
    ''' </summary>
    Private Sub btnDeleteStudent_Click(sender As Object, e As EventArgs) Handles btnDeleteStudent.Click
        Try
            If dgvStudents.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a student to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row = dgvStudents.SelectedRows(0)
            Dim studentId As Integer = Convert.ToInt32(row.Cells("id").Value)
            Dim studentName As String = row.Cells("name").Value.ToString()
            Dim studentIdNum As String = row.Cells("student_id").Value.ToString()

            Dim result = MessageBox.Show(
                $"Are you sure you want to permanently delete student '{studentName}' ({studentIdNum})?" & vbCrLf & vbCrLf &
                "This action cannot be undone and will also delete all attendance records for this student.",
                "Confirm Permanent Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then
                If StudentRepository.DeleteStudent(studentId) Then
                    LoadStudents()
                    LoadStatistics()
                    MessageBox.Show("Student deleted permanently.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Failed to delete student. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Catch ex As Exception
            Logger.LogError("Error deleting student", ex)
            MessageBox.Show($"Error deleting student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Archive/Restore student button click
    ''' </summary>
    Private Sub btnArchiveStudent_Click(sender As Object, e As EventArgs) Handles btnArchiveStudent.Click
        Try
            If dgvStudents.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a student to archive/restore.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim row = dgvStudents.SelectedRows(0)
            Dim studentId As Integer = Convert.ToInt32(row.Cells("id").Value)
            Dim studentName As String = row.Cells("name").Value.ToString()
            Dim studentIdNum As String = row.Cells("student_id").Value.ToString()
            Dim status As String = row.Cells("status").Value.ToString()

            Dim isArchived As Boolean = (status.ToLower() = "archived")

            Dim prompt As String = If(isArchived,
                $"Do you want to restore student '{studentName}' ({studentIdNum})?",
                $"Are you sure you want to archive student '{studentName}' ({studentIdNum})?")

            Dim result = MessageBox.Show(prompt,
                If(isArchived, "Confirm Restore", "Confirm Archive"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result <> DialogResult.Yes Then
                Return
            End If

            Dim success As Boolean = False
            If isArchived Then
                success = StudentRepository.RestoreStudent(studentId)
                If success Then MessageBox.Show($"Student '{studentName}' restored.", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                success = StudentRepository.ArchiveStudent(studentId)
                If success Then MessageBox.Show($"Student '{studentName}' archived.", "Archived", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            If success Then
                LoadStudents()
                LoadStatistics()
            Else
                MessageBox.Show("Operation failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Logger.LogError("Error archiving/restoring student", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Search button click
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim searchTerm As String = txtSearch.Text.Trim()
            Dim course As String = If(cmbCourseFilter.SelectedIndex > 0, cmbCourseFilter.SelectedItem.ToString(), "")
            Dim status As String = If(cmbStatusFilter.SelectedIndex > 0, cmbStatusFilter.SelectedItem.ToString(), "")

            If String.IsNullOrWhiteSpace(searchTerm) AndAlso String.IsNullOrWhiteSpace(course) AndAlso String.IsNullOrWhiteSpace(status) Then
                LoadStudents()
                Return
            End If

            currentStudentData = StudentRepository.SearchStudents(searchTerm, course, status)
            dgvStudents.DataSource = currentStudentData

            FormatStudentColumns()
            lblRecordCount.Text = $"Search Results: {currentStudentData.Rows.Count} student(s) found"

        Catch ex As Exception
            Logger.LogError("Error searching students", ex)
            MessageBox.Show($"Error searching students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Clear filters button click
    ''' </summary>
    Private Sub btnClearFilters_Click(sender As Object, e As EventArgs) Handles btnClearFilters.Click
        Try
            txtSearch.Clear()
            cmbCourseFilter.SelectedIndex = 0
            cmbStatusFilter.SelectedIndex = 0
            chkShowArchived.Checked = False
            LoadStudents()

        Catch ex As Exception
            Logger.LogError("Error clearing filters", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Refresh button click
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            LoadStudents()
            LoadCourseFilter()
            LoadStatistics()
            MessageBox.Show("Data refreshed successfully!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Logger.LogError("Error refreshing data", ex)
            MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Show archived checkbox changed
    ''' </summary>
    Private Sub chkShowArchived_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowArchived.CheckedChanged
        Try
            LoadStudents()
        Catch ex As Exception
            Logger.LogError("Error handling show archived change", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Export to CSV button click
    ''' </summary>
    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs) Handles btnExportCSV.Click
        Try
            If currentStudentData Is Nothing OrElse currentStudentData.Rows.Count = 0 Then
                MessageBox.Show("No data to export.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV Files (*.csv)|*.csv"
                sfd.FileName = $"Students_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv"

                If sfd.ShowDialog() = DialogResult.OK Then
                    Exporter.ExportToCSV(currentStudentData, sfd.FileName)
                    MessageBox.Show("Students exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If MessageBox.Show("Do you want to open the exported file?", "Open File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                    End If

                    Logger.LogInfo($"Students exported to {sfd.FileName}")
                End If
            End Using

        Catch ex As Exception
            Logger.LogError("Error exporting to CSV", ex)
            MessageBox.Show($"Error exporting: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Print report button click
    ''' </summary>
    Private Sub btnPrintReport_Click(sender As Object, e As EventArgs) Handles btnPrintReport.Click
        Try
            If currentStudentData Is Nothing OrElse currentStudentData.Rows.Count = 0 Then
                MessageBox.Show("No data to print.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Generate HTML report
            Dim html As String = GeneratePrintHTML()

            ' Show print preview
            Dim previewForm As New Form With {
                .Text = "Print Preview - Student Report",
                .Size = New Size(900, 700),
                .StartPosition = FormStartPosition.CenterParent
            }

            Dim webBrowser As New WebBrowser With {
                .Dock = DockStyle.Fill
            }
            webBrowser.DocumentText = html

            Dim printButton As New Button With {
                .Text = "Print",
                .Dock = DockStyle.Bottom,
                .Height = 50,
                .BackColor = Color.FromArgb(46, 139, 87),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 10, FontStyle.Bold)
            }

            AddHandler printButton.Click, Sub()
                                              webBrowser.ShowPrintDialog()
                                          End Sub

            previewForm.Controls.Add(webBrowser)
            previewForm.Controls.Add(printButton)
            previewForm.ShowDialog()

        Catch ex As Exception
            Logger.LogError("Error printing report", ex)
            MessageBox.Show($"Error printing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Generate HTML for print preview
    ''' </summary>
    Private Function GeneratePrintHTML() As String
        Try
            Dim html As New System.Text.StringBuilder()

            html.AppendLine("<html>")
            html.AppendLine("<head>")
            html.AppendLine("<style>")
            html.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; margin: 20px; }")
            html.AppendLine("h1 { color: #2E8B57; text-align: center; }")
            html.AppendLine("h2 { color: #2C3E50; }")
            html.AppendLine("table { border-collapse: collapse; width: 100%; margin-top: 20px; }")
            html.AppendLine("th { background-color: #2E8B57; color: white; padding: 10px; text-align: left; }")
            html.AppendLine("td { border: 1px solid #ddd; padding: 8px; }")
            html.AppendLine("tr:nth-child(even) { background-color: #f2f2f2; }")
            html.AppendLine(".active { color: #2E8B57; font-weight: bold; }")
            html.AppendLine(".inactive { color: #F39C12; font-weight: bold; }")
            html.AppendLine(".archived { color: #95A5A6; font-weight: bold; }")
            html.AppendLine(".graduated { color: #3498DB; font-weight: bold; }")
            html.AppendLine(".footer { margin-top: 30px; text-align: center; font-size: 12px; color: #7f8c8d; }")
            html.AppendLine("</style>")
            html.AppendLine("</head>")
            html.AppendLine("<body>")

            html.AppendLine("<h1>CSD Student Management System</h1>")
            html.AppendLine("<h2>Student Records Report</h2>")
            html.AppendLine($"<p><strong>Generated:</strong> {DateTime.Now:MMMM dd, yyyy hh:mm tt}</p>")
            html.AppendLine($"<p><strong>Total Students:</strong> {currentStudentData.Rows.Count}</p>")

            html.AppendLine("<table>")
            html.AppendLine("<tr>")
            html.AppendLine("<th>Student ID</th>")
            html.AppendLine("<th>Full Name</th>")
            html.AppendLine("<th>Email</th>")
            html.AppendLine("<th>Phone</th>")
            html.AppendLine("<th>Course/Program</th>")
            html.AppendLine("<th>Enrollment Date</th>")
            html.AppendLine("<th>Status</th>")
            html.AppendLine("</tr>")

            For Each row As DataRow In currentStudentData.Rows
                html.AppendLine("<tr>")
                html.AppendLine($"<td>{row("student_id")}</td>")
                html.AppendLine($"<td>{row("name")}</td>")
                html.AppendLine($"<td>{If(IsDBNull(row("email")), "", row("email"))}</td>")
                html.AppendLine($"<td>{If(IsDBNull(row("phone_number")), "", row("phone_number"))}</td>")
                html.AppendLine($"<td>{row("course")}</td>")
                html.AppendLine($"<td>{If(IsDBNull(row("enrollment_date")), "", Convert.ToDateTime(row("enrollment_date")).ToString("MM/dd/yyyy"))}</td>")

                Dim status As String = row("status").ToString()
                Dim statusClass As String = status.ToLower()
                html.AppendLine($"<td class='{statusClass}'>{status}</td>")
                html.AppendLine("</tr>")
            Next

            html.AppendLine("</table>")

            html.AppendLine("<div class='footer'>")
            html.AppendLine($"<p>Report generated by {currentUser.FullName} ({currentUser.Role})</p>")
            html.AppendLine("<p>&copy; 2025 CSD Student Management System. All rights reserved.</p>")
            html.AppendLine("</div>")

            html.AppendLine("</body>")
            html.AppendLine("</html>")

            Return html.ToString()

        Catch ex As Exception
            Logger.LogError("Error generating print HTML", ex)
            Return "<html><body><h1>Error generating report</h1></body></html>"
        End Try
    End Function

    ''' <summary>
    ''' Handle Enter key in search box
    ''' </summary>
    Private Sub txtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSearch.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            btnSearch.PerformClick()
        End If
    End Sub

    ''' <summary>
    ''' Handle form resize
    ''' </summary>
    Private Sub StudentManagementForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            If Me.Width > 0 AndAlso Me.Height > 0 Then
                pnlMain.Refresh()
            End If
        Catch ex As Exception
            ' Silently handle resize errors
        End Try
    End Sub

    ' 🔹 Auto-search with delay while typing
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            searchTimer.Stop()
            AddHandler searchTimer.Tick, AddressOf PerformDelayedSearch
            searchTimer.Start()
        Catch ex As Exception
            Logger.LogError("Auto-search debounce error", ex)
        End Try
    End Sub

    ' 🔹 The delayed search trigger
    Private Sub PerformDelayedSearch(sender As Object, e As EventArgs)
        Try
            searchTimer.Stop()
            btnSearch.PerformClick()
        Catch ex As Exception
            Logger.LogError("Error performing delayed search", ex)
        End Try
    End Sub



    ''' <summary>
    ''' Validate and normalize student status values
    ''' </summary>
    Private Sub ValidateStudentStatuses()
        Try
            If dgvStudents.Rows.Count = 0 Then Exit Sub

            For Each row As DataGridViewRow In dgvStudents.Rows
                If row.Cells("status").Value Is Nothing OrElse String.IsNullOrWhiteSpace(row.Cells("status").Value.ToString()) Then
                    row.Cells("status").Value = "Inactive"
                Else
                    Dim validStatuses As String() = {"active", "inactive", "graduated", "archived"}
                    Dim status As String = row.Cells("status").Value.ToString().Trim().ToLower()

                    If Not validStatuses.Contains(status) Then
                        row.Cells("status").Value = "Inactive"
                    End If
                End If
            Next

            ' Re-apply color coding after validation
            ColorCodeStatusColumn()

        Catch ex As Exception
            Logger.LogError("Error validating student statuses", ex)
        End Try
    End Sub


End Class