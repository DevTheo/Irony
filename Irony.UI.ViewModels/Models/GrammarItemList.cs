#region License
/* **********************************************************************************
 * Copyright (c) Roman Ivantsov
 * This source code is subject to terms and conditions of the MIT License
 * for Irony. A copy of the license can be found in the License.txt file
 * at the root of this distribution. 
 * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
 * MIT License.
 * You must not remove this notice from this software.
 * **********************************************************************************/
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Irony.UI.ViewModels.Models
{

    public class GrammarItemList : List<GrammarItem>
    {
        public static GrammarItemList FromXml(string xml)
        {
            GrammarItemList list = new GrammarItemList();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            XmlNodeList xlist = xdoc.SelectNodes("//Grammar");
            foreach (XmlElement xitem in xlist)
            {
                GrammarItem item = new GrammarItem(xitem);
                list.Add(item);
            }
            return list;
        }
        //public static GrammarItemList FromCombo(ComboBox combo)
        //{
        //    GrammarItemList list = new GrammarItemList();
        //    foreach (GrammarItem item in combo.Items)
        //        list.Add(item);
        //    return list;
        //}

        public string ToXml()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement xlist = xdoc.CreateElement("Grammars");
            xdoc.AppendChild(xlist);
            foreach (GrammarItem item in this)
            {
                XmlElement xitem = xdoc.CreateElement("Grammar");
                xlist.AppendChild(xitem);
                item.Save(xitem);
            } //foreach
            return xdoc.OuterXml;
        }//method

        //public void ShowIn(ComboBox combo)
        //{
        //    combo.Items.Clear();
        //    foreach (GrammarItem item in this)
        //        combo.Items.Add(item);
        //}

    }//class
}
