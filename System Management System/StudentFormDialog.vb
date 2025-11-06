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
            InitializeStatusDropdown()

            RemoveHandler chkNoDOB.CheckedChanged, AddressOf chkNoDOB_CheckedChanged

            If editMode Then
                lblTitle.Text = "Edit Student Record"
                btnSave.Text = "Update"
                LoadStudentData()
            Else
                lblTitle.Text = "Add New Student"
                btnSave.Text = "Save"
                dtpEnrollmentDate.Value = DateTime.Today
                cmbStatus.SelectedIndex = 0
                dtpDateOfBirth.Value = DateTime.Today.AddYears(-18)
                dtpDateOfBirth.Enabled = True
                chkNoDOB.Checked = False
            End If

            AddHandler chkNoDOB.CheckedChanged, AddressOf chkNoDOB_CheckedChanged
            ApplyTheme()

        Catch ex As Exception
            Logger.LogError("Error loading student form dialog", ex)
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeStatusDropdown()
        Try
            cmbStatus.Items.Clear()
            cmbStatus.Items.AddRange(New String() {"Active", "Inactive", "Graduated", "Archived"})
            cmbStatus.SelectedIndex = 0
        Catch ex As Exception
            Logger.LogError("Error initializing status dropdown", ex)
        End Try
    End Sub

    Private Sub LoadStudentData()
        Try
            If currentStudent Is Nothing Then Return

            txtStudentId.Text = currentStudent.StudentId
            txtName.Text = currentStudent.Name
            txtEmail.Text = currentStudent.Email
            txtPhoneNumber.Text = currentStudent.PhoneNumber
            txtCourse.Text = currentStudent.Course

            RemoveHandler chkNoDOB.CheckedChanged, AddressOf chkNoDOB_CheckedChanged

            If currentStudent.DateOfBirth.HasValue Then
                chkNoDOB.Checked = False
                dtpDateOfBirth.Value = currentStudent.DateOfBirth.Value
                dtpDateOfBirth.Enabled = True
            Else
                chkNoDOB.Checked = True
                dtpDateOfBirth.Value = DateTime.Today.AddYears(-20)
                dtpDateOfBirth.Enabled = False
            End If

            AddHandler chkNoDOB.CheckedChanged, AddressOf chkNoDOB_CheckedChanged

            If currentStudent.EnrollmentDate.HasValue Then
                dtpEnrollmentDate.Value = currentStudent.EnrollmentDate.Value
            Else
                dtpEnrollmentDate.Value = DateTime.Today
            End If

            Dim statusIndex As Integer = cmbStatus.FindStringExact(currentStudent.Status)
            If statusIndex >= 0 Then cmbStatus.SelectedIndex = statusIndex

        Catch ex As Exception
            Logger.LogError("Error loading student data", ex)
        End Try
    End Sub

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

            ' === Extra validation for name (supports ñ and accents) ===
            If Not System.Text.RegularExpressions.Regex.IsMatch(txtName.Text, "^[A-Za-zÀ-ÿñÑ .-]+$") Then
                MessageBox.Show("Name can only contain letters (including ñ, accents), spaces, periods (.), and hyphens (-).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

            ' === Validate Contact Number ===
            If Not String.IsNullOrWhiteSpace(txtPhoneNumber.Text) Then
                If Not System.Text.RegularExpressions.Regex.IsMatch(txtPhoneNumber.Text, "^\+?\d{11,12}$") Then
                    MessageBox.Show("Contact number must be 11–12 digits and may start with '+'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

            ' Validate Date of Birth
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

    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr = New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Not ValidateInputs() Then Return

            Me.Cursor = Cursors.WaitCursor
            btnSave.Enabled = False

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
                student.Id = currentStudent.Id
                success = StudentRepository.UpdateStudent(student)
            Else
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

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

    Private Sub txtStudentId_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtStudentId.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtName.Focus()
        End If
    End Sub

    ' === Validate Name (letters, ñ, Ñ, spaces, ., - only) ===
    Private Sub txtName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtName.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtEmail.Focus()
            Return
        End If

        Dim validChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZñÑ .-"
        If Not validChars.Contains(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmail.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtPhoneNumber.Focus()
        End If
    End Sub

    ' === Validate Contact Number (digits only, optional +, max 12) ===
    Private Sub txtPhoneNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhoneNumber.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            txtCourse.Focus()
            Return
        End If

        If Char.IsControl(e.KeyChar) Then
            e.Handled = False
            Return
        End If

        ' Allow '+' only if it's the first character
        If e.KeyChar = "+"c Then
            If txtPhoneNumber.SelectionStart <> 0 OrElse txtPhoneNumber.Text.Contains("+") Then
                e.Handled = True
            End If
            Return
        End If

        ' Only digits allowed
        If Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            Return
        End If

        ' Limit to 12 characters total
        Dim nextLength As Integer = txtPhoneNumber.Text.Length
        If txtPhoneNumber.SelectionLength = 0 Then nextLength += 1
        If nextLength > 12 Then e.Handled = True
    End Sub

    Private Sub txtCourse_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCourse.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            e.Handled = True
            dtpDateOfBirth.Focus()
        End If
    End Sub
End Class
