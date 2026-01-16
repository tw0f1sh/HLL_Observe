<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        PanelBG = New Panel()
        Panel2 = New Panel()
        GroupBox5 = New GroupBox()
        btnDownloader = New Button()
        btnUpdate = New Button()
        btnSnapshotFolder = New Button()
        Label7 = New Label()
        PanelMove = New Panel()
        Label11 = New Label()
        lblTime = New Label()
        lblTitle = New Label()
        GroupBox4 = New GroupBox()
        btnOpenUploader = New Button()
        btnExit = New Button()
        btnVideoCreatorOpen = New Button()
        Label21 = New Label()
        GroupBox3 = New GroupBox()
        lblChangelog = New Label()
        Label16 = New Label()
        lblDiscordURL = New Label()
        Label10 = New Label()
        PictureBox1 = New PictureBox()
        Label3 = New Label()
        Label5 = New Label()
        lblVersion = New Label()
        Label8 = New Label()
        Label14 = New Label()
        GroupBox2 = New GroupBox()
        btnStartTeachingCom = New Button()
        tbxResolution = New TextBox()
        Label15 = New Label()
        btnStartTeachingInf = New Button()
        pbStatus = New ProgressBar()
        lblStatus = New Label()
        Label12 = New Label()
        Label13 = New Label()
        GroupBox1 = New GroupBox()
        CheckBox2 = New CheckBox()
        cbRunInf = New CheckBox()
        lblSnapSize = New Label()
        Label6 = New Label()
        lblSnapTaken = New Label()
        Label4 = New Label()
        lblOffsetPoints = New Label()
        Label2 = New Label()
        Label1 = New Label()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        PanelBG.SuspendLayout()
        Panel2.SuspendLayout()
        GroupBox5.SuspendLayout()
        PanelMove.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox3.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox2.SuspendLayout()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelBG
        ' 
        PanelBG.BackColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        PanelBG.Controls.Add(Panel2)
        PanelBG.Dock = DockStyle.Fill
        PanelBG.Location = New Point(0, 0)
        PanelBG.Name = "PanelBG"
        PanelBG.Size = New Size(435, 450)
        PanelBG.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        Panel2.Controls.Add(GroupBox5)
        Panel2.Controls.Add(PanelMove)
        Panel2.Controls.Add(GroupBox4)
        Panel2.Controls.Add(GroupBox3)
        Panel2.Controls.Add(GroupBox2)
        Panel2.Controls.Add(GroupBox1)
        Panel2.Location = New Point(1, 1)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(433, 448)
        Panel2.TabIndex = 0
        ' 
        ' GroupBox5
        ' 
        GroupBox5.Controls.Add(btnDownloader)
        GroupBox5.Controls.Add(btnUpdate)
        GroupBox5.Controls.Add(btnSnapshotFolder)
        GroupBox5.Controls.Add(Label7)
        GroupBox5.Location = New Point(319, 24)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(101, 138)
        GroupBox5.TabIndex = 13
        GroupBox5.TabStop = False
        GroupBox5.Visible = False
        ' 
        ' btnDownloader
        ' 
        btnDownloader.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnDownloader.ForeColor = Color.WhiteSmoke
        btnDownloader.Location = New Point(6, 95)
        btnDownloader.Name = "btnDownloader"
        btnDownloader.Size = New Size(88, 26)
        btnDownloader.TabIndex = 12
        btnDownloader.Text = "Download"
        btnDownloader.UseVisualStyleBackColor = False
        btnDownloader.Visible = False
        ' 
        ' btnUpdate
        ' 
        btnUpdate.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnUpdate.ForeColor = Color.WhiteSmoke
        btnUpdate.Location = New Point(7, 58)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(87, 28)
        btnUpdate.TabIndex = 11
        btnUpdate.Text = "OfflineAnalyse"
        btnUpdate.UseVisualStyleBackColor = False
        btnUpdate.Visible = False
        ' 
        ' btnSnapshotFolder
        ' 
        btnSnapshotFolder.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnSnapshotFolder.ForeColor = Color.WhiteSmoke
        btnSnapshotFolder.Location = New Point(7, 20)
        btnSnapshotFolder.Name = "btnSnapshotFolder"
        btnSnapshotFolder.Size = New Size(86, 28)
        btnSnapshotFolder.TabIndex = 10
        btnSnapshotFolder.Text = "Snapshot Folder"
        btnSnapshotFolder.UseVisualStyleBackColor = False
        btnSnapshotFolder.Visible = False
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label7.Location = New Point(5, 1)
        Label7.Name = "Label7"
        Label7.Size = New Size(48, 15)
        Label7.TabIndex = 1
        Label7.Text = "Addons"
        Label7.Visible = False
        ' 
        ' PanelMove
        ' 
        PanelMove.Controls.Add(Label11)
        PanelMove.Controls.Add(lblTime)
        PanelMove.Controls.Add(lblTitle)
        PanelMove.Dock = DockStyle.Top
        PanelMove.Location = New Point(0, 0)
        PanelMove.Name = "PanelMove"
        PanelMove.Size = New Size(433, 24)
        PanelMove.TabIndex = 11
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Font = New Font("Alef", 8.25F)
        Label11.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label11.Location = New Point(253, 6)
        Label11.Name = "Label11"
        Label11.Size = New Size(60, 15)
        Label11.TabIndex = 6
        Label11.Text = "NTP Time:"
        ' 
        ' lblTime
        ' 
        lblTime.Font = New Font("Alef", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblTime.Location = New Point(312, 4)
        lblTime.Name = "lblTime"
        lblTime.Size = New Size(117, 15)
        lblTime.TabIndex = 7
        lblTime.Text = "-"
        lblTime.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Alef", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitle.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        lblTitle.Location = New Point(8, 1)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(74, 22)
        lblTitle.TabIndex = 2
        lblTitle.Text = "Observe"
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(btnOpenUploader)
        GroupBox4.Controls.Add(btnExit)
        GroupBox4.Controls.Add(btnVideoCreatorOpen)
        GroupBox4.Controls.Add(Label21)
        GroupBox4.Location = New Point(188, 23)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(125, 138)
        GroupBox4.TabIndex = 10
        GroupBox4.TabStop = False
        ' 
        ' btnOpenUploader
        ' 
        btnOpenUploader.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnOpenUploader.ForeColor = Color.WhiteSmoke
        btnOpenUploader.Location = New Point(7, 23)
        btnOpenUploader.Name = "btnOpenUploader"
        btnOpenUploader.Size = New Size(110, 26)
        btnOpenUploader.TabIndex = 13
        btnOpenUploader.Text = "Uploader"
        btnOpenUploader.UseVisualStyleBackColor = False
        ' 
        ' btnExit
        ' 
        btnExit.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnExit.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        btnExit.Location = New Point(7, 97)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(110, 28)
        btnExit.TabIndex = 12
        btnExit.Text = "Exit"
        btnExit.UseVisualStyleBackColor = False
        ' 
        ' btnVideoCreatorOpen
        ' 
        btnVideoCreatorOpen.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnVideoCreatorOpen.ForeColor = Color.WhiteSmoke
        btnVideoCreatorOpen.Location = New Point(7, 60)
        btnVideoCreatorOpen.Name = "btnVideoCreatorOpen"
        btnVideoCreatorOpen.Size = New Size(110, 26)
        btnVideoCreatorOpen.TabIndex = 11
        btnVideoCreatorOpen.Text = "Video Creator"
        btnVideoCreatorOpen.UseVisualStyleBackColor = False
        ' 
        ' Label21
        ' 
        Label21.AutoSize = True
        Label21.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label21.Location = New Point(5, 1)
        Label21.Name = "Label21"
        Label21.Size = New Size(38, 15)
        Label21.TabIndex = 1
        Label21.Text = "Menu"
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(lblChangelog)
        GroupBox3.Controls.Add(Label16)
        GroupBox3.Controls.Add(lblDiscordURL)
        GroupBox3.Controls.Add(Label10)
        GroupBox3.Controls.Add(PictureBox1)
        GroupBox3.Controls.Add(Label3)
        GroupBox3.Controls.Add(Label5)
        GroupBox3.Controls.Add(lblVersion)
        GroupBox3.Controls.Add(Label8)
        GroupBox3.Controls.Add(Label14)
        GroupBox3.Location = New Point(9, 292)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(414, 151)
        GroupBox3.TabIndex = 10
        GroupBox3.TabStop = False
        ' 
        ' lblChangelog
        ' 
        lblChangelog.Font = New Font("Segoe UI", 9F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        lblChangelog.ForeColor = Color.FromArgb(CByte(255), CByte(35), CByte(35))
        lblChangelog.Location = New Point(249, 103)
        lblChangelog.Name = "lblChangelog"
        lblChangelog.Size = New Size(61, 23)
        lblChangelog.TabIndex = 15
        lblChangelog.Text = "Open"
        lblChangelog.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label16
        ' 
        Label16.AutoSize = True
        Label16.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label16.Location = New Point(176, 106)
        Label16.Name = "Label16"
        Label16.Size = New Size(68, 15)
        Label16.TabIndex = 14
        Label16.Text = "Changelog:"
        ' 
        ' lblDiscordURL
        ' 
        lblDiscordURL.Location = New Point(247, 73)
        lblDiscordURL.Name = "lblDiscordURL"
        lblDiscordURL.Size = New Size(158, 23)
        lblDiscordURL.TabIndex = 13
        lblDiscordURL.Text = "url-auto-parse"
        lblDiscordURL.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label10.Location = New Point(194, 77)
        Label10.Name = "Label10"
        Label10.Size = New Size(50, 15)
        Label10.TabIndex = 12
        Label10.Text = "Discord:"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Location = New Point(8, 17)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(161, 128)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 11
        PictureBox1.TabStop = False
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(247, 19)
        Label3.Name = "Label3"
        Label3.Size = New Size(61, 23)
        Label3.TabIndex = 9
        Label3.Text = "Tw0F1sh"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label5.Location = New Point(196, 23)
        Label5.Name = "Label5"
        Label5.Size = New Size(49, 15)
        Label5.TabIndex = 8
        Label5.Text = "Creator:"
        ' 
        ' lblVersion
        ' 
        lblVersion.Location = New Point(246, 45)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(61, 23)
        lblVersion.TabIndex = 7
        lblVersion.Text = "0.0.0"
        lblVersion.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label8.Location = New Point(196, 49)
        Label8.Name = "Label8"
        Label8.Size = New Size(48, 15)
        Label8.TabIndex = 6
        Label8.Text = "Version:"
        ' 
        ' Label14
        ' 
        Label14.AutoSize = True
        Label14.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label14.Location = New Point(5, 1)
        Label14.Name = "Label14"
        Label14.Size = New Size(40, 15)
        Label14.TabIndex = 1
        Label14.Text = "About"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(btnStartTeachingCom)
        GroupBox2.Controls.Add(tbxResolution)
        GroupBox2.Controls.Add(Label15)
        GroupBox2.Controls.Add(btnStartTeachingInf)
        GroupBox2.Controls.Add(pbStatus)
        GroupBox2.Controls.Add(lblStatus)
        GroupBox2.Controls.Add(Label12)
        GroupBox2.Controls.Add(Label13)
        GroupBox2.Location = New Point(9, 167)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(414, 119)
        GroupBox2.TabIndex = 10
        GroupBox2.TabStop = False
        ' 
        ' btnStartTeachingCom
        ' 
        btnStartTeachingCom.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnStartTeachingCom.Enabled = False
        btnStartTeachingCom.ForeColor = Color.WhiteSmoke
        btnStartTeachingCom.Location = New Point(143, 19)
        btnStartTeachingCom.Name = "btnStartTeachingCom"
        btnStartTeachingCom.Size = New Size(131, 31)
        btnStartTeachingCom.TabIndex = 11
        btnStartTeachingCom.Text = "Teachin Commander"
        btnStartTeachingCom.UseVisualStyleBackColor = False
        btnStartTeachingCom.Visible = False
        ' 
        ' tbxResolution
        ' 
        tbxResolution.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        tbxResolution.ForeColor = Color.WhiteSmoke
        tbxResolution.Location = New Point(303, 42)
        tbxResolution.Name = "tbxResolution"
        tbxResolution.ReadOnly = True
        tbxResolution.Size = New Size(100, 23)
        tbxResolution.TabIndex = 10
        tbxResolution.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label15
        ' 
        Label15.AutoSize = True
        Label15.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label15.Location = New Point(309, 21)
        Label15.Name = "Label15"
        Label15.Size = New Size(90, 15)
        Label15.TabIndex = 9
        Label15.Text = "HLL Resolution:"
        ' 
        ' btnStartTeachingInf
        ' 
        btnStartTeachingInf.BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        btnStartTeachingInf.ForeColor = Color.WhiteSmoke
        btnStartTeachingInf.Location = New Point(6, 19)
        btnStartTeachingInf.Name = "btnStartTeachingInf"
        btnStartTeachingInf.Size = New Size(131, 31)
        btnStartTeachingInf.TabIndex = 7
        btnStartTeachingInf.Text = "Teachin"
        btnStartTeachingInf.UseVisualStyleBackColor = False
        ' 
        ' pbStatus
        ' 
        pbStatus.Location = New Point(5, 79)
        pbStatus.Name = "pbStatus"
        pbStatus.Size = New Size(398, 23)
        pbStatus.TabIndex = 6
        ' 
        ' lblStatus
        ' 
        lblStatus.Location = New Point(53, 53)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(244, 23)
        lblStatus.TabIndex = 5
        lblStatus.Text = "-"
        lblStatus.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label12.Location = New Point(5, 57)
        Label12.Name = "Label12"
        Label12.Size = New Size(42, 15)
        Label12.TabIndex = 3
        Label12.Text = "Status:"
        ' 
        ' Label13
        ' 
        Label13.AutoSize = True
        Label13.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label13.Location = New Point(5, 1)
        Label13.Name = "Label13"
        Label13.Size = New Size(37, 15)
        Label13.TabIndex = 1
        Label13.Text = "Setup"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CheckBox2)
        GroupBox1.Controls.Add(cbRunInf)
        GroupBox1.Controls.Add(lblSnapSize)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(lblSnapTaken)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(lblOffsetPoints)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(9, 23)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(173, 138)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Enabled = False
        CheckBox2.Location = New Point(57, 38)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.RightToLeft = RightToLeft.Yes
        CheckBox2.Size = New Size(106, 19)
        CheckBox2.TabIndex = 12
        CheckBox2.Text = "Run NoTeachIn"
        CheckBox2.UseVisualStyleBackColor = True
        CheckBox2.Visible = False
        ' 
        ' cbRunInf
        ' 
        cbRunInf.AutoSize = True
        cbRunInf.Location = New Point(45, 17)
        cbRunInf.Name = "cbRunInf"
        cbRunInf.Size = New Size(47, 19)
        cbRunInf.TabIndex = 11
        cbRunInf.Text = "Run"
        cbRunInf.UseVisualStyleBackColor = True
        ' 
        ' lblSnapSize
        ' 
        lblSnapSize.Location = New Point(85, 79)
        lblSnapSize.Name = "lblSnapSize"
        lblSnapSize.Size = New Size(68, 23)
        lblSnapSize.TabIndex = 9
        lblSnapSize.Text = "-"
        lblSnapSize.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label6.Location = New Point(8, 83)
        Label6.Name = "Label6"
        Label6.Size = New Size(71, 15)
        Label6.TabIndex = 8
        Label6.Text = "Session size:"
        ' 
        ' lblSnapTaken
        ' 
        lblSnapTaken.Location = New Point(110, 102)
        lblSnapTaken.Name = "lblSnapTaken"
        lblSnapTaken.Size = New Size(58, 23)
        lblSnapTaken.TabIndex = 7
        lblSnapTaken.Text = "-"
        lblSnapTaken.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label4.Location = New Point(8, 106)
        Label4.Name = "Label4"
        Label4.Size = New Size(96, 15)
        Label4.TabIndex = 6
        Label4.Text = "Snapshots taken:"
        ' 
        ' lblOffsetPoints
        ' 
        lblOffsetPoints.Location = New Point(91, 56)
        lblOffsetPoints.Name = "lblOffsetPoints"
        lblOffsetPoints.Size = New Size(68, 23)
        lblOffsetPoints.TabIndex = 5
        lblOffsetPoints.Text = "-"
        lblOffsetPoints.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label2.Location = New Point(7, 60)
        Label2.Name = "Label2"
        Label2.Size = New Size(78, 15)
        Label2.TabIndex = 3
        Label2.Text = "Offset Points:"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.ForeColor = Color.FromArgb(CByte(183), CByte(28), CByte(28))
        Label1.Location = New Point(5, 1)
        Label1.Name = "Label1"
        Label1.Size = New Size(34, 15)
        Label1.TabIndex = 1
        Label1.Text = "Main"
        ' 
        ' Main
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(26), CByte(26), CByte(26))
        ClientSize = New Size(435, 450)
        ControlBox = False
        Controls.Add(PanelBG)
        ForeColor = Color.WhiteSmoke
        FormBorderStyle = FormBorderStyle.None
        Name = "Main"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Form1"
        PanelBG.ResumeLayout(False)
        Panel2.ResumeLayout(False)
        GroupBox5.ResumeLayout(False)
        GroupBox5.PerformLayout()
        PanelMove.ResumeLayout(False)
        PanelMove.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelBG As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lblSnapTaken As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblOffsetPoints As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label14 As Label
    Friend WithEvents btnStartTeachingInf As Button
    Friend WithEvents pbStatus As ProgressBar
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnSnapshotFolder As Button
    Friend WithEvents Label21 As Label
    Friend WithEvents lblDiscordURL As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents tbxResolution As TextBox
    Friend WithEvents PanelMove As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents btnVideoCreatorOpen As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents btnOpenUploader As Button
    Friend WithEvents Label11 As Label
    Friend WithEvents lblTime As Label
    Friend WithEvents cbRunInf As CheckBox
    Friend WithEvents lblSnapSize As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblChangelog As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents btnStartTeachingCom As Button
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents btnDownloader As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog

End Class
