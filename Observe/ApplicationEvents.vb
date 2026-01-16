Imports System.Diagnostics
Imports System.IO
Imports System.Windows.Forms

Namespace My
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            If Not File.Exists(INIPath) Then
                Dim laufendeProzesse() As Process = Process.GetProcessesByName("HLL-Win64-Shipping")
                If laufendeProzesse.Length > 0 Then
                    MessageBox.Show("Warning: Seems like the Hell let Loose process is running, close it and restart the tool!",
                                "HLL Process detected", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    e.Cancel = True
                End If
            End If
        End Sub

    End Class
End Namespace
