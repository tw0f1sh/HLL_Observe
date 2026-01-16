<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VidCreator
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
        Panel1 = New Panel()
        GroupBox5 = New GroupBox()
        tbxVideoSpan = New TextBox()
        Label1 = New Label()
        btnBuildVideo = New Button()
        GroupBox6 = New GroupBox()
        rb264 = New RadioButton()
        rb265 = New RadioButton()
        Label9 = New Label()
        tbxFramerate = New TextBox()
        Label3 = New Label()
        btnSelectExtractedFolder = New Button()
        Label7 = New Label()
        GroupBox4 = New GroupBox()
        cbOldSnapshotFiles = New CheckBox()
        cbTimeline = New CheckBox()
        cbTimestamp = New CheckBox()
        pbStatus = New ProgressBar()
        btnFolder = New Button()
        lblStatus = New Label()
        Label4 = New Label()
        Label5 = New Label()
        PanelMove = New Panel()
        Panel2 = New Panel()
        lblClose = New Label()
        lblTitle = New Label()
        Panel3 = New Panel()
        Panel1.SuspendLayout()
        GroupBox5.SuspendLayout()
        GroupBox6.SuspendLayout()
        GroupBox4.SuspendLayout()
        PanelMove.SuspendLayout()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        Panel1.Controls.Add(GroupBox5)
        Panel1.Controls.Add(GroupBox4)
        Panel1.ForeColor = Color.WhiteSmoke
        Panel1.Location = New Point(1, 1)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(233, 413)
        Panel1.TabIndex = 18
        ' 
        ' GroupBox5
        ' 
        GroupBox5.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        GroupBox5.Controls.Add(tbxVideoSpan)
        GroupBox5.Controls.Add(Label1)
        GroupBox5.Controls.Add(btnBuildVideo)
        GroupBox5.Controls.Add(GroupBox6)
        GroupBox5.Controls.Add(tbxFramerate)
        GroupBox5.Controls.Add(Label3)
        GroupBox5.Controls.Add(btnSelectExtractedFolder)
        GroupBox5.Controls.Add(Label7)
        GroupBox5.Location = New Point(3, 167)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(227, 242)
        GroupBox5.TabIndex = 2
        GroupBox5.TabStop = False
        ' 
        ' tbxVideoSpan
        ' 
        tbxVideoSpan.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        tbxVideoSpan.ForeColor = Color.WhiteSmoke
        tbxVideoSpan.Location = New Point(125, 49)
        tbxVideoSpan.Name = "tbxVideoSpan"
        tbxVideoSpan.Size = New Size(93, 23)
        tbxVideoSpan.TabIndex = 19
        tbxVideoSpan.Text = "0"
        tbxVideoSpan.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label1.Location = New Point(2, 52)
        Label1.Name = "Label1"
        Label1.Size = New Size(120, 15)
        Label1.TabIndex = 18
        Label1.Text = "Video duration (Sec.):"
        ' 
        ' btnBuildVideo
        ' 
        btnBuildVideo.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnBuildVideo.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        btnBuildVideo.Location = New Point(21, 188)
        btnBuildVideo.Name = "btnBuildVideo"
        btnBuildVideo.Size = New Size(189, 28)
        btnBuildVideo.TabIndex = 17
        btnBuildVideo.Text = "Build Video"
        btnBuildVideo.UseVisualStyleBackColor = False
        ' 
        ' GroupBox6
        ' 
        GroupBox6.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        GroupBox6.Controls.Add(rb264)
        GroupBox6.Controls.Add(rb265)
        GroupBox6.Controls.Add(Label9)
        GroupBox6.Location = New Point(18, 99)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Size = New Size(190, 69)
        GroupBox6.TabIndex = 16
        GroupBox6.TabStop = False
        ' 
        ' rb264
        ' 
        rb264.AutoSize = True
        rb264.Checked = True
        rb264.Location = New Point(6, 22)
        rb264.Name = "rb264"
        rb264.Size = New Size(156, 19)
        rb264.TabIndex = 13
        rb264.TabStop = True
        rb264.Text = "libx265 (Discord friendly)"
        rb264.UseVisualStyleBackColor = True
        ' 
        ' rb265
        ' 
        rb265.AutoSize = True
        rb265.Location = New Point(6, 42)
        rb265.Name = "rb265"
        rb265.Size = New Size(92, 19)
        rb265.TabIndex = 14
        rb265.Text = "libx264 (fast)"
        rb265.UseVisualStyleBackColor = True
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label9.Location = New Point(5, 1)
        Label9.Name = "Label9"
        Label9.Size = New Size(41, 15)
        Label9.TabIndex = 1
        Label9.Text = "Codec"
        ' 
        ' tbxFramerate
        ' 
        tbxFramerate.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        tbxFramerate.ForeColor = Color.WhiteSmoke
        tbxFramerate.Location = New Point(125, 76)
        tbxFramerate.Name = "tbxFramerate"
        tbxFramerate.Size = New Size(93, 23)
        tbxFramerate.TabIndex = 15
        tbxFramerate.Text = "30"
        tbxFramerate.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label3.Location = New Point(15, 80)
        Label3.Name = "Label3"
        Label3.Size = New Size(106, 15)
        Label3.TabIndex = 14
        Label3.Text = "Framerate (Speed):"
        ' 
        ' btnSelectExtractedFolder
        ' 
        btnSelectExtractedFolder.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnSelectExtractedFolder.ForeColor = Color.WhiteSmoke
        btnSelectExtractedFolder.Location = New Point(21, 15)
        btnSelectExtractedFolder.Name = "btnSelectExtractedFolder"
        btnSelectExtractedFolder.Size = New Size(189, 28)
        btnSelectExtractedFolder.TabIndex = 13
        btnSelectExtractedFolder.Text = "Select Extracted Folder"
        btnSelectExtractedFolder.UseVisualStyleBackColor = False
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label7.Location = New Point(5, 1)
        Label7.Name = "Label7"
        Label7.Size = New Size(43, 15)
        Label7.TabIndex = 1
        Label7.Text = "Extract"
        ' 
        ' GroupBox4
        ' 
        GroupBox4.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        GroupBox4.Controls.Add(cbOldSnapshotFiles)
        GroupBox4.Controls.Add(cbTimeline)
        GroupBox4.Controls.Add(cbTimestamp)
        GroupBox4.Controls.Add(pbStatus)
        GroupBox4.Controls.Add(btnFolder)
        GroupBox4.Controls.Add(lblStatus)
        GroupBox4.Controls.Add(Label4)
        GroupBox4.Controls.Add(Label5)
        GroupBox4.Location = New Point(3, 23)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(227, 138)
        GroupBox4.TabIndex = 1
        GroupBox4.TabStop = False
        ' 
        ' cbOldSnapshotFiles
        ' 
        cbOldSnapshotFiles.AutoSize = True
        cbOldSnapshotFiles.Location = New Point(155, 73)
        cbOldSnapshotFiles.Name = "cbOldSnapshotFiles"
        cbOldSnapshotFiles.Size = New Size(68, 19)
        cbOldSnapshotFiles.TabIndex = 17
        cbOldSnapshotFiles.Text = "OldFiles"
        cbOldSnapshotFiles.UseVisualStyleBackColor = True
        ' 
        ' cbTimeline
        ' 
        cbTimeline.AutoSize = True
        cbTimeline.Location = New Point(33, 16)
        cbTimeline.Name = "cbTimeline"
        cbTimeline.Size = New Size(71, 19)
        cbTimeline.TabIndex = 16
        cbTimeline.Text = "Timeline"
        cbTimeline.UseVisualStyleBackColor = True
        ' 
        ' cbTimestamp
        ' 
        cbTimestamp.AutoSize = True
        cbTimestamp.Location = New Point(122, 16)
        cbTimestamp.Name = "cbTimestamp"
        cbTimestamp.Size = New Size(85, 19)
        cbTimestamp.TabIndex = 15
        cbTimestamp.Text = "Timestamp"
        cbTimestamp.UseVisualStyleBackColor = True
        ' 
        ' pbStatus
        ' 
        pbStatus.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        pbStatus.Location = New Point(8, 99)
        pbStatus.Name = "pbStatus"
        pbStatus.Size = New Size(213, 23)
        pbStatus.TabIndex = 14
        ' 
        ' btnFolder
        ' 
        btnFolder.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnFolder.ForeColor = Color.WhiteSmoke
        btnFolder.Location = New Point(21, 40)
        btnFolder.Name = "btnFolder"
        btnFolder.Size = New Size(189, 28)
        btnFolder.TabIndex = 13
        btnFolder.Text = "Select Snapshot Folder"
        btnFolder.UseVisualStyleBackColor = False
        ' 
        ' lblStatus
        ' 
        lblStatus.Location = New Point(67, 70)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(82, 23)
        lblStatus.TabIndex = 7
        lblStatus.Text = "0/0"
        lblStatus.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label4.Location = New Point(8, 74)
        Label4.Name = "Label4"
        Label4.Size = New Size(59, 15)
        Label4.TabIndex = 6
        Label4.Text = "Extracted:"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label5.Location = New Point(5, 1)
        Label5.Name = "Label5"
        Label5.Size = New Size(43, 15)
        Label5.TabIndex = 1
        Label5.Text = "Extract"
        ' 
        ' PanelMove
        ' 
        PanelMove.Controls.Add(Panel2)
        PanelMove.Controls.Add(lblTitle)
        PanelMove.Location = New Point(1, 1)
        PanelMove.Name = "PanelMove"
        PanelMove.Size = New Size(233, 22)
        PanelMove.TabIndex = 19
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(lblClose)
        Panel2.Location = New Point(205, 1)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(27, 21)
        Panel2.TabIndex = 8
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
        lblTitle.Size = New Size(117, 22)
        lblTitle.TabIndex = 2
        lblTitle.Text = "Video Creator"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Panel3.ForeColor = Color.WhiteSmoke
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(235, 415)
        Panel3.TabIndex = 20
        ' 
        ' VidCreator
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        ClientSize = New Size(235, 415)
        Controls.Add(PanelMove)
        Controls.Add(Panel1)
        Controls.Add(Panel3)
        ForeColor = Color.WhiteSmoke
        FormBorderStyle = FormBorderStyle.None
        Name = "VidCreator"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Form2"
        Panel1.ResumeLayout(False)
        GroupBox5.ResumeLayout(False)
        GroupBox5.PerformLayout()
        GroupBox6.ResumeLayout(False)
        GroupBox6.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        PanelMove.ResumeLayout(False)
        PanelMove.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnFolder As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents btnSelectExtractedFolder As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents pbStatus As ProgressBar
    Friend WithEvents tbxFramerate As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label9 As Label
    Friend WithEvents btnBuildVideo As Button
    Friend WithEvents rb264 As RadioButton
    Friend WithEvents rb265 As RadioButton
    Friend WithEvents PanelMove As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblClose As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents tbxVideoSpan As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cbTimestamp As CheckBox
    Friend WithEvents cbTimeline As CheckBox
    Friend WithEvents cbOldSnapshotFiles As CheckBox
End Class
