Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web.Design
Imports System.IO
Imports System.Web
Imports System.Web.UI.Page


Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object _
                            , ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim con_str As String = "workstation id=CR2013SRV;packet size=4096; " & _
                                    "user id=pps;data source=CR2013SRV;persi" & _
                                    "st security info=False;initial catalog=" & _
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

                ddlYear.DataSource = hire_yr
                ddlYear.DataTextField = "hire_year"
                ddlYear.DataValueField = "hire_year"
                ddlYear.DataBind()
                ddlYear.Items.Add("เลือกปีที่จ้าง")
                ddlYear.SelectedValue = "เลือกปีที่จ้าง"

                ddlStatus.DataSource = status
                ddlStatus.DataTextField = "maritalstatus"
                ddlStatus.DataValueField = "maritalstatus"
                ddlStatus.DataBind()
                ddlStatus.Items.Add("เลือกสถานะ")
                ddlStatus.SelectedValue = "เลือกสถานะ"

            Catch ex As Exception

            End Try
        End If

    End Sub

    Public Sub CreateReport(ByVal sReport As String, ByVal arParams As Array, _
                            Optional ByVal DoParams As Boolean = True)
        Dim oRpt As New ReportDocument
        Dim oSubRpt As New ReportDocument
        Dim Counter As Integer
        Dim crSections As Sections
        Dim crSection As Section
        Dim crReportObjects As ReportObjects
        Dim crReportObject As ReportObject
        Dim crSubreportObject As SubreportObject
        Dim crDatabase As Database
        Dim crTables As Tables
        Dim crTable As Table
        Dim crLogOnInfo As TableLogOnInfo
        Dim crConnInfo As New ConnectionInfo
        Dim crParameterValues As ParameterValues
        Dim crParameterDiscreteValue As ParameterDiscreteValue
        Dim crParameterRangeValue As ParameterRangeValue
        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldDefinition As ParameterFieldDefinition
        Dim crParameterFieldDefinition2 As ParameterFieldDefinition
        Dim strFile As String
        Dim fi As FileInfo
        Dim tstr As String
        Dim sPath As String
        Dim sReportPath As String = HttpContext.Current.Request.ServerVariables _
                                                       ("APPL_PHYSICAL_PATH") & _
                                                       "\Reports\" & sReport
        Dim pos As Integer
        Try
            tstr = Microsoft.VisualBasic.Format(Now, "MM/dd/yyyy HH:mm:ss")

            oRpt.Load(sReportPath)

            crDatabase = oRpt.Database
            crTables = crDatabase.Tables

            For Each crTable In crTables
                With crConnInfo
                    .ServerName = "CR2013SRV"
                    .DatabaseName = "AdventureWorks"
                    .UserID = "pps"
                    .Password = "p@ssw0rd"
                End With
                crLogOnInfo = crTable.LogOnInfo
                crLogOnInfo.ConnectionInfo = crConnInfo
                crTable.ApplyLogOnInfo(crLogOnInfo)
            Next


            crSections = oRpt.ReportDefinition.Sections

            If DoParams Then

                crParameterFieldDefinitions = oRpt.DataDefinition.ParameterFields()
                For Counter = 0 To UBound(arParams)
                    crParameterFieldDefinition = _
                    crParameterFieldDefinitions.Item(Counter)

                    crParameterValues = crParameterFieldDefinition.CurrentValues
                    If Not IsArray(arParams(Counter)) Then

                        crParameterDiscreteValue = New ParameterDiscreteValue
                        crParameterDiscreteValue.Value = arParams(Counter)

                        crParameterValues.Add(crParameterDiscreteValue)
                    Else
                        crParameterRangeValue = New ParameterRangeValue
                        crParameterRangeValue.StartValue = arParams(Counter)(0)
                        crParameterRangeValue.EndValue = arParams(Counter)(1)
                        crParameterValues.Add(crParameterRangeValue)
                    End If

                    crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)
                Next
            End If
            Dim s As System.IO.MemoryStream = _
            oRpt.ExportToStream(ExportFormatType.PortableDocFormat)

            With HttpContext.Current.Response
                .ClearContent()
                .ClearHeaders()
                .ContentType = "application/pdf"
                .AddHeader("Content-Disposition", "inline; filename=Report.pdf")
                .BinaryWrite(s.ToArray)
                .End()
            End With
        Catch ex As System.Exception
            'LogError("cryPrinter.CreateReport", ex.ToString)

        Finally
            Erase arParams
        End Try
    End Sub

    Protected Sub bt_ok_Click(sender As Object, e As EventArgs) Handles bt_ok.Click
        If ddlYear.SelectedValue <> "เลือกปีที่จ้าง" And ddlStatus.SelectedValue <> "เลือกสถานะ" Then
            Dim para(1) As String

            para(0) = CType(ddlYear.SelectedValue, String)
            para(1) = CType(ddlStatus.SelectedValue, String)

            CreateReport("employee.rpt", para)

        End If

    End Sub
End Class