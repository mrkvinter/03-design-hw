using System.Windows.Forms;

namespace TagCloud.Client
{
    class GuiClient
        : Form, IClient
    {
        public GuiClient(TagContainer container = null)
        {
            Name = "GUI";
        }
        
        public void Run()
        {
            Application.Run(this);
        }
    }
}
