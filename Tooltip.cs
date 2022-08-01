using FrooxEngine;

namespace Tooltippery
{
    public class Tooltip
    {
        private Slot slot;
        private TextRenderer textRenderer;
        public Tooltip(string label, Slot parent, BaseX.float3 localPosition)
        {
            // text slot for the tooltip
            slot = parent.AddLocalSlot("Local Tooltip");
            slot.LocalPosition = localPosition;
            textRenderer = slot.AttachComponent<TextRenderer>();
            textRenderer.Text.Value = label;
            textRenderer.VerticalAlign.Value = CodeX.TextVerticalAlignment.Top;
            textRenderer.HorizontalAlign.Value = CodeX.TextHorizontalAlignment.Left;
            textRenderer.Size.Value = 200 * getScale();
            textRenderer.Bounded.Value = true;
            textRenderer.BoundsSize.Value = new BaseX.float2(700 * getScale(), 1);
            textRenderer.BoundsAlignment.Value = BaseX.Alignment.TopLeft;
            textRenderer.Color.Value = Tooltippery.config.GetValue(Tooltippery.textColor);

            // back panel slot
            Slot backPanelOffset = slot.AddLocalSlot("bgOffset");
            backPanelOffset.LocalPosition = new BaseX.float3(0, 0, 1);
            Slot backPanel = backPanelOffset.AddLocalSlot("Background");
            QuadMesh quad = backPanel.AttachComponent<QuadMesh>();
            MeshRenderer meshRenderer = backPanel.AttachComponent<MeshRenderer>();
            meshRenderer.Mesh.Target = quad;
            BoundingBoxDriver sizeDriver = slot.AttachComponent<BoundingBoxDriver>();
            sizeDriver.BoundedSource.Target = textRenderer;
            sizeDriver.Size.Target = backPanel.Scale_Field;
            sizeDriver.Center.Target = backPanel.Position_Field;
            sizeDriver.Padding.Value = new BaseX.float3(8 * getScale(), 8 * getScale(), 0);
            
            UI_UnlitMaterial mat = backPanel.AttachComponent<UI_UnlitMaterial>();
            mat.Tint.Value = Tooltippery.config.GetValue(Tooltippery.bgColor);
            meshRenderer.Material.Target = mat;

            slot.GlobalScale = slot.World.LocalUserViewScale * new BaseX.float3(.001f, .001f, .001f);
        }

        private float getScale()
        {
            return (float) (Tooltippery.config.GetValue(Tooltippery.textScale) * (slot.GetComponentInParents<UserspaceRadiantDash>() == null? 1 : 2.5));
        }

        public void hide()
        {
            slot.Destroy();
        }

        public void setText(string newLabel)
        {
            textRenderer.Text.Value = newLabel;
        }
    }
}