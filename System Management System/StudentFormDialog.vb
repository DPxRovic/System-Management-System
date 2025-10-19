' ==========================================
' FILENAME: /Forms/StudentFormDialog.vb
' PURPOSE: Dialog form for adding/editing student records
' AUTHOR: System
' DATE: 2025-10-18
' ==========================================

Imports Guna.UI2.WinForms
Imports Org.BouncyCastle.Asn1.Cmp

Public Class StudentFormDialog
    Private editMode As Boolean = False
    Private currentStudent As Student = Nothing

    ''' <summary>
    ''' Constructor for adding new student
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        editMode = False
    End Sub

    ''' <summary>
    ''' Constructor for editing existing student
    ''' </summary>
    Public Sub New(student As Student)
        InitializeComponent()
        editMode = True
        currentStudent = student
    End Sub

    ''' <summary>
    ''' Form load event
    ''' </summary>
    Private Sub StudentFormDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Initialize status dropdown
            InitializeStatusDropdown()

            ' Set form title and button text based on mode
            If editMode Then
                lblTitle.Text = "Edit Student Record"
                btnSave.Text = "Update"
                LoadStudentData()
            Else
                lblTitle.Text = "Add New Student"
                btnSave.Text = "Save"
                ' Set default values
                dtpEnrollmentDate.Value = DateTime.Today
                cmbStatus.SelectedIndex = 0 ' Active
            End If

            ' Apply theme
            ApplyTheme()

        Catch ex As Exception
            Logger.LogError("Error loading student form dialog", ex)
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Initialize status dropdown
    ''' </summary>
    Private Sub InitializeStatusDropdown()
        Try
            cmbStatus.Items.Clear()
            cmbStatus.Items.AddRange(New String() {"Active", "Inactive", "Graduated", "Archived"})
            cmbStatus.SelectedIndex = 0

        Catch ex As Exception
            Logger.LogError("Error initializing status dropdown", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Load student data for editing
    ''' </summary>
    Private Sub LoadStudentData()
        Try
            If currentStudent Is Nothing Then
                Return
            End If

            txtStudentId.Text = currentStudent.StudentId
            txtName.Text = currentStudent.Name
            txtEmail.Text = currentStudent.Email
            txtPhoneNumber.Text = currentStudent.PhoneNumber
            txtCourse.Text = currentStudent.Course

            ' Date of birth
            If currentStudent.DateOfBirth.HasValue Then
                dtpDateOfBirth.Value = currentStudent.DateOfBirth.Value
                chkNoDOB.Checked = False
            Else
                dtpDateOfBirth.Value = DateTime.Today.AddYears(-20)
                chkNoDOB.Checked = True
            End If

            ' Enrollment date
            If currentStudent.EnrollmentDate.HasValue Then
                dtpEnrollmentDate.Value = currentStudent.EnrollmentDate.Value
            Else
                dtpEnrollmentDate.Value = DateTime.Today
            End If

            ' Status
            Dim statusIndex As Integer = cmbStatus.FindStringExact(currentStudent.Status)
            If statusIndex >= 0 Then
                cmbStatus.SelectedIndex = statusIndex
            End If

        Catch ex As Exception
            Logger.LogError("Error loading student data", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Apply theme to form controls
    ''' </summary>
    Private Sub ApplyTheme()
        Try
            Me.BackColor = ThemeManager.BackgroundColor
            pnlMain.FillColor = ThemeManager.WhiteColor
            ThemeManager.ApplyShadow(pnlMain, ThemeManager.ShadowDepthHeavy)

        Catch ex As Exception
            Logger.LogError("Error applying theme", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Validate form inputs
    ''' </summary>
    Private Function ValidateInputs() As Boolean
        Try
            ' Validate Student ID
            If String.IsNullOrWhiteSpace(txtStudentId.Text) Then
                MessageBox.Show("Please enter a Student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtStudentId.Focus()
                Return False
            End If

            If txtStudentId.Text.Length < 4 Then
                MessageBox.Show("Student ID must be at least 4 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtStudentId.Focus()
                Return False
            End If

            ' Validate Name
            If String.IsNullOrWhiteSpace(txtName.Text) Then
                MessageBox.Show("Please enter the student's full name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtName.Focus()
                Return False
            End If

            If txtName.Text.Length < 3 Then
                MessageBox.Show("Student name must be at least 3 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtName.Focus()
                Return False
            End If

            ' Validate Email (if provided)
            If Not String.IsNullOrWhiteSpace(txtEmail.Text) Then
                If Not IsValidEmail(txtEmail.Text) Then
                    MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtEmail.Focus()
                    Return False
                End If
            End If

            ' Validate Phone Number (if provided)
            If Not String.IsNullOrWhiteSpace(txtPhoneNumber.Text) Then
                If txtPhoneNumber.Text.Length < 7 Then
                    MessageBox.Show("Phone number must be at least 7 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtPhoneNumber.Focus()
                    Return False
                End If
            End If

            ' Validate Course
            If String.IsNullOrWhiteSpace(txtCourse.Text) Then
                MessageBox.Show("Please enter the course/program.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtCourse.Focus()
                Return False
            End If

            ' Validate Date of Birth (if not checked as no DOB)
            If Not chkNoDOB.Checked Then
                If dtpDateOfBirth.Value > DateTime.Today Then
                    MessageBox.Show("Date of birth cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    dtpDateOfBirth.Focus()
                    Return False
                End If

                Dim age As Integer = DateTime.Today.Year - dtpDateOfBirth.Value.Year
                If dtpDateOfBirth.Value.Date > DateTime.Today.AddYears(-age) Then age -= 1

                If age < 5 OrElse age > 100 Then
                    MessageBox.Show("Please enter a valid date of birth (age must be between 5 and 100 years).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    dtpDateOfBirth.Focus()
                    Return False
                End If
            End If

            ' Validate Enrollment Date
            If dtpEnrollmentDate.Value > DateTime.Today Then
                MessageBox.Show("Enrollment date cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                dtpEnrollmentDate.Focus()
                Return False
            End If

            ' Validate Status
            If cmbStatus.SelectedIndex < 0 Then
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                cmbStatus.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Logger.LogError("Error validating inputs", ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Validates email format
    ''' </summary>
    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr = New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Save button click
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Validate inputs
            If Not ValidateInputs() Then
                Return
            End If

            ' Show loading cursor
            Me.Cursor = Cursors.WaitCursor
            btnSave.Enabled = False

            ' Create student object
            Dim student As New Student With {
                .StudentId = txtStudentId.Text.Trim(),
                .Name = txtName.Text.Trim(),
                .Email = txtEmail.Text.Trim(),
                .PhoneNumber = txtPhoneNumber.Text.Trim(),
                .Course = txtCourse.Text.Trim(),
                .DateOfBirth = If(chkNoDOB.Checked, Nothing, dtpDateOfBirth.Value),
                .EnrollmentDate = dtpEnrollmentDate.Value,
                .Status = cmbStatus.SelectedItem.ToString()
            }

            Dim success As Boolean = False

            If editMode Then
                ' Update existing student
                student.Id = currentStudent.Id
                success = StudentRepository.UpdateStudent(student)
            Else
                ' Create new student
                success = StudentRepository.CreateStudent(student)
            End If

            If success Then
                MessageBox.Show($"Student {If(editMode, "updated", "added")} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                MessageBox.Show($"Failed to {If(editMode, "update", "add")} student. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            Logger.LogError($"Error {If(editMode, "updating", "adding")} student", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
            btnSave.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' Cancel button click
    ''' </summary>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' No DOB checkbox changed
    ''' </summary>
    Private Sub chkNoDOB_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoDOB.CheckedChanged
        Try
            dtpDateOfBirth.Enabled = Not chkNoDOB.Checked

            If chkNoDOB.Checked Then
                dtpDateOfBirth.Value = DateTime.Today.AddYears(-20)
            End If

        Catch ex As Exception
            Logger.LogError("Error handling no DOB checkbox", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Handle Enter key press in text boxes
    ''' </summary>
    Private Sub txtStudentId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStudentId.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtName.Focus()
        End If
    End Sub

    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtEmail.Focus()
        End If
    End Sub

    Private Sub txtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmail.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtPhoneNumber.Focus()
        End If
    End Sub

    Private Sub txtPhoneNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhoneNumber.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtCourse.Focus()
        End If
    End Sub

    Private Sub txtCourse_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCourse.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            dtpDateOfBirth.Focus()
        End If
    End Sub

End Class