<%@ Page Title="" Language="C#" MasterPageFile="~/Dealxpo.master" AutoEventWireup="true" CodeBehind="GestionCarriere.aspx.cs" Inherits="dealxpoWEB.GestionCarriere" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
    Gestion de Carrière
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/form_style.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftMenu" Runat="Server">
    <ul id="content_menu" runat="server" >
        <li><asp:HyperLink ID="hlModifierEmploye" runat="server" NavigateUrl="ModifierEmploye.aspx" >Modifier Employé</asp:HyperLink></li>
        <li><asp:HyperLink ID="hlDemandeConge" runat="server" NavigateUrl="GestionConge.aspx" >Demande de Congé</asp:HyperLink></li>
        <li><asp:HyperLink ID="hlRetourConge" runat="server" NavigateUrl="GestionConge.aspx" >Retour de Congé</asp:HyperLink></li>
        <li><asp:HyperLink ID="hlPromotion" runat="server" NavigateUrl="GestionCarriere.aspx" >Donner une Promotion</asp:HyperLink></li>
        <li><asp:HyperLink ID="hlTransfert" runat="server" NavigateUrl="GestionCarriere.aspx" >Transférer</asp:HyperLink></li>
        <li><asp:HyperLink ID="hlRevocation" runat="server" NavigateUrl="GestionCarriere.aspx" >Révoquer</asp:HyperLink></li>
        <li><asp:HyperLink ID="hlEliminerEmploye" runat="server" NavigateUrl="EliminerEmploye.aspx" >Eliminer Employé</asp:HyperLink></li>
    </ul>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="entity_members">

        <div class="entity_members_header members_entry">
		    <span id="operation_title" runat="server" >Gestion Carrière</span>
	    </div>

        <asp:Label ID="lbEspaceEmploye" runat="server" />

        <div class="pair_input">
            <asp:Label ID="lblSuccess" runat="server" CssClass="session_message session_success"></asp:Label>
            <asp:Label ID="lblFailure" runat="server" CssClass="session_message session_failure"></asp:Label>
        </div>
    
        <div class="form_half_left" style="margin:5px 0 10px 5px;float:left;width:400px;">

            <div class="pair_input">
                <span class="pair_span">Département *:</span>
                <asp:DropDownList ID="ddlDepartement" runat="server" CssClass="entity_input_look entity_input_behaviour" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Grade *:</span>
                <asp:DropDownList ID="ddlGrade" runat="server" CssClass="entity_input_look entity_input_behaviour" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Poste *:</span>
                <asp:DropDownList ID="ddlPoste" runat="server" CssClass="entity_input_look entity_input_behaviour" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Date Début *:</span>
                <asp:TextBox ID="tbDebutStatut" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                (MM/DD/YYYY)
            </div>

            <div class="pair_input">
                <span class="pair_span">Motif *:</span>
                <asp:DropDownList ID="ddlMotifStatut" runat="server" CssClass="entity_input_look entity_input_behaviour" />
            </div>

            <div id="revocation_input" class="pair_input" runat="server" style="display:none;">
                <span class="pair_span">Date Fin *:</span>
                <asp:TextBox ID="tbFinStatut" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                (MM/DD/YYYY)
            </div>

            <asp:Button id="btnValiderPromotion" runat="server" style="display:none;" CssClass="entity_input_look form_entity_button" 
                Text="Valider Promotion" OnClick="btnValiderPromotion_Click" />

            <asp:Button id="btnValiderTransfert" runat="server" style="display:none;" CssClass="entity_input_look form_entity_button" 
                Text="Valider Transfert" OnClick="btnValiderTransfert_Click" />

            <asp:Button id="btnValiderRevocation" runat="server" style="display:none;" CssClass="entity_input_look form_entity_button" 
                Text="Valider Revocation" OnClick="btnValiderRevocation_Click" />

        </div>

         <div class="form_half_right" style="margin:0 0 10px 50px;float:left;width:350px;">
            <h4>Statut Actuel</h4>

            <div class="pair_input">
                <span class="">Département :</span>
                <asp:Label ID="lbDepartement" runat="server" />
            </div>

            <div class="pair_input">
                <span class="">Grade :</span>
                <asp:Label ID="lbGrade" runat="server" />
            </div>

            <div class="pair_input">
                <span class="">Poste :</span>
                <asp:Label ID="lbPoste" runat="server" />
            </div>

            <div class="pair_input">
                <span class="">Date d'entrée :</span>
                <asp:Label ID="lbDebut" runat="server" />
            </div>

            <div class="pair_input">
                <span class="">Motif :</span>
                <asp:Label ID="lbMotif" runat="server" />
            </div>

        </div>

         <div class="grid_table_big" >
            <asp:DataGrid ID="dgCarriere" runat="server" />
        </div>

    </div>
</asp:Content>


