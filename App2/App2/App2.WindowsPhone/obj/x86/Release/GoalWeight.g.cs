﻿

#pragma checksum "C:\Users\Chi\Desktop\App2\App2\App2.WindowsPhone\GoalWeight.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "216EA69A6D02B7FA1DC4041FD09582BF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App2
{
    partial class GoalWeight : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 14 "..\..\..\GoalWeight.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarButton_Click_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 27 "..\..\..\GoalWeight.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.WeightList_SelectionChanged_1;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


