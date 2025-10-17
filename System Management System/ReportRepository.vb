' ==========================================
' FILENAME: /Data/ReportRepository.vb
' PURPOSE: Data access layer for reports and analytics
' AUTHOR: System
' DATE: 2025-10-17
' ==========================================

Imports System.Data

Public Class ReportRepository

#Region "Student Reports"

    ''' <summary>
    ''' Gets student attendance report for a date range
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="startDate">Start date</param>
    ''' <param name="endDate">End date</param>
    ''' <returns>DataTable with attendance records</returns>
    Public Shared Function GetStudentAttendanceReport(studentId As String, startDate As DateTime, endDate As DateTime) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    a.id,
                    a.date,
                    c.course_name,
                    a.status,
                    TIME_FORMAT(a.time_in, '%H:%i') as time_in,
                    TIME_FORMAT(a.time_out, '%H:%i') as time_out,
                    a.remarks,
                    a.recorded_by
                FROM attendance a
                INNER JOIN courses c ON a.course_id = c.id
                WHERE a.student_id = @student_id 
                AND a.date BETWEEN @start_date AND @end_date
                ORDER BY a.date DESC, a.time_in DESC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@start_date", startDate.ToString("yyyy-MM-dd")},
                {"@end_date", endDate.ToString("yyyy-MM-dd")}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting student attendance report", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets student attendance statistics
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="startDate">Start date</param>
    ''' <param name="endDate">End date</param>
    ''' <returns>Dictionary with statistics</returns>
    Public Shared Function GetStudentAttendanceStats(studentId As String, startDate As DateTime, endDate As DateTime) As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            Dim baseQuery As String = "SELECT COUNT(*) FROM attendance WHERE student_id = @student_id AND date BETWEEN @start_date AND @end_date"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@start_date", startDate.ToString("yyyy-MM-dd")},
                {"@end_date", endDate.ToString("yyyy-MM-dd")}
            }

            ' Get total records
            stats("TotalRecords") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(baseQuery, parameters))

            ' Get present count
            Dim presentQuery As String = baseQuery & " AND status = 'Present'"
            stats("PresentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(presentQuery, parameters))

            ' Get absent count
            Dim absentQuery As String = baseQuery & " AND status = 'Absent'"
            stats("AbsentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(absentQuery, parameters))

            ' Get late count
            Dim lateQuery As String = baseQuery & " AND status = 'Late'"
            stats("LateCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(lateQuery, parameters))

            ' Get excused count
            Dim excusedQuery As String = baseQuery & " AND status = 'Excused'"
            stats("ExcusedCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(excusedQuery, parameters))

            Return stats

        Catch ex As Exception
            Logger.LogError("Error getting student attendance statistics", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

#End Region

#Region "Course Reports"

    ''' <summary>
    ''' Gets course attendance report for a date range
    ''' </summary>
    ''' <param name="courseId">Course ID</param>
    ''' <param name="startDate">Start date</param>
    ''' <param name="endDate">End date</param>
    ''' <returns>DataTable with attendance records</returns>
    Public Shared Function GetCourseAttendanceReport(courseId As Integer, startDate As DateTime, endDate As DateTime) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    a.id,
                    a.date,
                    a.student_id,
                    s.name as student_name,
                    a.status,
                    TIME_FORMAT(a.time_in, '%H:%i') as time_in,
                    TIME_FORMAT(a.time_out, '%H:%i') as time_out,
                    a.remarks,
                    a.recorded_by
                FROM attendance a
                INNER JOIN students s ON a.student_id = s.student_id
                WHERE a.course_id = @course_id 
                AND a.date BETWEEN @start_date AND @end_date
                ORDER BY a.date DESC, a.time_in DESC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course_id", courseId},
                {"@start_date", startDate.ToString("yyyy-MM-dd")},
                {"@end_date", endDate.ToString("yyyy-MM-dd")}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting course attendance report", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets course attendance statistics
    ''' </summary>
    ''' <param name="courseId">Course ID</param>
    ''' <param name="startDate">Start date</param>
    ''' <param name="endDate">End date</param>
    ''' <returns>Dictionary with statistics</returns>
    Public Shared Function GetCourseAttendanceStats(courseId As Integer, startDate As DateTime, endDate As DateTime) As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            Dim baseQuery As String = "SELECT COUNT(*) FROM attendance WHERE course_id = @course_id AND date BETWEEN @start_date AND @end_date"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course_id", courseId},
                {"@start_date", startDate.ToString("yyyy-MM-dd")},
                {"@end_date", endDate.ToString("yyyy-MM-dd")}
            }

            ' Get total records
            stats("TotalRecords") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(baseQuery, parameters))

            ' Get present count
            Dim presentQuery As String = baseQuery & " AND status = 'Present'"
            stats("PresentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(presentQuery, parameters))

            ' Get absent count
            Dim absentQuery As String = baseQuery & " AND status = 'Absent'"
            stats("AbsentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(absentQuery, parameters))

            ' Get late count
            Dim lateQuery As String = baseQuery & " AND status = 'Late'"
            stats("LateCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(lateQuery, parameters))

            ' Get excused count
            Dim excusedQuery As String = baseQuery & " AND status = 'Excused'"
            stats("ExcusedCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(excusedQuery, parameters))

            Return stats

        Catch ex As Exception
            Logger.LogError("Error getting course attendance statistics", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

#End Region

#Region "Date Range Reports"

    ''' <summary>
    ''' Gets attendance report for a date range (all students/courses)
    ''' </summary>
    ''' <param name="startDate">Start date</param>
    ''' <param name="endDate">End date</param>
    ''' <returns>DataTable with attendance records</returns>
    Public Shared Function GetDateRangeAttendanceReport(startDate As DateTime, endDate As DateTime) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    a.id,
                    a.date,
                    a.student_id,
                    s.name as student_name,
                    c.course_name,
                    a.status,
                    TIME_FORMAT(a.time_in, '%H:%i') as time_in,
                    TIME_FORMAT(a.time_out, '%H:%i') as time_out,
                    a.remarks,
                    a.recorded_by
                FROM attendance a
                INNER JOIN students s ON a.student_id = s.student_id
                INNER JOIN courses c ON a.course_id = c.id
                WHERE a.date BETWEEN @start_date AND @end_date
                ORDER BY a.date DESC, a.time_in DESC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@start_date", startDate.ToString("yyyy-MM-dd")},
                {"@end_date", endDate.ToString("yyyy-MM-dd")}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting date range attendance report", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets attendance statistics for a date range
    ''' </summary>
    ''' <param name="startDate">Start date</param>
    ''' <param name="endDate">End date</param>
    ''' <returns>Dictionary with statistics</returns>
    Public Shared Function GetDateRangeAttendanceStats(startDate As DateTime, endDate As DateTime) As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            Dim baseQuery As String = "SELECT COUNT(*) FROM attendance WHERE date BETWEEN @start_date AND @end_date"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@start_date", startDate.ToString("yyyy-MM-dd")},
                {"@end_date", endDate.ToString("yyyy-MM-dd")}
            }

            ' Get total records
            stats("TotalRecords") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(baseQuery, parameters))

            ' Get present count
            Dim presentQuery As String = baseQuery & " AND status = 'Present'"
            stats("PresentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(presentQuery, parameters))

            ' Get absent count
            Dim absentQuery As String = baseQuery & " AND status = 'Absent'"
            stats("AbsentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(absentQuery, parameters))

            ' Get late count
            Dim lateQuery As String = baseQuery & " AND status = 'Late'"
            stats("LateCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(lateQuery, parameters))

            ' Get excused count
            Dim excusedQuery As String = baseQuery & " AND status = 'Excused'"
            stats("ExcusedCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(excusedQuery, parameters))

            Return stats

        Catch ex As Exception
            Logger.LogError("Error getting date range attendance statistics", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

#End Region

#Region "Overall Statistics"

    ''' <summary>
    ''' Gets overall attendance statistics (all-time)
    ''' </summary>
    ''' <returns>Dictionary with statistics</returns>
    Public Shared Function GetOverallAttendanceStats() As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            ' Get total records
            stats("TotalRecords") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM attendance"))

            ' Get present count
            stats("PresentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM attendance WHERE status = 'Present'"))

            ' Get absent count
            stats("AbsentCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM attendance WHERE status = 'Absent'"))

            ' Get late count
            stats("LateCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM attendance WHERE status = 'Late'"))

            ' Get excused count
            stats("ExcusedCount") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM attendance WHERE status = 'Excused'"))

            Return stats

        Catch ex As Exception
            Logger.LogError("Error getting overall attendance statistics", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

    ''' <summary>
    ''' Gets monthly attendance trend (last 6 months)
    ''' </summary>
    ''' <returns>DataTable with monthly statistics</returns>
    Public Shared Function GetMonthlyAttendanceTrend() As DataTable
        Try
            Dim query As String = "
                SELECT 
                    DATE_FORMAT(date, '%Y-%m') as month,
                    COUNT(*) as total_records,
                    SUM(CASE WHEN status = 'Present' THEN 1 ELSE 0 END) as present_count,
                    SUM(CASE WHEN status = 'Absent' THEN 1 ELSE 0 END) as absent_count,
                    SUM(CASE WHEN status = 'Late' THEN 1 ELSE 0 END) as late_count,
                    ROUND((SUM(CASE WHEN status IN ('Present', 'Late', 'Excused') THEN 1 ELSE 0 END) / COUNT(*)) * 100, 2) as attendance_rate
                FROM attendance
                WHERE date >= DATE_SUB(CURDATE(), INTERVAL 6 MONTH)
                GROUP BY DATE_FORMAT(date, '%Y-%m')
                ORDER BY month DESC"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting monthly attendance trend", ex)
            Return New DataTable()
        End Try
    End Function

#End Region

#Region "Helper Functions"

    ''' <summary>
    ''' Gets all active students for dropdown
    ''' </summary>
    ''' <returns>DataTable with students</returns>
    Public Shared Function GetAllActiveStudents() As DataTable
        Try
            Dim query As String = "
                SELECT 
                    student_id,
                    CONCAT(student_id, ' - ', name) as display_name
                FROM students
                WHERE status = 'Active'
                ORDER BY name"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting active students", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets all active courses for dropdown
    ''' </summary>
    ''' <returns>DataTable with courses</returns>
    Public Shared Function GetAllActiveCourses() As DataTable
        Try
            Dim query As String = "
                SELECT 
                    id,
                    course_name
                FROM courses
                WHERE status = 'Active'
                ORDER BY course_name"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting active courses", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets top students by attendance rate
    ''' </summary>
    ''' <param name="limit">Number of students to return</param>
    ''' <returns>DataTable with top students</returns>
    Public Shared Function GetTopStudentsByAttendance(Optional limit As Integer = 10) As DataTable
        Try
            Dim query As String = $"
                SELECT 
                    s.student_id,
                    s.name,
                    COUNT(a.id) as total_sessions,
                    SUM(CASE WHEN a.status = 'Present' THEN 1 ELSE 0 END) as present_count,
                    ROUND((SUM(CASE WHEN a.status IN ('Present', 'Late', 'Excused') THEN 1 ELSE 0 END) / COUNT(a.id)) * 100, 2) as attendance_rate
                FROM students s
                INNER JOIN attendance a ON s.student_id = a.student_id
                WHERE s.status = 'Active'
                GROUP BY s.student_id, s.name
                HAVING COUNT(a.id) >= 5
                ORDER BY attendance_rate DESC
                LIMIT {limit}"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting top students by attendance", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets students with low attendance rate
    ''' </summary>
    ''' <param name="threshold">Attendance rate threshold (default 75%)</param>
    ''' <param name="limit">Number of students to return</param>
    ''' <returns>DataTable with students</returns>
    Public Shared Function GetStudentsWithLowAttendance(Optional threshold As Double = 75.0, Optional limit As Integer = 10) As DataTable
        Try
            Dim query As String = $"
                SELECT 
                    s.student_id,
                    s.name,
                    s.course,
                    COUNT(a.id) as total_sessions,
                    SUM(CASE WHEN a.status = 'Present' THEN 1 ELSE 0 END) as present_count,
                    SUM(CASE WHEN a.status = 'Absent' THEN 1 ELSE 0 END) as absent_count,
                    ROUND((SUM(CASE WHEN a.status IN ('Present', 'Late', 'Excused') THEN 1 ELSE 0 END) / COUNT(a.id)) * 100, 2) as attendance_rate
                FROM students s
                INNER JOIN attendance a ON s.student_id = a.student_id
                WHERE s.status = 'Active'
                GROUP BY s.student_id, s.name, s.course
                HAVING COUNT(a.id) >= 5 AND attendance_rate < {threshold}
                ORDER BY attendance_rate ASC
                LIMIT {limit}"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting students with low attendance", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets course attendance summary
    ''' </summary>
    ''' <returns>DataTable with course summaries</returns>
    Public Shared Function GetCourseAttendanceSummary() As DataTable
        Try
            Dim query As String = "
                SELECT 
                    c.course_code,
                    c.course_name,
                    COUNT(DISTINCT e.student_id) as enrolled_students,
                    COUNT(a.id) as total_records,
                    SUM(CASE WHEN a.status = 'Present' THEN 1 ELSE 0 END) as present_count,
                    SUM(CASE WHEN a.status = 'Absent' THEN 1 ELSE 0 END) as absent_count,
                    ROUND((SUM(CASE WHEN a.status IN ('Present', 'Late', 'Excused') THEN 1 ELSE 0 END) / COUNT(a.id)) * 100, 2) as attendance_rate
                FROM courses c
                LEFT JOIN enrollments e ON c.id = e.course_id AND e.status = 'Enrolled'
                LEFT JOIN attendance a ON c.id = a.course_id
                WHERE c.status = 'Active'
                GROUP BY c.id, c.course_code, c.course_name
                ORDER BY c.course_name"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting course attendance summary", ex)
            Return New DataTable()
        End Try
    End Function

#End Region

End Class