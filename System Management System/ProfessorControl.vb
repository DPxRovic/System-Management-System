
Imports System
Imports System.Data
Imports System.Windows.Forms
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports System.ComponentModel

Partial Class ProfessorControl
    Inherits UserControl

    Private _professorId As Integer = 1

    Private Function IsInDesignMode() As Boolean
        ' More reliable design-time check for the WinForms designer
        Return (LicenseManager.UsageMode = LicenseUsageMode.Designtime) OrElse Me.DesignMode
    End Function

    Public Property ProfessorId As Integer
        Get
            Return _professorId
        End Get
        Set(value As Integer)
            _professorId = value
            ' Don't attempt to load data at design-time (prevents designer crashes)
            If Not IsInDesignMode() Then
                LoadCourses()
            End If
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
        ' Configure UI (safe at design-time)
        ConfigureGrid()
        ' Only load runtime data when not in designer
        If Not IsInDesignMode() Then
            ' Defer DB calls until Load event for runtime
            AddHandler Me.Load, AddressOf ProfessorControl_Load

            ' Wire UI events at runtime
            AddHandler cboCourse.SelectedIndexChanged, AddressOf cboCourse_SelectedIndexChanged
            AddHandler cboSection.SelectedIndexChanged, AddressOf cboSection_SelectedIndexChanged
            AddHandler txtSearch.TextChanged, AddressOf txtSearch_TextChanged
            AddHandler btnRefresh.Click, AddressOf btnRefresh_Click
            AddHandler btnViewDetails.Click, AddressOf btnViewDetails_Click
        End If
    End Sub

    Private Sub ConfigureGrid()
        dgvStudents.AutoGenerateColumns = False
        dgvStudents.Columns.Clear()
        Dim cId As New DataGridViewTextBoxColumn With {
            .Name = "StudentId",
            .HeaderText = "Student ID",
            .DataPropertyName = "StudentId"
        }
        Dim cName As New DataGridViewTextBoxColumn With {
            .Name = "FullName",
            .HeaderText = "Student Name",
            .DataPropertyName = "FullName"
        }
        Dim cEmail As New DataGridViewTextBoxColumn With {
            .Name = "Email",
            .HeaderText = "Email",
            .DataPropertyName = "Email"
        }
        Dim cStatus As New DataGridViewTextBoxColumn With {
            .Name = "Status",
            .HeaderText = "Status",
            .DataPropertyName = "Status"
        }
        dgvStudents.Columns.AddRange(New DataGridViewColumn() {cId, cName, cEmail, cStatus})
    End Sub

    Private Sub ProfessorControl_Load(sender As Object, e As EventArgs)
        Try
            If IsInDesignMode() Then Return
            LoadCourses()
        Catch ex As Exception
            MessageBox.Show("Failed loading professor data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCourses()
        If IsInDesignMode() Then Return

        cboCourse.Items.Clear()
        cboSection.Items.Clear()
        dgvStudents.DataSource = Nothing

        Try
            Using cn As MySqlConnection = DatabaseManager.GetConnection()
                cn.Open()

                ' Use the actual column names present in 'courses' table.
                ' Avoid referencing c.name which may not exist in all schemas.
                Dim sql As String = "
                    SELECT c.id, c.course_name AS course_name
                    FROM courses c
                    INNER JOIN professor_courses pc ON pc.course_id = c.id
                    WHERE pc.professor_id = @pid
                    ORDER BY c.course_name;"

                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@pid", ProfessorId)
                    Using rdr = cmd.ExecuteReader()
                        While rdr.Read()
                            Dim id = Convert.ToInt32(rdr("id"))
                            Dim name = If(rdr.IsDBNull(rdr.GetOrdinal("course_name")), "", rdr("course_name").ToString())
                            cboCourse.Items.Add(New KeyValuePair(Of Integer, String)(id, name))
                        End While
                    End Using
                End Using
            End Using

            If cboCourse.Items.Count > 0 Then
                cboCourse.SelectedIndex = 0
            End If

        Catch ex As MySqlException
            Logger.LogError("MySQL error in LoadCourses", ex)
            MessageBox.Show($"Failed loading courses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Logger.LogError("Error in LoadCourses", ex)
            MessageBox.Show($"Failed loading courses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadSections(courseId As Integer)
        If IsInDesignMode() Then Return

        cboSection.Items.Clear()
        dgvStudents.DataSource = Nothing

        Try
            Using cn As MySqlConnection = DatabaseManager.GetConnection()
                cn.Open()

                ' 'sections' table uses column 'name' as per initializer - keep that
                Dim sql As String = "
                    SELECT s.id, s.name
                    FROM sections s
                    INNER JOIN professor_sections ps ON ps.section_id = s.id AND ps.professor_id = @pid
                    WHERE s.course_id = @courseId
                    ORDER BY s.name;"

                Using cmd As New MySqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@pid", ProfessorId)
                    cmd.Parameters.AddWithValue("@courseId", courseId)
                    Using rdr = cmd.ExecuteReader()
                        While rdr.Read()
                            Dim id = Convert.ToInt32(rdr("id"))
                            Dim name = If(rdr.IsDBNull(rdr.GetOrdinal("name")), "", rdr("name").ToString())
                            cboSection.Items.Add(New KeyValuePair(Of Integer, String)(id, name))
                        End While
                    End Using
                End Using
            End Using

            If cboSection.Items.Count > 0 Then
                cboSection.SelectedIndex = 0
            End If

        Catch ex As MySqlException
            Logger.LogError("MySQL error in LoadSections", ex)
            MessageBox.Show($"Failed loading sections: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Logger.LogError("Error in LoadSections", ex)
            MessageBox.Show($"Failed loading sections: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadStudents(sectionId As Integer, Optional filter As String = "")
        If IsInDesignMode() Then Return

        Dim dt As New DataTable()
        Try
            Using cn As MySqlConnection = DatabaseManager.GetConnection()
                cn.Open()

                ' Use consistent column names (firstname/lastname) and safe COALESCE usage.
                Dim sql As String = "
                    SELECT s.id AS StudentId,
                           CONCAT(COALESCE(s.lastname, s.last_name), ', ', COALESCE(s.firstname, s.first_name)) AS FullName,
                           COALESCE(s.email, '') AS Email,
                           COALESCE(s.status, '') AS Status
                    FROM students s
                    INNER JOIN enrollments e ON e.student_id = s.id AND e.section_id = @sectionId
                    WHERE (@filter = '' OR CONCAT(COALESCE(s.firstname, s.first_name), ' ', COALESCE(s.lastname, s.last_name)) LIKE CONCAT('%', @filter, '%') OR s.id LIKE CONCAT('%', @filter, '%'))
                    ORDER BY COALESCE(s.lastname, s.last_name), COALESCE(s.firstname, s.first_name);"

                Using da As New MySqlDataAdapter(sql, cn)
                    da.SelectCommand.Parameters.AddWithValue("@sectionId", sectionId)
                    da.SelectCommand.Parameters.AddWithValue("@filter", filter.Trim())
                    da.Fill(dt)
                End Using
            End Using

            dgvStudents.DataSource = dt

        Catch ex As MySqlException
            Logger.LogError("MySQL error in LoadStudents", ex)
            MessageBox.Show($"Failed loading students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Logger.LogError("Error in LoadStudents", ex)
            MessageBox.Show($"Failed loading students: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetSelectedComboValue(combo As ComboBox) As Integer
        If combo.SelectedItem Is Nothing Then Return -1
        Try
            If TypeOf combo.SelectedItem Is KeyValuePair(Of Integer, String) Then
                Dim kvp = DirectCast(combo.SelectedItem, KeyValuePair(Of Integer, String))
                Return kvp.Key
            End If

            Dim text = combo.SelectedItem.ToString()
            Dim id As Integer
            If Integer.TryParse(text, id) Then
                Return id
            End If

            Return -1
        Catch ex As Exception
            Logger.LogError("Error parsing combo selection", ex)
            Return -1
        End Try
    End Function

    ' Event handlers remain the same (designer won't run DB code because of guards)
    Private Sub cboCourse_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim courseId = GetSelectedComboValue(cboCourse)
        If courseId > 0 Then
            LoadSections(courseId)
        Else
            cboSection.Items.Clear()
            dgvStudents.DataSource = Nothing
        End If
    End Sub

    Private Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim sectionId = GetSelectedComboValue(cboSection)
        If sectionId > 0 Then
            LoadStudents(sectionId, txtSearch.Text)
        Else
            dgvStudents.DataSource = Nothing
        End If
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        Dim sectionId = GetSelectedComboValue(cboSection)
        If sectionId > 0 Then
            LoadStudents(sectionId, txtSearch.Text)
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs)
        LoadCourses()
    End Sub

    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs)
        If dgvStudents.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a student from the list.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim studentId = dgvStudents.CurrentRow.Cells("StudentId").Value.ToString()
        MessageBox.Show("Open student details for: " & studentId, "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Removed the invalid CType operator (was causing designer/type conversion issues)
End Class