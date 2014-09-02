using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcCatalog;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.CatalogUI;



namespace ExploreFromHereAddin
{
    public class ExploreFromHere : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ExploreFromHere()
        {
        }

        protected override void OnClick()
        {
            IGxObject gxParent;
            IGxObject gxObj = ArcCatalog.ThisApplication.SelectedObject;
            string sCommand1;

            try
            {
                if (gxObj is IGxFolder)
                {
                    //do nothing
                }else{
                    gxParent = gxObj.Parent;
                    if (gxObj.Parent == null)
                    {
                        MessageBox.Show("Could not get parent");
                        return;
                    }
                    if (gxParent is IGxFolder){
                        gxObj = gxParent;
                    }else{
                        MessageBox.Show("Parent is not a folder");
                        return;
                    }
                }

                //Pass the folder location to the Shell
                sCommand1 =  "\"" + gxObj.FullName + "\"";
                System.Diagnostics.Process.Start("explorer.exe ", sCommand1);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "Explore from here");
            }

        }

        protected override void OnUpdate()
        {
            IGxObject gxParent;
            IGxObject gxObj = ArcCatalog.ThisApplication.SelectedObject;
            if (gxObj is IGxFolder){
                this.Enabled= true;
                return;
            }else
            {
                gxParent = gxObj.Parent;
                if (gxObj.Parent == null)
                {
                    this.Enabled = false;
                    return;
                }
                if (gxParent is IGxFolder)
                {
                    this.Enabled = true;

                }
                else
                {
                    this.Enabled = false;
                }

            }

        }
    }
}
