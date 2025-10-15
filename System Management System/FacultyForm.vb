' ==========================================
' FILENAME: /Forms/FacultyForm.vb
' PURPOSE: Faculty dashboard showing assigned courses and student records
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Imports System.Data
Imports Guna.UI2.WinForms

Public Class FacultyForm
    Private currentUser As User
    Private coursesData As DataTable

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
    Private Sub FacultyForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Load faculty courses
            LoadFacultyCourses()

            ' Apply fade-in animation
            ThemeManager.SlideIn(pnlMain, "LEFT", 300)

            Logger.LogInfo("Faculty form loaded")
        Catch ex As Exception
            Logger.LogError("Error loading faculty form", ex)
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Load faculty courses and display as cards
    ''' </summary>
    Private Sub LoadFacultyCourses()
        Try
            ' Clear existing cards
            flpCourses.Controls.Clear()

            ' Get faculty courses
            coursesData = FacultyRepository.GetFacultyCourses(currentUser.Id)

            If coursesData.Rows.Count > 0 Then
                ' Update header label
                lblCoursesCount.Text = $"My Courses ({coursesData.Rows.Count})"

                ' Create card for each course
                For Each row As DataRow In coursesData.Rows
                    Dim courseCard As Guna2GroupBox = CreateCourseCard(row)
                    flpCourses.Controls.Add(courseCard)
                Next
            Else
                ' No courses assigned
                lblCoursesCount.Text = "My Courses (0)"

                Dim noCoursesLabel As New Label With {
                    .Text = "No courses assigned yet.",
                    .Font = ThemeManager.HeaderFont,
                    .ForeColor = ThemeManager.SecondaryTextColor,
                    .Size = New Size(400, 50),
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Margin = New Padding(50, 50, 0, 0)
                }

                flpCourses.Controls.Add(noCoursesLabel)
            End If

        Catch ex As Exception
            Logger.LogError("Error loading faculty courses", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Creates a course card with information and action buttons
    ''' </summary>
    Private Function CreateCourseCard(courseRow As DataRow) As Guna2GroupBox
        Try
            Dim courseId As Integer = Convert.ToInt32(courseRow("id"))
            Dim courseCode As String = courseRow("course_code").ToString()
            Dim courseName As String = courseRow("course_name").ToString()
            Dim schedule As String = If(IsDBNull(courseRow("schedule")), "TBA", courseRow("schedule").ToString())
            Dim room As String = If(IsDBNull(courseRow("room")), "TBA", courseRow("room").ToString())
            Dim enrolledStudents As Integer = Convert.ToInt32(courseRow("enrolled_students"))

            ' Get attendance statistics
            Dim stats As Dictionary(Of String, Integer) = FacultyRepository.GetCourseAttendanceStats(courseId)

            ' Create card
            Dim card As New Guna2GroupBox With {
                .Size = New Size(350, 280),
                .BorderRadius = ThemeManager.BorderRadiusLarge,
                .FillColor = ThemeManager.WhiteColor,
                .BorderColor = ThemeManager.AccentColor,
                .CustomBorderColor = ThemeManager.AccentColor,
                .BorderThickness = 2,
                .Font = ThemeManager.HeaderFont,
                .ForeColor = ThemeManager.TextColor,
                .Text = courseCode,
                .Margin = New Padding(10),
                .Tag = courseId
            }

            ThemeManager.ApplyShadow(card, ThemeManager.ShadowDepth)

            ' Course name label
            Dim lblCourseName As New Label With {
                .Text = courseName,
                .Font = New Font("Segoe UI", 11, FontStyle.Bold),
                .ForeColor = ThemeManager.PrimaryColor,
                .Location = New Point(15, 40),
                .Size = New Size(320, 50),
                .AutoSize = False
            }

            ' Schedule label
            Dim lblSchedule As New Label With {
                .Text = $"📅 {schedule}",
                .Font = ThemeManager.DefaultFont,
                .ForeColor = ThemeManager.TextColor,
                .Location = New Point(15, 95),
                .Size = New Size(320, 20),
                .AutoSize = False
            }

            ' Room label
            Dim lblRoom As New Label With {
                .Text = $"🚪 Room: {room}",
                .Font = ThemeManager.DefaultFont,
                .ForeColor = ThemeManager.TextColor,
                .Location = New Point(15, 120),
                .Size = New Size(320, 20),
                .AutoSize = False
            }

            ' Students label
            Dim lblStudents As New Label With {
                .Text = $"👥 Students: {enrolledStudents}",
                .Font = ThemeManager.DefaultFont,
                .ForeColor = ThemeManager.TextColor,
                .Location = New Point(15, 145),
                .Size = New Size(150, 20),
                .AutoSize = False
            }

            ' Attendance rate label
            Dim lblAttendanceRate As New Label With {
                .Text = $"📊 Attendance: {stats("AttendanceRate")}%",
                .Font = ThemeManager.DefaultFont,
                .ForeColor = ThemeManager.SuccessColor,
                .Location = New Point(170, 145),
                .Size = New Size(165, 20),
                .AutoSize = False
            }

            ' Take Attendance button
            Dim btnTakeAttendance As Guna2Button = ThemeManager.CreatePrimaryButton("Take Attendance", 150, 35)
            btnTakeAttendance.Location = New Point(15, 180)
            AddHandler btnTakeAttendance.Click, Sub(s, e) OpenAttendanceForm(courseId, courseName)

            ' View Records button
            Dim btnViewRecords As Guna2Button = ThemeManager.CreateButton("View Records", ThemeManager.InfoColor, 150, 35)
            btnViewRecords.Location = New Point(175, 180)
            AddHandler btnViewRecords.Click, Sub(s, e) ViewCourseRecords(courseId, courseName)

            ' View Students button
            Dim btnViewStudents As Guna2Button = ThemeManager.CreateButton("View Students", ThemeManager.AccentColor, 150, 35)
            btnViewStudents.Location = New Point(15, 225)
            AddHandler btnViewStudents.Click, Sub(s, e) ViewEnrolledStudents(courseId, courseName)

            ' Course Details button
            Dim btnDetails As New Guna2Button With {
                .Size = New Size(150, 35),
                .Location = New Point(175, 225),
                .BorderRadius = ThemeManager.BorderRadiusMedium,
                .FillColor = ThemeManager.SecondaryTextColor,
                .Font = ThemeManager.DefaultFont,
                .ForeColor = ThemeManager.WhiteColor,
                .Text = "Details"
            }
            btnDetails.HoverState.FillColor = ThemeManager.DarkenColor(ThemeManager.SecondaryTextColor, 15)
            AddHandler btnDetails.Click, Sub(s, e) ShowCourseDetails(courseId)

            ' Add controls to card
            card.Controls.Add(lblCourseName)
            card.Controls.Add(lblSchedule)
            card.Controls.Add(lblRoom)
            card.Controls.Add(lblStudents)
            card.Controls.Add(lblAttendanceRate)
            card.Controls.Add(btnTakeAttendance)
            card.Controls.Add(btnViewRecords)
            card.Controls.Add(btnViewStudents)
            card.Controls.Add(btnDetails)

            Return card

        Catch ex As Exception
            Logger.LogError("Error creating course card", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Opens attendance form for specific course
    ''' </summary>
    Private Sub OpenAttendanceForm(courseId As Integer, courseName As String)
        Try
            Logger.LogInfo($"Opening attendance form for course: {courseName}")

            ' Create attendance form with course filter
            Dim attendanceForm As New AttendanceForm(currentUser)

            ' Load form in parent container if exists
            If Me.Parent IsNot Nothing Then
                Me.Parent.Controls.Clear()
                attendanceForm.TopLevel = False
                attendanceForm.FormBorderStyle = FormBorderStyle.None
                attendanceForm.Dock = DockStyle.Fill
                Me.Parent.Controls.Add(attendanceForm)
                attendanceForm.Show()
            Else
                ' Show as dialog if no parent
                attendanceForm.ShowDialog()
            End If

        Catch ex As Exception
            Logger.LogError("Error opening attendance form", ex)
            MessageBox.Show($"Error opening attendance form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Views course attendance records
    ''' </summary>
    Private Sub ViewCourseRecords(courseId As Integer, courseName As String)
        Try
            Dim recordsForm As New Form With {
                .Text = $"Attendance Records - {courseName}",
                .Size = New Size(900, 600),
                .StartPosition = FormStartPosition.CenterParent,
                .FormBorderStyle = FormBorderStyle.Sizable
            }

            Dim dgv As New Guna2DataGridView With {
                .Dock = DockStyle.Fill
            }

            ' Load attendance records
            Dim dt As DataTable = FacultyRepository.GetCourseAttendance(courseId)
            dgv.DataSource = dt

            ' Style the grid
            ThemeManager.StyleDataGridView(dgv)

            ' Format columns
            If dgv.Columns.Count > 0 Then
                dgv.Columns("id").Visible = False
                dgv.Columns("student_id").HeaderText = "Student ID"
                dgv.Columns("student_name").HeaderText = "Student Name"
                dgv.Columns("date").HeaderText = "Date"
                dgv.Columns("status").HeaderText = "Status"
                dgv.Columns("time_in").HeaderText = "Time In"
                dgv.Columns("time_out").HeaderText = "Time Out"
                dgv.Columns("remarks").HeaderText = "Remarks"
                dgv.Columns("recorded_by").HeaderText = "Recorded By"
            End If

            recordsForm.Controls.Add(dgv)
            recordsForm.ShowDialog()

        Catch ex As Exception
            Logger.LogError("Error viewing course records", ex)
            MessageBox.Show($"Error loading records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Views enrolled students in a course
    ''' </summary>
    Private Sub ViewEnrolledStudents(courseId As Integer, courseName As String)
        Try
            Dim studentsForm As New Form With {
                .Text = $"Enrolled Students - {courseName}",
                .Size = New Size(1000, 600),
                .StartPosition = FormStartPosition.CenterParent,
                .FormBorderStyle = FormBorderStyle.Sizable
            }

            Dim dgv As New Guna2DataGridView With {
                .Dock = DockStyle.Fill
            }

            ' Load student attendance summary
            Dim dt As DataTable = FacultyRepository.GetStudentAttendanceSummary(courseId)
            dgv.DataSource = dt

            ' Style the grid
            ThemeManager.StyleDataGridView(dgv)

            ' Format columns
            If dgv.Columns.Count > 0 Then
                dgv.Columns("student_id").HeaderText = "Student ID"
                dgv.Columns("name").HeaderText = "Name"
                dgv.Columns("total_sessions").HeaderText = "Total Sessions"
                dgv.Columns("present_count").HeaderText = "Present"
                dgv.Columns("absent_count").HeaderText = "Absent"
                dgv.Columns("late_count").HeaderText = "Late"
                dgv.Columns("excused_count").HeaderText = "Excused"
                dgv.Columns("attendance_rate").HeaderText = "Attendance Rate (%)"
            End If

            studentsForm.Controls.Add(dgv)
            studentsForm.ShowDialog()

        Catch ex As Exception
            Logger.LogError("Error viewing enrolled students", ex)
            MessageBox.Show($"Error loading students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Shows detailed course information
    ''' </summary>
    Private Sub ShowCourseDetails(courseId As Integer)
        Try
            Dim course As Course = FacultyRepository.GetCourseById(courseId)

            If course IsNot Nothing Then
                Dim details As String = $"Course Code: {course.CourseCode}" & vbCrLf &
                                       $"Course Name: {course.CourseName}" & vbCrLf &
                                       $"Credits: {course.Credits}" & vbCrLf &
                                       $"Schedule: {course.Schedule}" & vbCrLf &
                                       $"Room: {course.Room}" & vbCrLf &
                                       $"Description: {course.Description}"

                MessageBox.Show(details, "Course Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            Logger.LogError("Error showing course details", ex)
            MessageBox.Show($"Error loading course details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Refresh button click
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadFacultyCourses()
        MessageBox.Show("Courses refreshed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class