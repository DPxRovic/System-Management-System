' ==========================================
' FILENAME: /Data/DatabaseInitializer.vb
' PURPOSE: Checks if DB/tables exist and auto-creates them with seed data
' AUTHOR: System
' DATE: 2025-10-14
' Edited By Rovic
' For Future users please do not remove this header
' ==========================================

Imports MySql.Data.MySqlClient

Public Class DatabaseInitializer

    ''' <summary>
    ''' Initializes the database and creates tables if they don't exist
    ''' </summary>
    ''' <returns>True if initialization successful</returns>
    Public Shared Function Initialize() As Boolean
        Try
            Logger.LogInfo("Starting database initialization...")

            ' Check if database exists, create if not
            If Not DatabaseHandler.DatabaseExists(DatabaseManager.DatabaseName) Then
                Logger.LogInfo($"Database {DatabaseManager.DatabaseName} does not exist. Creating...")
                CreateDatabase()
            Else
                Logger.LogInfo($"Database {DatabaseManager.DatabaseName} already exists.")
            End If

            ' Test connection
            If Not DatabaseManager.TestConnection() Then
                Throw New Exception("Database connection test failed after initialization")
            End If

            ' Create tables if they don't exist
            CreateTables()

            ' Seed initial data
            SeedInitialData()

            Logger.LogInfo("Database initialization completed successfully.")
            Return True

        Catch ex As Exception
            Logger.LogError("Database initialization failed", ex)
            MessageBox.Show($"Database initialization failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Creates the database
    ''' </summary>
    Private Shared Sub CreateDatabase()
        Try
            Using conn As MySqlConnection = DatabaseManager.GetConnectionWithoutDatabase()
                conn.Open()

                Dim query As String = $"CREATE DATABASE IF NOT EXISTS `{DatabaseManager.DatabaseName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci"

                Using cmd As New MySqlCommand(query, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Logger.LogInfo($"Database {DatabaseManager.DatabaseName} created successfully.")
        Catch ex As Exception
            Logger.LogError("Failed to create database", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Creates all required tables
    ''' </summary>
    Private Shared Sub CreateTables()
        Try
            ' Create users table
            If Not DatabaseHandler.TableExists("users") Then
                Dim createUsersTable As String = "
                CREATE TABLE `users` (
                    `id` INT AUTO_INCREMENT PRIMARY KEY,
                    `username` VARCHAR(50) NOT NULL UNIQUE,
                    `password` VARCHAR(255) NOT NULL,
                    `role` VARCHAR(20) NOT NULL,
                    `fullname` VARCHAR(100) NOT NULL,
                    `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    INDEX idx_username (username),
                    INDEX idx_role (role)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

                DatabaseHandler.ExecuteNonQuery(createUsersTable)
                Logger.LogInfo("Users table created successfully.")
            End If

            ' Create students table
            If Not DatabaseHandler.TableExists("students") Then
                Dim createStudentsTable As String = "
                CREATE TABLE `students` (
                    `id` INT AUTO_INCREMENT PRIMARY KEY,
                    `student_id` VARCHAR(20) NOT NULL UNIQUE,
                    `name` VARCHAR(100) NOT NULL,
                    `course` VARCHAR(100) NOT NULL,
                    `email` VARCHAR(100),
                    `phone_number` VARCHAR(20),
                    `date_of_birth` DATE,
                    `enrollment_date` DATE DEFAULT (CURRENT_DATE),
                    `status` VARCHAR(20) DEFAULT 'Active',
                    `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    INDEX idx_student_id (student_id),
                    INDEX idx_course (course)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

                DatabaseHandler.ExecuteNonQuery(createStudentsTable)
                Logger.LogInfo("Students table created successfully.")
            End If

            ' Create courses table
            If Not DatabaseHandler.TableExists("courses") Then
                Dim createCoursesTable As String = "
                CREATE TABLE `courses` (
                    `id` INT AUTO_INCREMENT PRIMARY KEY,
                    `course_code` VARCHAR(20) NOT NULL UNIQUE,
                    `course_name` VARCHAR(100) NOT NULL,
                    `faculty_id` INT,
                    `description` TEXT,
                    `credits` INT DEFAULT 3,
                    `schedule` VARCHAR(100),
                    `room` VARCHAR(50),
                    `status` VARCHAR(20) DEFAULT 'Active',
                    `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    INDEX idx_course_code (course_code),
                    INDEX idx_faculty_id (faculty_id)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

                DatabaseHandler.ExecuteNonQuery(createCoursesTable)
                Logger.LogInfo("Courses table created successfully.")
            End If

            ' Create attendance table
            If Not DatabaseHandler.TableExists("attendance") Then
                Dim createAttendanceTable As String = "
                CREATE TABLE `attendance` (
                    `id` INT AUTO_INCREMENT PRIMARY KEY,
                    `student_id` VARCHAR(20) NOT NULL,
                    `course_id` INT NOT NULL,
                    `date` DATE NOT NULL,
                    `status` VARCHAR(20) NOT NULL,
                    `time_in` TIME,
                    `time_out` TIME,
                    `remarks` TEXT,
                    `recorded_by` VARCHAR(50),
                    `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    INDEX idx_student_id (student_id),
                    INDEX idx_course_id (course_id),
                    INDEX idx_date (date),
                    INDEX idx_composite (student_id, course_id, date),
                    FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

                DatabaseHandler.ExecuteNonQuery(createAttendanceTable)
                Logger.LogInfo("Attendance table created successfully.")
            End If

            ' Create faculty table
            If Not DatabaseHandler.TableExists("faculty") Then
                Dim createFacultyTable As String = "
                CREATE TABLE `faculty` (
                    `id` INT AUTO_INCREMENT PRIMARY KEY,
                    `faculty_id` VARCHAR(20) NOT NULL UNIQUE,
                    `name` VARCHAR(100) NOT NULL,
                    `department` VARCHAR(100),
                    `email` VARCHAR(100),
                    `phone_number` VARCHAR(20),
                    `specialization` VARCHAR(100),
                    `hire_date` DATE DEFAULT (CURRENT_DATE),
                    `status` VARCHAR(20) DEFAULT 'Active',
                    `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    INDEX idx_faculty_id (faculty_id),
                    INDEX idx_department (department)
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

                DatabaseHandler.ExecuteNonQuery(createFacultyTable)
                Logger.LogInfo("Faculty table created successfully.")
            End If

            ' Create enrollments table
            If Not DatabaseHandler.TableExists("enrollments") Then
                Dim createEnrollmentsTable As String = "
                CREATE TABLE `enrollments` (
                    `id` INT AUTO_INCREMENT PRIMARY KEY,
                    `student_id` VARCHAR(20) NOT NULL,
                    `course_id` INT NOT NULL,
                    `enrollment_date` DATE DEFAULT (CURRENT_DATE),
                    `status` VARCHAR(20) DEFAULT 'Enrolled',
                    `grade` VARCHAR(5),
                    `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                    INDEX idx_student_id (student_id),
                    INDEX idx_course_id (course_id),
                    UNIQUE KEY unique_enrollment (student_id, course_id),
                    FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE
                ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

                DatabaseHandler.ExecuteNonQuery(createEnrollmentsTable)
                Logger.LogInfo("Enrollments table created successfully.")
            End If

        Catch ex As Exception
            Logger.LogError("Failed to create tables", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Seeds initial data including admin user
    ''' </summary>
    Private Shared Sub SeedInitialData()
        Try
            ' Check if users table is empty
            Dim query As String = "SELECT COUNT(*) FROM users"
            Dim userCount As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query))

            If userCount = 0 Then
                Logger.LogInfo("Users table is empty. Seeding initial admin user...")

                ' Create default admin user
                ' Username: admin, Password: admin123
                Dim insertAdmin As String = "
                INSERT INTO users (username, password, role, fullname) 
                VALUES (@username, @password, @role, @fullname)"

                Dim parameters As New Dictionary(Of String, Object) From {
                    {"@username", "admin"},
                    {"@password", "admin123"},
                    {"@role", "Admin"},
                    {"@fullname", "System Administrator"}
                }

                DatabaseHandler.ExecuteNonQuery(insertAdmin, parameters)
                Logger.LogInfo("Default admin user created successfully (username: admin, password: admin123)")

                ' Create sample SuperAdmin user
                Dim insertSuperAdmin As String = "
                INSERT INTO users (username, password, role, fullname) 
                VALUES (@username, @password, @role, @fullname)"

                Dim superAdminParams As New Dictionary(Of String, Object) From {
                    {"@username", "superadmin"},
                    {"@password", "super123"},
                    {"@role", "SuperAdmin"},
                    {"@fullname", "Super Administrator"}
                }

                DatabaseHandler.ExecuteNonQuery(insertSuperAdmin, superAdminParams)
                Logger.LogInfo("Default superadmin user created successfully (username: superadmin, password: super123)")
            End If

            ' Check if faculty table is empty and seed sample data
            Dim facultyQuery As String = "SELECT COUNT(*) FROM faculty"
            Dim facultyCount As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(facultyQuery))

            If facultyCount = 0 Then
                Logger.LogInfo("Faculty table is empty. Seeding sample faculty...")

                Dim insertFaculty As String = "
                INSERT INTO faculty (faculty_id, name, department, email, specialization) 
                VALUES (@faculty_id, @name, @department, @email, @specialization)"

                Dim facultyParams As New Dictionary(Of String, Object) From {
                    {"@faculty_id", "FAC001"},
                    {"@name", "Dr. Juan Dela Cruz"},
                    {"@department", "Computer Science"},
                    {"@email", "juan.delacruz@school.edu"},
                    {"@specialization", "Software Engineering"}
                }

                DatabaseHandler.ExecuteNonQuery(insertFaculty, facultyParams)
                Logger.LogInfo("Sample faculty created successfully")
            End If

        Catch ex As Exception
            Logger.LogError("Failed to seed initial data", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Drops all tables (use with caution)
    ''' </summary>
    Public Shared Sub DropAllTables()
        Try
            Logger.LogWarning("Dropping all tables...")

            DatabaseHandler.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 0")
            DatabaseHandler.ExecuteNonQuery("DROP TABLE IF EXISTS enrollments")
            DatabaseHandler.ExecuteNonQuery("DROP TABLE IF EXISTS attendance")
            DatabaseHandler.ExecuteNonQuery("DROP TABLE IF EXISTS courses")
            DatabaseHandler.ExecuteNonQuery("DROP TABLE IF EXISTS students")
            DatabaseHandler.ExecuteNonQuery("DROP TABLE IF EXISTS faculty")
            DatabaseHandler.ExecuteNonQuery("DROP TABLE IF EXISTS users")
            DatabaseHandler.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 1")

            Logger.LogInfo("All tables dropped successfully.")
        Catch ex As Exception
            Logger.LogError("Failed to drop tables", ex)
            Throw
        End Try
    End Sub

End Class