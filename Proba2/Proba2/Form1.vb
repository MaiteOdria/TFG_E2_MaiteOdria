Imports System.IO

Public Class Form1



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label1.Show()


       
        'Dim alumno As Alumno

        Dim Alumnos As List(Of Alumno) = New List(Of Alumno)
        Dim codAlum As String = " "

        Dim extrOk, extrI, extrM, extrD, existD, existM As Boolean


        extrOk = True

        


        If File.Exists(OpenFileDialog1.FileName) Then

            ' ************************* LECTURA FICHERO INIKA ***********************************

            extrI = ExtraerFichero(Alumnos, 1, OpenFileDialog1.FileName)


                ' ************************* LECTURA FICHERO MATRICULA ***********************************


                If (extrI = False) Then

                    Form3.Label1.Text = "Es imprescindible haber extraído previamente los expedientes de Inika"
                    Form3.TextBox_Fich.Hide()
                    Form3.Show()


            Else
                If (File.Exists(OpenFileDialog2.FileName)) Then

                    extrM = ExtraerFichero(Alumnos, 2, OpenFileDialog2.FileName)
                    If (extrM = False) Then
                        Exit Sub
                    End If
                Else
                    existM = False
                End If

                If (File.Exists(OpenFileDialog3.FileName)) Then
                    extrD = ExtraerFichero(Alumnos, 3, OpenFileDialog3.FileName)
                    If (extrD = False) Then
                        Exit Sub
                    End If
                Else
                    existD = False
                End If


            End If

        Else
        Form3.Label1.Text = "ES OBLIGATORIO EXTRAER LOS DATOS DE INIKA"
        Form3.TextBox_Fich.Text = OpenFileDialog1.FileName
        Form3.Show()
        extrI = False


        End If





            For Each ikasle As Alumno In Alumnos

                Console.WriteLine("ALUMNO:")
                Console.WriteLine(ikasle.CodigoHZ)
                Console.WriteLine(ikasle.Apellido1)
                Console.WriteLine(ikasle.Apellido2)
                Console.WriteLine(ikasle.Nombre)
                Console.WriteLine(ikasle.Enseñanza)
                Console.WriteLine(ikasle.Curso)
                Console.WriteLine(" ")


                Console.WriteLine("ASIGNATURAS EXPEDIENTE INIKA:")

                For Each asigI As Asignatura In ikasle.Inika
                    Console.WriteLine(asigI.CodigoHZAsig)
                    Console.WriteLine(asigI.Descripcion)
                    'Console.WriteLine(asigI.Deskribapena)
                    'Console.WriteLine(asigI.EnseñanzaAsig)
                    'Console.WriteLine(asigI.CursoAsig)
                Next
                Console.WriteLine("*********************************************")
                Console.WriteLine(" ")

                Console.WriteLine("ASIGNATURAS EXPEDIENTE MATRICULA:")
                If (ikasle.Matricula Is Nothing) Then
                    Console.WriteLine("No está matriculado")
                Else
                    For Each asigM As Asignatura In ikasle.Matricula
                        Console.WriteLine(asigM.CodigoHZAsig)
                        Console.WriteLine(asigM.Descripcion)
                        'Console.WriteLine(asigM.Deskribapena)
                        'Console.WriteLine(asigM.EnseñanzaAsig)
                        'Console.WriteLine(asigM.CursoAsig)
                    Next
                End If
                Console.WriteLine("*********************************************")
                Console.WriteLine(" ")

                Console.WriteLine("ASIGNATURAS EXPEDIENTE DAE:")
                If (ikasle.DAE Is Nothing) Then
                    Console.WriteLine("El alumno no está incluido en el DAE")
                Else
                    For Each asigD As Asignatura In ikasle.DAE
                        Console.WriteLine(asigD.CodigoHZAsig)
                        Console.WriteLine(asigD.Descripcion)
                        'Console.WriteLine(asigD.Deskribapena)
                        'Console.WriteLine(asigD.EnseñanzaAsig)
                        'Console.WriteLine(asigD.CursoAsig)
                    Next

                End If
                Console.WriteLine("*********************************************")
                Console.WriteLine(" ")

            Next


            

        If (extrI = True) And (extrM = True Or existM = False) And (extrD = True Or existD = False) Then

            Form3.Label1.Text = "Extracción realizada correctamente"
            Form3.TextBox_Fich.Hide()
            Form3.Show()
        End If
        'Form3.TextBox_Fich.Text = OpenFileDialog1.FileName
        'End If
        'If (extrM = True) Then
        'Form3.TextBox_Fich.Text = OpenFileDialog2.FileName
        'End If
        'If (extrD = True) Then
        'Form3.TextBox_Fich.Text = OpenFileDialog3.FileName
        'End If

        'Form3.TextBox_Fich.Hide()
        'Form3.Show()
        'End If










    End Sub

    Public Function ExtraerFichero(ByVal Alumnos As List(Of Alumno), ByVal tipoFich As Integer, ByVal ubicFich As String) As Boolean

        Dim objStreamReader As StreamReader
        Dim strLinea, strLineaCab As String
        Dim aDatos() As String

        Dim codAlum As String = " "
        Dim asignatura As Asignatura

        Dim extr As Boolean
        Dim comparacion1 As String
        Dim aape1, aape2, anom, aens, acurs As Integer
        Dim shz, sdesc, sdesk As Integer

        Dim i As Integer


        objStreamReader = New StreamReader(ubicFich)


        If (tipoFich = 1) Then
            comparacion1 = "hz_Kodea"
            i = 0
            aape1 = 1
            aape2 = 2
            anom = 3
            aens = 4
            acurs = 5
            shz = 6
            sdesc = 7
            sdesk = 8
            'sens = 9
            'scurs = 10
        ElseIf (tipoFich = 2) Then
            comparacion1 = "COD_ALU"
            i = 0
            aape1 = 5
            aape2 = 4
            anom = 6
            aens = 22
            acurs = 23
            shz = 40
            sdesc = 41
            'sens
            'scurs
        Else
            comparacion1 = "Alumno"
            i = 6
            shz = 7
            sdesc = 8
            sdesk = 9
       
        End If

        ' ************************* LECTURA FICHERO INIKA ***********************************
        Dim alumno = New Alumno()
        'Lectura cabecera fichero
        strLineaCab = objStreamReader.ReadLine

        aDatos = Split(strLineaCab, ";")

        'Controlar si el fichero indicado es de Inika
        If (aDatos(i) = comparacion1) Then

            strLinea = objStreamReader.ReadLine


            'Continuar leyendo hasta el final del fichero
            Do While Not strLinea Is Nothing



                aDatos = Split(strLinea, ";")

                If (tipoFich = 1) Or (tipoFich = 2) Or (tipoFich = 3 And aDatos(0) <> "") Then


                    ' Si es un alumno todavía no tratado en el fichero

                    If (codAlum <> aDatos(i)) Then
                        If (tipoFich = 1) Then
                            alumno = New Alumno(aDatos(i), aDatos(aape1), aDatos(aape2), aDatos(anom), aDatos(aens), CInt(aDatos(acurs)))
                            Alumnos.Add(alumno)
                            alumno.Inika = New List(Of Asignatura)

                            'asignatura = New Asignatura(aDatos(shz), aDatos(sdesc), aDatos(sdesk))

                        Else

                            alumno = ObtenerAlumno(Alumnos, tipoFich, aDatos(i))

                            If (alumno Is Nothing) Then
                                alumno = New Alumno()
                                alumno.NombreCompleto = aDatos(i)
                                Alumnos.Add(alumno)
                            End If

                            If tipoFich = 2 Then
                                alumno.Matricula = New List(Of Asignatura)

                                'Añadir a la lista de matrícula la Lengua Extranjera
                                asignatura = New Asignatura(aDatos(31), aDatos(32), aDatos(32))
                                alumno.Matricula.Add(asignatura)

                                ' Añadir a la lista de matrícula Religión (en caso de que se imparta)
                                If (aDatos(33) = "SI") Then
                                    asignatura = New Asignatura(aDatos(34), aDatos(35), aDatos(35))
                                    alumno.Matricula.Add(asignatura)
                                End If

                            Else
                                alumno.DAE = New List(Of Asignatura)
                            End If

                        End If

                        codAlum = aDatos.GetValue(i)

                    End If


                    'Añadir asignatura de la línea correspondiente en la lista "Inika"
                    asignatura = New Asignatura(aDatos(shz), aDatos(sdesc), aDatos(sdesk))

                    If (tipoFich = 1) Then
                        alumno.Inika.Add(asignatura)
                    ElseIf tipoFich = 2 Then

                        alumno.Apellido1 = aDatos(aape1)
                        alumno.Apellido2 = aDatos(aape2)
                        alumno.Nombre = aDatos(anom)

                        alumno.Matricula.Add(asignatura)
                    Else
                        alumno.DAE.Add(asignatura)
                    End If

                End If
                strLinea = objStreamReader.ReadLine

            Loop

            'Cerrar fichero
            objStreamReader.Close()

            extr = True
        Else
            Form3.Label1.Text = "Fichero incorrecto:"
            Form3.TextBox_Fich.Text = ubicFich
            Form3.Show()
            extr = False 'Fitxerue ezta zuzena
        End If

        Return extr

    End Function


    Public Function ObtenerAlumno(ByVal Alumnos As List(Of Alumno), ByVal TipoFich As Integer, ByVal Dato As String) As Alumno

        ' Comparación mediante el código HZ del alumno (1)

        If (TipoFich = 2) Then
            For Each alum As Alumno In Alumnos
                If (alum.CodigoHZ.Equals(Dato)) Then
                    Return alum
                    Exit For
                End If
            Next

            ' Comparación mediante el nombre completo del alumno (2)
        ElseIf (TipoFich = 3) Then
            For Each alum As Alumno In Alumnos
                If (alum.NombreCompleto.Equals(Dato)) Then
                    Return alum
                    Exit For
                End If
            Next
        End If
        Return Nothing

    End Function

    Private Sub Label1_Click(sender As Object, e As EventArgs)
        Me.Hide()

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form3.Show()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        OpenFileDialog2.ShowDialog()
    End Sub

    Private Sub OpenFileDialog2_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog2.FileOk
        TextBox2.Text = OpenFileDialog2.FileName
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        OpenFileDialog3.ShowDialog()
    End Sub

    Private Sub OpenFileDialog3_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog3.FileOk
        TextBox3.Text = OpenFileDialog3.FileName
    End Sub


End Class
