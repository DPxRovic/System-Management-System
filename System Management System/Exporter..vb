' ==========================================
' FILENAME: /Utils/Exporter.vb
' PURPOSE: Utility class for exporting data to various formats (CSV, Excel, PDF)
' AUTHOR: System
' DATE: 2025-10-17
' ==========================================

Imports System.Data
Imports System.IO
Imports System.Text

Public Class Exporter

#Region "CSV Export"

    ''' <summary>
    ''' Exports DataTable to CSV file
    ''' </summary>
    ''' <param name="data">DataTable to export</param>
    ''' <param name="filePath">Output file path</param>
    ''' <returns>True if successful</returns>
    Public Shared Function ExportToCSV(data As DataTable, filePath As String) As Boolean
        Try
            If data Is Nothing OrElse data.Rows.Count = 0 Then
                Throw New Exception("No data to export")
            End If

            Dim csv As New StringBuilder()

            ' Write headers
            Dim headers As New List(Of String)()
            For Each column As DataColumn In data.Columns
                headers.Add(EscapeCSVValue(column.ColumnName))
            Next
            csv.AppendLine(String.Join(",", headers))

            ' Write data rows
            For Each row As DataRow In data.Rows
                Dim values As New List(Of String)()
                For Each column As DataColumn In data.Columns
                    Dim value As String = If(IsDBNull(row(column)), "", row(column).ToString())
                    values.Add(EscapeCSVValue(value))
                Next
                csv.AppendLine(String.Join(",", values))
            Next

            ' Write to file
            File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8)

            Logger.LogInfo($"Data exported to CSV: {filePath}")
            Return True

        Catch ex As Exception
            Logger.LogError("Error exporting to CSV", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Escapes CSV values (handles commas, quotes, and line breaks)
    ''' </summary>
    ''' <param name="value">Value to escape</param>
    ''' <returns>Escaped value</returns>
    Private Shared Function EscapeCSVValue(value As String) As String
        If String.IsNullOrEmpty(value) Then
            Return ""
        End If

        ' Check if value contains comma, quote, or newline
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(vbCr) OrElse value.Contains(vbLf) Then
            ' Escape quotes by doubling them
            value = value.Replace("""", """""")
            ' Wrap in quotes
            Return $"""{value}"""
        End If

        Return value
    End Function

#End Region

#Region "Excel Export (CSV format compatible with Excel)"

    ''' <summary>
    ''' Exports DataTable to Excel-compatible CSV file
    ''' </summary>
    ''' <param name="data">DataTable to export</param>
    ''' <param name="filePath">Output file path</param>
    ''' <param name="sheetName">Sheet name (optional)</param>
    ''' <returns>True if successful</returns>
    Public Shared Function ExportToExcel(data As DataTable, filePath As String, Optional sheetName As String = "Report") As Boolean
        Try
            ' For now, export as CSV which can be opened in Excel
            ' Future enhancement: Use EPPlus or similar library for native Excel format
            Return ExportToCSV(data, filePath)

        Catch ex As Exception
            Logger.LogError("Error exporting to Excel", ex)
            Throw
        End Try
    End Function

#End Region

#Region "HTML Export"

    ''' <summary>
    ''' Exports DataTable to HTML file
    ''' </summary>
    ''' <param name="data">DataTable to export</param>
    ''' <param name="filePath">Output file path</param>
    ''' <param name="title">Report title</param>
    ''' <returns>True if successful</returns>
    Public Shared Function ExportToHTML(data As DataTable, filePath As String, Optional title As String = "Report") As Boolean
        Try
            If data Is Nothing OrElse data.Rows.Count = 0 Then
                Throw New Exception("No data to export")
            End If

            Dim html As New StringBuilder()

            ' HTML header with styling
            html.AppendLine("<!DOCTYPE html>")
            html.AppendLine("<html>")
            html.AppendLine("<head>")
            html.AppendLine("<meta charset='UTF-8'>")
            html.AppendLine($"<title>{title}</title>")
            html.AppendLine("<style>")
            html.AppendLine("body { font-family: 'Segoe UI', Arial, sans-serif; margin: 20px; background-color: #f5f5f5; }")
            html.AppendLine("h1 { color: #2E8B57; text-align: center; }")
            html.AppendLine("table { border-collapse: collapse; width: 100%; margin-top: 20px; background-color: white; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }")
            html.AppendLine("th { background-color: #2E8B57; color: white; padding: 12px; text-align: left; font-weight: bold; }")
            html.AppendLine("td { border: 1px solid #ddd; padding: 10px; }")
            html.AppendLine("tr:nth-child(even) { background-color: #f9f9f9; }")
            html.AppendLine("tr:hover { background-color: #f0f0f0; }")
            html.AppendLine(".footer { margin-top: 30px; text-align: center; font-size: 12px; color: #777; }")
            html.AppendLine(".present { color: #2E8B57; font-weight: bold; }")
            html.AppendLine(".absent { color: #E74C3C; font-weight: bold; }")
            html.AppendLine(".late { color: #F39C12; font-weight: bold; }")
            html.AppendLine(".excused { color: #9B59B6; font-weight: bold; }")
            html.AppendLine("</style>")
            html.AppendLine("</head>")
            html.AppendLine("<body>")

            ' Title
            html.AppendLine($"<h1>{title}</h1>")
            html.AppendLine($"<p style='text-align: center;'>Generated on {DateTime.Now:MMMM dd, yyyy hh:mm tt}</p>")

            ' Table
            html.AppendLine("<table>")

            ' Table headers
            html.AppendLine("<tr>")
            For Each column As DataColumn In data.Columns
                html.AppendLine($"<th>{column.ColumnName.Replace("_", " ").ToUpper()}</th>")
            Next
            html.AppendLine("</tr>")

            ' Table data
            For Each row As DataRow In data.Rows
                html.AppendLine("<tr>")
                For Each column As DataColumn In data.Columns
                    Dim value As String = If(IsDBNull(row(column)), "", row(column).ToString())

                    ' Apply status styling if this is a status column
                    If column.ColumnName.ToLower() = "status" Then
                        Dim statusClass As String = value.ToLower()
                        html.AppendLine($"<td class='{statusClass}'>{value}</td>")
                    Else
                        html.AppendLine($"<td>{value}</td>")
                    End If
                Next
                html.AppendLine("</tr>")
            Next

            html.AppendLine("</table>")

            ' Footer
            html.AppendLine("<div class='footer'>")
            html.AppendLine($"<p>Total Records: {data.Rows.Count}</p>")
            html.AppendLine("<p>&copy; 2025 CSD Student Management System</p>")
            html.AppendLine("</div>")

            html.AppendLine("</body>")
            html.AppendLine("</html>")

            ' Write to file
            File.WriteAllText(filePath, html.ToString(), Encoding.UTF8)

            Logger.LogInfo($"Data exported to HTML: {filePath}")
            Return True

        Catch ex As Exception
            Logger.LogError("Error exporting to HTML", ex)
            Throw
        End Try
    End Function

#End Region

#Region "Text Export"

    ''' <summary>
    ''' Exports DataTable to plain text file
    ''' </summary>
    ''' <param name="data">DataTable to export</param>
    ''' <param name="filePath">Output file path</param>
    ''' <param name="title">Report title</param>
    ''' <returns>True if successful</returns>
    Public Shared Function ExportToText(data As DataTable, filePath As String, Optional title As String = "Report") As Boolean
        Try
            If data Is Nothing OrElse data.Rows.Count = 0 Then
                Throw New Exception("No data to export")
            End If

            Dim text As New StringBuilder()

            ' Title
            text.AppendLine(title)
            text.AppendLine("=" & New String("="c, title.Length))
            text.AppendLine($"Generated: {DateTime.Now:MMMM dd, yyyy hh:mm tt}")
            text.AppendLine()

            ' Calculate column widths
            Dim columnWidths As New Dictionary(Of String, Integer)()
            For Each column As DataColumn In data.Columns
                Dim maxWidth As Integer = column.ColumnName.Length
                For Each row As DataRow In data.Rows
                    Dim value As String = If(IsDBNull(row(column)), "", row(column).ToString())
                    If value.Length > maxWidth Then
                        maxWidth = value.Length
                    End If
                Next
                columnWidths(column.ColumnName) = Math.Min(maxWidth + 2, 50) ' Max 50 chars per column
            Next

            ' Headers
            For Each column As DataColumn In data.Columns
                text.Append(column.ColumnName.PadRight(columnWidths(column.ColumnName)))
            Next
            text.AppendLine()

            ' Separator
            For Each column As DataColumn In data.Columns
                text.Append(New String("-"c, columnWidths(column.ColumnName)))
            Next
            text.AppendLine()

            ' Data rows
            For Each row As DataRow In data.Rows
                For Each column As DataColumn In data.Columns
                    Dim value As String = If(IsDBNull(row(column)), "", row(column).ToString())
                    If value.Length > columnWidths(column.ColumnName) - 2 Then
                        value = value.Substring(0, columnWidths(column.ColumnName) - 5) & "..."
                    End If
                    text.Append(value.PadRight(columnWidths(column.ColumnName)))
                Next
                text.AppendLine()
            Next

            ' Footer
            text.AppendLine()
            text.AppendLine($"Total Records: {data.Rows.Count}")

            ' Write to file
            File.WriteAllText(filePath, text.ToString(), Encoding.UTF8)

            Logger.LogInfo($"Data exported to text: {filePath}")
            Return True

        Catch ex As Exception
            Logger.LogError("Error exporting to text", ex)
            Throw
        End Try
    End Function

#End Region

#Region "JSON Export"

    ''' <summary>
    ''' Exports DataTable to JSON file
    ''' </summary>
    ''' <param name="data">DataTable to export</param>
    ''' <param name="filePath">Output file path</param>
    ''' <returns>True if successful</returns>
    Public Shared Function ExportToJSON(data As DataTable, filePath As String) As Boolean
        Try
            If data Is Nothing OrElse data.Rows.Count = 0 Then
                Throw New Exception("No data to export")
            End If

            Dim json As New StringBuilder()
            json.AppendLine("[")

            Dim isFirst As Boolean = True
            For Each row As DataRow In data.Rows
                If Not isFirst Then
                    json.AppendLine(",")
                Else
                    isFirst = False
                End If

                json.Append("  {")

                Dim isFirstColumn As Boolean = True
                For Each column As DataColumn In data.Columns
                    If Not isFirstColumn Then
                        json.Append(", ")
                    Else
                        isFirstColumn = False
                    End If

                    Dim value As String = If(IsDBNull(row(column)), "", row(column).ToString())
                    value = EscapeJSONValue(value)

                    json.Append($"""{column.ColumnName}"": ""{value}""")
                Next

                json.Append("}")
            Next

            json.AppendLine()
            json.AppendLine("]")

            ' Write to file
            File.WriteAllText(filePath, json.ToString(), Encoding.UTF8)

            Logger.LogInfo($"Data exported to JSON: {filePath}")
            Return True

        Catch ex As Exception
            Logger.LogError("Error exporting to JSON", ex)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Escapes JSON values
    ''' </summary>
    ''' <param name="value">Value to escape</param>
    ''' <returns>Escaped value</returns>
    Private Shared Function EscapeJSONValue(value As String) As String
        If String.IsNullOrEmpty(value) Then
            Return ""
        End If

        value = value.Replace("\", "\\")
        value = value.Replace("""", "\""")
        value = value.Replace(vbCr, "\r")
        value = value.Replace(vbLf, "\n")
        value = value.Replace(vbTab, "\t")

        Return value
    End Function

#End Region

#Region "Statistics Export"

    ''' <summary>
    ''' Exports statistics dictionary to formatted text file
    ''' </summary>
    ''' <param name="stats">Dictionary with statistics</param>
    ''' <param name="filePath">Output file path</param>
    ''' <param name="title">Report title</param>
    ''' <returns>True if successful</returns>
    Public Shared Function ExportStatistics(stats As Dictionary(Of String, Integer), filePath As String, Optional title As String = "Statistics Report") As Boolean
        Try
            If stats Is Nothing OrElse stats.Count = 0 Then
                Throw New Exception("No statistics to export")
            End If

            Dim text As New StringBuilder()

            ' Title
            text.AppendLine(title)
            text.AppendLine("=" & New String("="c, title.Length))
            text.AppendLine($"Generated: {DateTime.Now:MMMM dd, yyyy hh:mm tt}")
            text.AppendLine()

            ' Statistics
            For Each kvp In stats
                text.AppendLine($"{kvp.Key}: {kvp.Value}")
            Next

            ' Write to file
            File.WriteAllText(filePath, text.ToString(), Encoding.UTF8)

            Logger.LogInfo($"Statistics exported: {filePath}")
            Return True

        Catch ex As Exception
            Logger.LogError("Error exporting statistics", ex)
            Throw
        End Try
    End Function

#End Region

#Region "Helper Methods"

    ''' <summary>
    ''' Gets safe filename from string
    ''' </summary>
    ''' <param name="filename">Original filename</param>
    ''' <returns>Safe filename</returns>
    Public Shared Function GetSafeFileName(filename As String) As String
        Try
            Dim invalidChars As Char() = Path.GetInvalidFileNameChars()
            Dim safeFilename As String = String.Join("_", filename.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries))
            Return safeFilename
        Catch ex As Exception
            Return "export"
        End Try
    End Function

    ''' <summary>
    ''' Gets file extension from export format
    ''' </summary>
    ''' <param name="format">Export format (CSV, Excel, HTML, Text, JSON)</param>
    ''' <returns>File extension with dot</returns>
    Public Shared Function GetFileExtension(format As String) As String
        Select Case format.ToUpper()
            Case "CSV"
                Return ".csv"
            Case "EXCEL"
                Return ".csv" ' Using CSV for Excel compatibility
            Case "HTML"
                Return ".html"
            Case "TEXT", "TXT"
                Return ".txt"
            Case "JSON"
                Return ".json"
            Case Else
                Return ".txt"
        End Select
    End Function

    ''' <summary>
    ''' Generates default filename for export
    ''' </summary>
    ''' <param name="prefix">Filename prefix</param>
    ''' <param name="format">Export format</param>
    ''' <returns>Generated filename</returns>
    Public Shared Function GenerateFileName(prefix As String, format As String) As String
        Try
            Dim safePrefix As String = GetSafeFileName(prefix)
            Dim timestamp As String = DateTime.Now.ToString("yyyyMMdd_HHmmss")
            Dim extension As String = GetFileExtension(format)
            Return $"{safePrefix}_{timestamp}{extension}"
        Catch ex As Exception
            Return $"export_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
        End Try
    End Function

    ''' <summary>
    ''' Validates export file path
    ''' </summary>
    ''' <param name="filePath">File path to validate</param>
    ''' <returns>True if valid</returns>
    Public Shared Function ValidateFilePath(filePath As String) As Boolean
        Try
            If String.IsNullOrWhiteSpace(filePath) Then
                Return False
            End If

            ' Check if directory exists
            Dim directory As String = Path.GetDirectoryName(filePath)
            If Not String.IsNullOrEmpty(directory) AndAlso Not System.IO.Directory.Exists(directory) Then
                Return False
            End If

            ' Check if filename is valid
            Dim filename As String = Path.GetFileName(filePath)
            If String.IsNullOrWhiteSpace(filename) Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

End Class