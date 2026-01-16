<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmupload
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
        txtUsername = New TextBox()
        Label1 = New Label()
        GroupBox1 = New GroupBox()
        btnAbort = New Button()
        Lable2 = New Label()
        lblStatus = New Label()
        progressBar = New ProgressBar()
        GroupBox2 = New GroupBox()
        btnSelectFolder = New Button()
        Label2 = New Label()
        PanelBG = New Panel()
        PanelBGOverlay = New Panel()
        PanelMove = New Panel()
        Panel2 = New Panel()
        lblClose = New Label()
        lblTitle = New Label()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        PanelBG.SuspendLayout()
        PanelBGOverlay.SuspendLayout()
        PanelMove.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' txtUsername
        ' 
        txtUsername.Location = New Point(142, 24)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(140, 23)
        txtUsername.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(7, 25)
        Label1.Name = "Label1"
        Label1.Size = New Size(131, 30)
        Label1.TabIndex = 2
        Label1.Text = "                     Username*:" & vbCrLf & "(no special characters!)"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(btnAbort)
        GroupBox1.Controls.Add(Lable2)
        GroupBox1.Controls.Add(lblStatus)
        GroupBox1.Controls.Add(progressBar)
        GroupBox1.Location = New Point(9, 106)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(455, 83)
        GroupBox1.TabIndex = 5
        GroupBox1.TabStop = False
        ' 
        ' btnAbort
        ' 
        btnAbort.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnAbort.ForeColor = Color.WhiteSmoke
        btnAbort.Location = New Point(349, 50)
        btnAbort.Name = "btnAbort"
        btnAbort.Size = New Size(97, 26)
        btnAbort.TabIndex = 14
        btnAbort.Text = "Abort Upload"
        btnAbort.UseVisualStyleBackColor = False
        ' 
        ' Lable2
        ' 
        Lable2.AutoSize = True
        Lable2.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Lable2.Location = New Point(10, 1)
        Lable2.Name = "Lable2"
        Lable2.Size = New Size(39, 15)
        Lable2.TabIndex = 8
        Lable2.Text = "Status"
        ' 
        ' lblStatus
        ' 
        lblStatus.Location = New Point(8, 49)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(335, 20)
        lblStatus.TabIndex = 6
        lblStatus.Text = "Status: idle..."
        lblStatus.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' progressBar
        ' 
        progressBar.Location = New Point(8, 21)
        progressBar.Name = "progressBar"
        progressBar.Size = New Size(441, 23)
        progressBar.TabIndex = 5
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(btnSelectFolder)
        GroupBox2.Controls.Add(Label2)
        GroupBox2.Controls.Add(txtUsername)
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Location = New Point(9, 31)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(455, 69)
        GroupBox2.TabIndex = 6
        GroupBox2.TabStop = False
        ' 
        ' btnSelectFolder
        ' 
        btnSelectFolder.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnSelectFolder.ForeColor = Color.WhiteSmoke
        btnSelectFolder.Location = New Point(299, 23)
        btnSelectFolder.Name = "btnSelectFolder"
        btnSelectFolder.Size = New Size(147, 26)
        btnSelectFolder.TabIndex = 13
        btnSelectFolder.Text = "Select Folder to Upload"
        btnSelectFolder.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label2.Location = New Point(10, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(49, 15)
        Label2.TabIndex = 7
        Label2.Text = "Settings"
        ' 
        ' PanelBG
        ' 
        PanelBG.BackColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        PanelBG.Controls.Add(PanelBGOverlay)
        PanelBG.Dock = DockStyle.Fill
        PanelBG.Location = New Point(0, 0)
        PanelBG.Name = "PanelBG"
        PanelBG.Size = New Size(478, 202)
        PanelBG.TabIndex = 7
        ' 
        ' PanelBGOverlay
        ' 
        PanelBGOverlay.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        PanelBGOverlay.Controls.Add(GroupBox2)
        PanelBGOverlay.Controls.Add(GroupBox1)
        PanelBGOverlay.Controls.Add(PanelMove)
        PanelBGOverlay.Location = New Point(1, 1)
        PanelBGOverlay.Name = "PanelBGOverlay"
        PanelBGOverlay.Size = New Size(476, 198)
        PanelBGOverlay.TabIndex = 0
        ' 
        ' PanelMove
        ' 
        PanelMove.Controls.Add(Panel2)
        PanelMove.Controls.Add(lblTitle)
        PanelMove.Dock = DockStyle.Top
        PanelMove.Location = New Point(0, 0)
        PanelMove.Name = "PanelMove"
        PanelMove.Size = New Size(476, 24)
        PanelMove.TabIndex = 12
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(lblClose)
        Panel2.Location = New Point(445, 1)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(27, 21)
        Panel2.TabIndex = 9
        ' 
        ' lblClose
        ' 
        lblClose.AutoSize = True
        lblClose.Font = New Font("Alef", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblClose.Location = New Point(6, 3)
        lblClose.Name = "lblClose"
        lblClose.Size = New Size(15, 16)
        lblClose.TabIndex = 0
        lblClose.Text = "X"
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Alef", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitle.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        lblTitle.Location = New Point(8, 1)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(80, 22)
        lblTitle.TabIndex = 2
        lblTitle.Text = "Uploader"
        ' 
        ' frmUpload
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        ClientSize = New Size(478, 202)
        Controls.Add(PanelBG)
        ForeColor = Color.WhiteSmoke
        FormBorderStyle = FormBorderStyle.None
        Name = "frmUpload"
        Text = "[#] Comp Snapshot Uploader BETA [#]"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        PanelBG.ResumeLayout(False)
        PanelBGOverlay.ResumeLayout(False)
        PanelMove.ResumeLayout(False)
        PanelMove.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents txtUsername As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents progressBar As ProgressBar
    Friend WithEvents Label2 As Label
    Friend WithEvents Lable2 As Label
    Friend WithEvents PanelBG As Panel
    Friend WithEvents PanelBGOverlay As Panel
    Friend WithEvents PanelMove As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents btnSelectFolder As Button
    Friend WithEvents btnAbort As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblClose As Label
End Class
