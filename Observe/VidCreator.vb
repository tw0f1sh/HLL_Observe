Imports System.IO
Imports System.IO.Compression
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Globalization
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Runtime.Intrinsics.X86


Public Class VidCreator

    Private AppWindowpoint As System.Drawing.Point
    Private AppXPos1 As Integer
    Private AppYPos1 As Integer

    Private Function getAppPos()
        AppXPos1 = Control.MousePosition.X - Me.Location.X
        AppYPos1 = Control.MousePosition.Y - Me.Location.Y
        Return 0
    End Function

    Private Function setAppPos(btn As MouseEventArgs)
        If btn.Button = MouseButtons.Left Then
            AppWindowpoint = Control.MousePosition
            AppWindowpoint.X -= (AppXPos1)
            AppWindowpoint.Y -= (AppYPos1)
            Me.Location = AppWindowpoint
        End If
        Return 0
    End Function

    Private Class FileItem
        Public Property FilePath As String
        Public Property TimeStamp As DateTime
        Public Property Random As Integer
        Public Property Rendertext As String
        Public Sub New(filePath As String, timeStamp As DateTime, random As Integer, rendertext As String)
            Me.FilePath = filePath
            Me.TimeStamp = timeStamp
            Me.Random = random
            Me.Rendertext = rendertext
        End Sub
    End Class

    Public snapshotfolder As String = ""
    Public dateFolder As String = "output"

    Private Async Sub btnFolder_Click(sender As Object, e As EventArgs) Handles btnFolder.Click
        Using fbd As New FolderBrowserDialog
            fbd.InitialDirectory = Application.StartupPath
            fbd.Description = "Select the Snapshot Folder:"
            fbd.ShowNewFolderButton = False
            fbd.AutoUpgradeEnabled = False

            If fbd.ShowDialog = DialogResult.OK Then
                snapshotfolder = fbd.SelectedPath.ToString
                pbStatus.Value = 0
                lblStatus.Text = "0/0"

                Await Task.Run(Sub() ProcessAndExtractSnapshots())
            End If
        End Using


    End Sub
    Public Sub ProcessAndExtractSnapshots()

        Dim extractedFolder As String = Path.Combine(snapshotfolder, "extracted")
        If Not Directory.Exists(extractedFolder) Then
            Directory.CreateDirectory(extractedFolder)
            UpdateStatusLabel("Create folder: " & extractedFolder)
        End If

        ' Hole alle Dateien im Snapshot-Ordner (ohne Unterordner)
        Dim allFiles As String() = Directory.GetFiles(snapshotfolder)
        Dim fileItems As New List(Of FileItem)
        Dim count As Integer = 0

        For Each filePath As String In allFiles
            'MsgBox("1")
            Dim fileName As String = Path.GetFileNameWithoutExtension(filePath)
            ' Erwartetes Format: "HH-mm-ss_dd-MM-yyyy_randomzahl"
            Dim parts() As String = fileName.Split("_"c)
            If parts.Length = 3 Then
                Try

                    Dim timePart As String = parts(1)   ' "HH-mm-ss"
                    Dim datePart As String = parts(0)   ' "dd-MM-yyyy"
                    dateFolder = datePart
                    Dim randomPart As String = parts(2)   ' randomzahl
                    Dim dateTimeStr As String = datePart & " " & timePart
                    Dim dt As DateTime = DateTime.ParseExact(dateTimeStr, "dd-MM-yyyy HH-mm-ss", CultureInfo.InvariantCulture)
                    Dim rndValue As Integer = 0

                    Dim timestamp As String = timePart.Replace("-", ":")
                    If Not Integer.TryParse(randomPart, rndValue) Then Continue For
                    fileItems.Add(New FileItem(filePath, dt, rndValue, timestamp))
                Catch
                    ' Überspringe Dateien, bei denen das Parsen fehlschlägt.
                End Try
            End If
        Next

        ' Sortiere zuerst nach Datum/Uhrzeit, dann nach Random-Zahl (aufsteigend)
        Dim sortedFiles = fileItems.OrderBy(Function(f) f.TimeStamp).ThenBy(Function(f) f.Random).ToList()

        ' Setze die ProgressBar auf die Gesamtanzahl der Dateien
        UpdateProgress(0, sortedFiles.Count)

        Dim index As Integer = 1
        For Each item In sortedFiles

            Try
                Dim bmp As Bitmap = Nothing
                If cbOldSnapshotFiles.Checked = True Then
                    bmp = LoadCompressedBitmap(item.FilePath)
                Else
                    Try
                        Dim webpBytes() As Byte = IO.File.ReadAllBytes(item.FilePath)
                        bmp = WebPWrapper.DecodeWebP(webpBytes)
                    Catch ex As Exception
                        MessageBox.Show("Error decode webp: " & ex.Message)
                    End Try
                End If
                If bmp IsNot Nothing Then
                    If cbTimestamp.Checked = True Then

                        Dim firstItem As FileItem = sortedFiles(0)

                        'Dim text As String = item.Rendertext
                        Dim uhrzeit_now As String = item.Rendertext
                        Dim uhrzeit_start As String = firstItem.Rendertext
                        Dim dt_current As DateTime = DateTime.ParseExact(uhrzeit_now, "HH:mm:ss", CultureInfo.InvariantCulture)
                        Dim dt_start As DateTime = DateTime.ParseExact(uhrzeit_start, "HH:mm:ss", CultureInfo.InvariantCulture)

                        ' Berechnung der Differenz (größere Zeit minus kleinere Zeit)
                        Dim diff As TimeSpan = dt_current - dt_start

                        ' Formatieren der Zeitdifferenz als String im Format HH:mm:ss
                        Dim text As String = String.Format("{0:00}h {1:00}m {2:00}s", diff.Hours, diff.Minutes, diff.Seconds)
                        Dim fontName As String = "Consolas"
                        Dim fontSize As Single = 35
                        Dim textColor As Color = Color.FromArgb(255, 255, 0, 0)
                        Dim outlineColor As Color = Color.Black
                        Dim outlineWidth As Single = 5
                        Using font As New Font(fontName, fontSize, FontStyle.Bold, GraphicsUnit.Pixel)
                            ' Textgröße ermitteln
                            Dim textSize As SizeF
                            Using g As Graphics = Graphics.FromImage(bmp)
                                textSize = g.MeasureString(text, font)
                            End Using

                            ' Zentrum berechnen
                            Dim textX As Integer = (bmp.Width - textSize.Width) \ 2
                            Dim textY As Integer = 0
                            Dim position As Point = New Point(textX, textY)

                            ' Grafikobjekt zum Zeichnen
                            Using g As Graphics = Graphics.FromImage(bmp)
                                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                                g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit

                                ' Textpfad erzeugen
                                Dim path As New GraphicsPath()
                                path.AddString(text, font.FontFamily, CInt(font.Style), font.Size, position, StringFormat.GenericDefault)

                                ' Outline zeichnen
                                Using outlinePen As New Pen(outlineColor, outlineWidth)
                                    outlinePen.LineJoin = LineJoin.Round
                                    g.DrawPath(outlinePen, path)
                                End Using

                                ' Text füllen
                                Using brush As New SolidBrush(textColor)
                                    g.FillPath(brush, path)
                                End Using
                            End Using
                        End Using
                    End If
                End If
                If cbTimeline.Checked = True Then
                    'BAR
                    Dim barWidth = 600
                    Dim barHeight = 10
                    Dim margin = 30
                    Dim x = (bmp.Width - barWidth) \ 2
                    Dim y = barHeight + margin 'bmp.Height - barHeight - margin

                    Using g As Graphics = Graphics.FromImage(bmp)
                        g.SmoothingMode = SmoothingMode.AntiAlias
                        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit
                        'g.Clear(Color.Gray)

                        ' Hintergrund (transparente Leiste – halbtransparentes Grau)
                        Using bgBrush As New SolidBrush(Color.FromArgb(100, 80, 80, 80))
                            g.FillRectangle(bgBrush, x, y, barWidth, barHeight)
                        End Using

                        ' Outline (schwarzer Rahmen)
                        Using outlinePen As New Pen(Color.Black, 3)
                            g.DrawRectangle(outlinePen, x, y, barWidth, barHeight)
                        End Using

                        ' Fortschritt berechnen
                        Dim progress = CSng(index - 1) / (sortedFiles.Count - 1)
                        Dim filledWidth = CInt(barWidth * progress)

                        ' Füllung (z. B. voll deckendes Blau)
                        Using fillBrush As New SolidBrush(Color.FromArgb(200, 255, 0, 0))
                            g.FillRectangle(fillBrush, x, y, filledWidth, barHeight)
                        End Using

                        Using outlinePen As New Pen(Color.Black, 3)
                            g.DrawRectangle(outlinePen, x, y, barWidth, barHeight)
                        End Using
                    End Using
                End If

                If bmp IsNot Nothing Then
                    Dim outputPath As String = Path.Combine(extractedFolder, index.ToString() & ".png")
                    bmp.Save(outputPath, ImageFormat.Png)
                    bmp.Dispose()
                End If
            Catch
                ' Fehler beim Verarbeiten einzelner Dateien werden stillschweigend übersprungen.
            End Try
            UpdateProgress(index, sortedFiles.Count)
            index += 1
        Next
        File.Create(extractedFolder & "/_index" & dateFolder.ToString).Dispose()
    End Sub

    ''' <summary>
    ''' Aktualisiert die ProgressBar und das Label im UI-thread.
    ''' </summary>
    Private Sub UpdateProgress(current As Integer, total As Integer)
        If pbStatus.InvokeRequired Then
            pbStatus.Invoke(Sub()
                                pbStatus.Maximum = Math.Max(total, 1)
                                pbStatus.Value = current
                                lblStatus.Text = $"{current}/{total}"
                            End Sub)
        Else
            pbStatus.Maximum = Math.Max(total, 1)
            pbStatus.Value = current
            lblStatus.Text = $"{current}/{total}"
        End If
    End Sub

    ''' <summary>
    ''' Aktualisiert das Label (z. B. für Statusmeldungen) im UI-thread.
    ''' </summary>
    Private Sub UpdateStatusLabel(message As String)
        If lblStatus.InvokeRequired Then
            lblStatus.Invoke(Sub()
                                 lblStatus.Text = message
                             End Sub)
        Else
            lblStatus.Text = message
        End If
    End Sub


    ''' <summary>
    ''' Liest die GZip-komprimierte Datei, dekomprimiert sie und gibt ein Bitmap zurück.
    ''' Das Bitmap wird geklont, um spätere GDI+-Fehler zu vermeiden.
    ''' </summary>
    Public Function LoadCompressedBitmap(ByVal filePath As String) As Bitmap
        Try
            Using fs As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                Using gzip As New IO.Compression.GZipStream(fs, IO.Compression.CompressionMode.Decompress)
                    Using ms As New MemoryStream()
                        gzip.CopyTo(ms)
                        ms.Position = 0
                        If ms.Length = 0 Then Return Nothing
                        Dim tempBmp As New Bitmap(ms)
                        Dim resultBmp As New Bitmap(tempBmp)
                        tempBmp.Dispose()
                        Return resultBmp
                    End Using
                End Using
            End Using
        Catch
            Return Nothing
        End Try
    End Function



    Public Extractedfolder As String = ""


    Private Sub btnSelectExtractedFolder_Click(sender As Object, e As EventArgs) Handles btnSelectExtractedFolder.Click
        Using fbd As New FolderBrowserDialog
            fbd.Description = "Select the Extracted Folder:"
            fbd.ShowNewFolderButton = False
            fbd.AutoUpgradeEnabled = False
            fbd.InitialDirectory = Application.StartupPath

            If fbd.ShowDialog = DialogResult.OK Then
                Extractedfolder = fbd.SelectedPath.ToString
                Dim allFiles As String() = Directory.GetFiles(Extractedfolder)
                lblStatus.Text = (allFiles.Count - 1).ToString & "/" & (allFiles.Count - 1).ToString
                tbxFramerate.Text = "1"
                tbxFramerate.Text = "30"
            End If
        End Using
    End Sub



    Private Sub btnBuildVideo_Click(sender As Object, e As EventArgs) Handles btnBuildVideo.Click
        Try
            ' 1. Parameter aus der Oberfläche auslesen
            Dim framerate As Integer = CInt(tbxFramerate.Text)
            Dim codec = If(rb264.Checked, "libx265", "libx264") '"libx264", "libx265")
            Dim startNumber = "1"
            Dim crf = "0"  ' Lossless (beste Qualität)
            Dim pixFmt = "yuv420p"

            ' 2. Eingabeverzeichnis: Der "extracted"-Ordner (Abhängig von deinem Snapshotfolder)
            If Not Directory.Exists(Extractedfolder) Then
                MessageBox.Show("The folder 'extracted' didnt exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' 3. Output-Verzeichnis: "video" neben der Anwendung
            Dim appDir = Application.StartupPath
            Dim videoFolder = Path.Combine(appDir, "video")
            If Not Directory.Exists(videoFolder) Then Directory.CreateDirectory(videoFolder)


            Dim outputFileName = "output.mp4"
            '4. get output name
            ' Suche alle Dateien, die mit "_index" beginnen
            Dim matchingFiles = Directory.GetFiles(Extractedfolder, "_index*")

            If matchingFiles.Length > 0 Then
                ' Beispiel: Wähle die erste gefundene Datei
                Dim fullFilePath = matchingFiles(0)
                ' Hole nur den Dateinamen (ohne Pfad)
                Dim fileName = Path.GetFileNameWithoutExtension(fullFilePath)

                ' Entferne den Präfix "_index"
                Dim processedFileName = fileName
                If fileName.StartsWith("_index") Then
                    outputFileName = fileName.Substring("_index".Length)
                End If
            End If
            Dim currentTime As DateTime = Main.SyncedTime()
            Dim rnd As New Random()
            Dim outputFilePath = Path.Combine(videoFolder, "Match_" & outputFileName & "----Created_" & currentTime.ToString("dd-MM-yyyy_HH-mm-ss") & "----" & rnd.Next(10, 99).ToString() & ".mp4")

            ' 5. ffmpeg-Eingabemuster: Es wird davon ausgegangen, dass die extrahierten Bilder fortlaufend nummeriert sind (1.png, 2.png, …)
            Dim inputPattern = Path.Combine(Extractedfolder, "%d.png")

            ' 6. ffmpeg.exe befindet sich im Anwendungsverzeichnis
            Dim ffmpegPath = Path.Combine(Application.StartupPath, "bin\ffmpeg.exe")
            If Not File.Exists(ffmpegPath) Then
                MessageBox.Show("ffmpeg.exe not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' 7. Bilde die ffmpeg-Commandline zusammen.
            ' Beispiel:
            ' ffmpeg.exe -framerate 30 -start_number 1 -i "extracted\%d.png" -c:v libx264 -crf 0 -pix_fmt yuv420p "video\dd-MM-yyyy.mp4"

            Dim args = $" -y -framerate {framerate} -start_number {startNumber} -i ""{inputPattern}"" -c:v {codec} -crf {crf} -pix_fmt {pixFmt} ""{outputFilePath}"""

            ' 8. Starte ffmpeg
            Dim psi As New ProcessStartInfo With {
                .FileName = ffmpegPath,
                .Arguments = args,
                .UseShellExecute = False,
                .CreateNoWindow = False,
                .RedirectStandardOutput = False,
                .RedirectStandardError = False
            }

            Dim proc = Process.Start(psi)
            ' Optional: Standard-Output und Fehler auslesen, wenn gewünscht.
            'Dim output As String = proc.StandardOutput.ReadToEnd()
            'Dim [error] As String = proc.StandardError.ReadToEnd()
            proc.WaitForExit()

            MessageBox.Show("Video created: " & outputFilePath, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PanelMove_MouseDown(sender As Object, e As MouseEventArgs) Handles PanelMove.MouseDown
        getAppPos()
    End Sub

    Private Sub PanelMove_MouseMove(sender As Object, e As MouseEventArgs) Handles PanelMove.MouseMove
        setAppPos(e)
    End Sub

    Private Sub lblTitle_MouseDown(sender As Object, e As MouseEventArgs) Handles lblTitle.MouseDown
        getAppPos()
    End Sub

    Private Sub lblTitle_MouseMove(sender As Object, e As MouseEventArgs) Handles lblTitle.MouseMove
        setAppPos(e)
    End Sub


    Private Sub Panel2_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel2.MouseClick
        Me.Close()
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        Me.Close()
    End Sub

    Private Sub tbxFramerate_TextChanged(sender As Object, e As EventArgs) Handles tbxFramerate.TextChanged
        Try
            Dim calc As Integer = CInt(lblStatus.Text.Split("/")(0)) / CInt(tbxFramerate.Text)
            tbxVideoSpan.Text = calc.ToString.Replace(",", ".")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub tbxVideoSpan_TextChanged(sender As Object, e As EventArgs) Handles tbxVideoSpan.TextChanged
        Try
            Dim calc As Integer = CInt(lblStatus.Text.Split("/")(0)) / CInt(tbxVideoSpan.Text)
            tbxFramerate.Text = calc.ToString.Replace(",", ".")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FolderBrowserDialog1.ShowDialog()
    End Sub


#Region "SortOut"



#End Region

End Class