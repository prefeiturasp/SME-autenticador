using System.Web.UI.WebControls;
using System.Web.UI;

namespace Autenticador.Web.WebProject
{
    public class CustomTreeView : TreeView
    {
 
    }

    public class CustomTreeNode : TreeNode
    {
        public TextBox TxtNovo
        {
            get
            {
                return m_pTextBox;
            }
        }

        private TextBox m_pTextBox;

        private static TextBox CreateTextBox()
        {
            TextBox pTextBox = new TextBox {ID = "txtNovo"};
            return pTextBox;
        }

        public CustomTreeNode()
        {
            m_pTextBox = CreateTextBox();
        }

        public CustomTreeNode(string text)
            : base(text)
        {
            m_pTextBox = CreateTextBox();
        }

        public CustomTreeNode(string text, string value)
            : base(text, value)
        {
            m_pTextBox = CreateTextBox();
        }

        protected internal CustomTreeNode(TreeView owner, bool isRoot)
            : base(owner, isRoot)
        {
            m_pTextBox = CreateTextBox();
        }

        protected override void RenderPostText(HtmlTextWriter writer)
        {
            base.RenderPostText(writer);

            if (m_pTextBox != null)
                m_pTextBox.RenderControl(writer);
        }
    }
}