' ==========================================
' FILENAME: /Data/StudentPortalRepository.vb
' PURPOSE: Data access layer for student portal - student-specific queries
' AUTHOR: System
' DATE: 2025-10-20
' ==========================================

Imports System.Data

Public Class StudentPortalRepository

#Region "Student Profile"

    ''' <summary>
    ''' Gets student profile by student ID
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <returns>Student object</returns>
    Public Shared Function GetStudentProfile(studentId As String) As Student
        Try
            Dim query As String = "
                SELECT 
                    id,
                    student_id,
                    name,
                    course,
                    email,
                    phone_number,
                    date_of_birth,
                    enrollment_date,
                    status,
                    created_at
                FROM students
                WHERE student_id = @student_id
                AND status IN ('Active', 'Inactive')"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Return New Student With {
                    .Id = Convert.ToInt32(row("id")),
                    .studentId = row("student_id").ToString(),
                    .Name = row("name").ToString(),
                    .Course = row("course").ToString(),
                    .Email = If(IsDBNull(row("email")), "", row("email").ToString()),
                    .PhoneNumber = If(IsDBNull(row("phone_number")), "", row("phone_number").ToString()),
                    .DateOfBirth = If(IsDBNull(row("date_of_birth")), Nothing, Convert.ToDateTime(row("date_of_birth"))),
                    .EnrollmentDate = If(IsDBNull(row("enrollment_date")), Nothing, Convert.ToDateTime(row("enrollment_date"))),
                    .Status = row("status").ToString(),
                    .CreatedAt = Convert.ToDateTime(row("created_at"))
                }
            End If

            Return Nothing

        Catch ex As Exception
            Logger.LogError("Error getting student profile", ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets student profile statistics
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <returns>Dictionary with profile stats</returns>
    Public Shared Function GetStudentProfileStats(studentId As String) As Dictionary(Of String, Object)
        Try
            Dim stats As New Dictionary(Of String, Object)

            ' Get enrolled courses count
            Dim coursesQuery As String = "SELECT COUNT(*) FROM enrollments WHERE student_id = @student_id AND status = 'Enrolled'"
            Dim coursesParams As New Dictionary(Of String, Object) From {{"@student_id", studentId}}
            stats("EnrolledCourses") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(coursesQuery, coursesParams))

            ' Get total attendance records
            Dim attendanceQuery As String = "SELECT COUNT(*) FROM attendance WHERE student_id = @student_id"
            Dim attendanceParams As New Dictionary(Of String, Object) From {{"@student_id", studentId}}
            stats("TotalAttendance") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(attendanceQuery, attendanceParams))

            ' Get overall attendance rate
            Dim rateQuery As String = "
                SELECT 
                    ROUND((SUM(CASE WHEN status IN ('Present', 'Late', 'Excused') THEN 1 ELSE 0 END) / COUNT(*)) * 100, 2) as rate
                FROM attendance 
                WHERE student_id = @student_id"
            Dim rateParams As New Dictionary(Of String, Object) From {{"@student_id", studentId}}
            Dim rateResult As Object = DatabaseHandler.ExecuteScalar(rateQuery, rateParams)
            stats("AttendanceRate") = If(IsDBNull(rateResult), 0.0, Convert.ToDouble(rateResult))

            ' Get days enrolled
            Dim student As Student = GetStudentProfile(studentId)
            If student IsNot Nothing AndAlso student.EnrollmentDate.HasValue Then
                Dim daysEnrolled As Integer = (DateTime.Today - student.EnrollmentDate.Value).Days
                stats("DaysEnrolled") = daysEnrolled
            Else
                stats("DaysEnrolled") = 0
            End If

            Return stats

        Catch ex As Exception
            Logger.LogError("Error getting student profile stats", ex)
            Return New Dictionary(Of String, Object)()
        End Try
    End Function

#End Region

#Region "Student Attendance"

    ''' <summary>
    ''' Gets student attendance records with date range filter
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="startDate">Start date (optional)</param>
    ''' <param name="endDate">End date (optional)</param>
    ''' <returns>DataTable with attendance records</returns>
    Public Shared Function GetStudentAttendance(studentId As String, Optional startDate As DateTime? = Nothing, Optional endDate As DateTime? = Nothing) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    a.id,
                    a.date,
                    c.course_code,
                    c.course_name,
                    a.status,
                    TIME_FORMAT(a.time_in, '%h:%i %p') as time_in,
                    TIME_FORMAT(a.time_out, '%h:%i %p') as time_out,
                    a.remarks,
                    a.recorded_by
                FROM attendance a
                INNER JOIN courses c ON a.course_id = c.id
                WHERE a.student_id = @student_id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            If startDate.HasValue Then
                query &= " AND a.date >= @start_date"
                parameters.Add("@start_date", startDate.Value.ToString("yyyy-MM-dd"))
            End If

            If endDate.HasValue Then
                query &= " AND a.date <= @end_date"
                parameters.Add("@end_date", endDate.Value.ToString("yyyy-MM-dd"))
            End If

            query &= " ORDER BY a.date DESC, a.time_in DESC"

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting student attendance", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets student attendance statistics for date range
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="startDate">Start date (optional)</param>
    ''' <param name="endDate">End date (optional)</param>
    ''' <returns>Dictionary with statistics</returns>
    Public Shared Function GetStudentAttendanceStats(studentId As String, Optional startDate As DateTime? = Nothing, Optional endDate As DateTime? = Nothing) As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            Dim baseQuery As String = "SELECT COUNT(*) FROM attendance WHERE student_id = @student_id"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            Dim dateFilter As String = ""
            If startDate.HasValue Then
                dateFilter &= " AND date >= @start_date"
                parameters.Add("@start_date", startDate.Value.ToString("yyyy-MM-dd"))
            End If
            If endDate.HasValue Then
                dateFilter &= " AND date <= @end_date"
                parameters.Add("@end_date", endDate.Value.ToString("yyyy-MM-dd"))
            End If

            ' Total records
            stats("TotalRecords") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(baseQuery & dateFilter, parameters))

            ' Present count
            Dim presentQuery As String = baseQuery & dateFilter & " AND status = 'Present'"
            stats("PresentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(presentQuery, parameters))

            ' Absent count
            Dim absentQuery As String = baseQuery & dateFilter & " AND status = 'Absent'"
            stats("AbsentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(absentQuery, parameters))

            ' Late count
            Dim lateQuery As String = baseQuery & dateFilter & " AND status = 'Late'"
            stats("LateCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(lateQuery, parameters))

            ' Excused count
            Dim excusedQuery As String = baseQuery & dateFilter & " AND status = 'Excused'"
            stats("ExcusedCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(excusedQuery, parameters))

            ' Calculate attendance rate
            If stats("TotalRecords") > 0 Then
                Dim attendedCount As Integer = stats("PresentCount") + stats("LateCount") + stats("ExcusedCount")
                stats("AttendanceRate") = CInt((attendedCount / stats("TotalRecords")) * 100)
            Else
                stats("AttendanceRate") = 0
            End If

            Return stats

        Catch ex As Exception
            Logger.LogError("Error getting student attendance stats", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

    ''' <summary>
    ''' Gets student attendance by course
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <returns>DataTable with course-wise attendance</returns>
    Public Shared Function GetStudentAttendanceByCourse(studentId As String) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    c.course_code,
                    c.course_name,
                    COUNT(a.id) as total_sessions,
                    SUM(CASE WHEN a.status = 'Present' THEN 1 ELSE 0 END) as present_count,
                    SUM(CASE WHEN a.status = 'Absent' THEN 1 ELSE 0 END) as absent_count,
                    SUM(CASE WHEN a.status = 'Late' THEN 1 ELSE 0 END) as late_count,
                    SUM(CASE WHEN a.status = 'Excused' THEN 1 ELSE 0 END) as excused_count,
                    ROUND((SUM(CASE WHEN a.status IN ('Present', 'Late', 'Excused') THEN 1 ELSE 0 END) / COUNT(a.id)) * 100, 2) as attendance_rate
                FROM courses c
                INNER JOIN attendance a ON c.id = a.course_id
                WHERE a.student_id = @student_id
                GROUP BY c.id, c.course_code, c.course_name
                ORDER BY c.course_name"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting student attendance by course", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets student attendance calendar data for a month
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="year">Year</param>
    ''' <param name="month">Month</param>
    ''' <returns>Dictionary with date and status</returns>
    Public Shared Function GetStudentAttendanceCalendar(studentId As String, year As Integer, month As Integer) As Dictionary(Of Date, String)
        Try
            Dim calendar As New Dictionary(Of Date, String)

            Dim query As String = "
                SELECT 
                    date,
                    status
                FROM attendance
                WHERE student_id = @student_id
                AND YEAR(date) = @year
                AND MONTH(date) = @month
                ORDER BY date"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@year", year},
                {"@month", month}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            For Each row As DataRow In dt.Rows
                Dim attendanceDate As Date = Convert.ToDateTime(row("date")).Date
                Dim status As String = row("status").ToString()

                If Not calendar.ContainsKey(attendanceDate) Then
                    calendar.Add(attendanceDate, status)
                End If
            Next

            Return calendar

        Catch ex As Exception
            Logger.LogError("Error getting student attendance calendar", ex)
            Return New Dictionary(Of Date, String)()
        End Try
    End Function

#End Region

#Region "Student Courses"

    ''' <summary>
    ''' Gets student's enrolled courses
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <returns>DataTable with enrolled courses</returns>
    Public Shared Function GetStudentCourses(studentId As String) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    c.id,
                    c.course_code,
                    c.course_name,
                    c.description,
                    c.credits,
                    c.schedule,
                    c.room,
                    COALESCE(f.name, 'Not Assigned') as faculty_name,
                    e.enrollment_date,
                    e.status as enrollment_status,
                    e.grade
                FROM courses c
                INNER JOIN enrollments e ON c.id = e.course_id
                LEFT JOIN faculty f ON c.faculty_id = f.id
                WHERE e.student_id = @student_id
                AND e.status = 'Enrolled'
                AND c.status = 'Active'
                ORDER BY c.course_name"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting student courses", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets course details with attendance summary
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="courseId">Course ID</param>
    ''' <returns>Dictionary with course details</returns>
    Public Shared Function GetCourseDetailsWithAttendance(studentId As String, courseId As Integer) As Dictionary(Of String, Object)
        Try
            Dim details As New Dictionary(Of String, Object)

            ' Get course information
            Dim courseQuery As String = "
                SELECT 
                    c.course_code,
                    c.course_name,
                    c.description,
                    c.credits,
                    c.schedule,
                    c.room,
                    COALESCE(f.name, 'Not Assigned') as faculty_name,
                    COALESCE(f.email, '') as faculty_email
                FROM courses c
                LEFT JOIN faculty f ON c.faculty_id = f.id
                WHERE c.id = @course_id"

            Dim courseParams As New Dictionary(Of String, Object) From {{"@course_id", courseId}}
            Dim courseDt As DataTable = DatabaseHandler.ExecuteReader(courseQuery, courseParams)

            If courseDt.Rows.Count > 0 Then
                Dim row As DataRow = courseDt.Rows(0)
                details("CourseCode") = row("course_code").ToString()
                details("CourseName") = row("course_name").ToString()
                details("Description") = If(IsDBNull(row("description")), "", row("description").ToString())
                details("Credits") = Convert.ToInt32(row("credits"))
                details("Schedule") = If(IsDBNull(row("schedule")), "TBA", row("schedule").ToString())
                details("Room") = If(IsDBNull(row("room")), "TBA", row("room").ToString())
                details("FacultyName") = row("faculty_name").ToString()
                details("FacultyEmail") = row("faculty_email").ToString()
            End If

            ' Get attendance stats for this course
            Dim statsQuery As String = "
                SELECT 
                    COUNT(*) as total_sessions,
                    SUM(CASE WHEN status = 'Present' THEN 1 ELSE 0 END) as present,
                    SUM(CASE WHEN status = 'Absent' THEN 1 ELSE 0 END) as absent,
                    SUM(CASE WHEN status = 'Late' THEN 1 ELSE 0 END) as late,
                    SUM(CASE WHEN status = 'Excused' THEN 1 ELSE 0 END) as excused
                FROM attendance
                WHERE student_id = @student_id AND course_id = @course_id"

            Dim statsParams As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@course_id", courseId}
            }
            Dim statsDt As DataTable = DatabaseHandler.ExecuteReader(statsQuery, statsParams)

            If statsDt.Rows.Count > 0 Then
                Dim row As DataRow = statsDt.Rows(0)
                details("TotalSessions") = Convert.ToInt32(row("total_sessions"))
                details("Present") = Convert.ToInt32(row("present"))
                details("Absent") = Convert.ToInt32(row("absent"))
                details("Late") = Convert.ToInt32(row("late"))
                details("Excused") = Convert.ToInt32(row("excused"))

                Dim total As Integer = Convert.ToInt32(row("total_sessions"))
                If total > 0 Then
                    Dim attended As Integer = Convert.ToInt32(row("present")) + Convert.ToInt32(row("late")) + Convert.ToInt32(row("excused"))
                    details("AttendanceRate") = CDbl((attended / total) * 100)
                Else
                    details("AttendanceRate") = 0.0
                End If
            End If

            Return details

        Catch ex As Exception
            Logger.LogError("Error getting course details with attendance", ex)
            Return New Dictionary(Of String, Object)()
        End Try
    End Function

#End Region

#Region "Student Reports"

    ''' <summary>
    ''' Generates comprehensive student report
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="startDate">Start date</param>
    ''' <param name="endDate">End date</param>
    ''' <returns>DataTable with report data</returns>
    Public Shared Function GenerateStudentReport(studentId As String, startDate As DateTime, endDate As DateTime) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    a.date,
                    c.course_code,
                    c.course_name,
                    a.status,
                    TIME_FORMAT(a.time_in, '%h:%i %p') as time_in,
                    TIME_FORMAT(a.time_out, '%h:%i %p') as time_out,
                    a.remarks,
                    f.name as faculty_name
                FROM attendance a
                INNER JOIN courses c ON a.course_id = c.id
                LEFT JOIN faculty f ON c.faculty_id = f.id
                WHERE a.student_id = @student_id
                AND a.date BETWEEN @start_date AND @end_date
                ORDER BY a.date DESC, c.course_name"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@start_date", startDate.ToString("yyyy-MM-dd")},
                {"@end_date", endDate.ToString("yyyy-MM-dd")}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error generating student report", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets student attendance trend by month
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="monthsBack">Number of months to look back</param>
    ''' <returns>DataTable with monthly trends</returns>
    Public Shared Function GetStudentAttendanceTrend(studentId As String, Optional monthsBack As Integer = 6) As DataTable
        Try
            Dim query As String = $"
                SELECT 
                    DATE_FORMAT(date, '%Y-%m') as month,
                    DATE_FORMAT(date, '%M %Y') as month_name,
                    COUNT(*) as total_sessions,
                    SUM(CASE WHEN status = 'Present' THEN 1 ELSE 0 END) as present_count,
                    SUM(CASE WHEN status = 'Absent' THEN 1 ELSE 0 END) as absent_count,
                    ROUND((SUM(CASE WHEN status IN ('Present', 'Late', 'Excused') THEN 1 ELSE 0 END) / COUNT(*)) * 100, 2) as attendance_rate
                FROM attendance
                WHERE student_id = @student_id
                AND date >= DATE_SUB(CURDATE(), INTERVAL {monthsBack} MONTH)
                GROUP BY DATE_FORMAT(date, '%Y-%m'), DATE_FORMAT(date, '%M %Y')
                ORDER BY month DESC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting student attendance trend", ex)
            Return New DataTable()
        End Try
    End Function

#End Region

#Region "Validation & Security"

    ''' <summary>
    ''' Validates if student ID exists and is active
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <returns>True if valid and active</returns>
    Public Shared Function ValidateStudentAccess(studentId As String) As Boolean
        Try
            ' 1) Check students table for matching active/inactive profile
            Dim query As String = "SELECT COUNT(*) FROM students WHERE student_id = @student_id AND status IN ('Active','Inactive')"
            Dim parameters As New Dictionary(Of String, Object) From {{"@student_id", studentId}}
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            If count > 0 Then
                Return True
            End If


            Return False
        Catch ex As Exception
            Logger.LogError("Error validating student access", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if student ID matches user account
    ''' </summary>
    ''' <param name="username">Username from login</param>
    ''' <param name="studentId">Student ID to verify</param>
    ''' <returns>True if match</returns>
    Public Shared Function VerifyStudentIdentity(username As String, studentId As String) As Boolean
        Try
            ' In a real system, you'd have a mapping table between users and students
            ' For now, we assume username equals student_id for student accounts
            Return username.Equals(studentId, StringComparison.OrdinalIgnoreCase)

        Catch ex As Exception
            Logger.LogError("Error verifying student identity", ex)
            Return False
        End Try
    End Function

#End Region

End Class