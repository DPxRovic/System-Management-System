' ==========================================
' FILENAME: /Data/StudentRepository.vb
' PURPOSE: Data access layer for student academic records management
' AUTHOR: System
' DATE: 2025-10-18
' ==========================================

Imports System.Data

Public Class StudentRepository

#Region "Student CRUD Operations"

    ''' <summary>
    ''' Gets all students (including archived if specified)
    ''' </summary>
    ''' <param name="includeArchived">Include archived students</param>
    ''' <returns>DataTable with student records</returns>
    Public Shared Function GetAllStudents(Optional includeArchived As Boolean = False) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    s.id,
                    s.student_id,
                    s.name,
                    s.email,
                    s.phone_number,
                    s.course,
                    s.date_of_birth,
                    s.enrollment_date,
                    s.status,
                    s.created_at
                FROM students s"

            If Not includeArchived Then
                query &= " WHERE s.status != 'Archived'"
            End If

            query &= " ORDER BY s.name ASC"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting all students", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets student by ID (database primary key)
    ''' </summary>
    ''' <param name="id">Database ID</param>
    ''' <returns>Student object</returns>
    Public Shared Function GetStudentById(id As Integer) As Student
        Try
            Dim query As String = "
                SELECT 
                    id,
                    student_id,
                    name,
                    email,
                    phone_number,
                    course,
                    date_of_birth,
                    enrollment_date,
                    status,
                    created_at
                FROM students
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", id}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            If dt.Rows.Count > 0 Then
                Return MapRowToStudent(dt.Rows(0))
            End If

            Return Nothing

        Catch ex As Exception
            Logger.LogError("Error getting student by ID", ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets student by student ID (unique identifier)
    ''' </summary>
    ''' <param name="studentId">Student ID number</param>
    ''' <returns>Student object</returns>
    Public Shared Function GetStudentByStudentId(studentId As String) As Student
        Try
            Dim query As String = "
                SELECT 
                    id,
                    student_id,
                    name,
                    email,
                    phone_number,
                    course,
                    date_of_birth,
                    enrollment_date,
                    status,
                    created_at
                FROM students
                WHERE student_id = @student_id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            If dt.Rows.Count > 0 Then
                Return MapRowToStudent(dt.Rows(0))
            End If

            Return Nothing

        Catch ex As Exception
            Logger.LogError("Error getting student by student ID", ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Creates a new student record
    ''' </summary>
    ''' <param name="student">Student object with data</param>
    ''' <returns>True if successful</returns>
    Public Shared Function CreateStudent(student As Student) As Boolean
        Try
            ' Check if student ID already exists
            If StudentIdExists(student.StudentId) Then
                Throw New Exception("Student ID already exists")
            End If

            ' Check if email already exists (if provided)
            If Not String.IsNullOrWhiteSpace(student.Email) AndAlso EmailExists(student.Email) Then
                Throw New Exception("Email already exists")
            End If

            Dim query As String = "
                INSERT INTO students (
                    student_id,
                    name,
                    email,
                    phone_number,
                    course,
                    date_of_birth,
                    enrollment_date,
                    status
                ) VALUES (
                    @student_id,
                    @name,
                    @email,
                    @phone_number,
                    @course,
                    @date_of_birth,
                    @enrollment_date,
                    @status
                )"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", student.StudentId},
                {"@name", student.Name},
                {"@email", If(String.IsNullOrWhiteSpace(student.Email), DBNull.Value, student.Email)},
                {"@phone_number", If(String.IsNullOrWhiteSpace(student.PhoneNumber), DBNull.Value, student.PhoneNumber)},
                {"@course", student.Course},
                {"@date_of_birth", If(student.DateOfBirth.HasValue, student.DateOfBirth.Value, DBNull.Value)},
                {"@enrollment_date", If(student.EnrollmentDate.HasValue, student.EnrollmentDate.Value, DateTime.Today)},
                {"@status", If(String.IsNullOrWhiteSpace(student.Status), "Active", student.Status)}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Student {student.StudentId} created successfully")
                Return True
            End If

            Return False

        Catch ex As Exception
            Logger.LogError("Error creating student", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Updates an existing student record
    ''' </summary>
    ''' <param name="student">Student object with updated data</param>
    ''' <returns>True if successful</returns>
    Public Shared Function UpdateStudent(student As Student) As Boolean
        Try
            ' Check if student ID exists for other students
            If StudentIdExistsForOtherStudent(student.StudentId, student.Id) Then
                Throw New Exception("Student ID already exists for another student")
            End If

            ' Check if email exists for other students (if provided)
            If Not String.IsNullOrWhiteSpace(student.Email) AndAlso EmailExistsForOtherStudent(student.Email, student.Id) Then
                Throw New Exception("Email already exists for another student")
            End If

            Dim query As String = "
                UPDATE students
                SET
                    student_id = @student_id,
                    name = @name,
                    email = @email,
                    phone_number = @phone_number,
                    course = @course,
                    date_of_birth = @date_of_birth,
                    enrollment_date = @enrollment_date,
                    status = @status
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", student.Id},
                {"@student_id", student.StudentId},
                {"@name", student.Name},
                {"@email", If(String.IsNullOrWhiteSpace(student.Email), DBNull.Value, student.Email)},
                {"@phone_number", If(String.IsNullOrWhiteSpace(student.PhoneNumber), DBNull.Value, student.PhoneNumber)},
                {"@course", student.Course},
                {"@date_of_birth", If(student.DateOfBirth.HasValue, student.DateOfBirth.Value, DBNull.Value)},
                {"@enrollment_date", If(student.EnrollmentDate.HasValue, student.EnrollmentDate.Value, DateTime.Today)},
                {"@status", If(String.IsNullOrWhiteSpace(student.Status), "Active", student.Status)}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Student {student.StudentId} updated successfully")
                Return True
            End If

            Return False

        Catch ex As Exception
            Logger.LogError("Error updating student", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Archives a student (soft delete)
    ''' </summary>
    ''' <param name="id">Student database ID</param>
    ''' <returns>True if successful</returns>
    Public Shared Function ArchiveStudent(id As Integer) As Boolean
        Try
            Dim query As String = "
                UPDATE students
                SET status = 'Archived'
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", id}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Student ID {id} archived successfully")
                Return True
            End If

            Return False

        Catch ex As Exception
            Logger.LogError("Error archiving student", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Restores an archived student
    ''' </summary>
    ''' <param name="id">Student database ID</param>
    ''' <returns>True if successful</returns>
    Public Shared Function RestoreStudent(id As Integer) As Boolean
        Try
            Dim query As String = "
                UPDATE students
                SET status = 'Active'
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", id}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Student ID {id} restored successfully")
                Return True
            End If

            Return False

        Catch ex As Exception
            Logger.LogError("Error restoring student", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Permanently deletes a student
    ''' </summary>
    ''' <param name="id">Student database ID</param>
    ''' <returns>True if successful</returns>
    Public Shared Function DeleteStudent(id As Integer) As Boolean
        Try
            ' First, delete related attendance records
            Dim deleteAttendanceQuery As String = "DELETE FROM attendance WHERE student_id IN (SELECT student_id FROM students WHERE id = @id)"
            DatabaseHandler.ExecuteNonQuery(deleteAttendanceQuery, New Dictionary(Of String, Object) From {{"@id", id}})

            ' Then delete the student
            Dim query As String = "DELETE FROM students WHERE id = @id"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", id}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Student ID {id} deleted permanently")
                Return True
            End If

            Return False

        Catch ex As Exception
            Logger.LogError("Error deleting student", ex)
            Return False
        End Try
    End Function

#End Region

#Region "Search and Filter"

    ''' <summary>
    ''' Searches students by various criteria
    ''' </summary>
    ''' <param name="searchTerm">Search term</param>
    ''' <param name="course">Filter by course (optional)</param>
    ''' <param name="status">Filter by status (optional)</param>
    ''' <returns>DataTable with matching students</returns>
    Public Shared Function SearchStudents(searchTerm As String, Optional course As String = "", Optional status As String = "") As DataTable
        Try
            Dim query As String = "
                SELECT 
                    id,
                    student_id,
                    name,
                    email,
                    phone_number,
                    course,
                    date_of_birth,
                    enrollment_date,
                    status,
                    created_at
                FROM students
                WHERE (
                    student_id LIKE @search OR
                    name LIKE @search OR
                    email LIKE @search OR
                    phone_number LIKE @search
                )"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@search", $"%{searchTerm}%"}
            }

            ' Add course filter if specified
            If Not String.IsNullOrWhiteSpace(course) Then
                query &= " AND course = @course"
                parameters.Add("@course", course)
            End If

            ' Add status filter if specified
            If Not String.IsNullOrWhiteSpace(status) Then
                query &= " AND status = @status"
                parameters.Add("@status", status)
            End If

            query &= " ORDER BY name ASC"

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error searching students", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets all distinct courses from students table
    ''' </summary>
    ''' <returns>DataTable with unique courses</returns>
    Public Shared Function GetAllCourses() As DataTable
        Try
            Dim query As String = "
                SELECT DISTINCT course
                FROM students
                WHERE course IS NOT NULL AND course != ''
                ORDER BY course ASC"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting courses", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets students by course
    ''' </summary>
    ''' <param name="course">Course name</param>
    ''' <returns>DataTable with students in course</returns>
    Public Shared Function GetStudentsByCourse(course As String) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    id,
                    student_id,
                    name,
                    email,
                    phone_number,
                    course,
                    date_of_birth,
                    enrollment_date,
                    status,
                    created_at
                FROM students
                WHERE course = @course
                AND status != 'Archived'
                ORDER BY name ASC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course", course}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting students by course", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets students by status
    ''' </summary>
    ''' <param name="status">Status (Active, Inactive, Graduated, etc.)</param>
    ''' <returns>DataTable with students</returns>
    Public Shared Function GetStudentsByStatus(status As String) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    id,
                    student_id,
                    name,
                    email,
                    phone_number,
                    course,
                    date_of_birth,
                    enrollment_date,
                    status,
                    created_at
                FROM students
                WHERE status = @status
                ORDER BY name ASC"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@status", status}
            }

            Return DatabaseHandler.ExecuteReader(query, parameters)

        Catch ex As Exception
            Logger.LogError("Error getting students by status", ex)
            Return New DataTable()
        End Try
    End Function

#End Region

#Region "Statistics"

    ''' <summary>
    ''' Gets student statistics
    ''' </summary>
    ''' <returns>Dictionary with statistics</returns>
    Public Shared Function GetStudentStatistics() As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            stats("TotalStudents") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM students WHERE status = 'Active'"))
            stats("InactiveStudents") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM students WHERE status = 'Inactive'"))
            stats("ArchivedStudents") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM students WHERE status = 'Archived'"))
            stats("GraduatedStudents") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM students WHERE status = 'Graduated'"))
            stats("TotalCourses") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(DISTINCT course) FROM students WHERE course IS NOT NULL"))

            Return stats

        Catch ex As Exception
            Logger.LogError("Error getting student statistics", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

    ''' <summary>
    ''' Gets student count by course
    ''' </summary>
    ''' <returns>DataTable with course enrollment counts</returns>
    Public Shared Function GetStudentCountByCourse() As DataTable
        Try
            Dim query As String = "
                SELECT 
                    course,
                    COUNT(*) as student_count
                FROM students
                WHERE status = 'Active'
                AND course IS NOT NULL
                GROUP BY course
                ORDER BY student_count DESC"

            Return DatabaseHandler.ExecuteReader(query)

        Catch ex As Exception
            Logger.LogError("Error getting student count by course", ex)
            Return New DataTable()
        End Try
    End Function

#End Region

#Region "Validation Helpers"

    ''' <summary>
    ''' Checks if student ID exists
    ''' </summary>
    ''' <param name="studentId">Student ID to check</param>
    ''' <returns>True if exists</returns>
    Private Shared Function StudentIdExists(studentId As String) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM students WHERE student_id = @student_id"
            Dim parameters As New Dictionary(Of String, Object) From {{"@student_id", studentId}}
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking student ID existence", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if student ID exists for another student
    ''' </summary>
    ''' <param name="studentId">Student ID to check</param>
    ''' <param name="currentId">Current student database ID</param>
    ''' <returns>True if exists for another student</returns>
    Private Shared Function StudentIdExistsForOtherStudent(studentId As String, currentId As Integer) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM students WHERE student_id = @student_id AND id != @id"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@student_id", studentId},
                {"@id", currentId}
            }
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking student ID for other student", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if email exists
    ''' </summary>
    ''' <param name="email">Email to check</param>
    ''' <returns>True if exists</returns>
    Private Shared Function EmailExists(email As String) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM students WHERE email = @email"
            Dim parameters As New Dictionary(Of String, Object) From {{"@email", email}}
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking email existence", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if email exists for another student
    ''' </summary>
    ''' <param name="email">Email to check</param>
    ''' <param name="currentId">Current student database ID</param>
    ''' <returns>True if exists for another student</returns>
    Private Shared Function EmailExistsForOtherStudent(email As String, currentId As Integer) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM students WHERE email = @email AND id != @id"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@email", email},
                {"@id", currentId}
            }
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking email for other student", ex)
            Return False
        End Try
    End Function

#End Region

#Region "Helper Methods"

    ''' <summary>
    ''' Maps a DataRow to a Student object
    ''' </summary>
    ''' <param name="row">DataRow with student data</param>
    ''' <returns>Student object</returns>
    Private Shared Function MapRowToStudent(row As DataRow) As Student
        Try
            Dim student As New Student With {
                .Id = Convert.ToInt32(row("id")),
                .StudentId = row("student_id").ToString(),
                .Name = row("name").ToString(),
                .Email = If(IsDBNull(row("email")), "", row("email").ToString()),
                .PhoneNumber = If(IsDBNull(row("phone_number")), "", row("phone_number").ToString()),
                .Course = row("course").ToString(),
                .DateOfBirth = If(IsDBNull(row("date_of_birth")), Nothing, Convert.ToDateTime(row("date_of_birth"))),
                .EnrollmentDate = If(IsDBNull(row("enrollment_date")), Nothing, Convert.ToDateTime(row("enrollment_date"))),
                .Status = row("status").ToString(),
                .CreatedAt = Convert.ToDateTime(row("created_at"))
            }

            Return student

        Catch ex As Exception
            Logger.LogError("Error mapping row to student", ex)
            Return Nothing
        End Try
    End Function

#End Region

End Class