<%@ Page Title="" Language="C#" MasterPageFile="~/Dealxpo.master" AutoEventWireup="true" CodeBehind="GestionEmploye.aspx.cs" Inherits="dealxpoWEB.GestionEmploye" %>

<asp:Content ID="Content1" ContentPlaceHolderID="page_title" Runat="Server">
    Gestion Employé
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/form_style.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="LeftMenu" Runat="Server">
            <h6><strong>Gestion Employé</strong></h6>
    <ul id="content_menu" runat="server" style="display:inline;" >
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
        <div class="entity_members_header members_entry" >
		    <h2><span id="operation_title" runat="server" >Gestion Employé</span></h2>
	    </div>
        <div class="form_submit_result">
            <asp:Label ID="lblSuccess" runat="server" CssClass="session_message session_success"></asp:Label>
            <asp:Label ID="lblFailure" runat="server" CssClass="session_message session_failure"></asp:Label>
        </div>

        <div class="form_half_left" style="margin:5px 0 10px 5px;float:left;width:400px;">

            <div class="pair_input" >
                <span class="pair_span">Code Employe*:</span>
                <asp:TextBox id="tbCode" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbCodeErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input" >
                <span class="pair_span">Nom *:</span>
                <asp:TextBox id="tbNom" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbNomErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Prenom *:</span>
                <asp:Textbox id="tbPrenom" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbPrenomErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input">
               <span class="pair_span">Sexe *:</span>
                <asp:DropDownList id="ddlSexe" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbSexeErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Date de Naissance :</span>
                <asp:TextBox ID="tbDateNaissance" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbDateNaissanceErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input" >
                <span class="pair_span">Telephone :</span>
                <asp:TextBox id="tbTelephone" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbTelephoneErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input" >
                <span class="pair_span">Email :</span>
                <asp:TextBox id="tbEmail" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbEmailErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input" >
                <span class="pair_span">Adresse :</span>
                <asp:TextBox id="tbAdresse" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbAdresseErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input" >
                <span class="pair_span">Surplus Salaire :</span>
                <asp:TextBox id="tbSurplusSalaire" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbSurplusSalaireErr" runat="server" class="input_error" />
            </div>
           
            <div class="pair_input">
                <span class="pair_span">Département *:</span>
                <asp:DropDownList ID="ddlDepartement" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbDepartementErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Grade *:</span>
                <asp:DropDownList ID="ddlGrade" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbGradeErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Poste *:</span>
                <asp:DropDownList ID="ddlPoste" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbPosteErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Date d'Entrée *:</span>
                <asp:TextBox ID="tbDebutStatut" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbDebutStatutErr" runat="server" class="input_error" />
            </div>

            <div class="pair_input">
                <span class="pair_span">Motif *:</span>
                <asp:DropDownList ID="ddlMotifStatut" runat="server" CssClass="entity_input_look entity_input_behaviour" />
                <div id="lbMotifStatutErr" runat="server" class="input_error" />
            </div>
         
            <asp:Button id="btnEmbaucher" runat="server" Style="display:none;" CssClass="entity_input_look form_entity_button" 
                Text="Embaucher" OnClick="btnEmbaucher_Click" />

            <asp:Button id="btnModifier" runat="server" Style="display:none;" CssClass="entity_input_look form_entity_button" 
                Text="Modifier" OnClick="btnModifier_Click" />

            <asp:Button id="btnEliminer" runat="server" Style="display:none;" CssClass="entity_input_look form_entity_button" 
                Text="Eliminer" OnClick="btnEliminer_Click" />

        </div>
    </div>

</asp:Content>
