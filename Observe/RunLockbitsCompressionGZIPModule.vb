Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.IO.Compression

Public Class RunLockbitsCompressionGZIPModule


    ' Prüft genau EINE Datei. Gibt True zurück, wenn gespeichert wurde (Treffer >= 10).
    Public Function ProcessSingleFrame(filePath As String,
                                   Optional doCrop As Boolean = False,
                                   Optional resolutionStr As String = Nothing,
                                   Optional disableUiUpdates As Boolean = True) As Boolean
        ' 1) Dateityp prüfen
        Dim ext As String = System.IO.Path.GetExtension(filePath).ToLowerInvariant()
        If ext <> ".png" AndAlso ext <> ".jpg" AndAlso ext <> ".jpeg" AndAlso ext <> ".bmp" Then
            Return False
        End If

        ' 2) Schnittpunkte laden
        Dim iniPath As String = System.IO.Path.Combine(Application.StartupPath, "settings.ini")
        Dim locations As List(Of Point) = LoadIntersectionsFromINI(iniPath)
        If locations Is Nothing OrElse locations.Count = 0 Then
            Return False
        End If

        ' 3) Parameter wie im Live-Modus
        Dim localThreshold As Integer = VarGlobal.Threshold
        Dim localMinRun As Integer = VarGlobal.MinLenth
        Dim localSeparation As Integer = VarGlobal.LineSpacing
        Dim localTolerance As Integer = VarGlobal.xDetectionTolerance

        ' 4) Ausgabeordner bestimmen
        Dim baseOut As String = SnapshotsFolder
        If String.IsNullOrEmpty(baseOut) Then
            baseOut = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePath), "output")
        End If
        If Not System.IO.Directory.Exists(baseOut) Then
            System.IO.Directory.CreateDirectory(baseOut)
        End If

        ' 5) Bild laden & ggf. croppen
        Using loaded As New Bitmap(filePath)
            Dim working As Bitmap
            If doCrop Then
                Dim res As String = If(String.IsNullOrWhiteSpace(resolutionStr),
                                   $"{loaded.Width}x{loaded.Height}",
                                   resolutionStr)
                Dim cropRect As Rectangle = GetMapCropRectangle(res)  ' nutzt deine bestehende Logik
                cropRect = Rectangle.Intersect(cropRect, New Rectangle(0, 0, loaded.Width, loaded.Height))
                working = loaded.Clone(cropRect, loaded.PixelFormat)
            Else
                working = New Bitmap(loaded)
            End If

            ' Unveränderte Kopie für die spätere Speicherung
            Dim originalCopy As Bitmap = working.Clone(New Rectangle(0, 0, working.Width, working.Height),
                                                   working.PixelFormat)

            ' 6) Erkennung wie im Live-Modus
            Dim matchCount As Integer = 0
            For Each pt In locations
                Dim regionRect As New Rectangle(pt.X - 25, pt.Y - 25, 50, 50)
                regionRect = Rectangle.Intersect(regionRect, New Rectangle(0, 0, working.Width, working.Height))
                If regionRect.Width > 0 AndAlso regionRect.Height > 0 Then
                    If ProcessSubRegion(working, regionRect, localThreshold, localMinRun, localSeparation, localTolerance) Then
                        matchCount += 1
                        'If matchCount >= 15 Then Exit For
                    End If
                End If
            Next

            Dim saved As Boolean = False
            If matchCount >= 10 Then
                Dim nameBase As String = System.IO.Path.GetFileNameWithoutExtension(filePath)
                Dim outBase As String = System.IO.Path.Combine(baseOut, nameBase & "_match")

                Dim scaled As Bitmap = ScaleBitmapToOutputSize(originalCopy)
                Try
                    ' WebP wie im Live-Flow
                    Dim webp() As Byte = WebPWrapper.EncodeWebP(scaled, 90.0F)
                    System.IO.File.WriteAllBytes(outBase & ".webp", webp)
                Catch
                    ' Fallback
                    scaled.Save(outBase & ".png", Imaging.ImageFormat.Png)
                Finally
                    scaled.Dispose()
                End Try

                saved = True

                ' UI-Updates optional deaktivierbar (Cross-Thread vermeiden)
                If Not disableUiUpdates Then
                    Try
                        TXTSnapTaken += 1
                        _formInstance.UpdateStatusLabel(TXTSnapTaken.ToString)
                        _formInstance.UpdateSize(baseOut)
                    Catch
                    End Try
                End If
            End If

            originalCopy.Dispose()
            working.Dispose()
            Return saved
        End Using
    End Function





    ' In RunLockbitsCompressionGZIPModule.vb (innerhalb der Klasse)
    Public Sub ProcessFramesInFolder(folderPath As String,
                                 Optional doCrop As Boolean = False,
                                 Optional resolutionStr As String = Nothing)

        Dim iniPath As String = System.IO.Path.Combine(Application.StartupPath, "settings.ini")
        Dim locations As List(Of Point) = LoadIntersectionsFromINI(iniPath)
        If locations.Count = 0 Then
            Debug.WriteLine("Keine Schnittpunkte in der INI gefunden!")
            Exit Sub
        End If

        ' Ausgabeordner
        Dim outputDir As String = If(String.IsNullOrEmpty(SnapshotsFolder),
                                 System.IO.Path.Combine(folderPath, "output"),
                                 SnapshotsFolder)
        If Not System.IO.Directory.Exists(outputDir) Then
            System.IO.Directory.CreateDirectory(outputDir)
        End If

        Dim localThreshold As Integer = VarGlobal.Threshold
        Dim localMinRun As Integer = VarGlobal.MinLenth
        Dim localSeparation As Integer = VarGlobal.LineSpacing
        Dim localTolerance As Integer = VarGlobal.xDetectionTolerance

        For Each filePath As String In System.IO.Directory.EnumerateFiles(folderPath)
            Dim ext As String = System.IO.Path.GetExtension(filePath).ToLowerInvariant()
            Select Case ext
                Case ".png", ".jpg", ".jpeg", ".bmp"
                Case Else
                    Continue For
            End Select

            Using loaded As New Bitmap(filePath)
                Dim working As Bitmap
                If doCrop Then
                    Dim res As String = If(String.IsNullOrWhiteSpace(resolutionStr),
                                       $"{loaded.Width}x{loaded.Height}",
                                       resolutionStr)
                    Dim cropRect As Rectangle = GetMapCropRectangle(res)
                    cropRect = Rectangle.Intersect(cropRect, New Rectangle(0, 0, loaded.Width, loaded.Height))
                    working = loaded.Clone(cropRect, loaded.PixelFormat)
                Else
                    working = New Bitmap(loaded)
                End If

                Dim originalCopy As Bitmap = working.Clone(New Rectangle(0, 0, working.Width, working.Height),
                                                       working.PixelFormat)

                Dim matchCount As Integer = 0
                For Each pt In locations
                    Dim regionRect As New Rectangle(pt.X - 25, pt.Y - 25, 50, 50)
                    regionRect = Rectangle.Intersect(regionRect, New Rectangle(0, 0, working.Width, working.Height))
                    If regionRect.Width > 0 AndAlso regionRect.Height > 0 Then
                        If ProcessSubRegion(working, regionRect, localThreshold, localMinRun, localSeparation, localTolerance) Then
                            matchCount += 1
                        End If
                    End If
                Next

                If matchCount >= 10 Then ' gleiche Logik wie Live
                    Dim nameBase As String = System.IO.Path.GetFileNameWithoutExtension(filePath)
                    Dim outBase As String = System.IO.Path.Combine(outputDir, nameBase & "_match")

                    Dim scaled As Bitmap = ScaleBitmapToOutputSize(originalCopy)
                    Try
                        Dim webp() As Byte = WebPWrapper.EncodeWebP(scaled, 90.0F)
                        System.IO.File.WriteAllBytes(outBase & ".webp", webp)
                    Catch
                        scaled.Save(outBase & ".png", Imaging.ImageFormat.Png)
                    Finally
                        scaled.Dispose()
                    End Try

                    ' (Optional) deine UI aktualisieren – die Klasse hat eine Form-Referenz
                    Try
                        TXTSnapTaken += 1
                        _formInstance.UpdateStatusLabel(TXTSnapTaken.ToString)
                        _formInstance.UpdateSize(outputDir)
                    Catch
                    End Try
                End If

                originalCopy.Dispose()
                working.Dispose()
            End Using
        Next
    End Sub








    Private ReadOnly _formInstance As Main

    ' Konstruktor, der die Form1-Instanz erwartet
    Public Sub New(form As Main)
        _formInstance = form
    End Sub

    ' --- Strukturen zur Linienbeschreibung ---
    Private Structure VerticalLine
        Public X As Integer
        Public YStart As Integer
        Public YEnd As Integer
    End Structure

    Private Structure HorizontalLine
        Public Y As Integer
        Public XStart As Integer
        Public XEnd As Integer
    End Structure

    ' CancellationTokenSource für den Continuous Mode
    Private continuousCTS As CancellationTokenSource
    Private captureTask As Task

    ' Folder-Pfad, der in Form1 erstellt und gesetzt wird
    Public SnapshotsFolder As String

    ''' <summary>
    ''' Startet die kontinuierliche Aufnahme.
    ''' </summary>
    Public Sub Start()
        If continuousCTS IsNot Nothing Then Return
        continuousCTS = New CancellationTokenSource()
        captureTask = Task.Run(Sub() ContinuousCaptureLoop(continuousCTS.Token))
    End Sub

    ''' <summary>
    ''' Stoppt die kontinuierliche Aufnahme.
    ''' </summary>
    Public Sub [Stop]()
        If continuousCTS IsNot Nothing Then
            continuousCTS.Cancel()
            continuousCTS = Nothing
        End If
    End Sub

    ''' <summary>
    '''  
    ''' 
    ''' MAP FUNKTIONEN
    ''' 
    ''' </summary>


    Public Function GetMapCropRectangle(ByVal resolutionStr As String) As Rectangle
        ' Entferne Leerzeichen und teile an "x"
        Dim parts() As String = resolutionStr.Replace(" ", "").Split("x"c)
        If parts.Length <> 2 Then
            Throw New ArgumentException("Ungültige Auflösung: " & resolutionStr)
        End If

        Dim screenWidth, screenHeight As Integer
        If Not Integer.TryParse(parts(0), screenWidth) OrElse Not Integer.TryParse(parts(1), screenHeight) Then
            Throw New ArgumentException("Ungültige Zahlen in der Auflösung: " & resolutionStr)
        End If

        '''''''''''' OFFSET BEFOR update 17.1
        ' Die Map-Größe entspricht ca. 79,86% der Bildschirmhöhe.
        'Dim mapSize As Integer = CInt(screenHeight * 0.7986)
        ' Der obere Versatz beträgt ca. 8,33% der Bildschirmhöhe.
        'Dim yOffset As Integer = CInt(screenHeight * 0.08333)
        ' Horizontal wird die Map zentriert.
        'Dim xOffset As Integer = CInt((screenWidth - mapSize) / 2)

        '''''''''' OFFSET NACH update 17.1
        ' Neue Map-Größe ≈ 77,22 % der Bildschirmhöhe
        Dim mapSize As Integer = CInt(screenHeight * 0.77222)
        ' Neuer oberer Versatz ≈ 11,11 % der Bildschirmhöhe
        Dim yOffset As Integer = CInt(screenHeight * 0.11111)
        ' Horizontal zentriert wie gehabt
        Dim xOffset As Integer = CInt((screenWidth - mapSize) / 2)

        Return New Rectangle(xOffset, yOffset, mapSize, mapSize)
    End Function

    Public Function CropMap(ByVal screenshot As Bitmap, ByVal resolutionStr As String) As Bitmap
        Dim cropRect As Rectangle = GetMapCropRectangle(resolutionStr)
        ' Hier wird der Bereich des Screenshots ausgeschnitten.
        Dim croppedMap As Bitmap = screenshot.Clone(cropRect, screenshot.PixelFormat)
        Return croppedMap
    End Function

    Public Function ScaleBitmapToOutputSize(ByVal originalBitmap As Bitmap) As Bitmap
        ' Lese die Zielgröße aus der VarGlobal-Variable
        Dim targetSize As Size = VarGlobal.output_map_scale

        ' Erstelle ein neues Bitmap in der Zielgröße
        Dim scaledBitmap As New Bitmap(targetSize.Width, targetSize.Height)

        ' Zeichne das Originalbild in das neue Bitmap und skaliere es dabei
        Using g As Graphics = Graphics.FromImage(scaledBitmap)
            ' Für eine bessere Bildqualität
            g.InterpolationMode = InterpolationMode.HighQualityBicubic
            g.CompositingQuality = CompositingQuality.HighQuality
            g.SmoothingMode = SmoothingMode.HighQuality

            ' Zeichne das Originalbild auf das neue Bitmap, sodass es komplett ausgefüllt wird
            g.DrawImage(originalBitmap, New Rectangle(0, 0, targetSize.Width, targetSize.Height))
        End Using

        Return scaledBitmap
    End Function


    Public Sub SaveCompressedBitmap(ByVal bitmap As Bitmap, ByVal filePath As String)
        ' Speichere die Bitmap zunächst in einem MemoryStream im BMP-Format (BMP ist unkomprimiert).
        Using ms As New MemoryStream()
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp)
            Dim bitmapBytes() As Byte = ms.ToArray()

            ' Schreibe die Byte-Daten in eine Datei und komprimiere sie dabei mit GZip.
            Using fs As New FileStream(filePath, FileMode.Create, FileAccess.Write)
                Using gzip As New GZipStream(fs, CompressionLevel.SmallestSize)
                    gzip.Write(bitmapBytes, 0, bitmapBytes.Length)
                End Using
            End Using
        End Using
    End Sub

    Private Sub ShowDebugImage(ByVal img As Bitmap)
        Dim frmPreview As New Form
        frmPreview.Text = "Debug Preview | Matches:"
        frmPreview.StartPosition = FormStartPosition.CenterScreen
        frmPreview.Size = New Size(img.Width + 20, img.Height + 40)
        Dim pb As New PictureBox
        pb.Dock = DockStyle.Fill
        pb.Image = img
        pb.SizeMode = PictureBoxSizeMode.Zoom
        frmPreview.Controls.Add(pb)
        frmPreview.ShowDialog()  ' Modal anzeigen, damit Du das Bild prüfen kannst
    End Sub


    ''' <summary>
    ''' Führt in einer Schleife den Screenshot durch, verarbeitet die einzelnen Bereiche 
    ''' anhand der INI-Schnittpunktdaten und speichert den unmodifizierten Screenshot,
    ''' falls genügend Treffer gefunden wurden.
    ''' Danach wird für VarGlobal.timeout_between_save bzw. VarGlobal.Interval gewartet.
    ''' </summary>
    Public Async Sub ContinuousCaptureLoop(token As CancellationToken)
        Dim iniPath As String = Path.Combine(Application.StartupPath, "settings.ini")
        Dim locations As List(Of Point) = LoadIntersectionsFromINI(iniPath)
        Dim internal_intervall = iniread(iniPath, "internal_debug", "timeout_between_save")
        Dim internal_intervall_pause = iniread(iniPath, "internal_debug", "timeout_between_check")

        If locations.Count = 0 Then
            Debug.WriteLine("Keine Schnittpunkte in der INI gefunden!")
            Return
        End If
        If internal_intervall = Nothing Then
            internal_intervall = timeout_between_save
        End If
        If internal_intervall_pause = Nothing Then
            internal_intervall_pause = Interval
        End If


        While Not token.IsCancellationRequested
            Try
                ' Screenshot erfassen
                Dim screenBounds As Rectangle = GetMapCropRectangle(TXTResolution)
                Dim screenshot As New Bitmap(screenBounds.Width, screenBounds.Height, PixelFormat.Format24bppRgb)

                Using g As Graphics = Graphics.FromImage(screenshot)
                    g.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size)
                End Using

                'screenshot = ScaleBitmapToOutputSize(screenshot)


                ' Unmodifizierter Screenshot (Klon) zum späteren Speichern
                Dim originalScreenshot As Bitmap = screenshot.Clone(New Rectangle(0, 0, screenshot.Width, screenshot.Height), screenshot.PixelFormat)
                Dim matchCount As Integer = 0

                ' Parameter aus VarGlobal
                Dim localThreshold As Integer = VarGlobal.Threshold
                Dim localMinRun As Integer = VarGlobal.MinLenth
                Dim localSeparation As Integer = VarGlobal.LineSpacing
                Dim localTolerance As Integer = VarGlobal.xDetectionTolerance

                ' Für jeden statischen Schnittpunkt den entsprechenden Bereich prüfen
                For Each pt In locations
                    Dim regionRect As New Rectangle(pt.X - 25, pt.Y - 25, 50, 50)
                    regionRect = Rectangle.Intersect(regionRect, New Rectangle(0, 0, screenshot.Width, screenshot.Height))
                    If regionRect.Width > 0 AndAlso regionRect.Height > 0 Then
                        If ProcessSubRegion(screenshot, regionRect, localThreshold, localMinRun, localSeparation, localTolerance) Then
                            matchCount += 1

                        End If
                    End If
                Next

                'MsgBox(internal_intervall.ToString)
                'MsgBox(internal_intervall_pause.ToString)
                ' (Optional) Debug-Bild: Hier könntest du das Ergebnis z. B. in eine PictureBox laden.
                ' UpdateDebugPictureBox(screenshot.Clone(New Rectangle(0, 0, screenshot.Width, screenshot.Height), screenshot.PixelFormat))
                'MsgBox(matchCount.ToString)
                If matchCount >= 10 Then
                    Dim currentTime As DateTime = Main.SyncedTime()
                    Dim rnd As New Random()
                    Dim picName As String = currentTime.ToString("dd-MM-yyyy_HH-mm-ss") & "_" & rnd.Next(1000, 9999).ToString() '& ".png"
                    Dim fullPath As String = Path.Combine(SnapshotsFolder, picName)
                    Dim scaleoutput = ScaleBitmapToOutputSize(originalScreenshot)
                    Try
                        Dim webpBytes() As Byte = WebPWrapper.EncodeWebP(scaleoutput, 90.0F)
                        IO.File.WriteAllBytes(fullPath & ".webp", webpBytes)
                    Catch ex As Exception
                        MsgBox("Error encode webp: " & ex.Message)
                    End Try
                    'SaveCompressedBitmap(scaleoutput, fullPath)
                    If EnableBeep = True Then
                        WAVbeep()
                    End If
                    TXTSnapTaken += 1
                    _formInstance.UpdateStatusLabel(TXTSnapTaken.ToString)
                    _formInstance.UpdateSize(SnapshotsFolder)
                    scaleoutput.Dispose()
                    originalScreenshot.Dispose()
                    screenshot.Dispose()
                    Await Task.Delay(internal_intervall, token).ConfigureAwait(False) 'VarGlobal.timeout_between_save
                Else
                    originalScreenshot.Dispose()
                    screenshot.Dispose()
                    Await Task.Delay(internal_intervall_pause, token).ConfigureAwait(False) 'VarGlobal.Interval
                End If

            Catch ex As Exception
                Debug.WriteLine("Fehler in ContinuousCaptureLoop: " & ex.Message)
            End Try
        End While
    End Sub

    ''' <summary>
    ''' Analysiert einen 50x50-Bereich (regionRect) des Screenshots.
    ''' Dabei wird zunächst der Bereich extrahiert, in Graustufen konvertiert und 
    ''' anschließend mittels direktem Pufferzugriff (LockBits) horizontal und vertikal 
    ''' nach Linien gesucht. Ergibt sich ein Schnittpunkt (innerhalb der Toleranz), 
    ''' wird True zurückgegeben.
    ''' </summary>
    Private Function ProcessSubRegion(ByVal screenshot As Bitmap,
                                      ByVal regionRect As Rectangle,
                                      ByVal thresholdValue As Integer,
                                      ByVal minRun As Integer,
                                      ByVal separation As Integer,
                                      ByVal tol As Integer) As Boolean
        ' Ausschnitt erstellen
        Dim subBmp As New Bitmap(regionRect.Width, regionRect.Height, PixelFormat.Format24bppRgb)
        Using g As Graphics = Graphics.FromImage(subBmp)
            g.DrawImage(screenshot, New Rectangle(0, 0, regionRect.Width, regionRect.Height), regionRect, GraphicsUnit.Pixel)
        End Using

        ' In Graustufen umwandeln – hier wird die optimierte Methode verwendet.
        Dim graySub As Bitmap = ConvertToGrayscale(subBmp)
        subBmp.Dispose()

        Dim percent As Double = thresholdValue
        Dim colorThreshold As Integer = CInt(255 * (percent / 100.0))

        Dim localBlueLines As New List(Of HorizontalLine)
        Dim localRedLines As New List(Of VerticalLine)

        ' LockBits des Graustufenbilds
        Dim rectGray As New Rectangle(0, 0, graySub.Width, graySub.Height)
        Dim bmpData As BitmapData = graySub.LockBits(rectGray, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb)
        Dim stride As Integer = bmpData.Stride
        Dim height As Integer = graySub.Height
        Dim width As Integer = graySub.Width
        Dim pixelData As Byte() = New Byte(stride * height - 1) {}
        Marshal.Copy(bmpData.Scan0, pixelData, 0, pixelData.Length)
        graySub.UnlockBits(bmpData)
        graySub.Dispose()

        ' Hilfsfunktion: Liefert den Grauwert (Blaukanal genügt, da in Graustufen alle Kanäle gleich sind)
        Dim getPixelValue = Function(x As Integer, y As Integer) As Byte
                                Dim index As Integer = y * stride + x * 3
                                Return pixelData(index)
                            End Function

        ' Horizontale Analyse
        For y As Integer = 0 To height - 1
            Dim rowIndex As Integer = y * stride
            Dim x As Integer = 0
            While x < width
                Dim index As Integer = rowIndex + x * 3
                Dim currentValue As Byte = pixelData(index)
                If x = 0 OrElse Not IsClose(pixelData(rowIndex + (x - 1) * 3), currentValue, colorThreshold) Then
                    Dim runStart As Integer = x
                    Dim runLength As Integer = 1
                    Dim runEnd As Integer = x
                    x += 1
                    While x < width
                        Dim value As Byte = pixelData(rowIndex + x * 3)
                        If IsClose(value, currentValue, colorThreshold) Then
                            runLength += 1
                            runEnd = x
                            x += 1
                        Else
                            Exit While
                        End If
                    End While
                    If runLength >= minRun Then
                        localBlueLines.Add(New HorizontalLine With {.Y = y, .XStart = runStart, .XEnd = runEnd})
                    End If
                Else
                    x += 1
                End If
            End While
        Next

        ' Vertikale Analyse
        For x As Integer = 0 To width - 1
            Dim y As Integer = 0
            While y < height
                Dim index As Integer = y * stride + x * 3
                Dim currentValue As Byte = pixelData(index)
                If y = 0 OrElse Not IsClose(pixelData((y - 1) * stride + x * 3), currentValue, colorThreshold) Then
                    Dim runStart As Integer = y
                    Dim runLength As Integer = 1
                    Dim runEnd As Integer = y
                    y += 1
                    While y < height
                        Dim value As Byte = pixelData(y * stride + x * 3)
                        If IsClose(value, currentValue, colorThreshold) Then
                            runLength += 1
                            runEnd = y
                            y += 1
                        Else
                            Exit While
                        End If
                    End While
                    If runLength >= minRun Then
                        localRedLines.Add(New VerticalLine With {.X = x, .YStart = runStart, .YEnd = runEnd})
                    End If
                Else
                    y += 1
                End If
            End While
        Next

        ' Filterung der Linien anhand des Abstands (separation)
        Dim filteredRed As New List(Of VerticalLine)
        For i As Integer = 0 To localRedLines.Count - 1
            Dim cur = localRedLines(i)
            Dim valid As Boolean = True
            For j As Integer = 0 To localRedLines.Count - 1
                If i = j Then Continue For
                Dim other = localRedLines(j)
                If Math.Abs(cur.X - other.X) < separation Then
                    valid = False
                    Exit For
                End If
            Next
            If valid Then filteredRed.Add(cur)
        Next

        Dim filteredBlue As New List(Of HorizontalLine)
        For i As Integer = 0 To localBlueLines.Count - 1
            Dim cur = localBlueLines(i)
            Dim valid As Boolean = True
            For j As Integer = 0 To localBlueLines.Count - 1
                If i = j Then Continue For
                Dim other = localBlueLines(j)
                If Math.Abs(cur.Y - other.Y) < separation Then
                    valid = False
                    Exit For
                End If
            Next
            If valid Then filteredBlue.Add(cur)
        Next

        Dim centerX As Integer = width \ 2
        Dim centerY As Integer = height \ 2

        Dim matchFound As Boolean = False
        For Each vLine In filteredRed
            For Each hLine In filteredBlue
                Dim intersectX As Integer = vLine.X
                Dim intersectY As Integer = hLine.Y
                If (intersectX >= hLine.XStart AndAlso intersectX <= hLine.XEnd) AndAlso
                   (intersectY >= vLine.YStart AndAlso intersectY <= vLine.YEnd) Then
                    If Math.Abs(intersectX - centerX) <= tol AndAlso Math.Abs(intersectY - centerY) <= tol Then
                        matchFound = True
                        Exit For
                    End If
                End If
            Next
            If matchFound Then Exit For
        Next

        ' Optional: Zeichnet die erkannten Linien und das Rechteck in den Screenshot (nur zu Debuggingzwecken)
        Using gScr As Graphics = Graphics.FromImage(screenshot)
            Dim offsetX As Integer = regionRect.X
            Dim offsetY As Integer = regionRect.Y

            Using penBlue As New Pen(Color.Blue, 1)
                For Each line In filteredBlue
                    gScr.DrawLine(penBlue, offsetX + line.XStart, offsetY + line.Y, offsetX + line.XEnd, offsetY + line.Y)
                Next
            End Using

            Using penRed As New Pen(Color.Red, 1)
                For Each line In filteredRed
                    gScr.DrawLine(penRed, offsetX + line.X, offsetY + line.YStart, offsetX + line.X, offsetY + line.YEnd)
                Next
            End Using

            Using penGreen As New Pen(Color.Green, 3)
                gScr.DrawRectangle(penGreen, regionRect)
            End Using

            If matchFound Then
                Using penPurple As New Pen(Color.Purple, 2)
                    Dim cx As Integer = offsetX + centerX
                    Dim cy As Integer = offsetY + centerY
                    gScr.DrawLine(penPurple, cx - 5, cy - 5, cx + 5, cy + 5)
                    gScr.DrawLine(penPurple, cx - 5, cy + 5, cx + 5, cy - 5)
                End Using
            End If
        End Using

        Return matchFound
    End Function

    ''' <summary>
    ''' Konvertiert ein 24bppRgb-Bitmap in ein Graustufenbild, wobei per LockBits
    ''' direkt mit dem Bildpuffer gearbeitet wird.
    ''' </summary>
    Private Function ConvertToGrayscale(ByVal original As Bitmap) As Bitmap
        Dim width As Integer = original.Width
        Dim height As Integer = original.Height
        Dim grayBitmap As New Bitmap(width, height, PixelFormat.Format24bppRgb)
        Dim rect As New Rectangle(0, 0, width, height)

        Dim originalData As BitmapData = original.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb)
        Dim grayData As BitmapData = grayBitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb)

        Dim strideOriginal As Integer = originalData.Stride
        Dim strideGray As Integer = grayData.Stride

        Dim bytesOriginal(strideOriginal * height - 1) As Byte
        Dim bytesGray(strideGray * height - 1) As Byte

        Marshal.Copy(originalData.Scan0, bytesOriginal, 0, bytesOriginal.Length)
        original.UnlockBits(originalData)

        For y As Integer = 0 To height - 1
            For x As Integer = 0 To width - 1
                Dim indexOriginal As Integer = y * strideOriginal + x * 3
                Dim B As Byte = bytesOriginal(indexOriginal)
                Dim G As Byte = bytesOriginal(indexOriginal + 1)
                Dim R As Byte = bytesOriginal(indexOriginal + 2)
                Dim grayValue As Byte = CByte(Math.Min(255, (0.3 * R + 0.59 * G + 0.11 * B)))
                Dim indexGray As Integer = y * strideGray + x * 3
                bytesGray(indexGray) = grayValue
                bytesGray(indexGray + 1) = grayValue
                bytesGray(indexGray + 2) = grayValue
            Next
        Next

        Marshal.Copy(bytesGray, 0, grayData.Scan0, bytesGray.Length)
        grayBitmap.UnlockBits(grayData)
        Return grayBitmap
    End Function

    ''' <summary>
    ''' Prüft, ob zwei Farben (hier als Bytewerte) innerhalb eines Schwellenwerts liegen.
    ''' </summary>
    Private Function IsClose(value1 As Byte, value2 As Byte, threshold As Integer) As Boolean
        Return Math.Abs(CInt(value1) - CInt(value2)) <= threshold
    End Function

    ''' <summary>
    ''' Vergleicht zwei Farben anhand des übergebenen Schwellenwertes.
    ''' </summary>
    Private Function ColorsAreClose(c1 As Color, c2 As Color, threshold As Integer) As Boolean
        Dim diffR As Integer = Math.Abs(CInt(c1.R) - CInt(c2.R))
        Dim diffG As Integer = Math.Abs(CInt(c1.G) - CInt(c2.G))
        Dim diffB As Integer = Math.Abs(CInt(c1.B) - CInt(c2.B))
        Return (diffR <= threshold AndAlso diffG <= threshold AndAlso diffB <= threshold)
    End Function

    ''' <summary>
    ''' Liest Schnittpunktkoordinaten aus der INI-Datei.
    ''' Erwartetes Format pro Zeile: PointX = x, y
    ''' </summary>
    Private Function LoadIntersectionsFromINI(iniPath As String) As List(Of Point)
        Dim points As New List(Of Point)
        If Not File.Exists(iniPath) Then Return points
        Dim lines() As String = File.ReadAllLines(iniPath)
        For Each line In lines
            If line.Trim().StartsWith("Point") Then
                Dim parts() As String = line.Split("="c)
                If parts.Length = 2 Then
                    Dim coords() As String = parts(1).Split(","c)
                    If coords.Length = 2 Then
                        Dim xVal, yVal As Integer
                        If Integer.TryParse(coords(0).Trim(), xVal) AndAlso Integer.TryParse(coords(1).Trim(), yVal) Then
                            points.Add(New Point(xVal, yVal))
                        End If
                    End If
                End If
            End If
        Next
        Return points
    End Function

End Class
