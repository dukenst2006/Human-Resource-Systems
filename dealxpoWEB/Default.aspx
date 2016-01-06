<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="dealxpoWEB.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Login Form</title>
    <style type="text/css">
        .auto-style1 {
            height: 32px;
        }
        .auto-style2 {
            width: 327px;
        }
        .auto-style3 {
            height: 32px;
            width: 327px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div style="margin-left: 404px">
<table border="0" style="width: 381px">
<tr>
<td>
Username:
</td>
<td class="auto-style2">
<asp:TextBox ID="txtUserName" runat="server"/>
<asp:RequiredFieldValidator ID="rfvUser" ErrorMessage="Entrer un Username SVP" ControlToValidate="txtUserName" runat="server" />
</td>
</tr>
<tr>
<td>
Password:
</td>
<td class="auto-style2">
<asp:TextBox ID="txtPWD" runat="server" TextMode="Password"/>
<asp:RequiredFieldValidator ID="rfvPWD" runat="server" ControlToValidate="txtPWD" ErrorMessage="Entrer un Password SVP"/>
</td>
</tr>
<tr>
<td class="auto-style1">
</td>
<td class="auto-style3" colspan="0">
<asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" />
</td>
</tr>
</table>
</div>
</form>
</body>
</html>
