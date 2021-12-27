Imports System.Data
Imports System.Data.SqlClient

Public Class DisplayEmployee

    Private Sub DisplayEmployee_Load(sender As Object _
                                     , e As EventArgs) _
                                     Handles MyBase.Load
        Dim con_str As String = "workstation id=CR2013SRV;packet size=4096; " & _
                                "user id=pps;data source=CR2013SRV;persist " & _
                                "security info=True;initial catalog=" & _
                                "AdventureWorks;password=""p@ssw0rd"""
        Dim conn As New SqlConnection(con_str)
        Dim get_year As String = "select distinct (year(hiredate)+543) " & _
                                 "as hire_year " & _
                                 "from humanresources.employee"
        Dim get_marry_status As String = "select distinct maritalstatus " & _
                                         "from humanresources.employee"
        Try
            Dim dap_year As New SqlDataAdapter(get_year, conn)
            Dim hire_yr As New DataSet
            dap_year.Fill(hire_yr)
            Dim dap_status As New SqlDataAdapter(get_marry_status, conn)
            Dim status As New DataSet
            dap_status.Fill(status)
            conn.Close()
            cbYear.DataSource = hire_yr.Tables(0)
            cbYear.DisplayMember = "hire_year"
            cbYear.ValueMember = "hire_year"
            cbStatus.DataSource = status.Tables(0)
            cbStatus.DisplayMember = "maritalstatus"
            cbStatus.ValueMember = "maritalstatus"
        Catch ex As Exception
        End Try

       
    End Sub


    Private Sub bt_ok_Click(sender As Object _
                            , e As EventArgs) Handles bt_ok.Click
        Try
            Dim prf As New CrystalDecisions.Shared.ParameterFields
            Dim pf(1) As CrystalDecisions.Shared.ParameterField
            Dim para(1) As CrystalDecisions.Shared.ParameterDiscreteValue

            pf(0) = New CrystalDecisions.Shared.ParameterField
            pf(1) = New CrystalDecisions.Shared.ParameterField
            para(0) = New CrystalDecisions.Shared.ParameterDiscreteValue
            para(1) = New CrystalDecisions.Shared.ParameterDiscreteValue

            pf(0).ParameterFieldName = "hired_year"
            pf(1).ParameterFieldName = "marry_status"

            para(0).Value = CType(cbYear.SelectedValue, String)
            para(1).Value = CType(cbStatus.SelectedValue, String)

            'Set Parameter to Report
            pf(0).CurrentValues.Add(para(0))
            pf(1).CurrentValues.Add(para(1))
            prf.Add(pf(0))
            prf.Add(pf(1))
            Me.CRViewer.ParameterFieldInfo = prf
            Me.CRViewer.ReportSource = Application.StartupPath & "\employee.rpt"

            'Report Connection
            Me.CRViewer.LogOnInfo.Item(0).ConnectionInfo.ServerName = "CR2013SRV"
            Me.CRViewer.LogOnInfo.Item(0).ConnectionInfo.DatabaseName = "AdventureWorks"
            Me.CRViewer.LogOnInfo.Item(0).ConnectionInfo.UserID = "pps"
            Me.CRViewer.LogOnInfo.Item(0).ConnectionInfo.Password = "p@ssw0rd"

            Me.Visible = True
            Me.CRViewer.Refresh()
        Catch ex As Exception

        End Try

    End Sub


End Class
