using System;
using Menu;
using Menu.Remix.MixedUI;
using Menu.Remix.MixedUI.ValueTypes;
using RWCustom;
using UnityEngine;

namespace ExpandedIDs
{
    internal class Options : OptionInterface
    {
        private readonly RainWorld rainWorld;

       
        public Options()
        {
            this.rainWorld = rainWorld;   
        }

        public override void Initialize()
        {
            base.Initialize();

            Tabs = [new OpTab(this)];

            var title = new OpLabel(new Vector2(0f, 345f), new Vector2(600f, 30f), "SPAWN PARAMETERS", FLabelAlignment.Center, true)
            {
                verticalAlignment = OpLabel.LabelVAlignment.Center
            };
            title.label.shader = Custom.rainWorld.Shaders["MenuText"];

            

         
        }
    }
}
