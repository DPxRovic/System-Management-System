' ==========================================
' FILENAME: /Forms/ReportsForm.vb
' PURPOSE: Comprehensive reports module for attendance statistics and analysis
' AUTHOR: System
' DATE: 2025-10-17
' ==========================================

Imports System.Data
Imports System.IO
Imports Guna.UI2.WinForms

Public Class ReportsForm
    Private currentUser As User
    Private reportData As DataTable
    Private chartType As String = "Student" ' Student, Course, or DateRange

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
    Private Sub ReportsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Set auto-scaling for responsive design
            Me.AutoScaleMode = AutoScaleMode.Dpi

            ' Initialize date pickers with current month
            dtpStartDate.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            dtpEndDate.Value = DateTime.Today

            ' Load initial data
            LoadStudents()
            LoadCourses()

            ' Set default report type
            rbStudent.Checked = True

            ' Load initial summary
            LoadOverallSummary()

            ' Apply theme and animations
            ApplyTheme()
            ThemeManager.SlideIn(pnlMain, "LEFT", 300)

            Logger.LogInfo("Reports form loaded successfully")

        Catch ex As Exception
            Logger.LogError("Error loading reports form", ex)
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Apply theme to form controls
    ''' </summary>
    Private Sub ApplyTheme()
        Try
            ThemeManager.StyleDataGridView(dgvReport)
        Catch ex As Exception
            Logger.LogError("Error applying theme", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load all students into dropdown
    ''' </summary>
    Private Sub LoadStudents()
        Try
            Dim dt As DataTable = ReportRepository.GetAllActiveStudents()

            If dt.Rows.Count > 0 Then
                cmbStudent.DataSource = dt
                cmbStudent.DisplayMember = "display_name"
                cmbStudent.ValueMember = "student_id"
                cmbStudent.SelectedIndex = -1
            Else
                cmbStudent.DataSource = Nothing
                cmbStudent.Items.Clear()
            End If

        Catch ex As Exception
            Logger.LogError("Error loading students", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load all courses into dropdown
    ''' </summary>
    Private Sub LoadCourses()
        Try
            Dim dt As DataTable = ReportRepository.GetAllActiveCourses()

            If dt.Rows.Count > 0 Then
                cmbCourse.DataSource = dt
                cmbCourse.DisplayMember = "course_name"
                cmbCourse.ValueMember = "id"
                cmbCourse.SelectedIndex = -1
            Else
                cmbCourse.DataSource = Nothing
                cmbCourse.Items.Clear()
            End If

        Catch ex As Exception
            Logger.LogError("Error loading courses", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load overall attendance summary statistics
    ''' </summary>
    Private Sub LoadOverallSummary()
        Try
            Dim stats As Dictionary(Of String, Integer) = ReportRepository.GetOverallAttendanceStats()

            ' Update statistics cards
            lblTotalRecords.Text = stats("TotalRecords").ToString()
            lblTotalPresent.Text = stats("PresentCount").ToString()
            lblTotalAbsent.Text = stats("AbsentCount").ToString()
            lblTotalLate.Text = stats("LateCount").ToString()

            ' Calculate and display overall attendance rate
            If stats("TotalRecords") > 0 Then
                Dim rate As Double = ((stats("PresentCount") + stats("LateCount")) / stats("TotalRecords")) * 100
                lblAttendanceRate.Text = $"{Math.Round(rate, 1)}%"
            Else
                lblAttendanceRate.Text = "0%"
            End If

        Catch ex As Exception
            Logger.LogError("Error loading overall summary", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Report type radio button changed
    ''' </summary>
    Private Sub rbStudent_CheckedChanged(sender As Object, e As EventArgs) Handles rbStudent.CheckedChanged
        If rbStudent.Checked Then
            chartType = "Student"
            cmbStudent.Enabled = True
            cmbCourse.Enabled = False
            lblReportTitle.Text = "Student Attendance Report"
        End If
    End Sub

    Private Sub rbCourse_CheckedChanged(sender As Object, e As EventArgs) Handles rbCourse.CheckedChanged
        If rbCourse.Checked Then
            chartType = "Course"
            cmbStudent.Enabled = False
            cmbCourse.Enabled = True
            lblReportTitle.Text = "Course Attendance Report"
        End If
    End Sub

    Private Sub rbDateRange_CheckedChanged(sender As Object, e As EventArgs) Handles rbDateRange.CheckedChanged
        If rbDateRange.Checked Then
            chartType = "DateRange"
            cmbStudent.Enabled = False
            cmbCourse.Enabled = False
            lblReportTitle.Text = "Date Range Attendance Report"
        End If
    End Sub

    ''' <summary>
    ''' Generate report button click
    ''' </summary>
    Private Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click
        Try
            ' Validate inputs based on report type
            If Not ValidateReportInputs() Then
                Return
            End If

            ' Show loading cursor
            Me.Cursor = Cursors.WaitCursor
            btnGenerateReport.Enabled = False

            ' Generate report based on selected type
            Select Case chartType
                Case "Student"
                    GenerateStudentReport()
                Case "Course"
                    GenerateCourseReport()
                Case "DateRange"
                    GenerateDateRangeReport()
            End Select

            ' Enable export/print buttons if data exists
            If reportData IsNot Nothing AndAlso reportData.Rows.Count > 0 Then
                btnExportCSV.Enabled = True
                btnPrintPreview.Enabled = True
            Else
                btnExportCSV.Enabled = False
                btnPrintPreview.Enabled = False
            End If

        Catch ex As Exception
            Logger.LogError("Error generating report", ex)
            MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            btnGenerateReport.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' Validate report inputs before generation
    ''' </summary>
    Private Function ValidateReportInputs() As Boolean
        Try
            Select Case chartType
                Case "Student"
                    If cmbStudent.SelectedIndex < 0 Then
                        MessageBox.Show("Please select a student.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        cmbStudent.Focus()
                        Return False
                    End If

                Case "Course"
                    If cmbCourse.SelectedIndex < 0 Then
                        MessageBox.Show("Please select a course.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        cmbCourse.Focus()
                        Return False
                    End If

                Case "DateRange"
                    If dtpStartDate.Value > dtpEndDate.Value Then
                        MessageBox.Show("Start date must be before end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        dtpStartDate.Focus()
                        Return False
                    End If
            End Select

            Return True

        Catch ex As Exception
            Logger.LogError("Error validating report inputs", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Generate student attendance report
    ''' </summary>
    Private Sub GenerateStudentReport()
        Try
            Dim studentId As String = cmbStudent.SelectedValue.ToString()
            Dim startDate As DateTime = dtpStartDate.Value.Date
            Dim endDate As DateTime = dtpEndDate.Value.Date

            ' Get student attendance data
            reportData = ReportRepository.GetStudentAttendanceReport(studentId, startDate, endDate)

            If reportData.Rows.Count > 0 Then
                ' Display in grid
                dgvReport.DataSource = reportData

                ' Format columns
                FormatStudentReportColumns()

                ' Get and display statistics
                Dim stats As Dictionary(Of String, Integer) = ReportRepository.GetStudentAttendanceStats(studentId, startDate, endDate)
                DisplayStudentStatistics(stats)

                ' Update record count
                lblRecordCount.Text = $"Total Records: {reportData.Rows.Count}"

                Logger.LogInfo($"Student report generated for {studentId}")
            Else
                MessageBox.Show("No attendance records found for the selected student and date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvReport.DataSource = Nothing
                lblRecordCount.Text = "Total Records: 0"
            End If

        Catch ex As Exception
            Logger.LogError("Error generating student report", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Format student report columns
    ''' </summary>
    Private Sub FormatStudentReportColumns()
        Try
            If dgvReport.Columns.Count > 0 Then
                If dgvReport.Columns.Contains("date") Then
                    dgvReport.Columns("date").HeaderText = "Date"
                    dgvReport.Columns("date").Width = 100
                End If
                If dgvReport.Columns.Contains("course_name") Then
                    dgvReport.Columns("course_name").HeaderText = "Course"
                    dgvReport.Columns("course_name").Width = 180
                End If
                If dgvReport.Columns.Contains("status") Then
                    dgvReport.Columns("status").HeaderText = "Status"
                    dgvReport.Columns("status").Width = 100
                End If
                If dgvReport.Columns.Contains("time_in") Then
                    dgvReport.Columns("time_in").HeaderText = "Time In"
                    dgvReport.Columns("time_in").Width = 100
                End If
                If dgvReport.Columns.Contains("time_out") Then
                    dgvReport.Columns("time_out").HeaderText = "Time Out"
                    dgvReport.Columns("time_out").Width = 100
                End If
                If dgvReport.Columns.Contains("remarks") Then
                    dgvReport.Columns("remarks").HeaderText = "Remarks"
                    dgvReport.Columns("remarks").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If
                If dgvReport.Columns.Contains("recorded_by") Then
                    dgvReport.Columns("recorded_by").HeaderText = "Recorded By"
                    dgvReport.Columns("recorded_by").Width = 120
                End If
            End If

            ' Color code status column
            ColorCodeStatusColumn()

        Catch ex As Exception
            Logger.LogError("Error formatting student report columns", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Display student statistics in summary panel
    ''' </summary>
    Private Sub DisplayStudentStatistics(stats As Dictionary(Of String, Integer))
        Try
            lblStatPresent.Text = $"Present: {stats("PresentCount")}"
            lblStatAbsent.Text = $"Absent: {stats("AbsentCount")}"
            lblStatLate.Text = $"Late: {stats("LateCount")}"
            lblStatExcused.Text = $"Excused: {stats("ExcusedCount")}"
            lblStatTotal.Text = $"Total Sessions: {stats("TotalRecords")}"

            ' Calculate attendance rate
            If stats("TotalRecords") > 0 Then
                Dim rate As Double = ((stats("PresentCount") + stats("LateCount") + stats("ExcusedCount")) / stats("TotalRecords")) * 100
                lblStatRate.Text = $"Attendance Rate: {Math.Round(rate, 1)}%"
                lblStatRate.ForeColor = If(rate >= 80, Color.FromArgb(46, 139, 87), Color.FromArgb(231, 76, 60))
            Else
                lblStatRate.Text = "Attendance Rate: N/A"
            End If

        Catch ex As Exception
            Logger.LogError("Error displaying student statistics", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Generate course attendance report
    ''' </summary>
    Private Sub GenerateCourseReport()
        Try
            Dim courseId As Integer = Convert.ToInt32(cmbCourse.SelectedValue)
            Dim startDate As DateTime = dtpStartDate.Value.Date
            Dim endDate As DateTime = dtpEndDate.Value.Date

            ' Get course attendance data
            reportData = ReportRepository.GetCourseAttendanceReport(courseId, startDate, endDate)

            If reportData.Rows.Count > 0 Then
                ' Display in grid
                dgvReport.DataSource = reportData

                ' Format columns
                FormatCourseReportColumns()

                ' Get and display statistics
                Dim stats As Dictionary(Of String, Integer) = ReportRepository.GetCourseAttendanceStats(courseId, startDate, endDate)
                DisplayCourseStatistics(stats)

                ' Update record count
                lblRecordCount.Text = $"Total Records: {reportData.Rows.Count}"

                Logger.LogInfo($"Course report generated for course ID {courseId}")
            Else
                MessageBox.Show("No attendance records found for the selected course and date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvReport.DataSource = Nothing
                lblRecordCount.Text = "Total Records: 0"
            End If

        Catch ex As Exception
            Logger.LogError("Error generating course report", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Format course report columns
    ''' </summary>
    Private Sub FormatCourseReportColumns()
        Try
            If dgvReport.Columns.Count > 0 Then
                If dgvReport.Columns.Contains("date") Then
                    dgvReport.Columns("date").HeaderText = "Date"
                    dgvReport.Columns("date").Width = 100
                End If
                If dgvReport.Columns.Contains("student_id") Then
                    dgvReport.Columns("student_id").HeaderText = "Student ID"
                    dgvReport.Columns("student_id").Width = 100
                End If
                If dgvReport.Columns.Contains("student_name") Then
                    dgvReport.Columns("student_name").HeaderText = "Student Name"
                    dgvReport.Columns("student_name").Width = 180
                End If
                If dgvReport.Columns.Contains("status") Then
                    dgvReport.Columns("status").HeaderText = "Status"
                    dgvReport.Columns("status").Width = 100
                End If
                If dgvReport.Columns.Contains("time_in") Then
                    dgvReport.Columns("time_in").HeaderText = "Time In"
                    dgvReport.Columns("time_in").Width = 100
                End If
                If dgvReport.Columns.Contains("remarks") Then
                    dgvReport.Columns("remarks").HeaderText = "Remarks"
                    dgvReport.Columns("remarks").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If
            End If

            ' Color code status column
            ColorCodeStatusColumn()

        Catch ex As Exception
            Logger.LogError("Error formatting course report columns", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Display course statistics in summary panel
    ''' </summary>
    Private Sub DisplayCourseStatistics(stats As Dictionary(Of String, Integer))
        Try
            lblStatPresent.Text = $"Present: {stats("PresentCount")}"
            lblStatAbsent.Text = $"Absent: {stats("AbsentCount")}"
            lblStatLate.Text = $"Late: {stats("LateCount")}"
            lblStatExcused.Text = $"Excused: {stats("ExcusedCount")}"
            lblStatTotal.Text = $"Total Sessions: {stats("TotalRecords")}"

            ' Calculate attendance rate
            If stats("TotalRecords") > 0 Then
                Dim rate As Double = ((stats("PresentCount") + stats("LateCount") + stats("ExcusedCount")) / stats("TotalRecords")) * 100
                lblStatRate.Text = $"Attendance Rate: {Math.Round(rate, 1)}%"
                lblStatRate.ForeColor = If(rate >= 80, Color.FromArgb(46, 139, 87), Color.FromArgb(231, 76, 60))
            Else
                lblStatRate.Text = "Attendance Rate: N/A"
            End If

        Catch ex As Exception
            Logger.LogError("Error displaying course statistics", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Generate date range attendance report
    ''' </summary>
    Private Sub GenerateDateRangeReport()
        Try
            Dim startDate As DateTime = dtpStartDate.Value.Date
            Dim endDate As DateTime = dtpEndDate.Value.Date

            ' Get date range attendance data
            reportData = ReportRepository.GetDateRangeAttendanceReport(startDate, endDate)

            If reportData.Rows.Count > 0 Then
                ' Display in grid
                dgvReport.DataSource = reportData

                ' Format columns
                FormatDateRangeReportColumns()

                ' Get and display statistics
                Dim stats As Dictionary(Of String, Integer) = ReportRepository.GetDateRangeAttendanceStats(startDate, endDate)
                DisplayDateRangeStatistics(stats)

                ' Update record count
                lblRecordCount.Text = $"Total Records: {reportData.Rows.Count}"

                Logger.LogInfo($"Date range report generated for {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}")
            Else
                MessageBox.Show("No attendance records found for the selected date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dgvReport.DataSource = Nothing
                lblRecordCount.Text = "Total Records: 0"
            End If

        Catch ex As Exception
            Logger.LogError("Error generating date range report", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Format date range report columns
    ''' </summary>
    Private Sub FormatDateRangeReportColumns()
        Try
            If dgvReport.Columns.Count > 0 Then
                If dgvReport.Columns.Contains("date") Then
                    dgvReport.Columns("date").HeaderText = "Date"
                    dgvReport.Columns("date").Width = 100
                End If
                If dgvReport.Columns.Contains("student_id") Then
                    dgvReport.Columns("student_id").HeaderText = "Student ID"
                    dgvReport.Columns("student_id").Width = 100
                End If
                If dgvReport.Columns.Contains("student_name") Then
                    dgvReport.Columns("student_name").HeaderText = "Student Name"
                    dgvReport.Columns("student_name").Width = 150
                End If
                If dgvReport.Columns.Contains("course_name") Then
                    dgvReport.Columns("course_name").HeaderText = "Course"
                    dgvReport.Columns("course_name").Width = 150
                End If
                If dgvReport.Columns.Contains("status") Then
                    dgvReport.Columns("status").HeaderText = "Status"
                    dgvReport.Columns("status").Width = 100
                End If
                If dgvReport.Columns.Contains("time_in") Then
                    dgvReport.Columns("time_in").HeaderText = "Time In"
                    dgvReport.Columns("time_in").Width = 100
                End If
                If dgvReport.Columns.Contains("recorded_by") Then
                    dgvReport.Columns("recorded_by").HeaderText = "Recorded By"
                    dgvReport.Columns("recorded_by").Width = 120
                End If
            End If

            ' Color code status column
            ColorCodeStatusColumn()

        Catch ex As Exception
            Logger.LogError("Error formatting date range report columns", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Display date range statistics in summary panel
    ''' </summary>
    Private Sub DisplayDateRangeStatistics(stats As Dictionary(Of String, Integer))
        Try
            lblStatPresent.Text = $"Present: {stats("PresentCount")}"
            lblStatAbsent.Text = $"Absent: {stats("AbsentCount")}"
            lblStatLate.Text = $"Late: {stats("LateCount")}"
            lblStatExcused.Text = $"Excused: {stats("ExcusedCount")}"
            lblStatTotal.Text = $"Total Records: {stats("TotalRecords")}"

            ' Calculate overall attendance rate
            If stats("TotalRecords") > 0 Then
                Dim rate As Double = ((stats("PresentCount") + stats("LateCount") + stats("ExcusedCount")) / stats("TotalRecords")) * 100
                lblStatRate.Text = $"Attendance Rate: {Math.Round(rate, 1)}%"
                lblStatRate.ForeColor = If(rate >= 80, Color.FromArgb(46, 139, 87), Color.FromArgb(231, 76, 60))
            Else
                lblStatRate.Text = "Attendance Rate: N/A"
            End If

        Catch ex As Exception
            Logger.LogError("Error displaying date range statistics", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Color code the status column in the grid
    ''' </summary>
    Private Sub ColorCodeStatusColumn()
        Try
            If dgvReport.Columns.Contains("status") Then
                For Each row As DataGridViewRow In dgvReport.Rows
                    If row.Cells("status").Value IsNot Nothing Then
                        Dim status As String = row.Cells("status").Value.ToString().ToLower()
                        Select Case status
                            Case "present"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(46, 139, 87)
                                row.Cells("status").Style.Font = New Font(dgvReport.Font, FontStyle.Bold)
                            Case "absent"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(231, 76, 60)
                                row.Cells("status").Style.Font = New Font(dgvReport.Font, FontStyle.Bold)
                            Case "late"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(243, 156, 18)
                                row.Cells("status").Style.Font = New Font(dgvReport.Font, FontStyle.Bold)
                            Case "excused"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(155, 89, 182)
                                row.Cells("status").Style.Font = New Font(dgvReport.Font, FontStyle.Bold)
                        End Select
                    End If
                Next
            End If
        Catch ex As Exception
            Logger.LogError("Error color coding status column", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Export to CSV button click
    ''' </summary>
    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs) Handles btnExportCSV.Click
        Try
            If reportData Is Nothing OrElse reportData.Rows.Count = 0 Then
                MessageBox.Show("No data to export. Please generate a report first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Create save file dialog
            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV Files (*.csv)|*.csv"
                sfd.FileName = $"Attendance_Report_{chartType}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"

                If sfd.ShowDialog() = DialogResult.OK Then
                    ' Export using Exporter utility
                    Exporter.ExportToCSV(reportData, sfd.FileName)

                    MessageBox.Show("Report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Ask if user wants to open the file
                    If MessageBox.Show("Do you want to open the exported file?", "Open File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                    End If

                    Logger.LogInfo($"Report exported to {sfd.FileName}")
                End If
            End Using

        Catch ex As Exception
            Logger.LogError("Error exporting to CSV", ex)
            MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Print preview button click
    ''' </summary>
    Private Sub btnPrintPreview_Click(sender As Object, e As EventArgs) Handles btnPrintPreview.Click
        Try
            If reportData Is Nothing OrElse reportData.Rows.Count = 0 Then
                MessageBox.Show("No data to print. Please generate a report first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Generate HTML for print preview
            Dim html As String = GeneratePrintHTML()

            ' Show print preview form
            Dim previewForm As New Form With {
                .Text = "Print Preview",
                .Size = New Size(900, 700),
                .StartPosition = FormStartPosition.CenterParent
            }

            Dim webBrowser As New WebBrowser With {
                .Dock = DockStyle.Fill
            }

            webBrowser.DocumentText = html

            ' Add print button
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

            Logger.LogInfo("Print preview opened")

        Catch ex As Exception
            Logger.LogError("Error showing print preview", ex)
            MessageBox.Show($"Error showing print preview: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            html.AppendLine(".present { color: #2E8B57; font-weight: bold; }")
            html.AppendLine(".absent { color: #E74C3C; font-weight: bold; }")
            html.AppendLine(".late { color: #F39C12; font-weight: bold; }")
            html.AppendLine(".excused { color: #9B59B6; font-weight: bold; }")
            html.AppendLine(".stats { margin: 20px 0; padding: 15px; background-color: #ecf0f1; border-radius: 5px; }")
            html.AppendLine(".footer { margin-top: 30px; text-align: center; font-size: 12px; color: #7f8c8d; }")
            html.AppendLine("</style>")
            html.AppendLine("</head>")
            html.AppendLine("<body>")

            ' Header
            html.AppendLine("<h1>CSD Student Management System</h1>")
            html.AppendLine($"<h2>{lblReportTitle.Text}</h2>")
            html.AppendLine($"<p><strong>Generated:</strong> {DateTime.Now:MMMM dd, yyyy hh:mm tt}</p>")
            html.AppendLine($"<p><strong>Date Range:</strong> {dtpStartDate.Value:MMMM dd, yyyy} - {dtpEndDate.Value:MMMM dd, yyyy}</p>")

            ' Statistics
            html.AppendLine("<div class='stats'>")
            html.AppendLine("<h3>Summary Statistics</h3>")
            html.AppendLine($"<p>{lblStatTotal.Text}</p>")
            html.AppendLine($"<p>{lblStatPresent.Text} | {lblStatAbsent.Text} | {lblStatLate.Text} | {lblStatExcused.Text}</p>")
            html.AppendLine($"<p><strong>{lblStatRate.Text}</strong></p>")
            html.AppendLine("</div>")

            ' Data table
            html.AppendLine("<table>")

            ' Table headers
            html.AppendLine("<tr>")
            For Each column As DataColumn In reportData.Columns
                html.AppendLine($"<th>{column.ColumnName.Replace("_", " ").ToUpper()}</th>")
            Next
            html.AppendLine("</tr>")

            ' Table data
            For Each row As DataRow In reportData.Rows
                html.AppendLine("<tr>")
                For Each column As DataColumn In reportData.Columns
                    Dim value As String = If(IsDBNull(row(column)), "", row(column).ToString())

                    ' Apply status styling if this is the status column
                    If column.ColumnName.ToLower() = "status" Then
                        Dim statusClass As String = value.ToLower()
                        html.AppendLine($"<td class='{statusClass}'>{value}</td>")
                    Else
                        html.AppendLine($"<td>{value}</td>")
                    End If
                Next
                html.AppendLine("</tr>")
            Next

            html.AppendLine("</table>")

            ' Footer
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
    ''' Refresh button click
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ' Reload dropdowns
            LoadStudents()
            LoadCourses()

            ' Reload overall summary
            LoadOverallSummary()

            ' Clear current report
            dgvReport.DataSource = Nothing
            reportData = Nothing
            lblRecordCount.Text = "Total Records: 0"

            ' Reset statistics
            lblStatPresent.Text = "Present: 0"
            lblStatAbsent.Text = "Absent: 0"
            lblStatLate.Text = "Late: 0"
            lblStatExcused.Text = "Excused: 0"
            lblStatTotal.Text = "Total Sessions: 0"
            lblStatRate.Text = "Attendance Rate: N/A"

            ' Disable export/print buttons
            btnExportCSV.Enabled = False
            btnPrintPreview.Enabled = False

            MessageBox.Show("Data refreshed successfully!", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Logger.LogError("Error refreshing data", ex)
            MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Clear filters button click
    ''' </summary>
    Private Sub btnClearFilters_Click(sender As Object, e As EventArgs) Handles btnClearFilters.Click
        Try
            ' Reset all filters
            cmbStudent.SelectedIndex = -1
            cmbCourse.SelectedIndex = -1
            dtpStartDate.Value = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
            dtpEndDate.Value = DateTime.Today
            rbStudent.Checked = True

            ' Clear report data
            dgvReport.DataSource = Nothing
            reportData = Nothing
            lblRecordCount.Text = "Total Records: 0"

            ' Reset statistics
            lblStatPresent.Text = "Present: 0"
            lblStatAbsent.Text = "Absent: 0"
            lblStatLate.Text = "Late: 0"
            lblStatExcused.Text = "Excused: 0"
            lblStatTotal.Text = "Total Sessions: 0"
            lblStatRate.Text = "Attendance Rate: N/A"

            ' Disable export/print buttons
            btnExportCSV.Enabled = False
            btnPrintPreview.Enabled = False

        Catch ex As Exception
            Logger.LogError("Error clearing filters", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handle form resize to adjust layout
    ''' </summary>
    Private Sub ReportsForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            If Me.Width > 0 AndAlso Me.Height > 0 Then
                ' Ensure proper layout when form is resized
                pnlMain.Refresh()
            End If
        Catch ex As Exception
            ' Silently handle resize errors
        End Try
    End Sub

End Class