Imports System.Net.Http

Public Class changelog
    Private Async Sub changelog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim httpClient As New HttpClient()
        Dim changelogTXT As String = Await httpClient.GetStringAsync(changelogURL)
        rtbChangelogs.Text = changelogTXT
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rtbChangelogs_TextChanged(sender As Object, e As EventArgs) Handles rtbChangelogs.TextChanged

    End Sub
End Class