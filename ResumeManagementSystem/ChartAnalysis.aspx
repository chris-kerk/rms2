<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChartAnalysis.aspx.cs" Inherits="ResumeManagementSystem.WebForm2" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Chart</h1>
<div>
    <asp:Label ID="lblChartType" Text="Select Chart Types:" runat="server" ></asp:Label>
    <asp:DropDownList ID="ddlChartType" runat="server" Width="35%" OnSelectedIndexChanged="ddlChartType_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
  </div>
    <div>
    <asp:Chart ID="SkillChart" runat="server" Width="585px" Visible="true">
    <Titles>
        <asp:Title ShadowOffset="3" Name="Items" />
    </Titles>
    <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
            LegendStyle="Row" />
    </Legends>
    <Series>
        <asp:Series Name="Level of Master" runat="server" ChartType="Pie"/>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="SkillChartArea1" BorderWidth="0" />
    </ChartAreas>
</asp:Chart>
    </div>
</asp:Content>
