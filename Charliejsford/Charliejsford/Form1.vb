'This Program will wait for a key to press in our case it will wait for the F8 key to be press
'Then it will copy the highlighted text (Outside and Inside of the form) and will concatinate the text with a webaddress'
Imports System.Net
Imports Charliejsford.Hotkey
Public Class Form1

    'Global Variable to Store the Highlighted value'

    Dim HighlightedValue As String
    'This Chunk of code will register the F8 key as a main key for the Program'
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hotkey.registerHotkey(Me, "f8", Hotkey.KeyModifier.None)
    End Sub
    'This sub will trigger the Hotkey Sub Code in the Hotkey.vb Class'

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = Hotkey.WM_HOTKEY Then
            Hotkey.handleHotKeyEvent(m.WParam)

            'After pressing the F8 key It will copy the highlighted data from anywhere and store it to the clipboard'
            If Clipboard.ContainsText Then
                HighlightedValue = My.Computer.Clipboard.GetData(DataFormats.Text).ToString

                'This part will concatinate the web address(Specified in Textbox1) with the clipboard value'

                Try
                    Dim webAddress As String = TextBox1.Text + HighlightedValue
                    Process.Start(webAddress)
                Catch ex As Exception
                    MessageBox.Show("Error in Program" + ex.ToString())
                End Try
            End If
        End If
        MyBase.WndProc(m)



    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            NotifyIcon1.Icon = SystemIcons.Application
            NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
            NotifyIcon1.BalloonTipTitle = "Running In the Background"
            NotifyIcon1.BalloonTipText = "Application is running in the Background. Double click it to maximize the form"
            NotifyIcon1.ShowBalloonTip(50000)
            Me.Hide()
            ShowInTaskbar = True
        End If
    End Sub

    Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick
        Me.Show()
        ShowInTaskbar = True
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False

    End Sub
    'System wide hotkey event handling




End Class
