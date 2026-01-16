<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class changelog
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        rtbChangelogs = New RichTextBox()
        btnClose = New Button()
        SuspendLayout()
        ' 
        ' rtbChangelogs
        ' 
        rtbChangelogs.BackColor = SystemColors.WindowText
        rtbChangelogs.BorderStyle = BorderStyle.FixedSingle
        rtbChangelogs.Dock = DockStyle.Fill
        rtbChangelogs.ForeColor = SystemColors.Control
        rtbChangelogs.Location = New Point(0, 0)
        rtbChangelogs.Name = "rtbChangelogs"
        rtbChangelogs.ReadOnly = True
        rtbChangelogs.Size = New Size(788, 438)
        rtbChangelogs.TabIndex = 0
        rtbChangelogs.Text = ""
        ' 
        ' btnClose
        ' 
        btnClose.BackColor = SystemColors.ControlText
        btnClose.Dock = DockStyle.Bottom
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnClose.ForeColor = Color.FromArgb(CByte(192), CByte(0), CByte(0))
        btnClose.Location = New Point(0, 388)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(788, 50)
        btnClose.TabIndex = 1
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = False
        ' 
        ' changelog
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ControlText
        ClientSize = New Size(788, 438)
        ControlBox = False
        Controls.Add(btnClose)
        Controls.Add(rtbChangelogs)
        ForeColor = SystemColors.Control
        FormBorderStyle = FormBorderStyle.Fixed3D
        Name = "changelog"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "[#] Change Log [#]"
        TopMost = True
        ResumeLayout(False)
    End Sub

    Friend WithEvents rtbChangelogs As RichTextBox
    Friend WithEvents btnClose As Button
End Class
