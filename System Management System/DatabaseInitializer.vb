' ==========================================
' FILENAME: /Data/DatabaseInitializer.vb
' PURPOSE: Enhanced database initialization with comprehensive error handling
' AUTHOR: System
' DATE: 2025-10-14
' LAST UPDATED: 2025-11-06 - Added professor_subjects, sections, professor_sections tables and enhanced relationship tracking
' Updated By: rainnn19
' For Future users please do not remove this header
' ==========================================

Imports MySql.Data.MySqlClient

Public Class DatabaseInitializer

#Region "Initialization (ENHANCED - Preserves Original Signature)"

    ''' <summary>
    ''' Initializes the database and creates tables if they don't exist
    ''' </summary>
    ''' <returns>True if initialization successful</returns>
    Public Shared Function Initialize() As Boolean
        Dim initContext As New ErrorHandler.ErrorContext With {
            .Source = "DatabaseInitializer",
            .Operation = "Initialize",
            .Timestamp = DateTime.Now
        }

        Try
            Logger.LogInfo("Starting database initialization...")

            ' NEW: Pre-initialization validation
            If Not ValidateEnvironment() Then
                Logger.LogError("Environment validation failed")
                Return False
            End If

            ' Check if database exists, create if not
            If Not DatabaseHandler.DatabaseExists(DatabaseManager.DatabaseName) Then
                Logger.LogInfo($"Database {DatabaseManager.DatabaseName} does not exist. Creating...")

                If Not CreateDatabase() Then
                    Logger.LogError("Failed to create database")
                    Return False
                End If
            Else
                Logger.LogInfo($"Database {DatabaseManager.DatabaseName} already exists.")
            End If

            ' Test connection
            If Not DatabaseManager.TestConnection() Then
                Dim shouldRetry As Boolean = ErrorHandler.HandleDatabaseError(
                    "Unable to connect to the database after initialization.",
                    New Exception("Connection test failed"),
                    initContext)

                If shouldRetry Then
                    ' Give it one more try
                    Threading.Thread.Sleep(1000)
                    If Not DatabaseManager.TestConnection() Then
                        Throw New Exception("Database connection test failed after retry")
                    End If
                Else
                    Return False
                End If
            End If

            ' Create tables if they don't exist
            If Not CreateTables() Then
                Logger.LogError("Failed to create tables")
                Return False
            End If

            ' Seed initial data
            If Not SeedInitialData() Then
                Logger.LogWarning("Failed to seed initial data, but initialization can continue")
                ' Not critical - allow continuation
            End If

            ' NEW: Verify database integrity
            If Not VerifyDatabaseIntegrity() Then
                Logger.LogWarning("Database integrity check found issues, but initialization completed")
            End If

            Logger.LogInfo("Database initialization completed successfully.")
            Return True

        Catch ex As Exception
            Logger.LogCritical("Database initialization failed critically", ex)
            ErrorHandler.Handle("Database initialization failed. The application may not function correctly.", ex, ErrorHandler.ErrorSeverity.Critical, initContext)
            Return False
        End Try
    End Function

#End Region

#Region "Database Creation (ENHANCED)"

    ''' <summary>
    ''' Creates the database with error handling
    ''' </summary>
    Private Shared Function CreateDatabase() As Boolean
        Dim attempts As Integer = 0
        Const maxAttempts As Integer = 3

        While attempts < maxAttempts
            Try
                Using conn As MySqlConnection = DatabaseManager.GetConnectionWithoutDatabase()
                    conn.Open()

                    Dim query As String = $"CREATE DATABASE IF NOT EXISTS `{DatabaseManager.DatabaseName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci"

                    Using cmd As New MySqlCommand(query, conn)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                Logger.LogInfo($"Database {DatabaseManager.DatabaseName} created successfully.")
                Return True

            Catch ex As MySqlException When attempts < maxAttempts - 1
                attempts += 1
                Logger.LogWarning($"Database creation attempt {attempts} failed, retrying: {ex.Message}")
                Threading.Thread.Sleep(1000 * attempts) ' Exponential backoff

            Catch ex As Exception
                Logger.LogError("Failed to create database", ex)

                If attempts < maxAttempts - 1 Then
                    attempts += 1
                    Logger.LogWarning($"Retrying database creation (attempt {attempts + 1}/{maxAttempts})")
                    Threading.Thread.Sleep(1000 * attempts)
                Else
                    ErrorHandler.Handle(
                        "Failed to create the database after multiple attempts. Please check your database server configuration and permissions.",
                        ex,
                        ErrorHandler.ErrorSeverity.Critical)
                    Return False
                End If
            End Try
        End While

        Return False
    End Function

#End Region

#Region "Table Creation (ENHANCED)"

    ''' <summary>
    ''' Creates all required tables with enhanced error handling
    ''' </summary>
    Private Shared Function CreateTables() As Boolean
        Dim tablesCreated As New List(Of String)
        Dim tableDefinitions As New Dictionary(Of String, String)

        Try
            ' Define all table creation queries
            DefineTableStructures(tableDefinitions)

            ' Create each table
            For Each tableDef In tableDefinitions
                Dim tableName As String = tableDef.Key
                Dim createQuery As String = tableDef.Value

                Try
                    If Not DatabaseHandler.TableExists(tableName) Then
                        Logger.LogInfo($"Creating table: {tableName}")

                        DatabaseHandler.ExecuteNonQuery(createQuery)
                        tablesCreated.Add(tableName)

                        Logger.LogInfo($"Table {tableName} created successfully.")
                    Else
                        Logger.LogInfo($"Table {tableName} already exists.")

                        ' NEW: Check and add missing columns if table exists
                        UpdateTableStructure(tableName)
                    End If

                Catch ex As Exception
                    Logger.LogError($"Failed to create table {tableName}", ex)
                    ErrorHandler.Handle($"Failed to create table '{tableName}'. Database initialization may be incomplete.", ex, ErrorHandler.ErrorSeverity.Error)

                    ' NEW: Attempt rollback of created tables
                    If tablesCreated.Count > 0 Then
                        Logger.LogWarning("Attempting to rollback created tables...")
                        RollbackTableCreation(tablesCreated)
                    End If

                    Return False
                End Try
            Next

            Return True

        Catch ex As Exception
            Logger.LogError("Failed to create tables", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Defines all table structures
    ''' </summary>
    Private Shared Sub DefineTableStructures(tableDefinitions As Dictionary(Of String, String))
        ' Users table
        tableDefinitions("users") = "
            CREATE TABLE `users` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `username` VARCHAR(50) NOT NULL UNIQUE,
                `password` VARCHAR(255) NOT NULL,
                `role` VARCHAR(20) NOT NULL,
                `fullname` VARCHAR(100) NOT NULL,
                `is_archived` TINYINT(1) DEFAULT 0,
                `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                INDEX idx_username (username),
                INDEX idx_role (role),
                INDEX idx_archived (is_archived)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Students table
        tableDefinitions("students") = "
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
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                INDEX idx_student_id (student_id),
                INDEX idx_course (course),
                INDEX idx_status (status)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Faculty table
        tableDefinitions("faculty") = "
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
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                INDEX idx_faculty_id (faculty_id),
                INDEX idx_department (department),
                INDEX idx_status (status)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Courses table
        tableDefinitions("courses") = "
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
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                INDEX idx_course_code (course_code),
                INDEX idx_faculty_id (faculty_id),
                INDEX idx_status (status),
                FOREIGN KEY (faculty_id) REFERENCES faculty(id) ON DELETE SET NULL
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Enrollments table (enhanced with subject_code and professor_id)
        tableDefinitions("enrollments") = "
            CREATE TABLE `enrollments` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `student_id` VARCHAR(20) NOT NULL,
                `course_id` INT NOT NULL,
                `subject_code` VARCHAR(20),
                `professor_id` INT,
                `enrollment_date` DATE DEFAULT (CURRENT_DATE),
                `status` VARCHAR(20) DEFAULT 'Enrolled',
                `grade` VARCHAR(5),
                `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                INDEX idx_student_id (student_id),
                INDEX idx_course_id (course_id),
                INDEX idx_subject_code (subject_code),
                INDEX idx_professor_id (professor_id),
                INDEX idx_status (status),
                UNIQUE KEY unique_enrollment (student_id, course_id),
                FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Attendance table
        tableDefinitions("attendance") = "
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
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                INDEX idx_student_id (student_id),
                INDEX idx_course_id (course_id),
                INDEX idx_date (date),
                INDEX idx_status (status),
                INDEX idx_composite (student_id, course_id, date),
                FOREIGN KEY (course_id) REFERENCES courses(id) ON DELETE CASCADE
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Professors table
        tableDefinitions("professors") = "
            CREATE TABLE `professors` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `professor_id` VARCHAR(20) NOT NULL UNIQUE,
                `name` VARCHAR(100) NOT NULL,
                `department` VARCHAR(100),
                `email` VARCHAR(100),
                `phone_number` VARCHAR(20),
                `subject_code` VARCHAR(20),
                `specialization` VARCHAR(100),
                `hire_date` DATE DEFAULT (CURRENT_DATE),
                `status` VARCHAR(20) DEFAULT 'Active',
                `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                `created_by` VARCHAR(50) DEFAULT 'rainnn19',
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                INDEX idx_professor_id (professor_id),
                INDEX idx_department (department),
                INDEX idx_subject_code (subject_code),
                INDEX idx_status (status)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Professor subjects junction table
        tableDefinitions("professor_subjects") = "
            CREATE TABLE `professor_subjects` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `professor_id` INT NOT NULL,
                `subject_code` VARCHAR(20) NOT NULL,
                `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                `created_by` VARCHAR(50) DEFAULT 'rainnn19',
                FOREIGN KEY (professor_id) REFERENCES professors(id) ON DELETE CASCADE,
                UNIQUE KEY unique_professor_subject (professor_id, subject_code),
                INDEX idx_subject_code (subject_code)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Sections table
        tableDefinitions("sections") = "
            CREATE TABLE `sections` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `section_code` VARCHAR(20) NOT NULL UNIQUE,
                `section_name` VARCHAR(100) NOT NULL,
                `subject_code` VARCHAR(20) NOT NULL,
                `capacity` INT DEFAULT 30,
                `year_level` VARCHAR(20),
                `semester` VARCHAR(20),
                `academic_year` VARCHAR(20),
                `status` VARCHAR(20) DEFAULT 'Active',
                `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                `created_by` VARCHAR(50) DEFAULT 'rainnn19',
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                INDEX idx_section_code (section_code),
                INDEX idx_subject_code (subject_code),
                INDEX idx_year_level (year_level),
                INDEX idx_status (status)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"

        ' Professor sections junction table
        tableDefinitions("professor_sections") = "
            CREATE TABLE `professor_sections` (
                `id` INT AUTO_INCREMENT PRIMARY KEY,
                `professor_id` INT NOT NULL,
                `section_id` INT NOT NULL,
                `subject_code` VARCHAR(20) NOT NULL,
                `assigned_date` DATE DEFAULT (CURRENT_DATE),
                `status` VARCHAR(20) DEFAULT 'Active',
                `created_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                `created_by` VARCHAR(50) DEFAULT 'rainnn19',
                `updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
                FOREIGN KEY (professor_id) REFERENCES professors(id) ON DELETE CASCADE,
                FOREIGN KEY (section_id) REFERENCES sections(id) ON DELETE CASCADE,
                UNIQUE KEY unique_professor_section (professor_id, section_id, subject_code),
                INDEX idx_professor_id (professor_id),
                INDEX idx_section_id (section_id),
                INDEX idx_subject_code (subject_code)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4"
    End Sub

    ''' <summary>
    ''' NEW: Updates table structure if columns are missing
    ''' </summary>
    Private Shared Sub UpdateTableStructure(tableName As String)
        Try
            Select Case tableName.ToLower()
                Case "users"
                    ' Check and add is_archived column
                    AddColumnIfNotExists("users", "is_archived", "TINYINT(1) DEFAULT 0")
                    AddColumnIfNotExists("users", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")

                Case "students"
                    AddColumnIfNotExists("students", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")

                Case "faculty"
                    AddColumnIfNotExists("faculty", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")

                Case "courses"
                    AddColumnIfNotExists("courses", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")

                Case "enrollments"
                    AddColumnIfNotExists("enrollments", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                    ' Migration support: Add new columns for enhanced relationship tracking
                    AddColumnIfNotExists("enrollments", "subject_code", "VARCHAR(20)")
                    AddColumnIfNotExists("enrollments", "professor_id", "INT")
                    ' Add indexes if columns were just added
                    AddIndexIfNotExists("enrollments", "idx_subject_code", "subject_code")
                    AddIndexIfNotExists("enrollments", "idx_professor_id", "professor_id")

                Case "attendance"
                    AddColumnIfNotExists("attendance", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")

                Case "professors"
                    ' Migration support for professors table if it exists
                    AddColumnIfNotExists("professors", "subject_code", "VARCHAR(20)")
                    AddColumnIfNotExists("professors", "created_by", "VARCHAR(50) DEFAULT 'rainnn19'")
                    AddColumnIfNotExists("professors", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                    AddIndexIfNotExists("professors", "idx_subject_code", "subject_code")

                Case "sections"
                    ' Migration support for sections table if it exists
                    AddColumnIfNotExists("sections", "subject_code", "VARCHAR(20) NOT NULL DEFAULT ''")
                    AddColumnIfNotExists("sections", "capacity", "INT DEFAULT 30")
                    AddColumnIfNotExists("sections", "created_by", "VARCHAR(50) DEFAULT 'rainnn19'")
                    AddColumnIfNotExists("sections", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                    AddIndexIfNotExists("sections", "idx_subject_code", "subject_code")

                Case "professor_sections"
                    ' Migration support for professor_sections table if it exists
                    AddColumnIfNotExists("professor_sections", "subject_code", "VARCHAR(20) NOT NULL DEFAULT ''")
                    AddColumnIfNotExists("professor_sections", "created_by", "VARCHAR(50) DEFAULT 'rainnn19'")
                    AddColumnIfNotExists("professor_sections", "updated_at", "TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                    AddIndexIfNotExists("professor_sections", "idx_subject_code", "subject_code")

                Case "professor_subjects"
                    ' Migration support for professor_subjects table if it exists
                    AddColumnIfNotExists("professor_subjects", "created_by", "VARCHAR(50) DEFAULT 'rainnn19'")
            End Select

            Logger.LogInfo($"Table structure update completed for {tableName}")

        Catch ex As Exception
            Logger.LogWarning($"Error updating table structure for {tableName}: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Adds a column to a table if it doesn't exist
    ''' </summary>
    Private Shared Sub AddColumnIfNotExists(tableName As String, columnName As String, columnDefinition As String)
        Try
            Dim checkQuery As String = "
                SELECT COUNT(*) 
                FROM information_schema.columns 
                WHERE table_schema = @database 
                AND table_name = @table 
                AND column_name = @column"

            Dim checkParams As New Dictionary(Of String, Object) From {
                {"@database", DatabaseManager.DatabaseName},
                {"@table", tableName},
                {"@column", columnName}
            }

            Dim columnExists As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(checkQuery, checkParams))

            If columnExists = 0 Then
                Dim alterQuery As String = $"ALTER TABLE `{tableName}` ADD COLUMN `{columnName}` {columnDefinition}"
                DatabaseHandler.ExecuteNonQuery(alterQuery)
                Logger.LogInfo($"Added column '{columnName}' to table '{tableName}'")
            End If

        Catch ex As Exception
            Logger.LogWarning($"Failed to add column '{columnName}' to table '{tableName}': {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Adds an index to a table if it doesn't exist
    ''' </summary>
    Private Shared Sub AddIndexIfNotExists(tableName As String, indexName As String, columnName As String)
        Try
            Dim checkQuery As String = "
                SELECT COUNT(*) 
                FROM information_schema.statistics 
                WHERE table_schema = @database 
                AND table_name = @table 
                AND index_name = @index"

            Dim checkParams As New Dictionary(Of String, Object) From {
                {"@database", DatabaseManager.DatabaseName},
                {"@table", tableName},
                {"@index", indexName}
            }

            Dim indexExists As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(checkQuery, checkParams))

            If indexExists = 0 Then
                Dim alterQuery As String = $"ALTER TABLE `{tableName}` ADD INDEX `{indexName}` (`{columnName}`)"
                DatabaseHandler.ExecuteNonQuery(alterQuery)
                Logger.LogInfo($"Added index '{indexName}' to table '{tableName}'")
            End If

        Catch ex As Exception
            Logger.LogWarning($"Failed to add index '{indexName}' to table '{tableName}': {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' NEW: Rolls back table creation on error
    ''' </summary>
    Private Shared Sub RollbackTableCreation(tablesToDrop As List(Of String))
        Try
            Logger.LogWarning($"Rolling back {tablesToDrop.Count} tables...")

            DatabaseHandler.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 0")

            For Each tableName In tablesToDrop
                Try
                    DatabaseHandler.ExecuteNonQuery($"DROP TABLE IF EXISTS `{tableName}`")
                    Logger.LogInfo($"Dropped table: {tableName}")
                Catch ex As Exception
                    Logger.LogWarning($"Failed to drop table {tableName} during rollback: {ex.Message}")
                End Try
            Next

            DatabaseHandler.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 1")

            Logger.LogInfo("Table rollback completed")

        Catch ex As Exception
            Logger.LogError("Error during table rollback", ex)
        End Try
    End Sub

#End Region

#Region "Data Seeding (ENHANCED)"

    ''' <summary>
    ''' Seeds initial data including admin user with enhanced error handling
    ''' </summary>
    Private Shared Function SeedInitialData() As Boolean
        Try
            ' Check if users table is empty
            Dim query As String = "SELECT COUNT(*) FROM users"
            Dim userCount As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(query))

            If userCount = 0 Then
                Logger.LogInfo("Users table is empty. Seeding initial admin users...")

                If Not SeedAdminUsers() Then
                    Logger.LogWarning("Failed to seed admin users")
                    Return False
                End If
            End If

            ' Check if faculty table is empty and seed sample data
            Dim facultyQuery As String = "SELECT COUNT(*) FROM faculty"
            Dim facultyCount As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(facultyQuery))

            If facultyCount = 0 Then
                Logger.LogInfo("Faculty table is empty. Seeding sample faculty...")

                If Not SeedSampleFaculty() Then
                    Logger.LogWarning("Failed to seed sample faculty")
                    ' Not critical, continue
                End If
            End If

            Return True

        Catch ex As Exception
            Logger.LogError("Failed to seed initial data", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' NEW: Seeds admin users with validation
    ''' </summary>
    Private Shared Function SeedAdminUsers() As Boolean
        Try
            Dim adminUsers As New List(Of Tuple(Of String, String, String, String)) From {
                Tuple.Create("admin", "admin123", "Admin", "System Administrator"),
                Tuple.Create("superadmin", "super123", "SuperAdmin", "Super Administrator")
            }

            For Each adminUser In adminUsers
                Try
                    Dim insertQuery As String = "
                        INSERT INTO users (username, password, role, fullname) 
                        VALUES (@username, @password, @role, @fullname)"

                    Dim parameters As New Dictionary(Of String, Object) From {
                        {"@username", adminUser.Item1},
                        {"@password", adminUser.Item2},
                        {"@role", adminUser.Item3},
                        {"@fullname", adminUser.Item4}
                    }

                    DatabaseHandler.ExecuteNonQuery(insertQuery, parameters)
                    Logger.LogInfo($"Created user: {adminUser.Item1} (Role: {adminUser.Item3})")

                Catch ex As Exception
                    Logger.LogWarning($"Failed to create user {adminUser.Item1}: {ex.Message}")
                    ' Continue with other users
                End Try
            Next

            Return True

        Catch ex As Exception
            Logger.LogError("Failed to seed admin users", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' NEW: Seeds sample faculty data
    ''' </summary>
    Private Shared Function SeedSampleFaculty() As Boolean
        Try
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

            Return True

        Catch ex As Exception
            Logger.LogWarning($"Failed to seed sample faculty: {ex.Message}")
            Return False
        End Try
    End Function

#End Region

#Region "Validation and Verification"

    ''' <summary>
    ''' NEW: Validates environment before initialization
    ''' </summary>
    Private Shared Function ValidateEnvironment() As Boolean
        Try
            ' Check if connection string is valid
            If Not DatabaseManager.ValidateConnectionString() Then
                Logger.LogError("Invalid connection string")
                Return False
            End If

            ' Check if MySQL server is accessible
            If Not DatabaseManager.IsConnectionHealthy() Then
                Logger.LogWarning("Database server health check failed, attempting connection...")
                If Not DatabaseManager.TestConnection() Then
                    Logger.LogError("Cannot connect to MySQL server")
                    Return False
                End If
            End If

            Return True

        Catch ex As Exception
            Logger.LogError("Environment validation failed", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' NEW: Verifies database integrity after initialization
    ''' </summary>
    Private Shared Function VerifyDatabaseIntegrity() As Boolean
        Dim allValid As Boolean = True

        Try
            Logger.LogInfo("Verifying database integrity...")

            Dim requiredTables As List(Of String) = New List(Of String) From {
                "users", "students", "faculty", "courses", "enrollments", "attendance",
                "professors", "professor_subjects", "sections", "professor_sections"
            }

            ' Check all required tables exist
            For Each tableName In requiredTables
                If Not DatabaseHandler.TableExists(tableName) Then
                    Logger.LogWarning($"Required table missing: {tableName}")
                    allValid = False
                End If
            Next

            ' Verify admin user exists
            Dim adminQuery As String = "SELECT COUNT(*) FROM users WHERE role IN ('Admin', 'SuperAdmin')"
            Dim adminCount As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(adminQuery))

            If adminCount = 0 Then
                Logger.LogWarning("No admin users found in database")
                allValid = False
            End If

            ' Verify new table relationships
            If DatabaseHandler.TableExists("professor_subjects") And DatabaseHandler.TableExists("professors") Then
                Dim fkQuery As String = "
                    SELECT COUNT(*) 
                    FROM information_schema.table_constraints 
                    WHERE table_schema = @database 
                    AND table_name = 'professor_subjects' 
                    AND constraint_type = 'FOREIGN KEY'"

                Dim fkParams As New Dictionary(Of String, Object) From {
                    {"@database", DatabaseManager.DatabaseName}
                }

                Dim fkCount As Integer = Convert.ToInt32(DatabaseHandler.ExecuteScalar(fkQuery, fkParams))

                If fkCount = 0 Then
                    Logger.LogWarning("Foreign key constraints missing on professor_subjects table")
                    allValid = False
                End If
            End If

            If allValid Then
                Logger.LogInfo("Database integrity verification passed")
            Else
                Logger.LogWarning("Database integrity verification found issues")
            End If

            Return allValid

        Catch ex As Exception
            Logger.LogError("Error during integrity verification", ex)
            Return False
        End Try
    End Function

#End Region

#Region "Utility Methods (ENHANCED - Preserves Original Signature)"

    ''' <summary>
    ''' Drops all tables (use with caution) - Enhanced with confirmation
    ''' </summary>
    Public Shared Sub DropAllTables()
        Try
            ' NEW: Add confirmation requirement
            Dim confirm As Boolean = ErrorHandler.Confirm(
                "WARNING: This will permanently delete all database tables and data!" & Environment.NewLine & Environment.NewLine &
                "This action cannot be undone. Are you absolutely sure?",
                "Confirm Drop All Tables",
                MessageBoxDefaultButton.Button2)

            If Not confirm Then
                Logger.LogInfo("Drop all tables cancelled by user")
                Return
            End If

            Logger.LogWarning("Dropping all tables...")

            DatabaseHandler.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 0")

            Dim tables As List(Of String) = New List(Of String) From {
                "professor_sections", "professor_subjects", "attendance", "enrollments", 
                "courses", "sections", "professors", "students", "faculty", "users"
            }

            For Each tableName In tables
                Try
                    DatabaseHandler.ExecuteNonQuery($"DROP TABLE IF EXISTS `{tableName}`")
                    Logger.LogInfo($"Dropped table: {tableName}")
                Catch ex As Exception
                    Logger.LogWarning($"Failed to drop table {tableName}: {ex.Message}")
                End Try
            Next

            DatabaseHandler.ExecuteNonQuery("SET FOREIGN_KEY_CHECKS = 1")

            Logger.LogInfo("All tables dropped successfully.")

            ' NEW: Log this critical action to audit log
            Logger.LogAudit("DROP_ALL_TABLES", Environment.UserName, "All database tables were dropped", "ALL_TABLES")

        Catch ex As Exception
            Logger.LogError("Failed to drop tables", ex)
            Throw
        End Try
    End Sub

#End Region

End Class