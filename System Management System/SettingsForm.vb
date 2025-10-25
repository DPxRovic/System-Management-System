' ==========================================
' FILENAME: /Forms/SimpleSettingsForm.vb
' PURPOSE: Simple, working settings form (Code-Behind Only)
' ==========================================

Imports Guna.UI2.WinForms

Public Class SettingsForm
    Private currentUser As User

    ''' <summary>
    ''' Constructor with user parameter
    ''' </summary>
    Public Sub New(user As User)
        InitializeComponent()
        currentUser = user
    End Sub

    ''' <summary>
    ''' Form load event
    ''' </summary>
    Private Sub SimpleSettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Load current settings
            LoadSettings()

            Logger.LogInfo($"Settings opened by {currentUser.Username}")

        Catch ex As Exception
            Logger.LogError("Error loading settings", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Safely assign integer to Guna2NumericUpDown by clamping to control Minimum/Maximum
    ''' </summary>
    Private Sub SafeSetNumericValue(nud As Guna2NumericUpDown, val As Integer)
        Try
            If nud Is Nothing Then Return
            Dim minV As Decimal = nud.Minimum
            Dim maxV As Decimal = nud.Maximum
            Dim safeVal As Decimal = Math.Max(minV, Math.Min(maxV, CDec(val)))
            nud.Value = safeVal
        Catch ex As Exception
            Logger.LogError($"Error setting numeric value for {If(nud?.Name, "Guna2NumericUpDown")}", ex)
            ' fallback: attempt to set to minimum
            Try
                nud.Value = nud.Minimum
            Catch
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' Backwards-compatible overload for System.Windows.Forms.NumericUpDown (if used elsewhere)
    ''' </summary>
    Private Sub SafeSetNumericValue(nud As NumericUpDown, val As Integer)
        Try
            If nud Is Nothing Then Return
            Dim minV As Decimal = nud.Minimum
            Dim maxV As Decimal = nud.Maximum
            Dim safeVal As Decimal = Math.Max(minV, Math.Min(maxV, CDec(val)))
            nud.Value = safeVal
        Catch ex As Exception
            Logger.LogError($"Error setting numeric value for {If(nud?.Name, "NumericUpDown")}", ex)
            ' fallback: attempt to set to minimum
            Try
                nud.Value = nud.Minimum
            Catch
            End Try
        End Try
    End Sub

    ''' <summary>
    ''' Load settings into controls
    ''' </summary>
    Private Sub LoadSettings()
        Try
            chkEnableNotifications.Checked = Settings.Instance.EnableNotifications

            ' Use SafeSetNumericValue to avoid exceptions when saved value is outside NumericUpDown range
            SafeSetNumericValue(nudNotificationDuration, Settings.Instance.NotificationDuration)
            chkAutoBackup.Checked = Settings.Instance.AutoBackup
            SafeSetNumericValue(nudBackupInterval, Settings.Instance.BackupInterval)
            chkRememberUsername.Checked = Settings.Instance.RememberUsername

            ' Update last backup label
            If Settings.Instance.LastBackupDate <> DateTime.MinValue Then
                lblLastBackup.Text = $"Last backup: {Settings.Instance.LastBackupDate:MM/dd/yyyy hh:mm tt}"
            Else
                lblLastBackup.Text = "Last backup: Never"
            End If

        Catch ex As Exception
            Logger.LogError("Error loading settings values", ex)
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save button click
    ''' </summary>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Update settings instance
            Settings.Instance.EnableNotifications = chkEnableNotifications.Checked
            Settings.Instance.NotificationDuration = CInt(nudNotificationDuration.Value)
            Settings.Instance.AutoBackup = chkAutoBackup.Checked
            Settings.Instance.BackupInterval = CInt(nudBackupInterval.Value)
            Settings.Instance.RememberUsername = chkRememberUsername.Checked

            ' Save to file
            Settings.Instance.Save()

            ' Show success message
            MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Logger.LogInfo($"Settings saved by {currentUser.Username}")

        Catch ex As Exception
            Logger.LogError("Error saving settings", ex)
            MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Cancel button click
    ''' </summary>
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            Logger.LogError("Error closing settings form", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Test notification button click
    ''' </summary>
    Private Sub btnTestNotification_Click(sender As Object, e As EventArgs) Handles btnTestNotification.Click
        Try
            ' Get duration from numeric updown
            Dim duration As Integer = CInt(nudNotificationDuration.Value)

            ' Show toast notification
            ThemeManager.ShowToast(Me, "This is a test notification! 🎉", "INFO", duration)

            Logger.LogInfo("Test notification triggered")

        Catch ex As Exception
            Logger.LogError("Error testing notification", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Backup now button click
    ''' </summary>
    Private Sub btnBackupNow_Click(sender As Object, e As EventArgs) Handles btnBackupNow.Click
        Try
            ' Show information message
            MessageBox.Show("Database backup functionality can be implemented here." & vbCrLf & vbCrLf &
                          "This would typically:" & vbCrLf &
                          "1. Export database to SQL file" & vbCrLf &
                          "2. Save to Backups folder" & vbCrLf &
                          "3. Update last backup date",
                          "Backup Information",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information)

            ' Update last backup date
            Settings.Instance.LastBackupDate = DateTime.Now
            Settings.Instance.Save()

            ' Update label
            lblLastBackup.Text = $"Last backup: {DateTime.Now:MM/dd/yyyy hh:mm tt}"

            Logger.LogInfo("Manual backup triggered")

        Catch ex As Exception
            Logger.LogError("Error during backup", ex)
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handle form closing
    ''' </summary>
    Private Sub SimpleSettingsForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Logger.LogInfo($"Settings form closed by {currentUser.Username}")
        Catch ex As Exception
            Logger.LogError("Error during form closing", ex)
        End Try
    End Sub

End Class