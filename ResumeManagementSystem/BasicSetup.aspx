<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BasicSetup.aspx.cs" Inherits="ResumeManagementSystem.BasicSetup" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Basic Details</h1>
    <script type="text/javascript">
        function Confirmation() {
            if (confirm('Confirm to update the detail ?')) {
                return true;
            } else {
                return false;
            }
        }
    </script>
    <div style="width: 100%; height: 300px; display: list-item">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div style="float: left; width: 50%;">
                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                        <colgroup>
                            <col width="21%">
                            <col width="70%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAlias" runat="server" Text="Alias"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAlias" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblnation" runat="server" Text="Nationality"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlNation" runat="server" Width="80%">
                                        <asp:ListItem Text="Malaysian" Value="MYS" />
                                        <asp:ListItem Text="Singaporean" Value="SGD" />
                                        <asp:ListItem Text="Indonesian" Value="IND" />
                                        <asp:ListItem Text="Burmese" Value="BUR" />
                                        <asp:ListItem Selected="True" Text="None" Value="" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblHolder" runat="server" Text="Holder"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHolder" runat="server" Width="80%">
                                        <asp:ListItem Text="Work Permit" Value="WP" />
                                        <asp:ListItem Text="S Pass" Value="SP" />
                                        <asp:ListItem Text="E Pass" Value="EP" />
                                        <asp:ListItem Text="Singapore PR" Value="PR" />
                                        <asp:ListItem Selected="True" Text="None" Value="" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblContact" runat="server" Text="Contact"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContact" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblOthers" runat="server" Text="Others"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOthers" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" AutoPostback="false" OnClick="btnUpdate_Click" OnClientClick="return Confirmation();" Text="Update" />
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                            </col>
                    </col>
                        </colgroup>
                    </table>

                </div>


                <div style="float: left; width: 30%; margin-left: 2em">
                    <asp:FormView ID="FormView1" runat="server">
                        <ItemTemplate>
                            <asp:Image ID="imgPhoto" Width="150px" Height="150px" BorderColor="#ccc" runat="server" ImageUrl='<%# Bind("ImagePath") %>' />

                        </ItemTemplate>
                    </asp:FormView>
                    <asp:Image ID="imgPhoto" Width="140px" Height="150px" BorderStyle="Double" BorderColor="#cccccc" runat="server" ImageUrl='<%# Bind("ImagePath") %>' />

                    <asp:FileUpload ID="PhotoUpload" runat="server" />
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                    <asp:Label ID="lblMsgPhoto" runat="server" Text=""></asp:Label>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>

