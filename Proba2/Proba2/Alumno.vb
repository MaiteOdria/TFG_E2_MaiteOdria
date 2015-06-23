Public Class Alumno

    'ATRIBUTOS
    Public Property Nombre As String
    Public Property Apellido1 As String
    Public Property Apellido2 As String
    Public Property NombreCompleto As String
        Get
            Return Apellido1 & ", " & Apellido2 & " " & Nombre
        End Get

        Set(value As String)

        End Set

    End Property
    Public Property CodigoHZ As String
    Public Property Enseñanza As String
    Public Property Curso As Integer

    


    Public Property Inika As List(Of Asignatura)
    Public Property Matricula As List(Of Asignatura)
    Public Property DAE As List(Of Asignatura)


    ' METODO CONSTRUCTOR
    Sub New()
        Me.Apellido1 = " "
        Me.Apellido2 = " "
        Me.Nombre = " "
        Me.CodigoHZ = " "
        Me.Enseñanza = " "
        Me.Curso = 0
        Me.Inika = New List(Of Asignatura)
        Me.Matricula = New List(Of Asignatura)
        Me.DAE = New List(Of Asignatura)

    End Sub

    Sub New(ByVal HZ As String, ByVal ape1 As String, ByVal ape2 As String, ByVal nombre As String, ByVal enseñanza As String, ByVal curso As String)
        Me.CodigoHZ = HZ
        Me.Apellido1 = ape1
        Me.Apellido2 = ape2
        Me.Nombre = nombre
        Me.Enseñanza = enseñanza
        Me.Curso = curso
    End Sub

End Class
