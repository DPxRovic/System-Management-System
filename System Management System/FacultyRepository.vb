' ==========================================
' FILENAME: /Data/FacultyRepository.vb
' PURPOSE: Data access layer for faculty operations
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Imports System.Data

Public Class FacultyRepository

    ''' <summary>
    ''' Gets all courses assigned to a faculty member
    ''' </summary>
    ''' <param name="facultyId">Faculty user ID</param>
    ''' <returns>DataTable with faculty courses</returns>
    Public Shared Function GetFacultyCourses(facultyId As Integer) As DataTable
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
                    c.status,
                    COUNT(DISTINCT e.student_id) as enrolled_students
                FROM courses c
                LEFT JOIN enrollments e ON c.id = e.course_id AND e.status = 'Enrolled'
                WHERE c.faculty_id = @faculty_id AND c.status = 'Active'
                GROUP BY c.id, c.course_code, c.course_name, c.description, c.credits, c.schedule, c.room, c.status
                ORDER BY c.course_name"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@faculty_id", facultyId}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)
        Catch ex As Exception
            Logger.LogError("Error getting faculty courses", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets students enrolled in a specific course
    ''' </summary>
    ''' <param name="courseId">Course ID</param>
    ''' <returns>DataTable with enrolled students</returns>
    Public Shared Function GetEnrolledStudents(courseId As Integer) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    s.id,
                    s.student_id,
                    s.name,
                    s.course as program,
                    s.email,
                    s.phone_number,
                    e.enrollment_date,
                    e.status as enrollment_status,
                    e.grade
                FROM students s
                INNER JOIN enrollments e ON s.student_id = e.student_id
                WHERE e.course_id = @course_id AND e.status = 'Enrolled'
                ORDER BY s.name"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course_id", courseId}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)
        Catch ex As Exception
            Logger.LogError("Error getting enrolled students", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets attendance statistics for a course
    ''' </summary>
    ''' <param name="courseId">Course ID</param>
    ''' <returns>Dictionary with attendance statistics</returns>
    Public Shared Function GetCourseAttendanceStats(courseId As Integer) As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            ' Get total attendance records
            Dim totalQuery As String = "SELECT COUNT(*) FROM attendance WHERE course_id = @course_id"
            Dim params As New Dictionary(Of String, Object) From {{"@course_id", courseId}}
            stats("Total") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(totalQuery, params))

            ' Get present count
            Dim presentQuery As String = "SELECT COUNT(*) FROM attendance WHERE course_id = @course_id AND status = 'Present'"
            stats("Present") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(presentQuery, params))

            ' Get absent count
            Dim absentQuery As String = "SELECT COUNT(*) FROM attendance WHERE course_id = @course_id AND status = 'Absent'"
            stats("Absent") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(absentQuery, params))

            ' Get late count
            Dim lateQuery As String = "SELECT COUNT(*) FROM attendance WHERE course_id = @course_id AND status = 'Late'"
            stats("Late") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(lateQuery, params))

            ' Get today's attendance count
            Dim todayQuery As String = "SELECT COUNT(*) FROM attendance WHERE course_id = @course_id AND date = CURDATE()"
            stats("Today") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(todayQuery, params))

            ' Calculate attendance rate
            If stats("Total") > 0 Then
                Dim attendanceRate As Double = ((stats("Present") + stats("Late")) / stats("Total")) * 100
                stats("AttendanceRate") = Convert.ToInt32(Math.Round(attendanceRate))
            Else
                stats("AttendanceRate") = 0
            End If

            Return stats
        Catch ex As Exception
            Logger.LogError("Error getting course attendance statistics", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

    ''' <summary>
    ''' Gets course details by ID
    ''' </summary>
    ''' <param name="courseId">Course ID</param>
    ''' <returns>Course object</returns>
    Public Shared Function GetCourseById(courseId As Integer) As Course
        Try
            Dim query As String = "
                SELECT id, course_code, course_name, faculty_id, description, 
                       credits, schedule, room, status
                FROM courses
                WHERE id = @course_id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course_id", courseId}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                Dim course As New Course With {
                    .Id = Convert.ToInt32(row("id")),
                    .CourseCode = row("course_code").ToString(),
                    .CourseName = row("course_name").ToString(),
                    .FacultyId = Convert.ToInt32(row("faculty_id")),
                    .Description = If(IsDBNull(row("description")), "", row("description").ToString()),
                    .Credits = If(IsDBNull(row("credits")), 0, Convert.ToInt32(row("credits"))),
                    .Schedule = If(IsDBNull(row("schedule")), "", row("schedule").ToString()),
                    .Room = If(IsDBNull(row("room")), "", row("room").ToString()),
                    .Status = row("status").ToString()
                }

                Return course
            End If

            Return Nothing
        Catch ex As Exception
            Logger.LogError("Error getting course by ID", ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets attendance records for a course
    ''' </summary>
    ''' <param name="courseId">Course ID</param>
    ''' <param name="startDate">Start date (optional)</param>
    ''' <param name="endDate">End date (optional)</param>
    ''' <returns>DataTable with attendance records</returns>
    Public Shared Function GetCourseAttendance(courseId As Integer, Optional startDate As DateTime? = Nothing, Optional endDate As DateTime? = Nothing) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    a.id,
                    a.student_id,
                    s.name as student_name,
                    a.date,
                    a.status,
                    a.time_in,
                    a.time_out,
                    a.remarks,
                    a.recorded_by
                FROM attendance a
                INNER JOIN students s ON a.student_id = s.student_id
                WHERE a.course_id = @course_id"

            If startDate.HasValue Then
                query &= " AND a.date >= @start_date"
            End If

            If endDate.HasValue Then
                query &= " AND a.date <= @end_date"
            End If

            query &= " ORDER BY a.date DESC, a.time_in DESC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course_id", courseId}
            }

            If startDate.HasValue Then
                parameters.Add("@start_date", startDate.Value.ToString("yyyy-MM-dd"))
            End If

            If endDate.HasValue Then
                parameters.Add("@end_date", endDate.Value.ToString("yyyy-MM-dd"))
            End If

            Return DatabaseHandler.ExecuteReader(query, parameters)
        Catch ex As Exception
            Logger.LogError("Error getting course attendance", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets student attendance summary for a course
    ''' </summary>
    ''' <param name="courseId">Course ID</param>
    ''' <returns>DataTable with student attendance summary</returns>
    Public Shared Function GetStudentAttendanceSummary(courseId As Integer) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    s.student_id,
                    s.name,
                    COUNT(a.id) as total_sessions,
                    SUM(CASE WHEN a.status = 'Present' THEN 1 ELSE 0 END) as present_count,
                    SUM(CASE WHEN a.status = 'Absent' THEN 1 ELSE 0 END) as absent_count,
                    SUM(CASE WHEN a.status = 'Late' THEN 1 ELSE 0 END) as late_count,
                    SUM(CASE WHEN a.status = 'Excused' THEN 1 ELSE 0 END) as excused_count,
                    ROUND(
                        (SUM(CASE WHEN a.status IN ('Present', 'Late', 'Excused') THEN 1 ELSE 0 END) / COUNT(a.id)) * 100, 
                        2
                    ) as attendance_rate
                FROM students s
                INNER JOIN enrollments e ON s.student_id = e.student_id
                LEFT JOIN attendance a ON s.student_id = a.student_id AND a.course_id = @course_id
                WHERE e.course_id = @course_id AND e.status = 'Enrolled'
                GROUP BY s.student_id, s.name
                ORDER BY s.name"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course_id", courseId}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)
        Catch ex As Exception
            Logger.LogError("Error getting student attendance summary", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets faculty information by user ID
    ''' </summary>
    ''' <param name="userId">User ID</param>
    ''' <returns>Faculty object</returns>
    Public Shared Function GetFacultyByUserId(userId As Integer) As Faculty
        Try
            Dim query As String = "
                SELECT f.id, f.faculty_id, f.name, f.department, f.email, 
                       f.phone_number, f.specialization, f.hire_date, f.status
                FROM faculty f
                INNER JOIN users u ON f.name = u.fullname
                WHERE u.id = @user_id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@user_id", userId}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                Dim faculty As New Faculty With {
                    .Id = Convert.ToInt32(row("id")),
                    .FacultyId = row("faculty_id").ToString(),
                    .Name = row("name").ToString(),
                    .Department = If(IsDBNull(row("department")), "", row("department").ToString()),
                    .Email = If(IsDBNull(row("email")), "", row("email").ToString()),
                    .PhoneNumber = If(IsDBNull(row("phone_number")), "", row("phone_number").ToString()),
                    .Specialization = If(IsDBNull(row("specialization")), "", row("specialization").ToString()),
                    .HireDate = If(IsDBNull(row("hire_date")), DateTime.MinValue, Convert.ToDateTime(row("hire_date"))),
                    .Status = row("status").ToString()
                }

                Return faculty
            End If

            Return Nothing
        Catch ex As Exception
            Logger.LogError("Error getting faculty by user ID", ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Updates course information
    ''' </summary>
    ''' <param name="course">Course object with updated information</param>
    ''' <returns>True if successful</returns>
    Public Shared Function UpdateCourse(course As Course) As Boolean
        Try
            Dim query As String = "
                UPDATE courses
                SET course_name = @course_name,
                    description = @description,
                    credits = @credits,
                    schedule = @schedule,
                    room = @room
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", course.Id},
                {"@course_name", course.CourseName},
                {"@description", course.Description},
                {"@credits", course.Credits},
                {"@schedule", course.Schedule},
                {"@room", course.Room}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Course {course.CourseCode} updated successfully")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error updating course", ex)
            Return False
        End Try
    End Function

End Class