﻿#pragma checksum "..\..\..\..\Windows\Pop-Ups\EditCategoryPopUpWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3932F43D7B9FF466CCFE26A452C4439D929DF607531EB9D211985F2A9F2F50F2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EventsManager.Windows.Pop_Ups;
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


namespace EventsManager.Windows.Pop_Ups {
    
    
    /// <summary>
    /// EditCategoryPopUpWindow
    /// </summary>
    public partial class EditCategoryPopUpWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\Windows\Pop-Ups\EditCategoryPopUpWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textCategory;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Windows\Pop-Ups\EditCategoryPopUpWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSubmit;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Windows\Pop-Ups\EditCategoryPopUpWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/EventsManager;component/windows/pop-ups/editcategorypopupwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\Pop-Ups\EditCategoryPopUpWindow.xaml"
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
            this.textCategory = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.buttonSubmit = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\Windows\Pop-Ups\EditCategoryPopUpWindow.xaml"
            this.buttonSubmit.Click += new System.Windows.RoutedEventHandler(this.buttonSubmit_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonCancel = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\Windows\Pop-Ups\EditCategoryPopUpWindow.xaml"
            this.buttonCancel.Click += new System.Windows.RoutedEventHandler(this.buttonCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
