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
    public class AraGridOfIconsIten
    {
        private AraObjectInstance<AraGridOfIcons> _Father= new AraObjectInstance<AraGridOfIcons>();
        public AraGridOfIcons Father
        {
            get { return _Father.Object; }
            set {
                _Father.Object = value;
                Active();
            }
        }
        
       
        string _Key;
        public string Key
        {
            get
            {
                return _Key;
            }
        }

        public AraGridOfIconsIten(string vKey ,string vCaption,string vImg)
        {
            _Key =vKey;
            _Caption = vCaption;
            if (vImg.ToLower().IndexOf("http://") == 0 || vImg.ToLower().IndexOf("https://") == 0)
                _Img = vImg;
            else
            {
                string vUrl = AraTools.UrlServer;
                string vUrlPath = vUrl.Substring(0, vUrl.LastIndexOf("/")+1);

                _Img = vUrlPath + vImg;
            }
        }

        public void Active()
        {
            Father.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.AddIco('" + AraTools.StringToStringJS(_Key) + "','" + AraTools.StringToStringJS(_Img) + "','" + AraTools.StringToStringJS(_Caption) + "'); \n");
        }

        string _Caption;
        public string Caption
        {
            get
            {
                return _Caption;
            }
        }

        string _Img;
        public string Img
        {
            get
            {
                return _Img;
            }
        }

        int _Left=0;
        public int Left
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;
                
                Father.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetIcoLeft('" + AraTools.StringToStringJS(_Key) + "'," + _Left + "); \n");
            }
        }

        int _Top = 0;
        public int Top
        {
            get
            {
                return _Top;
            }
            set
            {
                _Top = value;
                Father.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetIcoTop('" + AraTools.StringToStringJS(_Key) + "'," + _Top + "); \n");

            }
        }

        //public delegate void d_Click(string vKey,AraEventMouse vMouse);
        //public d_Click Click;
        //public void RunClick()
        //{
        //    try
        //    {
        //        this.Click(_Key,new AraEventMouse());
        //    }
        //    catch(Exception err) 
        //    {
        //        throw new Exception("Error on Click Icons.\n Err: " + err.Message);
        //    }
        //}

        //public delegate void d_ChangePosition(string vKey);
        //event d_ChangePosition _ChangePosition;
        //public d_ChangePosition ChangePosition
        //{
        //    set
        //    {
        //        _ChangePosition = value;

        //        Father.TickScriptCall();
        //        Tick.GetTick().Script.Send(" vObj.Event_ChangePosition = " + (_ChangePosition.GetInvocationList().Length > 0 ? "true" : "false") + "; \n");
        //    }
        //    get { return _ChangePosition; }
        //}

        public void SetInternalValue_LeftTop(int vLeft, int vTop)
        {
            _Left = vLeft;
            _Top = vTop;

            //if (this.Father.ChangePositionIco.InvokeEvent != null)
            //    this.Father.ChangePositionIco.InvokeEvent(this);
        }

        public object Tag = null;

    }

}
