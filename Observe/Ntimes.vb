Imports System.Net
Imports System.Net.Sockets

Public Class Ntimes
    Public Sub SaveMapScreenshot(ByVal bmp As Bitmap)
        Dim ntpTime As DateTime = GetNetworkTime()
        Dim rnd As New Random()
        Dim fileName As String = ntpTime.ToString("yyyyMMdd_HHmmss") & "_" & rnd.Next(1000, 9999).ToString() & ".png"
        Dim savePath As String = IO.Path.Combine(Application.StartupPath, "Screenshots")
        If Not IO.Directory.Exists(savePath) Then
            IO.Directory.CreateDirectory(savePath)
        End If
        Dim fullPath As String = IO.Path.Combine(savePath, fileName)
        Try
            bmp.Save(fullPath, Imaging.ImageFormat.Png)
        Catch ex As Exception
            MessageBox.Show("Fehler beim Speichern des Screenshots: " & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Fragt die aktuelle Zeit von einem NTP-Server (hier: pool.ntp.org) ab.
    ''' </summary>
    Public Function GetNetworkTime() As DateTime
        Const ntpServer As String = "pool.ntp.org"
        Dim ntpData(47) As Byte
        ' NTP-Request initialisieren
        ntpData(0) = &H1B

        Dim addresses As IPAddress() = Dns.GetHostEntry(ntpServer).AddressList
        Dim ipEndPoint As New IPEndPoint(addresses(0), 123)

        Using socket As New Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            socket.Connect(ipEndPoint)
            socket.Send(ntpData)
            socket.ReceiveTimeout = 3000
            socket.Receive(ntpData)
            socket.Close()
        End Using

        ' Aus den empfangenen Bytes wird die Zeit extrahiert (ab Byte 40)
        Const serverReplyTime As Byte = 40
        Dim intPart As UInteger = BitConverter.ToUInt32(ntpData, serverReplyTime)
        Dim fractPart As UInteger = BitConverter.ToUInt32(ntpData, serverReplyTime + 4)

        intPart = SwapEndianness(intPart)
        fractPart = SwapEndianness(fractPart)

        Dim milliseconds As ULong = (intPart * 1000UL) + ((fractPart * 1000UL) \ &H100000000UL)
        Dim networkDateTime As DateTime = (New DateTime(1900, 1, 1)).AddMilliseconds(CLng(milliseconds))
        Return networkDateTime.ToLocalTime()
    End Function

    ''' <summary>
    ''' Hilfsfunktion zum Endian-Wechsel (wichtig für die NTP-Zeitberechnung).
    ''' </summary>
    Private Function SwapEndianness(ByVal x As UInteger) As UInteger
        Return ((x And &HFF) << 24) Or ((x And &HFF00UI) << 8) Or ((x And &HFF0000UI) >> 8) Or ((x And &HFF000000UI) >> 24)
    End Function

End Class
