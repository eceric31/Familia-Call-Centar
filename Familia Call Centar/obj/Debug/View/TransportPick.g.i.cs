﻿#pragma checksum "..\..\..\View\TransportPick.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E597E2B7AA91AAC134A4C4953AE965CA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Familia_Call_Centar.View;
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


namespace Familia_Call_Centar.View {
    
    
    /// <summary>
    /// TransportPick
    /// </summary>
    public partial class TransportPick : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image kombiImage;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label kombiLabel;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image mopedImage;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label mopedLabel;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button zavrsiButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nazadButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid isporukaDataGrid;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\View\TransportPick.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock kolicinaTB;
        
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
            System.Uri resourceLocater = new System.Uri("/Familia Call Centar;component/view/transportpick.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\TransportPick.xaml"
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
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.kombiImage = ((System.Windows.Controls.Image)(target));
            
            #line 34 "..\..\..\View\TransportPick.xaml"
            this.kombiImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.kombiImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.kombiLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.mopedImage = ((System.Windows.Controls.Image)(target));
            
            #line 37 "..\..\..\View\TransportPick.xaml"
            this.mopedImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.mopedImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.mopedLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.zavrsiButton = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\View\TransportPick.xaml"
            this.zavrsiButton.Click += new System.Windows.RoutedEventHandler(this.zavrsiButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.nazadButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\View\TransportPick.xaml"
            this.nazadButton.Click += new System.Windows.RoutedEventHandler(this.nazadButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.isporukaDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 9:
            this.kolicinaTB = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
