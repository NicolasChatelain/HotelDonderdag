﻿#pragma checksum "..\..\..\MemberWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "80D4C463DDD949883439AF32E1402A0D7A932A6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Hotel.Presentation.Customer___Members;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Hotel.Presentation.Customer___Members {
    
    
    /// <summary>
    /// MemberWindow
    /// </summary>
    public partial class MemberWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox namebox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox birthdaybox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label dateformatlabel;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddMember;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveMembers;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UpdateConfirmation;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\MemberWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid MembersGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Hotel.Presentation;component/memberwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MemberWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.namebox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.birthdaybox = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\..\MemberWindow.xaml"
            this.birthdaybox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Birthdaybox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dateformatlabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.AddMember = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\MemberWindow.xaml"
            this.AddMember.Click += new System.Windows.RoutedEventHandler(this.AddMember_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SaveMembers = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\MemberWindow.xaml"
            this.SaveMembers.Click += new System.Windows.RoutedEventHandler(this.SaveMembers_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.UpdateConfirmation = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\MemberWindow.xaml"
            this.UpdateConfirmation.Click += new System.Windows.RoutedEventHandler(this.UpdateConfirmation_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.MembersGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

