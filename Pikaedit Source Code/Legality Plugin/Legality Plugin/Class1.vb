Imports Pikaedit_Lib
Imports System.Drawing

Public Class Class1
    Implements IPlugin
    Public pokemon As Pokemon
    Private seed As UInt32

    Public ability1() As Byte = {0, 65, 65, 65, 66, 66, 66, 67, 67, 67, 19, 61, 14, 19, 61, 68, 51, 51, 51, 50, 50, 51, 51, 22, 22, 9, 9, 8, 8, 38, 38, 38, 38, 38, 38, 56, 56, 18, 18, 56, 56, 39, 39, 34, 34, 34, 27, 27, 14, 19, 8, 8, 53, 7, 6, 6, 72, 72, 22, 22, 11, 11, 11, 28, 28, 28, 62, 62, 62, 34, 34, 34, 29, 29, 69, 69, 69, 50, 50, 12, 12, 42, 42, 51, 50, 50, 47, 47, 1, 1, 75, 75, 26, 26, 26, 69, 15, 15, 52, 52, 43, 43, 34, 34, 69, 69, 7, 51, 20, 26, 26, 31, 31, 30, 34, 48, 33, 38, 33, 33, 35, 35, 43, 68, 12, 9, 49, 52, 22, 33, 22, 11, 7, 50, 11, 10, 18, 36, 33, 33, 33, 33, 69, 17, 46, 46, 46, 61, 61, 39, 46, 28, 65, 65, 65, 66, 66, 66, 67, 67, 67, 50, 50, 15, 15, 68, 68, 68, 68, 39, 10, 10, 9, 56, 56, 55, 55, 28, 28, 9, 9, 9, 34, 47, 47, 5, 11, 34, 34, 34, 50, 34, 34, 3, 6, 6, 28, 28, 15, 12, 26, 26, 23, 39, 5, 5, 32, 52, 69, 22, 22, 38, 68, 5, 68, 39, 53, 62, 40, 40, 12, 12, 55, 55, 21, 72, 33, 51, 48, 48, 33, 53, 5, 36, 22, 20, 62, 22, 12, 9, 49, 47, 30, 46, 46, 46, 62, 61, 45, 46, 46, 30, 65, 65, 65, 66, 66, 66, 67, 67, 67, 50, 22, 53, 53, 19, 61, 68, 61, 19, 33, 33, 33, 34, 34, 34, 62, 62, 51, 51, 28, 28, 28, 33, 22, 27, 27, 54, 72, 54, 14, 3, 25, 43, 43, 43, 47, 47, 47, 5, 56, 56, 51, 52, 5, 5, 5, 74, 74, 9, 9, 57, 58, 35, 12, 30, 64, 64, 24, 24, 41, 41, 12, 40, 73, 47, 47, 20, 52, 26, 26, 8, 8, 30, 30, 17, 61, 26, 26, 12, 12, 52, 52, 26, 26, 21, 21, 4, 4, 33, 63, 59, 16, 15, 15, 26, 46, 34, 26, 46, 23, 39, 39, 47, 47, 47, 75, 33, 33, 33, 33, 69, 69, 22, 29, 29, 29, 29, 29, 29, 26, 26, 2, 70, 76, 32, 46, 65, 65, 65, 66, 66, 66, 67, 67, 67, 51, 22, 22, 86, 86, 61, 68, 79, 79, 79, 30, 30, 104, 104, 5, 5, 61, 107, 68, 118, 46, 50, 33, 33, 34, 122, 60, 60, 101, 106, 106, 50, 56, 26, 15, 7, 47, 26, 1, 1, 26, 26, 5, 43, 30, 51, 46, 8, 8, 8, 53, 80, 80, 45, 45, 4, 4, 107, 107, 26, 33, 33, 33, 117, 117, 46, 42, 20, 31, 34, 78, 49, 55, 3, 102, 81, 52, 12, 91, 80, 5, 46, 81, 26, 26, 26, 26, 46, 46, 18, 112, 46, 26, 93, 93, 123, 30, 121, 162, 65, 65, 65, 66, 66, 66, 67, 67, 67, 50, 35, 72, 22, 22, 7, 7, 82, 82, 82, 82, 82, 82, 108, 108, 145, 145, 145, 31, 31, 5, 5, 5, 109, 109, 146, 146, 131, 62, 62, 62, 33, 33, 33, 62, 5, 68, 102, 68, 38, 38, 38, 158, 158, 34, 34, 120, 22, 22, 22, 55, 125, 11, 5, 5, 61, 61, 147, 152, 152, 116, 116, 129, 129, 1, 1, 149, 149, 56, 56, 119, 119, 119, 142, 142, 142, 51, 51, 115, 115, 115, 34, 34, 9, 68, 68, 27, 27, 11, 11, 131, 14, 14, 160, 160, 57, 57, 57, 26, 26, 26, 140, 140, 18, 18, 18, 79, 79, 79, 81, 81, 26, 93, 93, 9, 39, 39, 24, 89, 89, 128, 128, 120, 51, 51, 145, 145, 82, 68, 55, 55, 26, 49, 49, 154, 154, 154, 158, 158, 163, 164, 159, 46, 154, 32, 88}
    Public ability2() As Byte = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 77, 77, 77, 62, 62, 0, 0, 61, 61, 0, 0, 0, 0, 79, 79, 79, 79, 79, 79, 98, 98, 0, 0, 0, 0, 0, 0, 0, 0, 0, 87, 87, 110, 110, 71, 71, 101, 101, 13, 13, 83, 83, 18, 18, 6, 6, 6, 39, 39, 39, 99, 99, 99, 0, 0, 0, 64, 64, 5, 5, 5, 18, 18, 20, 20, 5, 5, 39, 48, 48, 93, 93, 60, 60, 92, 92, 0, 0, 0, 5, 108, 108, 75, 75, 9, 9, 0, 0, 31, 31, 120, 89, 12, 0, 0, 69, 69, 32, 102, 113, 97, 97, 41, 41, 30, 30, 111, 101, 108, 0, 0, 104, 83, 0, 0, 75, 0, 91, 0, 0, 0, 88, 75, 75, 4, 4, 46, 47, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 51, 51, 51, 51, 48, 48, 15, 15, 0, 35, 35, 0, 98, 0, 32, 32, 48, 48, 0, 0, 0, 0, 37, 37, 69, 6, 102, 102, 102, 53, 94, 94, 14, 11, 11, 0, 0, 105, 20, 0, 0, 0, 48, 0, 0, 50, 8, 5, 50, 95, 33, 101, 82, 62, 51, 95, 95, 49, 49, 81, 81, 30, 97, 97, 55, 11, 5, 18, 18, 97, 0, 0, 88, 119, 101, 80, 101, 108, 0, 0, 113, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 95, 95, 82, 82, 0, 0, 0, 0, 0, 44, 44, 44, 48, 48, 48, 0, 0, 0, 0, 36, 36, 36, 0, 0, 90, 90, 0, 0, 0, 0, 0, 0, 0, 0, 0, 62, 62, 37, 42, 96, 96, 100, 22, 69, 69, 69, 0, 0, 31, 31, 0, 0, 68, 110, 38, 60, 60, 0, 0, 12, 12, 86, 116, 0, 20, 20, 77, 71, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 107, 107, 75, 75, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 119, 119, 0, 0, 94, 0, 105, 0, 115, 115, 115, 115, 115, 0, 0, 0, 69, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 109, 109, 0, 0, 22, 22, 22, 38, 38, 0, 0, 0, 0, 0, 0, 0, 0, 0, 53, 0, 0, 0, 0, 114, 114, 53, 84, 84, 103, 103, 0, 105, 20, 20, 0, 106, 106, 85, 85, 69, 111, 32, 77, 0, 0, 0, 0, 47, 39, 39, 0, 0, 97, 97, 87, 87, 0, 114, 114, 11, 0, 0, 0, 5, 12, 116, 102, 0, 0, 32, 110, 0, 0, 8, 81, 88, 0, 42, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 51, 51, 53, 146, 146, 84, 84, 0, 0, 0, 0, 0, 0, 28, 28, 105, 105, 105, 78, 78, 0, 0, 0, 103, 103, 159, 159, 144, 125, 125, 125, 93, 93, 143, 39, 39, 34, 34, 34, 68, 68, 68, 151, 151, 20, 20, 91, 153, 153, 153, 0, 0, 34, 75, 75, 153, 153, 98, 0, 0, 5, 5, 0, 0, 60, 133, 0, 0, 101, 101, 0, 0, 0, 98, 98, 98, 145, 145, 0, 0, 0, 157, 157, 0, 61, 75, 0, 0, 130, 130, 93, 127, 127, 0, 0, 58, 58, 58, 0, 0, 0, 28, 28, 49, 49, 49, 104, 104, 104, 0, 0, 0, 75, 60, 7, 144, 144, 125, 103, 103, 39, 39, 157, 125, 125, 142, 142, 18, 55, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    Public abilityHA() As Byte = {0, 34, 34, 34, 94, 94, 94, 44, 44, 44, 50, 61, 110, 50, 61, 97, 145, 145, 145, 55, 55, 97, 97, 127, 127, 31, 31, 146, 146, 55, 55, 125, 55, 55, 125, 132, 109, 70, 70, 132, 119, 151, 151, 50, 1, 27, 6, 6, 50, 147, 159, 159, 127, 127, 33, 33, 128, 128, 154, 154, 33, 33, 33, 98, 98, 98, 80, 80, 80, 82, 82, 82, 44, 44, 8, 8, 8, 49, 49, 144, 144, 148, 148, 128, 77, 77, 115, 115, 143, 143, 142, 142, 0, 0, 0, 133, 39, 39, 125, 125, 106, 106, 139, 139, 4, 4, 84, 39, 13, 0, 0, 120, 120, 131, 144, 39, 6, 6, 31, 31, 148, 148, 101, 80, 87, 72, 72, 153, 125, 155, 153, 93, 150, 107, 93, 95, 62, 148, 133, 133, 133, 133, 127, 82, 81, 31, 49, 63, 63, 136, 127, 0, 102, 102, 102, 18, 18, 18, 125, 125, 125, 119, 119, 110, 110, 155, 89, 97, 97, 151, 11, 11, 31, 132, 132, 105, 105, 156, 156, 57, 57, 57, 131, 157, 157, 155, 2, 151, 151, 151, 92, 48, 48, 119, 109, 109, 156, 39, 158, 144, 0, 0, 140, 157, 142, 142, 155, 17, 125, 155, 155, 22, 135, 126, 153, 124, 118, 127, 133, 133, 47, 47, 144, 141, 141, 15, 41, 133, 127, 127, 6, 8, 8, 148, 157, 141, 72, 80, 93, 72, 72, 157, 131, 10, 18, 11, 8, 61, 127, 136, 144, 0, 84, 84, 84, 3, 3, 3, 6, 6, 6, 155, 153, 95, 95, 50, 61, 79, 61, 14, 20, 20, 20, 124, 124, 124, 113, 113, 44, 44, 140, 140, 140, 44, 127, 95, 101, 0, 0, 0, 50, 151, 25, 155, 113, 113, 125, 125, 157, 159, 147, 147, 158, 125, 134, 134, 134, 140, 140, 58, 58, 0, 0, 158, 158, 102, 82, 82, 3, 3, 46, 46, 20, 83, 75, 82, 82, 126, 125, 26, 26, 11, 11, 13, 13, 137, 151, 0, 0, 93, 93, 91, 91, 0, 0, 114, 114, 33, 33, 91, 56, 0, 0, 130, 130, 0, 0, 139, 0, 154, 140, 141, 141, 12, 12, 12, 155, 41, 93, 5, 93, 125, 142, 153, 135, 135, 135, 5, 115, 135, 0, 0, 0, 0, 0, 0, 0, 75, 75, 75, 89, 89, 89, 128, 128, 128, 51, 120, 120, 141, 141, 50, 101, 62, 62, 62, 102, 101, 125, 125, 43, 43, 142, 142, 110, 55, 127, 10, 41, 41, 0, 0, 159, 159, 92, 138, 138, 7, 7, 0, 153, 51, 128, 0, 51, 51, 134, 134, 155, 101, 132, 145, 151, 24, 24, 24, 82, 158, 154, 159, 159, 51, 51, 143, 143, 0, 41, 41, 41, 43, 43, 124, 148, 13, 120, 144, 72, 72, 105, 119, 34, 115, 90, 47, 148, 154, 159, 0, 130, 0, 0, 0, 0, 140, 140, 49, 0, 140, 0, 0, 0, 0, 0, 0, 0, 126, 126, 126, 47, 47, 120, 75, 75, 75, 148, 148, 50, 113, 113, 158, 158, 65, 65, 66, 66, 67, 67, 140, 140, 79, 79, 79, 157, 157, 159, 159, 159, 86, 86, 104, 104, 103, 89, 89, 89, 11, 11, 11, 104, 104, 142, 142, 142, 95, 95, 95, 34, 34, 102, 102, 104, 83, 83, 83, 39, 161, 114, 133, 133, 22, 22, 110, 0, 0, 33, 33, 0, 0, 106, 106, 0, 0, 92, 92, 23, 23, 23, 144, 144, 144, 93, 93, 133, 133, 133, 32, 32, 78, 99, 142, 144, 144, 6, 6, 144, 68, 68, 0, 0, 29, 29, 29, 0, 0, 0, 148, 148, 23, 23, 23, 127, 127, 127, 155, 33, 0, 142, 84, 8, 120, 120, 104, 99, 99, 46, 46, 43, 55, 128, 133, 133, 73, 54, 0, 0, 0, 68, 68, 0, 0, 0, 128, 128, 0, 0, 125, 0, 0, 0, 0}

    ''' <summary>
    ''' Initialize seed
    ''' </summary>
    Private Sub srand(newSeed As UInt32)
        seed = newSeed
    End Sub

    ''' <summary>
    ''' Call the the next RNG
    ''' </summary>
    Private Function rand() As UInt32
        seed = ((&H41C64E6D * seed + &H6073)) And 4294967295
        Return seed >> 16
    End Function

    Private Function prand() As UInt32
        seed = (seed * &HEEB9EB65 + &HA3561A1) And 4294967295
        Return seed >> 16
    End Function

    ''' <summary>
    ''' Determine de ability index of the pokemon
    ''' </summary>
    Public Function abilityIndex()
        If pokemon.ability = PkmLib.abilities(ability1(pokemon.no)) Then
            Return "0"
        Else
            If pokemon.ability = PkmLib.abilities(ability2(pokemon.no)) Then
                Return "1"
            Else
                If pokemon.ability = PkmLib.abilities(abilityHA(pokemon.no)) Then
                    Return "DW"
                Else
                    Return "Illegal"
                End If
            End If
        End If
    End Function

    Public Sub [Do]() Implements IPlugin.Do
        Dim legalityform As New LegalityCheck
        Dim starter As Boolean = False
        If pokemon.locationmet = "Nuvema Town" Or pokemon.locationmet = "Aspertia City" Then
            If pokemon.species = "Snivy" Or pokemon.species = "Servine" Or pokemon.species = "Serperior" Or pokemon.species = "Tepig" Or pokemon.species = "Pignite" Or pokemon.species = "Emboar" Or pokemon.species = "Oshawott" Or pokemon.species = "Dewott" Or pokemon.species = "Samurott" Then
                starter = True
            Else
                starter = False
            End If
        End If
        'legalityform.Button2.Visible = True
        legalityform.Label20.Text = "Shiny"
        Dim nickseq As String = pokemon.nicktb.Replace(pokemon.nick, "")
        Dim nicklenght As Integer = pokemon.nick.Length
        Dim otseq As String = pokemon.ottb.Replace(pokemon.ot, "")
        Dim otlenght As Integer = pokemon.ot.Length
        Dim nicks() As String
        Dim nicknamed As String = pokemon.nicktb
        Dim nicknamed2 As String = ""
        nicks = nickseq.Split("\")
        Dim trash() As String = nickseq.Split("\")
        trash(0) = ""
        Dim ots() As String
        ots = otseq.Split("\")
        Dim nickcheck As String = ""
        Dim otcheck As String = ""
        Dim z As Integer = 0
        Dim nickcount As Integer = 0
        Dim otcount As Integer = 0
        Dim ot0cont As Integer = 0
        Dim pidcheck As String
        Dim pidc As Color
        Dim temp As Integer
        Dim nickc As Color
        Dim otc As Color
        Dim ballc As Color
        Dim locc As Color
        Dim abic As Color
        Dim eggcheck As String
        Dim ballcheck As String
        Dim abicheck As String
        Dim naturep As Boolean
        Dim shinycheck As String = ""
        Dim shinyc As Color
        Dim gencheck As String = ""
        Dim genc As Color
        Dim abilityp As Boolean
        Dim genderp As Boolean
        Dim n1 As String = ""
        Dim n2 As String = ""
        Dim j As Integer = 0
        Dim p As Integer = 0
        If pokemon.gen = 5 Then
            While z < nicks.Length - 1
                If nicks(z + 1) = "FFFF" Then
                    nickcount = nickcount + 1
                    If z = nicks.Length - 2 Or z = 0 Then
                        trash(z + 1) = ""
                    Else
                        trash(z + 1) = "\FFFF\"
                    End If
                Else
                    If trash(z + 1) = "0000" Then
                        trash(z + 1) = ""
                    Else
                        j = CDec("&H" & trash(z + 1))
                        n1 = Func.zeros(Hex(j And 255))
                        n2 = Func.zeros(Hex(j >> 8))
                        trash(z + 1) = ChrW(CDec("&H" & n1 & n2))
                    End If
                End If
                z = z + 1
            End While
            z = 0
            While z < ots.Length - 1
                temp = z + 1
                If ots(temp) = "FFFF" Then
                    otcount = otcount + 1
                End If
                If ots(temp) = "0000" Then
                    ot0cont = ot0cont + 1
                End If
                z = z + 1
            End While
            z = 0
            If nicklenght < 10 And nicklenght >= 1 Then
                If nickcount = 2 Then
                    nickcheck = "Valid nickname trash bytes " & pokemon.nicktb
                    nickc = Color.Green
                Else
                    nickcheck = "Invalid nickname trash bytes " & pokemon.nicktb
                    nickc = Color.Red
                End If
            Else
                If nickcount = 1 Then
                    nickcheck = "Valid nickname trash bytes " & pokemon.nicktb
                    nickc = Color.Green
                Else
                    nickcheck = "Invalid nickname trash bytes " & pokemon.nicktb
                    nickc = Color.Red
                End If
            End If
            Dim tb As String = ""
            While z < trash.Length
                If trash(z) <> "" Then
                    tb = tb & trash(z)
                    tb = tb.Replace("\\", "\")
                End If
                z = z + 1
            End While
            'If pokemon.isNick = True Or tb <> "" Then
            '    nicknamed2 = Form1.generateFunc(New Pokemon(pkm))
            '    If nicknamed = nicknamed2 Then
            '        nickcheck = "Valid nicknamed trash bytes " & nicknamed
            '        nickc = Color.Green
            '    Else
            '        nickcheck = "Unable to determine nicknamed trash bytes, possible nickname change " & nicknamed
            '        nickc = Color.Orange
            '        z = 0
            '        If tb <> "" Then
            '            nickcheck = "Valid sequence terminators, found trash bytes " & Chr(34) & tb & Chr(34) & " possible nickname/evolution or event " & nicknamed
            '            nickc = Color.Green
            '        End If
            '    End If
            'End If
            pokemon.nicktb = nicknamed
            z = 0
            If otlenght < 7 And otlenght >= 1 Then
                If otcount = 1 And ot0cont = (7 - otlenght) Then
                    otc = Color.Green
                    If starter = True Then
                        otcheck = "Valid OT trash bytes for Starter pokemon " & pokemon.ottb
                    Else
                        If pokemon.isHatched Then
                            otcheck = "Valid OT trash bytes for Hatched pokemon " & pokemon.ottb
                        Else
                            If pokemon.locationmet = "Link Trade" Then
                                otcheck = "Valid OT trash bytes for In-Game Traded pokemon " & pokemon.ottb
                            Else
                                If (pokemon.species = "Tornadus" Or pokemon.species = "Thundurus") And pokemon.locationmet <> "Pokémon Dream Radar" Then
                                    otcheck = "Valid OT trash bytes for Roaming pokemon " & pokemon.ottb
                                Else
                                    If (pokemon.DW And 2) = 2 Then
                                        otcheck = "Valid OT trash bytes for N's pokemon " & pokemon.ottb
                                    Else
                                        If pokemon.locationmet = "Entree Forest" Or pokemon.locationmet = "Pokémon Dream Radar" Then
                                            otcheck = "Valid OT trash bytes for Entree Forest pokemon " & pokemon.ottb
                                        Else
                                            otc = Color.Red
                                            otcheck = "Invalid OT trash bytes, these are only for starter/hatched/in-game traded and roaming pokemon " & pokemon.ottb
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Else
                    If otcount = 2 And ot0cont = (7 - otlenght) - 1 Then
                        otc = Color.Green
                        If pokemon.isFateful Then
                            otcheck = "Valid OT trash bytes for Event pokemon " & pokemon.ottb
                        Else
                            If pokemon.locationmet = "Pokétransfer" Or pokemon.eggloc = "Pokétransfer" Then
                                otcheck = "The pokemon is from gen 5 but has Poketransfer trash bytes " & pokemon.ottb
                                otc = Color.Red
                            Else
                                If pokemon.species = "Cobalion" Or pokemon.species = "Virizion" Or pokemon.species = "Terrakion" Or pokemon.species = "Zekrom" Or pokemon.species = "Reshiram" Or pokemon.species = "Kyurem" Then
                                    otcheck = "Valid OT trash bytes for Legendary pokemon " & pokemon.ottb
                                Else
                                    If pokemon.isHatched = False Then
                                        otcheck = "Valid OT trash bytes for Wild pokemon " & pokemon.ottb
                                    Else
                                        otcheck = "The trash bytes are valid if recieved from gts " & pokemon.ottb
                                        otc = Color.Orange
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                If otcount = 1 And ot0cont = 0 Then
                    otcheck = "Valid OT trash bytes " & pokemon.ottb
                    otc = Color.Green
                Else
                    otcheck = "Invalid OT trash bytes " & pokemon.ottb
                    otc = Color.Red
                End If
            End If
            If pokemon.eggloc = "" Then
                If pokemon.locationmet = "Day-Care Couple" Or pokemon.locationmet = "Day-Care Couple (G4)" Or pokemon.locationmet = "Pokétransfer" Then
                    eggcheck = "Invalid location"
                    locc = Color.Red
                Else
                    eggcheck = "Valid met location " & pokemon.locationmet
                    locc = Color.Green
                    If pokemon.locationmet = "" Then
                        eggcheck = "Unable to check location or invalid location"
                        locc = Color.Orange
                    End If
                End If
                If pokemon.pokeball = "Cherish Ball" Then
                    If pokemon.isFateful = True Then
                        ballcheck = "Event Pokemon"
                        ballc = Color.Green
                    Else
                        ballcheck = "Has a Cherish Ball but doesn't have a Fateful Encounter"
                        ballc = Color.Red
                    End If
                Else
                    ballcheck = "Valid"
                    ballc = Color.Green
                End If
                If pokemon.pokeball = "Dream Ball" And (pokemon.locationmet = "Entree Forest" Or pokemon.locationmet = "Pokémon Dream Radar") Then
                    ballcheck = "Valid"
                    ballc = Color.Green
                    eggcheck = "Valid met location for DW pokemon " & pokemon.locationmet
                    locc = Color.Green
                Else
                    If pokemon.pokeball = "Dream Ball" Then
                        ballcheck = "Invalid, Dream Ball is only available at Entree Forest and Pokémon Dream Radar"
                        ballc = Color.Red
                    End If
                End If
            Else
                If pokemon.locationmet = "Day-Care Couple" Or pokemon.locationmet = "Pokémon Event" Or pokemon.locationmet = "Stranger" Then
                    If pokemon.pokeball = "Poké Ball" Then
                        ballcheck = "Valid"
                        ballc = Color.Green
                    Else
                        ballcheck = "Invalid"
                        ballc = Color.Red
                    End If
                    eggcheck = "Valid egg hatched location " & pokemon.eggloc
                    locc = Color.Green
                Else
                    eggcheck = "The Pokemon hatched from an egg, but wasn't given at a valid location"
                    locc = Color.Orange
                End If
            End If
            'If invalidlocation = "Invalid" Then
            '    eggcheck = "It has no location in neither met location nor hatched location"
            '    locc = Color.Red
            'Else
            '    If invalidlocation = "Unknown" Then
            '        eggcheck = "Couldn't determine location met or hatched"
            '        locc = Color.Orange
            '    End If
            'End If
            If (pokemon.DW And 1) = 1 And abilityIndex() = "DW" Then
                abicheck = "Valid, HA ability " + pokemon.ability
                abic = Color.Green
            Else
                'If My.Resources.DWReleased <> "" Then
                '    If My.Resources.DWReleased.Contains(pokemon.species) Then
                '        abicheck = "Valid released DW ability"
                '        abic = Color.Green
                '    Else
                '        abicheck = "Valid DW ability, but it hasn't been released"
                '        abic = Color.Red
                '    End If
                'End If
                If abilityIndex() <> "Illegal" Then
                    abicheck = "Valid ability " + pokemon.ability
                    abic = Color.Green
                Else
                    If (pokemon.DW And 1) = 0 Then
                        abicheck = "Invalid ability " + pokemon.ability
                        abic = Color.Red
                    Else
                        abicheck = "Invalid ability " + pokemon.ability + ", HA flag marked"
                        abic = Color.Red
                    End If
                End If
            End If
            Dim temppid As UInt32 = pokemon.pid
            Dim ratiotemp As String = pokemon.genderRatio
            Select Case ratiotemp
                Case "100% Male"
                    ratiotemp = -1
                Case "100% Female"
                    ratiotemp = 255
                Case "Genderless"
                    ratiotemp = 0
                Case "87.5% Male-12.5% Female"
                    ratiotemp = 32
                Case "75% Male-25% Female"
                    ratiotemp = 64
                Case "50% Male-50% Female"
                    ratiotemp = 128
                Case "25% Male-75% Female"
                    ratiotemp = 192
                Case "12.5% Male-87.5% Female"
                    ratiotemp = 224
            End Select
            Dim genderpp As Boolean = False
            Dim gender As String = ""
            If (pokemon.female = 0) And (pokemon.genderless = 0) Then
                gender = "Male"
            Else
                If pokemon.female = 1 Then
                    gender = "Female"
                Else
                    gender = "Genderless"
                End If
            End If
            If gender = "Male" Then
                If (temppid Mod 256) > ratiotemp Then
                    genderpp = True
                Else
                    genderpp = False
                End If
            Else
                If gender = "Female" Then
                    If (temppid Mod 256) <= ratiotemp Then
                        genderpp = True
                    Else
                        genderpp = False
                    End If
                Else
                    genderpp = True
                End If
            End If
            Dim abilitypp As Boolean = False
            Dim abicalc As Integer = (temppid >> 16) And 1
            abilitypp = True
            If pokemon.isHatched = False Then
                'Wild/Stationary PID check
                Dim pid12 = pokemon.pid
                Dim validpid As Boolean = False
                Dim xord As Boolean = False
                Dim lsid = pokemon.sid And 1
                Dim ltid = pokemon.id And 1
                Dim upid = (pid12 And 2147483648) / 2147483648
                Dim lpid = pid12 And 1
                Dim xo1 = lsid Xor ltid
                Dim xo2 = upid Xor lpid
                Dim tempid = ""
                Dim pidseed = ""
                Dim dwforest As Boolean = False
                'If xo1 Xor xo2 = 1 Then
                '    pid12 = pid12 Xor 2147483648
                'End If
                'upid = (pid12 And 2147483648) / 2147483648
                'lpid = pid12 And 1
                'xo2 = upid Xor lpid
                If (xo1 Xor xo2) = 0 Then
                    dwforest = False
                    validpid = True
                    tempid = pid12 Xor 2147483648
                    upid = (tempid And 2147483648) / 2147483648
                    lpid = tempid And 1
                    xo1 = lsid Xor ltid
                    If (xo1 Xor xo2) = 1 Then
                        xord = True
                        pid12 = tempid
                    Else
                        xord = False
                    End If
                    pidseed = 65536 Xor pid12
                Else
                    validpid = False
                End If
                If pokemon.locationmet = "Entree Forest" Or pokemon.locationmet = "Pokémon Dream Radar" Then
                    dwforest = True
                Else
                    dwforest = False
                End If
                If validpid = True And dwforest = False Then
                    If abilitypp = True And genderpp = True Then
                        If xord = False Then
                            pidcheck = "Valid wild/stationary PID the upper seed was " & Hex(pidseed)
                            pidc = Color.Green
                        Else
                            pidcheck = "Valid wild/stationary PID, with a XOR applied at 0x80000000 the upper seed was " & Hex(pidseed)
                            pidc = Color.Green
                        End If
                    Else
                        If abilitypp = True And genderpp = False Then
                            pidcheck = "Valid wild/stationary PID but doesn't matches gender"
                            pidc = Color.Orange
                        Else
                            If abilitypp = False And genderpp = True Then
                                pidcheck = "Valid wild/stationary PID but doesn't matches ability"
                                pidc = Color.Orange
                            Else
                                pidcheck = "Valid wild/stationary PID but doesn't matches ability and gender"
                                pidc = Color.Orange
                            End If
                        End If
                    End If
                Else
                    If dwforest = True Then
                        If abilitypp = True And genderpp = True Then
                            pidcheck = "Valid Entree Forest/Dream Radar pkm PID"
                            pidc = Color.Green
                        Else
                            If abilitypp = True And genderpp = False Then
                                pidcheck = "Valid Entree Forest/Dream Radar pkm PID but doesn't matches gender"
                                pidc = Color.Orange
                            Else
                                If abilitypp = False And genderpp = True Then
                                    pidcheck = "Valid Entree Forest/Dream Radar pkm PID but doesn't matches ability"
                                    pidc = Color.Orange
                                Else
                                    pidcheck = "Valid Entree Forest/Dream Radar pkm PID but doesn't matches ability and gender"
                                    pidc = Color.Orange
                                End If
                            End If
                        End If
                    Else
                        If abilitypp = True And genderpp = True Then
                            pidcheck = "Invalid wild PID"
                            pidc = Color.Red
                        Else
                            If abilitypp = True And genderpp = False Then
                                pidcheck = "Invalid wild PID and invalid gender"
                                pidc = Color.Red
                            Else
                                If abilitypp = False And genderpp = True Then
                                    pidcheck = "Invalid wild PID and invalid ability"
                                    pidc = Color.Red
                                Else
                                    pidcheck = "Invalid wild PID and invalid ability and gender"
                                    pidc = Color.Red
                                End If
                            End If
                        End If
                    End If
                End If
                If starter = True Then
                    pid12 = pokemon.pid
                    validpid = False
                    xord = False
                    lsid = pokemon.sid And 1
                    ltid = pokemon.id And 1
                    upid = (pid12 And 4294901760) / 65536
                    lpid = pid12 And 65535
                    xo1 = pokemon.sid Xor pokemon.id
                    xo2 = upid Xor lpid
                    tempid = ""
                    pidseed = ""
                    dwforest = False
                    If (xo1 Xor xo2) <= 8 Then
                        pidcheck = "Valid Shiny Starter PID"
                        pidc = Color.Green
                    Else
                        If abilitypp = True And genderpp Then
                            pidcheck = "Valid Starter PID"
                            If pokemon.isFateful Then
                                pidcheck = "Valid Starter PID"
                            End If
                            pidc = Color.Green
                        Else
                            If abilitypp = False And genderpp = True Then
                                pidcheck = "Invalid Starter PID invalid ability"
                                If pokemon.isFateful Then
                                    pidcheck = "Invalid Starter PID invalid ability"
                                End If
                                pidc = Color.Red
                            Else
                                If abilitypp = True And genderpp = False Then
                                    pidcheck = "Invalid Starter PID invalid gender"
                                    If pokemon.isFateful Then
                                        pidcheck = "Invalid Starter PID invalid gender"
                                    End If
                                    pidc = Color.Red
                                Else
                                    pidcheck = "Invalid Starter PID invalid ability and gender"
                                    If pokemon.isFateful Then
                                        pidcheck = "Valid Starter PID invalid ability gender"
                                    End If
                                    pidc = Color.Red
                                End If
                            End If
                        End If
                    End If
                End If
                'Dim zz As Integer = 0
                'If isFateful Then
                '    'Event Database Check
                '    'If Not Form1.eventpkmList Is Nothing Then
                '    '    While zz < Form1.eventpkmList.data.Length - 1
                '    '        If pokemon.species = Form1.eventpkmList.data(zz).pokemon.species Then
                '    '            If ot = Form1.eventpkmList.data(zz).ot Then
                '    '                Exit While
                '    '            End If
                '    '        End If
                '    '        zz += 1
                '    '    End While
                '    '    If zz <> Form1.eventpkmList.data.Length - 1 Then
                '    '        'Found match
                '    '        otcheck = otcheck & " Event: " & Form1.eventpkmList.data(zz).name
                '    '    Else
                '    '        'Match not found

                '    '    End If
                '    'End If
                '    If Not Form1.eventpkmList Is Nothing Then
                '        Dim otevent() As Object = Form1.eventpkmList.verifyPKM(New Pokemon(pkm))
                '        otcheck = otcheck & " " & otevent(0)
                '        otc = otevent(1)
                '    End If
                'End If
                If pokemon.species = "Reshiram" Or pokemon.species = "Zekrom" Or pokemon.species = "Victini" Or pokemon.isFateful Then
                    'Wild/Stationary PID check
                    pid12 = pokemon.pid
                    validpid = False
                    xord = False
                    lsid = pokemon.sid And 1
                    ltid = pokemon.id And 1
                    upid = (pid12 And 4294901760) / 65536
                    lpid = pid12 And 65535
                    xo1 = pokemon.sid Xor pokemon.id
                    xo2 = upid Xor lpid
                    tempid = ""
                    pidseed = ""
                    dwforest = False
                    If (xo1 Xor xo2) < 8 Then
                        pidcheck = "Invalid non-shiny PID"
                        If pokemon.isFateful Then
                            pidcheck = "Invalid non-shiny Wonder Card PID"
                        End If
                        pidc = Color.Red
                    Else
                        If abilitypp = True And genderpp Then
                            pidcheck = "Valid non-shiny PID"
                            If pokemon.isFateful Then
                                pidcheck = "Valid non-shiny Wonder Card PID"
                            End If
                            pidc = Color.Green
                        Else
                            If abilitypp = False And genderpp = True Then
                                pidcheck = "Invalid non-shiny PID invalid ability"
                                If pokemon.isFateful Then
                                    pidcheck = "Invalid non-shiny Wonder Card PID invalid ability"
                                End If
                                pidc = Color.Red
                            Else
                                If abilitypp = True And genderpp = False Then
                                    pidcheck = "Invalid non-shiny PID invalid gender"
                                    If pokemon.isFateful Then
                                        pidcheck = "Invalid non-shiny Wonder Card PID invalid gender"
                                    End If
                                    pidc = Color.Red
                                Else
                                    pidcheck = "Invalid non-shiny PID invalid ability and gender"
                                    If pokemon.isFateful Then
                                        pidcheck = "Valid non-shiny Wonder Card PID invalid ability gender"
                                    End If
                                    pidc = Color.Red
                                End If
                            End If
                        End If
                    End If
                End If
                If pokemon.isFateful And pokemon.isShiny Then
                    pid12 = pokemon.pid
                    If ((pid12 >> 24) = (pokemon.id >> 8)) And (((pid12 And &HFF00) >> 8) = (pokemon.sid >> 8)) Then
                        pidcheck = "Valid Dynamic Shiny PID"
                        pidc = Color.Green
                    Else
                        pidcheck = "Invalid Dynamic Shiny PID"
                        pidc = Color.Red
                    End If
                End If
                If (pokemon.DW And 2) = 2 Then
                    If (pid12 And 4278190080) = 4278190080 And pokemon.iv.hp = "30" And pokemon.iv.atk = "30" And pokemon.iv.def = "30" And pokemon.iv.spa = "30" And pokemon.iv.spd = "30" And pokemon.iv.spe = "30" Then
                        pidcheck = "Valid N's Pokemon PID"
                        pidc = Color.Green
                    Else
                        pidcheck = "Invalid N's Pokemon PID"
                        pidc = Color.Red
                    End If
                End If
            Else
                If abilitypp = True And genderpp Then
                    pidcheck = "Valid egg PID"
                    pidc = Color.Green
                Else
                    If abilitypp = False And genderpp = True Then
                        pidcheck = "Invalid egg PID invalid ability"
                        pidc = Color.Red
                    Else
                        If abilitypp = True And genderpp = False Then
                            pidcheck = "Invalid egg PID invalid gender"
                            pidc = Color.Red
                        Else
                            pidcheck = "Invalid egg PID invalid ability and gender"
                            pidc = Color.Red
                        End If
                    End If
                End If
            End If
            If pokemon.isShiny Then
                legalityform.Label20.Text = "Shiny"
                If pokemon.species = "Celebi" Or pokemon.species = "Arceus" Or pokemon.species = "Victini" Or pokemon.species = "Zekrom" Or pokemon.species = "Reshiram" Or pokemon.species = "Keldeo" Or pokemon.species = "Meloetta" Then
                    shinycheck = pokemon.species & " cannot be shiny"
                    shinyc = Color.Red
                Else
                    shinycheck = "Valid shiny"
                    shinyc = Color.Green
                End If
            Else
                shinycheck = ""
                legalityform.Label20.Text = ""
                shinyc = Color.Black
            End If
            If pokemon.pokeball = "Safari Ball" Then
                ballcheck = "Invalid Pokeball, there is no Safari Zone in gen 5"
                ballc = Color.Red
            End If
            gencheck = ""
            legalityform.Label21.Text = ""
        Else
            If pokemon.gen = 4 Then
                legalityform.Label21.Text = "Gen"
                Dim eventnick As Boolean = False
                While z < nicks.Length - 1
                    If nicks(z + 1) = "FFFF" Then
                        nickcount = nickcount + 1
                    End If
                    z = z + 1
                End While
                z = 0
                While z < ots.Length - 1
                    temp = z + 1
                    If ots(temp) = "FFFF" Then
                        otcount = otcount + 1
                    End If
                    If ots(temp) = "0000" Then
                        ot0cont = ot0cont + 1
                    End If
                    z = z + 1
                End While
                z = 0
                If nicklenght < 10 And nicklenght >= 1 Then
                    If nickcount = 2 Then
                        nickcheck = "Valid nickname trash bytes " & pokemon.nicktb
                        nickc = Color.Green
                    Else
                        If nickcount = 11 - nicks.Length Then
                            nickcheck = "Valid 4 gen event nickname trash bytes " & pokemon.nicktb
                            nickc = Color.Green
                            eventnick = True
                        End If
                        nickcheck = "Invalid nickname trash bytes " & pokemon.nicktb
                        nickc = Color.Red
                    End If
                Else
                    If nickcount = 1 Then
                        nickcheck = "Valid nickname trash bytes " & pokemon.nicktb
                        nickc = Color.Green
                    Else
                        nickcheck = "Invalid nickname trash bytes " & pokemon.nicktb
                        nickc = Color.Red
                    End If
                End If
                If pokemon.isFateful Then
                    If nicklenght + nickcount = 11 Then
                        nickcheck = "Valid 4 gen event trashbytes " & pokemon.nicktb
                        nickc = Color.Green
                    Else
                        nickcheck = "Invalid 4 gen event trashbytes " & pokemon.nicktb
                        nickc = Color.Red
                    End If
                End If
                If otlenght < 7 And otlenght >= 1 Then
                    If otcount = 1 And ot0cont = (7 - otlenght) Then
                        otcheck = "Invalid OT trash bytes " & pokemon.ottb
                        otc = Color.Green
                    Else
                        otcheck = "Invalid OT trash bytes " & pokemon.ottb
                        otc = Color.Red
                        If otcount = 2 And ot0cont = (7 - otlenght) - 1 Then
                            otcheck = "Valid OT trash bytes for 4 gen pokemon " & pokemon.ottb
                            otc = Color.Green
                        End If
                    End If
                Else
                    If otcount = 1 And ot0cont = 0 Then
                        otcheck = "Valid OT trash bytes " & pokemon.ottb
                        otc = Color.Green
                    Else
                        otcheck = "Invalid OT trash bytes " & pokemon.ottb
                        otc = Color.Red
                    End If
                End If
                If pokemon.eggloc = "" Then
                    If pokemon.locationmet = "Day-Care Couple (G4)" Or pokemon.locationmet = "Pokétransfer" Or pokemon.locationmet = "(Entei/Raikou/Suicune pre-event)" Or pokemon.locationmet = "(Entei/Raikou/Suicune post-event)" Then
                        eggcheck = "Valid location " & pokemon.locationmet
                        locc = Color.Green
                    Else
                        If pokemon.locationmet = "" Then
                            eggcheck = "Unvalid location, for gen 3 and 4 Poketransfer is the only valid location"
                            locc = Color.Red
                        End If
                    End If
                    If pokemon.pokeball = "Cherish Ball" Then
                        If pokemon.isFateful = True Then
                            ballcheck = "Event Pokemon (fateful encounter), please check an event database"
                            ballc = Color.Green
                        Else
                            ballcheck = "Has a Cherish Ball but doesn't have a Fateful Encounter"
                            ballc = Color.Red
                        End If
                    Else
                        ballcheck = "Valid"
                        ballc = Color.Green
                    End If
                Else
                    If pokemon.locationmet = "Day-Care Couple (G4)" Then
                        If pokemon.pokeball = "Poké Ball" Then
                            ballcheck = "Valid"
                            ballc = Color.Green
                        Else
                            ballcheck = "Invalid"
                            ballc = Color.Red
                        End If
                        eggcheck = "Valid egg hatched location " & pokemon.eggloc
                        locc = Color.Green
                    Else
                        eggcheck = "The pokemon was hatched in gen 4, egg recieved location must be Day-Care Couple (G4)"
                        locc = Color.Orange
                    End If
                End If
                'If invalidlocation = "Invalid" Then
                '    eggcheck = "It has no location in neither met location nor hatched location"
                '    locc = Color.Red
                'Else
                '    If invalidlocation = "Unknown" Then
                '        eggcheck = "Couldn't determine location met or hatched"
                '        locc = Color.Orange
                '    End If
                'End If
                If pokemon.pokeball = "Dream Ball" Then
                    ballc = Color.Red
                    ballcheck = "Dream Ball is a gen 5 pokemon.pokeball"
                End If
                'If abilitychanged = "Valid" Then
                'If abilityIndex() <> "Illegal" And abilityIndex() <> "DW" Then
                abicheck = "Valid ability"
                abic = Color.Green
                'Else
                '    abicheck = "Invalid ability"
                '    abic = Color.Red
                'End If
                'End If
                Dim tempnature As String = pokemon.nature
                Dim nature2 As Integer = 0
                nature2 = PkmLib.natures.IndexOf(tempnature)
                Dim pid2 As String = pokemon.pid
                If (pid2 Mod 25) = nature2 Then
                    naturep = True
                Else
                    naturep = False
                End If
                Dim gender As String
                Dim ratio As String = pokemon.genderRatio
                Select Case ratio
                    Case "100% Male"
                        ratio = -1
                    Case "100% Female"
                        ratio = 255
                    Case "Genderless"
                        ratio = 255
                    Case "87.5% Male-12.5% Female"
                        ratio = 30
                    Case "75% Male-25% Female"
                        ratio = 63
                    Case "50% Male-50% Female"
                        ratio = 126
                    Case "25% Male-75% Female"
                        ratio = 190
                    Case "12.5% Male-87.5% Female"
                        ratio = 223
                    Case Else
                        ratio = 126
                End Select
                If (pokemon.female = 0) And (pokemon.genderless = 0) Then
                    gender = "Male"
                Else
                    If pokemon.female = 1 Then
                        gender = "Female"
                    Else
                        gender = "Genderless"
                    End If
                End If
                If gender = "Male" Then
                    If (pokemon.pid Mod 256) > ratio Then
                        genderp = True
                    Else
                        genderp = False
                    End If
                Else
                    If gender = "Female" Then
                        If (pokemon.pid Mod 256) <= ratio Then
                            genderp = True
                        Else
                            genderp = False
                        End If
                    Else
                        genderp = True
                    End If
                End If
                'Dim abi As String = abilityIndex()
                'Dim abilitypp As Boolean = False
                'If ((DW And 1) = 1) Or abilityIndex() = "Illegal" Then
                '    abilitypp = False
                'Else
                '    If (pokemon.pid And 1) = abilityIndex() Then
                '        abilitypp = True
                '    Else
                '        If Form1.getAbility(abilityIndex(), no, form) Then
                '            abilitypp = True
                '        Else
                '            If abilityIndex() <> "DW" Then
                '                abilitypp = False
                '            Else
                '                If Form1.getAbility(abilityIndex(), no, form) Then
                '                    abilitypp = True
                '                Else
                '                    abilitypp = False
                '                End If
                '            End If
                '        End If
                '    End If
                'End If
                abilityp = True
                'If ComboBox4.Items.Count <= 2 Then
                '    abilityp = True
                '    pidupdate()
                'End If
                Dim temp1 As String
                Dim temp2 As String
                Dim temp3 As String
                If pokemon.isHatched Then
                    If naturep = True And genderp = True And abilityp = True Then
                        pidcheck = "Valid egg PID"
                        pidc = Color.Green
                    Else
                        pidcheck = "Invalid egg PID" & " " & temp1 & temp2 & temp3
                        pidc = Color.Red
                    End If
                Else
                    If pokemon.isFateful = True Then
                        pidcheck = "Unable to determine event PID"
                        pidc = Color.Black
                    Else
                        Dim genderp2 As Boolean
                        Dim abilityp2 As Boolean
                        Dim naturep2 As Boolean
                        Dim iv1
                        Dim iv2
                        iv2 = pokemon.iv.spd + (pokemon.iv.spa * 32) + (pokemon.iv.spd * 1024)
                        iv1 = pokemon.iv.hp + (pokemon.iv.atk * 32) + (pokemon.iv.def * 1024)
                        Dim posseed
                        Dim a As Integer = 0
                        If naturep = True And genderp = True And abilityp = True Then
                            While a <= 131071
                                posseed = iv2 * 65536 + (a Mod 65536)
                                If a >= 65536 Then
                                    posseed = posseed + 2147483648
                                End If
                                srand(posseed)
                                Dim tempseed = prand()
                                If tempseed Mod 32768 = iv1 Then
                                    Dim pid22 As String = prand()
                                    Dim pid11 As String = prand()
                                    Dim pid3 As String = (pid22 * 65536) + pid11
                                    If gender = "Male" Then
                                        If (pid3 Mod 256) > ratio Then
                                            genderp2 = True
                                        Else
                                            genderp2 = False
                                        End If
                                    Else
                                        If gender = "Female" Then
                                            If (pid3 Mod 256) <= ratio Then
                                                genderp2 = True
                                            Else
                                                genderp2 = False
                                            End If
                                        Else
                                            genderp2 = True
                                        End If
                                    End If
                                    'If abi = (pid3 Mod 2) Then
                                    '    abilityp2 = True
                                    'Else
                                    '    abilityp2 = False
                                    'End If
                                    abilityp2 = True
                                    If (pid3 Mod 25) = pokemon.nature Then
                                        naturep2 = True
                                    Else
                                        naturep2 = False
                                    End If
                                    If naturep2 = True And genderp2 = True And abilityp2 = True Then
                                        If pid3 = pokemon.pid Then
                                            pidcheck = "Valid PID/Valid IV"
                                        End If
                                    End If
                                End If
                                a = a + 1
                            End While
                        End If
                        If pidcheck = "Valid PID/Valid IV" Then
                            pidc = Color.Green
                        Else
                            pidcheck = "Invalid PID"
                            If abilityp = False Then
                                pidcheck = pidcheck & " Invalid ability"
                            End If
                            If genderp = False Then
                                pidcheck = pidcheck & " Invalid gender"
                            End If
                            If naturep = False Then
                                pidcheck = pidcheck & " Invalid nature"
                            End If
                            pidc = Color.Red
                        End If
                    End If
                End If
                'Event Gen 4 pokemon.pid check

                If pokemon.isShiny Then
                    legalityform.Label20.Text = "Shiny"
                    If pokemon.species = "Celebi" Or pokemon.species = "Arceus" Or pokemon.species = "Victini" Or pokemon.species = "Zekrom" Or pokemon.species = "Reshiram" Or pokemon.species = "Keldeo" Or pokemon.species = "Meloetta" Then
                        shinycheck = pokemon.species & " cannot be shiny"
                        shinyc = Color.Red
                    Else
                        shinycheck = "Valid shiny"
                        shinyc = Color.Green
                    End If
                Else
                    shinycheck = ""
                    legalityform.Label20.Text = ""
                    shinyc = Color.Black
                End If
                If pokemon.no <= 493 Then
                    gencheck = "Valid gen"
                    genc = Color.Green
                Else
                    gencheck = "Invalid gen, this pokemon appeared in gen 5"
                    genc = Color.Red
                End If
            Else
                'If gen = 3 Then
                '    otcheck = "Currently no OT analysis due to trash bytes check"
                '    otc = Color.Black
                '    'Gen 3 legality analysis
                '    'Trash bytes are pal parked
                '    'pokemon.pid search through Methods 1-4
                '    'Dim eventnick As Boolean = False
                '    Dim r As String = palparkcheck()
                '    If pokemon.nicktb = r Then
                '        nickc = Color.Green
                '        nickcheck = "Valid pal park trash bytes"
                '    Else
                '        nickc = Color.Orange
                '        nickcheck = "Unknown pal park trash bytes (Pt/HG/SS)"
                '        If ComboBox9.SelectedItem = "Diamond" Or ComboBox9.SelectedItem = "Pearl" Then
                '            nickc = Color.Red
                '            nickcheck = "Invalid pal park trash bytes (D/P)"
                '        End If
                '    End If
                '    'z = 0
                '    'While z < ots.Length - 1
                '    '    temp = z + 1
                '    '    If ots(temp) = "FFFF" Then
                '    '        otcount = otcount + 1
                '    '    End If
                '    '    If ots(temp) = "0000" Then
                '    '        ot0cont = ot0cont + 1
                '    '    End If
                '    '    z = z + 1
                '    'End While
                '    'z = 0
                '    'If nicklenght < 10 And nicklenght >= 1 Then
                '    '    If nickcount = 2 Then
                '    '        nickcheck = "Valid nickname trash bytes " & pokemon.nicktb
                '    '        nickc = Color.Green
                '    '    Else
                '    '        If nickcount = 11 - nicks.Length Then
                '    '            nickcheck = "Valid 4 gen event nickname trash bytes " & pokemon.nicktb
                '    '            nickc = Color.Green
                '    '            eventnick = True
                '    '        End If
                '    '        nickcheck = "Invalid nickname trash bytes " & pokemon.nicktb
                '    '        nickc = Color.Red
                '    '    End If
                '    'Else
                '    '    If nickcount = 1 Then
                '    '        nickcheck = "Valid nickname trash bytes " & pokemon.nicktb
                '    '        nickc = Color.Green
                '    '    Else
                '    '        nickcheck = "Invalid nickname trash bytes " & pokemon.nicktb
                '    '        nickc = Color.Red
                '    '    End If
                '    'End If
                '    'If otlenght < 7 And otlenght >= 1 Then
                '    '    If otcount = 1 And ot0cont = (7 - otlenght) Then
                '    '        otcheck = "Valid OT pal parked trash bytes  " & pokemon.ottb
                '    '        otc = Color.Green
                '    '    Else
                '    '        otcheck = "Invalid OT trash bytes " & pokemon.ottb
                '    '        otc = Color.Red
                '    '    End If
                '    'Else
                '    '    If otcount = 1 And ot0cont = 0 Then
                '    '        otcheck = "Valid OT trash bytes " & pokemon.ottb
                '    '        otc = Color.Green
                '    '    Else
                '    '        otcheck = "Invalid OT trash bytes " & pokemon.ottb
                '    '        otc = Color.Red
                '    '    End If
                '    'End If
                '    If eggloc = "" And pokemon.locationmet = "Pokétransfer")) Then
                '        locc = Color.Green
                '        eggcheck = "Valid location Poketransfer"
                '    Else
                '        locc = Color.Red
                '        eggcheck = "Invalid location, for gen 3 only valid location is Poketransfer"
                '    End If

                '    If invalidlocation = "Invalid" Then
                '        locc = Color.Red
                '        eggcheck = "Unknown location, for gen 3 only valid location is Poketransfer"
                '    Else
                '        If invalidlocation = "Unknown" Then
                '            locc = Color.Red
                '            eggcheck = "Unknown location, for gen 3 only valid location is Poketransfer"
                '        End If
                '    End If
                '    If abilitychanged = "Valid" Then
                '        abicheck = "Valid ability"
                '        abic = Color.Green
                '        If dwgen4 = "DW" Then
                '            abicheck = "Has DW ability but is from a gen before Dream World"
                '            abic = Color.Red
                '        End If
                '    Else
                '        abicheck = "Invalid ability"
                '        abic = Color.Red
                '        If dwgen4 = "DW" Then
                '            abicheck = "Has DW ability but is from a gen before Dream World"
                '            abic = Color.Red
                '        End If
                '    End If
                '    Dim pokeball As String = pokeball
                '    Select Case pokeball
                '        Case "Poké Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Great Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Ultra Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Master Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Safari Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Net Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Dive Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Nest Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Repeat Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Timer Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Luxury Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Premier Ball"
                '            ballcheck = "Valid"
                '            ballc = Color.Green
                '        Case "Dusk Ball"
                '            ballcheck = "Invalid Dusk Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Heal Ball"
                '            ballcheck = "Invalid Heal Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Quick Ball"
                '            ballcheck = "Invalid Quick Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Cherish Ball"
                '            ballcheck = "Invalid  Cherish Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Fast Ball"
                '            ballcheck = "Invalid Fast Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Lure Ball"
                '            ballcheck = "Invalid Lure Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Heavy Ball"
                '            ballcheck = "Invalid Heavy Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Love Ball"
                '            ballcheck = "Invalid Love Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Friend Ball"
                '            ballcheck = "Invalid Friend Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Level Ball"
                '            ballcheck = "Invalid Level Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Sport Ball"
                '            ballcheck = "Invalid Sport Ball is a gen 4 pokeball"
                '            ballc = Color.Red
                '        Case "Dream Ball"
                '            ballcheck = "Invalid Dream Ball is a gen 5 pokeball"
                '            ballc = Color.Red
                '    End Select
                '    If CheckBox6.Checked Then
                '        legalityform.Label20.Text = "Shiny"
                '        If pokemon.species = "Celebi" Or pokemon.species = "Arceus" Or pokemon.species = "Victini" Or pokemon.species = "Zekrom" Or pokemon.species = "Reshiram" Or pokemon.species = "Keldeo" Or pokemon.species = "Meloetta" Then
                '            shinycheck = pokemon.species & " cannot be shiny"
                '            shinyc = Color.Red
                '        Else
                '            shinycheck = "Valid shiny"
                '            shinyc = Color.Green
                '        End If
                '    Else
                '        shinycheck = ""
                '        legalityform.Label20.Text = ""
                '        shinyc = Color.Black
                '    End If
                '    legalityform.Label21.Text = "Gen"
                '    If ComboBox12.SelectedIndex <= 385 Then
                '        gencheck = "Valid gen"
                '        genc = Color.Green
                '    Else
                '        If ComboBox12.SelectedIndex <= 492 Then
                '            gencheck = "Invalid gen, this pokemon appeared in gen 4"
                '            genc = Color.Red
                '        Else
                '            gencheck = "Invalid gen, this pokemon appeared in gen 5"
                '            genc = Color.Red
                '        End If
                '    End If
                '    pidcheck = method1()
                '    Dim method As String
                '    If pidcheck = "" Then
                '        pidcheck = method2()
                '        If pidcheck = "" Then
                '            pidcheck = method3()
                '            If pidcheck = "" Then
                '                pidcheck = method4()
                '                If pidcheck = "" Then
                '                    pidcheck = "Invalid"
                '                Else
                '                    method = "Method 4"
                '                End If
                '            Else
                '                method = "Method 3"
                '            End If
                '        Else
                '            method = "Method 2"
                '        End If
                '    Else
                '        method = "Method 1"
                '    End If

                '    If pidcheck = "Valid PID/Valid IV" Then
                '        pidc = Color.Green
                '        pidcheck = pidcheck & " " & method
                '    Else
                '        pidcheck = "Invalid PID, didn't matched any Method 1-4"
                '        pidc = Color.Red
                '    End If
                'End If
            End If
        End If

        Dim learn1 As String = movesets(PkmLib.moves(pokemon.moveset.move1.move))
        Dim learn2 As String = movesets(PkmLib.moves(pokemon.moveset.move2.move))
        Dim learn3 As String = movesets(PkmLib.moves(pokemon.moveset.move3.move))
        Dim learn4 As String = movesets(PkmLib.moves(pokemon.moveset.move4.move))
        Dim move1c As Color
        If learn1 = "Invalid" Then
            move1c = Color.Red
        Else
            move1c = Color.Green
        End If
        Dim move2c As Color
        If learn2 = "Invalid" Then
            move2c = Color.Red
        Else
            move2c = Color.Green
        End If
        Dim move3c As Color
        If learn3 = "Invalid" Then
            move3c = Color.Red
        Else
            move3c = Color.Green
        End If
        Dim move4c As Color
        If learn4 = "Invalid" Then
            move4c = Color.Red
        Else
            move4c = Color.Green
        End If
        If pokemon.moveset.isEmpty() Then
            move1c = Color.Red
            move2c = Color.Red
            move3c = Color.Red
            move4c = Color.Red
        End If

        legalityform.Label8.Text = pokemon.gen
        legalityform.Label9.Text = nickcheck
        legalityform.Label10.Text = otcheck
        legalityform.Label11.Text = eggcheck
        legalityform.Label12.Text = ballcheck
        legalityform.Label13.Text = abicheck
        legalityform.Label14.Text = pidcheck
        legalityform.Label14.ForeColor = pidc
        legalityform.Label9.ForeColor = nickc
        legalityform.Label10.ForeColor = otc
        legalityform.Label11.ForeColor = locc
        legalityform.Label12.ForeColor = ballc
        legalityform.Label13.ForeColor = abic
        legalityform.Label22.Text = gencheck
        legalityform.Label22.ForeColor = genc
        legalityform.Label23.Text = shinycheck
        legalityform.Label23.ForeColor = shinyc

        legalityform.Label16.Text = PkmLib.moves.Item(pokemon.moveset.move1.move) & " " & learn1
        legalityform.Label17.Text = PkmLib.moves.Item(pokemon.moveset.move2.move) & " " & learn2
        legalityform.Label18.Text = PkmLib.moves.Item(pokemon.moveset.move3.move) & " " & learn3
        legalityform.Label19.Text = PkmLib.moves.Item(pokemon.moveset.move4.move) & " " & learn4
        legalityform.Label16.ForeColor = move1c
        legalityform.Label17.ForeColor = move2c
        legalityform.Label18.ForeColor = move3c
        legalityform.Label19.ForeColor = move4c
        legalityform.Label24.Text = pokemon.species
        legalityform.PictureBox1.Image = pokemon.sprite
        legalityform.Text = "Legality Analysis"
        legalityform.ShowDialog()
    End Sub

    ''' <summary>
    ''' Return the type of move (Level-up, Egg, Tutor, TM, Previous gen, Event, Illegal)
    ''' </summary>
    Public Function movesets(ByVal move As String)
        Dim c As String = "Invalid"
        If move = "None" Then
            Return "Valid"
            Exit Function
        End If
        Dim y As Integer = pokemon.no
        Dim z As Integer = 0
        Dim levelup As String = ""
        Dim temp As String = ""
        Dim a() As String = My.Resources.levelup.Replace(vbLf, "").Split(vbCrLf)
        Dim b() As String
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    levelup = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim tmhm As String = ""
        a = My.Resources.tmhm.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    tmhm = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim eggmoves As String = ""
        a = My.Resources.egg.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    eggmoves = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim preevolution As String = ""
        a = My.Resources.preevo.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    preevolution = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        'Dim tt As String = Form1.preevoMovesets(species)
        'If tt <> "" Then
        '    preevolution = preevolution & "," & tt
        'End If
        z = 0
        Dim tutor As String = ""
        a = My.Resources.tutor.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    tutor = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim dw As String = ""
        If pokemon.locationmet = "Entree Forest" Or pokemon.locationmet = "Pokémon Dream Radar" Then
            a = My.Resources.dw.Replace(vbLf, "").Split(vbCrLf)
            While z < a.Length
                b = a(z).Split("=")
                If b(0) <> "" Then
                    If b(0) = y Then
                        dw = b(1)
                        Exit While
                    End If
                End If
                z += 1
            End While
            z = 0
        End If
        z = 0
        Dim special As String = ""
        If pokemon.isFateful Then
            a = My.Resources.special.Replace(vbLf, "").Split(vbCrLf)
            While z < a.Length
                b = a(z).Split("=")
                If b(0) <> "" Then
                    If b(0) = y Then
                        special = b(1)
                        Exit While
                    End If
                End If
                z += 1
            End While
            z = 0
        End If
        z = 0
        Dim gen3spec As String = ""
        a = My.Resources.gen3special.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    gen3spec = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim gen3tm As String = ""
        a = My.Resources.gen3tmhm.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    gen3tm = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim gen3tut As String = ""
        a = My.Resources.gen3tutor.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    gen3tut = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim gen4spec As String = ""
        a = My.Resources.gen4special.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    gen4spec = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim gen4tut As String = ""
        a = My.Resources.gen4tutor.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    gen4tut = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim gen4tm As String = ""
        a = My.Resources.gen4tmhm.Replace(vbLf, "").Split(vbCrLf)
        While z < a.Length
            b = a(z).Split("=")
            If b(0) <> "" Then
                If b(0) = y Then
                    gen4tm = b(1)
                    Exit While
                End If
            End If
            z += 1
        End While
        z = 0
        Dim m() As String = levelup.Split(",")
        While z < m.Length
            If m(z) = "" Or m(z) = "None" Then
                m(z) = "0"
            End If
            m(z) = PkmLib.moves(m(z))
            If move = m(z) Then
                c = "Level-up move"
                Return c
                Exit Function
            End If
            z += 1
        End While
        z = 0
        m = tmhm.Split(",")
        While z < m.Length
            If m(z) = "" Or m(z) = "None" Then
                m(z) = "0"
            End If
            m(z) = PkmLib.moves(m(z))
            If move = m(z) Then
                c = "TM/HM move"
                Return c
                Exit Function
            End If
            z += 1
        End While
        z = 0
        m = preevolution.Split(",")
        While z < m.Length
            If m(z) = "" Or m(z) = "None" Then
                m(z) = "0"
            End If
            m(z) = PkmLib.moves(m(z))
            If move = m(z) Then
                c = "Preevolution move"
                Return c
                Exit Function
            End If
            z += 1
        End While
        z = 0
        If pokemon.isHatched Or ((pokemon.version = "Ruby") Or (pokemon.version = "Sapphire") Or (pokemon.version = "Fire Red") Or (pokemon.version = "Leaf Green") Or (pokemon.version = "Emerald")) Then
            m = eggmoves.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Egg move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
        End If
        z = 0
        m = tutor.Split(",")
        While z < m.Length
            If m(z) = "" Or m(z) = "None" Then
                m(z) = "0"
            End If
            m(z) = PkmLib.moves(m(z))
            If move = m(z) Then
                c = "Tutor move"
                Return c
                Exit Function
            End If
            z += 1
        End While
        z = 0
        If pokemon.isFateful Then
            m = special.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Event move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
        End If
        z = 0
        If pokemon.locationmet = "Entree Forest" Or pokemon.locationmet = "Pokémon Dream Radar" Then
            m = dw.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Dream World move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
        End If
        z = 0
        If pokemon.gen = 4 Or pokemon.gen = 3 Then
            m = gen4tut.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Gen 4 Tutor move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
            z = 0
            m = gen4tm.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Gen 4 TM/HM move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
            z = 0
            m = gen4spec.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Gen 4 Event move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
            z = 0
        End If
        If pokemon.gen = 3 Then
            m = gen3tut.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Gen 3 Tutor move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
            z = 0
            m = gen3spec.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Gen 3 Event move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
            z = 0
            m = gen3tm.Split(",")
            While z < m.Length
                If m(z) = "" Or m(z) = "None" Then
                    m(z) = "0"
                End If
                m(z) = PkmLib.moves(m(z))
                If move = m(z) Then
                    c = "Gen 3 TM/HM move"
                    Return c
                    Exit Function
                End If
                z += 1
            End While
        End If
        'z = 0
        'Dim eventmove() As String
        'eventmove = My.Settings.moves.Replace(vbLf, "").Split(vbCrLf)
        'Dim temp2() As String
        'y = 0
        'z = 0
        'While y < eventmove.Length - 1
        '    If Form1.ComboBox12.SelectedItem = eventmove(y) Then
        '        temp2 = eventmove(y + 1).Split(",")
        '        z = 0
        '        While z < temp2.Length
        '            If move = Form1.movesid(Array.IndexOf(Form1.movesid, temp2(z))) Then
        '                c = "Event move"
        '                Return c
        '                Exit Function
        '            End If
        '            z = z + 1
        '        End While
        '    End If
        '    y = y + 1
        'End While
        Return "Invalid"
    End Function

    Public Function getAuthor() As String Implements IPlugin.getAuthor
        Return "Pikaedit"
    End Function

    Public Function getName() As String Implements IPlugin.getName
        Return "Legality Analysis"
    End Function

    Public Function getPluginInfo() As String Implements IPlugin.getPluginInfo
        Return "Get Legality Analysis of Loaded Pokemon (Using a limited version of Pikaedit v3.3.6 Legality Analysis)"
    End Function

    Public Property getPokemon As Pokemon Implements IPlugin.getPokemon
        Get
            Return pokemon
        End Get
        Set(value As Pokemon)
            pokemon = value
        End Set
    End Property

    Public Property getSaveFile As SaveFile Implements IPlugin.getSaveFile
        Get
            Return New SaveFile()
        End Get
        Set(value As SaveFile)

        End Set
    End Property

    Public Function getVersion() As String Implements IPlugin.getVersion
        Return "1.1"
    End Function

    Public ReadOnly Property setPostProcess As Process Implements IPlugin.setPostProcess
        Get
            Return Process.None
        End Get
    End Property

    Public ReadOnly Property setRequirement As Requirement Implements IPlugin.setRequirement
        Get
            Return Requirement.None
        End Get
    End Property

    Public Property getGen4Pokemon As PokemonGen4 Implements IPlugin.getGen4Pokemon
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As PokemonGen4)
            Throw New NotImplementedException()
        End Set
    End Property

    Public WriteOnly Property getLanguage As PkmLib.PikaeditLanguages Implements IPlugin.getLanguage
        Set(value As PkmLib.PikaeditLanguages)

        End Set
    End Property

    Public Property getPluginGen As PluginGen Implements IPlugin.getPluginGen
        Get
            Return PluginGen.Gen5
        End Get
        Set(value As PluginGen)

        End Set
    End Property

    Public Property getXYPokemon As PokemonXY Implements IPlugin.getXYPokemon
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As PokemonXY)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property getGen4SaveFile As SaveFileGen4 Implements IPlugin.getGen4SaveFile
End Class
