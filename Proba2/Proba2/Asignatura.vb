Public Class Asignatura

    'ATRIBUTOS
    Public Property CodigoHZAsig As String
    Public Property Descripcion As String
    Public Property Deskribapena As String
    Public Property EnseñanzaAsig As String
    Public Property CursoAsig As Integer
    Public Property Tipo As String

    ' METODO CONSTRUCTOR

    'Sub New()
    '   Me.CodigoHZAsig = " "
    '  Me.Descripcion = " "
    ' Me.Deskribapena = " "
    'Me.EnseñanzaAsig = " "
    'Me.CursoAsig = 0
    'Me.Tipo = " "
    'End Sub

    Sub New(ByVal HZ As String, ByVal descr As String, ByVal deskr As String)
        Me.CodigoHZAsig = HZ
        Me.Descripcion = descr
        Me.Deskribapena = deskr
        'Me.EnseñanzaAsig = enseñanza
        'Me.CursoAsig = curso
        'Me.Tipo = tipo
    End Sub

End Class
