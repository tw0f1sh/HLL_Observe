Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Media
Imports System.Drawing.Drawing2D

Public Class SetupModule

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

    Public AllIntersectionPoints As New List(Of Point)
    Public IntersectionLock As New Object


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


    Public Async Sub setupSub()

        Main.lblStatus.Text = "Ready."

        ' 1. Warten und einmal Beep abspielen
        Await Task.Delay(5000)
        WAVbeep()

        Main.lblStatus.Text = "Start recording..."
        Main.pbStatus.Value = 0

        Dim currentThreshold As Integer = Threshold

        ' Nur 2 Screenshots aufnehmen
        Dim tasks As New List(Of Task(Of (index As Integer, bmp As Bitmap)))
        For i As Integer = 0 To 0
            Dim index As Integer = i
            Await Task.Delay(1000)
            tasks.Add(Task.Run(Function() (index, AnalyzeScreenshot(currentThreshold))))
        Next

        Dim results(1) As Bitmap
        Dim totalTasks As Integer = tasks.Count
        Dim completedTasks As Integer = 0

        While tasks.Count > 0
            Dim finishedTask = Await Task.WhenAny(tasks)
            tasks.Remove(finishedTask)
            Dim tup = finishedTask.Result
            results(tup.index) = tup.bmp
            completedTasks += 1
            Dim overallPercent As Integer = CInt((completedTasks / totalTasks) * 100)
            Main.pbStatus.Value = overallPercent
            Main.lblStatus.Text = $"Progress: {completedTasks} / {totalTasks} Screenshots finished."
            Application.DoEvents()
        End While

        Main.lblStatus.Text = "Process done."

        ' INI-Datei schreiben

        Dim count As Integer = 1
        SyncLock IntersectionLock
            For Each pt In AllIntersectionPoints
                iniwrite(INIPath, "offset_points", "Point" & count, pt.X & "," & pt.Y)
                count += 1
            Next
        End SyncLock
        If count < 10 AndAlso count >= 150 Then
            MessageBox.Show("There are less then 10 or more then 150 Points, try again or ask the Creator for Help", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'iniSECTIONDELETE(INIPath, "offset_points")
            'Exit Sub
        End If

        Main.lblStatus.Text = "INI-File written."

        If SaveTeachinImage = True Then
            Dim finalImage As Bitmap = FinalScreenshotWithIntersections()
            Dim finalSavePath As String = Path.Combine(Application.StartupPath, "TeachinImage.png")
            finalImage.Save(finalSavePath, ImageFormat.Png)
            finalImage.Dispose()
        End If
        Main.lblStatus.Text = $"Finish"
        Main.pbStatus.Value = 100
        Main.lblOffsetPoints.Text = iniCount(INIPath, "offset_points")


        ' 3. Nach Abschluss 2 Beeps in kurzem Abstand abspielen
        WAVbeep()
        Await Task.Delay(200)
        WAVbeep()
    End Sub

    ' Analysiert einen Screenshot anhand des Tab1-Algorithmus (unverändert)
    Private Function AnalyzeScreenshot(thresholdValue As Integer) As Bitmap
        Dim screenBounds As Rectangle = GetMapCropRectangle(TXTResolution)
        Dim screenshot As New Bitmap(screenBounds.Width, screenBounds.Height, PixelFormat.Format24bppRgb)
        Using g As Graphics = Graphics.FromImage(screenshot)
            g.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size)
        End Using


        Dim grayImage As Bitmap = ConvertToGrayscale(screenshot)
        Dim percent As Double = thresholdValue
        Dim colorThreshold As Integer = CInt(255 * (percent / 100.0))
        Dim width As Integer = grayImage.Width
        Dim height As Integer = grayImage.Height
        Dim redLines As New List(Of VerticalLine)
        Dim blueLines As New List(Of HorizontalLine)

        ' Horizontale Analyse (nur wenn runLength >= 400)
        For y As Integer = 0 To height - 1
            For x As Integer = 0 To width - 1
                Dim currentColor As Color = grayImage.GetPixel(x, y)
                If x = 0 OrElse Not ColorsAreClose(grayImage.GetPixel(x - 1, y), currentColor, colorThreshold) Then
                    Dim runLength As Integer = 1
                    Dim runEnd As Integer = x
                    For x2 As Integer = x + 1 To width - 1
                        Dim neighborColor As Color = grayImage.GetPixel(x2, y)
                        If ColorsAreClose(neighborColor, currentColor, colorThreshold) Then
                            runLength += 1
                            runEnd = x2
                        Else
                            Exit For
                        End If
                    Next
                    If runLength >= 400 Then
                        blueLines.Add(New HorizontalLine With {
                            .Y = y,
                            .XStart = x,
                            .XEnd = runEnd
                        })
                    End If
                End If
            Next
        Next

        ' Vertikale Analyse
        For x As Integer = 0 To width - 1
            For y As Integer = 0 To height - 1
                Dim currentColor As Color = grayImage.GetPixel(x, y)
                If y = 0 OrElse Not ColorsAreClose(grayImage.GetPixel(x, y - 1), currentColor, colorThreshold) Then
                    Dim runLength As Integer = 1
                    Dim runEnd As Integer = y
                    For y2 As Integer = y + 1 To height - 1
                        Dim neighborColor As Color = grayImage.GetPixel(x, y2)
                        If ColorsAreClose(neighborColor, currentColor, colorThreshold) Then
                            runLength += 1
                            runEnd = y2
                        Else
                            Exit For
                        End If
                    Next
                    If runLength >= 400 Then
                        redLines.Add(New VerticalLine With {
                            .X = x,
                            .YStart = y,
                            .YEnd = runEnd
                        })
                    End If
                End If
            Next
        Next

        Dim resultImage As Bitmap = CType(grayImage.Clone(), Bitmap)
        Using gResult As Graphics = Graphics.FromImage(resultImage)
            Using penBlue As New Pen(Color.Blue, 1)
                For Each line In blueLines
                    gResult.DrawLine(penBlue, line.XStart, line.Y, line.XEnd, line.Y)
                Next
            End Using
            Using penRed As New Pen(Color.Red, 1)
                For Each line In redLines
                    gResult.DrawLine(penRed, line.X, line.YStart, line.X, line.YEnd)
                Next
            End Using
            Dim radius As Integer = 5
            Using penPurple As New Pen(Color.Purple, 2)
                For Each vLine In redLines
                    For Each hLine In blueLines
                        Dim intersectX As Integer = vLine.X
                        Dim intersectY As Integer = hLine.Y
                        If (intersectX >= hLine.XStart AndAlso intersectX <= hLine.XEnd) AndAlso
                           (intersectY >= vLine.YStart AndAlso intersectY <= vLine.YEnd) Then
                            gResult.DrawLine(penPurple, intersectX - radius, intersectY - radius, intersectX + radius, intersectY + radius)
                            gResult.DrawLine(penPurple, intersectX - radius, intersectY + radius, intersectX + radius, intersectY - radius)
                            SyncLock IntersectionLock
                                If Not AllIntersectionPoints.Contains(New Point(intersectX, intersectY)) Then
                                    AllIntersectionPoints.Add(New Point(intersectX, intersectY))
                                End If
                            End SyncLock
                        End If
                    Next
                Next
            End Using
        End Using

        screenshot.Dispose()
        grayImage.Dispose()

        Return resultImage

        resultImage.Dispose()
    End Function

    ' Erzeugt einen finalen Screenshot mit grünen Kreisen an den globalen Schnittpunkten
    Private Function FinalScreenshotWithIntersections() As Bitmap
        Dim screenBounds As Rectangle = GetMapCropRectangle(TXTResolution)
        Dim finalScreenshot As New Bitmap(screenBounds.Width, screenBounds.Height, PixelFormat.Format24bppRgb)

        Using g As Graphics = Graphics.FromImage(finalScreenshot)
            g.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size)
            Dim radius As Integer = 5
            Using penGreen As New Pen(Color.FromArgb(255, 0, 255, 0), 2)
                SyncLock IntersectionLock
                    For Each pt In AllIntersectionPoints
                        g.DrawEllipse(penGreen, pt.X - radius, pt.Y - radius, radius * 2, radius * 2)
                    Next
                End SyncLock
            End Using
        End Using
        Return finalScreenshot
        finalScreenshot.Dispose()
    End Function

    ' Gemeinsame Hilfsfunktion: Konvertiert ein Bitmap in Graustufen.
    Private Function ConvertToGrayscale(ByVal original As Bitmap) As Bitmap
        Dim grayBitmap As New Bitmap(original.Width, original.Height, PixelFormat.Format24bppRgb)
        For y As Integer = 0 To original.Height - 1
            For x As Integer = 0 To original.Width - 1
                Dim originalColor As Color = original.GetPixel(x, y)
                Dim grayValue As Integer = CInt(0.3 * originalColor.R + 0.59 * originalColor.G + 0.11 * originalColor.B)
                Dim grayColor As Color = Color.FromArgb(grayValue, grayValue, grayValue)
                grayBitmap.SetPixel(x, y, grayColor)
            Next
        Next
        Return grayBitmap
    End Function

    ' Gemeinsame Hilfsfunktion: Vergleicht zwei Farben anhand eines Toleranzwerts.
    Private Function ColorsAreClose(c1 As Color, c2 As Color, threshold As Integer) As Boolean
        Dim diffR As Integer = Math.Abs(CInt(c1.R) - CInt(c2.R))
        Dim diffG As Integer = Math.Abs(CInt(c1.G) - CInt(c2.G))
        Dim diffB As Integer = Math.Abs(CInt(c1.B) - CInt(c2.B))
        Return (diffR <= threshold AndAlso diffG <= threshold AndAlso diffB <= threshold)
    End Function

    ' Gemeinsame Hilfsfunktion: Liest die Schnittpunkt-Locations aus der INI-Datei.
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
