﻿#pragma checksum "..\..\..\ui\FenConge.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A7876CD500AA0A34A1EA2982C96F4208"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace com.levivoir.rh.ui {
    
    
    /// <summary>
    /// FenConge
    /// </summary>
    public partial class FenConge : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbEspaceEmploye;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboType;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbAnnee;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpDebut;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpRetourPrevu;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpRetourEffectif;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnValiderDemande;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnValiderRetour;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\ui\FenConge.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgConge;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/gestionWPF;component/ui/fenconge.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ui\FenConge.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.lbEspaceEmploye = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.cboType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.tbAnnee = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.dpDebut = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.dpRetourPrevu = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.dpRetourEffectif = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.btnValiderDemande = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\ui\FenConge.xaml"
            this.btnValiderDemande.Click += new System.Windows.RoutedEventHandler(this.btnValiderDemande_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnValiderRetour = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\ui\FenConge.xaml"
            this.btnValiderRetour.Click += new System.Windows.RoutedEventHandler(this.btnValiderRetour_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\ui\FenConge.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgConge = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

