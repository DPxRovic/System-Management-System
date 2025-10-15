' ==========================================
' FILENAME: /Models/Models.vb
' PURPOSE: Base models for Student, Faculty, User, Course, and Attendance
' AUTHOR: System
' DATE: 2025-10-14
' Edited By Rovic
' For Future users please do not remove this header
' ==========================================

''' <summary>
''' User model representing system users (Admin, Faculty, etc.)
''' </summary>
Public Class User
    Public Property Id As Integer
    Public Property Username As String
    Public Property Password As String
    Public Property Role As String
    Public Property FullName As String
    Public Property CreatedAt As DateTime

    Public Sub New()
        Id = 0
        Username = ""
        Password = ""
        Role = ""
        FullName = ""
        CreatedAt = DateTime.Now
    End Sub

    Public Sub New(id As Integer, username As String, password As String, role As String, fullName As String)
        Me.Id = id
        Me.Username = username
        Me.Password = password
        Me.Role = role
        Me.FullName = fullName
        Me.CreatedAt = DateTime.Now
    End Sub
End Class

''' <summary>
''' Student model representing student records
''' </summary>
Public Class Student
    Public Property Id As Integer
    Public Property StudentId As String
    Public Property Name As String
    Public Property Course As String
    Public Property Email As String
    Public Property PhoneNumber As String
    Public Property DateOfBirth As DateTime
    Public Property EnrollmentDate As DateTime
    Public Property Status As String

    Public Sub New()
        Id = 0
        StudentId = ""
        Name = ""
        Course = ""
        Email = ""
        PhoneNumber = ""
        DateOfBirth = DateTime.Now
        EnrollmentDate = DateTime.Now
        Status = "Active"
    End Sub

    Public Sub New(id As Integer, studentId As String, name As String, course As String)
        Me.Id = id
        Me.StudentId = studentId
        Me.Name = name
        Me.Course = course
        Me.Email = ""
        Me.PhoneNumber = ""
        Me.DateOfBirth = DateTime.Now
        Me.EnrollmentDate = DateTime.Now
        Me.Status = "Active"
    End Sub
End Class

''' <summary>
''' Course model representing courses/subjects
''' </summary>
Public Class Course
    Public Property Id As Integer
    Public Property CourseCode As String
    Public Property CourseName As String
    Public Property FacultyId As Integer
    Public Property FacultyName As String
    Public Property Description As String
    Public Property Credits As Integer
    Public Property Schedule As String
    Public Property Room As String
    Public Property Status As String

    Public Sub New()
        Id = 0
        CourseCode = ""
        CourseName = ""
        FacultyId = 0
        FacultyName = ""
        Description = ""
        Credits = 0
        Schedule = ""
        Room = ""
        Status = "Active"
    End Sub

    Public Sub New(id As Integer, courseCode As String, courseName As String, facultyId As Integer)
        Me.Id = id
        Me.CourseCode = courseCode
        Me.CourseName = courseName
        Me.FacultyId = facultyId
        Me.FacultyName = ""
        Me.Description = ""
        Me.Credits = 0
        Me.Schedule = ""
        Me.Room = ""
        Me.Status = "Active"
    End Sub
End Class

''' <summary>
''' Attendance model representing student attendance records
''' </summary>
Public Class AttendanceRecord
    Public Property Id As Integer
    Public Property StudentId As String
    Public Property CourseId As Integer
    Public Property AttendanceDate As DateTime
    Public Property Status As String ' Present, Absent, Late, Excused
    Public Property TimeIn As DateTime?
    Public Property TimeOut As DateTime?
    Public Property Remarks As String
    Public Property RecordedBy As String

    Public Sub New()
        Id = 0
        StudentId = ""
        CourseId = 0
        AttendanceDate = DateTime.Today
        Status = "Present"
        TimeIn = Nothing
        TimeOut = Nothing
        Remarks = ""
        RecordedBy = ""
    End Sub

    Public Sub New(id As Integer, studentId As String, courseId As Integer, attendanceDate As DateTime, status As String)
        Me.Id = id
        Me.StudentId = studentId
        Me.CourseId = courseId
        Me.AttendanceDate = attendanceDate
        Me.Status = status
        Me.TimeIn = Nothing
        Me.TimeOut = Nothing
        Me.Remarks = ""
        Me.RecordedBy = ""
    End Sub
End Class

''' <summary>
''' Faculty model representing faculty/teacher records
''' </summary>
Public Class Faculty
    Public Property Id As Integer
    Public Property FacultyId As String
    Public Property Name As String
    Public Property Department As String
    Public Property Email As String
    Public Property PhoneNumber As String
    Public Property Specialization As String
    Public Property HireDate As DateTime
    Public Property Status As String

    Public Sub New()
        Id = 0
        FacultyId = ""
        Name = ""
        Department = ""
        Email = ""
        PhoneNumber = ""
        Specialization = ""
        HireDate = DateTime.Now
        Status = "Active"
    End Sub

    Public Sub New(id As Integer, facultyId As String, name As String, department As String)
        Me.Id = id
        Me.FacultyId = facultyId
        Me.Name = name
        Me.Department = department
        Me.Email = ""
        Me.PhoneNumber = ""
        Me.Specialization = ""
        Me.HireDate = DateTime.Now
        Me.Status = "Active"
    End Sub
End Class

''' <summary>
''' Enrollment model representing student course enrollments
''' </summary>
Public Class Enrollment
    Public Property Id As Integer
    Public Property StudentId As String
    Public Property CourseId As Integer
    Public Property EnrollmentDate As DateTime
    Public Property Status As String
    Public Property Grade As String

    Public Sub New()
        Id = 0
        StudentId = ""
        CourseId = 0
        EnrollmentDate = DateTime.Now
        Status = "Enrolled"
        Grade = ""
    End Sub

    Public Sub New(id As Integer, studentId As String, courseId As Integer)
        Me.Id = id
        Me.StudentId = studentId
        Me.CourseId = courseId
        Me.EnrollmentDate = DateTime.Now
        Me.Status = "Enrolled"
        Me.Grade = ""
    End Sub
End Class

''' <summary>
''' Enum for user roles
''' </summary>
Public Enum UserRole
    Student
    Faculty
    Admin
    SuperAdmin
End Enum

''' <summary>
''' Enum for attendance status
''' </summary>
Public Enum AttendanceStatus
    Present
    Absent
    Late
    Excused
End Enum