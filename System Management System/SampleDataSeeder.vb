' ==========================================
' FILENAME: /Data/SampleDataSeeder.vb
' PURPOSE: Seeds sample data for testing the system
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Public Class SampleDataSeeder

    ''' <summary>
    ''' Seeds sample data into the database
    ''' </summary>
    Public Shared Sub SeedSampleData()
        Try
            Logger.LogInfo("Starting sample data seeding...")

            ' Seed students
            SeedStudents()

            ' Seed faculty
            SeedFaculty()

            ' Seed courses
            SeedCourses()

            ' Seed enrollments
            SeedEnrollments()

            ' Seed sample attendance
            SeedSampleAttendance()

            Logger.LogInfo("Sample data seeding completed successfully")

        Catch ex As Exception
            Logger.LogError("Error seeding sample data", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Seeds sample students
    ''' </summary>
    Private Shared Sub SeedStudents()
        Try
            ' Check if students already exist
            Dim checkQuery As String = "SELECT COUNT(*) FROM students"
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(checkQuery))

            If count > 0 Then
                Logger.LogInfo("Students already exist, skipping seeding")
                Return
            End If

            Dim students As New List(Of Dictionary(Of String, Object)) From {
                New Dictionary(Of String, Object) From {
                    {"student_id", "2024-0001"},
                    {"name", "Juan Dela Cruz"},
                    {"course", "Bachelor of Science in Computer Science"},
                    {"email", "juan.delacruz@student.edu"},
                    {"phone_number", "09171234567"}
                },
                New Dictionary(Of String, Object) From {
                    {"student_id", "2024-0002"},
                    {"name", "Maria Santos"},
                    {"course", "Bachelor of Science in Information Technology"},
                    {"email", "maria.santos@student.edu"},
                    {"phone_number", "09181234567"}
                },
                New Dictionary(Of String, Object) From {
                    {"student_id", "2024-0003"},
                    {"name", "Pedro Reyes"},
                    {"course", "Bachelor of Science in Computer Science"},
                    {"email", "pedro.reyes@student.edu"},
                    {"phone_number", "09191234567"}
                },
                New Dictionary(Of String, Object) From {
                    {"student_id", "2024-0004"},
                    {"name", "Ana Lim"},
                    {"course", "Bachelor of Science in Information Technology"},
                    {"email", "ana.lim@student.edu"},
                    {"phone_number", "09201234567"}
                },
                New Dictionary(Of String, Object) From {
                    {"student_id", "2024-0005"},
                    {"name", "Carlos Garcia"},
                    {"course", "Bachelor of Science in Computer Science"},
                    {"email", "carlos.garcia@student.edu"},
                    {"phone_number", "09211234567"}
                }
            }

            Dim insertQuery As String = "
                INSERT INTO students (student_id, name, course, email, phone_number, enrollment_date)
                VALUES (@student_id, @name, @course, @email, @phone_number, CURDATE())"

            For Each student In students
                DatabaseHandler.ExecuteNonQuery(insertQuery, student)
            Next

            Logger.LogInfo($"{students.Count} sample students seeded")

        Catch ex As Exception
            Logger.LogError("Error seeding students", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Seeds sample faculty
    ''' </summary>
    Private Shared Sub SeedFaculty()
        Try
            ' Check if faculty already exist (beyond the initial sample)
            Dim checkQuery As String = "SELECT COUNT(*) FROM faculty"
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(checkQuery))

            If count > 1 Then
                Logger.LogInfo("Additional faculty already exist, skipping seeding")
                Return
            End If

            Dim facultyMembers As New List(Of Dictionary(Of String, Object)) From {
                New Dictionary(Of String, Object) From {
                    {"faculty_id", "FAC002"},
                    {"name", "Dr. Maria Santos"},
                    {"department", "Information Technology"},
                    {"email", "maria.santos@school.edu"},
                    {"specialization", "Database Management"}
                },
                New Dictionary(Of String, Object) From {
                    {"faculty_id", "FAC003"},
                    {"name", "Prof. Roberto Cruz"},
                    {"department", "Computer Science"},
                    {"email", "roberto.cruz@school.edu"},
                    {"specialization", "Web Development"}
                }
            }

            Dim insertQuery As String = "
                INSERT INTO faculty (faculty_id, name, department, email, specialization)
                VALUES (@faculty_id, @name, @department, @email, @specialization)"

            For Each faculty In facultyMembers
                DatabaseHandler.ExecuteNonQuery(insertQuery, faculty)
            Next

            Logger.LogInfo($"{facultyMembers.Count} sample faculty seeded")

        Catch ex As Exception
            Logger.LogError("Error seeding faculty", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Seeds sample courses
    ''' </summary>
    Private Shared Sub SeedCourses()
        Try
            ' Check if courses already exist
            Dim checkQuery As String = "SELECT COUNT(*) FROM courses"
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(checkQuery))

            If count > 0 Then
                Logger.LogInfo("Courses already exist, skipping seeding")
                Return
            End If

            ' Get faculty ID from the seeded faculty (FAC001)
            Dim facultyQuery As String = "SELECT id FROM faculty WHERE faculty_id = 'FAC001' LIMIT 1"
            Dim facultyIdObj As Object = DatabaseHandler.ExecuteScalar(facultyQuery)
            Dim facultyId As Integer = If(facultyIdObj IsNot Nothing, Convert.ToInt32(facultyIdObj), 1)

            Dim courses As New List(Of Dictionary(Of String, Object)) From {
                New Dictionary(Of String, Object) From {
                    {"course_code", "CS101"},
                    {"course_name", "Introduction to Programming"},
                    {"faculty_id", facultyId},
                    {"description", "Fundamental concepts of programming using VB.NET"},
                    {"credits", 3},
                    {"schedule", "MWF 9:00-10:00 AM"},
                    {"room", "Room 301"}
                },
                New Dictionary(Of String, Object) From {
                    {"course_code", "CS102"},
                    {"course_name", "Data Structures and Algorithms"},
                    {"faculty_id", facultyId},
                    {"description", "Study of data structures and algorithm design"},
                    {"credits", 3},
                    {"schedule", "TTh 10:30-12:00 PM"},
                    {"room", "Room 302"}
                },
                New Dictionary(Of String, Object) From {
                    {"course_code", "CS103"},
                    {"course_name", "Database Management Systems"},
                    {"faculty_id", facultyId},
                    {"description", "Introduction to database design and SQL"},
                    {"credits", 3},
                    {"schedule", "MWF 1:00-2:00 PM"},
                    {"room", "Lab 201"}
                },
                New Dictionary(Of String, Object) From {
                    {"course_code", "IT101"},
                    {"course_name", "Web Development Fundamentals"},
                    {"faculty_id", facultyId},
                    {"description", "HTML, CSS, JavaScript basics"},
                    {"credits", 3},
                    {"schedule", "TTh 2:00-3:30 PM"},
                    {"room", "Lab 202"}
                }
            }

            Dim insertQuery As String = "
                INSERT INTO courses (course_code, course_name, faculty_id, description, credits, schedule, room)
                VALUES (@course_code, @course_name, @faculty_id, @description, @credits, @schedule, @room)"

            For Each course In courses
                DatabaseHandler.ExecuteNonQuery(insertQuery, course)
            Next

            Logger.LogInfo($"{courses.Count} sample courses seeded")

        Catch ex As Exception
            Logger.LogError("Error seeding courses", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Seeds sample enrollments
    ''' </summary>
    Private Shared Sub SeedEnrollments()
        Try
            ' Check if enrollments already exist
            Dim checkQuery As String = "SELECT COUNT(*) FROM enrollments"
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(checkQuery))

            If count > 0 Then
                Logger.LogInfo("Enrollments already exist, skipping seeding")
                Return
            End If

            ' Get course IDs
            Dim coursesQuery As String = "SELECT id FROM courses ORDER BY id LIMIT 4"
            Dim coursesDt As DataTable = DatabaseHandler.ExecuteReader(coursesQuery)

            If coursesDt.Rows.Count = 0 Then
                Logger.LogWarning("No courses found for enrollment seeding")
                Return
            End If

            ' Get student IDs
            Dim studentsQuery As String = "SELECT student_id FROM students"
            Dim studentsDt As DataTable = DatabaseHandler.ExecuteReader(studentsQuery)

            If studentsDt.Rows.Count = 0 Then
                Logger.LogWarning("No students found for enrollment seeding")
                Return
            End If

            Dim insertQuery As String = "
                INSERT INTO enrollments (student_id, course_id, enrollment_date, status)
                VALUES (@student_id, @course_id, CURDATE(), 'Enrolled')"

            ' Enroll each student in first 2 courses
            For Each studentRow As DataRow In studentsDt.Rows
                For i As Integer = 0 To Math.Min(1, coursesDt.Rows.Count - 1)
                    Dim params As New Dictionary(Of String, Object) From {
                        {"@student_id", studentRow("student_id").ToString()},
                        {"@course_id", Convert.ToInt32(coursesDt.Rows(i)("id"))}
                    }

                    DatabaseHandler.ExecuteNonQuery(insertQuery, params)
                Next
            Next

            Logger.LogInfo("Sample enrollments seeded")

        Catch ex As Exception
            Logger.LogError("Error seeding enrollments", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Seeds sample attendance records
    ''' </summary>
    Private Shared Sub SeedSampleAttendance()
        Try
            ' Check if attendance already exists
            Dim checkQuery As String = "SELECT COUNT(*) FROM attendance"
            Dim count As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(checkQuery))

            If count > 0 Then
                Logger.LogInfo("Attendance records already exist, skipping seeding")
                Return
            End If

            ' Get first course
            Dim courseQuery As String = "SELECT id FROM courses ORDER BY id LIMIT 1"
            Dim courseIdObj As Object = DatabaseHandler.ExecuteScalar(courseQuery)

            If courseIdObj Is Nothing Then
                Logger.LogWarning("No courses found for attendance seeding")
                Return
            End If

            Dim courseId As Integer = Convert.ToInt32(courseIdObj)

            ' Get students
            Dim studentsQuery As String = "SELECT student_id FROM students LIMIT 3"
            Dim studentsDt As DataTable = DatabaseHandler.ExecuteReader(studentsQuery)

            If studentsDt.Rows.Count = 0 Then
                Logger.LogWarning("No students found for attendance seeding")
                Return
            End If

            Dim insertQuery As String = "
                INSERT INTO attendance (student_id, course_id, date, status, time_in, recorded_by)
                VALUES (@student_id, @course_id, @date, @status, @time_in, 'admin')"

            Dim statuses As String() = {"Present", "Present", "Late", "Absent"}
            Dim rand As New Random()

            ' Create attendance for last 5 days
            For dayOffset As Integer = 4 To 0 Step -1
                Dim attendanceDate As DateTime = DateTime.Today.AddDays(-dayOffset)

                For Each studentRow As DataRow In studentsDt.Rows
                    Dim status As String = statuses(rand.Next(statuses.Length))

                    Dim params As New Dictionary(Of String, Object) From {
                        {"@student_id", studentRow("student_id").ToString()},
                        {"@course_id", courseId},
                        {"@date", attendanceDate.ToString("yyyy-MM-dd")},
                        {"@status", status},
                        {"@time_in", "09:00:00"}
                    }

                    DatabaseHandler.ExecuteNonQuery(insertQuery, params)
                Next
            Next

            Logger.LogInfo("Sample attendance records seeded")

        Catch ex As Exception
            Logger.LogError("Error seeding sample attendance", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Clears all sample data (for testing)
    ''' </summary>
    Public Shared Sub ClearSampleData()
        Try
            Logger.LogWarning("Clearing sample data...")

            DatabaseHandler.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 0")
            DatabaseHandler.ExecuteNonQuery("DELETE FROM attendance")
            DatabaseHandler.ExecuteNonQuery("DELETE FROM enrollments")
            DatabaseHandler.ExecuteNonQuery("DELETE FROM courses")
            DatabaseHandler.ExecuteNonQuery("DELETE FROM students WHERE student_id LIKE '2024-%'")
            DatabaseHandler.ExecuteNonQuery("DELETE FROM faculty WHERE faculty_id != 'FAC001'")
            DatabaseHandler.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 1")

            Logger.LogInfo("Sample data cleared successfully")

        Catch ex As Exception
            Logger.LogError("Error clearing sample data", ex)
            Throw
        End Try
    End Sub

End Class