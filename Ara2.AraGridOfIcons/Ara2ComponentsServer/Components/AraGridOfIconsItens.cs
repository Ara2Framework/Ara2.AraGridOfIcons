// Copyright (c) 2010-2016, Rafael Leonel Pontani. All rights reserved.
// For licensing, see LICENSE.md or http://www.araframework.com.br/license
// This file is part of AraFramework project details visit http://www.arafrework.com.br
// AraFramework - Rafael Leonel Pontani, 2016-4-14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Ara2.Components
{
    [Serializable]
    public class AraGridOfIconsItens
    {
        private AraObjectInstance<AraGridOfIcons> _Father = new AraObjectInstance<AraGridOfIcons>();
        AraGridOfIcons Father
        {
            get { return _Father.Object; }
            set { _Father.Object = value; }
        }

        List<AraGridOfIconsIten> Itens = new List<AraGridOfIconsIten>();

        public AraGridOfIconsItens(AraGridOfIcons vFather)
        {
            Father = vFather;
        }

        public AraGridOfIconsIten Add(AraGridOfIconsIten vIco)
        {
            vIco.Father = Father;
            Itens.Add(vIco);
            return vIco;
        }

        public void Del(AraGridOfIconsIten vIco)
        {
            Itens.Remove(vIco);
            Father.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.DelIco('" + AraTools.StringToStringJS(vIco.Key) + "'); \n");
        }

        public int Count
        {
            get
            {
                return Itens.Count;
            }
        }

        public AraGridOfIconsIten this[int vIndex]
        {
            get
            {
                try
                {
                    return Itens[vIndex];
                }
                catch { return null; }
            }
        }

        public AraGridOfIconsIten this[string Key]
        {
            get
            {
                foreach (AraGridOfIconsIten Ico in Itens)
                {
                    if (Ico.Key ==Key)
                        return Ico;
                }
                return null;
            }
        }

        public void Clear()
        {
            Itens = new List<AraGridOfIconsIten>();
            Father.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.ClearIcos(); \n");
        }

        public AraGridOfIconsIten[] ToArray
        {
            get
            {
                return Itens.ToArray();
            }
        }

    }

 
}
