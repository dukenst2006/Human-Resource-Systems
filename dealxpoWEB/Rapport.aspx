<%@ Page Title="" Language="C#" MasterPageFile="~/Dealxpo.master" AutoEventWireup="true" CodeBehind="Rapport.aspx.cs" Inherits="dealxpoWEB.Rapport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
    Rapport
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/form_style.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftMenu" Runat="Server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="entity_members flow_control ">
        <div class="entity_members_header members_entry">
		    <span id="operation_title" runat="server">Rapport</span>
	    </div>

        <div class="grid_table_big" >
            <asp:GridView ID="gvRapport" runat="server" />
        </div>

        <div class="grid_table_big" >
            <asp:GridView ID="gvRapport2" runat="server" />
        </div>

    </div>

</asp:Content>