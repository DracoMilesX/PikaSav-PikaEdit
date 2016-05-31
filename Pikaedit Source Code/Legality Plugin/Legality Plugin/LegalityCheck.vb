Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms

Public Class LegalityCheck

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim res As DialogResult = SaveFileDialog2.ShowDialog
        If res = Windows.Forms.DialogResult.OK Then
            Button1.Visible = False
            Dim bm As Bitmap = New Bitmap(Me.Width, Me.Height)
            Me.DrawToBitmap(bm, New Rectangle(0, 0, Me.Width, Me.Height))
            bm.Save(SaveFileDialog2.FileName)
            Button1.Visible = True
            MsgBox("Report saved", MsgBoxStyle.OkOnly, "Analysis Report")
        End If
    End Sub

End Class