' ==========================================
' FILENAME: /Forms/AttendanceForm.vb
' PURPOSE: Enhanced student attendance recording form with scrolling and filters
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Imports System.Data
Imports System.IO
Imports System.Text

Public Class AttendanceForm
    Private currentUser As User
    Private currentStudent As Student = Nothing
    Private selectedCourseId As Integer = 1 ' Default course ID

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
    Private Sub AttendanceForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Set auto-scaling for responsive design
            Me.AutoScaleMode = AutoScaleMode.Dpi

            ' Load courses into combo box
            LoadCourses()

            ' Set date filters to current week
            dtpStartDate.Value = DateTime.Today.AddDays(-7)
            dtpEndDate.Value = DateTime.Today

            ' Set initial state
            ResetForm()

            ' Load today's attendance
            LoadTodayAttendance()

            ' Update statistics
            UpdateTodayStatistics()

            ' Set focus to student ID textbox
            txtStudentId.Focus()

            ' Fix button layout - hide Present, show Absent first
            btnPresent.Visible = False
            btnAbsent.Location = New Point(240, 360)
            btnLate.Location = New Point(413, 360)
            btnExcused.Location = New Point(627, 360)
            btnClear.Location = New Point(840, 360)

            ' Configure scroll container for proper sizing
            ConfigureResponsiveLayout()

            Logger.LogInfo("Attendance form loaded")
        Catch ex As Exception
            Logger.LogError("Error loading attendance form", ex)
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Configure responsive layout for the form
    ''' </summary>
    Private Sub ConfigureResponsiveLayout()
        Try
            ' Ensure scroll container fills the form
            pnlScrollContainer.Dock = DockStyle.Fill

            ' Set minimum sizes to prevent layout issues
            pnlScrollContainer.AutoScroll = True
            pnlScrollContainer.AutoScrollMinSize = New Size(1200, 1400)

            ' Configure content panel
            pnlContent.AutoSize = True
            pnlContent.MinimumSize = New Size(1200, 0)

            ' Set anchor properties for bottom panel
            pnlBottom.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

        Catch ex As Exception
            Logger.LogError("Error configuring responsive layout", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load active courses into combo box
    ''' </summary>
    Private Sub LoadCourses()
        Try
            Dim dt As DataTable = AttendanceRepository.GetActiveCourses()

            If dt.Rows.Count > 0 Then
                cmbCourse.DataSource = dt
                cmbCourse.DisplayMember = "course_name"
                cmbCourse.ValueMember = "id"
                cmbCourse.SelectedIndex = 0
            Else
                ' Add default course if none exist
                Dim defaultTable As New DataTable()
                defaultTable.Columns.Add("id", GetType(Integer))
                defaultTable.Columns.Add("course_name", GetType(String))
                defaultTable.Rows.Add(1, "General Attendance")

                cmbCourse.DataSource = defaultTable
                cmbCourse.DisplayMember = "course_name"
                cmbCourse.ValueMember = "id"
            End If
        Catch ex As Exception
            Logger.LogError("Error loading courses", ex)
            ' Set default value
            Dim defaultTable As New DataTable()
            defaultTable.Columns.Add("id", GetType(Integer))
            defaultTable.Columns.Add("course_name", GetType(String))
            defaultTable.Rows.Add(1, "General Attendance")

            cmbCourse.DataSource = defaultTable
            cmbCourse.DisplayMember = "course_name"
            cmbCourse.ValueMember = "id"
        End Try
    End Sub

    ''' <summary>
    ''' Student ID text changed event - Auto-load student info
    ''' </summary>
    Private Sub txtStudentId_TextChanged(sender As Object, e As EventArgs) Handles txtStudentId.TextChanged
        Try
            ' Clear student info if text is empty
            If String.IsNullOrWhiteSpace(txtStudentId.Text) Then
                ClearStudentInfo()
                Return
            End If

            ' Load student info after user stops typing (debounce)
            tmrStudentIdDebounce.Stop()
            tmrStudentIdDebounce.Start()

        Catch ex As Exception
            Logger.LogError("Error in student ID text changed", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Timer tick for debouncing student ID lookup
    ''' </summary>
    Private Sub tmrStudentIdDebounce_Tick(sender As Object, e As EventArgs) Handles tmrStudentIdDebounce.Tick
        tmrStudentIdDebounce.Stop()
        LoadStudentInfo()
    End Sub

    ''' <summary>
    ''' Load student information by student ID
    ''' </summary>
    Private Sub LoadStudentInfo()
        Try
            Dim studentId As String = txtStudentId.Text.Trim()

            If String.IsNullOrWhiteSpace(studentId) Then
                ClearStudentInfo()
                Return
            End If

            ' Get student from database
            currentStudent = AttendanceRepository.GetStudentByStudentId(studentId)

            If currentStudent IsNot Nothing Then
                ' Display student information
                lblStudentName.Text = currentStudent.Name
                lblStudentName.ForeColor = Color.FromArgb(46, 139, 87)
                lblCourse.Text = $"Course: {currentStudent.Course}"
                lblCourse.Visible = True

                ' Enable attendance buttons
                btnPresent.Enabled = True
                btnAbsent.Enabled = True
                btnLate.Enabled = True
                btnExcused.Enabled = True

                ' Check if attendance already recorded today
                selectedCourseId = If(cmbCourse.SelectedValue IsNot Nothing, Convert.ToInt32(cmbCourse.SelectedValue), 1)

                If AttendanceRepository.AttendanceExistsToday(studentId, selectedCourseId) Then
                    lblStatus.Text = "✓ Attendance already recorded today (will update if you mark again)"
                    lblStatus.ForeColor = Color.FromArgb(243, 156, 18)
                    lblStatus.Visible = True
                Else
                    lblStatus.Visible = False
                End If

            Else
                ' Student not found
                lblStudentName.Text = "Student not found"
                lblStudentName.ForeColor = Color.FromArgb(231, 76, 60)
                lblCourse.Visible = False
                lblStatus.Visible = False

                ' Disable attendance buttons
                btnPresent.Enabled = False
                btnAbsent.Enabled = False
                btnLate.Enabled = False
                btnExcused.Enabled = False
            End If

        Catch ex As Exception
            Logger.LogError("Error loading student info", ex)
            MessageBox.Show($"Error loading student information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Clear student information display
    ''' </summary>
    Private Sub ClearStudentInfo()
        lblStudentName.Text = "Enter Student ID"
        lblStudentName.ForeColor = Color.FromArgb(127, 140, 141)
        lblCourse.Visible = False
        lblStatus.Visible = False
        currentStudent = Nothing

        ' Disable attendance buttons
        btnPresent.Enabled = False
        btnAbsent.Enabled = False
        btnLate.Enabled = False
        btnExcused.Enabled = False
    End Sub

    ''' <summary>
    ''' Present button click
    ''' </summary>
    Private Sub btnPresent_Click(sender As Object, e As EventArgs) Handles btnPresent.Click
        RecordAttendance("Present")
    End Sub

    ''' <summary>
    ''' Absent button click
    ''' </summary>
    Private Sub btnAbsent_Click(sender As Object, e As EventArgs) Handles btnAbsent.Click
        RecordAttendance("Absent")
    End Sub

    ''' <summary>
    ''' Late button click
    ''' </summary>
    Private Sub btnLate_Click(sender As Object, e As EventArgs) Handles btnLate.Click
        RecordAttendance("Late")
    End Sub

    ''' <summary>
    ''' Excused button click
    ''' </summary>
    Private Sub btnExcused_Click(sender As Object, e As EventArgs) Handles btnExcused.Click
        RecordAttendance("Excused")
    End Sub

    ''' <summary>
    ''' Record attendance with specified status
    ''' </summary>
    Private Sub RecordAttendance(status As String)
        Try
            If currentStudent Is Nothing Then
                MessageBox.Show("Please enter a valid student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtStudentId.Focus()
                Return
            End If

            ' Get selected course ID
            selectedCourseId = If(cmbCourse.SelectedValue IsNot Nothing, Convert.ToInt32(cmbCourse.SelectedValue), 1)

            ' Get remarks if any
            Dim remarks As String = txtRemarks.Text.Trim()

            ' Record attendance
            Dim success As Boolean = AttendanceRepository.RecordAttendance(
                currentStudent.StudentId,
                selectedCourseId,
                status,
                currentUser.Username,
                remarks
            )

            If success Then
                ' Show success toast
                ShowToastNotification($"✓ Attendance Recorded: {status}", Color.FromArgb(46, 204, 113))

                ' Update status label
                lblStatus.Text = $"✓ Marked as {status}"
                lblStatus.ForeColor = Color.FromArgb(46, 139, 87)
                lblStatus.Visible = True

                ' Load today's attendance summary
                LoadTodayAttendance()

                ' Update statistics
                UpdateTodayStatistics()

                ' Reset form after short delay
                tmrResetForm.Start()
            Else
                MessageBox.Show("Failed to record attendance. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Logger.LogError($"Error recording attendance: {status}", ex)
            MessageBox.Show($"Error recording attendance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Show toast notification
    ''' </summary>
    Private Sub ShowToastNotification(message As String, backgroundColor As Color)
        Try
            pnlToast.BackColor = backgroundColor
            lblToastMessage.Text = message
            pnlToast.Visible = True
            pnlToast.BringToFront()

            ' Auto-hide after 3 seconds
            tmrToast.Start()
        Catch ex As Exception
            Logger.LogError("Error showing toast notification", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Toast timer tick - Hide toast
    ''' </summary>
    Private Sub tmrToast_Tick(sender As Object, e As EventArgs) Handles tmrToast.Tick
        tmrToast.Stop()
        pnlToast.Visible = False
    End Sub

    ''' <summary>
    ''' Reset form timer tick
    ''' </summary>
    Private Sub tmrResetForm_Tick(sender As Object, e As EventArgs) Handles tmrResetForm.Tick
        tmrResetForm.Stop()
        ResetForm()
    End Sub

    ''' <summary>
    ''' Reset form to initial state
    ''' </summary>
    Private Sub ResetForm()
        txtStudentId.Clear()
        txtRemarks.Clear()
        ClearStudentInfo()
        txtStudentId.Focus()
    End Sub

    ''' <summary>
    ''' Load today's attendance summary
    ''' </summary>
    Private Sub LoadTodayAttendance()
        Try
            Dim dt As DataTable = AttendanceRepository.GetTodayAttendanceSummary()
            dgvTodayAttendance.DataSource = dt

            ' Format DataGridView
            If dgvTodayAttendance.Columns.Count > 0 Then
                dgvTodayAttendance.Columns("student_id").HeaderText = "Student ID"
                dgvTodayAttendance.Columns("student_id").Width = 120
                dgvTodayAttendance.Columns("student_name").HeaderText = "Student Name"
                dgvTodayAttendance.Columns("student_name").Width = 200
                dgvTodayAttendance.Columns("course_name").HeaderText = "Course"
                dgvTodayAttendance.Columns("course_name").Width = 180
                dgvTodayAttendance.Columns("status").HeaderText = "Status"
                dgvTodayAttendance.Columns("status").Width = 100
                dgvTodayAttendance.Columns("time_in").HeaderText = "Time In"
                dgvTodayAttendance.Columns("time_in").Width = 120
                dgvTodayAttendance.Columns("recorded_by").HeaderText = "Recorded By"
                dgvTodayAttendance.Columns("recorded_by").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                ' Color code status column
                For Each row As DataGridViewRow In dgvTodayAttendance.Rows
                    If row.Cells("status").Value IsNot Nothing Then
                        Dim status As String = row.Cells("status").Value.ToString()
                        Select Case status.ToLower()
                            Case "present"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(46, 139, 87)
                                row.Cells("status").Style.Font = New Font(dgvTodayAttendance.Font, FontStyle.Bold)
                            Case "absent"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(231, 76, 60)
                                row.Cells("status").Style.Font = New Font(dgvTodayAttendance.Font, FontStyle.Bold)
                            Case "late"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(243, 156, 18)
                                row.Cells("status").Style.Font = New Font(dgvTodayAttendance.Font, FontStyle.Bold)
                            Case "excused"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(155, 89, 182)
                                row.Cells("status").Style.Font = New Font(dgvTodayAttendance.Font, FontStyle.Bold)
                        End Select
                    End If
                Next
            End If

            ' Update count label
            lblTodayCount.Text = $"Total Records: {dt.Rows.Count}"

        Catch ex As Exception
            Logger.LogError("Error loading today's attendance", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Update today's attendance statistics
    ''' </summary>
    Private Sub UpdateTodayStatistics()
        Try
            Dim dt As DataTable = AttendanceRepository.GetTodayAttendanceSummary()

            Dim presentCount As Integer = 0
            Dim absentCount As Integer = 0
            Dim lateCount As Integer = 0

            For Each row As DataRow In dt.Rows
                Dim status As String = row("status").ToString().ToLower()
                Select Case status
                    Case "present"
                        presentCount += 1
                    Case "absent"
                        absentCount += 1
                    Case "late"
                        lateCount += 1
                End Select
            Next

            lblPresentCount.Text = presentCount.ToString()
            lblAbsentCount.Text = absentCount.ToString()
            lblLateCount.Text = lateCount.ToString()

        Catch ex As Exception
            Logger.LogError("Error updating statistics", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clear button click
    ''' </summary>
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ResetForm()
    End Sub

    ''' <summary>
    ''' Refresh button click
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadTodayAttendance()
        UpdateTodayStatistics()
        ShowToastNotification("✓ Data Refreshed", Color.FromArgb(52, 152, 219))
    End Sub

    ''' <summary>
    ''' Apply date filter button click
    ''' </summary>
    Private Sub btnApplyFilter_Click(sender As Object, e As EventArgs) Handles btnApplyFilter.Click
        Try
            Dim startDate As DateTime = dtpStartDate.Value.Date
            Dim endDate As DateTime = dtpEndDate.Value.Date

            If startDate > endDate Then
                MessageBox.Show("Start date must be before end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Load filtered attendance data
            LoadFilteredAttendance(startDate, endDate)

        Catch ex As Exception
            Logger.LogError("Error applying date filter", ex)
            MessageBox.Show($"Error applying filter: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Load filtered attendance data
    ''' </summary>
    Private Sub LoadFilteredAttendance(startDate As DateTime, endDate As DateTime)
        Try
            Dim query As String = "
                SELECT a.student_id, s.name AS student_name, 
                       c.course_name, a.status, 
                       DATE_FORMAT(a.date, '%Y-%m-%d') as date,
                       TIME_FORMAT(a.time_in, '%H:%i') as time_in, 
                       a.recorded_by
                FROM attendance a
                INNER JOIN students s ON a.student_id = s.student_id
                LEFT JOIN courses c ON a.course_id = c.id
                WHERE a.date BETWEEN @start_date AND @end_date
                ORDER BY a.date DESC, a.time_in DESC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@start_date", startDate.ToString("yyyy-MM-dd")},
                {"@end_date", endDate.ToString("yyyy-MM-dd")}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)
            dgvTodayAttendance.DataSource = dt

            ' Format DataGridView
            If dgvTodayAttendance.Columns.Count > 0 Then
                dgvTodayAttendance.Columns("student_id").HeaderText = "Student ID"
                dgvTodayAttendance.Columns("student_id").Width = 120
                dgvTodayAttendance.Columns("student_name").HeaderText = "Student Name"
                dgvTodayAttendance.Columns("student_name").Width = 180
                dgvTodayAttendance.Columns("course_name").HeaderText = "Course"
                dgvTodayAttendance.Columns("course_name").Width = 160
                dgvTodayAttendance.Columns("date").HeaderText = "Date"
                dgvTodayAttendance.Columns("date").Width = 100
                dgvTodayAttendance.Columns("status").HeaderText = "Status"
                dgvTodayAttendance.Columns("status").Width = 100
                dgvTodayAttendance.Columns("time_in").HeaderText = "Time In"
                dgvTodayAttendance.Columns("time_in").Width = 100
                dgvTodayAttendance.Columns("recorded_by").HeaderText = "Recorded By"
                dgvTodayAttendance.Columns("recorded_by").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                ' Color code status column
                For Each row As DataGridViewRow In dgvTodayAttendance.Rows
                    If row.Cells("status").Value IsNot Nothing Then
                        Dim status As String = row.Cells("status").Value.ToString()
                        Select Case status.ToLower()
                            Case "present"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(46, 139, 87)
                                row.Cells("status").Style.Font = New Font(dgvTodayAttendance.Font, FontStyle.Bold)
                            Case "absent"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(231, 76, 60)
                                row.Cells("status").Style.Font = New Font(dgvTodayAttendance.Font, FontStyle.Bold)
                            Case "late"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(243, 156, 18)
                                row.Cells("status").Style.Font = New Font(dgvTodayAttendance.Font, FontStyle.Bold)
                            Case "excused"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(155, 89, 182)
                                row.Cells("status").Style.Font = New Font(dgvTodayAttendance.Font, FontStyle.Bold)
                        End Select
                    End If
                Next
            End If

            lblTodayCount.Text = $"Total Records: {dt.Rows.Count} ({startDate:MMM dd} - {endDate:MMM dd})"

            ' Update statistics for filtered period
            UpdateFilteredStatistics(dt)

        Catch ex As Exception
            Logger.LogError("Error loading filtered attendance", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Update statistics for filtered data
    ''' </summary>
    Private Sub UpdateFilteredStatistics(dt As DataTable)
        Try
            Dim presentCount As Integer = 0
            Dim absentCount As Integer = 0
            Dim lateCount As Integer = 0

            For Each row As DataRow In dt.Rows
                Dim status As String = row("status").ToString().ToLower()
                Select Case status
                    Case "present"
                        presentCount += 1
                    Case "absent"
                        absentCount += 1
                    Case "late"
                        lateCount += 1
                End Select
            Next

            lblPresentCount.Text = presentCount.ToString()
            lblAbsentCount.Text = absentCount.ToString()
            lblLateCount.Text = lateCount.ToString()

        Catch ex As Exception
            Logger.LogError("Error updating filtered statistics", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Export attendance to CSV
    ''' </summary>
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If dgvTodayAttendance.Rows.Count = 0 Then
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Create save file dialog
            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV Files (*.csv)|*.csv"
                sfd.FileName = $"Attendance_Report_{DateTime.Now:yyyyMMdd_HHmmss}.csv"

                If sfd.ShowDialog() = DialogResult.OK Then
                    ExportToCSV(sfd.FileName)
                    MessageBox.Show("Data exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Ask if user wants to open the file
                    If MessageBox.Show("Do you want to open the exported file?", "Open File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                    End If
                End If
            End Using

        Catch ex As Exception
            Logger.LogError("Error exporting to CSV", ex)
            MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Export DataGridView to CSV file
    ''' </summary>
    Private Sub ExportToCSV(filePath As String)
        Try
            Dim sb As New StringBuilder()

            ' Write headers
            Dim headers As New List(Of String)()
            For Each column As DataGridViewColumn In dgvTodayAttendance.Columns
                If column.Visible Then
                    headers.Add(column.HeaderText)
                End If
            Next
            sb.AppendLine(String.Join(",", headers))

            ' Write data rows
            For Each row As DataGridViewRow In dgvTodayAttendance.Rows
                If Not row.IsNewRow Then
                    Dim cells As New List(Of String)()
                    For Each cell As DataGridViewCell In row.Cells
                        If cell.OwningColumn.Visible Then
                            Dim value As String = If(cell.Value?.ToString(), "")
                            ' Escape commas and quotes
                            If value.Contains(",") OrElse value.Contains("""") Then
                                value = """" & value.Replace("""", """""") & """"
                            End If
                            cells.Add(value)
                        End If
                    Next
                    sb.AppendLine(String.Join(",", cells))
                End If
            Next

            ' Write to file
            File.WriteAllText(filePath, sb.ToString())

            Logger.LogInfo($"Attendance data exported to {filePath}")

        Catch ex As Exception
            Logger.LogError("Error writing CSV file", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Course selection changed
    ''' </summary>
    Private Sub cmbCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCourse.SelectedIndexChanged
        ' Reload student info to check attendance for selected course
        If Not String.IsNullOrWhiteSpace(txtStudentId.Text) Then
            LoadStudentInfo()
        End If
    End Sub

    ''' <summary>
    ''' Handle Enter key in student ID textbox
    ''' </summary>
    Private Sub txtStudentId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStudentId.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            If btnAbsent.Enabled Then
                btnAbsent.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' DataGridView cell double click - View/Edit attendance
    ''' </summary>
    Private Sub dgvTodayAttendance_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvTodayAttendance.CellDoubleClick
        Try
            If e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = dgvTodayAttendance.Rows(e.RowIndex)
                Dim studentId As String = row.Cells("student_id").Value.ToString()

                ' Load student info
                txtStudentId.Text = studentId

                ' Scroll to top
                pnlScrollContainer.ScrollControlIntoView(pnlAttendance)
            End If
        Catch ex As Exception
            Logger.LogError("Error handling cell double click", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handle form resize to adjust layout
    ''' </summary>
    Private Sub AttendanceForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            ' Ensure proper layout when form is resized
            If Me.Width > 0 AndAlso Me.Height > 0 Then
                pnlContent.Width = Math.Max(1200, pnlScrollContainer.ClientSize.Width - 40)
                pnlScrollContainer.Refresh()
            End If
        Catch ex As Exception
            ' Silently handle resize errors
        End Try
    End Sub

    ''' <summary>
    ''' Handle scroll container resize
    ''' </summary>
    Private Sub pnlScrollContainer_Resize(sender As Object, e As EventArgs) Handles pnlScrollContainer.Resize
        Try
            ' Adjust content width when scroll container resizes
            If pnlScrollContainer.ClientSize.Width > 0 Then
                Dim availableWidth As Integer = pnlScrollContainer.ClientSize.Width - 40
                If availableWidth > 1200 Then
                    pnlContent.Width = availableWidth
                End If
            End If
        Catch ex As Exception
            ' Silently handle resize errors
        End Try
    End Sub

End Class