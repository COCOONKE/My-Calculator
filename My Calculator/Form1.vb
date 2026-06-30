Public Class Form1
    Dim operand1 As Double = 0
    Dim currentOperator As String = ""
    Dim isCalculationDone As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDisplay.Text = "0"
    End Sub

    ' --- 1. NUMBER BUTTONS & DOT ---
    ' Make sure your 0-9 and Dot buttons have this selected in their Click Event (Lightning Bolt)
    Private Sub Number_Click(sender As Object, e As EventArgs) Handles Button16.Click, Button10.Click, Button20.Click, Button24.Click, Button23.Click, Button19.Click, Button11.Click, Button12.Click, Button18.Click, Button22.Click, Button25.Click
        Dim btn = CType(sender, Button)

        ' If a previous calculation was done, clear screen completely for a fresh start
        If isCalculationDone Then
            lblDisplay.Text = ""
            isCalculationDone = False
        End If

        ' Prevent appending multiple leading zeros
        If lblDisplay.Text = "0" AndAlso btn.Text <> "." Then
            lblDisplay.Text = btn.Text
        Else
            ' Append the number or dot straight to the display string
            If btn.Text = "." Then
                ' Only allow a dot if the current segment doesn't have one
                If Not lblDisplay.Text.EndsWith(".") Then
                    lblDisplay.Text &= "."
                End If
                Exit Sub
            End If
            lblDisplay.Text &= btn.Text
        End If
    End Sub

    ' --- 2. OPERATOR BUTTONS (+, -, x, ÷) ---
    ' Make sure your +, -, x, ÷ buttons have this selected in their Click Event (Lightning Bolt)
    Private Sub Operator_Click(sender As Object, e As EventArgs) Handles Button29.Click, Button28.Click, Button27.Click, Button26.Click
        Dim btn = CType(sender, Button)

        ' Don't allow starting with an operator if display is empty or error
        If lblDisplay.Text = "0" OrElse lblDisplay.Text.Contains("Error") Then Exit Sub

        ' If an operator is already present in the string, ignore extra clicks
        If lblDisplay.Text.Contains("+") OrElse lblDisplay.Text.Contains("-") OrElse lblDisplay.Text.Contains("x") OrElse lblDisplay.Text.Contains("÷") Then
            Exit Sub
        End If

        ' Save the first number and the operator symbol, then append it to the screen text
        currentOperator = btn.Text.Trim
        lblDisplay.Text &= " " & currentOperator & " "
        isCalculationDone = False
    End Sub

    ' --- 3. EQUALS BUTTON (=) ---
    ' Double click your '=' button and make sure it points here
    Private Sub btnEquals_Click(sender As Object, e As EventArgs) Handles btnEquals.Click
        Try
            ' Split the display text by spaces to read parts (e.g., "9", "x", "5")
            Dim parts As String() = lblDisplay.Text.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

            ' Ensure we have all 3 parts needed for a basic equation
            If parts.Length < 3 Then Exit Sub

            operand1 = Convert.ToDouble(parts(0))
            Dim op As String = parts(1)
            Dim operand2 As Double = Convert.ToDouble(parts(2))
            Dim result As Double = 0

            Select Case op
                Case "+"
                    result = operand1 + operand2
                Case "-"
                    result = operand1 - operand2
                Case "x", "×"
                    result = operand1 * operand2
                Case "÷", "/"
                    If operand2 = 0 Then
                        lblDisplay.Text = "Cannot divide by zero"
                        Exit Sub
                    Else
                        result = operand1 / operand2
                    End If
                Case Else
                    Exit Sub
            End Select

            ' This formatting shows "9 x 5 = 45" completely on screen for your assignment snapshot!
            lblDisplay.Text &= " = " & result.ToString()
            isCalculationDone = True

        Catch ex As Exception
            lblDisplay.Text = "Error"
        End Try
    End Sub

    ' --- 4. CLEAR (C) BUTTON ---
    Private Sub btnC_Click(sender As Object, e As EventArgs) Handles btnC.Click
        lblDisplay.Text = "0"
        operand1 = 0
        currentOperator = ""
        isCalculationDone = False
    End Sub
End Class
