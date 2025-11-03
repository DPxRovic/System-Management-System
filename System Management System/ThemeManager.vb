' ==========================================
' FILENAME: /Utils/ThemeManager.vb
' PURPOSE: Manages consistent theming, colors, shadows, and animations
' AUTHOR: System
' DATE: 2025-10-15
' ==========================================

Imports Guna.UI2.WinForms

Public Class ThemeManager

    ' Color Palette
    Public Shared ReadOnly PrimaryColor As Color = Color.FromArgb(46, 139, 87)        ' Dark Green
    Public Shared ReadOnly AccentColor As Color = Color.FromArgb(26, 188, 156)        ' Teal
    Public Shared ReadOnly BackgroundColor As Color = Color.FromArgb(244, 246, 247)   ' Light Gray
    Public Shared ReadOnly TextColor As Color = Color.FromArgb(44, 62, 80)            ' Dark Blue-Gray
    Public Shared ReadOnly SecondaryTextColor As Color = Color.FromArgb(127, 140, 141) ' Gray
    Public Shared ReadOnly SuccessColor As Color = Color.FromArgb(46, 204, 113)       ' Green
    Public Shared ReadOnly WarningColor As Color = Color.FromArgb(243, 156, 18)       ' Orange
    Public Shared ReadOnly DangerColor As Color = Color.FromArgb(231, 76, 60)         ' Red
    Public Shared ReadOnly InfoColor As Color = Color.FromArgb(52, 152, 219)          ' Blue
    Public Shared ReadOnly WhiteColor As Color = Color.White
    Public Shared ReadOnly SidebarColor As Color = Color.FromArgb(44, 62, 80)

    ' Font Settings
    Public Shared ReadOnly DefaultFont As Font = New Font("Segoe UI", 10.5F)
    Public Shared ReadOnly HeaderFont As Font = New Font("Segoe UI", 11.0F, FontStyle.Bold)
    Public Shared ReadOnly TitleFont As Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
    Public Shared ReadOnly LargeTitleFont As Font = New Font("Segoe UI", 18.0F, FontStyle.Bold)

    ' Shadow Settings
    Public Shared ReadOnly ShadowDepth As Integer = 5
    Public Shared ReadOnly ShadowDepthHeavy As Integer = 10

    ' Border Radius
    Public Shared ReadOnly BorderRadiusSmall As Integer = 5
    Public Shared ReadOnly BorderRadiusMedium As Integer = 8
    Public Shared ReadOnly BorderRadiusLarge As Integer = 10
    Public Shared ReadOnly BorderRadiusXLarge As Integer = 15

    ' Padding Settings
    Public Shared ReadOnly PaddingSmall As New Padding(10)
    Public Shared ReadOnly PaddingMedium As New Padding(15)
    Public Shared ReadOnly PaddingLarge As New Padding(20)

    ' --- Small helper data class used for animation timers (strongly typed) ---
    Private Class AnimationData
        Public Property Control As Control
        Public Property Duration As Integer
        Public Property Elapsed As Integer
        Public Property Callback As Action

        Public Sub New(ctrl As Control, duration As Integer, Optional callback As Action = Nothing)
            Me.Control = ctrl
            Me.Duration = duration
            Me.Elapsed = 0
            Me.Callback = callback
        End Sub
    End Class

    ''' <summary>
    ''' Applies shadow to a Guna2Panel
    ''' </summary>
    Public Shared Sub ApplyShadow(panel As Guna2Panel, Optional depth As Integer = 5)
        panel.ShadowDecoration.Enabled = True
        panel.ShadowDecoration.Depth = depth
        panel.ShadowDecoration.BorderRadius = panel.BorderRadius
    End Sub

    ''' <summary>
    ''' Applies shadow to a Guna2GroupBox
    ''' </summary>
    Public Shared Sub ApplyShadow(groupBox As Guna2GroupBox, Optional depth As Integer = 5)
        groupBox.ShadowDecoration.Enabled = True
        groupBox.ShadowDecoration.Depth = depth
        groupBox.ShadowDecoration.BorderRadius = groupBox.BorderRadius
    End Sub

    ''' <summary>
    ''' Applies shadow to a Guna2Button
    ''' </summary>
    Public Shared Sub ApplyShadow(button As Guna2Button, Optional depth As Integer = 3)
        button.ShadowDecoration.Enabled = True
        button.ShadowDecoration.Depth = depth
    End Sub

    ''' <summary>
    ''' Creates a styled card panel
    ''' </summary>
    Public Shared Function CreateCard(width As Integer, height As Integer, Optional backgroundColor As Color? = Nothing) As Guna2Panel
        Dim card As New Guna2Panel With {
            .Size = New Size(width, height),
            .BorderRadius = BorderRadiusLarge,
            .FillColor = If(backgroundColor, WhiteColor),
            .Padding = PaddingMedium
        }

        ApplyShadow(card)
        Return card
    End Function

    ''' <summary>
    ''' Creates a styled button
    ''' </summary>
    Public Shared Function CreateButton(text As String, buttonColor As Color, Optional width As Integer = 150, Optional height As Integer = 40) As Guna2Button
        Dim btn As New Guna2Button With {
            .Text = text,
            .Size = New Size(width, height),
            .BorderRadius = BorderRadiusMedium,
            .FillColor = buttonColor,
            .Font = DefaultFont,
            .ForeColor = WhiteColor
        }

        ' Set hover color (15% darker)
        Dim hoverColor As Color = DarkenColor(buttonColor, 15)
        btn.HoverState.FillColor = hoverColor

        Return btn
    End Function

    ''' <summary>
    ''' Creates a primary button
    ''' </summary>
    Public Shared Function CreatePrimaryButton(text As String, Optional width As Integer = 150, Optional height As Integer = 40) As Guna2Button
        Return CreateButton(text, PrimaryColor, width, height)
    End Function

    ''' <summary>
    ''' Creates a success button
    ''' </summary>
    Public Shared Function CreateSuccessButton(text As String, Optional width As Integer = 150, Optional height As Integer = 40) As Guna2Button
        Return CreateButton(text, SuccessColor, width, height)
    End Function

    ''' <summary>
    ''' Creates a danger button
    ''' </summary>
    Public Shared Function CreateDangerButton(text As String, Optional width As Integer = 150, Optional height As Integer = 40) As Guna2Button
        Return CreateButton(text, DangerColor, width, height)
    End Function

    ''' <summary>
    ''' Creates a warning button
    ''' </summary>
    Public Shared Function CreateWarningButton(text As String, Optional width As Integer = 150, Optional height As Integer = 40) As Guna2Button
        Return CreateButton(text, WarningColor, width, height)
    End Function

    ''' <summary>
    ''' Darkens a color by a percentage
    ''' </summary>
    Public Shared Function DarkenColor(baseColor As Color, percentage As Integer) As Color
        Dim factor As Double = 1 - (percentage / 100.0)
        Return Color.FromArgb(
            baseColor.A,
            CInt(baseColor.R * factor),
            CInt(baseColor.G * factor),
            CInt(baseColor.B * factor)
        )
    End Function

    ''' <summary>
    ''' Lightens a color by a percentage
    ''' </summary>
    Public Shared Function LightenColor(baseColor As Color, percentage As Integer) As Color
        Dim factor As Double = percentage / 100.0
        Return Color.FromArgb(
            baseColor.A,
            CInt(baseColor.R + (255 - baseColor.R) * factor),
            CInt(baseColor.G + (255 - baseColor.G) * factor),
            CInt(baseColor.B + (255 - baseColor.B) * factor)
        )
    End Function

    ''' <summary>
    ''' Applies fade-in animation to a control (only Forms support Opacity)
    ''' </summary>
    Public Shared Sub FadeIn(control As Control, Optional duration As Integer = 300)
        Try
            ' Only set opacity if the control is a Form
            If TypeOf control Is Form Then
                DirectCast(control, Form).Opacity = 0.0
            End If

            Dim timer As New Timer With {
                .Interval = 10,
                .Tag = New AnimationData(control, duration)
            }

            AddHandler timer.Tick, Sub(sender, e)
                                       Dim t = DirectCast(sender, Timer)
                                       Dim data = DirectCast(t.Tag, AnimationData)
                                       Dim ctrl = data.Control

                                       data.Elapsed += t.Interval
                                       Dim progress As Double = Math.Min(data.Elapsed / data.Duration, 1.0)

                                       If TypeOf ctrl Is Form Then
                                           DirectCast(ctrl, Form).Opacity = progress
                                       End If

                                       If progress >= 1.0 Then
                                           t.Stop()
                                           t.Dispose()
                                       End If
                                   End Sub

            timer.Start()
        Catch ex As Exception
            Logger.LogError("Error applying fade-in animation", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Applies fade-out animation to a control (only Forms support Opacity)
    ''' </summary>
    Public Shared Sub FadeOut(control As Control, Optional duration As Integer = 300, Optional callback As Action = Nothing)
        Try
            Dim timer As New Timer With {
                .Interval = 0,
                .Tag = New AnimationData(control, duration, callback)
            }

            AddHandler timer.Tick, Sub(sender, e)
                                       Dim t = DirectCast(sender, Timer)
                                       Dim data = DirectCast(t.Tag, AnimationData)
                                       Dim ctrl = data.Control

                                       data.Elapsed += t.Interval
                                       Dim progress As Double = Math.Min(data.Elapsed / data.Duration, 1.0)

                                       If TypeOf ctrl Is Form Then
                                           DirectCast(ctrl, Form).Opacity = 1.0 - progress
                                       End If

                                       If progress >= 1.0 Then
                                           t.Stop()
                                           t.Dispose()
                                           data.Callback?.Invoke()
                                       End If
                                   End Sub

            timer.Start()
        Catch ex As Exception
            Logger.LogError("Error applying fade-out animation", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Applies smooth slide-in animation to a panel
    ''' </summary>
    Public Shared Sub SlideIn(panel As Panel, direction As String, Optional duration As Integer = 300)
        Try
            Dim originalLocation As Point = panel.Location
            Dim startLocation As Point

            Select Case direction.ToUpper()
                Case "LEFT"
                    startLocation = New Point(-panel.Width, panel.Location.Y)
                Case "RIGHT"
                    startLocation = New Point(panel.Parent.Width, panel.Location.Y)
                Case "TOP"
                    startLocation = New Point(panel.Location.X, -panel.Height)
                Case "BOTTOM"
                    startLocation = New Point(panel.Location.X, panel.Parent.Height)
                Case Else
                    startLocation = New Point(-panel.Width, panel.Location.Y)
            End Select

            panel.Location = startLocation

            Dim timer As New Timer With {
                .Interval = 0,
                .Tag = New With {.Panel = panel, .Start = startLocation, .Target = originalLocation, .Duration = duration, .Elapsed = 0}
            }

            AddHandler timer.Tick, Sub(sender, e)
                                       Dim t = DirectCast(sender, Timer)
                                       Dim data = t.Tag

                                       data.Elapsed += t.Interval
                                       Dim progress As Double = Math.Min(data.Elapsed / data.Duration, 1.0)

                                       ' Ease-out interpolation
                                       Dim easeProgress As Double = 1 - Math.Pow(1 - progress, 3)

                                       Dim newX As Integer = CInt(data.Start.X + (data.Target.X - data.Start.X) * easeProgress)
                                       Dim newY As Integer = CInt(data.Start.Y + (data.Target.Y - data.Start.Y) * easeProgress)

                                       data.Panel.Location = New Point(newX, newY)

                                       If progress >= 1.0 Then
                                           data.Panel.Location = data.Target
                                           t.Stop()
                                           t.Dispose()
                                       End If
                                   End Sub

            timer.Start()
        Catch ex As Exception
            Logger.LogError("Error applying slide-in animation", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Styles a DataGridView with theme colors
    ''' </summary>
    Public Shared Sub StyleDataGridView(dgv As Guna2DataGridView)
        Try
            dgv.AlternatingRowsDefaultCellStyle.BackColor = WhiteColor
            dgv.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = WhiteColor
            dgv.ColumnHeadersDefaultCellStyle.Font = HeaderFont
            dgv.DefaultCellStyle.BackColor = WhiteColor
            dgv.DefaultCellStyle.ForeColor = TextColor
            dgv.DefaultCellStyle.SelectionBackColor = AccentColor
            dgv.DefaultCellStyle.SelectionForeColor = WhiteColor
            dgv.GridColor = Color.FromArgb(231, 229, 255)
            dgv.RowHeadersVisible = False
            dgv.AllowUserToAddRows = False
            dgv.AllowUserToDeleteRows = False
            dgv.ReadOnly = True
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Catch ex As Exception
            Logger.LogError("Error styling DataGridView", ex)
        End Try
    End Sub

#Region "NEW METHODS - Add to existing ThemeManager"

    ''' <summary>
    ''' Shows a themed toast notification (NEW METHOD - doesn't affect existing UI)
    ''' </summary>
    Public Shared Sub ShowToast(parentForm As Form, message As String, toastType As String, Optional duration As Integer = 3000)
        Try
            Dim toastColor As Color
            Select Case toastType.ToUpper()
                Case "SUCCESS"
                    toastColor = SuccessColor
                Case "ERROR", "DANGER"
                    toastColor = DangerColor
                Case "WARNING"
                    toastColor = WarningColor
                Case "INFO"
                    toastColor = InfoColor
                Case Else
                    toastColor = InfoColor
            End Select

            Dim toast As New Guna2Panel With {
                .Size = New Size(350, 80),
                .BackColor = toastColor,
                .BorderRadius = BorderRadiusMedium,
                .Location = New Point((parentForm.Width - 350) \ 2, -100),
                .Visible = False
            }

            Dim lblMessage As New Label With {
                .Text = message,
                .Font = DefaultFont,
                .ForeColor = WhiteColor,
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleCenter,
                .AutoEllipsis = True
            }

            toast.Controls.Add(lblMessage)
            parentForm.Controls.Add(toast)
            toast.BringToFront()

            toast.Visible = True
            Dim targetY As Integer = 20
            Dim timer As New Timer With {
                .Interval = 10,
                .Tag = New With {.Panel = toast, .StartY = -100, .TargetY = targetY, .Duration = 300, .Elapsed = 0, .Phase = "in"}
            }

            AddHandler timer.Tick, Sub(sender, e)
                                       Dim t = DirectCast(sender, Timer)
                                       Dim data = t.Tag

                                       If data.Phase = "in" Then
                                           data.Elapsed += t.Interval
                                           Dim progress As Double = Math.Min(data.Elapsed / data.Duration, 1.0)
                                           Dim easeProgress As Double = 1 - Math.Pow(1 - progress, 3)

                                           data.Panel.Location = New Point(data.Panel.Location.X, CInt(data.StartY + (data.TargetY - data.StartY) * easeProgress))

                                           If progress >= 1.0 Then
                                               data.Phase = "wait"
                                               data.Elapsed = 0
                                           End If
                                       ElseIf data.Phase = "wait" Then
                                           data.Elapsed += t.Interval
                                           If data.Elapsed >= duration Then
                                               data.Phase = "out"
                                               data.Elapsed = 0
                                               data.StartY = data.Panel.Location.Y
                                               data.TargetY = -100
                                           End If
                                       Else
                                           data.Elapsed += t.Interval
                                           Dim progress As Double = Math.Min(data.Elapsed / data.Duration, 1.0)
                                           Dim easeProgress As Double = Math.Pow(progress, 3)

                                           data.Panel.Location = New Point(data.Panel.Location.X, CInt(data.StartY + (data.TargetY - data.StartY) * easeProgress))

                                           If progress >= 1.0 Then
                                               t.Stop()
                                               t.Dispose()
                                               parentForm.Controls.Remove(data.Panel)
                                               data.Panel.Dispose()
                                           End If
                                       End If
                                   End Sub

            timer.Start()

        Catch ex As Exception
            Logger.LogError("Error showing toast", ex)
        End Try
    End Sub

#End Region

End Class