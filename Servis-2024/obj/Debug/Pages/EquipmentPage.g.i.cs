﻿#pragma checksum "..\..\..\Pages\EquipmentPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3695898F7F9ACB9ADF97E3C2338F58EF0B816D8DD394DB1F4241EDD2D22AAFBC"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Servis_2024.Pages;
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


namespace Servis_2024.Pages {
    
    
    /// <summary>
    /// EquipmentPage
    /// </summary>
    public partial class EquipmentPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\Pages\EquipmentPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGrid;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Pages\EquipmentPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTextByName;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\EquipmentPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchTextBySerial;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Pages\EquipmentPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Pages\EquipmentPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboClient;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Pages\EquipmentPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox category;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Pages\EquipmentPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox serialNumber;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Pages\EquipmentPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox problemDecription;
        
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
            System.Uri resourceLocater = new System.Uri("/Servis-2024;component/pages/equipmentpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\EquipmentPage.xaml"
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
            this.DataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 26 "..\..\..\Pages\EquipmentPage.xaml"
            this.DataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 29 "..\..\..\Pages\EquipmentPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Update);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 30 "..\..\..\Pages\EquipmentPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportToCSV);
            
            #line default
            #line hidden
            return;
            case 4:
            this.searchTextByName = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\..\Pages\EquipmentPage.xaml"
            this.searchTextByName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.searchNameChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.searchTextBySerial = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\..\Pages\EquipmentPage.xaml"
            this.searchTextBySerial.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.searchNameChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.comboClient = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.category = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.serialNumber = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.problemDecription = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

