﻿

#pragma checksum "C:\Users\Utilisateur\Desktop\Cali Wp\App2\App2\App2.Windows\GoalWeight.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EE9D2AD0E4158EF01661AC079AE35262"
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
                #line 13 "..\..\..\GoalWeight.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 14 "..\..\..\GoalWeight.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarButton_Click_1;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 31 "..\..\..\GoalWeight.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.WeightList_SelectionChanged_1;
                 #line default
                 #line hidden
                #line 31 "..\..\..\GoalWeight.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DragLeave += this.WeightList_DragLeave;
                 #line default
                 #line hidden
                #line 31 "..\..\..\GoalWeight.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.WeightList_Tapped;
                 #line default
                 #line hidden
                #line 31 "..\..\..\GoalWeight.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).DragItemsStarting += this.WeightList_DragItemsStarting;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


