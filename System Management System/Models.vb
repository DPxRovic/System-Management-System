' ==========================================
' FILENAME: /Models/Models.vb
' PURPOSE: Data models for the application - ENHANCED WITH STUDENT PORTAL FEATURES
' AUTHOR: System
' DATE: 2025-10-14
' LAST UPDATED: 2025-10-21 - Added Student Portal Properties
' Edited By Rovic
' For Future users please do not remove this header
' ==========================================

''' <summary>
''' User model for authentication and authorization
''' </summary>
Public Class User
    Public Property Id As Integer
    Public Property Username As String
    Public Property Password As String
    Public Property Role As String
    Public Property FullName As String
    Public Property IsArchived As Boolean
    Public Property CreatedAt As DateTime

    Public Sub New()
        Id = 0
        Username = ""
        Password = ""
        Role = ""
        FullName = ""
        IsArchived = False
        CreatedAt = DateTime.Now
    End Sub
End Class

''' <summary>
''' Student model with enhanced properties for student portal
''' </summary>
Public Class Student
    Public Property Id As Integer
    Public Property StudentId As String
    Public Property Name As String
    Public Property Course As String
    Public Property Email As String
    Public Property PhoneNumber As String
    Public Property DateOfBirth As DateTime?
    Public Property EnrollmentDate As DateTime?
    Public Property Status As String
    Public Property CreatedAt As DateTime

    ' Enhanced properties for Student Portal
    Public Property Age As Integer?
        Get
            If DateOfBirth.HasValue Then
                Dim today As DateTime = DateTime.Today
                Dim calculatedAge As Integer = today.Year - DateOfBirth.Value.Year
                If DateOfBirth.Value.Date > today.AddYears(-calculatedAge) Then
                    calculatedAge -= 1
                End If
                Return calculatedAge
            End If
            Return Nothing
        End Get
        Set(value As Integer?)
            ' Age is calculated, setter not used
        End Set
    End Property

    Public Property DaysEnrolled As Integer
        Get
            If EnrollmentDate.HasValue Then
                Return (DateTime.Today - EnrollmentDate.Value).Days
            End If
            Return 0
        End Get
        Set(value As Integer)
            ' DaysEnrolled is calculated, setter not used
        End Set
    End Property

    ' Additional properties for student portal
    Public Property TotalCourses As Integer
    Public Property TotalAttendance As Integer
    Public Property AttendanceRate As Double
    Public Property PresentCount As Integer
    Public Property AbsentCount As Integer
    Public Property LateCount As Integer
    Public Property ExcusedCount As Integer

    Public Sub New()
        Id = 0
        StudentId = ""
        Name = ""
        Course = ""
        Email = ""
        PhoneNumber = ""
        DateOfBirth = Nothing
        EnrollmentDate = Nothing
        Status = "Active"
        CreatedAt = DateTime.Now
        TotalCourses = 0
        TotalAttendance = 0
        AttendanceRate = 0.0
        PresentCount = 0
        AbsentCount = 0
        LateCount = 0
        ExcusedCount = 0
    End Sub
End Class

''' <summary>
''' Course model with enhanced properties
''' </summary>
Public Class Course
    Public Property Id As Integer
    Public Property CourseCode As String
    Public Property CourseName As String
    Public Property FacultyId As Integer?
    Public Property Description As String
    Public Property Credits As Integer
    Public Property Schedule As String
    Public Property Room As String
    Public Property Status As String
    Public Property CreatedAt As DateTime

    ' Additional properties for display
    Public Property FacultyName As String
    Public Property EnrolledStudents As Integer

    Public Sub New()
        Id = 0
        CourseCode = ""
        CourseName = ""
        FacultyId = Nothing
        Description = ""
        Credits = 3
        Schedule = ""
        Room = ""
        Status = "Active"
        CreatedAt = DateTime.Now
        FacultyName = "Not Assigned"
        EnrolledStudents = 0
    End Sub
End Class

''' <summary>
''' Attendance record model
''' </summary>
Public Class Attendance
    Public Property Id As Integer
    Public Property StudentId As String
    Public Property CourseId As Integer
    Public Property [Date] As DateTime
    Public Property Status As String
    Public Property TimeIn As TimeSpan?
    Public Property TimeOut As TimeSpan?
    Public Property Remarks As String
    Public Property RecordedBy As String
    Public Property CreatedAt As DateTime

    ' Additional properties for display
    Public Property StudentName As String
    Public Property CourseCode As String
    Public Property CourseName As String

    Public Sub New()
        Id = 0
        StudentId = ""
        CourseId = 0
        [Date] = DateTime.Today
        Status = "Present"
        TimeIn = Nothing
        TimeOut = Nothing
        Remarks = ""
        RecordedBy = ""
        CreatedAt = DateTime.Now
        StudentName = ""
        CourseCode = ""
        CourseName = ""
    End Sub
End Class

''' <summary>
''' Faculty model
''' </summary>
Public Class Faculty
    Public Property Id As Integer
    Public Property FacultyId As String
    Public Property Name As String
    Public Property Department As String
    Public Property Email As String
    Public Property PhoneNumber As String
    Public Property Specialization As String
    Public Property HireDate As DateTime?
    Public Property Status As String
    Public Property CreatedAt As DateTime

    ' Additional properties
    Public Property AssignedCourses As Integer

    Public Sub New()
        Id = 0
        FacultyId = ""
        Name = ""
        Department = ""
        Email = ""
        PhoneNumber = ""
        Specialization = ""
        HireDate = Nothing
        Status = "Active"
        CreatedAt = DateTime.Now
        AssignedCourses = 0
    End Sub
End Class

''' <summary>
''' Enrollment model - represents student enrollment in a course
''' </summary>
Public Class Enrollment
    Public Property Id As Integer
    Public Property StudentId As String
    Public Property CourseId As Integer
    Public Property EnrollmentDate As DateTime
    Public Property Status As String
    Public Property Grade As String
    Public Property CreatedAt As DateTime

    ' Additional properties for display
    Public Property StudentName As String
    Public Property CourseCode As String
    Public Property CourseName As String
    Public Property Credits As Integer

    Public Sub New()
        Id = 0
        StudentId = ""
        CourseId = 0
        EnrollmentDate = DateTime.Today
        Status = "Enrolled"
        Grade = ""
        CreatedAt = DateTime.Now
        StudentName = ""
        CourseCode = ""
        CourseName = ""
        Credits = 0
    End Sub
End Class

''' <summary>
''' Attendance statistics model for reporting
''' </summary>
Public Class AttendanceStats
    Public Property TotalSessions As Integer
    Public Property PresentCount As Integer
    Public Property AbsentCount As Integer
    Public Property LateCount As Integer
    Public Property ExcusedCount As Integer
    Public Property AttendanceRate As Double

    Public Sub New()
        TotalSessions = 0
        PresentCount = 0
        AbsentCount = 0
        LateCount = 0
        ExcusedCount = 0
        AttendanceRate = 0.0
    End Sub

    ''' <summary>
    ''' Calculates attendance rate based on counts
    ''' </summary>
    Public Sub CalculateRate()
        If TotalSessions > 0 Then
            Dim attendedCount As Integer = PresentCount + LateCount + ExcusedCount
            AttendanceRate = (attendedCount / TotalSessions) * 100
        Else
            AttendanceRate = 0.0
        End If
    End Sub
End Class

''' <summary>
''' Course attendance summary model for student portal
''' </summary>
Public Class CourseAttendanceSummary
    Public Property CourseId As Integer
    Public Property CourseCode As String
    Public Property CourseName As String
    Public Property TotalSessions As Integer
    Public Property PresentCount As Integer
    Public Property AbsentCount As Integer
    Public Property LateCount As Integer
    Public Property ExcusedCount As Integer
    Public Property AttendanceRate As Double
    Public Property Credits As Integer
    Public Property FacultyName As String

    Public Sub New()
        CourseId = 0
        CourseCode = ""
        CourseName = ""
        TotalSessions = 0
        PresentCount = 0
        AbsentCount = 0
        LateCount = 0
        ExcusedCount = 0
        AttendanceRate = 0.0
        Credits = 0
        FacultyName = ""
    End Sub
End Class

''' <summary>
''' Student profile statistics model for dashboard
''' </summary>
Public Class StudentProfileStats
    Public Property EnrolledCourses As Integer
    Public Property TotalAttendance As Integer
    Public Property AttendanceRate As Double
    Public Property DaysEnrolled As Integer
    Public Property PresentCount As Integer
    Public Property AbsentCount As Integer
    Public Property LateCount As Integer
    Public Property ExcusedCount As Integer
    Public Property AverageGrade As String

    Public Sub New()
        EnrolledCourses = 0
        TotalAttendance = 0
        AttendanceRate = 0.0
        DaysEnrolled = 0
        PresentCount = 0
        AbsentCount = 0
        LateCount = 0
        ExcusedCount = 0
        AverageGrade = "N/A"
    End Sub
End Class

''' <summary>
''' Course details with attendance model for student portal
''' </summary>
Public Class CourseDetailsWithAttendance
    Public Property CourseId As Integer
    Public Property CourseCode As String
    Public Property CourseName As String
    Public Property Description As String
    Public Property Credits As Integer
    Public Property Schedule As String
    Public Property Room As String
    Public Property FacultyName As String
    Public Property FacultyEmail As String
    Public Property TotalSessions As Integer
    Public Property PresentCount As Integer
    Public Property AbsentCount As Integer
    Public Property LateCount As Integer
    Public Property ExcusedCount As Integer
    Public Property AttendanceRate As Double
    Public Property EnrollmentDate As DateTime?
    Public Property CurrentGrade As String

    Public Sub New()
        CourseId = 0
        CourseCode = ""
        CourseName = ""
        Description = ""
        Credits = 0
        Schedule = ""
        Room = ""
        FacultyName = ""
        FacultyEmail = ""
        TotalSessions = 0
        PresentCount = 0
        AbsentCount = 0
        LateCount = 0
        ExcusedCount = 0
        AttendanceRate = 0.0
        EnrollmentDate = Nothing
        CurrentGrade = "N/A"
    End Sub
End Class

''' <summary>
''' Attendance report item model
''' </summary>
Public Class AttendanceReportItem
    Public Property [Date] As DateTime
    Public Property CourseCode As String
    Public Property CourseName As String
    Public Property Status As String
    Public Property TimeIn As String
    Public Property TimeOut As String
    Public Property FacultyName As String
    Public Property Remarks As String

    Public Sub New()
        [Date] = DateTime.Today
        CourseCode = ""
        CourseName = ""
        Status = ""
        TimeIn = ""
        TimeOut = ""
        FacultyName = ""
        Remarks = ""
    End Sub
End Class

''' <summary>
''' Monthly attendance trend model
''' </summary>
Public Class MonthlyAttendanceTrend
    Public Property Month As String
    Public Property MonthName As String
    Public Property TotalSessions As Integer
    Public Property PresentCount As Integer
    Public Property AbsentCount As Integer
    Public Property AttendanceRate As Double

    Public Sub New()
        Month = ""
        MonthName = ""
        TotalSessions = 0
        PresentCount = 0
        AbsentCount = 0
        AttendanceRate = 0.0
    End Sub
End Class

''' <summary>
''' Validation result model
''' </summary>
Public Class ValidationResult
    Public Property IsValid As Boolean
    Public Property ErrorMessage As String
    Public Property Errors As New List(Of String)

    Public Sub New()
        IsValid = True
        ErrorMessage = ""
    End Sub

    Public Sub AddError(message As String)
        IsValid = False
        Errors.Add(message)
        If String.IsNullOrEmpty(ErrorMessage) Then
            ErrorMessage = message
        Else
            ErrorMessage &= vbCrLf & message
        End If
    End Sub
End Class

''' <summary>
''' Report filter model for generating reports
''' </summary>
Public Class ReportFilter
    Public Property StartDate As DateTime
    Public Property EndDate As DateTime
    Public Property StudentId As String
    Public Property CourseId As Integer?
    Public Property Status As String
    Public Property ReportType As String

    Public Sub New()
        StartDate = DateTime.Today.AddMonths(-1)
        EndDate = DateTime.Today
        StudentId = ""
        CourseId = Nothing
        Status = "All"
        ReportType = "Summary"
    End Sub
End Class

''' <summary>
''' Dashboard summary model
''' </summary>
Public Class DashboardSummary
    Public Property TotalStudents As Integer
    Public Property TotalCourses As Integer
    Public Property TotalFaculty As Integer
    Public Property TotalUsers As Integer
    Public Property TodayAttendance As Integer
    Public Property OverallAttendanceRate As Double
    Public Property ActiveEnrollments As Integer

    Public Sub New()
        TotalStudents = 0
        TotalCourses = 0
        TotalFaculty = 0
        TotalUsers = 0
        TodayAttendance = 0
        OverallAttendanceRate = 0.0
        ActiveEnrollments = 0
    End Sub
End Class

''' <summary>
''' System notification model
''' </summary>
Public Class SystemNotification
    Public Property Id As Integer
    Public Property Title As String
    Public Property Message As String
    Public Property NotificationType As String
    Public Property IsRead As Boolean
    Public Property CreatedAt As DateTime
    Public Property TargetUserId As Integer?
    Public Property TargetRole As String

    Public Sub New()
        Id = 0
        Title = ""
        Message = ""
        NotificationType = "Info"
        IsRead = False
        CreatedAt = DateTime.Now
        TargetUserId = Nothing
        TargetRole = ""
    End Sub
End Class