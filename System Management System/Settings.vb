' ==========================================
' FILENAME: /Utils/Settings.vb (MINIMAL VERSION)
' PURPOSE: Simple settings without changing existing UI
' ==========================================

Imports System.IO
Imports System.Xml.Serialization

Public Class Settings

#Region "Singleton Pattern"
    Private Shared _instance As Settings
    Private Shared ReadOnly _lock As New Object()

    Public Shared ReadOnly Property Instance As Settings
        Get
            SyncLock _lock
                If _instance Is Nothing Then
                    _instance = New Settings()
                    _instance.Load()
                End If
                Return _instance
            End SyncLock
        End Get
    End Property

    Private Sub New()
    End Sub
#End Region

#Region "Basic Settings Only"
    ' Only essential settings that won't break UI
    Public Property ApplicationName As String = "Student Management System"
    Public Property ApplicationVersion As String = "1.0.0"

    ' Notification Settings
    Public Property EnableNotifications As Boolean = True
    Public Property NotificationDuration As Integer = 3000

    ' Database Settings
    Public Property AutoBackup As Boolean = False
    Public Property BackupInterval As Integer = 7
    Public Property LastBackupDate As DateTime = DateTime.MinValue

    ' User Preferences
    Public Property LastLoginUsername As String = ""
    Public Property RememberUsername As Boolean = False
#End Region

#Region "File Management"
    Private ReadOnly Property SettingsFilePath As String
        Get
            Dim appDataPath As String = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "StudentManagementSystem"
            )

            If Not Directory.Exists(appDataPath) Then
                Directory.CreateDirectory(appDataPath)
            End If

            Return Path.Combine(appDataPath, "settings.xml")
        End Get
    End Property

    Public Sub Load()
        Try
            If File.Exists(SettingsFilePath) Then
                Dim serializer As New XmlSerializer(GetType(Settings))
                Using reader As New StreamReader(SettingsFilePath)
                    Dim loadedSettings As Settings = DirectCast(serializer.Deserialize(reader), Settings)
                    CopyProperties(loadedSettings, Me)
                End Using
                Logger.LogInfo("Settings loaded")
            End If
        Catch ex As Exception
            Logger.LogError("Error loading settings", ex)
        End Try
    End Sub

    Public Sub Save()
        Try
            Dim serializer As New XmlSerializer(GetType(Settings))
            Using writer As New StreamWriter(SettingsFilePath)
                serializer.Serialize(writer, Me)
            End Using
            Logger.LogInfo("Settings saved")
        Catch ex As Exception
            Logger.LogError("Error saving settings", ex)
        End Try
    End Sub

    Private Sub CopyProperties(source As Settings, target As Settings)
        Try
            Dim properties = GetType(Settings).GetProperties()
            For Each prop In properties
                If prop.CanWrite Then
                    prop.SetValue(target, prop.GetValue(source))
                End If
            Next
        Catch ex As Exception
            Logger.LogError("Error copying settings", ex)
        End Try
    End Sub
#End Region

End Class