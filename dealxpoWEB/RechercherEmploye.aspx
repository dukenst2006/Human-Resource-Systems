<%@ Page Title="" Language="C#" MasterPageFile="~/Dealxpo.master" AutoEventWireup="true" CodeBehind="RechercherEmploye.aspx.cs" Inherits="dealxpoWEB.RechercherEmploye" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
    Recherche d'Employés
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/form_style.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="entity_members">

        <div class="entity_members_header members_entry">
		    <span>Recherche d'Employés</span>
	    </div>

        <div class="form_submit_result">
            <asp:Label ID="lblSuccess" runat="server" CssClass="session_message session_success"></asp:Label>
            <asp:Label ID="lblFailure" runat="server" CssClass="session_message session_failure"></asp:Label>
        </div>
    
        <div class="form_half_left" style="margin:5px 0 10px 5px;float:left;width:800px;">

            <div class="search_wrapper" >
                <div class="search_input" >
                    <asp:TextBox id="tbMotsCles" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                    <div class="search_target_input" >
                        <asp:RadioButton GroupName="rbTarget" ID="rbCode" Text="Par Code" runat="server" CssClass="radio_input radio_content radio_content_position" />
                        <asp:RadioButton GroupName="rbTarget" ID="rbNom" Text="Par Nom" runat="server" CssClass="radio_input radio_content radio_content_position" />
                    </div>
                </div>

                <div class="search_submit" >
                    <asp:Button id="btnSubmit" runat="server" CssClass="entity_input_look form_entity_button" 
                        Text="Rechercher" OnClick="btnSubmit_Click" />
                </div>

                <div class="resultat_recherche"> 
                    <asp:Label ID="lbResultatRecherche" runat="server" >Résultat...</asp:Label>
                </div>
            </div>

            <div class="grid_table" >
                <asp:DataGrid ID="dgEmploye" runat="server" OnSelectedIndexChanged="gvEmploye_SelectedIndexChanged" />
            </div>
        </div>

    </div>

</asp:Content>


