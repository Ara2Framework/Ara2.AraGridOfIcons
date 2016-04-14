// Copyright (c) 2010-2016, Rafael Leonel Pontani. All rights reserved.
// For licensing, see LICENSE.md or http://www.araframework.com.br/license
// This file is part of AraFramework project details visit http://www.arafrework.com.br
// AraFramework - Rafael Leonel Pontani, 2016-4-14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using Ara2;
using Ara2.Dev;

namespace Ara2.Components
{
    [Serializable]
    [AraDevComponent]
    public class AraGridOfIcons : AraComponentVisualAnchorConteiner, IAraDev
    {

        // Padão Ara 

        public AraGridOfIcons(IAraObject ConteinerFather)
            : base(AraObjectClienteServer.Create(ConteinerFather, "Div"), ConteinerFather, "AraGridOfIcons")
        {
            ClickIco = new AraComponentEvent<DEventIco>(this,"ClickIco");
            ChangePositionIco = new AraComponentEvent<DEventIco>(this, "ChangePositionIco");    
            Click = new AraComponentEvent<EventHandler>(this, "Click");

            Itens = new AraGridOfIconsItens(this);
            this.EventInternal += this_EventInternal;
            this.SetProperty += this_SetProperty;

            this.ScrollBar = new AraScrollBar(this);

            this.Width = 500;
            this.Height = 500;
        }

        public AraGridOfIconsItens Itens;

        public static bool ArquivosHdCarregado = false;
        public override void LoadJS()
        {
            Tick vTick = Tick.GetTick();
            vTick.Session.AddJs("Ara2/Components/AraGridOfIcons/AraGridOfIcons.js");
        }

        private void this_EventInternal(String vFunction)
        {
            Tick Tick = Tick.GetTick();
            switch (vFunction.ToUpper())
            {
                case "CLICK":
                    Click.InvokeEvent(this,new EventArgs());
                    break;
                case "CLICKICO":
                    try
                    {
                        if (ClickIco.InvokeEvent!=null)
                            ClickIco.InvokeEvent(Itens[Convert.ToString(Tick.Page.Request["key"])]);
                    }
                    catch(Exception err)
                    {
                        throw new Exception("Error on CLICKICO.\n" + err.Message);
                    }
                break;

                case "CHANGEPOSITIONICO":
                    if (ChangePositionIco.InvokeEvent!=null)
                        ChangePositionIco.InvokeEvent(Itens[Convert.ToString(Tick.Page.Request["key"])]);
                break;
            }
        }

        private void this_SetProperty(String vNome, dynamic vValor)
        {
            switch (vNome.ToUpper())
            {
                case "GETPOSICONS()":
                    {
                        if (vValor != "")
                        {
                            foreach (dynamic vObj in Json.DynamicJson.Parse(vValor))
                            {
                                try
                                {
                                    string vkey = vObj.key.ToString();
                                    int vtop = Convert.ToInt32(vObj.top);
                                    int vleft = Convert.ToInt32(vObj.left);

                                    Itens[vkey].SetInternalValue_LeftTop(vleft, vtop);
                                }
                                catch { }
                            }
                        }
                    }
                    break;
            }
        }
        
        // Fim Padão 

        #region Events
        [AraDevEvent]
        public AraComponentEvent<EventHandler> Click;

        public delegate void DEventIco(AraGridOfIconsIten Iten);
        [AraDevEvent]
        public AraComponentEvent<DEventIco> ClickIco;
        [AraDevEvent]
        public AraComponentEvent<DEventIco> ChangePositionIco;            
        #endregion

        public void RemoveInterface()
        {
            this.TickScriptCall();
            Tick.GetTick().Script.Send(" vObj.RemoveInterface(); \n");
        }

        private bool _Visible = true;
        [AraDevProperty(true)]
        [PropertySupportLayout]
        public bool Visible
        {
            set
            {
                _Visible = value;
                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetVisible(" + (_Visible == true ? "true" : "false") + "); \n");
            }
            get { return _Visible; }
        }

        private int _IcoWidth=64;
        [AraDevProperty(64)]
        public int IcoWidth
        {
            get { return _IcoWidth; }
            set
            {
                _IcoWidth = value;
                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetIcoWidth(" + _IcoWidth + "); \n");
            }
        }

        private int _IcoHeight=64;
        [AraDevProperty(64)]
        public int IcoHeight
        {
            get { return _IcoHeight; }
            set
            {
                _IcoHeight = value;
                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetIcoHeight(" + _IcoHeight + "); \n");
            }
        }

        private string _TextBackgroundColor = "";
        [AraDevProperty("")]
        public string TextBackgroundColor
        {
            get { return _TextBackgroundColor; }
            set
            {
                _TextBackgroundColor = value;

                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetTextBackgroundColor('" + AraTools.StringToStringJS(_TextBackgroundColor) + "'); \n");
            }
        }

        private string _TextColor = "black";
        [AraDevProperty("black")]
        public string TextColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;

                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetTextColor('" + AraTools.StringToStringJS(_TextColor) + "'); \n");
            }
        }

        private string _TextFont = "Arial";
        [AraDevProperty("Arial")]
        public string TextFont
        {
            get { return _TextFont; }
            set
            {
                _TextFont = value;

                this.TickScriptCall();
                Tick.GetTick().Script.Send(" vObj.SetTextFont('" + AraTools.StringToStringJS(_TextFont) + "'); \n");
            }
        }


        #region Ara2Dev

        private string _Name = "";
        [AraDevProperty("")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private AraEvent<DStartEditPropertys> _StartEditPropertys = null;
        public AraEvent<DStartEditPropertys> StartEditPropertys
        {
            get
            {
                if (_StartEditPropertys == null)
                {
                    _StartEditPropertys = new AraEvent<DStartEditPropertys>();
                    this.Click += this_ClickEdit;
                }

                return _StartEditPropertys;
            }
            set
            {
                _StartEditPropertys = value;
            }
        }
        private void this_ClickEdit(object sender, EventArgs e)
        {
            if (_StartEditPropertys.InvokeEvent != null)
                _StartEditPropertys.InvokeEvent(this);
        }

        private AraEvent<DStartEditPropertys> _ChangeProperty = new AraEvent<DStartEditPropertys>();
        public AraEvent<DStartEditPropertys> ChangeProperty
        {
            get
            {
                return _ChangeProperty;
            }
            set
            {
                _ChangeProperty = value;
            }
        }

       
        #endregion

    }

   
}
