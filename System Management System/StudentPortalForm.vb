' ==========================================
' FILENAME: /Forms/StudentPortalForm.vb
' PURPOSE: Student portal interface for personal academic information access
' AUTHOR: System
' DATE: 2025-10-20
' ==========================================

Imports System.Data
Imports Guna.UI2.WinForms

Public Class StudentPortalForm
    Private currentUser As User
    Private currentStudent As Student
    Private studentId As String

    ''' <summary>
    ''' Constructor with user parameter
    ''' </summary>
    Public Sub New(user As User)
        InitializeComponent()
        currentUser = user
        ' For student accounts, username typically equals student_id
        studentId = user.Username
    End Sub

    ''' <summary>
    ''' Form load event
    ''' </summary>
    Private Sub StudentPortalForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Validate student access
            If Not StudentPortalRepository.ValidateStudentAccess(studentId) Then
                MessageBox.Show("Unable to access student portal. Please contact administration.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            ' Load student profile
            currentStudent = StudentPortalRepository.GetStudentProfile(studentId)
            If currentStudent Is Nothing Then
                MessageBox.Show("Student profile not found. Please contact administration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Close()
                Return
            End If

            ' Initialize date pickers
            dtpStartDate.Value = DateTime.Today.AddMonths(-1)
            dtpEndDate.Value = DateTime.Today

            ' Load all sections
            LoadProfileData()
            LoadAttendanceData()
            LoadCoursesData()
            LoadStatistics()

            ' Apply theme
            ApplyTheme()
            ThemeManager.SlideIn(pnlMain, "LEFT", 300)

            Logger.LogInfo($"Student portal loaded for {studentId}")

        Catch ex As Exception
            Logger.LogError("Error loading student portal", ex)
            MessageBox.Show($"Error loading portal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Apply theme to form controls
    ''' </summary>
    Private Sub ApplyTheme()
        Try
            ThemeManager.StyleDataGridView(dgvAttendance)
            ThemeManager.StyleDataGridView(dgvCourses)
        Catch ex As Exception
            Logger.LogError("Error applying theme", ex)
        End Try
    End Sub

#Region "Profile Section"

    ''' <summary>
    ''' Load student profile data
    ''' </summary>
    Private Sub LoadProfileData()
        Try
            If currentStudent Is Nothing Then Return

            ' Basic Information
            lblStudentIdValue.Text = currentStudent.StudentId
            lblNameValue.Text = currentStudent.Name
            lblEmailValue.Text = If(String.IsNullOrWhiteSpace(currentStudent.Email), "Not provided", currentStudent.Email)
            lblPhoneValue.Text = If(String.IsNullOrWhiteSpace(currentStudent.PhoneNumber), "Not provided", currentStudent.PhoneNumber)
            lblCourseValue.Text = currentStudent.Course

            ' Date Information
            If currentStudent.DateOfBirth.HasValue Then
                lblDOBValue.Text = currentStudent.DateOfBirth.Value.ToString("MMMM dd, yyyy")
                If currentStudent.Age.HasValue Then
                    lblDOBValue.Text &= $" ({currentStudent.Age} years old)"
                End If
            Else
                lblDOBValue.Text = "Not on file"
            End If

            If currentStudent.EnrollmentDate.HasValue Then
                lblEnrollmentValue.Text = currentStudent.EnrollmentDate.Value.ToString("MMMM dd, yyyy")
            Else
                lblEnrollmentValue.Text = "Not available"
            End If

            ' Status with color coding
            lblStatusValue.Text = currentStudent.Status
            Select Case currentStudent.Status.ToLower()
                Case "active"
                    lblStatusValue.ForeColor = Color.FromArgb(46, 139, 87)
                Case "inactive"
                    lblStatusValue.ForeColor = Color.FromArgb(243, 156, 18)
                Case Else
                    lblStatusValue.ForeColor = Color.FromArgb(149, 165, 166)
            End Select

        Catch ex As Exception
            Logger.LogError("Error loading profile data", ex)
        End Try
    End Sub

#End Region

#Region "Attendance Section"

    ''' <summary>
    ''' Load attendance data with date filter
    ''' </summary>
    Private Sub LoadAttendanceData()
        Try
            Dim startDate As DateTime = dtpStartDate.Value.Date
            Dim endDate As DateTime = dtpEndDate.Value.Date

            ' Load attendance records
            Dim dt As DataTable = StudentPortalRepository.GetStudentAttendance(studentId, startDate, endDate)
            dgvAttendance.DataSource = dt

            ' Format columns
            FormatAttendanceColumns()

            ' Load attendance statistics
            LoadAttendanceStatistics(startDate, endDate)

            ' Update record count
            lblAttendanceCount.Text = $"Records: {dt.Rows.Count}"

        Catch ex As Exception
            Logger.LogError("Error loading attendance data", ex)
            MessageBox.Show($"Error loading attendance: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Format attendance grid columns
    ''' </summary>
    Private Sub FormatAttendanceColumns()
        Try
            If dgvAttendance.Columns.Count > 0 Then
                ' Hide ID
                If dgvAttendance.Columns.Contains("id") Then
                    dgvAttendance.Columns("id").Visible = False
                End If

                ' Format visible columns
                If dgvAttendance.Columns.Contains("date") Then
                    dgvAttendance.Columns("date").HeaderText = "Date"
                    dgvAttendance.Columns("date").Width = 120
                End If

                If dgvAttendance.Columns.Contains("course_code") Then
                    dgvAttendance.Columns("course_code").HeaderText = "Course Code"
                    dgvAttendance.Columns("course_code").Width = 100
                End If

                If dgvAttendance.Columns.Contains("course_name") Then
                    dgvAttendance.Columns("course_name").HeaderText = "Course Name"
                    dgvAttendance.Columns("course_name").Width = 250
                End If

                If dgvAttendance.Columns.Contains("status") Then
                    dgvAttendance.Columns("status").HeaderText = "Status"
                    dgvAttendance.Columns("status").Width = 100
                End If

                If dgvAttendance.Columns.Contains("time_in") Then
                    dgvAttendance.Columns("time_in").HeaderText = "Time In"
                    dgvAttendance.Columns("time_in").Width = 100
                End If

                If dgvAttendance.Columns.Contains("time_out") Then
                    dgvAttendance.Columns("time_out").HeaderText = "Time Out"
                    dgvAttendance.Columns("time_out").Width = 100
                End If

                If dgvAttendance.Columns.Contains("remarks") Then
                    dgvAttendance.Columns("remarks").HeaderText = "Remarks"
                    dgvAttendance.Columns("remarks").Width = 200
                End If

                If dgvAttendance.Columns.Contains("recorded_by") Then
                    dgvAttendance.Columns("recorded_by").HeaderText = "Recorded By"
                    dgvAttendance.Columns("recorded_by").Width = 150
                End If
            End If

            ' Color code status
            ColorCodeAttendanceStatus()

        Catch ex As Exception
            Logger.LogError("Error formatting attendance columns", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Color code attendance status column
    ''' </summary>
    Private Sub ColorCodeAttendanceStatus()
        Try
            If dgvAttendance.Columns.Contains("status") Then
                For Each row As DataGridViewRow In dgvAttendance.Rows
                    If row.Cells("status").Value IsNot Nothing Then
                        Dim status As String = row.Cells("status").Value.ToString().ToLower()
                        Select Case status
                            Case "present"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(46, 139, 87)
                                row.Cells("status").Style.Font = New Font(dgvAttendance.Font, FontStyle.Bold)
                            Case "absent"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(231, 76, 60)
                                row.Cells("status").Style.Font = New Font(dgvAttendance.Font, FontStyle.Bold)
                            Case "late"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(243, 156, 18)
                                row.Cells("status").Style.Font = New Font(dgvAttendance.Font, FontStyle.Bold)
                            Case "excused"
                                row.Cells("status").Style.ForeColor = Color.FromArgb(155, 89, 182)
                                row.Cells("status").Style.Font = New Font(dgvAttendance.Font, FontStyle.Bold)
                        End Select
                    End If
                Next
            End If
        Catch ex As Exception
            Logger.LogError("Error color coding attendance status", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load attendance statistics
    ''' </summary>
    Private Sub LoadAttendanceStatistics(startDate As DateTime, endDate As DateTime)
        Try
            Dim stats As Dictionary(Of String, Integer) = StudentPortalRepository.GetStudentAttendanceStats(studentId, startDate, endDate)

            ' Update stat labels
            lblPresentCount.Text = stats("PresentCount").ToString()
            lblAbsentCount.Text = stats("AbsentCount").ToString()
            lblLateCount.Text = stats("LateCount").ToString()
            lblExcusedCount.Text = stats("ExcusedCount").ToString()

            ' Update attendance rate
            Dim rate As Integer = stats("AttendanceRate")
            lblAttendanceRate.Text = $"{rate}%"

            ' Update progress bar
            pbAttendanceRate.Value = Math.Min(100, Math.Max(0, rate))

            ' Color code rate
            If rate >= 90 Then
                lblAttendanceRate.ForeColor = Color.FromArgb(46, 139, 87)
            ElseIf rate >= 75 Then
                lblAttendanceRate.ForeColor = Color.FromArgb(243, 156, 18)
            Else
                lblAttendanceRate.ForeColor = Color.FromArgb(231, 76, 60)
            End If

            ' Load course-wise attendance
            LoadCourseWiseAttendance()

        Catch ex As Exception
            Logger.LogError("Error loading attendance statistics", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load course-wise attendance breakdown
    ''' </summary>
    Private Sub LoadCourseWiseAttendance()
        Try
            Dim dt As DataTable = StudentPortalRepository.GetStudentAttendanceByCourse(studentId)

            ' Clear existing course cards
            flpCourseAttendance.Controls.Clear()

            If dt.Rows.Count = 0 Then
                Dim lblNoData As New Label With {
                    .Text = "No attendance records found",
                    .Font = New Font("Segoe UI", 10, FontStyle.Italic),
                    .ForeColor = Color.FromArgb(127, 140, 141),
                    .AutoSize = True,
                    .Margin = New Padding(10)
                }
                flpCourseAttendance.Controls.Add(lblNoData)
                Return
            End If

            ' Create card for each course
            For Each row As DataRow In dt.Rows
                Dim card As Guna2Panel = CreateCourseAttendanceCard(row)
                flpCourseAttendance.Controls.Add(card)
            Next

        Catch ex As Exception
            Logger.LogError("Error loading course-wise attendance", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Create course attendance card
    ''' </summary>
    Private Function CreateCourseAttendanceCard(row As DataRow) As Guna2Panel
        Try
            Dim card As New Guna2Panel With {
                .Size = New Size(320, 140),
                .BorderRadius = 10,
                .FillColor = Color.White,
                .BorderColor = Color.FromArgb(46, 139, 87),
                .BorderThickness = 2,
                .Margin = New Padding(10)
            }
            ThemeManager.ApplyShadow(card, ThemeManager.ShadowDepth)

            ' Course info
            Dim lblCourse As New Label With {
                .Text = row("course_name").ToString(),
                .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .ForeColor = Color.FromArgb(44, 62, 80),
                .Location = New Point(15, 15),
                .Size = New Size(290, 25),
                .AutoEllipsis = True
            }

            Dim lblCode As New Label With {
                .Text = row("course_code").ToString(),
                .Font = New Font("Segoe UI", 8, FontStyle.Regular),
                .ForeColor = Color.FromArgb(127, 140, 141),
                .Location = New Point(15, 40),
                .AutoSize = True
            }

            ' Stats
            Dim rate As Double = Convert.ToDouble(row("attendance_rate"))
            Dim lblRate As New Label With {
                .Text = $"Attendance: {rate:F1}%",
                .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                .Location = New Point(15, 70),
                .AutoSize = True
            }

            ' Color code rate
            If rate >= 90 Then
                lblRate.ForeColor = Color.FromArgb(46, 139, 87)
            ElseIf rate >= 75 Then
                lblRate.ForeColor = Color.FromArgb(243, 156, 18)
            Else
                lblRate.ForeColor = Color.FromArgb(231, 76, 60)
            End If

            Dim lblSessions As New Label With {
                .Text = $"Total Sessions: {row("total_sessions")}",
                .Font = New Font("Segoe UI", 8),
                .ForeColor = Color.FromArgb(127, 140, 141),
                .Location = New Point(15, 95),
                .AutoSize = True
            }

            Dim lblBreakdown As New Label With {
                .Text = $"P: {row("present_count")} | A: {row("absent_count")} | L: {row("late_count")}",
                .Font = New Font("Segoe UI", 8),
                .ForeColor = Color.FromArgb(127, 140, 141),
                .Location = New Point(15, 115),
                .AutoSize = True
            }

            card.Controls.Add(lblCourse)
            card.Controls.Add(lblCode)
            card.Controls.Add(lblRate)
            card.Controls.Add(lblSessions)
            card.Controls.Add(lblBreakdown)

            Return card

        Catch ex As Exception
            Logger.LogError("Error creating course attendance card", ex)
            Return New Guna2Panel()
        End Try
    End Function

#End Region

#Region "Courses Section"

    ''' <summary>
    ''' Load enrolled courses
    ''' </summary>
    Private Sub LoadCoursesData()
        Try
            Dim dt As DataTable = StudentPortalRepository.GetStudentCourses(studentId)
            dgvCourses.DataSource = dt

            ' Format columns
            FormatCoursesColumns()

            ' Update count
            lblCoursesCount.Text = $"Enrolled Courses: {dt.Rows.Count}"

        Catch ex As Exception
            Logger.LogError("Error loading courses data", ex)
            MessageBox.Show($"Error loading courses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Format courses grid columns
    ''' </summary>
    Private Sub FormatCoursesColumns()
        Try
            If dgvCourses.Columns.Count > 0 Then
                ' Hide ID
                If dgvCourses.Columns.Contains("id") Then
                    dgvCourses.Columns("id").Visible = False
                End If

                ' Format visible columns
                If dgvCourses.Columns.Contains("course_code") Then
                    dgvCourses.Columns("course_code").HeaderText = "Code"
                    dgvCourses.Columns("course_code").Width = 100
                End If

                If dgvCourses.Columns.Contains("course_name") Then
                    dgvCourses.Columns("course_name").HeaderText = "Course Name"
                    dgvCourses.Columns("course_name").Width = 250
                End If

                If dgvCourses.Columns.Contains("credits") Then
                    dgvCourses.Columns("credits").HeaderText = "Credits"
                    dgvCourses.Columns("credits").Width = 70
                End If

                If dgvCourses.Columns.Contains("schedule") Then
                    dgvCourses.Columns("schedule").HeaderText = "Schedule"
                    dgvCourses.Columns("schedule").Width = 150
                End If

                If dgvCourses.Columns.Contains("room") Then
                    dgvCourses.Columns("room").HeaderText = "Room"
                    dgvCourses.Columns("room").Width = 80
                End If

                If dgvCourses.Columns.Contains("faculty_name") Then
                    dgvCourses.Columns("faculty_name").HeaderText = "Faculty"
                    dgvCourses.Columns("faculty_name").Width = 150
                End If

                If dgvCourses.Columns.Contains("enrollment_date") Then
                    dgvCourses.Columns("enrollment_date").HeaderText = "Enrolled On"
                    dgvCourses.Columns("enrollment_date").Width = 120
                End If

                If dgvCourses.Columns.Contains("grade") Then
                    dgvCourses.Columns("grade").HeaderText = "Grade"
                    dgvCourses.Columns("grade").Width = 70
                End If

                If dgvCourses.Columns.Contains("description") Then
                    dgvCourses.Columns("description").Visible = False
                End If

                If dgvCourses.Columns.Contains("enrollment_status") Then
                    dgvCourses.Columns("enrollment_status").Visible = False
                End If
            End If

        Catch ex As Exception
            Logger.LogError("Error formatting courses columns", ex)
        End Try
    End Sub

#End Region

#Region "Statistics Section"

    ''' <summary>
    ''' Load overall statistics
    ''' </summary>
    Private Sub LoadStatistics()
        Try
            Dim stats As Dictionary(Of String, Object) = StudentPortalRepository.GetStudentProfileStats(studentId)

            ' Update statistics cards
            lblStatEnrolledCourses.Text = stats("EnrolledCourses").ToString()
            lblStatTotalAttendance.Text = stats("TotalAttendance").ToString()
            lblStatOverallRate.Text = $"{stats("AttendanceRate"):F1}%"
            lblStatDaysEnrolled.Text = stats("DaysEnrolled").ToString()

        Catch ex As Exception
            Logger.LogError("Error loading statistics", ex)
        End Try
    End Sub

#End Region

#Region "Event Handlers"

    ''' <summary>
    ''' Apply filter button click
    ''' </summary>
    Private Sub btnApplyFilter_Click(sender As Object, e As EventArgs) Handles btnApplyFilter.Click
        Try
            If dtpStartDate.Value > dtpEndDate.Value Then
                MessageBox.Show("Start date must be before end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            LoadAttendanceData()

        Catch ex As Exception
            Logger.LogError("Error applying filter", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Reset filter button click
    ''' </summary>
    Private Sub btnResetFilter_Click(sender As Object, e As EventArgs) Handles btnResetFilter.Click
        Try
            dtpStartDate.Value = DateTime.Today.AddMonths(-1)
            dtpEndDate.Value = DateTime.Today
            LoadAttendanceData()

        Catch ex As Exception
            Logger.LogError("Error resetting filter", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Refresh all button click
    ''' </summary>
    Private Sub btnRefreshAll_Click(sender As Object, e As EventArgs) Handles btnRefreshAll.Click
        Try
            LoadProfileData()
            LoadAttendanceData()
            LoadCoursesData()
            LoadStatistics()

            MessageBox.Show("All data refreshed successfully!", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Logger.LogError("Error refreshing all data", ex)
            MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Export report button click
    ''' </summary>
    Private Sub btnExportReport_Click(sender As Object, e As EventArgs) Handles btnExportReport.Click
        Try
            Dim startDate As DateTime = dtpStartDate.Value.Date
            Dim endDate As DateTime = dtpEndDate.Value.Date

            Dim reportData As DataTable = StudentPortalRepository.GenerateStudentReport(studentId, startDate, endDate)

            If reportData.Rows.Count = 0 Then
                MessageBox.Show("No attendance records found for the selected date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Filter = "CSV Files (*.csv)|*.csv|HTML Files (*.html)|*.html"
                sfd.FileName = $"MyAttendanceReport_{DateTime.Now:yyyyMMdd}.csv"

                If sfd.ShowDialog() = DialogResult.OK Then
                    If sfd.FileName.EndsWith(".csv") Then
                        Exporter.ExportToCSV(reportData, sfd.FileName)
                    Else
                        Exporter.ExportToHTML(reportData, sfd.FileName, $"Attendance Report - {currentStudent.Name}")
                    End If

                    MessageBox.Show("Report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    If MessageBox.Show("Do you want to open the exported file?", "Open File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})
                    End If

                    Logger.LogInfo($"Student {studentId} exported attendance report")
                End If
            End Using

        Catch ex As Exception
            Logger.LogError("Error exporting report", ex)
            MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Print report button click
    ''' </summary>
    Private Sub btnPrintReport_Click(sender As Object, e As EventArgs) Handles btnPrintReport.Click
        Try
            Dim startDate As DateTime = dtpStartDate.Value.Date
            Dim endDate As DateTime = dtpEndDate.Value.Date

            Dim reportData As DataTable = StudentPortalRepository.GenerateStudentReport(studentId, startDate, endDate)

            If reportData.Rows.Count = 0 Then
                MessageBox.Show("No attendance records found for the selected date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Generate HTML for print
            Dim html As String = GeneratePrintHTML(reportData, startDate, endDate)

            ' Show print preview
            Dim previewForm As New Form With {
                .Text = "Print Preview - My Attendance Report",
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
            MessageBox.Show($"Error printing report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Generate HTML for print preview
    ''' </summary>
    Private Function GeneratePrintHTML(reportData As DataTable, startDate As DateTime, endDate As DateTime) As String
        Try
            Dim html As New System.Text.StringBuilder()

            html.AppendLine("<html>")
            html.AppendLine("<head>")
            html.AppendLine("<style>")
            html.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; margin: 20px; }")
            html.AppendLine("h1 { color: #2E8B57; text-align: center; }")
            html.AppendLine("h2 { color: #2C3E50; }")
            html.AppendLine(".info { background: #ecf0f1; padding: 15px; border-radius: 5px; margin: 20px 0; }")
            html.AppendLine(".info p { margin: 5px 0; }")
            html.AppendLine("table { border-collapse: collapse; width: 100%; margin-top: 20px; }")
            html.AppendLine("th { background-color: #2E8B57; color: white; padding: 10px; text-align: left; }")
            html.AppendLine("td { border: 1px solid #ddd; padding: 8px; }")
            html.AppendLine("tr:nth-child(even) { background-color: #f2f2f2; }")
            html.AppendLine(".present { color: #2E8B57; font-weight: bold; }")
            html.AppendLine(".absent { color: #E74C3C; font-weight: bold; }")
            html.AppendLine(".late { color: #F39C12; font-weight: bold; }")
            html.AppendLine(".excused { color: #9B59B6; font-weight: bold; }")
            html.AppendLine(".footer { margin-top: 30px; text-align: center; font-size: 12px; color: #7f8c8d; }")
            html.AppendLine("</style>")
            html.AppendLine("</head>")
            html.AppendLine("<body>")

            html.AppendLine("<h1>Student Attendance Report</h1>")

            ' Student information
            html.AppendLine("<div class='info'>")
            html.AppendLine($"<p><strong>Student ID:</strong> {currentStudent.StudentId}</p>")
            html.AppendLine($"<p><strong>Name:</strong> {currentStudent.Name}</p>")
            html.AppendLine($"<p><strong>Course/Program:</strong> {currentStudent.Course}</p>")
            html.AppendLine($"<p><strong>Report Period:</strong> {startDate:MMMM dd, yyyy} to {endDate:MMMM dd, yyyy}</p>")
            html.AppendLine($"<p><strong>Generated:</strong> {DateTime.Now:MMMM dd, yyyy hh:mm tt}</p>")
            html.AppendLine("</div>")

            ' Statistics
            Dim stats As Dictionary(Of String, Integer) = StudentPortalRepository.GetStudentAttendanceStats(studentId, startDate, endDate)
            html.AppendLine("<h2>Attendance Summary</h2>")
            html.AppendLine("<div class='info'>")
            html.AppendLine($"<p><strong>Total Records:</strong> {stats("TotalRecords")}</p>")
            html.AppendLine($"<p><strong>Present:</strong> {stats("PresentCount")} | <strong>Absent:</strong> {stats("AbsentCount")} | <strong>Late:</strong> {stats("LateCount")} | <strong>Excused:</strong> {stats("ExcusedCount")}</p>")
            html.AppendLine($"<p><strong>Attendance Rate:</strong> {stats("AttendanceRate")}%</p>")
            html.AppendLine("</div>")

            ' Detailed records
            html.AppendLine("<h2>Detailed Attendance Records</h2>")
            html.AppendLine("<table>")
            html.AppendLine("<tr>")
            html.AppendLine("<th>Date</th>")
            html.AppendLine("<th>Course Code</th>")
            html.AppendLine("<th>Course Name</th>")
            html.AppendLine("<th>Status</th>")
            html.AppendLine("<th>Time In</th>")
            html.AppendLine("<th>Time Out</th>")
            html.AppendLine("<th>Faculty</th>")
            html.AppendLine("</tr>")

            For Each row As DataRow In reportData.Rows
                html.AppendLine("<tr>")
                html.AppendLine($"<td>{Convert.ToDateTime(row("date")):MM/dd/yyyy}</td>")
                html.AppendLine($"<td>{row("course_code")}</td>")
                html.AppendLine($"<td>{row("course_name")}</td>")

                Dim status As String = row("status").ToString()
                Dim statusClass As String = status.ToLower()
                html.AppendLine($"<td class='{statusClass}'>{status}</td>")

                html.AppendLine($"<td>{If(IsDBNull(row("time_in")), "", row("time_in"))}</td>")
                html.AppendLine($"<td>{If(IsDBNull(row("time_out")), "", row("time_out"))}</td>")
                html.AppendLine($"<td>{If(IsDBNull(row("faculty_name")), "", row("faculty_name"))}</td>")
                html.AppendLine("</tr>")
            Next

            html.AppendLine("</table>")

            html.AppendLine("<div class='footer'>")
            html.AppendLine("<p>This is an official student attendance report</p>")
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
    ''' View course details button click (in courses grid)
    ''' </summary>
    Private Sub dgvCourses_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCourses.CellDoubleClick
        Try
            If e.RowIndex < 0 Then Return

            Dim courseId As Integer = Convert.ToInt32(dgvCourses.Rows(e.RowIndex).Cells("id").Value)
            ShowCourseDetails(courseId)

        Catch ex As Exception
            Logger.LogError("Error viewing course details", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Show course details dialog
    ''' </summary>
    Private Sub ShowCourseDetails(courseId As Integer)
        Try
            Dim details As Dictionary(Of String, Object) = StudentPortalRepository.GetCourseDetailsWithAttendance(studentId, courseId)

            If details.Count = 0 Then
                MessageBox.Show("Course details not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim message As String = $"Course Code: {details("CourseCode")}" & vbCrLf &
                                   $"Course Name: {details("CourseName")}" & vbCrLf &
                                   $"Credits: {details("Credits")}" & vbCrLf &
                                   $"Schedule: {details("Schedule")}" & vbCrLf &
                                   $"Room: {details("Room")}" & vbCrLf &
                                   $"Faculty: {details("FacultyName")}" & vbCrLf &
                                   vbCrLf &
                                   "=== Attendance Summary ===" & vbCrLf &
                                   $"Total Sessions: {details("TotalSessions")}" & vbCrLf &
                                   $"Present: {details("Present")}" & vbCrLf &
                                   $"Absent: {details("Absent")}" & vbCrLf &
                                   $"Late: {details("Late")}" & vbCrLf &
                                   $"Excused: {details("Excused")}" & vbCrLf &
                                   $"Attendance Rate: {details("AttendanceRate"):F1}%" & vbCrLf &
                                   vbCrLf &
                                   If(Not String.IsNullOrWhiteSpace(details("Description").ToString()),
                                      $"Description: {details("Description")}", "")

            MessageBox.Show(message, "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Logger.LogError("Error showing course details", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Tab control selected index changed
    ''' </summary>
    Private Sub tabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabControl.SelectedIndexChanged
        Try
            ' Refresh data when switching tabs
            Select Case tabControl.SelectedIndex
                Case 0 ' Profile
                    LoadProfileData()
                Case 1 ' Attendance
                    LoadAttendanceData()
                Case 2 ' Courses
                    LoadCoursesData()
                Case 3 ' Reports
                    ' Reports tab uses existing data
            End Select

        Catch ex As Exception
            Logger.LogError("Error handling tab change", ex)
        End Try
    End Sub

#End Region

#Region "Form Events"

    ''' <summary>
    ''' Handle form resize
    ''' </summary>
    Private Sub StudentPortalForm_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            If Me.Width > 0 AndAlso Me.Height > 0 Then
                pnlMain.Refresh()
            End If
        Catch ex As Exception
            ' Silently handle resize errors
        End Try
    End Sub

    ''' <summary>
    ''' Form closing event
    ''' </summary>
    Private Sub StudentPortalForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Logger.LogInfo($"Student portal closed for {studentId}")
        Catch ex As Exception
            Logger.LogError("Error during form closing", ex)
        End Try
    End Sub



#End Region

End Class