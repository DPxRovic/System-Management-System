Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
    Try
        Dim role = If(currentUser?.Role, "").ToUpperInvariant()

        If role = "ADMIN" OrElse role = "SUPERADMIN" Then
            ' Open AdminForm inside the content panel
            Dim adminForm As New AdminForm(currentUser)
            LoadChildForm(adminForm, "Administration")
            SetActiveButton(btnUsers)
            Logger.LogInfo($"Admin panel opened by {currentUser.Username}")
        Else
            ' Non-admins see the Users message (or a read-only user view)
            SetActiveButton(btnUsers)
            ShowMessage("User Management", "User management module coming soon...")
        End If
    Catch ex As Exception
        Logger.LogError("Error opening admin/users panel", ex)
        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub