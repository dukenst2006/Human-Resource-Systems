<%@ Page Title="" Language="C#" MasterPageFile="~/Dealxpo.master" AutoEventWireup="true" CodeBehind="GestionConge.aspx.cs" Inherits="dealxpoWEB.GestionConge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
    Gestion de Congé d'un Employé
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
		    <span id="operation_title" runat="server" >Gestion de Congé</span>
	    </div>
        <asp:Label ID="lbEspaceEmploye" runat="server" />

        <div class="pair_input">
            <asp:Label ID="lblSuccess" runat="server" CssClass="session_message session_success"></asp:Label>
            <asp:Label ID="lblFailure" runat="server" CssClass="session_message session_failure"></asp:Label>
        </div>

        <div class="form_half_left" style="margin:5px 0 10px 5px;float:left;width:400px;">

            <div class="pair_input" >
                <span class="pair_span">Type Congé *:</span>
                <asp:DropDownlist id="ddlType" runat="server" CssClass="entity_input_look entity_input_behaviour" />  
            </div>

            <div class="pair_input" >
                <span class="pair_span">Année Fiscale *:</span>
                <asp:TextBox id="tbAnnee" runat="server" CssClass="entity_input_look entity_input_behaviour" />  
            </div>

            <div class="pair_input" >
                <span class="pair_span">Début Congé *:</span>
                <asp:TextBox id="tbDebut" runat="server" CssClass="entity_input_look entity_input_behaviour" />  
            </div>

            <div class="pair_input" >
                <span class="pair_span">Retour Prevu *:</span>
                <asp:TextBox id="tbRetourPrevu" runat="server" CssClass="entity_input_look entity_input_behaviour" />  
            </div>

            <div class="pair_input" >
                <span class="pair_span">Retour Effectif *:</span>
                <asp:TextBox id="tbRetourEffectif" runat="server" CssClass="entity_input_look entity_input_behaviour" />  
            </div>

            <asp:Button id="btnValiderDemande" runat="server" style="display:none;" CssClass="entity_input_look form_entity_button" 
                Text="Valider Demande" OnClick="btnValiderDemande_Click" />

            <asp:Button id="btnValiderRetour" runat="server" style="display:none;" CssClass="entity_input_look form_entity_button" 
                Text="Valider Retour" OnClick="btnValiderRetour_Click" />

        </div>

        <div class="grid_table_big" >
            <asp:DataGrid ID="dgConge" runat="server" />
        </div>

    </div>

</asp:Content>

