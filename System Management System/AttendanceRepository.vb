' ==========================================
' FILENAME: /Data/AttendanceRepository.vb
' PURPOSE: Data access layer for attendance operations
' AUTHOR: System
' DATE: 2025-10-14
' ==========================================

Imports System.Data

Public Class AttendanceRepository

    ''' <summary>
    ''' Records attendance for a student
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="courseId">Course ID</param>
    ''' <param name="status">Attendance status (Present, Absent, Late, Excused)</param>
    ''' <param name="recordedBy">Username of person recording attendance</param>
    ''' <param name="remarks">Optional remarks</param>
    ''' <returns>True if successful</returns>
    Public Shared Function RecordAttendance(studentId As String, courseId As Integer, status As String, recordedBy As String, Optional remarks As String = "") As Boolean
        Try
            ' Check if attendance already exists for today
            If AttendanceExistsToday(studentId, courseId) Then
                ' Update existing attendance
                Return UpdateAttendance(studentId, courseId, status, recordedBy, remarks)
            Else
                ' Insert new attendance
                Return InsertAttendance(studentId, courseId, status, recordedBy, remarks)
            End If
        Catch ex As Exception
            Logger.LogError("Error recording attendance", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Inserts new attendance record
    ''' </summary>
    Private Shared Function InsertAttendance(studentId As String, courseId As Integer, status As String, recordedBy As String, remarks As String) As Boolean
        Try
            Dim query As String = "
                INSERT INTO attendance (student_id, course_id, date, status, time_in, remarks, recorded_by)
                VALUES (@student_id, @course_id, CURDATE(), @status, CURTIME(), @remarks, @recorded_by)"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@course_id", courseId},
                {"@status", status},
                {"@remarks", remarks},
                {"@recorded_by", recordedBy}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Attendance recorded for student {studentId}: {status}")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error inserting attendance", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Updates existing attendance record
    ''' </summary>
    Private Shared Function UpdateAttendance(studentId As String, courseId As Integer, status As String, recordedBy As String, remarks As String) As Boolean
        Try
            Dim query As String = "
                UPDATE attendance 
                SET status = @status, 
                    time_in = CURTIME(), 
                    remarks = @remarks, 
                    recorded_by = @recorded_by
                WHERE student_id = @student_id 
                AND course_id = @course_id 
                AND date = CURDATE()"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@course_id", courseId},
                {"@status", status},
                {"@remarks", remarks},
                {"@recorded_by", recordedBy}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Attendance updated for student {studentId}: {status}")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error updating attendance", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Checks if attendance record exists for today
    ''' </summary>
    Public Shared Function AttendanceExistsToday(studentId As String, courseId As Integer) As Boolean
        Try
            Dim query As String = "
                SELECT COUNT(*) 
                FROM attendance 
                WHERE student_id = @student_id 
                AND course_id = @course_id 
                AND date = CURDATE()"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@course_id", courseId}
            }

            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking attendance existence", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets student by student ID
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <returns>Student object if found, Nothing otherwise</returns>
    Public Shared Function GetStudentByStudentId(studentId As String) As Student
        Try
            Dim query As String = "
                SELECT id, student_id, name, course, email, phone_number, date_of_birth, enrollment_date, status
                FROM students
                WHERE student_id = @student_id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                Dim student As New Student With {
                    .Id = Convert.ToInt32(row("id")),
                    .StudentId = row("student_id").ToString(),
                    .Name = row("name").ToString(),
                    .Course = row("course").ToString(),
                    .Email = If(IsDBNull(row("email")), "", row("email").ToString()),
                    .PhoneNumber = If(IsDBNull(row("phone_number")), "", row("phone_number").ToString()),
                    .DateOfBirth = If(IsDBNull(row("date_of_birth")), DateTime.MinValue, Convert.ToDateTime(row("date_of_birth"))),
                    .EnrollmentDate = If(IsDBNull(row("enrollment_date")), DateTime.MinValue, Convert.ToDateTime(row("enrollment_date"))),
                    .Status = row("status").ToString()
                }

                Return student
            End If

            Return Nothing
        Catch ex As Exception
            Logger.LogError("Error getting student by ID", ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets attendance records for a student
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <param name="startDate">Start date (optional)</param>
    ''' <param name="endDate">End date (optional)</param>
    ''' <returns>DataTable with attendance records</returns>
    Public Shared Function GetStudentAttendance(studentId As String, Optional startDate As DateTime? = Nothing, Optional endDate As DateTime? = Nothing) As DataTable
        Try
            Dim query As String = "
                SELECT a.id, a.student_id, s.name AS student_name, 
                       c.course_name, a.date, a.status, 
                       a.time_in, a.time_out, a.remarks, a.recorded_by
                FROM attendance a
                INNER JOIN students s ON a.student_id = s.student_id
                LEFT JOIN courses c ON a.course_id = c.id
                WHERE a.student_id = @student_id"

            If startDate.HasValue Then
                query &= " AND a.date >= @start_date"
            End If

            If endDate.HasValue Then
                query &= " AND a.date <= @end_date"
            End If

            query &= " ORDER BY a.date DESC, a.time_in DESC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            If startDate.HasValue Then
                parameters.Add("@start_date", startDate.Value.ToString("yyyy-MM-dd"))
            End If

            If endDate.HasValue Then
                parameters.Add("@end_date", endDate.Value.ToString("yyyy-MM-dd"))
            End If

            Return DatabaseHandler.ExecuteReader(query, parameters)
        Catch ex As Exception
            Logger.LogError("Error getting student attendance", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets today's attendance summary
    ''' </summary>
    ''' <returns>DataTable with attendance summary</returns>
    Public Shared Function GetTodayAttendanceSummary() As DataTable
        Try
            Dim query As String = "
                SELECT a.student_id, s.name AS student_name, 
                       c.course_name, a.status, a.time_in, a.recorded_by
                FROM attendance a
                INNER JOIN students s ON a.student_id = s.student_id
                LEFT JOIN courses c ON a.course_id = c.id
                WHERE a.date = CURDATE()
                ORDER BY a.time_in DESC"

            Return DatabaseHandler.ExecuteReader(query)
        Catch ex As Exception
            Logger.LogError("Error getting today's attendance summary", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets attendance statistics for a student
    ''' </summary>
    ''' <param name="studentId">Student ID</param>
    ''' <returns>Dictionary with attendance statistics</returns>
    Public Shared Function GetAttendanceStatistics(studentId As String) As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            ' Get total attendance records
            Dim totalQuery As String = "SELECT COUNT(*) FROM attendance WHERE student_id = @student_id"
            Dim totalParams As New Dictionary(Of String, Object) From {{"@student_id", studentId}}
            stats("Total") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(totalQuery, totalParams))

            ' Get present count
            Dim presentQuery As String = "SELECT COUNT(*) FROM attendance WHERE student_id = @student_id AND status = 'Present'"
            stats("Present") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(presentQuery, totalParams))

            ' Get absent count
            Dim absentQuery As String = "SELECT COUNT(*) FROM attendance WHERE student_id = @student_id AND status = 'Absent'"
            stats("Absent") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(absentQuery, totalParams))

            ' Get late count
            Dim lateQuery As String = "SELECT COUNT(*) FROM attendance WHERE student_id = @student_id AND status = 'Late'"
            stats("Late") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(lateQuery, totalParams))

            ' Get excused count
            Dim excusedQuery As String = "SELECT COUNT(*) FROM attendance WHERE student_id = @student_id AND status = 'Excused'"
            stats("Excused") = Convert.ToInt32(DatabaseHandler.ExecuteScalar(excusedQuery, totalParams))

            ' Calculate attendance rate
            If stats("Total") > 0 Then
                Dim attendanceRate As Double = ((stats("Present") + stats("Late") + stats("Excused")) / stats("Total")) * 100
                stats("AttendanceRate") = Convert.ToInt32(Math.Round(attendanceRate))
            Else
                stats("AttendanceRate") = 0
            End If

            Return stats
        Catch ex As Exception
            Logger.LogError("Error getting attendance statistics", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

    ''' <summary>
    ''' Gets all active courses
    ''' </summary>
    ''' <returns>DataTable with active courses</returns>
    Public Shared Function GetActiveCourses() As DataTable
        Try
            Dim query As String = "
                SELECT id, course_code, course_name, faculty_id
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
    ''' Deletes an attendance record
    ''' </summary>
    ''' <param name="attendanceId">Attendance record ID</param>
    ''' <returns>True if successful</returns>
    Public Shared Function DeleteAttendance(attendanceId As Integer) As Boolean
        Try
            Dim query As String = "DELETE FROM attendance WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", attendanceId}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Attendance record {attendanceId} deleted")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error deleting attendance record", ex)
            Return False
        End Try
    End Function

End Class