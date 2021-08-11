﻿#pragma checksum "..\..\..\Windows\AddNewEventWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "190846C2505A86437B2EDB06B9B4C45FD438CE04546B7E4BE8050E1BC875F43B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EventsManager.Windows;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace EventsManager.Windows {
    
    
    /// <summary>
    /// AddNewEventWindow
    /// </summary>
    public partial class AddNewEventWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\..\Windows\AddNewEventWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textTitle;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Windows\AddNewEventWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxCategory;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Windows\AddNewEventWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxSubCategory;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\Windows\AddNewEventWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textEventDetails;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\Windows\AddNewEventWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewModes;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\Windows\AddNewEventWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSaveEvent;
        
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
            System.Uri resourceLocater = new System.Uri("/EventsManager;component/windows/addneweventwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AddNewEventWindow.xaml"
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
            this.textTitle = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.comboBoxCategory = ((System.Windows.Controls.ComboBox)(target));
            
            #line 57 "..\..\..\Windows\AddNewEventWindow.xaml"
            this.comboBoxCategory.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBoxCategory_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.comboBoxSubCategory = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.textEventDetails = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.listViewModes = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            this.buttonSaveEvent = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\..\Windows\AddNewEventWindow.xaml"
            this.buttonSaveEvent.Click += new System.Windows.RoutedEventHandler(this.buttonSaveEvent_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
