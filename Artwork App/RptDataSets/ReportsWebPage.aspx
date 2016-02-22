<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportsWebPage.aspx.cs" Inherits="Artwork_App.RptDataSets.ReportsWebPage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="411px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="675px">
            <LocalReport ReportPath="RptDataSets\Report1.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet3" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtWorkDBConnectionString %>" SelectCommand="SELECT [ArtType], [Total] FROM [TotalValueView]"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
