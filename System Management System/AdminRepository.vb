' ==========================================
' FILENAME: /Data/AdminRepository.vb
' PURPOSE: Data access layer for admin operations (CRUD for users and courses)
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Imports System.Data

Public Class AdminRepository

#Region "User Management"

    ''' <summary>
    ''' Gets all users (including archived if specified)
    ''' </summary>
    Public Shared Function GetAllUsers(Optional includeArchived As Boolean = False) As DataTable
        Try
            Dim query As String = "
                SELECT id, username, password, role, fullname, created_at, is_archived
                FROM users"

            If Not includeArchived Then
                query &= " WHERE is_archived = 0"
            End If

            query &= " ORDER BY created_at DESC"

            Return DatabaseHandler.ExecuteReader(query)
        Catch ex As Exception
            Logger.LogError("Error getting all users", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets user by ID
    ''' </summary>
    Public Shared Function GetUserById(userId As Integer) As User
        Try
            Dim query As String = "
                SELECT id, username, password, role, fullname, created_at, is_archived
                FROM users
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", userId}
            }

            Dim dt As DataTable = DatabaseHandler.ExecuteReader(query, parameters)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                Return New User With {
                    .Id = Convert.ToInt32(row("id")),
                    .Username = row("username").ToString(),
                    .Password = row("password").ToString(),
                    .Role = row("role").ToString(),
                    .FullName = row("fullname").ToString(),
                    .CreatedAt = Convert.ToDateTime(row("created_at"))
                }
            End If

            Return Nothing
        Catch ex As Exception
            Logger.LogError("Error getting user by ID", ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Creates a new user
    ''' </summary>
    Public Shared Function CreateUser(username As String, password As String, role As String, fullname As String) As Boolean
        Try
            ' Check if username already exists
            If UsernameExists(username) Then
                Throw New Exception("Username already exists")
            End If

            Dim query As String = "
                INSERT INTO users (username, password, role, fullname, is_archived)
                VALUES (@username, @password, @role, @fullname, 0)"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@username", username},
                {"@password", password},
                {"@role", role},
                {"@fullname", fullname}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"User {username} created successfully with role {role}")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error creating user", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Updates an existing user
    ''' </summary>
    Public Shared Function UpdateUser(userId As Integer, username As String, password As String, role As String, fullname As String) As Boolean
        Try
            ' Check if username exists for other users
            If UsernameExistsForOtherUser(username, userId) Then
                Throw New Exception("Username already exists")
            End If

            Dim query As String = "
                UPDATE users
                SET username = @username,
                    password = @password,
                    role = @role,
                    fullname = @fullname
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", userId},
                {"@username", username},
                {"@password", password},
                {"@role", role},
                {"@fullname", fullname}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"User {username} updated successfully")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error updating user", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Archives a user (soft delete)
    ''' </summary>
    Public Shared Function ArchiveUser(userId As Integer) As Boolean
        Try
            Dim query As String = "
                UPDATE users
                SET is_archived = 1
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", userId}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"User ID {userId} archived successfully")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error archiving user", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Restores an archived user
    ''' </summary>
    Public Shared Function RestoreUser(userId As Integer) As Boolean
        Try
            Dim query As String = "
                UPDATE users
                SET is_archived = 0
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", userId}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"User ID {userId} restored successfully")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error restoring user", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Permanently deletes a user
    ''' </summary>
    Public Shared Function DeleteUser(userId As Integer) As Boolean
        Try
            Dim query As String = "DELETE FROM users WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", userId}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"User ID {userId} deleted permanently")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error deleting user", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if username exists
    ''' </summary>
    Private Shared Function UsernameExists(username As String) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM users WHERE username = @username"
            Dim parameters As New Dictionary(Of String, Object) From {{"@username", username}}

            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking username existence", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if username exists for other users (for update validation)
    ''' </summary>
    Private Shared Function UsernameExistsForOtherUser(username As String, userId As Integer) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM users WHERE username = @username AND id != @id"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@username", username},
                {"@id", userId}
            }

            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking username for other users", ex)
            Return False
        End Try
    End Function

#End Region

#Region "Course Management"

    ''' <summary>
    ''' Gets all courses (including archived if specified)
    ''' </summary>
    Public Shared Function GetAllCourses(Optional includeArchived As Boolean = False) As DataTable
        Try
            Dim query As String = "
                SELECT 
                    c.id,
                    c.course_code,
                    c.course_name,
                    c.faculty_id,
                    COALESCE(f.name, 'Unassigned') as faculty_name,
                    c.description,
                    c.credits,
                    c.schedule,
                    c.room,
                    c.status,
                    c.created_at
                FROM courses c
                LEFT JOIN faculty f ON c.faculty_id = f.id"

            If Not includeArchived Then
                query &= " WHERE c.status = 'Active'"
            End If

            query &= " ORDER BY c.course_name"

            Return DatabaseHandler.ExecuteReader(query)
        Catch ex As Exception
            Logger.LogError("Error getting all courses", ex)
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Gets course by ID
    ''' </summary>
    Public Shared Function GetCourseById(courseId As Integer) As Course
        Try
            Return FacultyRepository.GetCourseById(courseId)
        Catch ex As Exception
            Logger.LogError("Error getting course by ID", ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Creates a new course
    ''' </summary>
    Public Shared Function CreateCourse(courseCode As String, courseName As String, facultyId As Integer?, description As String, credits As Integer, schedule As String, room As String) As Boolean
        Try
            ' Check if course code already exists
            If CourseCodeExists(courseCode) Then
                Throw New Exception("Course code already exists")
            End If

            Dim query As String = "
                INSERT INTO courses (course_code, course_name, faculty_id, description, credits, schedule, room, status)
                VALUES (@course_code, @course_name, @faculty_id, @description, @credits, @schedule, @room, 'Active')"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course_code", courseCode},
                {"@course_name", courseName},
                {"@faculty_id", If(facultyId, DBNull.Value)},
                {"@description", description},
                {"@credits", credits},
                {"@schedule", schedule},
                {"@room", room}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Course {courseCode} created successfully")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error creating course", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Updates an existing course
    ''' </summary>
    Public Shared Function UpdateCourse(courseId As Integer, courseCode As String, courseName As String, facultyId As Integer?, description As String, credits As Integer, schedule As String, room As String) As Boolean
        Try
            ' Check if course code exists for other courses
            If CourseCodeExistsForOtherCourse(courseCode, courseId) Then
                Throw New Exception("Course code already exists")
            End If

            Dim query As String = "
                UPDATE courses
                SET course_code = @course_code,
                    course_name = @course_name,
                    faculty_id = @faculty_id,
                    description = @description,
                    credits = @credits,
                    schedule = @schedule,
                    room = @room
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", courseId},
                {"@course_code", courseCode},
                {"@course_name", courseName},
                {"@faculty_id", If(facultyId, DBNull.Value)},
                {"@description", description},
                {"@credits", credits},
                {"@schedule", schedule},
                {"@room", room}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Course {courseCode} updated successfully")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error updating course", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Archives a course (soft delete)
    ''' </summary>
    Public Shared Function ArchiveCourse(courseId As Integer) As Boolean
        Try
            Dim query As String = "
                UPDATE courses
                SET status = 'Archived'
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", courseId}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Course ID {courseId} archived successfully")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error archiving course", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Restores an archived course
    ''' </summary>
    Public Shared Function RestoreCourse(courseId As Integer) As Boolean
        Try
            Dim query As String = "
                UPDATE courses
                SET status = 'Active'
                WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", courseId}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Course ID {courseId} restored successfully")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error restoring course", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Permanently deletes a course
    ''' </summary>
    Public Shared Function DeleteCourse(courseId As Integer) As Boolean
        Try
            Dim query As String = "DELETE FROM courses WHERE id = @id"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id", courseId}
            }

            Dim rowsAffected As Integer = DatabaseHandler.ExecuteNonQuery(query, parameters)

            If rowsAffected > 0 Then
                Logger.LogInfo($"Course ID {courseId} deleted permanently")
                Return True
            End If

            Return False
        Catch ex As Exception
            Logger.LogError("Error deleting course", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if course code exists
    ''' </summary>
    Private Shared Function CourseCodeExists(courseCode As String) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM courses WHERE course_code = @course_code"
            Dim parameters As New Dictionary(Of String, Object) From {{"@course_code", courseCode}}

            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking course code existence", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if course code exists for other courses
    ''' </summary>
    Private Shared Function CourseCodeExistsForOtherCourse(courseCode As String, courseId As Integer) As Boolean
        Try
            Dim query As String = "SELECT COUNT(*) FROM courses WHERE course_code = @course_code AND id != @id"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@course_code", courseCode},
                {"@id", courseId}
            }

            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query, parameters))
            Return count > 0
        Catch ex As Exception
            Logger.LogError("Error checking course code for other courses", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets all faculty members for dropdown
    ''' </summary>
    Public Shared Function GetAllFaculty() As DataTable
        Try
            Dim query As String = "
                SELECT id, faculty_id, name
                FROM faculty
                WHERE status = 'Active'
                ORDER BY name"

            Return DatabaseHandler.ExecuteReader(query)
        Catch ex As Exception
            Logger.LogError("Error getting all faculty", ex)
            Return New DataTable()
        End Try
    End Function

#End Region

#Region "Statistics"

    ''' <summary>
    ''' Gets admin dashboard statistics
    ''' </summary>
    Public Shared Function GetAdminStatistics() As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)

            stats("TotalUsers") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM users WHERE is_archived = 0"))
            stats("ArchivedUsers") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM users WHERE is_archived = 1"))
            stats("TotalCourses") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM courses WHERE status = 'Active'"))
            stats("ArchivedCourses") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM courses WHERE status = 'Archived'"))
            stats("TotalStudents") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM students WHERE status = 'Active'"))
            stats("TotalFaculty") = Convert.ToInt32(DatabaseHandler.ExecuteScalar("SELECT COUNT(*) FROM faculty WHERE status = 'Active'"))

            Return stats
        Catch ex As Exception
            Logger.LogError("Error getting admin statistics", ex)
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

#End Region

End Class