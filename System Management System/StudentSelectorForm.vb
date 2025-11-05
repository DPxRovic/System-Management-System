' ==========================================
' FILENAME: /Forms/StudentSelectorForm.vb
' PURPOSE: Allows Admin/Faculty to select which student portal to access
' AUTHOR: System
' DATE: 2025-10-26
' CREATED FOR: FIX #3 - Student Portal Access for Admin/Faculty
' ==========================================

Imports System.Data
Imports Guna.UI2.WinForms

Public Class StudentSelectorForm
    Private currentUser As User
    Private allStudents As DataTable

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New(user As User)
        InitializeComponent()
        currentUser = user
    End Sub

    ''' <summary>
    ''' Form load event
    ''' </summary>
    Private Sub StudentSelectorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Load all active students
            LoadStudents()

            ' Apply theme
            ThemeManager.StyleDataGridView(dgvStudents)
            ThemeManager.SlideIn(pnlMain, "TOP", 300)

            Logger.LogInfo($"Student selector loaded by {currentUser.Username}")

        Catch ex As Exception
            Logger.LogError("Error loading student selector", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Load all active students
    ''' </summary>
    Private Sub LoadStudents()
        Try
            Dim query As String = "
                SELECT 
                    s.id,
                    s.student_id,
                    s.name,
                    s.course,
                    s.email,
                    s.phone_number,
                    s.status,
                    s.enrollment_date,
                    COUNT(DISTINCT e.course_id) as enrolled_courses,
                    COUNT(DISTINCT a.id) as total_attendance
                FROM students s
                LEFT JOIN enrollments e ON s.student_id = e.student_id
                LEFT JOIN attendance a ON s.student_id = a.student_id
                WHERE s.status = 'Active'
                GROUP BY s.id, s.student_id, s.name, s.course, s.email, s.phone_number, s.status, s.enrollment_date
                ORDER BY s.name ASC"

            allStudents = DatabaseHandler.ExecuteReader(query)
            dgvStudents.DataSource = allStudents

            ' Format columns
            FormatColumns()

            ' Update count
            lblStudentCount.Text = $"Total Active Students: {allStudents.Rows.Count}"

        Catch ex As Exception
            Logger.LogError("Error loading students", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Format DataGridView columns
    ''' </summary>
    Private Sub FormatColumns()
        Try
            If dgvStudents.Columns.Count = 0 Then Return

            ' Hide ID column
            If dgvStudents.Columns.Contains("id") Then
                dgvStudents.Columns("id").Visible = False
            End If

            ' Format visible columns
            If dgvStudents.Columns.Contains("student_id") Then
                dgvStudents.Columns("student_id").HeaderText = "Student ID"
                dgvStudents.Columns("student_id").Width = 120
            End If

            If dgvStudents.Columns.Contains("name") Then
                dgvStudents.Columns("name").HeaderText = "Full Name"
                dgvStudents.Columns("name").Width = 250
            End If

            If dgvStudents.Columns.Contains("course") Then
                dgvStudents.Columns("course").HeaderText = "Course/Program"
                dgvStudents.Columns("course").Width = 200
            End If

            If dgvStudents.Columns.Contains("email") Then
                dgvStudents.Columns("email").HeaderText = "Email"
                dgvStudents.Columns("email").Width = 200
            End If

            If dgvStudents.Columns.Contains("phone_number") Then
                dgvStudents.Columns("phone_number").HeaderText = "Phone"
                dgvStudents.Columns("phone_number").Width = 120
            End If

            If dgvStudents.Columns.Contains("enrolled_courses") Then
                dgvStudents.Columns("enrolled_courses").HeaderText = "Enrolled Courses"
                dgvStudents.Columns("enrolled_courses").Width = 120
            End If

            If dgvStudents.Columns.Contains("total_attendance") Then
                dgvStudents.Columns("total_attendance").HeaderText = "Total Attendance"
                dgvStudents.Columns("total_attendance").Width = 120
            End If

            If dgvStudents.Columns.Contains("enrollment_date") Then
                dgvStudents.Columns("enrollment_date").HeaderText = "Enrolled On"
                dgvStudents.Columns("enrollment_date").Width = 120
            End If

            If dgvStudents.Columns.Contains("status") Then
                dgvStudents.Columns("status").HeaderText = "Status"
                dgvStudents.Columns("status").Width = 100
            End If

        Catch ex As Exception
            Logger.LogError("Error formatting columns", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Search students
    ''' </summary>
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim searchText As String = txtSearch.Text.Trim().ToLower()

            If String.IsNullOrWhiteSpace(searchText) Then
                ' Reset to show all
                dgvStudents.DataSource = allStudents
                lblStudentCount.Text = $"Total Active Students: {allStudents.Rows.Count}"
                Return
            End If

            ' Filter data
            Dim filteredView As DataView = allStudents.DefaultView
            filteredView.RowFilter = $"student_id LIKE '%{searchText}%' OR name LIKE '%{searchText}%' OR course LIKE '%{searchText}%'"

            dgvStudents.DataSource = filteredView
            lblStudentCount.Text = $"Found: {filteredView.Count} students"

        Catch ex As Exception
            Logger.LogError("Error searching students", ex)
            MessageBox.Show($"Search error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Clear search
    ''' </summary>
    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            txtSearch.Clear()
            dgvStudents.DataSource = allStudents
            lblStudentCount.Text = $"Total Active Students: {allStudents.Rows.Count}"

        Catch ex As Exception
            Logger.LogError("Error clearing search", ex)
        End Try
    End Sub

    ''' <summary>
    ''' View selected student portal
    ''' </summary>
    Private Sub btnViewPortal_Click(sender As Object, e As EventArgs) Handles btnViewPortal.Click
        Try
            If dgvStudents.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a student first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim selectedRow As DataGridViewRow = dgvStudents.SelectedRows(0)
            Dim studentId As String = selectedRow.Cells("student_id").Value.ToString()
            Dim studentName As String = selectedRow.Cells("name").Value.ToString()

            ' Confirm selection
            Dim result As DialogResult = MessageBox.Show(
                $"Open student portal for:{vbCrLf}{vbCrLf}Student ID: {studentId}{vbCrLf}Name: {studentName}{vbCrLf}{vbCrLf}Continue?",
                "Confirm Student Portal Access",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                OpenStudentPortal(studentId)
            End If

        Catch ex As Exception
            Logger.LogError("Error viewing student portal", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Open student portal for selected student
    ''' </summary>
    Private Sub OpenStudentPortal(studentId As String)
        Try
            ' Validate student access
            If Not StudentPortalRepository.ValidateStudentAccess(studentId) Then
                MessageBox.Show("This student does not have portal access. Please check student enrollment status.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim profile = StudentPortalRepository.GetStudentProfile(studentId)
            If profile Is Nothing Then
                MessageBox.Show("Student profile not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Create a temporary user object for the student
            Dim studentUser As New User With {
                .Username = studentId,
                .FullName = profile.Name,
                .Role = "Student"
            }

            ' Open student portal
            Dim portalForm As New StudentPortalForm(studentUser)

            ' Find parent DashboardForm and load the portal
            Dim parentForm As Form = Me.FindForm()
            If TypeOf parentForm Is DashboardForm Then
                Dim dashboard As DashboardForm = DirectCast(parentForm, DashboardForm)
                ' The selector form will be replaced by the portal form
            Else
                ' Standalone mode - open in new window
                portalForm.Show()
            End If

            Logger.LogInfo($"{currentUser.Username} ({currentUser.Role}) accessed portal for student {studentId}")

            ' Close this selector form
            Me.Close()

        Catch ex As Exception
            Logger.LogError("Error opening student portal", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Double-click to open portal
    ''' </summary>
    Private Sub dgvStudents_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudents.CellDoubleClick
        Try
            If e.RowIndex >= 0 Then
                btnViewPortal.PerformClick()
            End If
        Catch ex As Exception
            Logger.LogError("Error on cell double click", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Refresh student list
    ''' </summary>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            txtSearch.Clear()
            LoadStudents()
            MessageBox.Show("Student list refreshed.", "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Logger.LogError("Error refreshing students", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Selection changed - enable/disable view button
    ''' </summary>
    Private Sub dgvStudents_SelectionChanged(sender As Object, e As EventArgs) Handles dgvStudents.SelectionChanged
        Try
            btnViewPortal.Enabled = dgvStudents.SelectedRows.Count > 0
        Catch ex As Exception
            ' Silently handle
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub

    Private Sub dgvStudents_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudents.CellContentClick

    End Sub
End Class