Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Module WebPWrapper

    ' P/Invoke-Deklarationen basierend auf der libwebp-API

    <DllImport("libwebp.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Function WebPEncodeBGR(ByVal bgr As IntPtr, ByVal width As Integer, ByVal height As Integer, ByVal stride As Integer, ByVal quality_factor As Single, ByRef output As IntPtr) As UIntPtr
    End Function

    <DllImport("libwebp.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Function WebPDecodeBGR(ByVal data As IntPtr, ByVal data_size As UIntPtr, ByRef width As Integer, ByRef height As Integer) As IntPtr
    End Function

    <DllImport("libwebp.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Sub WebPFree(ByVal ptr As IntPtr)
    End Sub

    ' Wrapper-Funktion zum Encodieren eines Bitmaps in ein WebP-Bytearray
    Public Function EncodeWebP(ByVal bmp As Bitmap, ByVal quality As Single) As Byte()
        Dim bmp24 As Bitmap
        ' Falls das Bitmap nicht bereits im Format24bppRgb vorliegt, konvertieren
        If bmp.PixelFormat <> PixelFormat.Format24bppRgb Then
            bmp24 = New Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb)
            Using g As Graphics = Graphics.FromImage(bmp24)
                g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height)
            End Using
        Else
            bmp24 = bmp
        End If

        Dim width As Integer = bmp24.Width
        Dim height As Integer = bmp24.Height
        Dim stride As Integer = width * 3   ' Bei 24bpp: 3 Bytes pro Pixel, ohne Padding

        ' Pixel-Daten aus dem Bitmap auslesen
        Dim rect As New Rectangle(0, 0, width, height)
        Dim bmpData As BitmapData = bmp24.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb)
        Dim pixelBytes(stride * height - 1) As Byte
        For y As Integer = 0 To height - 1
            Marshal.Copy(IntPtr.Add(bmpData.Scan0, y * bmpData.Stride), pixelBytes, y * stride, stride)
        Next
        bmp24.UnlockBits(bmpData)

        ' Das Array im Speicher pinnen
        Dim handle As GCHandle = GCHandle.Alloc(pixelBytes, GCHandleType.Pinned)
        Dim outputPtr As IntPtr = IntPtr.Zero
        Dim encodedSize As UIntPtr = WebPEncodeBGR(handle.AddrOfPinnedObject(), width, height, stride, quality, outputPtr)
        handle.Free()

        If encodedSize = UIntPtr.Zero OrElse outputPtr = IntPtr.Zero Then
            Throw New Exception("Fehler beim Kodieren in WebP.")
        End If

        Dim size As Integer = CInt(encodedSize)
        Dim webpBytes(size - 1) As Byte
        Marshal.Copy(outputPtr, webpBytes, 0, size)
        WebPFree(outputPtr)

        Return webpBytes
    End Function

    ' Wrapper-Funktion zum Dekodieren eines WebP-Bytearrays in ein Bitmap
    Public Function DecodeWebP(ByVal webpData As Byte()) As Bitmap
        Dim handle As GCHandle = GCHandle.Alloc(webpData, GCHandleType.Pinned)
        Dim width As Integer = 0, height As Integer = 0
        Dim decodedPtr As IntPtr = WebPDecodeBGR(handle.AddrOfPinnedObject(), CType(webpData.Length, UIntPtr), width, height)
        handle.Free()

        If decodedPtr = IntPtr.Zero Then
            Throw New Exception("Fehler beim Dekodieren von WebP.")
        End If

        ' Neues Bitmap erzeugen und die dekodierten Rohdaten zeilenweise übernehmen
        Dim bmp As New Bitmap(width, height, PixelFormat.Format24bppRgb)
        Dim rect As New Rectangle(0, 0, width, height)
        Dim bmpData As BitmapData = bmp.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb)
        Dim stride As Integer = width * 3

        For y As Integer = 0 To height - 1
            Dim srcPtr As IntPtr = IntPtr.Add(decodedPtr, y * stride)
            Dim destPtr As IntPtr = IntPtr.Add(bmpData.Scan0, y * bmpData.Stride)
            Dim rowData(stride - 1) As Byte
            Marshal.Copy(srcPtr, rowData, 0, stride)
            Marshal.Copy(rowData, 0, destPtr, stride)
        Next
        bmp.UnlockBits(bmpData)

        WebPFree(decodedPtr)
        Return bmp
    End Function

End Module
