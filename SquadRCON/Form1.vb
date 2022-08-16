Imports System
Imports System.Net
Imports System.Net.IPAddress
Imports System.Threading
Imports SquadRCON
Imports SquadRCON.SourceRcon


Public Class Form1


    Dim ipaddress, password, command As String
    Dim port As Integer
    Dim MesssageString As String
    Public Srcon As New SourceRcon()

    Public Sub Invoke(ByVal control As Control, ByVal action As Action)
        If control.InvokeRequired Then
            control.Invoke(New MethodInvoker(Sub() action()), Nothing)
        Else
            action.Invoke()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbRdyForCommand.Enabled = False
        tbCommand.Enabled = False
        lbOutput.Enabled = False
        rtbOutput.Enabled = False


    End Sub


    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click



        ipaddress = TextBox1.Text
        port = TextBox2.Text
        password = TextBox3.Text


        Dim IP As Long = GetIPAddress(ipaddress)


        AddHandler Srcon.Errors, AddressOf ErrorOutput
        AddHandler Srcon.ServerOutput, AddressOf ReadOutput

        If Srcon.Connected = False Then
            If Srcon.Connect(New IPEndPoint(IP, port), password) Then

                While Srcon.Connected = True
                    Thread.Sleep(5)
                End While

                lbRdyForCommand.Enabled = True
                tbCommand.Enabled = True
                lbOutput.Enabled = True
                rtbOutput.Enabled = True
            End If
        End If



    End Sub



    Private Sub tbCommand_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCommand.KeyDown
        command = tbCommand.Text

        If (e.KeyData = Keys.Enter) Then
            Srcon.ServerCommand(command)
            Thread.Sleep(1000)
            Return
        End If



    End Sub



    Private Sub ErrorOutput(input As String)
        Dim sInput As String
        sInput = input


        'For Each sInput In input
        'tbOutput.Invoke(Sub() tbOutput.Text = sInput)
        'Next
        'MessageBox.Show(input, CStr(vbOK))
        rtbOutput.Invoke(Sub() rtbOutput.Text = sInput)

    End Sub



    Private Sub ReadOutput(input As String)
        Dim sInput As String
        sInput = input

        'MessageBox.Show(input, CStr(vbOK))
        rtbOutput.Invoke(Sub() rtbOutput.Text = sInput)

    End Sub


    Public Function GetIPAddress(ByRef URL As String) As Long
        Dim UrlEntry As IPHostEntry = Dns.GetHostEntry(URL)
        Dim IP As IPAddress() = Dns.GetHostAddresses(URL)
        Dim IPLong As Long

        IPLong = IP.GetValue(0).Address
        Return IPLong

    End Function



End Class


