<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Experience.aspx.cs" Inherits="ResumeManagementSystem.Experience" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Career History</h1>
    <script type="text/javascript">
        function Confirmation() {
            if (confirm('Confirm to update the detail ?')) {
                return true;
            } else {
                return false;
            }
        }
        function PopulateDays() {
            var ddlMonthF = document.getElementById("<%=ddlMonthF.ClientID%>");
            var ddlYearF = document.getElementById("<%=ddlYearF.ClientID%>");
            var ddlMonthT = document.getElementById("<%=ddlMonthF.ClientID%>");
            var ddlYearT = document.getElementById("<%=ddlYearF.ClientID%>");

            var y = ddlYearF.options[ddlYear.selectedIndex].value;
            var m = ddlMonthF.options[ddlMonth.selectedIndex].value != 0;
            if (ddlMonthF.options[ddlMonth.selectedIndex].value != 0 && ddlYearF.options[ddlYear.selectedIndex].value != 0) {
                var dayCount = 32 - new Date(ddlYear.options[ddlYear.selectedIndex].value, ddlMonthF.options[ddlMonth.selectedIndex].value - 1, 32).getDate();
                ddlDayF.options.length = 0;
                AddOption(ddlDayF, "DD", "0");
                for (var i = 1; i <= dayCount; i++) {
                    AddOption(ddlDayF, i, i);
                }
            }
        }

        function AddOption(ddl, text, value) {
            var opt = document.createElement("OPTION");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }

        function Validate(sender, args) {
            var ddlMonthF = document.getElementById("<%=ddlMonthF.ClientID%>");
            var ddlYearF = document.getElementById("<%=ddlYearF.ClientID%>");
            var ddlMonthT = document.getElementById("<%=ddlMonthT.ClientID%>");
            var ddlYearT = document.getElementById("<%=ddlYearT.ClientID%>");
            args.IsValid = (ddlDay.selectedIndex != 0 && ddlMonth.selectedIndex != 0 && ddlYear.selectedIndex != 0)
        }
    </script>
    <div style="width: 85%; height: 300px; display: list-item; overflow-y: auto">
        <table border="0" cellpadding="2" cellspacing="2" width="85%">
            <colgroup>
                <col width="30%">
                <col width="70%">
            <tr>
                <td>
                    <asp:Label ID="lblComName" runat="server" Text="Company Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtComName" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDuration" runat="server" Text="Duration"></asp:Label>
                </td>
                <td>From
                    <asp:DropDownList ID="ddlMonthF" runat="server" />
                    <asp:DropDownList ID="ddlYearF" runat="server" onchange="PopulateDays()" />
                    To
                    <asp:DropDownList ID="ddlMonthT" runat="server" onchange="PopulateDays()" />
                    <asp:DropDownList ID="ddlYearT" runat="server" onchange="PopulateDays()" />

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPosition" runat="server" Text="Position Title"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPosition" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Label ID="lblDesc" runat="server" Text="Job Responsibility"></asp:Label>
                </td>
                <td>
                    <textarea id="txtDesc" typeof="input" runat="server" TextMode="Multiline" rows="10" maxlength="500"></textarea>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" AutoPostback="false" OnClick="btnUpdate_Click" OnClientClick="return Confirmation();" Text="Update" />
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
